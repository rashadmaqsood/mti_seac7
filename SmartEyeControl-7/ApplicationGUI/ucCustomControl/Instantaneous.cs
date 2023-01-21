using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DLMS;
using SmartEyeControl_7;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SmartEyeControl_7.comm;
using SharedCode.Comm.DataContainer;
using DLMS.Comm;
using SharedCode.Comm.Param;
using SharedCode.Comm.HelperClasses;
using SharedCode.Controllers;
using SmartEyeControl_7.DB;
using SEAC.Common;
using SharedCode.Common;
using SmartEyeControl_7.Reporting;
using OptocomSoftware.Reporting;

namespace ucCustomControl
{
    public partial class Instantaneous : UserControl
    {
        string[] event_names = {
                                   #region event_names

		                           "Imbalance Volt",
                                   "Phase Sequence",
                                   "Reverse Polarity",
                                   "Phase Fail",
                                   "Under Volt",
                                   "Over Volt",
                                   "Over Current",
                                   "High Neutral Current",
                                   "Over Load",
                                   "Reverse Energy",
                                   "Tamper Energy",
                                   "CT Fail",
                                   "PT Fail",
                                   "Software Login",
                                   "Power Fail",
                                   "Power Fail End",
                                   "One Wire Tamper",
                                   "Meter On Load",
                                   "Meter On Load End",
                                   "Under Volt End",
                                   "MDI_Exceed",
                                   "Ststem Reset",
                                   "System Problems",
                                   "MDI Reset",
                                   "Parameters",
                                   "Password",
                                   "Customer Code",
                                   "Time Change",
                                   "Window Sequence Change",
                                   "Over Volt End",
                                   "Billing Register Over Flow",
                                   "Param Error",
                                   "Power Factor Change",
                                   "Battery Low Off",
                                   "Door Open",
                                   "Power Down",
                                   "Record Recovered",
                                   "Time Based Event 1",
                                   "Time Based Event 2",
                                   "CONTACTOR STATUS ON",
                                   "CONTACTOR STATUS OFF",
                                   "Power Down End",
                                   "Reverse Energy End",
                                   "Tamper Energy End",
                                   "Over Load End",
                                   "MDI Occurance",
                                   "Billing Register Error",
                                   "Phase Fail End" 

	                               #endregion
                               };
        bool HidePrintReportButtons;
        // System.Drawing.Image backImage = System.Drawing.Image.FromFile(Environment.CurrentDirectory + @"\images\backImage.jpg");
        ds_Ins dataSet_INS = new ds_Ins();
        ds_Ins dataSet_INS_new = new ds_Ins();
        string ManufacturingID = null;
        string MeterDT = null;
        InstantaneousData ins_data = null;
        //ds_all_in_one ds_All_In_One = new ds_all_in_one(); //Azeem

        Param_Customer_Code obj_CustomerCode;
        List<StOBISCode> MDIOBISCodes = new List<StOBISCode>();
        List<Class_5> MDIValues = new List<Class_5>();

        #region NewInsVariables

        int packetSize_Total = 0;
        int packetSize_Energies = 0;
        int tariffCount = 1;
        int gridCount = 0;
        int TOTAL = 0;
        AllInOne allInOne = new AllInOne();

        #endregion

        List<string> selected_Quantities = new List<string>();
        //================================================================
        //================================================================
        DLMS_Application_Process Application_Process;
        private ConnectionManager connectionManager;
        private ConnectionController ConnController;
        private ApplicationProcess_Controller AP_Controller;
        private ApplicationController application_Controller;
        private ParameterController Param_Controller;
        private BillingController Instantanous_Controller_1;
        private InstantaneousController Instantanous_Controller;
        List<CB_DayRecord> CBdayRecord;
        private ds_CB_Day_Record ds_day_record = new ds_CB_Day_Record();
        private DS_TL_LOAD_PROFILE ds_tl_load_profile = new DS_TL_LOAD_PROFILE();

        private DBConnect MyDataBase;
        private string currentQuantityLabel;
        private bool GetCompleted = false;
        private int record_index = 0;
        InstantaneousData Ins_data;

        ApplicationRight _Rights = null;
        // NEW CODE
        ReadRawData rawData = new ReadRawData();

        public ApplicationController Application_Controller
        {
            get
            {
                //if (application_Controller == null)
                //    throw new Exception("Application not Initialized properly");
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

        private void Application_Controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                ///Okay IsIOBusy Status
                if ("IsIOBusy".Equals(e.PropertyName) && sender is ApplicationController)
                {
                    UpdateReadWriteStatus(Application_Controller.IsIOBusy);
                }

            }
            catch (Exception ex)
            {
            }
        }

        #region BckWorker

        private Thread InstantanouseThread;
        private Exception ex;

        #endregion

        //================================================================
        //================================================================
        Instantaneous_Class Instantaneous_Class_obj = new Instantaneous_Class();
        InstantaneousMDI obj_InstantaneousMDI = new InstantaneousMDI();
        Param_Energy_Parameter Param_Energy_Parameter_obj = new Param_Energy_Parameter();
        List<DBConnect.Insert_Record> List_Records = new List<DBConnect.Insert_Record>();
        Dictionary<string, int> D_Instantaneous = new Dictionary<string, int>();
        //================================================================
        //================================================================

        public Instantaneous()
        {
            InitializeComponent();
            MDIOBISCodes.Add(Get_Index.DEMAND_ACTIVE_ABSOLUTE);
            MDIOBISCodes.Add(Get_Index.DEMAND_ACTIVE_IMPORT);
            MDIOBISCodes.Add(Get_Index.DEMAND_ACTIVE_EXPORT);
            MDIOBISCodes.Add(Get_Index.DEMAND_REACTIVE_ABSOLUTE);
            MDIOBISCodes.Add(Get_Index.DEMAND_REACTIVE_IMPORT);
            MDIOBISCodes.Add(Get_Index.DEMAND_REACTIVE_EXPORT);

            for (int i = 0; i < listInstantaneousMDI.Items.Count; i++)
            {
                MDIValues.Add(new Class_5(MDIOBISCodes[i]));
                cbxMditoMonitor.Items.Add(Enum.GetName(typeof(Get_Index),MDIOBISCodes[i].OBISIndex));
            }
            cbxMditoMonitor.SelectedIndex = 0;

            ApplicationLookAndFeel.UseTheme(this);

            btn_InsOldReport.Visible = false;
            // initializeEventgrid();
            obj_CustomerCode = new Param_Customer_Code();
            Instantanous_Controller_1 = new BillingController();
            // tcontrolMain.TabPages.RemoveAt(3); //remove salman sb tab 

            // if (application_Controller.isSinglePhase)
            // {
            //     // update grid according to single phase meters
            // }
            // else
            // {
            //     // update grid according to three phase meters
            // }

            #region DataGrid Sorting Disable
            foreach (DataGridViewColumn column in grid_CurrentMDI.Columns)
            { column.SortMode = DataGridViewColumnSortMode.NotSortable; }
            foreach (DataGridViewColumn column in grid_MDI.Columns)
            { column.SortMode = DataGridViewColumnSortMode.NotSortable; }
            foreach (DataGridViewColumn column in grid_misc.Columns)
            { column.SortMode = DataGridViewColumnSortMode.NotSortable; }
            foreach (DataGridViewColumn column in grid_Instanstanouse.Columns)
            { column.SortMode = DataGridViewColumnSortMode.NotSortable; }
            #endregion

            Instantaneous_Class_obj.MDIsToShow = PopulateMDIsToRead();
            chkLstMDIs.DisplayMember = "QuantityName";
            chkLstMDIs.Items.AddRange(Instantaneous_Class_obj.MDIsToShow.ToArray());
        }

        #region Read Instantanouse Reading Data

        private void btn_read_all_instantaneous_values_Click(object sender, System.EventArgs e)
        {
            btn_InsOldReport.Enabled = false;
            #region Selected quantities to list

            //clear previously selected quantites for instantaneous report
            btn_InsOldReport.Visible = false;
            selected_Quantities.Clear();
            //now storing selected quantities names 
            if (check_AllPhy.Checked)
                selected_Quantities.Add("All");
            if (check_Voltage.Checked)
                selected_Quantities.Add("Voltage");
            if (check_Current.Checked)
                selected_Quantities.Add("Current");
            if (check_ActivePower.Checked)
                selected_Quantities.Add("Active Power");
            if (check_ReactivePower.Checked)
                selected_Quantities.Add("Reactive Power");
            if (check_Apparent.Checked)
                selected_Quantities.Add("Apparant Power");
            if (check_Powerfactor.Checked)
                selected_Quantities.Add("Power Factor");
            if (check_misc.Checked)
                selected_Quantities.Add("MISC");
            if (check_Mdi.Checked)
                selected_Quantities.Add("MDI");
            #endregion
            int currentPageIndex = 0;
            try
            {
                if (tcontrolMain.SelectedIndex > -1)
                {
                    currentPageIndex = tcontrolMain.SelectedIndex;
                }
                dataSet_INS.Clear();
                ClearGrid(grid_CurrentMDI);
                pb_ins.Visible = true;
                lbl_PbStatus.Visible = true;
                if (Application_Process.Is_Association_Developed)
                {
                    if (bckWorker_Instantanouse.IsBusy)   ///already busy
                    {
                        DialogResult MsgResult = MessageBox.Show(this.Parent, "Instantanouse read process already continued,abort current process?", "Process Abort", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (MsgResult == DialogResult.OK)
                        {
                            bckWorker_Instantanouse.CancelAsync();
                            Thread.Sleep(5);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        bckWorker_Instantanouse.RunWorkerAsync();
                        Application_Controller.IsIOBusy = true;
                    }
                }
                else
                {
                    ///MessageBox.Show("Create Association to Electrical via Management");
                    Notification Notifier = new Notification("Error", "Please connect meter");
                }

                tcontrolMain.SelectedIndex = currentPageIndex;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (bckWorker_Instantanouse.IsBusy)
                {
                    btn_read_all_instantaneous_values.Text = "Cancel";
                }
                else
                {
                    btn_read_all_instantaneous_values.Text = "Read Data";
                }
            }

        }

        private void bckWorker_Instantanouse_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.ex = null;
                this.InstantanouseThread = new Thread(this.bckWorker_Instantanouse_DoWorkHelper);
                InstantanouseThread.Name = "InstantanouseThread";
                InstantanouseThread.IsBackground = true;
                ///Disable Controls
                this.GpQuantity.Enabled = false;
                this.pb_ins.Enabled = true;
                ///lbl_PbStatus.Enabled = true;
                this.pb_ins.Visible = true;
                lbl_PbStatus.Text = "";
                ///___________
                InstantanouseThread.Start();
                while (bckWorker_Instantanouse.IsBusy && (InstantanouseThread.ThreadState != ThreadState.Aborted && InstantanouseThread.ThreadState != ThreadState.Stopped))
                {
                    if (bckWorker_Instantanouse.CancellationPending) ///True
                    {
                        InstantanouseThread.Abort();       ///Cancel Reading Billing
                        throw new Exception("Reading Instantanouse data process is aborted");
                    }
                    Application.DoEvents();
                    Thread.Sleep(1);
                }

                if (InstantanouseThread.ThreadState == ThreadState.Aborted && this.ex != null)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                ///Disable Controls
                this.GpQuantity.Enabled = true;
                this.pb_ins.Enabled = false;
                this.pb_ins.Visible = false;
                lbl_PbStatus.Text = "";
                ///___________
            }
        }

        private void bckWorker_Instantanouse_DoWorkHelper()
        {
            try
            {
                Get_Instantanouse_Data();
                if (check_I_AddtoDB.Checked)
                {
                    Save_Instantanouse_Data();
                }
                if (!HidePrintReportButtons)
                {
                    // get customer code
                    obj_CustomerCode.Customer_Code_String = (string)Application_Controller.Param_Controller.GETString_Any(obj_CustomerCode, Get_Index.Customer_Reference_No, 2);

                    // get active season
                    Instantaneous_Class_obj.Dummy_String = (string)Application_Controller.InstantaneousController.GETString_Any(Instantaneous_Class_obj, Get_Index.Active_Season, 2);
                    byte[] tempBytes = Encoding.ASCII.GetBytes(Instantaneous_Class_obj.Dummy_String);
                    Instantaneous_Class_obj.Active_Season = tempBytes[1];
                }
            }
            catch (Exception ex)
            {
                this.ex = ex;
                Thread.CurrentThread.Abort();
            }
        }

        private void bckWorker_Instantanouse_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Notification Notifier;
                Cursor.Current = Cursors.Arrow;
                if (e.Error == null)
                {
                    Show_Instantanose_Data();
                    if (!HidePrintReportButtons)
                    {
                        btn_InsOldReport.Visible = true;
                        btn_InsOldReport.Enabled = true;
                    }
                    //MessageBox.Show("Instananeous Values UPDATED");
                    if (GetCompleted)
                        Notifier = new Notification("Process Completed", "Instananeous Values Successfuly UPDATED");
                    else
                        Notifier = new Notification("Process Failed", "Error getting Instananeous Values ");
                }
                else
                    throw e.Error;
            }
            catch (Exception ex)
            {
                /// MessageBox.Show(ex.Message);
                Notification Notifier = new Notification("Error",
                    String.Format("Error reading Instantanous\r\n{0}", ex.Message));
            }
            finally
            {
                if (bckWorker_Instantanouse.IsBusy &&
                    (InstantanouseThread.ThreadState != ThreadState.Aborted &&
                    InstantanouseThread.ThreadState != ThreadState.Stopped))
                {
                    btn_read_all_instantaneous_values.Text = "Cancel";
                    Application_Controller.IsIOBusy = true;
                }
                else
                {
                    btn_read_all_instantaneous_values.Text = "Read Data";
                    Application_Controller.IsIOBusy = false;
                }
            }
        }

        #endregion

        private void Get_Instantanouse_Data()
        {
            try
            {
                pb_ins.Visible = true;
                lbl_PbStatus.Visible = true;
                Intantaneous_values_reinitialize();
                int total_commands = 0;
                pb_ins.Value = 0;

                #region DateTime

                Param_Clock_Caliberation Meter_Clock = new Param_Clock_Caliberation();
                currentQuantityLabel = "Requesting Meter Date Time";

                // Param_Controller.GET_MeterClock(ref Meter_Clock);
                Param_Controller.GET_MeterClock_Date_Time(ref Meter_Clock);     // Read Only Clock DataTime
                Instantaneous_Class_obj.Local_Date_Time = Meter_Clock.Set_Time;

                #region ADD TO DATASET METER DATETIME
                MeterDT = Meter_Clock.Set_Time.ToString();
                dataSet_INS.dataTable_M.Rows.Add();
                dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Meter DateTime";
                dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Meter_Clock.Set_Time.ToString();
                #endregion

                #endregion

                #region Manufacturing_ID

                currentQuantityLabel = "Requesting Meter Serial Number";
                Instantaneous_Class_obj.Manufacturing_ID = Instantanous_Controller.GET_Any_string(ref Instantaneous_Class_obj, Get_Index.Manufacturing_ID, 2,1);
                update_Progress_bar(pb_ins, total_commands);
                // update_Progress_bar(pb_ins, total_commands);
                // lbl_MeterSerialNo.Text = Instantaneous_Class_obj.Manufacturing_ID; Application.DoEvents();
                #region ADD TO DATASET MANUFACTURING ID

                ManufacturingID = application_Controller.ConnectionManager.ConnectionInfo.MSN;
                // dataSet_INS.dataTable_M.Rows.Add();
                // dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Manufacturing ID";
                // dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Manufacturing_ID.ToString();

                #endregion

                #endregion

                #region Voltage

                if (check_Voltage.Checked)
                {
                    currentQuantityLabel = "Requesting Voltages";
                    total_commands = 3;

                    Instantaneous_Class_obj.voltage_PhaseA = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Voltage_Ph_A, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.voltage_PhaseB = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Voltage_Ph_B, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.voltage_PhaseC = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Voltage_Ph_C, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.voltage_total = ((Instantaneous_Class_obj.voltage_PhaseA + Instantaneous_Class_obj.voltage_PhaseA + Instantaneous_Class_obj.voltage_PhaseC) / 3);

                    #region Add_To_DB_VOLTAGE

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Voltage_Ph_A), Instantaneous_Class_obj.voltage_PhaseA, "V", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Voltage_Ph_B), Instantaneous_Class_obj.voltage_PhaseB, "V", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Voltage_Ph_C), Instantaneous_Class_obj.voltage_PhaseC, "V", 0, true);

                    DB_Add_DataSet_toWrite(111001, Instantaneous_Class_obj.voltage_total, "V", 0, true);

                    #endregion

                    #region ADD TO DATASET VOLTAGE

                    dataSet_INS.dataTable_P.Rows.Add();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Quantity = "Voltage (V)";
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_A = Instantaneous_Class_obj.voltage_PhaseA.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_B = Instantaneous_Class_obj.voltage_PhaseB.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_C = Instantaneous_Class_obj.voltage_PhaseC.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_Total = Instantaneous_Class_obj.voltage_total.ToString("f2");

                    #endregion
                }

                #endregion

                #region Current

                if (check_Current.Checked)
                {
                    total_commands = 3;
                    pb_ins.Value = 0;
                    currentQuantityLabel = "Requesting Currents";

                    Instantaneous_Class_obj.current_PhaseA = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Current_Ph_A, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.current_PhaseB = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Current_Ph_B, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.current_PhaseC = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Current_Ph_C, 0); update_Progress_bar(pb_ins, total_commands);

                    Instantaneous_Class_obj.current_total = ((Instantaneous_Class_obj.current_PhaseA + Instantaneous_Class_obj.current_PhaseB + Instantaneous_Class_obj.current_PhaseC));

                    #region Add_To_DB_CURRENT

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Current_Ph_A), Instantaneous_Class_obj.current_PhaseA, "A", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Current_Ph_B), Instantaneous_Class_obj.current_PhaseB, "A", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Current_Ph_C), Instantaneous_Class_obj.current_PhaseC, "A", 0, true);

                    DB_Add_DataSet_toWrite(111002, Instantaneous_Class_obj.current_total, "A", 0, true);

                    #endregion

                    #region ADD TO DATASET CURRENT

                    dataSet_INS.dataTable_P.Rows.Add();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Quantity = "Current (A)";
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_A = Instantaneous_Class_obj.current_PhaseA.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_B = Instantaneous_Class_obj.current_PhaseB.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_C = Instantaneous_Class_obj.current_PhaseC.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_Total = Instantaneous_Class_obj.current_total.ToString("f2");

                    #endregion

                }

                #endregion

                #region Active Power

                if (check_ActivePower.Checked)
                {
                    total_commands = 4;
                    pb_ins.Value = 0;

                    currentQuantityLabel = "Requesting Active Power Positive";
                    Instantaneous_Class_obj.active_powerPositive_PhaseA = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_A_Pos, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.active_powerPositive_PhaseB = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_B_Pos, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.active_powerPositive_PhaseC = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_C_Pos, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.active_powerPositive_PhaseTL = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Total_Pos, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                    total_commands = 4;
                    pb_ins.Value = 0;

                    currentQuantityLabel = "Requesting Active Power Negative";
                    Instantaneous_Class_obj.active_powerNegative_PhaseA = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_A_Neg, 0)) / 1000; update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.active_powerNegative_PhaseB = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_B_Neg, 0)) / 1000; update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.active_powerNegative_PhaseC = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Ph_C_Neg, 0)) / 1000; update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.active_powerNegative_PhaseTL = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_Total_Neg, 0)) / 1000; update_Progress_bar(pb_ins, total_commands);

                    #region Add_To_DB_ACTIVE POWER +

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Power_Ph_A_Pos), Instantaneous_Class_obj.active_powerPositive_PhaseA, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Power_Ph_B_Pos), Instantaneous_Class_obj.active_powerPositive_PhaseB, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Power_Ph_C_Pos), Instantaneous_Class_obj.active_powerPositive_PhaseC, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Power_Total_Pos), Instantaneous_Class_obj.active_powerPositive_PhaseTL, "W", 0, true);

                    #endregion

                    #region ADD_TO_DB_ACTIVE POWER NEGATIVE

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Power_Ph_A_Neg), Instantaneous_Class_obj.active_powerNegative_PhaseA , "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Power_Ph_B_Neg), Instantaneous_Class_obj.active_powerNegative_PhaseB , "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Power_Ph_C_Neg), Instantaneous_Class_obj.active_powerNegative_PhaseC , "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Power_Total_Neg), Instantaneous_Class_obj.active_powerNegative_PhaseTL, "W", 0, true);


                    #endregion

                    #region ADD TO DATASET ACTIVE POWER POSITIVE

                    dataSet_INS.dataTable_P.Rows.Add();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Quantity = "Active Power Positive (KW)";
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_A = Instantaneous_Class_obj.active_powerPositive_PhaseA.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_B = Instantaneous_Class_obj.active_powerPositive_PhaseB.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_C = Instantaneous_Class_obj.active_powerPositive_PhaseC.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_Total = Instantaneous_Class_obj.active_powerPositive_PhaseTL.ToString("f2");

                    #endregion

                    #region ADD TO DATASET ACTIVE POWER NEGATIVE

                    dataSet_INS.dataTable_P.Rows.Add();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Quantity = "Active Power Negative (KW)";
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_A = Instantaneous_Class_obj.active_powerNegative_PhaseA .ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_B = Instantaneous_Class_obj.active_powerNegative_PhaseB .ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_C = Instantaneous_Class_obj.active_powerNegative_PhaseC .ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_Total = Instantaneous_Class_obj.active_powerNegative_PhaseTL.ToString("f2");

                    #endregion
                }

                #endregion

                #region Reactive Power

                if (check_ReactivePower.Checked)
                {
                    currentQuantityLabel = "Requesting Reactive Power Positive";
                    Instantaneous_Class_obj.reactive_powerPositive_PhaseA = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_A_Pos, 0) / 1000); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.reactive_powerPositive_PhaseB = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_B_Pos, 0) / 1000); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.reactive_powerPositive_PhaseC = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_C_Pos, 0) / 1000); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.reactive_powerPositive_PhaseTL = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Total_Pos, 0) / 1000); update_Progress_bar(pb_ins, total_commands);

                    currentQuantityLabel = "Requesting Reactive Power Negative";
                    Instantaneous_Class_obj.reactive_powerNegative_PhaseA = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_A_Neg, 0) / 1000); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.reactive_powerNegative_PhaseB = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_B_Neg, 0) / 1000); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.reactive_powerNegative_PhaseC = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Ph_C_Neg, 0) / 1000); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.reactive_powerNegative_PhaseTL = (Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Reactive_Power_Total_Neg, 0) / 1000); update_Progress_bar(pb_ins, total_commands);

                    #region ADD_TO_DB_ReaCTIVE POWER POSITIVE

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Reactive_Power_Ph_A_Pos), Instantaneous_Class_obj.reactive_powerPositive_PhaseA, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Reactive_Power_Ph_B_Pos), Instantaneous_Class_obj.reactive_powerPositive_PhaseB, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Reactive_Power_Ph_C_Pos), Instantaneous_Class_obj.reactive_powerPositive_PhaseC, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Reactive_Power_Total_Pos), Instantaneous_Class_obj.reactive_powerPositive_PhaseTL, "W", 0, true);

                    #endregion

                    #region ADD_TO_DB_REACTIVE POWER NEGATIVE

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Reactive_Power_Ph_A_Neg), Instantaneous_Class_obj.reactive_powerNegative_PhaseA, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Reactive_Power_Ph_B_Neg), Instantaneous_Class_obj.reactive_powerNegative_PhaseB, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Reactive_Power_Ph_C_Neg), Instantaneous_Class_obj.reactive_powerNegative_PhaseC, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Reactive_Power_Total_Neg), Instantaneous_Class_obj.reactive_powerNegative_PhaseTL, "W", 0, true);

                    #endregion

                    #region ADD TO DATASET REACTIVE POWER POSITIVE

                    dataSet_INS.dataTable_P.Rows.Add();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Quantity = "Reactive Power Positive (KVar)";
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_A = Instantaneous_Class_obj.reactive_powerPositive_PhaseA.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_B = Instantaneous_Class_obj.reactive_powerPositive_PhaseB.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_C = Instantaneous_Class_obj.reactive_powerPositive_PhaseC.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_Total = Instantaneous_Class_obj.reactive_powerPositive_PhaseTL.ToString("f2");

                    #endregion

                    #region ADD TO DATASET REACTIVE POWER NEGATIVE

                    dataSet_INS.dataTable_P.Rows.Add();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Quantity = "Reactive Power Negative (KVar)";
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_A = Instantaneous_Class_obj.reactive_powerNegative_PhaseA.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_B = Instantaneous_Class_obj.reactive_powerNegative_PhaseB.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_C = Instantaneous_Class_obj.reactive_powerNegative_PhaseC.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_Total = Instantaneous_Class_obj.reactive_powerNegative_PhaseTL.ToString("f2");

                    #endregion
                }

                #endregion

                #region Apparent Power

                if (check_Apparent.Checked)
                {
                    total_commands = 4;
                    pb_ins.Value = 0;

                    currentQuantityLabel = "Requesting Apparent Powers";
                    Instantaneous_Class_obj.Apparent_Power_Ph_A = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index._Apparent_Power_Phase1, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.Apparent_Power_Ph_B = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index._Apparent_Power_Phase2, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.Apparent_Power_Ph_C = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index._Apparent_Power_Phase3, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.Apparent_Power_Tot = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index._Apparent_Power_Total, 0) / 1000; update_Progress_bar(pb_ins, total_commands);

                    #region Add_To_DB_Apparent Powers
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index._Apparent_Power_Phase1), Instantaneous_Class_obj.Apparent_Power_Ph_A, "W", 0, true);

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index._Apparent_Power_Phase2), Instantaneous_Class_obj.Apparent_Power_Ph_B, "W", 0, true);

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index._Apparent_Power_Phase3), Instantaneous_Class_obj.Apparent_Power_Ph_C, "W", 0, true);

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index._Apparent_Power_Total), Instantaneous_Class_obj.Apparent_Power_Tot, "W", 0, true);

                    #endregion

                    #region ADD TO DATASET APPARANT POWER
                    dataSet_INS.dataTable_P.Rows.Add();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Quantity = "Apparant Power (kva)";
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_A = Instantaneous_Class_obj.Apparent_Power_Ph_A.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_B = Instantaneous_Class_obj.Apparent_Power_Ph_B.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_C = Instantaneous_Class_obj.Apparent_Power_Ph_C.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_Total = Instantaneous_Class_obj.Apparent_Power_Tot.ToString("f2");
                    #endregion

                }
                #endregion

                #region Power Factor

                if (check_Powerfactor.Checked)
                {
                    total_commands = 8;
                    pb_ins.Value = 0;

                    currentQuantityLabel = "Requesting Power Factors";
                    Instantaneous_Class_obj.Power_Factor_Ph_A = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Power_Factor_Ph_A, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.Power_Factor_Ph_B = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Power_Factor_Ph_B, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.Power_Factor_Ph_C = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Power_Factor_Ph_C, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.Power_Factor_All = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Power_Factor_All, 0); update_Progress_bar(pb_ins, total_commands);

                    Instantaneous_Class_obj.Cumulative_Tariff1_PowerFactor = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff1_PowerFactor, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.Cumulative_Tariff2_PowerFactor = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff2_PowerFactor, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.Cumulative_Tariff3_PowerFactor = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff3_PowerFactor, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.Cumulative_Tariff4_PowerFactor = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff4_PowerFactor, 0); update_Progress_bar(pb_ins, total_commands);
                    Instantaneous_Class_obj.Cumulative_TariffTL_PowerFactor = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_TariffTL_PowerFactor, 0); update_Progress_bar(pb_ins, total_commands);

                    #region ADD_TO_DB_POWER_FACTOR

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Power_Factor_Ph_A), Instantaneous_Class_obj.Power_Factor_Ph_A, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Power_Factor_Ph_B), Instantaneous_Class_obj.Power_Factor_Ph_B, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Power_Factor_Ph_C), Instantaneous_Class_obj.Power_Factor_Ph_C, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Power_Factor_All), Instantaneous_Class_obj.Power_Factor_All, "W", 0, true);

                    #endregion

                    #region ADD_TO_DB_RUNNING_POWER_FACTOR

                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff1_PowerFactor), Instantaneous_Class_obj.Cumulative_Tariff1_PowerFactor, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff2_PowerFactor), Instantaneous_Class_obj.Cumulative_Tariff2_PowerFactor, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff3_PowerFactor), Instantaneous_Class_obj.Cumulative_Tariff3_PowerFactor, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff4_PowerFactor), Instantaneous_Class_obj.Cumulative_Tariff4_PowerFactor, "W", 0, true);
                    DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_TariffTL_PowerFactor), Instantaneous_Class_obj.Cumulative_TariffTL_PowerFactor, "W", 0, true);

                    #endregion

                    #region ADD TO DATASET POWER FACTOR

                    dataSet_INS.dataTable_P.Rows.Add();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Quantity = "Power Factor";
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_A = Instantaneous_Class_obj.Power_Factor_Ph_A.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_B = Instantaneous_Class_obj.Power_Factor_Ph_B.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_C = Instantaneous_Class_obj.Power_Factor_Ph_C.ToString("f2");
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_Total = Instantaneous_Class_obj.Power_Factor_All.ToString("f2");

                    #endregion

                    #region ADD TO DATASET RUNNING POWER FACTOR

                    dataSet_INS.dataTable_T.Rows.Add();
                    dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].Quantity = "Running Power Factor";
                    dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T1 = Instantaneous_Class_obj.Cumulative_Tariff1_PowerFactor.ToString();
                    dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T2 = Instantaneous_Class_obj.Cumulative_Tariff2_PowerFactor.ToString();
                    dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T3 = Instantaneous_Class_obj.Cumulative_Tariff3_PowerFactor.ToString();
                    dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T4 = Instantaneous_Class_obj.Cumulative_Tariff4_PowerFactor.ToString();
                    dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].TL = Instantaneous_Class_obj.Cumulative_TariffTL_PowerFactor.ToString("f3");

                    #endregion
                }

                #endregion

                #region Misc

                if (check_misc.Checked)
                {
                    Get_MISC(pb_ins, total_commands);
                }

                #endregion

                #region MDI's

                if (check_Mdi.Checked)
                {
                    if (Instantaneous_Class_obj.MDIsToShow == null)
                        Instantaneous_Class_obj.MDIsToShow = new List<QuantityMDI>();
                    Instantaneous_Class_obj.MDIsToShow.Clear();
                    foreach (object mdi in chkLstMDIs.CheckedItems)
                    {
                        Instantaneous_Class_obj.MDIsToShow.Add((QuantityMDI)mdi);
                    }

                    if (Instantaneous_Class_obj.MDIsToShow != null)
                    {
                        int kWDivider = 1000;
                        foreach (QuantityMDI mdi in Instantaneous_Class_obj.MDIsToShow)
                        {
                            currentQuantityLabel = "Requesting " + mdi.QuantityName;
                            pb_ins.Value = 0;
                            total_commands = 5;
                            DateTime timeStamp = DateTime.MinValue;

                            mdi.QuantityIndex = mdi.QuantityIndex.Set_OBISCode_Feild_E(0);
                            mdi.TariffTL.Value = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, mdi.QuantityIndex, 0, ref timeStamp) / kWDivider; update_Progress_bar(pb_ins, total_commands);
                            mdi.TariffTL.TimeStamp = timeStamp;

                            mdi.QuantityIndex = mdi.QuantityIndex.Set_OBISCode_Feild_E(1);
                            mdi.Tariff1.Value = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, mdi.QuantityIndex, 0, ref timeStamp) / kWDivider; update_Progress_bar(pb_ins, total_commands);
                            mdi.Tariff1.TimeStamp = timeStamp;

                            mdi.QuantityIndex = mdi.QuantityIndex.Set_OBISCode_Feild_E(2);
                            mdi.Tariff2.Value = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, mdi.QuantityIndex, 0, ref timeStamp) / kWDivider; update_Progress_bar(pb_ins, total_commands);
                            mdi.Tariff2.TimeStamp = timeStamp;

                            mdi.QuantityIndex = mdi.QuantityIndex.Set_OBISCode_Feild_E(3);
                            mdi.Tariff3.Value = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, mdi.QuantityIndex, 0, ref timeStamp) / kWDivider; update_Progress_bar(pb_ins, total_commands);
                            mdi.Tariff3.TimeStamp = timeStamp;

                            mdi.QuantityIndex = mdi.QuantityIndex.Set_OBISCode_Feild_E(4);
                            mdi.Tariff4.Value = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, mdi.QuantityIndex, 0, ref timeStamp) / kWDivider; update_Progress_bar(pb_ins, total_commands);
                            mdi.Tariff4.TimeStamp = timeStamp;

                            #region Add To Data Set
                            dataSet_INS.dataTable_T.Rows.Add();
                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].Quantity = mdi.QuantityName;
                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T1 = mdi.Tariff1.Value.ToString();
                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T2 = mdi.Tariff2.Value.ToString();
                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T3 = mdi.Tariff3.Value.ToString();
                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T4 = mdi.Tariff4.Value.ToString();
                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].TL = mdi.TariffTL.Value.ToString();
                            #endregion
                        }
                    }


                    /*
                                        currentQuantityLabel = "Requesting Current Month MDI kW";

                                        Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff1_CurrentMonthMdiKw, 0, ref Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw);
                                        Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff2_CurrentMonthMdiKw, 0, ref Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff2_CurrentMonthMdiKw);
                                        Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff3_CurrentMonthMdiKw, 0, ref Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff3_CurrentMonthMdiKw);
                                        Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff4_CurrentMonthMdiKw, 0, ref Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff4_CurrentMonthMdiKw);
                                        Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_TariffTL_CurrentMonthMdiKw, 0, ref Instantaneous_Class_obj.TimeStamp_Cumulative_TariffTL_CurrentMonthMdiKw);

                                        // Using Data_Properties
                                        // Instantanous_Controller.TryGETValue_Any(Instantaneous_Class_obj, "Cumulative_Tariff1_CurrentMonthMdiKw", "TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw", Get_Index.Cumulative_Tariff1_CurrentMonthMdiKw, 0);
                                        // Instantanous_Controller.TryGETValue_Any(Instantaneous_Class_obj, "Cumulative_Tariff2_CurrentMonthMdiKw", "TimeStamp_Cumulative_Tariff2_CurrentMonthMdiKw", Get_Index.Cumulative_Tariff2_CurrentMonthMdiKw, 0);
                                        // Instantanous_Controller.TryGETValue_Any(Instantaneous_Class_obj, "Cumulative_Tariff3_CurrentMonthMdiKw", "TimeStamp_Cumulative_Tariff3_CurrentMonthMdiKw", Get_Index.Cumulative_Tariff3_CurrentMonthMdiKw, 0);
                                        // Instantanous_Controller.TryGETValue_Any(Instantaneous_Class_obj, "Cumulative_Tariff4_CurrentMonthMdiKw", "TimeStamp_Cumulative_Tariff4_CurrentMonthMdiKw", Get_Index.Cumulative_Tariff4_CurrentMonthMdiKw, 0);
                                        // Instantanous_Controller.TryGETValue_Any(Instantaneous_Class_obj, "Cumulative_TariffTL_CurrentMonthMdiKw", "TimeStamp_Cumulative_TariffTL_CurrentMonthMdiKw", Get_Index.Cumulative_TariffTL_CurrentMonthMdiKw, 0);

                                        Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw = Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw / 1000;
                                        Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKw = Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw / 1000;
                                        Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKw = Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw / 1000;
                                        Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKw = Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw / 1000;
                                        Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKw = Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw / 1000;

                                        total_commands = 4;
                                        pb_ins.Value = 0;

                                        currentQuantityLabel = "Requesting Current Month MDI KVar";
                                        Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKvar = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff1_CurrentMonthMdiKvar, 0, ref Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKvar); update_Progress_bar(pb_ins, total_commands);
                                        Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKvar = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff2_CurrentMonthMdiKvar, 0, ref Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff2_CurrentMonthMdiKvar); update_Progress_bar(pb_ins, total_commands);
                                        Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKvar = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff3_CurrentMonthMdiKvar, 0, ref Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff3_CurrentMonthMdiKvar); update_Progress_bar(pb_ins, total_commands);
                                        Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKvar = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_Tariff4_CurrentMonthMdiKvar, 0, ref Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff4_CurrentMonthMdiKvar); update_Progress_bar(pb_ins, total_commands);
                                        Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKvar = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Cumulative_TariffTL_CurrentMonthMdiKvar, 0, ref Instantaneous_Class_obj.TimeStamp_Cumulative_TariffTL_CurrentMonthMdiKvar); update_Progress_bar(pb_ins, total_commands);


                                        Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKvar = Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKvar / 1000;
                                        Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKvar = Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKvar / 1000;
                                        Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKvar = Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKvar / 1000;
                                        Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKvar = Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKvar / 1000;
                                        Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKvar = Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKvar / 1000;

                                        // PREVIOUS MONTH MDI

                                        //total_commands = 4;
                                        //pb_ins.Value = 0;
                                        //currentQuantityLabel = "Requesting Previous Month MDI KW";

                                        //Instantaneous_Class_obj.PB_MDI_KW_T1 = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PB_PREVIOUS_MONTH_MDI_KW_T1, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                                        //Instantaneous_Class_obj.PB_MDI_KW_T2 = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PB_PREVIOUS_MONTH_MDI_KW_T2, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                                        //Instantaneous_Class_obj.PB_MDI_KW_T3 = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PB_PREVIOUS_MONTH_MDI_KW_T3, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                                        //Instantaneous_Class_obj.PB_MDI_KW_T4 = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PB_PREVIOUS_MONTH_MDI_KW_T4, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                                        //Instantaneous_Class_obj.PB_MDI_KW_TL = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PB_PREVIOUS_MONTH_MDI_KW_TL, 0) / 1000; update_Progress_bar(pb_ins, total_commands);


                                        //total_commands = 4;
                                        //pb_ins.Value = 0;
                                        //currentQuantityLabel = "Requesting Previous Month MDI KVar";

                                        //Instantaneous_Class_obj.PB_MDI_KVAR_T1 = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_T1, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                                        //Instantaneous_Class_obj.PB_MDI_KVAR_T2 = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_T2, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                                        //Instantaneous_Class_obj.PB_MDI_KVAR_T3 = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_T3, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                                        //Instantaneous_Class_obj.PB_MDI_KVAR_T4 = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_T4, 0) / 1000; update_Progress_bar(pb_ins, total_commands);
                                        //Instantaneous_Class_obj.PB_MDI_KVAR_TL = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_TL, 0) / 1000; update_Progress_bar(pb_ins, total_commands);

                                        #region ADD TO DB Current Month MDI KW

                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff1_CurrentMonthMdiKw), Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw, "kW", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff2_CurrentMonthMdiKw), Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw, "kW", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff3_CurrentMonthMdiKw), Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw, "kW", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff4_CurrentMonthMdiKw), Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw, "kW", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_TariffTL_CurrentMonthMdiKw), Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw, "kW", 0, true);

                                        #endregion

                                        #region ADD TO DB Current Month MDI KVar

                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff1_CurrentMonthMdiKvar), Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKvar, "kVar", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff2_CurrentMonthMdiKvar), Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKvar, "kVar", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff3_CurrentMonthMdiKvar), Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKvar, "kVar", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_Tariff4_CurrentMonthMdiKvar), Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKvar, "kVar", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Cumulative_TariffTL_CurrentMonthMdiKvar), Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKvar, "kVar", 0, true);

                                        #endregion

                                        #region ADD TO DB Previous Month MDI KW

                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PB_PREVIOUS_MONTH_MDI_KW_T1), Instantaneous_Class_obj.PB_MDI_KW_T1, "kW", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PB_PREVIOUS_MONTH_MDI_KW_T2), Instantaneous_Class_obj.PB_MDI_KW_T2, "kW", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PB_PREVIOUS_MONTH_MDI_KW_T3), Instantaneous_Class_obj.PB_MDI_KW_T3, "kW", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PB_PREVIOUS_MONTH_MDI_KW_T4), Instantaneous_Class_obj.PB_MDI_KW_T4, "kW", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PB_PREVIOUS_MONTH_MDI_KW_TL), Instantaneous_Class_obj.PB_MDI_KW_TL, "kW", 0, true);

                                        #endregion

                                        #region ADD TO DB Previous Month MDI KW

                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_T1), Instantaneous_Class_obj.PB_MDI_KVAR_T1, "kVar", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_T2), Instantaneous_Class_obj.PB_MDI_KVAR_T2, "kVar", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_T3), Instantaneous_Class_obj.PB_MDI_KVAR_T3, "kVar", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_T4), Instantaneous_Class_obj.PB_MDI_KVAR_T4, "kVar", 0, true);
                                        //DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_TL), Instantaneous_Class_obj.PB_MDI_KVAR_TL, "kVar", 0, true);

                                        #endregion

                                        #region ADD TO DATASET CURRENT MONTH MDI KW

                                        dataSet_INS.dataTable_T.Rows.Add();
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].Quantity = "Current Month MDI KW";
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T1 = Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw.ToString();
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T2 = Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKw.ToString();
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T3 = Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKw.ToString();
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T4 = Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKw.ToString();
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].TL = Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKw.ToString();

                                        #endregion

                                        #region ADD TO DATASET CURRENT MONTH MDI KW DATETIME

                                        if (Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw != DateTime.MinValue &&
                                            Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff2_CurrentMonthMdiKw != DateTime.MinValue &&
                                            Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff3_CurrentMonthMdiKw != DateTime.MinValue &&
                                            Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff4_CurrentMonthMdiKw != DateTime.MinValue &&
                                            Instantaneous_Class_obj.TimeStamp_Cumulative_TariffTL_CurrentMonthMdiKw != DateTime.MinValue)
                                        {
                                            dataSet_INS.dataTable_T.Rows.Add();
                                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].Quantity = "MDI Occurance DateTime";
                                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T1 = Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw.ToString("dd/MM/yyyy HH:mm:ss");
                                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T2 = Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff2_CurrentMonthMdiKw.ToString("dd/MM/yyyy HH:mm:ss");
                                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T3 = Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff3_CurrentMonthMdiKw.ToString("dd/MM/yyyy HH:mm:ss");
                                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T4 = Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff4_CurrentMonthMdiKw.ToString("dd/MM/yyyy HH:mm:ss");
                                            dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].TL = Instantaneous_Class_obj.TimeStamp_Cumulative_TariffTL_CurrentMonthMdiKw.ToString("dd/MM/yyyy HH:mm:ss");
                                        }

                                        #endregion

                                        #region ADD TO DATASET CURRENT MONTH MDI Kvar

                                        dataSet_INS.dataTable_T.Rows.Add();
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].Quantity = "Current Month MDI Kvar";
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T1 = Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKvar.ToString();
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T2 = Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKvar.ToString();
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T3 = Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKvar.ToString();
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T4 = Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKvar.ToString();
                                        dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].TL = Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKvar.ToString();

                                        #endregion

                                        #region ADD TO DATASET PREVIOUS MONTH MDI KW

                                        // dataSet_INS.dataTable_T.Rows.Add();
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].Quantity = "Previous Month MDI KW";
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T1 = Instantaneous_Class_obj.PB_MDI_KW_T1.ToString();
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T2 = Instantaneous_Class_obj.PB_MDI_KW_T2.ToString();
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T3 = Instantaneous_Class_obj.PB_MDI_KW_T3.ToString();
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T4 = Instantaneous_Class_obj.PB_MDI_KW_T4.ToString();
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].TL = Instantaneous_Class_obj.PB_MDI_KW_TL.ToString();

                                        #endregion

                                        #region ADD TO DATASET PREVIOUS MONTH MDI Kvar

                                        // dataSet_INS.dataTable_T.Rows.Add();
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].Quantity = "Prevoius Month MDI Kvar";
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T1 = Instantaneous_Class_obj.PB_MDI_KVAR_T1.ToString();
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T2 = Instantaneous_Class_obj.PB_MDI_KVAR_T2.ToString();
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T3 = Instantaneous_Class_obj.PB_MDI_KVAR_T3.ToString();
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T4 = Instantaneous_Class_obj.PB_MDI_KVAR_T4.ToString();
                                        // dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].TL = Instantaneous_Class_obj.PB_MDI_KVAR_TL.ToString();

                                        #endregion
                                        */
                }

                #endregion

                #region MDI Interval

                if (check_readMDI_Interval.Checked)
                {
                    total_commands = 4;
                    pb_ins.Value = 0;
                    currentQuantityLabel = "Requesting Current Month MDI KW";
                    Param_Controller.GET_Instantaneous_MDI(ref  obj_InstantaneousMDI);
                }

                #endregion

                pb_ins.Value = pb_ins.Maximum;
                GetCompleted = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                GetCompleted = false;
            }
        }

        private void Show_Instantanose_Data()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //tcontrolMain.SelectedTab = INs;

                ClearGrid(grid_Instanstanouse);
                ClearGrid(grid_misc);

                int currentRow = 0;

                #region DateTime

                lbl_meter_datetime.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", Instantaneous_Class_obj.Local_Date_Time);
                #endregion

                #region Manufacturing ID

                lbl_meter_serial.Text = ConnController.ConnectionManager.ConnectionInfo.MSN;

                #endregion

                #region Voltage

                if (check_Voltage.Checked)
                {

                    currentRow = grid_Instanstanouse.Rows.Count;
                    grid_Instanstanouse.Rows.Add();

                    grid_Instanstanouse.Rows[currentRow].HeaderCell.Value = "Voltage (V)";
                    grid_Instanstanouse[0, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.voltage_PhaseA);
                    grid_Instanstanouse[1, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.voltage_PhaseB);
                    grid_Instanstanouse[2, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.voltage_PhaseC);
                    grid_Instanstanouse[3, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.voltage_total, true);

                    grid_Instanstanouse.FirstDisplayedScrollingRowIndex = grid_Instanstanouse.Rows.Count - 1;
                }

                #endregion

                #region Current
                if (check_Current.Checked)
                {

                    currentRow = grid_Instanstanouse.Rows.Count;

                    grid_Instanstanouse.Rows.Add();

                    grid_Instanstanouse.Rows[currentRow].HeaderCell.Value = "Current (A)";
                    grid_Instanstanouse[0, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.current_PhaseA);
                    grid_Instanstanouse[1, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.current_PhaseB);
                    grid_Instanstanouse[2, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.current_PhaseC);
                    grid_Instanstanouse[3, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.current_total, true);

                    string NoValue = "----";
                    currentRow = grid_Instanstanouse.Rows.Count;
                    grid_Instanstanouse.Rows.Add();

                    grid_Instanstanouse.Rows[currentRow].HeaderCell.Value = "Current_Nuteral (A)";
                    grid_Instanstanouse[0, currentRow].Value = NoValue;
                    grid_Instanstanouse[1, currentRow].Value = NoValue;
                    grid_Instanstanouse[2, currentRow].Value = NoValue;
                    grid_Instanstanouse[3, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Current_on_neutral, true);


                    grid_Instanstanouse.FirstDisplayedScrollingRowIndex = grid_Instanstanouse.Rows.Count - 1;

                }
                #endregion

                #region Active Power

                if (check_ActivePower.Checked)
                {

                    currentRow = grid_Instanstanouse.Rows.Count;
                    grid_Instanstanouse.Rows.Add();
                    grid_Instanstanouse.Rows[currentRow].HeaderCell.Value = "Active Powers Postive (kW)";
                    grid_Instanstanouse[0, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.active_powerPositive_PhaseA);
                    grid_Instanstanouse[1, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.active_powerPositive_PhaseB);
                    grid_Instanstanouse[2, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.active_powerPositive_PhaseC);
                    grid_Instanstanouse[3, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.active_powerPositive_PhaseTL);

                    currentRow = grid_Instanstanouse.Rows.Count;
                    grid_Instanstanouse.Rows.Add();
                    grid_Instanstanouse.Rows[currentRow].HeaderCell.Value = "Active Powers Negative (kW)";
                    grid_Instanstanouse[0, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.active_powerNegative_PhaseA);
                    grid_Instanstanouse[1, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.active_powerNegative_PhaseB);
                    grid_Instanstanouse[2, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.active_powerNegative_PhaseC);
                    grid_Instanstanouse[3, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.active_powerNegative_PhaseTL);

                    grid_Instanstanouse.FirstDisplayedScrollingRowIndex = grid_Instanstanouse.Rows.Count - 1;

                }

                #endregion

                #region Reactive Power

                if (check_ReactivePower.Checked)
                {
                    currentRow = grid_Instanstanouse.Rows.Count;
                    grid_Instanstanouse.Rows.Add();
                    grid_Instanstanouse.Rows[currentRow].HeaderCell.Value = "Reactive Powers Positive (kVar)";
                    grid_Instanstanouse[0, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.reactive_powerPositive_PhaseA);
                    grid_Instanstanouse[1, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.reactive_powerPositive_PhaseB);
                    grid_Instanstanouse[2, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.reactive_powerPositive_PhaseC);
                    grid_Instanstanouse[3, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.reactive_powerPositive_PhaseTL);

                    currentRow = grid_Instanstanouse.Rows.Count;
                    grid_Instanstanouse.Rows.Add();
                    grid_Instanstanouse.Rows[currentRow].HeaderCell.Value = "Reactive Powers Negative (kVar)";
                    grid_Instanstanouse[0, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.reactive_powerNegative_PhaseA);
                    grid_Instanstanouse[1, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.reactive_powerNegative_PhaseB);
                    grid_Instanstanouse[2, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.reactive_powerNegative_PhaseC);
                    grid_Instanstanouse[3, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.reactive_powerNegative_PhaseTL);

                    grid_Instanstanouse.FirstDisplayedScrollingRowIndex = grid_Instanstanouse.Rows.Count - 1;

                }

                if (check_ReactivePower.Checked && check_ActivePower.Checked)
                {
                    currentRow = grid_Instanstanouse.Rows.Count;

                    grid_Instanstanouse.Rows.Add();
                    grid_Instanstanouse.Rows[currentRow].HeaderCell.Value = "Power Quadrant";
                    Instantaneous_Class_obj.Power_Qardant_Phase_A = GetQuadrant(Instantaneous_Class_obj.active_powerNegative_PhaseA, Instantaneous_Class_obj.active_powerPositive_PhaseA, Instantaneous_Class_obj.reactive_powerNegative_PhaseA, Instantaneous_Class_obj.reactive_powerPositive_PhaseA);
                    Instantaneous_Class_obj.Power_Qardant_Phase_B = GetQuadrant(Instantaneous_Class_obj.active_powerNegative_PhaseB, Instantaneous_Class_obj.active_powerPositive_PhaseB, Instantaneous_Class_obj.reactive_powerNegative_PhaseB, Instantaneous_Class_obj.reactive_powerPositive_PhaseB);
                    Instantaneous_Class_obj.Power_Qardant_Phase_C = GetQuadrant(Instantaneous_Class_obj.active_powerNegative_PhaseC, Instantaneous_Class_obj.active_powerPositive_PhaseC, Instantaneous_Class_obj.reactive_powerNegative_PhaseC, Instantaneous_Class_obj.reactive_powerPositive_PhaseC);
                    Instantaneous_Class_obj.Power_Qardant_Phase_TL = GetQuadrant(Instantaneous_Class_obj.active_powerNegative_PhaseTL, Instantaneous_Class_obj.active_powerPositive_PhaseTL, Instantaneous_Class_obj.reactive_powerNegative_PhaseTL, Instantaneous_Class_obj.reactive_powerPositive_PhaseTL);
                    grid_Instanstanouse[0, currentRow].Value = Instantaneous_Class_obj.Power_Qardant_Phase_A;
                    grid_Instanstanouse[1, currentRow].Value = Instantaneous_Class_obj.Power_Qardant_Phase_B;
                    grid_Instanstanouse[2, currentRow].Value = Instantaneous_Class_obj.Power_Qardant_Phase_C;
                    grid_Instanstanouse[3, currentRow].Value = Instantaneous_Class_obj.Power_Qardant_Phase_TL;

                    dataSet_INS.dataTable_P.Rows.Add();

                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Quantity = "Power Quadrant";
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_A = Instantaneous_Class_obj.Power_Qardant_Phase_A.ToString();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_B = Instantaneous_Class_obj.Power_Qardant_Phase_B.ToString();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_C = Instantaneous_Class_obj.Power_Qardant_Phase_C.ToString();
                    dataSet_INS.dataTable_P[dataSet_INS.dataTable_P.Rows.Count - 1].Phase_Total = Instantaneous_Class_obj.Power_Qardant_Phase_TL.ToString();
                }


                #endregion

                #region Power Factor

                if (check_Powerfactor.Checked)
                {
                    currentRow = grid_Instanstanouse.Rows.Count;
                    grid_Instanstanouse.Rows.Add();
                    grid_Instanstanouse.Rows[currentRow].HeaderCell.Value = "Power Factors";
                    grid_Instanstanouse[0, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Power_Factor_Ph_A);
                    grid_Instanstanouse[1, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Power_Factor_Ph_B);
                    grid_Instanstanouse[2, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Power_Factor_Ph_C);
                    grid_Instanstanouse[3, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Power_Factor_All);

                    /*
                    grid_CurrentMDI.Rows.Add();
                    grid_CurrentMDI.Rows[grid_CurrentMDI.Rows.Count - 1].HeaderCell.Value = "Running Power Factor";
                    grid_CurrentMDI[0, grid_CurrentMDI.Rows.Count - 1].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff1_PowerFactor);
                    grid_CurrentMDI[1, grid_CurrentMDI.Rows.Count - 1].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff2_PowerFactor);
                    grid_CurrentMDI[2, grid_CurrentMDI.Rows.Count - 1].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff3_PowerFactor);
                    grid_CurrentMDI[3, grid_CurrentMDI.Rows.Count - 1].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff4_PowerFactor);
                    grid_CurrentMDI[4, grid_CurrentMDI.Rows.Count - 1].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_TariffTL_PowerFactor);
                    */
                    grid_Instanstanouse.FirstDisplayedScrollingRowIndex = grid_Instanstanouse.Rows.Count - 1;

                }

                #endregion

                #region Apparent Power

                if (check_Apparent.Checked)
                {
                    currentRow = grid_Instanstanouse.Rows.Count;
                    grid_Instanstanouse.Rows.Add();
                    grid_Instanstanouse.Rows[currentRow].HeaderCell.Value = "Apparant Powers (kVA)";
                    grid_Instanstanouse[0, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Apparent_Power_Ph_A);
                    grid_Instanstanouse[1, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Apparent_Power_Ph_B);
                    grid_Instanstanouse[2, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Apparent_Power_Ph_C);
                    grid_Instanstanouse[3, currentRow].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Apparent_Power_Tot);

                    grid_Instanstanouse.FirstDisplayedScrollingRowIndex = grid_Instanstanouse.Rows.Count - 1;
                }

                #endregion

                #region Misc

                if (check_misc.Checked)
                {
                    Show_MISC(this.grid_misc);
                }

                #endregion

                #region MDI's

                if (check_Mdi.Checked)
                {
                    Clear_Instantaneous_MDIs(true);
                    Update_Intantaneous_MDIs();
                }
                else
                {
                    // Disable Instantanoue_MDIs
                    Clear_Instantaneous_MDIs(false);
                }

                #endregion

                #region MDI INTERVAL

                if (check_readMDI_Interval.Checked)
                {
                    const double divider = 1000;

                    Clear_Instantaneous_MDIs(true);
                    lbl_MDI_Counter.Text = obj_InstantaneousMDI.Time_Elapsed.ToString();
                    lbl_MDI_time.Text = obj_InstantaneousMDI.Time.ToString();
                    lbl_MDI_SlideCount.Text = obj_InstantaneousMDI.SlidesCount.ToString();
                    lbl_MDI_SlideCounter.Text = obj_InstantaneousMDI.SlidesCounter.ToString();
                    lbl_MDI_Running_Energy.Text = ((obj_InstantaneousMDI.RunningKVar) / divider).ToString("f3");
                    lbl_MDI_Running_Power.Text = ((obj_InstantaneousMDI.RunningKW) / divider).ToString("f3");
                    lbl_MDI_Previous_Energy.Text = ((obj_InstantaneousMDI.PrevoiusKVAR) / divider).ToString("f3");
                    lbl_MDI_Previous_Power.Text = ((obj_InstantaneousMDI.PreviousKW) / divider).ToString("f3");
                    lbl_MDI_TimeLeft.Text = obj_InstantaneousMDI.TimeLeft.ToString();

                    ClearGrid(grid_MDI);

                    #region MDI_Slide_KW

                    grid_MDI.Rows.Add();
                    grid_MDI.Rows[grid_MDI.Rows.Count - 1].HeaderCell.Value = "kW";

                    for (int i = 0; i < 6; i++)
                    {
                        grid_MDI[i, grid_MDI.Rows.Count - 1].Value = (obj_InstantaneousMDI.MDI_SlideKW[i] / divider).ToString("f3");
                    }
                    // grid1.FirstDisplayedScrollingRowIndex = grid1.Rows.Count - 1;

                    #endregion

                    #region MDI_Slide_Kvar

                    grid_MDI.Rows.Add();
                    grid_MDI.Rows[grid_MDI.Rows.Count - 1].HeaderCell.Value = "kVar";
                    for (int i = 0; i < 6; i++)
                    {
                        grid_MDI[i, grid_MDI.Rows.Count - 1].Value = (obj_InstantaneousMDI.MDI_SlideKVAR[i] / divider).ToString("f3");
                    }
                    // grid1.FirstDisplayedScrollingRowIndex = grid1.Rows.Count - 1;

                    #endregion
                }

                #endregion

                //  btn_INS_Report.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        public void Show_MISC(DataGridView grid_misc)
        {
            int misc_currentRow = 0;

            #region Activity Calendar
            grid_misc.Rows.Add();

            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "Active Season";
            grid_misc[0, misc_currentRow++].Value = (Instantaneous_Class_obj.Active_Season);
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "Active Tariff";
            grid_misc[0, misc_currentRow++].Value = (Instantaneous_Class_obj.Active_Tariff);
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "Active DayProfile";
            grid_misc[0, misc_currentRow++].Value = (Instantaneous_Class_obj.Active_Day_Profile);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            #region Active Firmware ID
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "Active Firmware ID";
            grid_misc[0, misc_currentRow++].Value = (Instantaneous_Class_obj.Active_Firmware_ID);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            #region Battery Volts
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "Battery Volts (V)";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Battery_Volts);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            #region Billing Period Counter
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "MDI Reset Counter";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Billing_Period_Counter_VZ);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            #region CT Ratio Numerator
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "CT Ratio Numerator";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.CT_Ratio_Numerator);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            #region CT Ratio Denominator
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "CT Ratio Denominator";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.CT_Ratio_Denominator);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            #region Event Counter
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "Event_Counter ";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Event_Counter);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            # region Load Profile Counter
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "Load_Profile_Counter";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Load_Profile_Counter);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            #region PT Ratio Numerator
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "PT Ratio Numerator";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PT_Ratio_Numerator);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            #region PT Ratio Denominator
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "PT Ratio Denominator";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PT_Ratio_Denominator);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            #region Power Failure Number
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "Number of Power Failures ";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Power_Failures_No);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;

            #endregion

            #region Supply Frequency
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "Supply Frequency (Hz)";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Supply_Frequency);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion

            #region Tamper_Power
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "Tamper Power (KW)";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Convert.ToDouble(((Instantaneous_Class_obj.Tamper_Power) / 1000).ToString("f3")));
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion
            #region RSSI Signal Strength
            grid_misc.Rows.Add();
            grid_misc.Rows[misc_currentRow].HeaderCell.Value = "RSSI Signal Strength (dBm)";
            grid_misc[0, misc_currentRow++].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.RSSI);
            grid_misc.FirstDisplayedScrollingRowIndex = grid_misc.Rows.Count - 1;
            #endregion
        }

        //v5.3.12 Added for DateTime
        public string MdiFormat(TariffMDI mdi)
        {
            //return string.Format("{0}: [{1}]", mdi.ToString(), mdi.TimeStamp.ToString(Commons.DateTimeFormat));
            return string.Format("{0}", mdi.ToString());
        }

        public void Update_Intantaneous_MDIs()
        {
            foreach (QuantityMDI mdi in Instantaneous_Class_obj.MDIsToShow)
            {
                int ColumnCounter = 0;
                int row = grid_CurrentMDI.Rows.Add();
                grid_CurrentMDI.Rows[row].HeaderCell.Value = grid_CurrentMDI.Rows.Count.ToString();
                grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.QuantityName;
                grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.Tariff1.Value;
                grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.Tariff2.Value;
                grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.Tariff3.Value;
                grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.Tariff4.Value;
                grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.TariffTL.Value;

                if (mdi.TariffTL.IsValidTimeStamp)
                {
                    ColumnCounter = 0;
                    row = grid_CurrentMDI.Rows.Add();
                    grid_CurrentMDI.Rows[row].HeaderCell.Value = grid_CurrentMDI.Rows.Count.ToString();
                    grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.QuantityName + " Time";
                    grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.Tariff1.TimeStamp.ToString(Commons.DateTimeFormat);
                    grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.Tariff2.TimeStamp.ToString(Commons.DateTimeFormat);
                    grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.Tariff3.TimeStamp.ToString(Commons.DateTimeFormat);
                    grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.Tariff4.TimeStamp.ToString(Commons.DateTimeFormat);
                    grid_CurrentMDI.Rows[row].Cells[ColumnCounter++].Value = mdi.TariffTL.TimeStamp.ToString(Commons.DateTimeFormat); 
                }
            }

            /*
                        grid_CurrentMDI.Rows.Add(5);
                        grid_CurrentMDI.Rows[0].HeaderCell.Value = "T1";
                        grid_CurrentMDI.Rows[1].HeaderCell.Value = "T2";
                        grid_CurrentMDI.Rows[2].HeaderCell.Value = "T3";
                        grid_CurrentMDI.Rows[3].HeaderCell.Value = "T4";
                        grid_CurrentMDI.Rows[4].HeaderCell.Value = "TL";

                        #region Current_Month_ACTIVE_MDI

                        grid_CurrentMDI["Active_MDI", 0].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw, "f3", 1000);
                        grid_CurrentMDI["Active_MDI", 1].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw, "f3", 1000);
                        grid_CurrentMDI["Active_MDI", 2].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw, "f3", 1000);
                        grid_CurrentMDI["Active_MDI", 3].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw, "f3", 1000);
                        grid_CurrentMDI["Active_MDI", 4].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw, "f3", 1000);

                        #endregion

                        #region Current_Month_REACTIVE_MDI

                        grid_CurrentMDI["Reactive_MDI", 0].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKvar, "f3", 1000);
                        grid_CurrentMDI["Reactive_MDI", 1].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKvar, "f3", 1000);
                        grid_CurrentMDI["Reactive_MDI", 2].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKvar, "f3", 1000);
                        grid_CurrentMDI["Reactive_MDI", 3].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKvar, "f3", 1000);
                        grid_CurrentMDI["Reactive_MDI", 4].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKvar, "f3", 1000);

                        #endregion

                        #region Current Month MDI_TIME
                         Commented By Rashad Maqsood
                        if (Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKvar != DateTime.MinValue &&
                            Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff2_CurrentMonthMdiKvar != DateTime.MinValue &&
                            Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff3_CurrentMonthMdiKvar != DateTime.MinValue &&
                            Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff4_CurrentMonthMdiKvar != DateTime.MinValue &&
                            Instantaneous_Class_obj.TimeStamp_Cumulative_TariffTL_CurrentMonthMdiKvar != DateTime.MinValue)
                        {
                            grid_CurrentMDI["Time", 0].Value = (Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKvar.ToString());
                            grid_CurrentMDI["Time", 1].Value = (Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff2_CurrentMonthMdiKvar.ToString());
                            grid_CurrentMDI["Time", 2].Value = (Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff3_CurrentMonthMdiKvar.ToString());
                            grid_CurrentMDI["Time", 3].Value = (Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff4_CurrentMonthMdiKvar.ToString());
                            grid_CurrentMDI["Time", 4].Value = (Instantaneous_Class_obj.TimeStamp_Cumulative_TariffTL_CurrentMonthMdiKvar.ToString());
                        }
                        else
                        {
                            grid_CurrentMDI["Time", 0].Value = "-";
                            grid_CurrentMDI["Time", 1].Value = "-";
                            grid_CurrentMDI["Time", 2].Value = "-";
                            grid_CurrentMDI["Time", 3].Value = "-";
                            grid_CurrentMDI["Time", 4].Value = "-";
                        }

                        #endregion

                        #region PREVIOUS_MONTH_ACTIVE_MDI

                        // grid_CurrentMDI["PM_MDI_KW", 0].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PB_MDI_KW_T1);
                        // grid_CurrentMDI["PM_MDI_KW", 1].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PB_MDI_KW_T2);
                        // grid_CurrentMDI["PM_MDI_KW", 2].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PB_MDI_KW_T3);
                        // grid_CurrentMDI["PM_MDI_KW", 3].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PB_MDI_KW_T4);
                        // grid_CurrentMDI["PM_MDI_KW", 4].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PB_MDI_KW_TL);

                        #endregion

                        #region PREVIOUS_MONTH_REACTIVE_MDI

                        // grid_CurrentMDI["PM_MDI_Kvar", 0].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PB_MDI_KVAR_T1);
                        // grid_CurrentMDI["PM_MDI_Kvar", 1].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PB_MDI_KVAR_T2);
                        // grid_CurrentMDI["PM_MDI_Kvar", 2].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PB_MDI_KVAR_T3);
                        // grid_CurrentMDI["PM_MDI_Kvar", 3].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PB_MDI_KVAR_T4);
                        // grid_CurrentMDI["PM_MDI_Kvar", 4].Value = LocalCommon.value_to_string(Instantaneous_Class_obj.PB_MDI_KVAR_TL);

                        #endregion

                        */
        }

        public void Clear_Instantaneous_MDIs(bool enable)
        {
            try
            {
                gp_MDIs.Enabled = enable;
                ClearGrid(grid_MDI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Intantaneous_values_reinitialize()
        {
            Instantaneous_Class_obj = new Instantaneous_Class();

        }

        public void update_Progress_bar(ProgressBar _ProgressBar, int total_commands)
        {
            int min = _ProgressBar.Minimum;
            int max = _ProgressBar.Maximum;
            try
            {
                if (!String.IsNullOrEmpty(this.currentQuantityLabel))
                    lbl_PbStatus.Text = currentQuantityLabel;
                else
                    lbl_PbStatus.Text = "__";

                int valueNew = _ProgressBar.Value + ((max - min) / 100) * (100 / total_commands);
                if (valueNew >= min && valueNew <= max)
                    _ProgressBar.Value = valueNew;

                Application.DoEvents();
            }
            catch
            {
                _ProgressBar.Value = 0;
            }
        }

        #region Read Instantanouse Values

        public void Get_MISC(ProgressBar pb_ins, int total_commands)
        {
            int misc_currentRow = 0;
            total_commands = 15;

            #region Activity Calendar

            currentQuantityLabel = "Requesting Activity Calendar Info";
            Instantaneous_Class_obj.Dummy_String = Instantanous_Controller.GET_Any_string(ref Instantaneous_Class_obj, Get_Index.Active_Season, 2,1);
            update_Progress_bar(pb_ins, total_commands);
            byte[] tempBytes = Encoding.ASCII.GetBytes(Instantaneous_Class_obj.Dummy_String);
            Instantaneous_Class_obj.Active_Day_Profile = tempBytes[0];
            Instantaneous_Class_obj.Active_Season = tempBytes[1];
            Instantaneous_Class_obj.Active_Tariff = tempBytes[2];

            #region ADD_TO_DB_ACTIVITY CALENDAR

            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Season), Instantaneous_Class_obj.Active_Season, "", 0, true);
            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Tariff), Instantaneous_Class_obj.Active_Tariff, "", 0, true);
            // DB_Add_DataSet_toWrite(0, Instantaneous_Class_obj.Active_Day_Profile, "", 0, true);

            #endregion

            #region ADD TO DATASET ACTIVITY CALENDAR

            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Active Day Profile";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Active_Day_Profile.ToString();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Active Season";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Active_Season.ToString();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Active Tariff";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Active_Tariff.ToString();

            #endregion

            #endregion

            #region Active Firmware ID

            currentQuantityLabel = "Requesting Firmware ID";
            Instantaneous_Class_obj.Active_Firmware_ID = Instantanous_Controller.GET_Any_string(ref Instantaneous_Class_obj, Get_Index.Active_Firmware_ID, 2,1);
            update_Progress_bar(pb_ins, total_commands);

            #region Add_To_DB_FIRMWARE VERSION

            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Active_Firmware_ID), Instantaneous_Class_obj.Active_Firmware_ID, "", 0, true);

            #endregion

            #region ADD TO DATASET ACTIVITY CALENDAR

            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Active Firmware Version";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Active_Firmware_ID.ToString();

            #endregion

            #endregion

            #region Battery Volts

            currentQuantityLabel = "Requesting Battery Voltage";
            Instantaneous_Class_obj.Battery_Volts = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Battery_Volts, 0);
            update_Progress_bar(pb_ins, total_commands);

            #region Add_To_DB_BATTERY VOLTS

            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Battery_Volts), Instantaneous_Class_obj.Battery_Volts, "V", 0, true);

            #endregion

            #region ADD TO DATASET BATTERY VOLTS

            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Battery Volts (V)";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Battery_Volts.ToString();

            #endregion

            #endregion

            #region Billing Period Counter

            currentQuantityLabel = "Requesting MDI Reset Counter";
            Instantaneous_Class_obj.Billing_Period_Counter_VZ = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Billing_Period_Counter_VZ, 2);
            update_Progress_bar(pb_ins, total_commands);

            #region Add_To_DB_BILLING COUNTER VZ

            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Billing_Period_Counter_VZ), Instantaneous_Class_obj.Billing_Period_Counter_VZ, "", 0, true);

            #endregion

            #region ADD TO DATASET BILLING PERIOD COUNTER VZ

            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Billing Period Counter VZ";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Billing_Period_Counter_VZ.ToString();

            #endregion

            #endregion

            #region CT Ratio Numerator

            currentQuantityLabel = "Requesting CT Ratio Numerator";
            Instantaneous_Class_obj.CT_Ratio_Numerator = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.CT_Ratio_Numerator, 0);
            update_Progress_bar(pb_ins, total_commands);

            #region Add_To_DB_CT RATIO NUMERATOR

            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.CT_Ratio_Numerator), Instantaneous_Class_obj.CT_Ratio_Numerator, "", 0, true);

            #endregion

            #region ADD TO DATASET CT RATIO NUMERATOR

            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "CT Ratio Numerator";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.CT_Ratio_Numerator.ToString();

            #endregion
            #endregion

            #region CT Ratio Denominator

            currentQuantityLabel = "Requesting CT Ratio Denominator";
            Instantaneous_Class_obj.CT_Ratio_Denominator = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.CT_Ratio_Denominator, 0);

            update_Progress_bar(pb_ins, total_commands);

            #region Add to DB CT RATIO DENOMINATOR

            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.CT_Ratio_Denominator), Instantaneous_Class_obj.CT_Ratio_Denominator, "", 0, true);

            #endregion
            #region ADD TO DATASET CT RATIO DENOMINATOR
            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "CT Ratio Denominator";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.CT_Ratio_Denominator.ToString();
            #endregion

            #endregion

            #region Event Counter

            currentQuantityLabel = "Requesting Event Counter";
            Instantaneous_Class_obj.Event_Counter = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Event_Counter_00, 2);

            update_Progress_bar(pb_ins, total_commands);
            #region ADD_TO_DB_EVENT COUNTER
            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Event_Counter_00), Instantaneous_Class_obj.Event_Counter, "", 0, true);

            #endregion
            #region ADD TO DATASET EVENT COUNTER
            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Event Counter";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Event_Counter.ToString();
            #endregion

            #endregion

            #region Load Profile Counter

            currentQuantityLabel = "Requesting Load Profile Counter";
            Instantaneous_Class_obj.Load_Profile_Counter = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Load_Profile_Counter, 2);

            update_Progress_bar(pb_ins, total_commands);
            #region ADD_TO_DB_LOAD PROFILE COUNTER
            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Load_Profile_Counter), Instantaneous_Class_obj.Load_Profile_Counter, "", 0, true);


            #endregion
            #region ADD TO DATASET LOAD PROFILE COUNTER
            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Load Profile Counter";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Load_Profile_Counter.ToString();
            #endregion

            #endregion

            #region PT Ratio Numerator

            currentQuantityLabel = "Requesting PT Ratio Numerator";
            Instantaneous_Class_obj.PT_Ratio_Numerator = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PT_Ratio_Numerator, 0);

            update_Progress_bar(pb_ins, total_commands);
            #region PT RATIO NUMERATOR DB
            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PT_Ratio_Numerator), Instantaneous_Class_obj.PT_Ratio_Numerator, "", 0, true);

            #endregion
            #region ADD TO DATASET PT RATIO NUMERATOR
            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "PT Ratio Numerator";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.PT_Ratio_Numerator.ToString();
            #endregion

            #endregion

            #region PT Ratio Denominator

            currentQuantityLabel = "Requesting PT Ratio Denominator";
            Instantaneous_Class_obj.PT_Ratio_Denominator = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.PT_Ratio_Denominator, 0);

            update_Progress_bar(pb_ins, total_commands);
            #region Add_To_DB_PT RATIO DENOMINATOR
            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.PT_Ratio_Denominator), Instantaneous_Class_obj.PT_Ratio_Denominator, "", 0, true);

            #endregion
            #region ADD TO DATASET PT RATIO DENOMINATOR
            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "PT Ratio Denominator";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.PT_Ratio_Denominator.ToString();
            #endregion

            #endregion

            #region Power Failure Number

            currentQuantityLabel = "Requesting Number of Power failures";
            Instantaneous_Class_obj.Power_Failures_No = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Power_Failures_No, 2);

            update_Progress_bar(pb_ins, total_commands);

            #region ADD_TO_DB_POWER FAILURE NO
            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Power_Failures_No), Instantaneous_Class_obj.Power_Failures_No, "", 0, true);

            #endregion
            #region ADD TO DATASET POWER FAILURE NUMBER
            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Power Failure Number";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Power_Failures_No.ToString();
            #endregion

            #endregion

            #region Supply Frequency

            currentQuantityLabel = "Requesting Supply Frequency";
            Instantaneous_Class_obj.Supply_Frequency = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Supply_Frequency, 0);

            update_Progress_bar(pb_ins, total_commands);
            #region Add_To_DB_SUPPLY FREQUENCY
            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Supply_Frequency), Instantaneous_Class_obj.Supply_Frequency, "Hz", 0, true);

            #endregion
            #region ADD TO DATASET SUPPLY FREQUENCY
            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Supply Frequency (Hz)";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Supply_Frequency.ToString();
            #endregion

            #endregion

            #region Tamper_Power

            currentQuantityLabel = "Requesting Tamper Power";
            Instantaneous_Class_obj.Tamper_Power = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.Tamper_Power, 0);

            #region ADD_TO_DB_TAMPER_POWER
            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.Tamper_Power), Instantaneous_Class_obj.Tamper_Power, "KW", 0, true);

            #endregion

            #region ADD TO DATASET TAMPER POWER
            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "Tamper Power (Kw)";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.Tamper_Power.ToString();
            #endregion

            #endregion

            #region RSSI

            currentQuantityLabel = "Requesting RSSI Signal Strength";
            Instantaneous_Class_obj.RSSI = Instantanous_Controller.TryGET_Any(Instantaneous_Class_obj, Get_Index.RSSI_SignalStrength, 0);

            update_Progress_bar(pb_ins, total_commands);
            #region Add_To_DB_RSSI
            DB_Add_DataSet_toWrite(Convert.ToInt64(Get_Index.RSSI_SignalStrength), Instantaneous_Class_obj.RSSI, "dbm", 0, true);

            #endregion
            #region ADD TO DATASET RSSI Signal Strength
            dataSet_INS.dataTable_M.Rows.Add();
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Quantity = "RSSI Signal Strength (dBm)";
            dataSet_INS.dataTable_M[dataSet_INS.dataTable_M.Rows.Count - 1].Value = Instantaneous_Class_obj.RSSI.ToString();
            #endregion

            #endregion
        }

        #endregion

        public void ClearGrid(DataGridView grid)
        {
            for (int rows = grid.Rows.Count - 1; rows >= 0; rows--)
            {
                grid.Rows.RemoveAt(rows);
            }
        }

        private void check_AllPhy_CheckedChanged(object sender, EventArgs e)
        {
            check_ActivePower.Checked = check_AllPhy.Checked;
            check_Current.Checked = check_AllPhy.Checked;
            check_misc.Checked = check_AllPhy.Checked;
            check_Powerfactor.Checked = check_AllPhy.Checked;
            check_PowerQuadrent.Checked = check_AllPhy.Checked;
            check_ReactivePower.Checked = check_AllPhy.Checked;
            check_Voltage.Checked = check_AllPhy.Checked;
            check_Mdi.Checked = check_AllPhy.Checked;
            check_Apparent.Checked = check_AllPhy.Checked;
            check_readMDI_Interval.Checked = check_AllPhy.Checked;
        }

        #region DataSet Fill

        public bool DB_Add_DataSet_toWrite(Int64 Quantity_ID, double Value, string Unit, sbyte Scalar, bool isNumber)
        {
            try
            {
                DBConnect.Insert_Record record = new DBConnect.Insert_Record();
                record.arrival_time = Instantaneous_Class_obj.Local_Date_Time;
                record.cat_id = 'I';
                record.msn = application_Controller.ConnectionManager.ConnectionInfo.MSN;
                record.qty_id = Quantity_ID;
                record.scalar = Scalar;
                record.session_id = 1;
                record.unit = Unit;

                if (isNumber && Value != Double.PositiveInfinity &&
                    Value != Double.NegativeInfinity && !Double.IsNaN(Value))
                    record.value = (double)Value;
                else if (isNumber)
                {
                    if (Value == Double.PositiveInfinity)
                        record.value = "Data not Present";
                    if (Value == Double.NegativeInfinity)
                        record.value = "Meter Error";
                    if (Double.IsNaN(Value))
                        record.value = "No Access";
                }
                else if (!isNumber)
                {
                    if (Value == 0)
                        record.value = false;
                    else
                        record.value = true;
                }

                List_Records.Add(record);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DB_Add_DataSet_toWrite(Int64 Quantity_ID, string Value, string Unit, sbyte Scalar, bool isNumber)
        {
            try
            {
                DBConnect.Insert_Record record = new DBConnect.Insert_Record();

                record.arrival_time = Instantaneous_Class_obj.Local_Date_Time;
                record.cat_id = 'I';
                record.msn = application_Controller.ConnectionManager.ConnectionInfo.MSN;
                record.qty_id = Quantity_ID;  //Voltage Phase A
                record.scalar = Scalar;
                record.session_id = 1;
                record.unit = Unit;
                record.value = (string)Value;
                List_Records.Add(record);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void Save_Instantanouse_Data()
        {
            List<DBConnect.Insert_Record> InsertLIST = new List<DBConnect.Insert_Record>();
            foreach (var item in List_Records)
            {
                if (item.msn != null)
                {
                    InsertLIST.Add(item);
                }
            }

            // Array.Resize(ref List_Records, List_Records.Length);
            // MyDataBase.SaveToDataBase(List_Records);
            if (!MyDataBase.SaveToDataBase(InsertLIST, "I", 0))
            {
                Notification notify = new Notification("Error", "Instananeous Values not saved!!!!", 6000);
            }
            else
            {
                Notification notify = new Notification("Instantaneous", "Instananeous Values Added to Database!!!!", 6000);
            }
            List_Records.Clear();

        }

        #endregion


        private void Instantaneous_Load(object sender, EventArgs e)
        {
            try
            {
                // ================================================================
                // =================================Param_Controller = ApplicationController.GetInstance().Param_Controller;
                // *********************************For SETTING PARAMETERS
                if (Application_Controller != null)
                {
                    Param_Controller = Application_Controller.Param_Controller;
                    Application_Process = Application_Controller.Applicationprocess_Controller.ApplicationProcess;
                    connectionManager = Application_Controller.ConnectionManager;
                    ConnController = Application_Controller.ConnectionController;
                    Instantanous_Controller = Application_Controller.InstantaneousController;
                    Instantanous_Controller_1 = Application_Controller.Billing_Controller;

                    AP_Controller = Application_Controller.Applicationprocess_Controller;
                    Instantanous_Controller.AP_Controller = Application_Controller.Applicationprocess_Controller;

                    // Remove PM_MDI_KW,PM_MDI_Kvar Columns From GRID
                    // grid_CurrentMDI.Columns.Remove(PM_MDI_KW);
                    // grid_CurrentMDI.Columns.Remove(PM_MDI_Kvar);
                    //PM_MDI_KW.Visible = false;
                    //PM_MDI_Kvar.Visible = false;
                    // ================================================================
                    // ================================================================
                    MyDataBase = new DBConnect();
                    Populate_Grid();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Initializing Instantanouse Interface Properly", ex);
            }
        }

        private void Populate_Grid()
        {
            for (int i = 0; i < 48; i++)
            {
                grid_EventNames.Rows.Add();
                grid_EventNames[0, i].Value = event_names[i];
            }
        }

        public void UpdateReadWriteStatus(bool IsReadWriteBusy)
        {
            try
            {
                ///Disable IO Read Write btns 
                if (IsReadWriteBusy && !bckWorker_Instantanouse.IsBusy)
                {
                    btn_read_all_instantaneous_values.Enabled = false;
                }
                ///Enable Read Write btns
                else
                {
                    btn_read_all_instantaneous_values.Enabled = true;
                }
                if (IsReadWriteBusy)
                {
                    gpReadInst.Enabled = false;
                }
                else
                {
                    gpReadInst.Enabled = true;
                }

                pb_ins.Visible = false;
                lbl_PbStatus.Visible = false;

            }
            catch (Exception ex)
            {
                btn_read_all_instantaneous_values.Enabled = true;
                gpReadInst.Enabled = true;
                pb_ins.Visible = false;
                lbl_PbStatus.Visible = false;
            }
        }

        public byte GetQuadrant(double Active_Power_Ph_Neg, double Active_Power_Ph_Pos, double Reactive_Power_Ph_Neg, double Reactive_Power_Ph_Pos)
        {
            byte Quadrant = 1;
            bool Active = true;
            bool Reative = true;
            if (Active_Power_Ph_Neg > Active_Power_Ph_Pos)
                Active = false;
            if (Reactive_Power_Ph_Neg > Reactive_Power_Ph_Pos)
                Reative = false;
            if (Active && Reative) Quadrant = 1;
            if (!Active && Reative) Quadrant = 2;
            if (Active && !Reative) Quadrant = 4;
            if (!Active && !Reative) Quadrant = 3;
            return Quadrant;
        }

        internal void Reset_State()
        {
            try
            {
                this.ClearGrid(this.grid_misc);
                this.ClearGrid(this.grid_MDI);
                this.ClearGrid(this.grid_CurrentMDI);
                this.ClearGrid(this.grid_misc);
                this.ClearGrid(this.grid_Instanstanouse);

                tcontrolMain.TabPages.Clear();
                btn_read_all_instantaneous_values.Enabled = false;

            }
            catch (Exception ex)
            {
            }
        }

        private void showInstantaneousData(InstantaneousData data)
        {
            try
            {
                while (grid_NEwIns.Columns.Count > 0)
                {
                    grid_NEwIns.Columns.RemoveAt(0);
                }
                //grid_NEwIns.Size = new Size(595, 324);

                grid_NEwIns.Columns.Add("PhaseA", "Phase A");
                grid_NEwIns.Columns.Add("PhaseB", "Phase B");
                grid_NEwIns.Columns.Add("PhaseC", "Phase C");
                grid_NEwIns.Columns.Add("PhaseAvg", "Phase Avg/Total");
                ClearGrid(grid_NEwIns);
                dataSet_INS_new.Clear();

                foreach (InstantaneousItem item in data.InstantaneousItems)
                {
                    grid_NEwIns.Rows.Add();
                    grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = item.Name.ToString() + "(" + item._Unit.ToString() + ")";
                    grid_NEwIns["PhaseA", grid_NEwIns.Rows.Count - 1].Value = item.Value.PhaseA.ToString("F2");
                    grid_NEwIns["PhaseB", grid_NEwIns.Rows.Count - 1].Value = item.Value.PhaseB.ToString("F2");
                    grid_NEwIns["PhaseC", grid_NEwIns.Rows.Count - 1].Value = item.Value.PhaseC.ToString("F2");
                    grid_NEwIns["PhaseAvg", grid_NEwIns.Rows.Count - 1].Value = item.Value.PhaseAvg_Total.ToString("F2");

                    // add to dataset
                    dataSet_INS_new.dataTable_P.Rows.Add();
                    dataSet_INS_new.dataTable_P[dataSet_INS_new.dataTable_P.Rows.Count - 1].Quantity = item.Name.ToString() + "(" + item._Unit.ToString() + ")";
                    dataSet_INS_new.dataTable_P[dataSet_INS_new.dataTable_P.Rows.Count - 1].Phase_A = item.Value.PhaseA.ToString("F2");
                    dataSet_INS_new.dataTable_P[dataSet_INS_new.dataTable_P.Rows.Count - 1].Phase_B = item.Value.PhaseB.ToString("F2");
                    dataSet_INS_new.dataTable_P[dataSet_INS_new.dataTable_P.Rows.Count - 1].Phase_C = item.Value.PhaseC.ToString("F2");
                    dataSet_INS_new.dataTable_P[dataSet_INS_new.dataTable_P.Rows.Count - 1].Phase_Total = item.Value.PhaseAvg_Total.ToString("F2");
                }

                DateTime temp = new DateTime(data.TimeStamp.Year,
                                             data.TimeStamp.Month,
                                             data.TimeStamp.DayOfMonth,
                                             data.TimeStamp.Hour, data.TimeStamp.Minute, data.TimeStamp.Second);
                bl.Visible = true;
                label5.Visible = true;
                lbl_Frequency.Visible = true;
                lbl_MeterDate.Visible = true;
                lbl_MeterDate.Text = temp.ToString();
                lbl_Frequency.Text = data.Frequency.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void showInstantaneousData(InstantaneousData data, bool isSinglePhase)
        {
            try
            {
                while (grid_NEwIns.Columns.Count > 0)
                {
                    grid_NEwIns.Columns.RemoveAt(0);
                }

                grid_NEwIns.Columns.Add("Value", "Value");
                grid_NEwIns.Columns["Value"].Width = 150;
                //grid_NEwIns.Size = new Size(400, 324);

                ClearGrid(grid_NEwIns);
                dataSet_INS_new.Clear();
                foreach (InstantaneousItem item in data.InstantaneousItems)
                {
                    grid_NEwIns.Rows.Add();
                    if (item._Unit.Equals(Unit.UnitLess))
                    {
                        grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = item.Name.ToString();

                    }
                    else
                    {
                        grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = item.Name.ToString() + "(" + item._Unit.ToString() + ")";
                    }
                    grid_NEwIns["Value", grid_NEwIns.Rows.Count - 1].Value = item.Value.PhaseA.ToString();

                    // add to dataset
                    // dataSet_INS_new.dataTable_P.Rows.Add();
                    // dataSet_INS_new.dataTable_P[dataSet_INS_new.dataTable_P.Rows.Count - 1].Quantity = item.Name.ToString() + "(" + item._Unit.ToString() + ")";
                    // dataSet_INS_new.dataTable_P[dataSet_INS_new.dataTable_P.Rows.Count - 1].Phase_A = item.Value.PhaseA.ToString("F2");
                    // dataSet_INS_new.dataTable_P[dataSet_INS_new.dataTable_P.Rows.Count - 1].Phase_B = item.Value.PhaseB.ToString("F2");
                    // dataSet_INS_new.dataTable_P[dataSet_INS_new.dataTable_P.Rows.Count - 1].Phase_C = item.Value.PhaseC.ToString("F2");
                    // dataSet_INS_new.dataTable_P[dataSet_INS_new.dataTable_P.Rows.Count - 1].Phase_Total = item.Value.PhaseAvg_Total.ToString("F2");

                }

                try
                {
                    DateTime temp = new DateTime(data.TimeStamp.Year, data.TimeStamp.Month, data.TimeStamp.DayOfMonth, data.TimeStamp.Hour, data.TimeStamp.Minute, data.TimeStamp.Second);
                    grid_NEwIns.Rows.Add();
                    grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "Meter Date Time";
                    grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = temp.ToString();
                    // lbl_MeterDate.Text = temp.ToString();
                }
                catch (Exception ex)
                {
                    lbl_MeterDate.Text = "Invalid Date";
                }

                grid_NEwIns.Rows.Add();
                grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "Battery Volt (V)";
                grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = Instantaneous_Class_obj.Battery_Volts.ToString();

                //lbl_BatteryVolt.Text = Instantaneous_Class_obj.Battery_Volts.ToString()+" V";

                grid_NEwIns.Rows.Add();
                grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "Meter Serial Number";
                grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = Instantaneous_Class_obj.Manufacturing_ID.ToString();


                // lbl_MSN.Text = Instantaneous_Class_obj.Manufacturing_ID.ToString();
                grid_NEwIns.Rows.Add();
                grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "Number of Power Failures";

                if (double.IsPositiveInfinity(Instantaneous_Class_obj.Power_Failures_No))
                {
                    grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = "Data Not Present";
                }
                else
                {
                    // lbl_NumberOfPowerFailures.Text = Instantaneous_Class_obj.Power_Failures_No.ToString();
                    grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = Instantaneous_Class_obj.Power_Failures_No.ToString();
                }

                grid_NEwIns.Rows.Add();
                grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "Supply Frequency (Hz)";
                grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = Instantaneous_Class_obj.Frequency_PhA.ToString();

                //lbl_Frequency.Text = Instantaneous_Class_obj.Frequency_PhA.ToString();


                grid_NEwIns.Rows.Add();
                grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "Current on Neutral (A)";
                grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = Instantaneous_Class_obj.Current_on_neutral.ToString();


                grid_NEwIns.Rows.Add();
                grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "Active Power on Neutral (kW)";
                grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = Instantaneous_Class_obj.Active_power_on_neutral.ToString();

                grid_NEwIns.Rows.Add();
                grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "Active Power on Live (kW)";
                grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = Instantaneous_Class_obj.Active_power_on_live.ToString();

                grid_NEwIns.Rows.Add();
                grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "MDI Reset Count";
                grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = Instantaneous_Class_obj.MDI_reset_count.ToString();

                grid_NEwIns.Rows.Add();
                grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "Last MDI Reset DateTime";
                grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = Instantaneous_Class_obj.Last_MDI_REset_Date.ToString();

                grid_NEwIns.Rows.Add();
                grid_NEwIns.Rows[grid_NEwIns.Rows.Count - 1].HeaderCell.Value = "RSSI Signal Strength";
                grid_NEwIns[0, grid_NEwIns.Rows.Count - 1].Value = Instantaneous_Class_obj.RSSI.ToString();

                Application.DoEvents();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private InstantaneousData Get_Instantanouse_Data_NEW()
        {
            try
            {
                Ins_data = null;
                if (application_Controller.isSinglePhase)
                {
                    Ins_data = Instantanous_Controller.ReadInstantaneousData(true);
                }
                else
                {
                    //Ins_data = Instantanous_Controller.ReadInstantaneousData();
                    Ins_data = Instantanous_Controller.ReadInstantaneousData(_Rights.InstantenousDataRights);
                }
                GetCompleted = true;
                return Ins_data;
            }
            catch (Exception)
            {
                GetCompleted = false;
                throw;
            }

        }

        private void btn_InsOldReport_Click(object sender, EventArgs e)
        {
            string meterModel = string.Empty;

            if (connectionManager != null &&
                connectionManager.ConnectionInfo != null &&
                connectionManager.ConnectionInfo.MeterInfo != null)
                meterModel = connectionManager.ConnectionInfo.MeterInfo.MeterModel;

            ReportSelection RVObj = new ReportSelection(dataSet_INS, ManufacturingID, MeterDT, meterModel, selected_Quantities,
                                                        obj_CustomerCode.Customer_Code_String,
                                                        Application_Controller.Applicationprocess_Controller.UserId,
                                                        Instantaneous_Class_obj.Active_Season.ToString(),
                                                        application_Controller.ConnectionController.SelectedMeter);

            if (dataSet_INS.dataTable_M.Count > 1 || dataSet_INS.dataTable_P.Count > 0 || dataSet_INS.dataTable_T.Count > 0)
                RVObj.ShowDialog();
            else
            {
                Notification notify = new Notification("No Report!", "You need to read Instantaneous values first!");
            }
        }

        private void bgw_NewINsRead_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Get_Instantanouse_Data_NEW();

                if (!HidePrintReportButtons)
                {
                    // get customer code
                    obj_CustomerCode.Customer_Code_String = (string)Application_Controller.Param_Controller.GETString_Any(obj_CustomerCode, Get_Index.Customer_Reference_No, 2);

                    // get active season
                    Instantaneous_Class_obj.Dummy_String = (string)Application_Controller.InstantaneousController.GETString_Any(Instantaneous_Class_obj, Get_Index.Active_Season, 2);
                    byte[] tempBytes = Encoding.ASCII.GetBytes(Instantaneous_Class_obj.Dummy_String);
                    Instantaneous_Class_obj.Active_Season = tempBytes[1];
                }

                if (application_Controller.isSinglePhase)
                {
                    Instantaneous_Class_obj.Manufacturing_ID = Instantanous_Controller.GET_Any_string(ref Instantaneous_Class_obj, Get_Index.Manufacturing_ID, 2,1);
                    Instantaneous_Class_obj.Battery_Volts = (Instantanous_Controller.GET_Any(Instantaneous_Class_obj, Get_Index.Battery_Volts, 2)) / 100;
                    Instantaneous_Class_obj.Power_Failures_No = Instantanous_Controller.GET_Any(Instantaneous_Class_obj, Get_Index.Number_of_Power_Failures_SP, 2);
                    Instantaneous_Class_obj.Frequency_PhA = (Instantanous_Controller.GET_Any(Instantaneous_Class_obj, Get_Index.Supply_Frequency, 2)) / 100;

                    Instantaneous_Class_obj.Current_on_neutral = (Instantanous_Controller.GET_Any(Instantaneous_Class_obj, Get_Index.Current_on_Neutral, 2)) / 1000;
                    Instantaneous_Class_obj.Active_power_on_neutral = (Instantanous_Controller.GET_Any(Instantaneous_Class_obj, Get_Index.Active_Power_on_Neutral, 2)) / 1000;

                    Instantaneous_Class_obj.Active_power_on_live = (Instantanous_Controller.GET_Any(Instantaneous_Class_obj, Get_Index.Active_power_on_Live, 2)) / 1000;

                    Instantaneous_Class_obj.MDI_reset_count = Convert.ToInt32((Instantanous_Controller.GET_Any(Instantaneous_Class_obj, Get_Index.Billing_Period_Counter_VZ, 2)));
                    byte[] array = Instantanous_Controller.GET_Byte_Array(ref Instantaneous_Class_obj, Get_Index._Last_MDI_Reset_Date_Time, 2,1);
                    StDateTime obj = new StDateTime();
                    obj.DecodeDateTime(array);
                    Instantaneous_Class_obj.Last_MDI_REset_Date = obj.GetDateTime();

                    Instantaneous_Class_obj.RSSI = Instantanous_Controller.GET_Any(Instantaneous_Class_obj, Get_Index.RSSI_SignalStrength, 2);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void bgw_NewINsRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pb_newIns.Visible = false;
            pb_newIns.Enabled = false;
            if (application_Controller.isSinglePhase)
            {
                showInstantaneousData(Ins_data, true);
            }
            else
            {
                showInstantaneousData(Ins_data);
            }
        }

        private void btn_GET_INS_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            byte[] All_IN_ONE_DATA = Param_Controller.GETArray_Any(Get_Index.Instantaneous_ALL_IN_ONE, 2);
            this.Cursor = Cursors.Default;
            allInOne.DECODE_Flags(All_IN_ONE_DATA, grid_EventNames);
            showToGUI_EventFlags();
            allInOne.DECODE_ALL_In_One_Data(All_IN_ONE_DATA, grv_general_instentanious, grv_event_logs, grid_EventNames, grv_commulative_billing, grv_monthly_billing);
            show_to_GUI();
            //DECODE_ALL_In_One_Data(All_IN_ONE_DATA); ;
            richTextBox1.Text = DLMS_Common.ArrayToHexString(All_IN_ONE_DATA); ;
        }
        private void show_to_GUI()
        {
            Reset_GUI();
            lbl_meterserial.Text = allInOne.meterSerial;
            lbl_meter_date_time.Text = allInOne.meter_date_time.ToString();
            lbl_day_profile.Text = "Day_Profile_" + allInOne.current_day_profile;
            lbl_season_profile.Text = "Season_Profile_" + allInOne.current_season_profile;
            if (chk_Tamper_power.Checked)
                lbl_TP.Text = allInOne.Tamper_Power.ToString() + " kw";
            if (chk_frq.Checked)
                lbl_FRQ.Text = allInOne.Frequency.ToString() + " Hz";
            if (chk_LPCount.Checked)
            {
                lbl_time_id.Text = allInOne.loadprofile.TimingID.ToString();
                lbl_channel_1.Text = allInOne.loadprofile.Channel_1_Value_ID.ToString();
                lbl_channel_2.Text = allInOne.loadprofile.Channel_2_Value_ID.ToString();
                lbl_channel_3.Text = allInOne.loadprofile.Channel_3_Value_ID.ToString();
                lbl_channel_4.Text = allInOne.loadprofile.Channel_4_Value_ID.ToString();
                lbl_count.Text = allInOne.load_profile_count.ToString();
            }
            if (chk_MdiPre.Checked)
            {
                lbl_mdi_pre_kvar.Text = allInOne.MDI_Previous_Interval_kvar.ToString();
                lbl_mdi_pre_kw.Text = allInOne.MDI_Previous_Interval_kw.ToString();
            }
            if (chk_MDITime.Checked)
            {
                lbl_timer.Text = ((int)allInOne.MDI_Timer / 60) + " : " + ((int)allInOne.MDI_Timer % 60);
                lblmdi_time.Text = ((int)allInOne.MDI_Time / 60) + " : " + ((int)allInOne.MDI_Time % 60); ;
                lbl_slide_count.Text = allInOne.MDI_Slide_Count.ToString();
            }
            if (chk_MDI_Reset.Checked)
            {
                lbl_mdi_end_date.Text = allInOne.MDI_Reset_End_Date.ToString();
                lbl_mdi_count.Text = allInOne.MDI_Reset_Count.ToString();
            }
            if (chk_AlarmSTS.Checked)
            {
                txt_alarm_status.Text = allInOne.alarm_status;
            }
            if (chk_CT.Checked)
            {
                lbl_ct_nominator.Text = allInOne.CT_Nominator.ToString();
                lbl_ct_denominator.Text = allInOne.CT_DeNominator.ToString();
            }
            if (chk_PT.Checked)
            {
                lbl_pt_nominator.Text = allInOne.PT_Nominator.ToString();
                lbl_pt_denominator.Text = allInOne.PT_DeNominator.ToString();
            }
            //show_general_Instantenious_grid();
            tabControl1.SelectedIndex = 2;
        }

        private void Reset_GUI()
        {
            lbl_ct_denominator.Text = "----";
            lbl_ct_nominator.Text = "----";
            lbl_pt_nominator.Text = "----";
            lbl_pt_denominator.Text = "----";
            txt_alarm_status.Text = "----";
            lbl_mdi_count.Text = "----";
            lbl_mdi_end_date.Text = "----";
            lbl_slide_count.Text = "----";
            lbl_mdi_pre_kvar.Text = "----";
            lbl_mdi_pre_kw.Text = "----";
            lbl_timer.Text = "----";
            lblmdi_time.Text = "----";
            lbl_TP.Text = "----";
            lbl_FRQ.Text = "----";
            lbl_time_id.Text = "----";
            lbl_channel_1.Text = "----";
            lbl_channel_2.Text = "----";
            lbl_channel_3.Text = "----";
            lbl_channel_4.Text = "----";
            lbl_count.Text = "----";
        }


        private void btn_GET_ALL_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            byte[] All_IN_ONE_FLAGS = Param_Controller.GETArray_Any(Get_Index.RW_ALL_IN_ONE, 0);
            this.Cursor = Cursors.Default;
            allInOne.DECODE_Flags(All_IN_ONE_FLAGS, grid_EventNames);
            showToGUI_EventFlags();

        }

        private void btn_SetAll_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            byte[] All_IN_ONE_FLAGS = allInOne.encodeTOByteArray(grid_EventNames);
            if (All_IN_ONE_FLAGS != null)
            {
                Base_Class RW_ALL = Param_Controller.Encode_RW_ALL_IN_ONE_string(Param_Controller.GetSAPEntry, All_IN_ONE_FLAGS);
                Param_Controller.SET_Param(RW_ALL);
            }
            else
            {
                MessageBox.Show("ALL_IN_ONE_FLAGS=null");
            }
            this.Cursor = Cursors.Default;
        }

        #region NEW TAB EVENTS
        private void chk_frq_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.FRQ = chk_frq.Checked;
            packetSize_Total = (allInOne.FRQ) ? packetSize_Total + 2 : packetSize_Total - 2;
            showPacketSize();
        }

        private void chk_T_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.TP = chk_Tamper_power.Checked;
            packetSize_Total = (allInOne.TP) ? packetSize_Total + 2 : packetSize_Total - 2;
            showPacketSize();
        }

        private void check_PF_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.PF = check_PF.Checked;
            packetSize_Total = (allInOne.PF) ? packetSize_Total + 4 : packetSize_Total - 4;
            showPacketSize();
        }

        private void chk_Q_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.Q = chk_Q.Checked;
            packetSize_Total = (allInOne.Q) ? packetSize_Total + 6 : packetSize_Total - 6;
            showPacketSize();
        }

        private void chk_P_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.P = chk_P.Checked;
            packetSize_Total = (allInOne.P) ? packetSize_Total + 6 : packetSize_Total - 6;
            showPacketSize();
        }

        private void chk_EventLog_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.EVENT_LOG = chk_EventLog.Checked;
            chk_EventCount.Checked = allInOne.EVENT_LOG;
            packetSize_Total = (allInOne.EVENT_LOG) ? packetSize_Total + 20 : packetSize_Total - 20;
            showPacketSize();
        }

        private void chk_EventCount_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.EVENT_COUNT = chk_EventCount.Checked;
            chk_EventLog.Checked = allInOne.EVENT_COUNT;
            packetSize_Total = (allInOne.EVENT_COUNT) ? packetSize_Total + 4 : packetSize_Total - 4;
            showPacketSize();
        }

        private void chk_LPCount_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.LP_COUNT = chk_LPCount.Checked;
            chk_LPLog.Checked = allInOne.LP_COUNT;
            packetSize_Total = (allInOne.LP_COUNT) ? packetSize_Total + 4 : packetSize_Total - 4;
            showPacketSize();

        }

        private void chk_LPLog_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.LP_LOG = chk_LPLog.Checked;
            chk_LPCount.Checked = allInOne.LP_LOG;
            packetSize_Total = (allInOne.LP_LOG) ? packetSize_Total + 32 : packetSize_Total - 32;
            showPacketSize();
        }

        private void chk_MDITime_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.MDI_TIME = chk_MDITime.Checked;
            packetSize_Total = (allInOne.MDI_TIME) ? packetSize_Total + 5 : packetSize_Total - 5;
            showPacketSize();
        }

        private void chk_I_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.I = chk_I.Checked;
            packetSize_Total = (allInOne.I) ? packetSize_Total + 6 : packetSize_Total - 6;
            showPacketSize();
        }

        private void chk_V_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.V = chk_V.Checked;
            packetSize_Total = (allInOne.V) ? packetSize_Total + 6 : packetSize_Total - 6;
            showPacketSize();
        }

        private void chk_S_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.S = chk_S.Checked;
            packetSize_Total = (allInOne.S) ? packetSize_Total + 4 : packetSize_Total - 4;
            showPacketSize();
        }

        private void chk_MDI_Reset_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.MDI_RESET = chk_MDI_Reset.Checked;
            packetSize_Total = (allInOne.MDI_RESET) ? packetSize_Total + 14 : packetSize_Total - 14;
            showPacketSize();
        }

        private void chk_MdiPre_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.MDI_PRE = chk_MdiPre.Checked;
            packetSize_Total = (allInOne.MDI_PRE) ? packetSize_Total + 8 : packetSize_Total - 8;
            showPacketSize();
        }

        private void chk_AlarmSTS_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.ALARM_STS = chk_AlarmSTS.Checked;
            packetSize_Total = (allInOne.ALARM_STS) ? packetSize_Total + 6 : packetSize_Total - 6;
            showPacketSize();
        }

        private void chk_CT_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.CT = chk_CT.Checked;
            packetSize_Total = (allInOne.CT) ? packetSize_Total + 8 : packetSize_Total - 8;
            showPacketSize();
        }

        private void chk_PT_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.PT = chk_PT.Checked;
            packetSize_Total = (allInOne.PT) ? packetSize_Total + 8 : packetSize_Total - 8;
            showPacketSize();
        }

        private void chk_MDIQ15_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.MDI_Q15 = chk_MDIQ15.Checked;
            packetSize_Energies = (allInOne.MDI_Q15) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();

        }

        private void chk_MDIQ16_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.MDI_Q16 = chk_MDIQ16.Checked;
            packetSize_Energies = (allInOne.MDI_Q16) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void chk_MDIP15_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.MDI_P15 = chk_MDIP15.Checked;
            packetSize_Energies = (allInOne.MDI_P15) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void chk_MDIP16_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.MDI_P16 = chk_MDIP16.Checked;
            packetSize_Energies = (allInOne.MDI_P16) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_T15_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.T15 = check_T15.Checked;
            packetSize_Energies = (allInOne.T15) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();

        }

        private void check_T16_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.T16 = check_T16.Checked;
            packetSize_Energies = (allInOne.T16) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_S15_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.S15 = check_S15.Checked;
            packetSize_Energies = (allInOne.S15) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_S16_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.S16 = check_S16.Checked;
            packetSize_Energies = (allInOne.S16) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_Q15_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.Q15 = check_Q15.Checked;
            packetSize_Energies = (allInOne.Q15) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_Q16_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.Q16 = check_Q16.Checked;
            packetSize_Energies = (allInOne.Q16) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_QAbs15_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.QABS15 = check_QAbs15.Checked;
            packetSize_Energies = (allInOne.QABS15) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_QAbs16_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.QABS16 = check_QAbs16.Checked;
            packetSize_Energies = (allInOne.QABS16) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_P15_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.P15 = check_P15.Checked;
            packetSize_Energies = (allInOne.P15) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_P16_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.P16 = check_P16.Checked;
            packetSize_Energies = (allInOne.P16) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_PABS15_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.PABS15 = check_PABS15.Checked;
            packetSize_Energies = (allInOne.PABS15) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void check_PABS16_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.PABS16 = check_PABS16.Checked;
            packetSize_Energies = (allInOne.PABS16) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void chk_CumMDIQ_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.CUM_MDI_Q = chk_CumMDIQ.Checked;
            packetSize_Energies = (allInOne.CUM_MDI_Q) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();
        }

        private void chk_CUMMDIP_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.CUM_MDI_P = chk_CUMMDIP.Checked;
            packetSize_Energies = (allInOne.CUM_MDI_P) ? packetSize_Energies + 4 : packetSize_Energies - 4;
            showPacketSize();

        }

        private void chk_T1_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.T1 = chk_T1.Checked;
            tariffCount = (allInOne.T1) ? tariffCount + 1 : tariffCount - 1;
            if (!isAnyChecked()) chk_T1.Checked = true;
            showPacketSize();
        }

        private void chk_T2_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.T2 = chk_T2.Checked;
            tariffCount = (allInOne.T2) ? tariffCount + 1 : tariffCount - 1;
            if (!isAnyChecked()) chk_T1.Checked = true;
            showPacketSize();
        }

        private void chk_T3_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.T3 = chk_T3.Checked;
            tariffCount = (allInOne.T3) ? tariffCount + 1 : tariffCount - 1;
            if (!isAnyChecked()) chk_T1.Checked = true;
            showPacketSize();
        }

        private void chk_T4_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.T4 = chk_T4.Checked;
            tariffCount = (allInOne.T4) ? tariffCount + 1 : tariffCount - 1;
            if (!isAnyChecked()) chk_T1.Checked = true;
            showPacketSize();
        }

        private void chk_TL_CheckedChanged(object sender, EventArgs e)
        {
            allInOne.TL = chk_TL.Checked;
            tariffCount = (allInOne.TL) ? tariffCount + 1 : tariffCount - 1;
            if (!isAnyChecked()) chk_T1.Checked = true;
            showPacketSize();
        }




        private void grid_EventNames_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showToGUI_EventFlags()
        {
            chk_AlarmSTS.Checked = allInOne.ALARM_STS;
            chk_CT.Checked = allInOne.CT;
            chk_CUMMDIP.Checked = allInOne.CUM_MDI_P;
            chk_CumMDIQ.Checked = allInOne.CUM_MDI_Q;
            chk_EventCount.Checked = allInOne.EVENT_COUNT;
            chk_EventLog.Checked = allInOne.EVENT_LOG;
            chk_frq.Checked = allInOne.FRQ;
            chk_I.Checked = allInOne.I;
            chk_LPCount.Checked = allInOne.LP_COUNT;
            chk_LPLog.Checked = allInOne.LP_LOG;
            chk_MDI_Reset.Checked = allInOne.MDI_RESET;
            chk_MDIP15.Checked = allInOne.MDI_P15;
            chk_MDIP16.Checked = allInOne.MDI_P16;
            chk_MdiPre.Checked = allInOne.MDI_PRE;
            chk_MDIQ15.Checked = allInOne.MDI_Q15;
            chk_MDIQ16.Checked = allInOne.MDI_Q16;
            chk_MDITime.Checked = allInOne.MDI_TIME;
            chk_P.Checked = allInOne.P;
            chk_PT.Checked = allInOne.PT;
            chk_Q.Checked = allInOne.Q;
            chk_S.Checked = allInOne.S;
            chk_Tamper_power.Checked = allInOne.TP;
            chk_T1.Checked = allInOne.T1;
            chk_T2.Checked = allInOne.T2;
            chk_T3.Checked = allInOne.T3;
            chk_T4.Checked = allInOne.T4;
            chk_TL.Checked = allInOne.TL;
            chk_V.Checked = allInOne.V;

            check_P15.Checked = allInOne.P15;
            check_P16.Checked = allInOne.P16;
            check_PABS15.Checked = allInOne.PABS15;
            check_PABS16.Checked = allInOne.PABS16;
            check_PF.Checked = allInOne.PF;
            check_Q15.Checked = allInOne.Q15;
            check_Q16.Checked = allInOne.Q16;
            check_QAbs15.Checked = allInOne.QABS15;
            check_QAbs16.Checked = allInOne.QABS16;
            check_S15.Checked = allInOne.S15;
            check_S16.Checked = allInOne.S16;
            check_T15.Checked = allInOne.T15;
            check_T16.Checked = allInOne.T16;

        }
        private int GetTotalofGrid()
        {
            gridCount = 0;
            for (int i = 0; i < grid_EventNames.Rows.Count; i++)
            {
                gridCount = Convert.ToBoolean((grid_EventNames[1, i].Value)) ? gridCount + 20 : gridCount;
            }
            for (int i = 0; i < grid_EventNames.Rows.Count; i++)
            {
                gridCount = Convert.ToBoolean((grid_EventNames[2, i].Value)) ? gridCount + 3 : gridCount;
            }

            return gridCount;
        }

        private void getTotal()
        {
            int Fromgrid = GetTotalofGrid();
            TOTAL = packetSize_Total + packetSize_Energies * tariffCount + Fromgrid;
        }
        private void showPacketSize()
        {
            getTotal();
            lbl_packetSize.Text = TOTAL.ToString();
        }

        private bool isAnyChecked()
        {
            return allInOne.T1 || allInOne.T2 || allInOne.T3 || allInOne.T4 || allInOne.TL;
        }


        #endregion

        private void btn_get_Dwd_Click(object sender, EventArgs e)
        {
            bgw_DisplayWindows.RunWorkerAsync();
            progressBar1.Visible = true;
        }

        private void bgw_DisplayWindows_DoWork(object sender, DoWorkEventArgs e)
        {
            string s = null;
            if (radio_nor.Checked)
            {
                s = Instantanous_Controller.GetDisplayWindowData(0);
            }
            else if (radio_alt.Checked)
            {
                s = Instantanous_Controller.GetDisplayWindowData(1);
            }
            else if (radio_test.Checked)
            {
                s = Instantanous_Controller.GetDisplayWindowData(2);

            }
            rtb_1.Text = s;
        }

        private void bgw_DisplayWindows_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;

        }

        private void btn_ReadRawData_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] Byte_Array = new byte[400];
                Byte_Array = Instantanous_Controller.GETArray_Any(Get_Index.Read_Raw_data, 2);
                txt_general.Text = DLMS_Common.ArrayToHexString(Byte_Array);
                // string s = Instantanous_Controller.GETString_Any(Instantaneous_Class_obj, Get_Index.Read_Raw_data, 2);
                // txt_general.Text = s;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void txt_RawDataAddress_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rawData.Address = Convert.ToUInt16(txt_RawDataAddress.Text);
            }
            catch (Exception)
            {
            }
        }

        private void txt_RawDataLength_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rawData.Length = Convert.ToUInt16(txt_RawDataLength.Text);
            }
            catch (Exception)
            {


            }
        }

        private void txt_EPMNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rawData.EPM_Number = Convert.ToByte(txt_EPMNumber.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btn_Set_ReadRawData_Click(object sender, EventArgs e)
        {
            Data_Access_Result result = Param_Controller.SET_Read_Raw_Data(rawData);
        }

        private void btn_CBDayRecord_Click(object sender, EventArgs e)
        {


        }

        private void btn_TLLoadProfile_Click(object sender, EventArgs e)
        {
            List<TL_LoadProfile> TL_LoadProfileRecord = Instantanous_Controller.Read_TLLOADPROFILE_Record();

            foreach (var item in TL_LoadProfileRecord)
            {
                txt_general.Text += DLMS_Common.ArrayToHexString(item.RAWBYTES);
                txt_general.Text += "\r\n\r\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txt_general.Text = "";
        }

        private void btn_NewIns_Click(object sender, EventArgs e)
        {
            pb_newIns.Visible = true;
            pb_newIns.Enabled = true;
            bgw_NewINsRead.RunWorkerAsync();
        }

        private void btn_INS_Report_Click(object sender, EventArgs e)
        {
            DateTime temp = new DateTime(Ins_data.TimeStamp.Year, Ins_data.TimeStamp.Month, Ins_data.TimeStamp.DayOfMonth, Ins_data.TimeStamp.Hour, Ins_data.TimeStamp.Minute, Ins_data.TimeStamp.Second);
            //ds_Ins tempDS, string MSN, string MeterDT, string Model, string customerCode, ushort pid, string active_season, MeterConfig meter_type_info, ApplicationRight _currentAccessRights, ReportFormat rptFormat
            ReportViewer RVObj = new ReportViewer(
                                        dataSet_INS_new,
                                        application_Controller.ConnectionManager.ConnectionInfo.MSN,
                                        temp.ToString(),
                                        Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.MeterModel,
                                        obj_CustomerCode.Customer_Code_String,
                                        Application_Controller.Applicationprocess_Controller.UserId,
                                        Instantaneous_Class_obj.Active_Season.ToString(),
                                        connectionManager.ConnectionInfo.MeterInfo,
                                        _Rights); //,
            //ReportFormat.WEB_GALAXY);
            if (dataSet_INS_new.dataTable_P.Rows.Count > 0)
                RVObj.ShowDialog();
            else
            {
                Notification notify = new Notification("No Report!", "You need to read Instantaneous values first!");
            }
        }

        #region CB_DAY_RECORD

        private void btn_CBDayRecord_Click_1(object sender, EventArgs e)
        {
            try
            {
                pnl_TL_Load_Profile.Visible = false;
                pnl_cb_day_record.Visible = true;
                this.Cursor = Cursors.WaitCursor;
                pnl_cb_day_record.Enabled = false;
                CBdayRecord = Instantanous_Controller.ReadCumulativeDayRecord();
                this.Cursor = Cursors.Default;
                pnl_cb_day_record.Enabled = true;
                record_index = 0;
                show_cb_day_record_on_grid(record_index);
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                pnl_cb_day_record.Enabled = true;
            }

        }

        private void show_cb_day_record_on_grid(int record_index)
        {
            CB_DayRecord record = CBdayRecord[record_index];
            ds_day_record.CB_Day_Record.Clear();
            record.Decode_Any();


            lbl_record_no.Text = (record_index + 1).ToString();
            lbl_total_records.Text = CBdayRecord.Count.ToString();
            lbl_record_counter.Text = record.ThisRecordCounter.ToString();
            lbl_this_date_time.Text = record.ThisDateTime.ToString();
            lbl_last_reset_date_time.Text = record.LastResetDateTime.ToString();

            CB_Day_Recoord_Tarrification T1 = record.tarrifs[0];
            CB_Day_Recoord_Tarrification T2 = record.tarrifs[1];
            CB_Day_Recoord_Tarrification T3 = record.tarrifs[2];
            CB_Day_Recoord_Tarrification T4 = record.tarrifs[3];
            CB_Day_Recoord_Tarrification TL = record.tarrifs[4];

            Add_Row_To_DataSet_Cb_Day_Record("KWH_P", T1.KWH_P.ToString(), T2.KWH_P.ToString(), T3.KWH_P.ToString(), T4.KWH_P.ToString(), TL.KWH_P.ToString());
            Add_Row_To_DataSet_Cb_Day_Record("KWH_N", T1.KWH_N.ToString(), T2.KWH_N.ToString(), T3.KWH_N.ToString(), T4.KWH_N.ToString(), TL.KWH_N.ToString());

            Add_Row_To_DataSet_Cb_Day_Record("Kvarh Q1", T1.KVarh[0].ToString(), T2.KVarh[0].ToString(), T3.KVarh[0].ToString(), T4.KVarh[0].ToString(), TL.KVarh[0].ToString());
            Add_Row_To_DataSet_Cb_Day_Record("Kvarh Q2", T1.KVarh[1].ToString(), T2.KVarh[1].ToString(), T3.KVarh[1].ToString(), T4.KVarh[1].ToString(), TL.KVarh[1].ToString());
            Add_Row_To_DataSet_Cb_Day_Record("Kvarh Q3", T1.KVarh[2].ToString(), T2.KVarh[2].ToString(), T3.KVarh[2].ToString(), T4.KVarh[2].ToString(), TL.KVarh[2].ToString());
            Add_Row_To_DataSet_Cb_Day_Record("Kvarh Q4", T1.KVarh[3].ToString(), T2.KVarh[3].ToString(), T3.KVarh[3].ToString(), T4.KVarh[3].ToString(), TL.KVarh[3].ToString());
            Add_Row_To_DataSet_Cb_Day_Record("KVAH", T1.Kvah.ToString(), T2.Kvah.ToString(), T3.Kvah.ToString(), T4.Kvah.ToString(), TL.Kvah.ToString());
            Add_Row_To_DataSet_Cb_Day_Record("Tamper", T1.Tamper.ToString(), T2.Tamper.ToString(), T3.Tamper.ToString(), T4.Tamper.ToString(), TL.Tamper.ToString());
            Add_Row_To_DataSet_Cb_Day_Record("MDI_KW", T1.MDI_kw.ToString(), T2.MDI_kw.ToString(), T3.MDI_kw.ToString(), T4.MDI_kw.ToString(), TL.MDI_kw.ToString());
            Add_Row_To_DataSet_Cb_Day_Record("MDI_KVAR", T1.MDI_kvar.ToString(), T2.MDI_kvar.ToString(), T3.MDI_kvar.ToString(), T4.MDI_kvar.ToString(), TL.MDI_kvar.ToString());
            Add_Row_To_DataSet_Cb_Day_Record("PF", T1.pf.ToString(), T2.pf.ToString(), T3.pf.ToString(), T4.pf.ToString(), TL.pf.ToString());
            Add_Row_To_DataSet_Cb_Day_Record("MDI Capture Time", T1.MDI_CaptureTIme.ToString(), T2.MDI_CaptureTIme.ToString(), T3.MDI_CaptureTIme.ToString(), T4.MDI_CaptureTIme.ToString(), TL.MDI_CaptureTIme.ToString());
            //Add_Row_To_DataSet_Cb_Day_Record("CRC16", T1.CRC16.ToString(), T2.CRC16.ToString(), T3.CRC16.ToString(), T4.CRC16.ToString(), TL.CRC16.ToString());
            //txt_general.Text += "\n MDI_CaptureTIme = " + new string(item.MDI_CaptureTIme);


            txt_general.Text = DLMS_Common.ArrayToHexString(CBdayRecord[record_index].RAWBYTES);
            grid_view_cb_day_record.DataSource = ds_day_record.CB_Day_Record;

        }

        private void Add_Row_To_DataSet_Cb_Day_Record(string title, string t1, string t2, string t3, string t4, string tl)
        {
            ds_day_record.CB_Day_Record.Rows.Add();
            ds_day_record.CB_Day_Record[ds_day_record.CB_Day_Record.Rows.Count - 1].title = title;
            ds_day_record.CB_Day_Record[ds_day_record.CB_Day_Record.Rows.Count - 1].T1 = t1;
            ds_day_record.CB_Day_Record[ds_day_record.CB_Day_Record.Rows.Count - 1].T2 = t2;
            ds_day_record.CB_Day_Record[ds_day_record.CB_Day_Record.Rows.Count - 1].T3 = t3;
            ds_day_record.CB_Day_Record[ds_day_record.CB_Day_Record.Rows.Count - 1].T4 = t4;
            ds_day_record.CB_Day_Record[ds_day_record.CB_Day_Record.Rows.Count - 1].TL = tl;
        }

        #endregion

        #region TL_Load_Profile

        private void btn_TLLoadProfile_Click_1(object sender, EventArgs e)
        {
            // pnl_cb_day_record.Visible = false;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                pnl_TL_Load_Profile.Visible = true;
                pnl_TL_Load_Profile.Enabled = false;
                //pnl_TL_Load_Profile.BringToFront();
                List<TL_LoadProfile> TL_LoadProfileRecord = Instantanous_Controller.Read_TLLOADPROFILE_Record();
                ds_tl_load_profile.TL_LOAD_PROFILE.Clear();
                foreach (var item in TL_LoadProfileRecord)
                {
                    item.DECODE_Any();
                    Add_Row_To_DataSet_TL_LOAD_PROFILE_Record(item.datetime.ToString(), item.counter.ToString(), item.TL_Record.KWH_P.ToString(), item.TL_Record.KWH_N.ToString(), item.TL_Record.KVarh[0].ToString(), item.TL_Record.KVarh[1].ToString(), item.TL_Record.KVarh[2].ToString(), item.TL_Record.KVarh[3].ToString(), item.TL_Record.Kvah.ToString(), item.TL_Record.Tamper.ToString(), item.TL_Record.MDI_kw.ToString(), item.TL_Record.MDI_kvar.ToString(), item.TL_Record.pf.ToString(), item.TL_Record.MDI_CaptureTIme.ToString().ToString(), item.MDI_thatDay_kw.ToString(), item.MDI_thatDay_kVAr.ToString());
                    txt_general.Text = DLMS_Common.ArrayToHexString(item.RAWBYTES);
                }
                grid_tl_load_profile.DataSource = new DS_TL_LOAD_PROFILE().TL_LOAD_PROFILE;

                grid_tl_load_profile.DataSource = ds_tl_load_profile.TL_LOAD_PROFILE;
                this.Cursor = Cursors.Default;
                pnl_TL_Load_Profile.Enabled = true;
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                pnl_TL_Load_Profile.Enabled = true;
                throw;
            }
        }


        private void Add_Row_To_DataSet_TL_LOAD_PROFILE_Record(string date, string counter, string kwh_p, string kwh_n, string kvarh_q1, string kvarh_q2, string kvarh_q3, string kvarh_q4, string kvah, string tamper, string mdi_kw, string mdi_kvar, string pf, string capture_time, string day_mdi_kw, string day_mdi_kvar)
        {
            ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Add();
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].DATE = date;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].COUNTER = counter;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].KWH_P = kwh_p;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].KWH_N = kwh_n;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].KVARH_Q1 = kvarh_q1;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].KVARH_Q2 = kvarh_q2;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].KVARH_Q3 = kvarh_q3;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].KVARH_Q4 = kvarh_q4;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].KVAH = kvah;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].TAMPER = tamper;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].MDI_KW = mdi_kw;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].MDI_KVAR = mdi_kvar;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].PF = pf;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].CAPTURE_TIME = capture_time;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].DAY_MDI_KW = day_mdi_kw;
            ds_tl_load_profile.TL_LOAD_PROFILE[ds_tl_load_profile.TL_LOAD_PROFILE.Rows.Count - 1].DAY_MDI_KVAR = day_mdi_kvar;
        }


        #endregion

        private void btn_previous_Click(object sender, EventArgs e)
        {
            if (record_index > 0) show_cb_day_record_on_grid(--record_index);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (record_index < CBdayRecord.Count - 1) show_cb_day_record_on_grid(++record_index);
        }

        private void grid_EventNames_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grid_EventNames.CurrentCell != null)
                {
                    int currentCol = e.ColumnIndex;
                    int currentRow = e.RowIndex;
                    int nextCol = currentCol + 1;
                    int prevCol = currentCol - 1;
                    grid_EventNames[currentCol, currentRow].Value = !(Convert.ToBoolean(grid_EventNames[currentCol, currentRow].Value));
                    if (currentCol == 1) //Lower Byte
                    {
                        if (Convert.ToBoolean(grid_EventNames[currentCol, currentRow].Value))
                        {
                            grid_EventNames[nextCol, currentRow].Value = false;
                            Application.DoEvents();
                        }
                        else
                        {
                            ;
                        }
                    }
                    else if (currentCol == 2) //upper byte
                    {
                        if (Convert.ToBoolean(grid_EventNames[currentCol, currentRow].Value))
                        {
                            grid_EventNames[prevCol, currentRow].Value = false;

                            Application.DoEvents();

                        }
                        else
                        {
                            ;
                        }
                    }

                }
                showPacketSize();
            }
            catch
            {

            }
        }

        private void rb_check_all_log_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 48; i++)
            {
                grid_EventNames[1, i].Value = true;
                grid_EventNames[2, i].Value = false;
            }
            showPacketSize();
        }

        private void rb_Counter_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 48; i++)
            {
                grid_EventNames[1, i].Value = false;
                grid_EventNames[2, i].Value = true;
            }
            showPacketSize();
        }

        private void rb_uuncheck_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 48; i++)
            {
                grid_EventNames[1, i].Value = false;
                grid_EventNames[2, i].Value = false;
            }
            showPacketSize();
        }

        private void grid_tl_load_profile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void chk_check_all_cummulative_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_check_all_cummulative.Checked)
            {
                chk_MDIQ15.Checked = true;
                chk_MDIP15.Checked = true;
                check_T15.Checked = true;
                check_S15.Checked = true;
                check_Q15.Checked = true;
                check_P15.Checked = true;
                check_QAbs15.Checked = true;
                check_PABS15.Checked = true;
                chk_CUMMDIP.Checked = true;
                chk_CumMDIQ.Checked = true;
            }
            else
            {
                chk_MDIQ15.Checked = false;
                chk_MDIP15.Checked = false;
                check_T15.Checked = false;
                check_S15.Checked = false;
                check_Q15.Checked = false;
                check_P15.Checked = false;
                check_QAbs15.Checked = false;
                check_PABS15.Checked = false;
                chk_CUMMDIP.Checked = false;
                chk_CumMDIQ.Checked = false;
            }
        }

        private void chk_all_monthly_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_all_monthly.Checked)
            {
                chk_MDIQ16.Checked = true;
                chk_MDIP16.Checked = true;
                check_T16.Checked = true;
                check_S16.Checked = true;
                check_Q16.Checked = true;
                check_P16.Checked = true;
                check_QAbs16.Checked = true;
                check_PABS16.Checked = true;
            }
            else
            {
                chk_MDIQ16.Checked = false;
                chk_MDIP16.Checked = false;
                check_T16.Checked = false;
                check_S16.Checked = false;
                check_Q16.Checked = false;
                check_P16.Checked = false;
                check_QAbs16.Checked = false;
                check_PABS16.Checked = false;
            }
        }
        #region Contactor Control Status

        private void ShowContactorControlFlags(byte val)
        {
            chkOverLoadByPhase.Checked = ((val & 0x01) == 0x01);
            chkOverLoadByTotal.Checked = ((val & 0x02) == 0x02);
            chkFailureStateDetected.Checked = ((val & 0x04) == 0x04);
        }

        private void ShowContactorControlFlagsTrig(byte val)
        {
            chkOverLoadByPhaseTrig.Checked = ((val & 0x01) == 0x01);
            chkOverLoadByTotalTrig.Checked = ((val & 0x02) == 0x02);
            chkFailureStateDetectedTrig.Checked = ((val & 0x04) == 0x04);
        }

        private string GetContactorState(byte value)
        {
            string state = string.Empty;
            switch (value)
            {
                default: state = string.Format("{0}", value); break;
                case 0: state = "Waiting"; break;
                case 1: state = "Normal"; break;
                case 2: state = "Disabled"; break;
                case 3: state = "Forcely Off By Remote"; break;
                case 4: state = "Forcely Off By Retry Finished"; break;
                case 5: state = "Off for retry"; break;
                case 6: state = "On for Retry"; break;
                case 7: state = "ON on Tariff Change"; break;
                case 8: state = "Schedully Off"; break;
            }
            return state;
        }

        private void ShowContactorStatus(byte[] flag_array, int offset)
        {
            check_Contactor.Checked = ((flag_array[offset + 0] & 0x01) == 0x01);
            check_Overload.Checked = ((flag_array[offset + 0] & 0x02) == 0x02);
            check_overCurrent.Checked = ((flag_array[offset + 0] & 0x04) == 0x04);
            check_MDIExceed.Checked = ((flag_array[offset + 0] & 0x08) == 0x08);
            check_overVolt.Checked = ((flag_array[offset + 0] & 0x10) == 0x10);
            check_underVolt.Checked = ((flag_array[offset + 0] & 0x20) == 0x20);
            chkDisabled.Checked = ((flag_array[offset + 0] & 0x40) == 0x40);

            check_offByRetryExpire.Checked = ((flag_array[offset + 1] & 0x01) == 0x01);
            check_offByIRDAcommand.Checked = ((flag_array[offset + 1] & 0x02) == 0x02);
            check_offbyRemoteCommand.Checked = ((flag_array[offset + 1] & 0x04) == 0x04);
            check_onBySwitchwithRemote.Checked = ((flag_array[offset + 1] & 0x08) == 0x08);
            check_onBySwitch.Checked = ((flag_array[offset + 1] & 0x10) == 0x10);
            check_OnTariffChange.Checked = ((flag_array[offset + 1] & 0x20) == 0x20);
            check_onIRDAcommand.Checked = ((flag_array[offset + 1] & 0x40) == 0x40);
            check_onRemoteCommand.Checked = ((flag_array[offset + 1] & 0x80) == 0x80);

            lblRetryCount.Text = string.Format("{0:000}", flag_array[offset + 2]);
            lblTariffIndex_Status.Text = string.Format("{0:000}", (flag_array[offset + 3] & 0x0f));

            chkRecoverFromPowerDown.Checked = ((flag_array[offset + 3] & 0x10) == 0x10);
            chkFailureStateDetected_State.Checked = ((flag_array[offset + 3] & 0x20) == 0x20);
        }

        private void ShowContactorCommonFlags(ushort flags)
        {
            chkMakePulseContactor.Checked = ((flags & 0x0001) == 0x0001);
            chkContactorToOn.Checked = ((flags & 0x0002) == 0x0002);
            chkDelayBWContactorState.Checked = ((flags & 0x0004) == 0x0004);
            chkPUDContactor.Checked = ((flags & 0x0008) == 0x0008);
            chkMakeCOntactorEvent.Checked = ((flags & 0x0010) == 0x0010);
            chkIsCapCharged.Checked = ((flags & 0x0020) == 0x0020);
            chkContactorState.Checked = ((flags & 0x0040) == 0x0040);
            chkContactorDisabled.Checked = ((flags & 0x0080) == 0x0080);
            chkContactorEventIndex.Checked = ((flags & 0x0100) == 0x0100);
            chkWaitOnTariffChange.Checked = ((flags & 0x0200) == 0x0200);
            chkOnForRetryBySwitch.Checked = ((flags & 0x0400) == 0x0400);
            chkSkipSchedule.Checked = ((flags & 0x1000) == 0x1000);
        }



        private void btn_GetContactorFlags_Click(object sender, EventArgs e)
        {
            // list_contactor.Items.Clear();
            byte[] flag_array = Instantanous_Controller.GETArray_Any(Get_Index.I_Contactor_Flag, 2);

            if (flag_array != null)
            {
                lblRetryCounter.Text = string.Format("{0:000}", flag_array[0]);
                lblState.Text = GetContactorState(flag_array[1]);
                lblStateShouldBe.Text = GetContactorState(flag_array[2]);
                lblTariffIndex.Text = string.Format("{0:000}", flag_array[3]);
                lblConnectThroughSwitch.Text = string.Format("{0:000}", flag_array[4]);
                lblTimerX2.Text = string.Format("{0:000}", flag_array[5]);

                ShowContactorControlFlags(flag_array[6]);
                ShowContactorControlFlagsTrig(flag_array[7]);
                ShowContactorStatus(flag_array, 8);


                lblTimerX.Text = string.Format("{0:000}", LocalCommon.ByteToShort(flag_array[12], flag_array[13]));
                lblFailureStateTimer.Text = string.Format("{0:000}", LocalCommon.ByteToShort(flag_array[14], flag_array[15]));
                lblWaitOnTariffChange.Text = string.Format("{0:000}", LocalCommon.ByteToShort(flag_array[16], flag_array[17]));

                ShowContactorCommonFlags(LocalCommon.ByteToShort(flag_array[18], flag_array[19]));

                lblScheduleIndex.Text = string.Format("{0:000}", flag_array[20]);
                lblSkippedScheduleIndex.Text = string.Format("{0:000}", flag_array[21]);
            }
        }

        #endregion


        private void resetFlags()
        {
            check_Contactor.Checked = false;
            check_Overload.Checked = false;
            check_overCurrent.Checked = false;
            check_MDIExceed.Checked = false;
            check_overVolt.Checked = false;
            check_underVolt.Checked = false;
            // check_RFU.Checked = false;
            //check_Contactor.Checked = false;
            check_offByRetryExpire.Checked = false;
            check_offByIRDAcommand.Checked = false;
            check_offbyRemoteCommand.Checked = false;
            check_onBySwitchwithRemote.Checked = false;
            check_onBySwitch.Checked = false;
            check_OnTariffChange.Checked = false;
            check_onIRDAcommand.Checked = false;
            check_onRemoteCommand.Checked = false;

        }

        private void label64_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private List<QuantityMDI> PopulateMDIsToRead()
        {
            List<QuantityMDI> MDIsToRead = new List<QuantityMDI>();

            // Current MOnth Active MDI
            MDIsToRead.Add(new QuantityMDI("Current Month MDI kW Import", Get_Index.Monthly_Active_MDI_Positive_TL));
            MDIsToRead.Add(new QuantityMDI("Current Month MDI kW Export", Get_Index.Monthly_Active_MDI_Negative_TL));
            MDIsToRead.Add(new QuantityMDI("Current Month MDI kW Absolute", Get_Index.Monthly_Active_MDI_Absolute_TL));

            MDIsToRead.Add(new QuantityMDI("Current Month MDI kVar Import", Get_Index.Monthly_Reactive_MDI_Absolute_TL));
            MDIsToRead.Add(new QuantityMDI("Current Month MDI kVar Export", Get_Index.Monthly_Reactive_MDI_Negative_TL));
            MDIsToRead.Add(new QuantityMDI("Current Month MDI kVar Absolute", Get_Index.MONTHLY_REACTIVE_MDI_ABSOLUTE_TOTAL));

            MDIsToRead.Add(new QuantityMDI("Previous Month MDI kW Import", Get_Index.PREVIOUS_MONTH_MDI_KW_P_TOTAL));
            MDIsToRead.Add(new QuantityMDI("Previous Month MDI kW Export", Get_Index.PREVIOUS_MONTH_MDI_KW_N_TOTAL));
            MDIsToRead.Add(new QuantityMDI("Previous Month MDI kW Absolute", Get_Index.PB_PREVIOUS_MONTH_MDI_KW_TL));

            MDIsToRead.Add(new QuantityMDI("Previous Month MDI kVar Import", Get_Index.PREVIOUS_MONTH_MDI_KVAR_P_TOTAL));
            MDIsToRead.Add(new QuantityMDI("Previous Month MDI kVar Export", Get_Index.PREVIOUS_MONTH_MDI_KVAR_N_TOTAL));
            MDIsToRead.Add(new QuantityMDI("Previous Month MDI kVar Absolute", Get_Index.PB_PREVIOUS_MONTH_MDI_KVAR_TL));

            MDIsToRead.Add(new QuantityMDI("Cumulative MDI kW Import", Get_Index.Active_MDI_Positive_TL));
            MDIsToRead.Add(new QuantityMDI("Cumulative MDI kW Export", Get_Index.Active_MDI_Negative_TL));
            MDIsToRead.Add(new QuantityMDI("Cumulative MDI kW Absolute", Get_Index.Active_MDI_Absolute_TL));

            MDIsToRead.Add(new QuantityMDI("Cumulative MDI kVar Import", Get_Index.Reactive_MDI_Positive_TL));
            MDIsToRead.Add(new QuantityMDI("Cumulative MDI kVar Export", Get_Index.Reactive_MDI_Negative_TL));
            MDIsToRead.Add(new QuantityMDI("Cumulative MDI kVar Absolute", Get_Index.CUM_MDI_KVAR_TOTAL));
            //chkLstMDIs.Items.Clear();
            //chkLstMDIs.Items.AddRange(MDIsToRead.ToArray());
            return MDIsToRead;
        }

        private void listInstantaneousMDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowToGUI_InstantaneousMDI(MDIValues[listInstantaneousMDI.SelectedIndex]);
        }
        private void ShowToGUI_InstantaneousMDI(Class_5 objClass5)
        {
            string defaultVal = "---";
            if (objClass5.CValue_Array != null || objClass5.CValue != null)
                lblCurrentAverageValue.Text = objClass5.CValue_Array == null ? objClass5.CValue.ToString() : LocalCommon.ArrayToHexString(objClass5.CValue_Array);
            else
                lblCurrentAverageValue.Text = defaultVal;
            if (objClass5.LValue_Array != null || objClass5.LValue != null)
                lblLastAverageValue.Text = objClass5.LValue_Array == null ? objClass5.LValue.ToString() : LocalCommon.ArrayToHexString(objClass5.LValue_Array);
            else
                lblLastAverageValue.Text = defaultVal;
            lblScalerUnit.Text = objClass5.scaler.ToString() + " " + Enum.GetName(typeof(Unit), objClass5.Unit);
            if (objClass5.capture_time != null)
            {
                objClass5.capture_time.Kind = StDateTime.DateTimeType.ShortDateTime;
                lblCaptureTime.Text = objClass5.capture_time.ToString();
            }
            else
                lblCaptureTime.Text = defaultVal;;
            if (objClass5.start_time_Current != null)
            {
                objClass5.start_time_Current.Kind = StDateTime.DateTimeType.ShortDateTime;
                lblStartCaptureTime.Text = objClass5.start_time_Current.ToString();
            }
            else
                lblStartCaptureTime.Text = defaultVal;

            lblPeriod.Text = objClass5.period.ToString();
            lblNumberOfPeriods.Text = objClass5.periodCount.ToString();
        }

        private void lblReadMDI_Click(object sender, EventArgs e)
        {
            if (Application_Process.Is_Association_Developed)
            {
                MDIValues[listInstantaneousMDI.SelectedIndex].DecodingAttribute = 0;
                MDIValues[listInstantaneousMDI.SelectedIndex] = (Class_5)Instantanous_Controller.GET_Object(MDIValues[listInstantaneousMDI.SelectedIndex]);
                ShowToGUI_InstantaneousMDI(MDIValues[listInstantaneousMDI.SelectedIndex]);
                Notification Notifier = new Notification("Success", "Read Successful");
            }
            else
            {
                Notification Notifier = new Notification("Error", "Please connect meter");
            }
        }

        private void btnSetMonitoredValue_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (Application_Process.Is_Association_Developed)
                {
                    Data_Access_Result result = Param_Controller.Set_MDI_ToMonitor(MDIOBISCodes[cbxMditoMonitor.SelectedIndex]);
                    Notification Notifier = new Notification("Result", result.ToString());
                }
                else
                {
                    Notification Notifier = new Notification("Error", "Please connect meter");
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error", ex.Message);
            }
        }

        private void btnGetMditoMonitor_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application_Process.Is_Association_Developed)
                {
                    StOBISCode result = Param_Controller.GET_MDI_ToMonitor();
                    cbxMditoMonitor.Text = Enum.GetName(typeof(Get_Index), result.OBISIndex);
                    Notification Notifier = new Notification("Result", "Success");
                }
                else
                {
                    Notification Notifier = new Notification("Error", "Please connect meter");
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error", ex.Message);
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(ApplicationRight Rights)
        {
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                this.tcontrolMain.SuspendLayout();
                _Rights = Rights;

                var AccessRight = Rights.InstantenousDataRights.Find((x) => x.QuantityType == typeof(eInstantaneousData)
                                                    && (x.Read || x.Write));

                if (AccessRight != null && (AccessRight.Read == true || AccessRight.Write == true))
                {
                    ////For Default Behaviour
                    //if (!this.tcontrolMain.TabPages.Contains(INs))
                    //    this.tcontrolMain.TabPages.Add(INs);

                    foreach (var item in Rights.InstantenousDataRights)
                    {
                        _HelperAccessRights((eInstantaneousData)Enum.Parse(item.QuantityType,
                            item.QuantityName), item.Read, item.Write);
                    }

                    if( this.tcontrolMain.TabPages.Count > 0 )
                    {
                        this.tcontrolMain.SelectedIndex = 0;
                        this.btn_read_all_instantaneous_values.Enabled = true;
                        this.tcontrolMain.Visible = true;

                        //Rights implemented
                        ////Access Rights for Contactor Implement Later...
                        //if (!this.tcontrolMain.TabPages.Contains(this.tab_Contactor))
                        //    this.tcontrolMain.TabPages.Add(this.tab_Contactor);
                    }
                    else
                    {
                        this.tcontrolMain.Visible = false;
                    }
                    isSuccess = true;
                }
                else
                    return false;


            }
            catch(Exception ex)
            { }
            finally
            {
                this.ResumeLayout();
                this.tcontrolMain.ResumeLayout();
                HidePrintReportButtons = false;
                if (Rights.GeneralRights.Find(x => x.QuantityName == GeneralRights.IgnoreReports.ToString() && x.Read) != null)
                {
                    HidePrintReportButtons = true;
                    this.btn_InsOldReport.Visible = false;
                    this.btn_INS_Report.Visible = false;
                }
                else if(tcontrolMain.TabPages.Contains(INs) || tcontrolMain.TabPages.Contains(tab_MDI))
                {
                    this.btn_InsOldReport.Visible = true;
                    this.btn_INS_Report.Visible = true;
                }
            }
            return isSuccess;
        }

        private void _HelperAccessRights(eInstantaneousData qty, bool read, bool write)
        {
            if (qty == eInstantaneousData.TabInstantaneous)
            {
                if (read)
                {
                    if (!this.tcontrolMain.TabPages.Contains(INs))
                        this.tcontrolMain.TabPages.Add(INs);
                }
                else
                    this.tcontrolMain.TabPages.Remove(INs);
            }

            if (qty == eInstantaneousData.Voltage)
            {
                check_Voltage.Enabled = check_Voltage.Visible = read;
                if (!read) check_Voltage.Checked = false;

                //if ((read || write) && !this.tcontrolMain.TabPages.Contains(INs))
                //    this.tcontrolMain.TabPages.Add(INs);

            }
            else if (qty == eInstantaneousData.Current)
            {
                check_Current.Enabled = check_Current.Visible = read;
                if (!read) check_Current.Checked = false;

                //if ((read || write) && !this.tcontrolMain.TabPages.Contains(INs))
                //    this.tcontrolMain.TabPages.Add(INs);
            }
            else if (qty == eInstantaneousData.ActivePower ||
                     qty == eInstantaneousData.ActivePower_Abs ||
                     qty == eInstantaneousData.ActivePower_Pos ||
                     qty == eInstantaneousData.ActivePower_Neg)
            {
                check_ActivePower.Enabled = check_ActivePower.Visible = read;
                if (!read) check_ActivePower.Checked = false;

                //if ((read || write) && !this.tcontrolMain.TabPages.Contains(INs))
                //    this.tcontrolMain.TabPages.Add(INs);
            }
            else if (qty == eInstantaneousData.ApparentPower)
            {
                check_Apparent.Enabled = check_Apparent.Visible = read;
                if (!read) check_Apparent.Checked = false;

                //if ((read || write) && !this.tcontrolMain.TabPages.Contains(INs))
                //    this.tcontrolMain.TabPages.Add(INs);
            }
            else if (qty == eInstantaneousData.ReactivePower ||
                     qty == eInstantaneousData.ReactivePower_Abs ||
                     qty == eInstantaneousData.ReactivePower_Pos ||
                     qty == eInstantaneousData.ReactivePower_Neg)
            {
                check_ReactivePower.Enabled = check_ReactivePower.Visible = read;
                if (!read) check_ReactivePower.Checked = false;

                //if ((read || write) && !this.tcontrolMain.TabPages.Contains(INs))
                //    this.tcontrolMain.TabPages.Add(INs);
            }
            else if (qty == eInstantaneousData.PowerFactor)
            {
                check_Powerfactor.Enabled = check_Powerfactor.Visible = read;
                if (!read) check_Powerfactor.Checked = false;

                //if ((read || write) && !this.tcontrolMain.TabPages.Contains(INs))
                //    this.tcontrolMain.TabPages.Add(INs);
            }
            //Added by Azeem Inayat
            else if (qty == eInstantaneousData.PowerQuadrant)
            {
                check_PowerQuadrent.Enabled = check_PowerQuadrent.Visible = read;
                if (!read) check_PowerQuadrent.Checked = false;

                //if ((read || write) && !this.tcontrolMain.TabPages.Contains(INs))
                //    this.tcontrolMain.TabPages.Add(INs);
            }
            else if (qty == eInstantaneousData.Misc)
            {
                check_misc.Enabled = check_misc.Visible = read;
                if (!read) check_misc.Checked = false;

                //if ((read || write) && !this.tcontrolMain.TabPages.Contains(INs))
                //    this.tcontrolMain.TabPages.Add(INs);
            }
            else if (qty == eInstantaneousData.MDI)
            {
                check_Mdi.Enabled = check_Mdi.Visible = read;
                if (!read) check_Mdi.Checked = false;

                if ((read || write) && !this.tcontrolMain.TabPages.Contains(tab_MDI))
                {
                    this.tcontrolMain.TabPages.Add(tab_MDI);
                    if (!this.tcontrolMain.TabPages.Contains(INs))
                        this.tcontrolMain.TabPages.Add(INs);
                }
            }
            else if (qty == eInstantaneousData.MDI_Interval)
            {
                check_readMDI_Interval.Enabled = check_readMDI_Interval.Visible = read;
                if (!read) check_readMDI_Interval.Checked = false;

                if ((read || write) && !this.tcontrolMain.TabPages.Contains(tab_MDI))
                {
                    this.tcontrolMain.TabPages.Add(tab_MDI);
                    if (!this.tcontrolMain.TabPages.Contains(INs))
                        this.tcontrolMain.TabPages.Add(INs);
                }
            }
            else if (qty == eInstantaneousData.NewInstantaneousData)
            {
                if ((read || write) && !this.tcontrolMain.TabPages.Contains(tab_NewIns))
                    this.tcontrolMain.TabPages.Add(tab_NewIns);
            }
            else if (qty == eInstantaneousData.InstantaneousRecord)
            {
                if ((read || write) && !this.tcontrolMain.TabPages.Contains(tab_Record))
                    this.tcontrolMain.TabPages.Add(tab_Record);
            }
            else if (qty == eInstantaneousData.CurrentDisplayWindows)
            {
                if ((read || write) && !this.tcontrolMain.TabPages.Contains(tab_CrDwData))
                    this.tcontrolMain.TabPages.Add(tab_CrDwData);
                else
                    this.tcontrolMain.TabPages.Remove(tab_CrDwData);
            }
            else if (qty == eInstantaneousData.Contactor)
            {
                if ((read || write) && !this.tcontrolMain.TabPages.Contains(tab_Contactor))
                    this.tcontrolMain.TabPages.Add(tab_Contactor);
                else
                    this.tcontrolMain.TabPages.Remove(tab_Contactor);
            }

            ///Make btns Visible
            btn_read_all_instantaneous_values.Visible = btn_InsOldReport.Visible = this.tcontrolMain.TabPages.Contains(INs);
        }

        public AccessRights GetAccessRights(eInstantaneousData qty)
        {
            AccessRights ac = new AccessRights() { Read = false, Write = false };
            try
            {
                ac = _Rights.InstantenousDataRights.Find(delegate (AccessRights ar)
                {
                    if (ar != null && qty.ToString() == ar.QuantityName &&
                        ar.QuantityType == typeof(eInstantaneousData))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
            catch (Exception)
            { }
            if (ac == null) ac = new AccessRights() { Read = false, Write = false };
            return ac;
        }

        #endregion

        private void tcontrolMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcontrolMain.SelectedTab != null)
            {
                if (tcontrolMain.SelectedTab.Equals(INs) || tcontrolMain.SelectedTab.Equals(tab_MDI))
                {
                    this.btn_read_all_instantaneous_values.Visible = true;
                    if (!HidePrintReportButtons)
                    {
                        this.btn_InsOldReport.Visible = true;
                    }
                }
                else
                {
                    this.btn_read_all_instantaneous_values.Visible = this.btn_InsOldReport.Visible = false;
                }
            }
        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chk_All.Checked)
                {
                    for (int i = 0; i < chkLstMDIs.Items.Count; i++)
                    {
                        chkLstMDIs.SetItemChecked(i, true);
                    }
                }
                else
                {
                    for (int i = 0; i < chkLstMDIs.Items.Count; i++)
                    {
                        chkLstMDIs.SetItemChecked(i, false);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
