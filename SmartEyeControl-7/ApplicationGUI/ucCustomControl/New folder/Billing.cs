#define ReadCurrentMonthMDIKW

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using comm;
using DatabaseControl;
using DLMS;
using SmartEyeControl_7.comm;
using SmartEyeControl_7.Controllers;
using GUI;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SmartEyeControl_7.Reporting;
using SmartEyeControl_7.Common;
using SmartDebugUtility.Common;
using System.Text;
using Comm;
using SmartEyeControl_7.DataBase;
using AccurateOptocomSoftware.Reporting;


namespace SmartEyeControl_7.ApplicationGUI.GUI
{
    public partial class PnlBilling : UserControl
    {
        /// System.Drawing.Image backImage = System.Drawing.Image.FromFile(Environment.CurrentDirectory + @"\images\backImage.jpg");
        int temp = 0;
        List<BillingData> BillingData;
        List<BillingData> MonthlyBillingData;
        BillingData       CumulativeBillingData;
        private DBConnect MyDataBase = new DBConnect();
        private bool CumulativeBillingSelected;
        //================================================================
        //================================================================
        DLMS_Application_Process Application_Process;
        private SmartEyeControl_7.Controllers.ApplicationController application_Controller;
        private bool GetCompleted = false;
        Param_Customer_Code obj_CustomerCode;
        Instantaneous_Class Instantaneous_Class_obj;
        int maxCounter = -1;


        double deltaMDIkw_T1 = 0;
        double deltaMDIkw_T2 = 0;
        double deltaMDIkw_T3 = 0;
        double deltaMDIkw_T4 = 0;
        double deltaMDIkw_TL = 0;

        double GUI_deltaMDIkw_T1 = 0;
        double GUI_deltaMDIkw_T2 = 0;
        double GUI_deltaMDIkw_T3 = 0;
        double GUI_deltaMDIkw_T4 = 0;
        double GUI_deltaMDIkw_TL = 0;

        //const int billingDP = 3; //Commented by Azeem Inayat
        int billingDP = 1; //Added by Azeem Inayat
        public int MaxCounter
        {
            get
            {
                try
                {
                    return MyDataBase.get_Counter("MB", BillingController.CurrentConnectionInfo.MSN);
                }
                catch (Exception)
                {

                    return -1;
                }
            }
            set { maxCounter = value; }
        }

        public SmartEyeControl_7.Controllers.ApplicationController Application_Controller
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


        private Dictionary<string, int> D_Billing = new Dictionary<string, int>();

        List<DBConnect.Insert_Record> List_Records = new List<DBConnect.Insert_Record>();
        Dictionary<string, int> BillingItems = new Dictionary<string, int>();
        //dataset for reporting
        ds_Billing DataSet_Billing = new ds_Billing();
        ds_Monthly_Billing DataSet_Monthly_Billing = new ds_Monthly_Billing();

        DataBaseController dbController = new DataBaseController();

        #region Billing BckWorker
        private Thread billingReadBckThread;
        private Exception ex;
        #endregion

        private BillingController BillingController;

        public PnlBilling()
        {
            InitializeComponent();

            ///Initialze Billing Objects
            BillingData = new List<BillingData>();
            MonthlyBillingData = new List<BillingData>();
            CumulativeBillingData = null;
            btn_BillingReport.Visible = false;
            btn_comm_verification_rpt.Visible = false;
            Instantaneous_Class_obj = new Instantaneous_Class();
            obj_CustomerCode = new Param_Customer_Code();
        }

        private bool Store_Monthly_Billing_to_Disk(string Dir, Monthly_Billing Monthly_Billing_obj, int Tarrif_number)
        {
            try
            {
                DateTime date_time = DateTime.Now;
                string time = date_time.Year.ToString() + "_" + date_time.Month.ToString() + "_" + date_time.Day.ToString() + "_" + date_time.Hour.ToString() + "_" + date_time.Minute.ToString() + "_" + date_time.Second.ToString();
                Dir = Dir + "Monthly Billing\\" + time + "\\";
                Directory.CreateDirectory(Dir);
                // Path for saving test files.
                string FILE = "T" + Tarrif_number.ToString() + ".xml";     // For saving the items.
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(Monthly_Billing_obj.GetType());
                FileStream new_File = new FileStream(Dir + FILE, FileMode.Create);
                x.Serialize(new_File, Monthly_Billing_obj);
                new_File.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private bool Store_Cumulative_Billing_to_Disk(string Dir, Cumulative_Billing Cumulative_Billing_obj, int Tarrif_number)
        {
            try
            {
                DateTime date_time = DateTime.Now;
                string time = date_time.Year.ToString() + "_" + date_time.Month.ToString() + "_" + date_time.Day.ToString() + "_" + date_time.Hour.ToString() + "_" + date_time.Minute.ToString() + "_" + date_time.Second.ToString();
                Dir = Dir + "Cumulative Billing\\" + time + "\\";
                Directory.CreateDirectory(Dir);
                // Path for saving test files.
                string FILE = "T" + Tarrif_number.ToString() + ".xml";     // For saving the items.
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(Cumulative_Billing_obj.GetType());
                FileStream new_File = new FileStream(Dir + FILE, FileMode.Create);
                x.Serialize(new_File, Cumulative_Billing_obj);
                new_File.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void Re_initialize_classes()
        {
            Monthly_Billing Monthly_Billing_T1_obj = new Monthly_Billing();
            Monthly_Billing Monthly_Billing_T2_obj = new Monthly_Billing();
            Monthly_Billing Monthly_Billing_T3_obj = new Monthly_Billing();
            Monthly_Billing Monthly_Billing_T4_obj = new Monthly_Billing();
            Monthly_Billing Monthly_Billing_Tot_obj = new Monthly_Billing();
            //================================================================
            //================================================================
            Cumulative_Billing Cumulative_Billing_T1_obj = new Cumulative_Billing();
            Cumulative_Billing Cumulative_Billing_T2_obj = new Cumulative_Billing();
            Cumulative_Billing Cumulative_Billing_T3_obj = new Cumulative_Billing();
            Cumulative_Billing Cumulative_Billing_T4_obj = new Cumulative_Billing();
            Cumulative_Billing Cumulative_Billing_Tot_obj = new Cumulative_Billing();
        }

        private void UpdateBilling()
        {
            //if (rdbCumulativeBilling.Checked)
            //{
            //    CumulativeBillingSelected = true;

            //}
            //else
            //    CumulativeBillingSelected = false;
            ///Update Billing Interface
            BillingData.Clear();
            if (CumulativeBillingSelected)
            {
                lblHeading.Text = "      Cumulative Read Data";
                gpMonthlyBillingFilter.Visible = false;
                if (CumulativeBillingData != null)
                {
                    BillingData.Add(CumulativeBillingData);
                }
                if (application_Controller.isSinglePhase)
                {
                    displayBilling(0, true);
                }
                else
                {
                    displayBilling(0);
                }
            }
            else
            {
                lblHeading.Text = "      Monthly Read Data";
                gpMonthlyBillingFilter.Visible = true;
                ///Mark All Last 24 Months 
                //check_readBilling.Checked = true;
                //combo_billingSelective.SelectedItem = "24";
                ///**********
                if (MonthlyBillingData != null && MonthlyBillingData.Count > 0)
                {
                    BillingData.AddRange(MonthlyBillingData);
                }
                if (application_Controller.isSinglePhase)
                {
                    displayBilling(temp, true);
                }
                else
                {
                    if (application_Controller.isSinglePhase)
                    {
                        displayBilling(temp, true);
                    }
                    else
                    {
                        displayBilling(temp);
                    }
                }
            }


        }

        private void btn_NextMonth_Click(object sender, EventArgs e)
        {

            if (temp >= 0 && temp < BillingData.Count - 1)
            {
                temp++;
                if (application_Controller.isSinglePhase)
                {
                    displayBilling(temp, true);
                }
                else
                {
                    displayBilling(temp);
                }
            }

        }
        private void btn_PreviousMonth_Click(object sender, EventArgs e)
        {

            if (temp > 0 && temp <= BillingData.Count)
            {
                temp--;
                if (application_Controller.isSinglePhase)
                {
                    displayBilling(temp, true);
                }
                else
                {
                    displayBilling(temp);
                }
            }

        }
        private void btn_lastRecord_Click(object sender, EventArgs e)
        {
            temp = BillingData.Count - 1;
            if (application_Controller.isSinglePhase)
            {
                displayBilling(temp, true);
            }
            else
            {
                displayBilling(temp);
            }
        }
        private void btn_firstRecord_Click(object sender, EventArgs e)
        {
            temp = 0;
            if (application_Controller.isSinglePhase)
            {
                displayBilling(temp, true);
            }
            else
            {
                displayBilling(temp);
            }

        }

        private void GUI_CurrentBilling()
        {
            lbl_month.Visible = true;
            label22.Visible = true;
            label24.Visible = false;
            lbl_billingCount.Visible = false;
            btn_firstRecord.Visible = false;
            btn_lastRecord.Visible = false;
            btn_NextMonth.Visible = false;
            btn_PreviousMonth.Visible = false;
            panel1.Visible = false;
        }
        private void GUI_MonthlyBilling()
        {
            lbl_month.Visible = true;
            label22.Visible = true;
            label24.Visible = true;
            lbl_billingCount.Visible = true;
            btn_firstRecord.Visible = true;
            btn_lastRecord.Visible = true;
            btn_NextMonth.Visible = true;
            btn_PreviousMonth.Visible = true;
            panel1.Visible = true;
        }

        public void displayBilling(int index)
        {
            System.Drawing.Size Grid_Size = grid_billing.Size;
            try
            {
                ((System.ComponentModel.ISupportInitialize)(grid_billing)).BeginInit();

                //Uncommented By Azeem Inayat //v10.0.9
                grid_billing.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                grid_billing.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                grid_billing.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                grid_billing.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                grid_billing.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

                ClearBillingGrid();

                if (index < 0 || index >= BillingData.Count)
                {
                    return;
                }

                if (BillingData != null && BillingData.Count > 0)
                {
                    var BillPeriodData = BillingData[index];// we decide which month's data to display!
                    lbl_TotalPages.Text = BillingData.Count.ToString(); //keeps the count of total months data received
                    lbl_billingCount.Text = BillingData[index].BillingCounter.ToString("000");
                    if (BillPeriodData.TimeStamp != DateTime.MinValue)
                        lbl_month.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", BillPeriodData.TimeStamp);
                    else
                        lbl_month.Text = "-----";
                    lbl_CurrentPage.Text = (index + 1).ToString();

                    int rows_count = 0;
                    string headerTitle = "";
                    String value_T1 = "", value_T2 = "", value_T3 = "", value_T4 = "", value_TL = ""; //By Azeem
                    
                    if (CumulativeBillingSelected)
                    {
                        value_T1 = value_T2 = value_T3 = value_T4 = value_TL = "0.0";
                        billingDP = 1;
                    }
                    else //if monthly
                    {
                        value_T1 = value_T2 = value_T3 = value_T4 = value_TL = "0.0";
                        billingDP = 1;
                    }

                    DataSet_Billing.DataTable_Billing.Clear();
                    grid_billing.Rows.Clear();
                    foreach (var item in BillPeriodData.BillingItems)
                    {
                        grid_billing.Rows.Add();
                        bool dtTimeCaptures = false;

                        #region IsDateTimeDefined
                        ///DateTime Values Exists
                        if (item.Value.CaptureTimeStamp == null || item.Value.CaptureTimeStamp.Count <= 5 ||
                          (item.Value.CaptureTimeStamp[Tariff.T1_Index] == null ||
                          item.Value.CaptureTimeStamp[Tariff.T1_Index].Equals(DateTime.MinValue)) ||
                          (item.Value.CaptureTimeStamp[Tariff.T2_Index] == null ||
                          item.Value.CaptureTimeStamp[Tariff.T2_Index].Equals(DateTime.MinValue)) ||
                          (item.Value.CaptureTimeStamp[Tariff.T3_Index] == null ||
                          item.Value.CaptureTimeStamp[Tariff.T3_Index].Equals(DateTime.MinValue)) ||
                          (item.Value.CaptureTimeStamp[Tariff.T4_Index] == null ||
                          item.Value.CaptureTimeStamp[Tariff.T4_Index].Equals(DateTime.MinValue)) ||
                          (item.Value.CaptureTimeStamp[Tariff.TL_Index] == null ||
                          item.Value.CaptureTimeStamp[Tariff.TL_Index].Equals(DateTime.MinValue)))
                        {
                            dtTimeCaptures = false;
                        }
                        else
                            dtTimeCaptures = true;
                        #endregion
                        #region Define Row Header
                        if (dtTimeCaptures)
                            headerTitle = String.Format("{0} {1}\r{2}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString(), "DateTime");
                        else
                            headerTitle = String.Format("{0} {1}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString());

                        grid_billing.Rows[rows_count].HeaderCell.Value = headerTitle;
                        #endregion
                        #region Copy Data & Capture Time Values
                        //Commented by Azeem
                        //String value_T1 = App_Common.value_to_string(Convert_to_Valid(item.Value.T1), true);   //T1
                        //String value_T2 = App_Common.value_to_string(Convert_to_Valid(item.Value.T2), true);   //T2
                        //String value_T3 = App_Common.value_to_string(Convert_to_Valid(item.Value.T3), true);   //T3
                        //String value_T4 = App_Common.value_to_string(Convert_to_Valid(item.Value.T4), true);   //T4
                        //String value_TL = App_Common.value_to_string(Convert_to_Valid(item.Value.TL), true);   //TL
                        
                        //If RoundingOff not Required then call following
                        //value_T1 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T1).ToString(), billingDP);
                        //For RoundingOff use
                        //value_T1 = App_Common.value_to_string(Convert_to_Valid(item.Value.T1), "f0");
                        value_T1 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T1).ToString(), billingDP);   //T1
                        value_T2 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T2).ToString(), billingDP);   //T2
                        value_T3 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T3).ToString(), billingDP);   //T3
                        value_T4 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T4).ToString(), billingDP);   //T4
                        value_TL = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.TL).ToString(), billingDP);   //TL

                        DateTime dt_1 = DateTime.MinValue, dt_2 = DateTime.MinValue, dt_3 = DateTime.MinValue, dt_4 = DateTime.MinValue, dt_tL = DateTime.MinValue;

                        if (dtTimeCaptures)
                        {

                            dt_1 = (item.Value.CaptureTimeStamp[Tariff.T1_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T1_Index] : DateTime.MinValue;    //T1
                            dt_2 = (item.Value.CaptureTimeStamp[Tariff.T2_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T2_Index] : DateTime.MinValue;    //T2
                            dt_3 = (item.Value.CaptureTimeStamp[Tariff.T3_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T3_Index] : DateTime.MinValue;    //T3
                            dt_4 = (item.Value.CaptureTimeStamp[Tariff.T4_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T4_Index] : DateTime.MinValue;    //T4
                            dt_tL = (item.Value.CaptureTimeStamp[Tariff.TL_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.TL_Index] : DateTime.MinValue;   //TL 
                        }
                        #endregion
                         //=="Reactive Energy Total"
                        if (true)
                        {
                            ///Date Time Value Defined
                            #region Tariff_1 Quantity
                            if (!dt_1.Equals(DateTime.MinValue))
                                grid_billing[0, rows_count].Value = String.Format("{0}\r{1:dd/MM/yyyy HH:mm:ss}", value_T1, dt_1);
                            else
                                grid_billing[0, rows_count].Value = value_T1;
                            #endregion
                            #region Tariff_2 Quantity
                            if (!dt_2.Equals(DateTime.MinValue))
                                grid_billing[1, rows_count].Value = String.Format("{0}\r{1:dd/MM/yyyy HH:mm:ss}", value_T2, dt_2);
                            else
                                grid_billing[1, rows_count].Value = value_T2;
                            #endregion
                            #region Tariff_3 Quantity
                            if (!dt_3.Equals(DateTime.MinValue))
                                grid_billing[2, rows_count].Value = String.Format("{0}\r{1:dd/MM/yyyy HH:mm:ss}", value_T3, dt_3);
                            else
                                grid_billing[2, rows_count].Value = value_T3;
                            #endregion
                            #region Tariff_4 Quantity
                            if (!dt_4.Equals(DateTime.MinValue))
                                grid_billing[3, rows_count].Value = String.Format("{0}\r{1:dd/MM/yyyy HH:mm:ss}", value_T4, dt_4);
                            else
                                grid_billing[3, rows_count].Value = value_T4;
                            #endregion
                            #region Tariff_tL Quantity
                            if (!dt_tL.Equals(DateTime.MinValue))
                                grid_billing[4, rows_count].Value = String.Format("{0}\r{1:dd/MM/yyyy HH:mm:ss}", value_TL, dt_tL);
                            else
                                grid_billing[4, rows_count].Value = value_TL;
                            #endregion 
                        }

                        //for reporting
                        try
                        {
                           //DataSet_Billing.DataTable_Billing.Rows.Add();

                           //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = headerTitle;
                           //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T1).ToString(), billingDP);  //T1
                           //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T2).ToString(), billingDP); //T2
                           //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T3).ToString(), billingDP); //T3
                           //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T4).ToString(), billingDP); //T4
                           //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.TL).ToString(), billingDP); //TL
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                        rows_count++;
                        ////lbl_status.Text = " Reading Billing Data...";
                    }


                    try
                    {
                        if (CumulativeBillingSelected)
                        {

#if ReadCurrentMonthMDIKW

                            grid_billing.Rows.Add();
                            grid_billing.Rows[grid_billing.Rows.Count - 1].HeaderCell.Value = "Current Month MDI Kw";

                            grid_billing[0, grid_billing.Rows.Count - 1].Value = Common.App_Common.
                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw.ToString(), 3);  //T1

                            grid_billing[1, grid_billing.Rows.Count - 1].Value = Common.App_Common.
                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKw.ToString(), 3); //T2

                            grid_billing[2, grid_billing.Rows.Count - 1].Value = Common.App_Common.
                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKw.ToString(), 3); //T3

                            grid_billing[3, grid_billing.Rows.Count - 1].Value = Common.App_Common.
                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKw.ToString(), 3); //T4

                            grid_billing[4, grid_billing.Rows.Count - 1].Value = Common.App_Common.
                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKw.ToString(), 3); //TL

                            grid_billing.Rows.Add();
                            string DT_Format = "{0:dd/MM/yyyy HH:mm:ss}"; //Added by Azeem //v10.0.9
                            grid_billing.Rows[grid_billing.Rows.Count - 1].HeaderCell.Value = "Current Max kW DateTime";

                            grid_billing[0, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format,
                                Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

                            grid_billing[1, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format,
                                Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

                            grid_billing[2, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format,
                                Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

                            grid_billing[3, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format,
                                Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

                            grid_billing[4, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format,
                                Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
#endif
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    #region commented by Azeem
                    //add it to dataset for reporting and to list for storing records to database
                    //DataSet_Billing.DataTable_Billing.Rows.Add();
                    //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = headerTitle;
                    //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = 
                    //        Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T1).ToString("f1");  //T1
                    
                    //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = 
                    //    Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T2).ToString("f1"); //T2

                    //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = 
                    //    Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T3).ToString("f1"); //T3

                    //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = 
                    //    Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T4).ToString("f1"); //T4

                    //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL =
                    //    Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.TL).ToString("f1"); //TL
                    #endregion
                    //BillPeriodData.BillingItems.RemoveAt(BillPeriodData.BillingItems.Count - 1);
                    //database list

                    //end addition to dataset and list for database
                    ////lbl_status.Text = " Reading Billing Data Complete...";
                }
                else
                {
                    Notification Notifier = new Notification("Error", "Billing Data not available");
                }
            }
            finally
            {
                //Force To Resize GridView
                Grid_Size = grid_billing.Size;
                grid_billing.Size = new System.Drawing.Size(0, 0);
                grid_billing.Size = Grid_Size;

                ((System.ComponentModel.ISupportInitialize)(grid_billing)).EndInit();
            }
        }

        public void displayBilling(int index, bool isSinglePhase)
        {
            System.Drawing.Size Grid_Size = grid_billing.Size;
            try
            {
                ((System.ComponentModel.ISupportInitialize)(grid_billing)).BeginInit();

                //grid_billing.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                ClearBillingGrid();

                if (index < 0 || index >= BillingData.Count)
                {
                    return;
                }

                if (BillingData != null && BillingData.Count > 0)
                {
                    var BillPeriodData = BillingData[index];// we decide which month's data to display!
                    lbl_TotalPages.Text = BillingData.Count.ToString(); //keeps the count of total months data received
                    lbl_billingCount.Text = BillingData[index].BillingCounter.ToString("000");
                    DateTime datetoshow = BillPeriodData.TimeStampRaw.GetDateTime();
                    lbl_month.Text = datetoshow.ToString(); ;
                    lbl_CurrentPage.Text = (index + 1).ToString();
                    int rows_count = 0;
                    string headerTitle = "";
                    DataSet_Billing.DataTable_Billing.Clear();

                    foreach (var item in BillPeriodData.BillingItems)
                    {
                        grid_billing.Rows.Add();
                        bool dtTimeCaptures = false;
                        if (item.Value.CaptureTimeStamp.Count > 0)
                        {
                            dtTimeCaptures = true;
                        }
                        else
                        {
                            dtTimeCaptures = false;
                        }

                        #region Define Row Header

                        if (dtTimeCaptures)
                            headerTitle = String.Format("{0} {1}\r :{2}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString(), "DateTime");
                        else
                            headerTitle = String.Format("{0} {1}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString());

                        grid_billing.Rows[rows_count].HeaderCell.Value = headerTitle;

                        #endregion

                        ///Date Time Value Defined
                        #region Tariff_tL Quantity

                        if (dtTimeCaptures)
                            grid_billing["Value", rows_count].Value = String.Format("{0} :   {1:dd/MM/yyyy HH:mm:ss}", item.Value.TL, item.Value.CaptureTimeStamp[0].ToString());
                        else
                            grid_billing["Value", rows_count].Value = item.Value.TL;

                        #endregion

                        //for reporting
                        DataSet_Billing.DataTable_Billing.Rows.Add();
                        
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = headerTitle;
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = Convert_to_Valid(item.Value.T1).ToString(item.Format);  //T1
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = Convert_to_Valid(item.Value.T2).ToString(item.Format); //T2
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = Convert_to_Valid(item.Value.T3).ToString(item.Format); //T3
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = Convert_to_Valid(item.Value.T4).ToString(item.Format); //T4
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = Convert_to_Valid(item.Value.TL).ToString(item.Format); //TL

                        rows_count++;
                        ////lbl_status.Text = " Reading Billing Data...";
                    }

                    //add it to dataset for reporting and to list for storing records to database
                    DataSet_Billing.DataTable_Billing.Rows.Add();
                    
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = headerTitle;
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T1).ToString("f1");  //T1

                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T2).ToString("f1"); //T2

                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T3).ToString("f1"); //T3

                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T4).ToString("f1"); //T4

                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.TL).ToString("f1"); //TL

                    //BillPeriodData.BillingItems.RemoveAt(BillPeriodData.BillingItems.Count - 1);
                    //database list

                    //end addition to dataset and list for database
                    ////lbl_status.Text = " Reading Billing Data Complete...";
                }
                else
                {
                    Notification Notifier = new Notification("Error", "Billing Data not available");
                }
            }
            finally
            {
                //Force To Resize GridView
                Grid_Size = grid_billing.Size;
                grid_billing.Size = new System.Drawing.Size(0, 0);
                grid_billing.Size = Grid_Size;

                ((System.ComponentModel.ISupportInitialize)(grid_billing)).EndInit();
            }
        }

        private void ClearBillingGrid()
        {
            //for (int i = grid_billing.Rows.Count - 1; i >= 0; i--)
            //{
            //    grid_billing.Rows.RemoveAt(i);
            //}
            grid_billing.Rows.Clear();

            lbl_CurrentPage.Text = "";
            lbl_TotalPages.Text = "";
            lbl_month.Text = "";
            lbl_billingCount.Text = "";
        }

        private void ClearBillingData()
        {
            BillingData.Clear();

            if (true)//tabControl1.SelectedIndex == 0)
            {
                CumulativeBillingData = null;
            }
            else// if (tabControl1.SelectedIndex == 1)
            {
                MonthlyBillingData.Clear();
            }

            for (int i = grid_billing.Rows.Count - 1; i >= 0; i--)
            {
                grid_billing.Rows.RemoveAt(i);
            }

            lbl_CurrentPage.Text = "";
            lbl_TotalPages.Text = "";
            lbl_month.Text = "";
            lbl_billingCount.Text = "";
        }


        private double Convert_to_Valid(double arg)
        {
            if (arg < 0)
            {
                arg = (0x80000000 + arg) + 0x80000000;
            }
            return arg;
        }

        public void update_Progress_bar(ProgressBar _ProgressBar, int total_commands)
        {
            int min = _ProgressBar.Minimum;
            int max = _ProgressBar.Maximum;

            try
            {
                _ProgressBar.Value += ((max - min) / 100) * (100 / total_commands);
                Application.DoEvents();
            }
            catch
            {
                _ProgressBar.Value = 0;
            }
        }

        private void pnlBilling_Load(object sender, EventArgs e)
        {
            try
            {
                ///Init Billing Interface Work

                ///initializeBillingDictionary();
                if (Application_Controller != null)
                {
                    BillingController = Application_Controller.Billing_Controller;
                    Application_Process = Application_Controller.Applicationprocess_Controller.ApplicationProcess;
                    //Mark All Last 24 Months 
                    check_readBilling.Checked = true;
                    combo_billingSelective.SelectedIndex = 23;
                    UpdateBilling();
                }

                //this.panel2.BackgroundImage = backImage;


            }
            catch (Exception ex)
            {
                //throw new Exception("Error Initializing Billing Interface Properly",ex);
                //MessageBox.Show(ex.Message);
                Notification Notifier = new Notification("Error", "Error Initializing Billing Interface Properly");
            }
            finally
            {

            }
        }

        private void rdbCumulativeBilling_CheckedChanged(object sender, EventArgs e)
        {
            //UpdateBilling();
            //tabControl1.SelectedIndex = 0;
        }

        private void btn_clearBillingGrid_Click(object sender, EventArgs e)
        {
            ClearBillingData();
        }

        private void rdbMonthlyBilling_CheckedChanged(object sender, EventArgs e)
        {
            //UpdateBilling();
            //tabControl1.SelectedIndex = 1;
        }

        private void combo_billingSelective_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddFilterValue();
        }

        private void check_readBilling_CheckedChanged(object sender, EventArgs e)
        {
            AddFilterValue();
        }

        private void AddFilterValue()
        {
            if (combo_billingSelective.SelectedIndex != -1)
            {
                byte x = BillingController.MonthlyBilling_All;
                ///Read All Previous Month Data
                if (check_readBilling.Checked)
                    x = 124;
                else
                    x = Convert.ToByte(combo_billingSelective.SelectedIndex + 101);
                BillingController.MonthlyBillingFilter = x;
            }
        }

        #region Billing_Read_BckWorker

        private void btn_newBillingbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_newBillingbutton.Text == "Read Data")
                {
                    if (Application_Process.Is_Association_Developed)
                    {
                        //tabControl1.Enabled = true;
                        btn_newBillingbutton.Enabled = true;
                        int colCount = grid_billing.Columns.Count;
                        for (int i = 0; i < colCount; i++)
                        {
                            grid_billing.Columns.RemoveAt(0);
                        }
                        if (bckWorker_Billing.IsBusy)   ///already busy
                        {
                            DialogResult MsgResult = MessageBox.Show(this.Parent, "Billing read process already continue,abort current process?", "Process Abort", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            if (MsgResult == DialogResult.OK)
                            {
                                bckWorker_Billing.CancelAsync();
                                Thread.Sleep(5);
                            }
                            else
                            {
                                return;
                            }
                        }

                        ////lbl_status.Text = " Reading Billing Data...";
                        Cursor.Current = Cursors.WaitCursor;
                        bckWorker_Billing.RunWorkerAsync();
                        pb_billing.Enabled = true;
                        pb_billing.Visible = true;
                        Application_Controller.IsIOBusy = true;
                        ///SetReadWriteStatus(true);
                        ///String fileURL = String.Format(@"{0}\RawBillingData.xml" , Environment.CurrentDirectory);
                        //BillingData  = BillingController.ReadBillingData(fileURL);

                        ///BillInfo = BillingController.GetBillingInfo();

                        ///Request Billing Data Cumulative/Monthly Billing Data

                        //if (CumulativeBillingSelected)
                        //{
                        //    CumulativeBillingData = BillingController.GetCumulativeBillingData();
                        //}
                        //else
                        //{
                        //    MonthlyBillingData = BillingController.GetBillingData();
                        //}
                        //UpdateBilling();
                        //Cursor.Current = Cursors.Arrow;

                        //displayBilling(0);
                        //lbl_status.Text = " Reading Billing Data Complete";

                    }
                }
                else
                {
                    if (btn_newBillingbutton.Text == "Cancel")
                    {
                        //tabControl1.Enabled = true;
                        btn_newBillingbutton.Enabled = true;
                        bckWorker_Billing.CancelAsync();
                    }
                    else
                    {
                        ///MessageBox.Show("Create Association to Electrical via Management");
                        Notification Notifier = new Notification("Error", "Create Association to Meter");
                    }
                }
            }
            catch (Exception ex)
            {
                //tabControl1.Enabled = true;
                btn_newBillingbutton.Enabled = true;
                if (CumulativeBillingSelected)
                {
                    CumulativeBillingData = null;
                }
                else
                {
                    MonthlyBillingData = new List<BillingData>();
                }
                UpdateBilling();
                if (application_Controller.isSinglePhase)
                {
                    displayBilling(0, true);
                }
                else
                {
                    displayBilling(0);
                }

                ///MessageBox.Show(String.Format("Billing Data not available,{0}", ex.Message), "Billing Error", MessageBoxButtons.OK);
                Notification Notifier = new Notification("Error",
                    String.Format("Billing Data not available,\r\n{0}", ex.Message));
            }
            finally
            {

                if (bckWorker_Billing.IsBusy)
                {
                    btn_newBillingbutton.Text = "Cancel";
                }
                else
                {
                    btn_newBillingbutton.Text = "Read Data";
                }
            }
        }

        private void bckWorker_Billing_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.ex = null;
                this.billingReadBckThread = new Thread(bckWorker_Billing_DoWorkHelper);
                billingReadBckThread.Name = "Billing_Thread";
                billingReadBckThread.IsBackground = true;
                billingReadBckThread.Start();
                while (bckWorker_Billing.IsBusy && (billingReadBckThread.ThreadState != ThreadState.Aborted && billingReadBckThread.ThreadState != ThreadState.Stopped))
                {
                    if (bckWorker_Billing.CancellationPending) ///True
                    {
                        billingReadBckThread.Abort();       ///Cancel Reading Billing
                        pb_billing.Visible = false;
                        throw new Exception("Reading Billing process is aborted");
                    }
                    Thread.Sleep(1);
                    Application.DoEvents();
                }
                if ((billingReadBckThread.ThreadState == ThreadState.Aborted || billingReadBckThread.ThreadState == ThreadState.Stopped) && this.ex != null)
                    throw ex;
            }
            catch (Exception ex)
            {
                //throw ex;
                MessageBox.Show("Billing Data Not Available");
            }

        }

        private void bckWorker_Billing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //tabControl1.Enabled = true;
                btn_newBillingbutton.Enabled = true;
                if (application_Controller.isSinglePhase)
                {
                    grid_billing.Columns.Add("Value", "Value");
                    grid_billing.Size = new System.Drawing.Size(417, 332);
                    grid_billing.Columns["Value"].Width = grid_billing.Columns["Value"].Width + 100;
                }
                else
                {
                    grid_billing.Columns.Add("T1", "T1");
                    grid_billing.Columns.Add("T2", "T2");
                    grid_billing.Columns.Add("T3", "T3");
                    grid_billing.Columns.Add("T4", "T4");
                    grid_billing.Columns.Add("TL", "TL");

                    for (int i = 0; i < grid_billing.Columns.Count; i++)
                    {
                        grid_billing.Columns[i].Width += 20;
                    }
                    grid_billing.Size = new System.Drawing.Size(700, 332);

                }
                Notification Notifier;
                pb_billing.Visible = false;
                btn_BillingReport.Visible = true;
                if (true)//tabControl1.SelectedIndex == 0)
                {
                    //btn_comm_verification_rpt.Visible = true;
                    btn_comm_verification_rpt.Visible = false; //should be added in User Rights  //by M.Azeem Inayat
                }
                else
                {
                    btn_comm_verification_rpt.Visible = false;
                }
                if (e.Error == null)
                {
                    UpdateBilling();
                    Cursor.Current = Cursors.Arrow;

                    //**********************************
                    try
                    {
                        if (application_Controller.isSinglePhase)
                        {
                            displayBilling(0, true);
                            if (CumulativeBillingSelected)
                            {
                                cumulativeBilling_SinglePhase toSave_SP = BillingController.saveToClass_SinglePhase(BillingData[0], Application_Controller.ConnectionManager.ConnectionInfo.MSN);
                                dbController.saveCumulativeBillingData_SinglePhase(toSave_SP);
                            }
                            else
                            {
                                Monthly_Billing_data_SinglePhase monthlyData_SP = BillingController.saveToClass_SinglePhase(BillingData, Application_Controller.ConnectionManager.ConnectionInfo.MSN);
                                dbController.saveMonthlyBillingData_SinglePhase(monthlyData_SP);
                            }
                        }
                        else
                        {
                            displayBilling(0);
                            if (CumulativeBillingSelected)
                            {
                                Cumulative_billing_data toSave = BillingController.saveToClass(BillingData[0], Application_Controller.ConnectionManager.ConnectionInfo.MSN);
                                dbController.saveCumulativeBillingData(toSave);
                            }
                            else
                            {
                                Monthly_Billing_data toSave = BillingController.saveToClass(BillingData, Application_Controller.ConnectionManager.ConnectionInfo.MSN);
                                if (!dbController.saveMonthlyBillingData(toSave))
                                {
                                    Notification n = new Notification("Error", "Error saving Monthly Billing Data");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Error saving Billing data, Reason: " + ex);
                    }
                    //********************************

                    ////lbl_status.Text = "Reading Billing Data Complete";
                    if (GetCompleted)
                    {
                        Notifier = new Notification("Process Completed", "Reading Billing Data Complete");

                    }
                    else
                        Notifier = new Notification("Process Failed", " Error Reading Billing Data ");
                }
                else
                    throw e.Error;
            }
            catch (Exception ex)
            {
                if (CumulativeBillingSelected)
                {
                    CumulativeBillingData = null;
                }
                else
                {
                    MonthlyBillingData = new List<BillingData>();
                }
                UpdateBilling();
                if (application_Controller.isSinglePhase)
                {
                    displayBilling(0, true);
                }
                else
                {
                    displayBilling(0);
                }
                /// MessageBox.Show(this, String.Format("Billing Data not available,\r\n{0}\r\n{1}", (ex != null) ? ex.Message : "", (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""), "Billing Error", MessageBoxButtons.OK);
                Notification Notifier = new Notification("Error",
                    String.Format("Billing Data not available,\r\n{0}\r\n{1}",
                    (ex != null) ? ex.Message : "",
                    (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));
            }
            finally
            {
                if (bckWorker_Billing.IsBusy &&
                   (billingReadBckThread.ThreadState != ThreadState.Aborted &&
                    billingReadBckThread.ThreadState != ThreadState.Stopped))
                {
                    ///SetReadWriteStatus(true);
                    Application_Controller.IsIOBusy = true;
                    btn_newBillingbutton.Text = "Cancel";
                }
                else
                {
                    ///SetReadWriteStatus(false);
                    Application_Controller.IsIOBusy = false;
                    btn_newBillingbutton.Text = "Read Data";
                    pb_billing.Visible = false;
                }
            }

        }

        private void bckWorker_Billing_DoWorkHelper()
        {
            try
            {
                if (CumulativeBillingSelected)
                {
                    CumulativeBillingData = BillingController.GetCumulativeBillingData(application_Controller.isSinglePhase);
                    //  GetCompleted = true;

                }
                else
                {
                    MonthlyBillingData = BillingController.GetBillingData(application_Controller.isSinglePhase);

                    if (!Application_Controller.isSinglePhase)
                    {
                        BillingItem CurrentMDI;
                        BillingItem defaultBillingItem = MonthlyBillingData[0].BillingItems.Find(x => x.Name.Equals("Cumulative MDI"));
                        if (defaultBillingItem == null)
                            defaultBillingItem = MonthlyBillingData[0].BillingItems.Find(x => x.Name.Equals("Active MDI"));
                    
#if ReadCurrentMonthMDIKW

                        BillingItem firstItem = new BillingItem(defaultBillingItem);
                        firstItem.Name = "Month MDI ";
                        MonthlyBillingData[0].BillingItems.Add(firstItem);

                        for (int i = 1; i < MonthlyBillingData.Count; i++)
                        {
                            CurrentMDI = new BillingItem();
                            CurrentMDI.Name = " Month MDI (kW)";

                            BillingItem currentMonth = MonthlyBillingData[i].BillingItems.Find(x => x.Name.Equals("Active MDI"));
                            if (currentMonth == null)
                                currentMonth = MonthlyBillingData[i].BillingItems.Find(x => x.Name.Equals("Cumulative MDI"));
                            
                            BillingItem PreviousMonth = MonthlyBillingData[i - 1].BillingItems.Find(x => x.Name.Equals("Active MDI"));
                            if (PreviousMonth == null)
                                PreviousMonth = MonthlyBillingData[i - 1].BillingItems.Find(x => x.Name.Equals("Cumulative MDI"));

                            CurrentMDI.Value.T1 = currentMonth.Value.T1 - PreviousMonth.Value.T1;
                            CurrentMDI.Value.T2 = currentMonth.Value.T2 - PreviousMonth.Value.T2;
                            CurrentMDI.Value.T3 = currentMonth.Value.T3 - PreviousMonth.Value.T3;
                            CurrentMDI.Value.T4 = currentMonth.Value.T4 - PreviousMonth.Value.T4;
                            CurrentMDI.Value.TL = currentMonth.Value.TL - PreviousMonth.Value.TL;
                            
                            //Added by Azeem Inayat
                            

                            MonthlyBillingData[i].BillingItems.Add(CurrentMDI);

                        }

#endif
                    }
                    //MonthlyBillingData.Sort((x, y) => x.BillingCounter.CompareTo(y.BillingCounter)); //AHMED
                    //  GetCompleted = true;

                }

                if (!Application_Controller.isSinglePhase)
                {
                    //Get Customer Reference Code
                    Application_Controller.Param_Controller.GET_Customer_Reference(ref obj_CustomerCode);
                    //Get Active Season
                    Application_Controller.InstantaneousController.Get_Active_Season(Instantaneous_Class_obj);
                }
                if (CumulativeBillingSelected)
                {
#if ReadCurrentMonthMDIKW
                    
                    Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw = Application_Controller.Param_Controller.
                                                                        GET_Any(Get_Index.Cumulative_Tariff1_CurrentMonthMdiKw);

                    Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw = Application_Controller.Param_Controller.
                                                                        GET_Any(Get_Index.Cumulative_Tariff2_CurrentMonthMdiKw);

                    Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw = Application_Controller.Param_Controller.
                                                                        GET_Any(Get_Index.Cumulative_Tariff3_CurrentMonthMdiKw);

                    Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw = Application_Controller.Param_Controller.
                                                                        GET_Any(Get_Index.Cumulative_Tariff4_CurrentMonthMdiKw);

                    Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw = Application_Controller.Param_Controller.
                                                                        GET_Any(Get_Index.Cumulative_TariffTL_CurrentMonthMdiKw);

                    Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw = Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw.Value / 1000;
                    Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKw = Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw.Value / 1000;
                    Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKw = Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw.Value / 1000;
                    Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKw = Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw.Value / 1000;
                    Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKw = Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw.Value / 1000;
#endif
                }
                GetCompleted = true;

            }
            catch (Exception ex)
            {
                GetCompleted = false;
                this.ex = ex;
                Thread.CurrentThread.Abort();
            }
            finally
            {
                if (bckWorker_Billing.IsBusy &&
                    (billingReadBckThread.ThreadState != ThreadState.Aborted &&
                    billingReadBckThread.ThreadState != ThreadState.Stopped))
                {
                    btn_newBillingbutton.Text = "Cancel";
                }
                else
                {
                    btn_newBillingbutton.Text = "Read Data";
                    pb_billing.Visible = false;
                }
            }
        }

        #endregion

        public void Add_Billing_to_DB(List<BillingData> BillingData)
        {
            try
            {
                int quantityID = 0;
                List_Records.Clear();//clearing Record List
                string Manufacturing_ID = application_Controller.ConnectionManager.ConnectionInfo.MSN;

                foreach (var BillingPeriodData in BillingData)
                {
                    int i = 0;
                    foreach (var item in BillingPeriodData.BillingItems)
                    {
                        i = 0;
                        List<Get_Index> temp = new List<Get_Index>();
                        temp = item.ValueInfo;
                        int temporary = item.ValueInfo.Count;

                        long t_quantityID = Convert.ToInt64(temp[i++]);
                        if (t_quantityID != 0)
                        {
                            DB_Add_DataSet_toWrite(BillingPeriodData.TimeStamp, Manufacturing_ID, t_quantityID, item.Value.T1, item.Unit.ToString(), Convert.ToSByte(item.Multiplier), true);
                        }
                        t_quantityID = Convert.ToInt64(temp[i++]);


                        if (t_quantityID != 0)
                        {
                            DB_Add_DataSet_toWrite(BillingPeriodData.TimeStamp, Manufacturing_ID, t_quantityID, item.Value.T2, item.Unit.ToString(), Convert.ToSByte(item.Multiplier), true);
                        }

                        t_quantityID = Convert.ToInt64(temp[i++]);
                        if (t_quantityID != 0)
                        {
                            DB_Add_DataSet_toWrite(BillingPeriodData.TimeStamp, Manufacturing_ID, t_quantityID, item.Value.T3, item.Unit.ToString(), Convert.ToSByte(item.Multiplier), true);
                        }

                        t_quantityID = Convert.ToInt64(temp[i++]);
                        if (t_quantityID != 0)
                        {
                            DB_Add_DataSet_toWrite(BillingPeriodData.TimeStamp, Manufacturing_ID, t_quantityID, item.Value.T4, item.Unit.ToString(), Convert.ToSByte(item.Multiplier), true);
                        }
                        t_quantityID = Convert.ToInt64(temp[i++]);
                        if (t_quantityID != 0)
                        {
                            DB_Add_DataSet_toWrite(BillingPeriodData.TimeStamp, Manufacturing_ID, t_quantityID, item.Value.TL, item.Unit.ToString(), Convert.ToSByte(item.Multiplier), true);
                        }
                    }
                }
                //  DB_Add_DataSet_toWrite(BillingPeriodData.TimeStamp, Manufacturing_ID, quantityID, item.Value.TL, item.Unit.ToString(), Convert.ToSByte(item.Multiplier), true);
            }

            catch (Exception)
            {

                throw;
            }
        }

        public bool DB_Add_DataSet_toWrite(DateTime timestamp, string MeterSerialNo, long Quantity_ID, double Value, string Unit, sbyte Scalar, bool isNumber)
        {
            try
            {
                DBConnect.Insert_Record record = new DBConnect.Insert_Record();
                record.arrival_time = timestamp;
                record.cat_id = 'B';
                record.msn = MeterSerialNo;
                record.qty_id = Convert.ToInt64(Quantity_ID);
                record.scalar = Scalar;
                record.session_id = 1;
                record.unit = Unit;
                if (isNumber && Value != Double.PositiveInfinity && Value != Double.NegativeInfinity && !Double.IsNaN(Value))
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

        ////  public bool DB_Add_DataSet_toWrite(DateTime timestamp, string MeterSerialNo, uint Quantity_ID, string Value, string Unit, byte Scalar, bool isNumber)
        //  {
        //      try
        //      {
        //          DBConnect.Insert_Record record = new DBConnect.Insert_Record();

        //          record.arrival_time = timestamp;
        //          record.cat_id = 'I';
        //          record.msn = MeterSerialNo;
        //          record.qty_id = Quantity_ID;  //Voltage Phase A
        //          record.scalar = Scalar;
        //          record.session_id = 1;
        //          record.unit = Unit;
        //          record.value = (string)Value;
        //          List_Records.Add(record);
        //          return true;
        //      }
        //      catch (Exception ex)
        //      {
        //          return false;
        //      }
        //  }

        public int GET_QuantityID(string itemName)
        {
            //if (itemName.Equals("Cumulative_Tariff1_KwhExport"))
            //{
            if (BillingItems.ContainsKey(itemName))
            {

                return BillingItems[itemName];
            }
            else
            {
                return 0;
            }
            //}
            //return 0;
        }

        public void UpdateReadWriteStatus(bool IsReadWriteBusy)
        {
            try
            {
                ///Disable IO Read Write btns 
                if (IsReadWriteBusy && !bckWorker_Billing.IsBusy)
                {
                    btn_newBillingbutton.Enabled = false;
                }
                ///Enable Read Write btns
                else
                {
                    btn_newBillingbutton.Enabled = true;
                    ///Disable IO Controls & Others
                    if (!IsReadWriteBusy)
                    {
                        pb_billing.Visible = false;
                        pb_billing.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                btn_newBillingbutton.Enabled = true;
                pb_billing.Visible = false;
                pb_billing.Enabled = false;


            }
            finally
            {
                btn_newBillingbutton.Text = "Read Data";
            }
        }

        private void Save_Billing_Data()
        {
            List<DBConnect.Insert_Record> InsertLIST = new List<DBConnect.Insert_Record>();
            foreach (var item in List_Records)
            {
                if (item.msn != null)
                {
                    InsertLIST.Add(item);
                }
            }
            if (true)//tabControl1.SelectedTab.Text == "Cumulative")
            {
                if (MyDataBase.SaveToDataBase(InsertLIST, "CB", 0))
                {
                    Notification not = new Notification("Successfull Saving", "Data saved to database", 6000);
                    List_Records.Clear();
                }
                else
                {
                    Notification not = new Notification("Error Saving", "Data not saved to database", 6000);
                }
            }
            else //if (rdbMonthlyBilling.Checked)
            {
                int index_Billing = BillingData.Count;
                uint max_Counter = BillingData[index_Billing - 1].BillingCounter;
                //getting difference from last counter
                // InsertLIST = Billing_With_Counter_Difference(InsertLIST);
                if (InsertLIST.Count > 0)
                {
                    if (MyDataBase.SaveToDataBase(InsertLIST, "MB", max_Counter))
                    {
                        Notification not = new Notification("Successfull Saving", "Monthly Billing data saved to database", 6000);
                        List_Records.Clear();
                    }
                    else
                    {
                        Notification not = new Notification("Error Saving", "Monthly Billing data not saved to database", 6000);
                    }
                }
                else
                {
                    Notification notify = new Notification("Alert!", "Monthly Billing data upto dated");
                    List_Records.Clear();
                }
            }

        }
        private List<DBConnect.Insert_Record> Billing_With_Counter_Difference(List<DBConnect.Insert_Record> list)
        {
            List<DBConnect.Insert_Record> temp = new List<DBConnect.Insert_Record>();
            int lastCounter = -1;
            int j = 0;
            lastCounter = this.MaxCounter;
            for (int i = lastCounter * 60; i < list.Count; i++, j++)
            {
                temp[j] = list[i];
            }
            return temp;
        }

        public double Floor(double value, int precision)
        {
            value = (Math.Floor(10 * value)) / (Math.Pow(10, precision));
            return value;
        }
        
        public bool getReportData()
        {
            deltaMDIkw_T1 = 0;
            deltaMDIkw_T2 = 0;
            deltaMDIkw_T3 = 0;
            deltaMDIkw_T4 = 0;
            deltaMDIkw_TL = 0;

            DataSet_Monthly_Billing.DataTable_Monthly_Billing.Clear(); //monthly
            DataSet_Billing.DataTable_Billing.Clear(); //cumulative
            List<BillingItem> quantitiesToShow = new List<BillingItem>();
            if (BillingData.Count > 0)
            {
                Billing_ReportSelection billing_report_selection = new Billing_ReportSelection(BillingData[0]);
                //Commented by Azeem to hide selection form
                //if (billing_report_selection.ShowDialog() == DialogResult.OK)
                //{
                    if (billing_report_selection.Obj_ToShow.Count == 0)
                    {
                        return false;
                    }
                    quantitiesToShow = billing_report_selection.Obj_ToShow;
                //}
            }
            //Now we have a list "quantitiesToShow" that has the list of objects to be displayed in a report

            if (CumulativeBillingSelected)
            {
                billingDP = 1;
            }
            else
            {
                billingDP = 1;
            }
            for (int billingCount = 0; billingCount < BillingData.Count; billingCount++)
            {
                var BillPeriodData = BillingData[billingCount];// we decide which month's data to display!

                foreach (var item in BillPeriodData.BillingItems)
                {
                    BillingItem _item = (BillingItem)(item);

                    //if (_item.Name.Contains("Current Month MDI"))
                    //    continue;
                    //if (quantitiesToShow.Contains(_item))

                    //  if (quantitiesToShow.Find(x => x.Name == item.Name) != null)
                    {
                        string headerTitle = "";
                        if (item.Unit != Unit.UnitLess)
                        {
                            headerTitle = String.Format("{0} ( {1} )", item.Name, item.Unit);

                        }
                        else
                        {
                            headerTitle = String.Format("{0}", item.Name);
                        }

                        item.Format = "f1";
                        //for Billing report
                        if (true)//tabControl1.SelectedTab.Text == "Cumulative")
                        {
                            DataSet_Billing.DataTable_Billing.Rows.Add();
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = headerTitle;
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T1).ToString(), billingDP);  //T1
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T2).ToString(), billingDP); //T2
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T3).ToString(), billingDP); //T3
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T4).ToString(), billingDP); //T4
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.TL).ToString(), billingDP); //TL
                            //Rounding Off Values //Azeem Inayat
                            //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = App_Common.value_to_string(Convert_to_Valid(item.Value.T1), "f0");  //T1
                            //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = App_Common.value_to_string(Convert_to_Valid(item.Value.T2), "f0"); //T2
                            //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = App_Common.value_to_string(Convert_to_Valid(item.Value.T3), "f0"); //T3
                            //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = App_Common.value_to_string(Convert_to_Valid(item.Value.T4), "f0"); //T4
                            //DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = App_Common.value_to_string(Convert_to_Valid(item.Value.TL), "f0"); //TL
                        }
                        //for monthly billing report
                        else
                        {
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Add();

                            //if (item.Name.Equals("Cumulative MDI"))
                            //{

                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].Quantity = "Maximum Demand (Kw)";
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T1 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T1 - deltaMDIkw_T1).ToString(), billingDP);  //T1
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T2 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T2 - deltaMDIkw_T2).ToString(), billingDP); //T2
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T3 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T3 - deltaMDIkw_T3).ToString(), billingDP); //T3
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T4 = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.T4 - deltaMDIkw_T4).ToString(), billingDP); //T4
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].TL = Common.App_Common.notRoundingOff(Convert_to_Valid(item.Value.TL - deltaMDIkw_TL).ToString(), billingDP); //TL
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].DateTime = BillingData[billingCount].TimeStamp.ToString("dd/MM/yyyy HH:mm:ss");
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].MDI_Counter = BillingData[billingCount].BillingCounter.ToString("000"); ;


                            //    deltaMDIkw_T1 = (Convert_to_Valid(item.Value.T1) - deltaMDIkw_T1);
                            //    deltaMDIkw_T2 = (Convert_to_Valid(item.Value.T2) - deltaMDIkw_T2);
                            //    deltaMDIkw_T3 = (Convert_to_Valid(item.Value.T3) - deltaMDIkw_T3);
                            //    deltaMDIkw_T4 = (Convert_to_Valid(item.Value.T4) - deltaMDIkw_T4);
                            //    deltaMDIkw_TL = (Convert_to_Valid(item.Value.TL) - deltaMDIkw_TL);

                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Add();
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].Quantity = "Cumulative Demand (Kw)";
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T1 = Common.App_Common.notRoundingOff((Convert_to_Valid(item.Value.T1)).ToString(), billingDP);  //T1
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T2 = Common.App_Common.notRoundingOff((Convert_to_Valid(item.Value.T2)).ToString(), billingDP); //T2
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T3 = Common.App_Common.notRoundingOff((Convert_to_Valid(item.Value.T3)).ToString(), billingDP); //T3
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T4 = Common.App_Common.notRoundingOff((Convert_to_Valid(item.Value.T4)).ToString(), billingDP); //T4
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].TL = Common.App_Common.notRoundingOff((Convert_to_Valid(item.Value.TL)).ToString(), billingDP); //TL
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].DateTime = BillingData[billingCount].TimeStamp.ToString("dd/MM/yyyy HH:mm:ss");
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].MDI_Counter = BillingData[billingCount].BillingCounter.ToString("000"); ;

                            //}
                            //else
                            //{
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].Quantity = headerTitle;
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T1 = Floor(Convert_to_Valid(item.Value.T1), 1).ToString("f2");  //T1
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T2 = Floor(Convert_to_Valid(item.Value.T2), 1).ToString("f2"); //T2
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T3 = Floor(Convert_to_Valid(item.Value.T3), 1).ToString("f2"); //T3
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T4 = Floor(Convert_to_Valid(item.Value.T4), 1).ToString("f2"); //T4
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].TL = Floor(Convert_to_Valid(item.Value.TL), 1).ToString("f2"); //TL
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].DateTime = BillingData[billingCount].TimeStamp.ToString("dd/MM/yyyy HH:mm:ss");
                            //    DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].MDI_Counter = BillingData[billingCount].BillingCounter.ToString("000"); ;
                            
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].Quantity = headerTitle;
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T1 = Common.App_Common.notRoundingOff((Convert_to_Valid(item.Value.T1)).ToString(), billingDP);  //T1
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T2 = Common.App_Common.notRoundingOff((Convert_to_Valid(item.Value.T2)).ToString(), billingDP); //T2
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T3 = Common.App_Common.notRoundingOff((Convert_to_Valid(item.Value.T3)).ToString(), billingDP); //T3
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T4 = Common.App_Common.notRoundingOff((Convert_to_Valid(item.Value.T4)).ToString(), billingDP); //T4
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].TL = Common.App_Common.notRoundingOff((Convert_to_Valid(item.Value.TL)).ToString(), billingDP); //TL

                            //Rounding Off values //by Azeem Inayat
                            //DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T1 = App_Common.value_to_string((Convert_to_Valid(item.Value.T1)), "f2");  //T1
                            //DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T2 = App_Common.value_to_string((Convert_to_Valid(item.Value.T2)), "f2"); //T2
                            //DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T3 = App_Common.value_to_string((Convert_to_Valid(item.Value.T3)), "f2"); //T3
                            //DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].T4 = App_Common.value_to_string((Convert_to_Valid(item.Value.T4)), "f2"); //T4
                            //DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].TL = App_Common.value_to_string((Convert_to_Valid(item.Value.TL)), "f2"); //TL
                            
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].DateTime = BillingData[billingCount].TimeStamp.ToString("dd MMM, yyyy");
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1].MDI_Counter = BillingData[billingCount].BillingCounter.ToString("000"); ;

                            //}
                        }

                    }
                }

                if (true)//tabControl1.SelectedTab.Text == "Cumulative") //currentMonthMDIKW from instantaneous Packet
                {

#if ReadCurrentMonthMDIKW

                    DataSet_Billing.DataTable_Billing.Rows.Add();
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = "Current Month MDI Kw";
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw.ToString(), billingDP);  //T1
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKw.ToString(), billingDP); //T2
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKw.ToString(), billingDP); //T3
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKw.ToString(), billingDP); //T4
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKw.ToString(), billingDP); //TL


                    DataSet_Billing.DataTable_Billing.Rows.Add();
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = "Current Max kW DateTime";
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = String.Format("{0:dd/MM/yyyy HH:mm}", Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = String.Format("{0:dd/MM/yyyy HH:mm}", Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = String.Format("{0:dd/MM/yyyy HH:mm}", Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = String.Format("{0:dd/MM/yyyy HH:mm}", Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = String.Format("{0:dd/MM/yyyy HH:mm}", Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

#endif

                }

            }
            return true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (true)//tabControl1.SelectedTab.Text == "Cumulative")
            {
                //rdbCumulativeBilling.Checked = true;
                //rdbMonthlyBilling.Checked = false;
                GUI_CurrentBilling();
                //Commented by Azeem
                //if (grid_billing.Rows.Count > 1)
                //    btn_comm_verification_rpt.Visible = true;

            }
            else
            {
                //rdbCumulativeBilling.Checked = false;
                //rdbMonthlyBilling.Checked = true;
                btn_comm_verification_rpt.Visible = false;
                GUI_MonthlyBilling();

            }
            UpdateBilling();
        }

        private void btn_BillingReport_Click(object sender, EventArgs e)
        {

            if (!getReportData())
            {
                return;
            }

            int billCount = BillingData.Count;
            if (true)//tabControl1.SelectedTab.Text == "Cumulative")
            {
                string Title_Billing = "Cumulative Read Report";
                string DateTime_Billing = BillingData[0].TimeStamp.ToString("dd:MM:yyyy  HH:mm:ss");

                ReportViewer viewer_Billing = new ReportViewer(Title_Billing, DataSet_Billing, BillingController.CurrentConnectionInfo.MSN, Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.Model, DateTime_Billing, obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString());
                viewer_Billing.Show();
            }
            else //if (rdbMonthlyBilling.Checked)
            {
                string Title_Monthly_Billing = "Monthly Read Report";
                ReportViewer monthly_Billing = new ReportViewer(Title_Monthly_Billing, DataSet_Monthly_Billing, BillingController.CurrentConnectionInfo.MSN, Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.Model, obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString());
                monthly_Billing.Show();
            }
        }

        internal void Reset_State()
        {
            try
            {
                /////////////// Added by Azeem
                CumulativeBillingData = null;
                DataSet_Billing.DataTable_Billing.Rows.Clear();
                BillingData.Clear();
                MonthlyBillingData.Clear();
                DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Clear();
                //////////////
                ClearBillingGrid();
               
            }
            catch (Exception)
            {

            }
        }

        private void btn_comm_verification_rpt_Click(object sender, EventArgs e)
        {
            string kwh = "0.0", kvarh = "0.0";
            for (int dataCount = 0; dataCount < DataSet_Billing.DataTable_Billing.Count; dataCount++)
            {
                if (DataSet_Billing.DataTable_Billing[dataCount].Quantity.Equals("Active Energy Total kWh"))
                {//kwh = Convert.ToDouble(DataSet_Billing.DataTable_Billing[dataCount].TL).ToString("F2");
                    kwh = DataSet_Billing.DataTable_Billing[dataCount].TL;

                    if (kwh.Contains("."))
                    {
                        kwh = kwh.Substring(0, kwh.IndexOf(".") + 2);
                    }
                    else
                    {
                        kwh += ".0";
                    }
                }
                if (DataSet_Billing.DataTable_Billing[dataCount].Quantity.Equals("Reactive Energy Total kVarh"))
                { //kvarh = Convert.ToDouble(DataSet_Billing.DataTable_Billing[dataCount].TL).ToString("F2");
                    kvarh = DataSet_Billing.DataTable_Billing[dataCount].TL;
                    if (kvarh.Contains("."))
                    {
                        kvarh = kvarh.Substring(0, kvarh.IndexOf(".") + 2);
                    }
                    else
                    {
                        kvarh += ".0";
                    }
                }
            }

            ReportViewer rpt = new ReportViewer(kwh, kvarh, BillingController.CurrentConnectionInfo.MSN, BillingController.CurrentConnectionInfo.MeterInfo.Model);
            rpt.Show();
        }

    }
}
