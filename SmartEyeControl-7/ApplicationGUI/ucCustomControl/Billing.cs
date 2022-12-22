/*
 In Fusion: MDI & occur DT in Cumulative billing read sapartly from Instantaneous Class, While Decoded from packet in Monthly billing
 */

//#define Read_Display_CumMDI_CumBill_InstCls
#define Read_Display_CumMdiOccurDateTime_CumBill_InstCls
//#define Display_Mdi_FromPacket_CumBill   //NO NEED
//#define Display_MdiOccurDateTime_FromPacket_CumBill   //NO NEED

//#define Read_Display_MDI_Delta_Monthly_InstCls
//#define Read_Display_MdiOccurDateTime_Delta_Monthly_InstCls
#define Display_Mdi_FromPacket_MonthlyBill
#define Display_MdiOccurDateTime_FromPacket_MonthlyBill //NO NEED
//#define Read_Display_MDI_kvar_Delta_Monthly_InstCls

#define Display_MdiOccurDateTime_FromBillPacket

using AccurateOptocomSoftware.Common;
using DLMS;
using DLMS.Comm;
using OptocomSoftware.Reporting;
using SEAC.Common;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using SharedCode.Controllers;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SmartEyeControl_7.DB;
using SmartEyeControl_7.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SmartEyeControl_7.ApplicationGUI.GUI
{
    public partial class PnlBilling : UserControl
    {
        bool IsReadCum_currentMonthMdi = false;
        #region DataMembers
        bool isDisableRights = true;
        bool isDisableMonthlyDeltaMDI = true;
        //ApplicationRight _Rights = null;
        List<AccessRights> _Rights = null;
        bool HidePrintReportButtons;

        Cumulative_Billing Cumulative_Billing_T1_obj = new Cumulative_Billing();
        Cumulative_Billing Cumulative_Billing_T2_obj = new Cumulative_Billing();
        Cumulative_Billing Cumulative_Billing_T3_obj = new Cumulative_Billing();
        Cumulative_Billing Cumulative_Billing_T4_obj = new Cumulative_Billing();
        Cumulative_Billing Cumulative_Billing_Tot_obj = new Cumulative_Billing();

        bool IsReadingCumulativeBilling = true;
        //bool IsWapdaFormat = true;
        //bool IsWebFormat = false;
        int temp = 0;
        List<BillingData> BillingData;
        List<BillingData> CurrentBillingData;
        List<BillingData> MonthlyBillingData;
        List<BillingData> DailyBillingData;
        BillingData CumulativeBillingData;
        private DBConnect MyDataBase = new DBConnect();
        private bool CumulativeBillingSelected;
        bool MonthlyBillingSelected = false;
        bool DailyEnergyProfileSelected = false;

        DLMS_Application_Process Application_Process;
        private ApplicationController application_Controller;
        private bool GetCompleted = false;
        Param_Customer_Code obj_CustomerCode;
        Instantaneous_Class Instantaneous_Class_obj;
        StDateTime StDateTimeobj;

        int maxCounter = -1;

        int billingDP = 2; //Added by Azeem Inayat
        string dt_dash = "--/--/----";
        string DateTimeFormat = "{0:dd/MM/yyyy HH:mm}";
        DateTime minDateTime = DateTime.MinValue;

        private Dictionary<string, int> D_Billing = new Dictionary<string, int>();

        List<DBConnect.Insert_Record> List_Records = new List<DBConnect.Insert_Record>();
        Dictionary<string, int> BillingItems = new Dictionary<string, int>();
        //dataset for reporting
        ds_Billing DataSet_Billing = new ds_Billing();
        ds_Monthly_Billing DataSet_Monthly_Billing = new ds_Monthly_Billing();

        DataBaseController dbController = new DataBaseController();

        private Thread billingReadBckThread;
        private Exception ex;

        private bool _readDetail = false;

        #endregion

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

        private BillingController BillingController;
        private InstantaneousController instantController;

        public PnlBilling()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

#if  Read_Display_CumMDI_CumBill_InstCls
            IsReadCum_currentMonthMdi=true;
#endif
#if Read_Display_CumMdiOccurDateTime_CumBill_InstCls
            IsReadCum_currentMonthMdi = true;
#endif

            ///Initialze Billing Objects
            BillingData = new List<BillingData>();
            MonthlyBillingData = new List<BillingData>();
            CumulativeBillingData = null;
            btn_BillingReport.Visible = false;
            btn_comm_verification_rpt.Visible = false;
            Instantaneous_Class_obj = new Instantaneous_Class();
            obj_CustomerCode = new Param_Customer_Code();
            StDateTimeobj = new StDateTime();
        }

        #region Not_Used_Store_Billing_to_Disk
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

        #endregion

        private void UpdateBilling()
        {
            if (rdbCumulativeBilling.Checked)
            {
                CumulativeBillingSelected = true;
                MonthlyBillingSelected = false;
                DailyEnergyProfileSelected = false;
            }
            else if (rdbMonthlyBilling.Checked)
            {
                MonthlyBillingSelected = true;
                CumulativeBillingSelected = false;
                DailyEnergyProfileSelected = false;
            }
            else if (rdbDailyMonthlyBilling.Checked)
            {
                DailyEnergyProfileSelected = true;
                CumulativeBillingSelected = false;
                MonthlyBillingSelected = false;

            }

            // Update Billing Interface
            BillingData.Clear();

            if (CumulativeBillingSelected)
            {
                // lblHeading.Text = "Cumulative Billing Data";
                gpMonthlyBillingFilter.Visible = false;
                //lbl_MDI_Reset_Counter.Visible = false;
                //lbl_billingCount.Visible = false;

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
                    if (_readDetail && CumulativeBillingSelected)
                        UpdateBillingDetailGUI();
                    else
                        displayBilling(0);
                }

            }
            else if (MonthlyBillingSelected || DailyEnergyProfileSelected)
            {
                if (MonthlyBillingSelected)
                {
                    // lblHeading.Text = "Monthly Billing Data";
                    gpMonthlyBillingFilter.Visible = true;
                    lbl_MDI_Reset_Counter.Visible = true;
                    lbl_billingCount.Visible = true;
                }
                else if (DailyEnergyProfileSelected)
                {
                    gpMonthlyBillingFilter.Visible = false;
                    lbl_MDI_Reset_Counter.Visible = true;
                    lbl_billingCount.Visible = true;
                }

                // Mark All Last 24 Months 
                // check_readBilling.Checked = true;
                // combo_billingSelective.SelectedItem = "24";

                // Update Billing Data Here
                if (MonthlyBillingSelected)
                {
                    if (MonthlyBillingData != null && MonthlyBillingData.Count > 0)
                        BillingData.AddRange(MonthlyBillingData);
                }
                else if (DailyEnergyProfileSelected)
                {
                    if (DailyBillingData != null && DailyBillingData.Count > 0)
                        BillingData.AddRange(DailyBillingData);
                }
                else
                {
                    if (CurrentBillingData != null && CurrentBillingData.Count > 0)
                        BillingData.AddRange(CurrentBillingData);
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

        private void UpdateBillingDetailGUI()
        {
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kWh_Cumm_Import)).Read)
            {
                UpdateGrid("KwhImport",
                    LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.KwhImport / 1000).ToString(), 3),
                    LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.KwhImport / 1000).ToString(), 3),
                    LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.KwhImport / 1000).ToString(), 3),
                    LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.KwhImport / 1000).ToString(), 3),
                    LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.KwhImport / 1000).ToString(), 3)
                    );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kWh_Cumm_Export)).Read)
            {
                UpdateGrid("kWh Export",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.KwhExport / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.KwhExport / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.KwhExport / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.KwhExport / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.KwhExport / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kWh_Cumm_Absolute)).Read)
            {
                UpdateGrid("Total kWh",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.KwhAbsolute / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.KwhAbsolute / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.KwhAbsolute / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.KwhAbsolute / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.KwhAbsolute / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kVarh_Cumm_Q1)).Read)
            {
                UpdateGrid("KvarhQ1",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.KvarhQ1 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.KvarhQ1 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.KvarhQ1 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.KvarhQ1 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.KvarhQ1 / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kVarh_Cumm_Q2)).Read)
            {
                UpdateGrid("KvarhQ2",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.KvarhQ2 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.KvarhQ2 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.KvarhQ2 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.KvarhQ2 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.KvarhQ2 / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kVarh_Cumm_Q3)).Read)
            {
                UpdateGrid("KvarhQ3",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.KvarhQ3 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.KvarhQ3 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.KvarhQ3 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.KvarhQ3 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.KvarhQ3 / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kVarh_Cumm_Q4)).Read)
            {
                UpdateGrid("KvarhQ4",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.KvarhQ4 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.KvarhQ4 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.KvarhQ4 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.KvarhQ4 / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.KvarhQ4 / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kVarh_Cumm_Absolute)).Read)
            {
                UpdateGrid("Total Kvarh",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.KvarhAbsolute / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.KvarhAbsolute / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.KvarhAbsolute / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.KvarhAbsolute / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.KvarhAbsolute / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_Cumm_kvah)).Read)
            {
                UpdateGrid("Kvah",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.Kvah / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.Kvah / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.Kvah / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.Kvah / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.Kvah / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_Cumm_Tamper_kW)).Read)
            {
                UpdateGrid("TamperKwh",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.TamperKwh / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.TamperKwh / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.TamperKwh / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.TamperKwh / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.TamperKwh / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kW_Cumm_Absolute)).Read)
            {
                UpdateGrid("Mdi Kw",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.MdiKw / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.MdiKw / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.MdiKw / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.MdiKw / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.MdiKw / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kVar_Cumm_Absolute)).Read)
            {
                UpdateGrid("Mdi Kvar",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.MdiKvar / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.MdiKvar / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.MdiKvar / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.MdiKvar / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.MdiKvar / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_Cumm_PowerFactor)).Read)
            {
                UpdateGrid("PowerFactor",
                Cumulative_Billing_T1_obj.PowerFactor,
                Cumulative_Billing_T2_obj.PowerFactor,
                Cumulative_Billing_T3_obj.PowerFactor,
                Cumulative_Billing_T4_obj.PowerFactor,
                Cumulative_Billing_Tot_obj.PowerFactor
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kW_CurMonth_Absolute)).Read)
            {
                UpdateGrid("Current Month MDI Kw",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.CurrentMonthMdiKw / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.CurrentMonthMdiKw / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.CurrentMonthMdiKw / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.CurrentMonthMdiKw / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.CurrentMonthMdiKw / 1000).ToString(), 3)
                );
            }
            if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Itm_kVar_CurMonth_Absolute)).Read)
            {
                UpdateGrid("Current Month MDI Kvar",
                LocalCommon.notRoundingOff((Cumulative_Billing_T1_obj.CurrentMonthMdiKvar / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T2_obj.CurrentMonthMdiKvar / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T3_obj.CurrentMonthMdiKvar / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_T4_obj.CurrentMonthMdiKvar / 1000).ToString(), 3),
                LocalCommon.notRoundingOff((Cumulative_Billing_Tot_obj.CurrentMonthMdiKvar / 1000).ToString(), 3)
                );
            }
        }

        private void UpdateGrid(string header, object t1, object t2, object t3, object t4, object tl)
        {

            grid_billing.Rows.Add();
            grid_billing.Rows[grid_billing.Rows.Count - 1].HeaderCell.Value = header;
            grid_billing[0, grid_billing.Rows.Count - 1].Value = t1;
            grid_billing[1, grid_billing.Rows.Count - 1].Value = t2;
            grid_billing[2, grid_billing.Rows.Count - 1].Value = t3;
            grid_billing[3, grid_billing.Rows.Count - 1].Value = t4;
            grid_billing[4, grid_billing.Rows.Count - 1].Value = tl;

            DataSet_Billing.DataTable_Billing.Rows.Add();
            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = header;
            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = t1.ToString();  //T1
            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = t2.ToString(); //T2
            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = t3.ToString(); //T3
            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = t4.ToString(); //T4
            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = tl.ToString(); //TL

        }

        #region Prev Next Billing Scroll
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
        #endregion

        private string ParseMdiDateTime(DateTime dt, DateTime lastMdiReset)
        {
            DateTime dtTemp = DateTime.MinValue;
            try
            {
                if (dt == null || dt == DateTime.MinValue || (dt.Year <= 2005 && dt.Month == 01 && dt.Day == 01 && dt.Hour == 0 && dt.Minute == 0))
                    throw new Exception();
                else if (lastMdiReset != minDateTime && (dt.Year == lastMdiReset.Year && dt.Month == lastMdiReset.Month && dt.Day == lastMdiReset.Day && dt.Hour == lastMdiReset.Hour && dt.Minute == lastMdiReset.Minute))
                    throw new Exception();
                else
                    return String.Format(DateTimeFormat, dt);
            }
            catch (Exception ex)
            {
                return dt_dash;
            }
        }

        public void displayBilling(int index)
        {
            ClearBillingGrid();

            if (index < 0 || index >= BillingData.Count)
            {
                return;
            }

            if (BillingData != null && BillingData.Count > 0)
            {
                var BillPeriodData = BillingData[index];// we decide which month's data to display!
                lbl_TotalPages.Text = BillingData.Count.ToString(); //keeps the count of total months data received

                if (MonthlyBillingSelected)
                {
                    lbl_billingCount.Text = BillingData[index].BillingCounter.ToString("000");
                    if (BillPeriodData.TimeStamp != DateTime.MinValue)
                        lbl_month.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", BillPeriodData.TimeStamp);
                    else
                        lbl_month.Text = "-----";
                }
                else if (DailyEnergyProfileSelected)
                {

                    lbl_billingCount.Text = BillingData[index].BillingCounter.ToString("000");
                    lbl_day_count.Text = BillingData[index].BillingCounterDay.ToString("000");
                    if (BillPeriodData.TimeStampDay != DateTime.MinValue)
                        lbl_month.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", BillPeriodData.TimeStampDay);
                    else
                        lbl_month.Text = "-----";

                    if (BillPeriodData.TimeStamp != DateTime.MinValue)
                        lbl_day_reset_date.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", BillPeriodData.TimeStamp);
                    else
                        lbl_day_reset_date.Text = "-----";
                }
                lbl_CurrentPage.Text = (index + 1).ToString();

                int rows_count = 0;
                string headerTitle = "";
                DataSet_Billing.DataTable_Billing.Clear();
                grid_billing.Rows.Clear();

                foreach (var item in BillPeriodData.BillingItems)
                {
                    try
                    {
                        //bool dtTimeCaptures = false;
                        // if (item.ValueInfo.Count>0) ;
                        if (index == 0 && BillingData[BillingData.Count - 1].BillingCounter > 24 && item.Name.Contains("Last Month Delta")) continue;

                        bool isDisplay = false;

                        List<AccessRights> _tempRight = new List<AccessRights>();
                        string unit = "";

                        if (!isDisableRights)
                        {
                            if (item.Unit != Unit.UnitLess)
                            {
                                foreach (var b in _Rights)
                                {
                                    if (!b.QuantityName.Contains("OccurDT") && !b.QuantityName.Contains("DateTime"))
                                    {
                                        string[] splitted = b.QuantityName.Split('_');
                                        foreach (string s in splitted)
                                        {
                                            if (s == item.Unit.ToString())
                                            {
                                                _tempRight.Add(b);
                                                break;
                                            }
                                        }
                                    }
                                }
                                //_tempRight = _Rights.FindAll(x => ((AccessRights)x).QuantityName.ToString().Contains(item.Unit.ToString()));
                            }
                            else
                            {
                                _tempRight = _Rights.FindAll(x => ((AccessRights)x).QuantityName.Contains("OccurDT") || ((AccessRights)x).QuantityName.Contains("DateTime"));
                            }
                        }

                        if (isDisableRights || (_tempRight != null && _tempRight.Count > 0))
                        {
                            var import = (isDisableRights || (item.Name.Contains("Import") && ((AccessRights)_tempRight.Find(x => x.QuantityName.Contains("Import"))).Read));
                            var export = (isDisableRights || (item.Name.Contains("Export") && ((AccessRights)_tempRight.Find(x => x.QuantityName.Contains("Export"))).Read));
                            var absolute = (isDisableRights || ((item.Name.Contains("Total") || (item.Name.Contains("Absolute"))) &&
                                            ((AccessRights)_tempRight.Find(x => x.QuantityName.Contains("Absolute"))).Read));

                            if (import || export || absolute)
                            {
                                if (!item.Name.Equals("Current Max Delta"))
                                {
                                    rows_count = grid_billing.Rows.Add();

                                    String value_T1 = LocalCommon.value_to_string(Convert_to_Valid(item.Value.T1), true);   //T1
                                    String value_T2 = LocalCommon.value_to_string(Convert_to_Valid(item.Value.T2), true);   //T2
                                    String value_T3 = LocalCommon.value_to_string(Convert_to_Valid(item.Value.T3), true);   //T3
                                    String value_T4 = LocalCommon.value_to_string(Convert_to_Valid(item.Value.T4), true);   //T4
                                    String value_TL = LocalCommon.value_to_string(Convert_to_Valid(item.Value.TL), true);   //TL

                                    headerTitle = String.Format("{0} {1}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString());
                                    grid_billing.Rows[rows_count].HeaderCell.Value = headerTitle;

                                    grid_billing[0, rows_count].Value = value_T1;
                                    grid_billing[1, rows_count].Value = value_T2;
                                    grid_billing[2, rows_count].Value = value_T3;
                                    grid_billing[3, rows_count].Value = value_T4;
                                    grid_billing[4, rows_count].Value = value_TL;
                                }

                                #region IsDateTimeDefined
                                ///DateTime Values Exists
                                if (!CumulativeBillingSelected) //MDI in Cumulative related to Class-3 while in Monthly related to Class-4 (which contains MDI Occur DT) //Azeem
                                {
                                    if (item.Value.CaptureTimeStamp != null && item.Value.CaptureTimeStamp.Count >= 5)
                                    {
                                        //dtTimeCaptures = false;

                                        rows_count = grid_billing.Rows.Add();
                                        //dtTimeCaptures = true;
                                        headerTitle = String.Format("{0} {1} {2}", item.Name.Replace(" Delta", ""), (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString(), "DateTime");
                                        grid_billing.Rows[rows_count].HeaderCell.Value = headerTitle;
                                        grid_billing[0, rows_count].Value = ParseMdiDateTime(item.Value.CaptureTimeStamp[Tariff.T1_Index], BillPeriodData.TimeStamp);
                                        grid_billing[1, rows_count].Value = ParseMdiDateTime(item.Value.CaptureTimeStamp[Tariff.T2_Index], BillPeriodData.TimeStamp);
                                        grid_billing[2, rows_count].Value = ParseMdiDateTime(item.Value.CaptureTimeStamp[Tariff.T3_Index], BillPeriodData.TimeStamp);
                                        grid_billing[3, rows_count].Value = ParseMdiDateTime(item.Value.CaptureTimeStamp[Tariff.T4_Index], BillPeriodData.TimeStamp);
                                        grid_billing[4, rows_count].Value = ParseMdiDateTime(item.Value.CaptureTimeStamp[Tariff.TL_Index], BillPeriodData.TimeStamp);
                                    }
                                }
                                #endregion
                            }
                        }

                        // for reporting
                        try
                        {
                            DataSet_Billing.DataTable_Billing.Rows.Add();
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = headerTitle;
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = Convert_to_Valid(item.Value.T1).ToString(item.Format);// T1
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = Convert_to_Valid(item.Value.T2).ToString(item.Format);// T2
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = Convert_to_Valid(item.Value.T3).ToString(item.Format);// T3
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = Convert_to_Valid(item.Value.T4).ToString(item.Format);// T4
                            DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = Convert_to_Valid(item.Value.TL).ToString(item.Format);// TL

                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                        //rows_count++;
                        // lbl_status.Text = " Reading Billing Data...";
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }

                try
                {
                    if (CumulativeBillingSelected)
                    {
                        //if (!application_Controller.ConnectionController.CurrentDevice.DeviceName.Equals("Fusion"))
                        //{
                        //    UpdateGrid("Current Month MDI Kw",
                        //        // Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw.ToString(), 3),// old format
                        //        Common.App_Common.notRoundingOff((Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw / 1000).ToString(), 3),
                        //        Common.App_Common.notRoundingOff((Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw / 1000).ToString(), 3),
                        //        Common.App_Common.notRoundingOff((Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw / 1000).ToString(), 3),
                        //        Common.App_Common.notRoundingOff((Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw / 1000).ToString(), 3),
                        //        Common.App_Common.notRoundingOff((Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw / 1000).ToString(), 3)
                        //        );
                        //}

                        /*grid_billing.Rows.Add();
                        grid_billing.Rows[grid_billing.Rows.Count - 1].HeaderCell.Value = "Current Month MDI Kw";
                        grid_billing[0, grid_billing.Rows.Count - 1].Value = Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw.ToString(), 3);  //T1
                        grid_billing[1, grid_billing.Rows.Count - 1].Value = Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKw.ToString(), 3); //T2
                        grid_billing[2, grid_billing.Rows.Count - 1].Value = Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKw.ToString(), 3); //T3
                        grid_billing[3, grid_billing.Rows.Count - 1].Value = Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKw.ToString(), 3); //T4
                        grid_billing[4, grid_billing.Rows.Count - 1].Value = Common.App_Common.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKw.ToString(), 3); //TL*/

                        //if (Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw != DateTime.MinValue &&
                        //    Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff2_CurrentMonthMdiKw != DateTime.MinValue &&
                        //    Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff3_CurrentMonthMdiKw != DateTime.MinValue &&
                        //    Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff4_CurrentMonthMdiKw != DateTime.MinValue &&
                        //    Instantaneous_Class_obj.TimeStamp_Cumulative_TariffTL_CurrentMonthMdiKw != DateTime.MinValue)

                        //    UpdateGrid("Current Max kW DateTime",
                        //                String.Format("{0:dd/MM/yyyy HH:mm:ss}", Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw),
                        //                String.Format("{0:dd/MM/yyyy HH:mm:ss}", Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw),
                        //                String.Format("{0:dd/MM/yyyy HH:mm:ss}", Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw),
                        //                String.Format("{0:dd/MM/yyyy HH:mm:ss}", Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw),
                        //                String.Format("{0:dd/MM/yyyy HH:mm:ss}", Instantaneous_Class_obj.TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw));
                        if (Instantaneous_Class_obj.MDIsToShow != null && Instantaneous_Class_obj.MDIsToShow.Count > 0)
                        {
                            foreach (QuantityMDI mdi in Instantaneous_Class_obj.MDIsToShow)
                            {
                                //if (mdi.TariffTL.IsValidTimeStamp)
                                //{
                                UpdateGrid(
                                      mdi.QuantityName,
                                      (mdi.Tariff1.Value.ToString()),
                                      (mdi.Tariff2.Value.ToString()),
                                      (mdi.Tariff3.Value.ToString()),
                                      (mdi.Tariff4.Value.ToString()),
                                      (mdi.TariffTL.Value.ToString())
                                      );
                                UpdateGrid(
                                      mdi.QuantityName + " Date Time",
                                      ParseMdiDateTime(mdi.Tariff1.TimeStamp, Instantaneous_Class_obj.Last_MDI_REset_Date),
                                      ParseMdiDateTime(mdi.Tariff2.TimeStamp, Instantaneous_Class_obj.Last_MDI_REset_Date),
                                      ParseMdiDateTime(mdi.Tariff3.TimeStamp, Instantaneous_Class_obj.Last_MDI_REset_Date),
                                      ParseMdiDateTime(mdi.Tariff4.TimeStamp, Instantaneous_Class_obj.Last_MDI_REset_Date),
                                      ParseMdiDateTime(mdi.TariffTL.TimeStamp, Instantaneous_Class_obj.Last_MDI_REset_Date)
                                 );
                                //}
                            }
                        }
                    }
                    // if (_readDetail) UpdateBillingDetailGUI(CumulativeBillingSelected);

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    try
                    {
                        lbl_month.Text = String.Format(DateTimeFormat, Instantaneous_Class_obj.Last_MDI_REset_Date);
                        lbl_billingCount.Text = Instantaneous_Class_obj.MDI_reset_count.ToString();
                    }
                    catch (Exception)
                    {
                    }
                }
                // add it to dataset for reporting and to list for storing records to database
                DataSet_Billing.DataTable_Billing.Rows.Add();
                DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = headerTitle;
                DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T1).ToString("f1");  //T1
                DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T2).ToString("f1"); //T2
                DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T3).ToString("f1"); //T3
                DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.T4).ToString("f1"); //T4
                DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = Convert_to_Valid(BillPeriodData.BillingItems[BillPeriodData.BillingItems.Count - 1].Value.TL).ToString("f1"); //TL
                // BillPeriodData.BillingItems.RemoveAt(BillPeriodData.BillingItems.Count - 1);
                // database list

                // end addition to dataset and list for database
                //lbl_status.Text = " Reading Billing Data Complete...";
            }
            else
            {
                Notification Notifier = new Notification("Error", "Billing Data not available");
            }
        }

        //        public void displayBilling(int index)
        //        {
        //            System.Drawing.Size Grid_Size = grid_billing.Size;
        //            try
        //            {
        //                ((System.ComponentModel.ISupportInitialize)(grid_billing)).BeginInit();

        //                //Uncommented By Azeem Inayat //v10.0.9
        //                grid_billing.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
        //                grid_billing.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
        //                grid_billing.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
        //                grid_billing.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
        //                grid_billing.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

        //                ClearBillingGrid();

        //                if (index < 0 || index >= BillingData.Count)
        //                {
        //                    return;
        //                }

        //                if (BillingData != null && BillingData.Count > 0)
        //                {
        //                    var BillPeriodData = BillingData[index];// we decide which month's data to display!
        //                    lbl_TotalPages.Text = BillingData.Count.ToString(); //keeps the count of total months data received
        //                    lbl_billingCount.Text = BillingData[index].BillingCounter.ToString("000");
        //                    if (BillPeriodData.TimeStamp != DateTime.MinValue)
        //                        lbl_month.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", BillPeriodData.TimeStamp);
        //                    else
        //                        lbl_month.Text = "-----";
        //                    lbl_CurrentPage.Text = (index + 1).ToString();

        //                    int rows_count = 0;
        //                    string headerTitle = "";

        //                    if (CumulativeBillingSelected)
        //                    {
        //                        billingDP = 2;
        //                        lbl_billingCount.Text = Instantaneous_Class_obj.MDI_reset_count.ToString();
        //                    }
        //                    else //if monthly
        //                    {
        //                        billingDP = 2;
        //                    }
        //                    string InitVal = (billingDP > 0) ? ("0.0").PadRight((billingDP + 2), '0') : "0";
        //                    String value_T1 = InitVal, value_T2 = InitVal, value_T3 = InitVal, value_T4 = InitVal, value_TL = InitVal; //By Azeem

        //                    lbl_LastMdiResetDate.Text = Instantaneous_Class_obj.Last_MDI_REset_Date.ToString("dd/MM/yyyy");
        //                    lbl_LastMdiResetTime.Text = Instantaneous_Class_obj.Last_MDI_REset_Date.ToString("HH:mm:ss"); //ResetTime v4.8.22

        //                    DataSet_Billing.DataTable_Billing.Clear();
        //                    grid_billing.Rows.Clear();
        //                    foreach (var item in BillPeriodData.BillingItems)
        //                    {
        //                        grid_billing.Rows.Add();
        //                        //bool dtTimeCaptures = false;

        //                        #region Define Row Header
        //                        //if (dtTimeCaptures)
        //                        //    headerTitle = String.Format("{0} {1} {2}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString(), "DateTime");
        //                        //else
        //                        headerTitle = String.Format("{0} {1}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString());

        //                        grid_billing.Rows[rows_count].HeaderCell.Value = headerTitle;
        //                        #endregion
        //                        #region Copy Data & Capture Time Values

        //                        //If RoundingOff not Required then call following
        //                        //value_T1 = LocalCommon.Commons.notRoundingOff(Convert_to_Valid(item.Value.T1).ToString(), billingDP);
        //                        //For RoundingOff use
        //                        //value_T1 = Commons.value_to_string(Convert_to_Valid(item.Value.T1), "f0");
        //                        grid_billing[0, rows_count].Value = LocalCommon.notRoundingOff(Convert_to_Valid(item.Value.T1).ToString(), billingDP);   //T1
        //                        grid_billing[1, rows_count].Value = LocalCommon.notRoundingOff(Convert_to_Valid(item.Value.T2).ToString(), billingDP);   //T2
        //                        grid_billing[2, rows_count].Value = LocalCommon.notRoundingOff(Convert_to_Valid(item.Value.T3).ToString(), billingDP);   //T3
        //                        grid_billing[3, rows_count].Value = LocalCommon.notRoundingOff(Convert_to_Valid(item.Value.T4).ToString(), billingDP);   //T4
        //                        grid_billing[4, rows_count].Value = LocalCommon.notRoundingOff(Convert_to_Valid(item.Value.TL).ToString(), billingDP);   //TL

        //                        #endregion
        //                        ////lbl_status.Text = " Reading Billing Data...";
        //#if Display_MdiOccurDateTime_FromBillPacket
        //                        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                        //v4.8.29 New MDI Occurrence DateTime Implementation by Azeem
        //                        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //                        #region IsDateTimeDefined
        //                        ///DateTime Values Exists
        //                        if (!CumulativeBillingSelected)
        //                        {
        //                            if (item.Value.CaptureTimeStamp == null || item.Value.CaptureTimeStamp.Count == 0)
        //                            {
        //                                //dtTimeCaptures = false;
        //                            }
        //                            else
        //                            {
        //                                rows_count++;
        //                                grid_billing.Rows.Add();
        //                                //headerTitle = String.Format("{0} {1} {2}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString(), "DateTime");
        //                                headerTitle = "Max "+ item.Unit.ToString() + " Date and Time";
        //                                grid_billing.Rows[rows_count].HeaderCell.Value = headerTitle;
        //                                grid_billing[0, rows_count].Value = ParseMdiDateTime(item.Value.CaptureTimeStamp[Tariff.T1_Index]);
        //                                grid_billing[1, rows_count].Value = ParseMdiDateTime(item.Value.CaptureTimeStamp[Tariff.T2_Index]);
        //                                grid_billing[2, rows_count].Value = ParseMdiDateTime(item.Value.CaptureTimeStamp[Tariff.T3_Index]);
        //                                grid_billing[3, rows_count].Value = ParseMdiDateTime(item.Value.CaptureTimeStamp[Tariff.T4_Index]);
        //                                grid_billing[4, rows_count].Value = ParseMdiDateTime(item.Value.CaptureTimeStamp[Tariff.TL_Index]);
        //                            } 
        //                        }
        //                        #endregion
        //#endif
        //                        rows_count++;
        //                    } //Fearch Loop BillingDataItem ends here

        //                    try
        //                    {
        //                        if (CumulativeBillingSelected)
        //                        {
        //#if Read_Display_CumMDI_CumBill_InstCls
        //                            grid_billing.Rows.Add();
        //                            grid_billing.Rows[grid_billing.Rows.Count - 1].HeaderCell.Value = "Current Month MDI Kw";

        //                            grid_billing[0, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw.ToString(), billingDP);  //T1

        //                            grid_billing[1, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKw.ToString(), billingDP); //T2

        //                            grid_billing[2, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKw.ToString(), billingDP); //T3

        //                            grid_billing[3, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKw.ToString(), billingDP); //T4

        //                            grid_billing[4, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKw.ToString(), billingDP); //TL
        //#endif  //v4.8.25

        //#if Read_Display_CumMdiOccurDateTime_CumBill_InstCls
        //                            grid_billing.Rows.Add();
        //                            //string DT_Format = "{0:dd/MM/yyyy HH:mm:ss}"; //Added by Azeem //v10.0.9
        //                            grid_billing.Rows[grid_billing.Rows.Count - 1].HeaderCell.Value = "Current Max kW Date and Time";
        //                            grid_billing[0, grid_billing.Rows.Count - 1].Value = ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
        //                            grid_billing[1, grid_billing.Rows.Count - 1].Value = ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
        //                            grid_billing[2, grid_billing.Rows.Count - 1].Value = ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
        //                            grid_billing[3, grid_billing.Rows.Count - 1].Value = ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
        //                            grid_billing[4, grid_billing.Rows.Count - 1].Value = ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
        //#endif  //v4.8.25
        //                        }
        //                        else
        //                        {
        //                            //v4.8.29
        //#if Read_Display_MDI_Delta_Monthly_InstCls
        //                            grid_billing.Rows.Add();
        //                            grid_billing.Rows[grid_billing.Rows.Count - 1].HeaderCell.Value = "This Month MDI Kw";

        //                            grid_billing[0, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw.ToString(), 3);  //T1

        //                            grid_billing[1, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKw.ToString(), 3); //T2

        //                            grid_billing[2, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKw.ToString(), 3); //T3

        //                            grid_billing[3, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKw.ToString(), 3); //T4

        //                            grid_billing[4, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKw.ToString(), 3); //TL
        //#endif

        //#if Read_Display_MdiOccurDateTime_Delta_Monthly_InstCls
        //                            grid_billing.Rows.Add();
        //                            string DT_Format = "{0:dd/MM/yyyy HH:mm:ss}"; //Added by Azeem //v10.0.9
        //                            grid_billing.Rows[grid_billing.Rows.Count - 1].HeaderCell.Value = "This Max kW DateTime";

        //                            grid_billing[0, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format,
        //                                Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

        //                            grid_billing[1, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format,
        //                                Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

        //                            grid_billing[2, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format,
        //                                Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

        //                            grid_billing[3, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format,
        //                                Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

        //                            grid_billing[4, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format,
        //                                Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
        //#endif

        //#if Read_Display_MDI_kvar_Delta_Monthly_InstCls
        //                            grid_billing.Rows.Add();
        //                            grid_billing.Rows[grid_billing.Rows.Count - 1].HeaderCell.Value = "This Month MDI Kvar";

        //                            grid_billing[0, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKvar.ToString(), 3);  //T1

        //                            grid_billing[1, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKvar.ToString(), 3); //T2

        //                            grid_billing[2, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKvar.ToString(), 3); //T3

        //                            grid_billing[3, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKvar.ToString(), 3); //T4

        //                            grid_billing[4, grid_billing.Rows.Count - 1].Value = LocalCommon.Commons.
        //                                notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKvar.ToString(), 3); //TL

        //                            grid_billing.Rows.Add();
        //                            string DT_Format2 = "{0:dd/MM/yyyy HH:mm:ss}"; //Added by Azeem //v10.0.9
        //                            grid_billing.Rows[grid_billing.Rows.Count - 1].HeaderCell.Value = "This Max kvar DateTime";

        //                            grid_billing[0, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format2,
        //                                Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKvar.Date_Time_Stamp.GetDateTime());

        //                            grid_billing[1, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format2,
        //                                Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKvar.Date_Time_Stamp.GetDateTime());

        //                            grid_billing[2, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format2,
        //                                Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKvar.Date_Time_Stamp.GetDateTime());

        //                            grid_billing[3, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format2,
        //                                Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKvar.Date_Time_Stamp.GetDateTime());

        //                            grid_billing[4, grid_billing.Rows.Count - 1].Value = String.Format(DT_Format2,
        //                                Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKvar.Date_Time_Stamp.GetDateTime());

        //#endif
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                        throw;
        //                    }
        //                    ////lbl_status.Text = " Reading Billing Data Complete...";
        //                }
        //                else
        //                {
        //                    Notification Notifier = new Notification("Error", "Billing Data not available");
        //                }
        //            }
        //            finally
        //            {
        //                //Force To Resize GridView
        //                Grid_Size = grid_billing.Size;
        //                grid_billing.Size = new System.Drawing.Size(0, 0);
        //                grid_billing.Size = Grid_Size;

        //                ((System.ComponentModel.ISupportInitialize)(grid_billing)).EndInit();
        //            }
        //        }

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

        private void GUI_CurrentBilling()
        {
            lbl_month.Visible = true;
            label22.Visible = true;
            lbl_MDI_Reset_Counter.Visible = true;
            lbl_billingCount.Visible = true;
            btn_firstRecord.Visible = false;
            btn_lastRecord.Visible = false;
            btn_NextMonth.Visible = false;
            btn_PreviousMonth.Visible = false;
            panel1.Visible = false;

            lbl_MDI_Reset_Counter.Text = "MDI Reset Count";
        }
        private void GUI_MonthlyBilling()
        {
            lbl_month.Visible = true;
            label22.Visible = true;
            lbl_MDI_Reset_Counter.Visible = true;
            lbl_billingCount.Visible = true;
            btn_firstRecord.Visible = true;
            btn_lastRecord.Visible = true;
            btn_NextMonth.Visible = true;
            btn_PreviousMonth.Visible = true;
            panel1.Visible = true;

            lbl_MDI_Reset_Counter.Text = "MDI Reset Counter:";
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
            lbl_day_reset_date.Text = "";
            lbl_day_count.Text = "";
        }

        private void ClearBillingData()
        {
            BillingData.Clear();

            #region Clear Billing Data Container

            if (rdbCumulativeBilling.Checked)
            {
                CumulativeBillingData = null;
            }
            else if (rdbMonthlyBilling.Checked)
            {
                // Clear Current Billing Data
                if (CurrentBillingData != null)
                    CurrentBillingData.Clear();

                if (MonthlyBillingData != null)
                    MonthlyBillingData.Clear();
            }
            else if (rdbDailyMonthlyBilling.Checked)
            {
                // Clear Current Billing Data
                if (CurrentBillingData != null)
                    CurrentBillingData.Clear();

                if (DailyBillingData != null)
                    DailyBillingData.Clear();
            }

            #endregion

            for (int i = grid_billing.Rows.Count - 1; i >= 0; i--)
            {
                grid_billing.Rows.RemoveAt(i);
            }

            lbl_CurrentPage.Text = "";
            lbl_TotalPages.Text = "";
            lbl_month.Text = "";
            lbl_billingCount.Text = "";
            lbl_day_reset_date.Text = "";
            lbl_day_count.Text = "";
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
                if (tbBillingTypes.SelectedIndex == 0)
                {
                    CumulativeBillingSelected = true;
                }
                else
                {
                    CumulativeBillingSelected = false;
                }
                ///Init Billing Interface Work

                ///initializeBillingDictionary();
                if (Application_Controller != null)
                {
                    BillingController = Application_Controller.Billing_Controller;
                    instantController = Application_Controller.InstantaneousController;
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

                if (rdbMonthlyBilling.Checked)
                    BillingController.MonthlyBillingDFieldFilter = BillingController.MonthlyBilling_Field;
                else
                    BillingController.MonthlyBillingDFieldFilter = BillingController.DailyEnergyProfile_Field;
            }
        }

        #region Billing_Read_BckWorker

        private void btn_newBillingbutton_Click(object sender, EventArgs e)
        {
            _readDetail = chkDetail.Checked;
            this.btn_clearBillingGrid.Visible = false;
            this.btn_BillingReport.Visible = false;
            IsReadingCumulativeBilling = (CumulativeBillingSelected) ? true : false;
            try
            {
                if (btn_newBillingbutton.Text == "Read Data")
                {
                    if (Application_Process.Is_Association_Developed)
                    {
                        tbBillingTypes.Enabled = true;
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
                        Cursor.Current = Cursors.WaitCursor;
                        bckWorker_Billing.RunWorkerAsync();
                        clsStatus.ProgBarVisible(true);
                        Application_Controller.IsIOBusy = true;
                    }
                    else
                    {
                        Notification Notifier = new Notification("Error", "Create Association to Meter");
                    }
                }
                else
                {
                    if (btn_newBillingbutton.Text == "Cancel")
                    {
                        tbBillingTypes.Enabled = true;
                        btn_newBillingbutton.Enabled = true;
                        bckWorker_Billing.CancelAsync();
                    }
                    else
                    {
                        Notification Notifier = new Notification("Error", "Create Association to Meter");
                    }
                }
            }
            catch (Exception ex)
            {
                tbBillingTypes.Enabled = true;
                btn_newBillingbutton.Enabled = true;
                if (IsReadingCumulativeBilling)
                {
                    CumulativeBillingData = null;
                }
                else
                {
                    MonthlyBillingData = new List<BillingData>();
                }
                UpdateBilling();

                Notification Notifier = new Notification("Error",
                    String.Format("Billing Data not available,\r\n{0}", ex.Message));
                MessageBox.Show(String.Format("Billing Data not available,\r\n{0}", ex.Message)); //v4.8.30
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
                //AccurateOptocomSoftware.LocalCommon.clsStatus.ProgBarVisible(true);
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
                        //pb_billing.Visible = false;
                        clsStatus.ProgBarVisible(false);
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
                Notification Notifier = new Notification("Error", "Billing Data Not Available");
                //MessageBox.Show("Billing Data Not Available");
            }

        }

        private void bckWorker_Billing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region Commented Code
            //try
            //{
            //    tabControl1.Enabled = true;
            //    btn_newBillingbutton.Enabled = true;

            //    if (application_Controller.isSinglePhase)
            //    {
            //        grid_billing.Columns.Add("Value", "Value");
            //        grid_billing.Size = new System.Drawing.Size(417, 332);
            //        grid_billing.Columns["Value"].Width = grid_billing.Columns["Value"].Width + 100;
            //    }
            //    else
            //    {
            //        grid_billing.Columns.Add("T1", "T1");
            //        grid_billing.Columns.Add("T2", "T2");
            //        grid_billing.Columns.Add("T3", "T3");
            //        grid_billing.Columns.Add("T4", "T4");
            //        grid_billing.Columns.Add("TL", "TL");

            //        for (int i = 0; i < grid_billing.Columns.Count; i++)
            //        {
            //            grid_billing.Columns[i].Width += 20;
            //        }
            //        //grid_billing.Size = new System.Drawing.Size(750, 332);

            //    }

            //    Notification Notifier;
            //    //pb_billing.Visible = false;
            //    btn_BillingReport.Visible = true;
            //    if (tabControl1.SelectedIndex == 0)
            //    {
            //        btn_comm_verification_rpt.Visible = true;
            //    }
            //    else
            //    {
            //        btn_comm_verification_rpt.Visible = false;
            //    }

            //    if (e.Error == null)
            //    {
            //        // Update Data Objects
            //        //if (rdbMonthlyBilling.Checked)
            //        //    MonthlyBillingData = CurrentBillingData;
            //        //else if (rdbDailyMonthlyBilling.Checked)
            //        //    DailyBillingData = CurrentBillingData;

            //        UpdateBilling();
            //        Cursor.Current = Cursors.Arrow;

            //        if (check_Billing_AddToDB.Checked)
            //        {
            //            Add_Billing_to_DB(BillingData);
            //            Save_Billing_Data();
            //        }
            //        //lbl_status.Text = "Reading Billing Data Complete";
            //        if (GetCompleted)
            //            Notifier = new Notification("Process Completed", "Reading Billing Data Complete");
            //        else
            //            Notifier = new Notification("Process Failed", " Error Reading Billing Data ");
            //    }
            //    else
            //        throw e.Error;
            //}
            //catch (Exception ex)
            //{
            //    if (CumulativeBillingSelected)
            //    {
            //        CumulativeBillingData = null;
            //    }
            //    else
            //    {
            //        MonthlyBillingData = new List<BillingData>();
            //    }

            //    UpdateBilling();

            //    Notification Notifier = new Notification("Error",
            //        String.Format("Billing Data not available,{0}\r\n{1}",
            //        (ex != null) ? ex.Message : "",
            //        (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));
            //}
            //finally
            //{
            //    if (bckWorker_Billing.IsBusy &&
            //        (billingReadBckThread.ThreadState != ThreadState.Aborted &&
            //        billingReadBckThread.ThreadState != ThreadState.Stopped))
            //    {
            //        // SetReadWriteStatus(true);
            //        Application_Controller.IsIOBusy = true;
            //        btn_newBillingbutton.Text = "Cancel";
            //    }
            //    else
            //    {
            //        // SetReadWriteStatus(false);
            //        Application_Controller.IsIOBusy = false;
            //        btn_newBillingbutton.Text = "Read Data";
            //        //pb_billing.Visible = false;
            //    }

            //    clsStatus.ProgBarVisible(false);
            //} 
            #endregion

            try
            {
                tbBillingTypes.Enabled = true;
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
                    //grid_billing.Size = new System.Drawing.Size(750, 332);

                }

                Notification Notifier;
                // pb_billing.Visible = false;
                btn_BillingReport.Visible = true;
                //if (tbBillingTypes.SelectedIndex == 0)
                //{
                //    btn_comm_verification_rpt.Visible = true;
                //}
                //else
                //{
                //    btn_comm_verification_rpt.Visible = false;
                //}

                if (e.Error == null)
                {
                    // Update Data Objects
                    if (rdbMonthlyBilling.Checked)
                        MonthlyBillingData = CurrentBillingData;
                    else if (rdbDailyMonthlyBilling.Checked)
                        DailyBillingData = CurrentBillingData;

                    UpdateBilling();
                    Cursor.Current = Cursors.Arrow;

                    if (check_Billing_AddToDB.Checked)
                    {
                        Add_Billing_to_DB(BillingData);
                        Save_Billing_Data();
                    }
                    //lbl_status.Text = "Reading Billing Data Complete";
                    if (GetCompleted)
                        Notifier = new Notification("Process Completed", "Reading Billing Data Complete");
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
                    CurrentBillingData = new List<BillingData>();

                    if (rdbMonthlyBilling.Checked)
                        MonthlyBillingData = CurrentBillingData;
                    else if (rdbDailyMonthlyBilling.Checked)
                        DailyBillingData = CurrentBillingData;
                }

                UpdateBilling();

                Notification Notifier = new Notification("Error",
                    String.Format("Billing Data not available,{0}\r\n{1}",
                    (ex != null) ? ex.Message : "",
                    (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));
            }
            finally
            {
                if (bckWorker_Billing.IsBusy &&
                    (billingReadBckThread.ThreadState != ThreadState.Aborted &&
                    billingReadBckThread.ThreadState != ThreadState.Stopped))
                {
                    // SetReadWriteStatus(true);
                    Application_Controller.IsIOBusy = true;
                    btn_newBillingbutton.Text = "Cancel";
                }
                else
                {
                    // SetReadWriteStatus(false);
                    Application_Controller.IsIOBusy = false;
                    btn_newBillingbutton.Text = "Read Data";
                    clsStatus.ProgBarVisible(false);
                    //pb_billing.Visible = false;
                }
                this.btn_clearBillingGrid.Visible = true;
                this.btn_BillingReport.Visible = true;
            }

        }
        //private void bckWorker_Billing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    try
        //    {
        //        tabControl1.Enabled = true;
        //        btn_newBillingbutton.Enabled = true;
        //        if (application_Controller.isSinglePhase)
        //        {
        //            grid_billing.Columns.Add("Value", "Value");
        //            grid_billing.Size = new System.Drawing.Size(417, 332);
        //            grid_billing.Columns["Value"].Width = grid_billing.Columns["Value"].Width + 100;
        //        }
        //        else
        //        {
        //            grid_billing.Columns.Add("T1", "T1");
        //            grid_billing.Columns.Add("T2", "T2");
        //            grid_billing.Columns.Add("T3", "T3");
        //            grid_billing.Columns.Add("T4", "T4");
        //            grid_billing.Columns.Add("TL", "TL");

        //            for (int i = 0; i < grid_billing.Columns.Count; i++)
        //            {
        //                grid_billing.Columns[i].Width += 20;
        //            }
        //            grid_billing.Size = new System.Drawing.Size(950, 271);//(763, 271);//700, 332);

        //        }
        //        Notification Notifier;
        //        //pb_billing.Visible = false;
        //        btn_BillingReport.Visible = true;
        //        AccurateOptocomSoftware.LocalCommon.clsStatus.ProgBarVisible(false);
        //        if (tabControl1.SelectedIndex == 0)
        //        {
        //            //btn_comm_verification_rpt.Visible = true;
        //            btn_comm_verification_rpt.Visible = false; //should be added in User Rights  //by M.Azeem Inayat
        //        }
        //        else
        //        {
        //            btn_comm_verification_rpt.Visible = false;
        //        }
        //        if (e.Error == null)
        //        {
        //            UpdateBilling();
        //            Cursor.Current = Cursors.Arrow;

        //            #region Save Billing Data in DB
        //            //**********************************
        //            try
        //            {
        //                if (application_Controller.isSinglePhase)
        //                {
        //                    //displayBilling(0, true);
        //                    if (check_Billing_AddToDB.Checked) //v4.8.23
        //                    {
        //                        if (IsReadingCumulativeBilling)
        //                        {
        //                            cumulativeBilling_SinglePhase toSave_SP = BillingController.saveToClass_SinglePhase(BillingData[0], Application_Controller.ConnectionManager.ConnectionInfo.MSN);
        //                            dbController.saveCumulativeBillingData_SinglePhase(toSave_SP);
        //                        }
        //                        else
        //                        {
        //                            Monthly_Billing_data_SinglePhase monthlyData_SP = BillingController.saveToClass_SinglePhase(BillingData, Application_Controller.ConnectionManager.ConnectionInfo.MSN);
        //                            dbController.saveMonthlyBillingData_SinglePhase(monthlyData_SP);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    //displayBilling(0);

        //                    if (check_Billing_AddToDB.Checked) //v4.8.23
        //                    {
        //                        if (IsReadingCumulativeBilling)
        //                        {
        //                            Cumulative_billing_data toSave = BillingController.saveToClass(BillingData[0], Application_Controller.ConnectionManager.ConnectionInfo.MSN);
        //                            dbController.saveCumulativeBillingData(toSave);
        //                        }
        //                        else
        //                        {
        //                            Monthly_Billing_data toSave = BillingController.saveToClass(BillingData, Application_Controller.ConnectionManager.ConnectionInfo.MSN);
        //                            if (!dbController.saveMonthlyBillingData(toSave))
        //                            {
        //                                Notification n = new Notification("Error", "Error saving Monthly Billing Data");
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //                throw new Exception("Error saving Billing data, Reason: " + ex);
        //            }
        //            #endregion
        //            //********************************

        //            ////lbl_status.Text = "Reading Billing Data Complete";
        //            if (GetCompleted)
        //            {
        //                Notifier = new Notification("Process Completed", "Reading Billing Data Complete");

        //            }
        //            else
        //                Notifier = new Notification("Process Failed", " Error Reading Billing Data ");
        //        }
        //        else
        //            throw e.Error;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (IsReadingCumulativeBilling)
        //        {
        //            CumulativeBillingData = null;
        //        }
        //        else
        //        {
        //            MonthlyBillingData = new List<BillingData>();
        //        }
        //        UpdateBilling();

        //        Notification Notifier = new Notification("Error",
        //            String.Format("Billing Data not available,\r\n{0}\r\n{1}",
        //            (ex != null) ? ex.Message : "",
        //            (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));
        //    }
        //    finally
        //    {
        //        if (bckWorker_Billing.IsBusy &&
        //           (billingReadBckThread.ThreadState != ThreadState.Aborted &&
        //            billingReadBckThread.ThreadState != ThreadState.Stopped))
        //        {
        //            ///SetReadWriteStatus(true);
        //            Application_Controller.IsIOBusy = true;
        //            btn_newBillingbutton.Text = "Cancel";
        //        }
        //        else
        //        {
        //            ///SetReadWriteStatus(false);
        //            Application_Controller.IsIOBusy = false;
        //            btn_newBillingbutton.Text = "Read Data";
        //            //pb_billing.Visible = false;
        //            AccurateOptocomSoftware.LocalCommon.clsStatus.ProgBarVisible(false);
        //        }
        //    }

        //}

        private void bckWorker_Billing_DoWorkHelper()
        {
            try
            {
                if (IsReadingCumulativeBilling && IsReadCum_currentMonthMdi)
                {
                    if (!_readDetail)
                        //{
                        // if this chek is in then first time detail data is not read.//sajid
                        CumulativeBillingData = BillingController.GetCumulativeBillingData(application_Controller.isSinglePhase);
                    // int ee = CumulativeBillingData.BillingItems.Count;
                    else
                    {
                        #region Detailed_Billing


                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "KwhImport", Get_Index.Cumulative_Tariff1_KwhImport, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "KwhImport", Get_Index.Cumulative_Tariff2_KwhImport, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "KwhImport", Get_Index.Cumulative_Tariff3_KwhImport, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "KwhImport", Get_Index.Cumulative_Tariff4_KwhImport, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "KwhImport", Get_Index.Cumulative_TariffTL_KwhImport, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "KwhExport", Get_Index.Cumulative_Tariff1_KwhExport, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "KwhExport", Get_Index.Cumulative_Tariff2_KwhExport, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "KwhExport", Get_Index.Cumulative_Tariff3_KwhExport, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "KwhExport", Get_Index.Cumulative_Tariff4_KwhExport, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "KwhExport", Get_Index.Cumulative_TariffTL_KwhExport, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "KwhAbsolute", Get_Index.Cumulative_Tariff1_KwhAbsolute, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "KwhAbsolute", Get_Index.Cumulative_Tariff2_KwhAbsolute, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "KwhAbsolute", Get_Index.Cumulative_Tariff3_KwhAbsolute, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "KwhAbsolute", Get_Index.Cumulative_Tariff4_KwhAbsolute, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "KwhAbsolute", Get_Index.Cumulative_TariffTL_KwhAbsolute, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "KvarhQ1", Get_Index.Cumulative_Tariff1_KvarhQ1, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "KvarhQ1", Get_Index.Cumulative_Tariff2_KvarhQ1, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "KvarhQ1", Get_Index.Cumulative_Tariff3_KvarhQ1, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "KvarhQ1", Get_Index.Cumulative_Tariff4_KvarhQ1, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "KvarhQ1", Get_Index.Cumulative_TariffTL_KvarhQ1, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "KvarhQ2", Get_Index.Cumulative_Tariff1_KvarhQ2, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "KvarhQ2", Get_Index.Cumulative_Tariff2_KvarhQ2, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "KvarhQ2", Get_Index.Cumulative_Tariff3_KvarhQ2, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "KvarhQ2", Get_Index.Cumulative_Tariff4_KvarhQ2, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "KvarhQ2", Get_Index.Cumulative_TariffTL_KvarhQ2, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "KvarhQ3", Get_Index.Cumulative_Tariff1_KvarhQ3, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "KvarhQ3", Get_Index.Cumulative_Tariff2_KvarhQ3, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "KvarhQ3", Get_Index.Cumulative_Tariff3_KvarhQ3, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "KvarhQ3", Get_Index.Cumulative_Tariff4_KvarhQ3, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "KvarhQ3", Get_Index.Cumulative_TariffTL_KvarhQ3, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "KvarhQ4", Get_Index.Cumulative_Tariff1_KvarhQ4, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "KvarhQ4", Get_Index.Cumulative_Tariff2_KvarhQ4, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "KvarhQ4", Get_Index.Cumulative_Tariff3_KvarhQ4, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "KvarhQ4", Get_Index.Cumulative_Tariff4_KvarhQ4, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "KvarhQ4", Get_Index.Cumulative_TariffTL_KvarhQ4, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "KvarhAbsolute", Get_Index.Cumulative_Tariff1_KvarhAbsolute, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "KvarhAbsolute", Get_Index.Cumulative_Tariff2_KvarhAbsolute, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "KvarhAbsolute", Get_Index.Cumulative_Tariff3_KvarhAbsolute, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "KvarhAbsolute", Get_Index.Cumulative_Tariff4_KvarhAbsolute, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "KvarhAbsolute", Get_Index.Cumulative_TariffTL_KvarhAbsolute, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "Kvah", Get_Index.Cumulative_Tariff1_Kvah, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "Kvah", Get_Index.Cumulative_Tariff2_Kvah, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "Kvah", Get_Index.Cumulative_Tariff3_Kvah, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "Kvah", Get_Index.Cumulative_Tariff4_Kvah, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "Kvah", Get_Index.Cumulative_TariffTL_Kvah, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "TamperKwh", Get_Index.Cumulative_Tariff1_TamperKwh, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "TamperKwh", Get_Index.Cumulative_Tariff2_TamperKwh, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "TamperKwh", Get_Index.Cumulative_Tariff3_TamperKwh, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "TamperKwh", Get_Index.Cumulative_Tariff4_TamperKwh, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "TamperKwh", Get_Index.Cumulative_TariffTL_TamperKwh, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "MdiKw", Get_Index.Cumulative_Tariff1_MdiKw, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "MdiKw", Get_Index.Cumulative_Tariff2_MdiKw, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "MdiKw", Get_Index.Cumulative_Tariff3_MdiKw, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "MdiKw", Get_Index.Cumulative_Tariff4_MdiKw, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "MdiKw", Get_Index.Cumulative_TariffTL_MdiKw, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "MdiKvar", Get_Index.Cumulative_Tariff1_MdiKvar, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "MdiKvar", Get_Index.Cumulative_Tariff2_MdiKvar, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "MdiKvar", Get_Index.Cumulative_Tariff3_MdiKvar, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "MdiKvar", Get_Index.Cumulative_Tariff4_MdiKvar, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "MdiKvar", Get_Index.Cumulative_TariffTL_MdiKvar, 0);

                        instantController.TryGETValue_Any(Cumulative_Billing_T1_obj, "PowerFactor", Get_Index.Cumulative_Tariff1_PowerFactor, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T2_obj, "PowerFactor", Get_Index.Cumulative_Tariff2_PowerFactor, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T3_obj, "PowerFactor", Get_Index.Cumulative_Tariff3_PowerFactor, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_T4_obj, "PowerFactor", Get_Index.Cumulative_Tariff4_PowerFactor, 0);
                        instantController.TryGETValue_Any(Cumulative_Billing_Tot_obj, "PowerFactor", Get_Index.Cumulative_TariffTL_PowerFactor, 0);
                        #endregion
                    }
                }
                else
                {
                    BillingItem CurrentMDI;
                    CurrentBillingData = BillingController.GetBillingData(application_Controller.isSinglePhase);

                    if (!isDisableMonthlyDeltaMDI && !Application_Controller.isSinglePhase)
                    {
                        //BillingItem defaultBillingItem = MonthlyBillingData[0].BillingItems.Find(x => x.Name.Equals("Cumulative MDI"));
                        //if (defaultBillingItem == null)
                        //    defaultBillingItem = MonthlyBillingData[0].BillingItems.Find(x => x.Name.Equals("Active MDI"));
                        //v4.8.29 by Azeem
                        BillingItem defaultBillingItem = CurrentBillingData[0].BillingItems.Find(x => x.Name.Contains("Cumulative MDI"));
                        if (defaultBillingItem == null)
                            defaultBillingItem = CurrentBillingData[0].BillingItems.Find(x => x.Name.Contains("Active MDI"));

                        if (defaultBillingItem != null)
                        {
                            BillingItem firstItem = new BillingItem(defaultBillingItem);
                            firstItem.Name = "Last Month Delta";
                            CurrentBillingData[0].BillingItems.Add(firstItem);
                            for (int i = 0; i < CurrentBillingData.Count; i++)
                            {
                                BillingItem currentMonth = CurrentBillingData[i].BillingItems.Find(x => x.Name.Equals("Cumulative MDI"));
                                if (currentMonth != null)
                                    currentMonth.Name = "Last Month Cumulative";
                            }
                            for (int i = 1; i < CurrentBillingData.Count; i++)
                            {
                                CurrentMDI = new BillingItem();
                                CurrentMDI.Name = "Last Month Delta";

                                BillingItem currentMonth = CurrentBillingData[i].BillingItems.Find(x => x.Name.Equals("Last Month Cumulative"));
                                BillingItem PreviousMonth = CurrentBillingData[i - 1].BillingItems.Find(x => x.Name.Equals("Last Month Cumulative"));

                                if (currentMonth != null && PreviousMonth != null)
                                {
                                    CurrentMDI.Value.T1 = currentMonth.Value.T1 - PreviousMonth.Value.T1;
                                    CurrentMDI.Value.T2 = currentMonth.Value.T2 - PreviousMonth.Value.T2;
                                    CurrentMDI.Value.T3 = currentMonth.Value.T3 - PreviousMonth.Value.T3;
                                    CurrentMDI.Value.T4 = currentMonth.Value.T4 - PreviousMonth.Value.T4;
                                    CurrentMDI.Value.TL = currentMonth.Value.TL - PreviousMonth.Value.TL;

                                    CurrentBillingData[i].BillingItems.Add(CurrentMDI);
                                }
                            }
                        }

                        #region Commented Code
                        //#if Read_Display_MDI_Delta_Monthly_InstCls
                        //                        BillingItem CurrentMDI;
                        //                        BillingItem firstItem = new BillingItem(defaultBillingItem);
                        //                        firstItem.Name = "Month MDI ";
                        //                        MonthlyBillingData[0].BillingItems.Add(firstItem);

                        //                        for (int i = 1; i < MonthlyBillingData.Count; i++)
                        //                        {
                        //                            CurrentMDI = new BillingItem();
                        //                            CurrentMDI.Name = " Month MDI (kW)";

                        //                            BillingItem currentMonth = MonthlyBillingData[i].BillingItems.Find(x => x.Name.Contains("Active MDI")); //v4.8.29 Equals/Contains
                        //                            if (currentMonth == null)
                        //                                currentMonth = MonthlyBillingData[i].BillingItems.Find(x => x.Name.Contains("Cumulative MDI"));//v4.8.29 Equals/Contains

                        //                            BillingItem PreviousMonth = MonthlyBillingData[i - 1].BillingItems.Find(x => x.Name.Contains("Active MDI"));//v4.8.29 Equals/Contains
                        //                            if (PreviousMonth == null)
                        //                                PreviousMonth = MonthlyBillingData[i - 1].BillingItems.Find(x => x.Name.Contains("Cumulative MDI"));//v4.8.29 Equals/Contains

                        //                            CurrentMDI.Value.T1 = currentMonth.Value.T1 - PreviousMonth.Value.T1;
                        //                            CurrentMDI.Value.T2 = currentMonth.Value.T2 - PreviousMonth.Value.T2;
                        //                            CurrentMDI.Value.T3 = currentMonth.Value.T3 - PreviousMonth.Value.T3;
                        //                            CurrentMDI.Value.T4 = currentMonth.Value.T4 - PreviousMonth.Value.T4;
                        //                            CurrentMDI.Value.TL = currentMonth.Value.TL - PreviousMonth.Value.TL;

                        //                            //Added by Azeem Inayat
                        //                            MonthlyBillingData[i].BillingItems.Add(CurrentMDI);
                        //                        }
                        //#endif

                        //                        //.........................
                        //#if Read_Display_MDI_kvar_Delta_Monthly_InstCls

                        //                        Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKvar = Application_Controller.Param_Controller.GET_Any(Get_Index.Cumulative_Tariff1_CurrentMonthMdiKvar);
                        //                        Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKvar = Application_Controller.Param_Controller.GET_Any(Get_Index.Cumulative_Tariff2_CurrentMonthMdiKvar);
                        //                        Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKvar = Application_Controller.Param_Controller.GET_Any(Get_Index.Cumulative_Tariff3_CurrentMonthMdiKvar);
                        //                        Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKvar = Application_Controller.Param_Controller.GET_Any(Get_Index.Cumulative_Tariff4_CurrentMonthMdiKvar);
                        //                        Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKvar = Application_Controller.Param_Controller.GET_Any(Get_Index.Cumulative_TariffTL_CurrentMonthMdiKvar);

                        //                        Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKvar = Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKvar.Value / 1000;
                        //                        Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKvar = Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKvar.Value / 1000;
                        //                        Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKvar = Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKvar.Value / 1000;
                        //                        Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKvar = Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKvar.Value / 1000;
                        //                        Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKvar = Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKvar.Value / 1000;
                        //#endif
                        //                    }
                        //                    //MonthlyBillingData.Sort((x, y) => x.BillingCounter.CompareTo(y.BillingCounter)); //AHMED
                        //                    //  GetCompleted = true; 
                        #endregion

                    }
                }
                if (!Application_Controller.isSinglePhase)
                {
                    //if (!IgnoreReportRights)
                    //{
                    //Added by Azeem Inayat

                    //Get Customer Reference Code
                    Application_Controller.Param_Controller.GET_Customer_Reference(ref obj_CustomerCode);
                    //Get Active Season
                    Application_Controller.InstantaneousController.Get_Active_Season(Instantaneous_Class_obj);
                    //Get Total MDI Reset Count
                    Instantaneous_Class_obj.MDI_reset_count = Convert.ToInt32((application_Controller.InstantaneousController.GET_Any(ref Instantaneous_Class_obj, Get_Index.Billing_Period_Counter_VZ, 2, 1)));

                    byte[] array = application_Controller.InstantaneousController.GET_Byte_Array(ref Instantaneous_Class_obj, Get_Index._Last_MDI_Reset_Date_Time, 2, 1);
                    Instantaneous_Class_obj.Last_MDI_REset_Date = BasicEncodeDecode.Decode_DateTime(array);

                    if (!_readDetail && CumulativeBillingSelected)
                    {
                        if (Instantaneous_Class_obj.MDIsToShow == null)
                            Instantaneous_Class_obj.MDIsToShow = new List<QuantityMDI>();
                        Instantaneous_Class_obj.MDIsToShow.Clear();
                        if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Inst_Itm_Current_Max_kW_DateTime_Import.ToString())).Read)
                            Instantaneous_Class_obj.MDIsToShow.Add(new QuantityMDI("Current Max kW Import", Get_Index.MONTHLY_ACTIVE_MDI_IMPORT_TOTAL));
                        if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Inst_Itm_Current_Max_kW_DateTime_Export.ToString())).Read)
                            Instantaneous_Class_obj.MDIsToShow.Add(new QuantityMDI("Current Max kW Export", Get_Index.MONTHLY_ACTIVE_MDI_EXPORT_TOTAL));
                        if (_Rights.Find((x) => x.QuantityName.Equals(Billing.Inst_Itm_Current_Max_kW_DateTime_Absolute.ToString())).Read)
                            Instantaneous_Class_obj.MDIsToShow.Add(new QuantityMDI("Current Max kW", Get_Index.Cumulative_TariffTL_CurrentMonthMdiKw));

                        if (Instantaneous_Class_obj.MDIsToShow != null)
                        {
                            int kWDivider = 1000;
                            foreach (QuantityMDI mdi in Instantaneous_Class_obj.MDIsToShow)
                            {
                                DateTime timeStamp = DateTime.MinValue;

                                mdi.QuantityIndex = mdi.QuantityIndex.Set_OBISCode_Feild_E(0);
                                mdi.TariffTL.Value = application_Controller.InstantaneousController.TryGET_Any(Instantaneous_Class_obj, mdi.QuantityIndex, 0, ref timeStamp) / kWDivider; //update_Progress_bar(pb_ins, total_commands);
                                mdi.TariffTL.TimeStamp = timeStamp;

                                mdi.QuantityIndex = mdi.QuantityIndex.Set_OBISCode_Feild_E(1);
                                mdi.Tariff1.Value = application_Controller.InstantaneousController.TryGET_Any(Instantaneous_Class_obj, mdi.QuantityIndex, 0, ref timeStamp) / kWDivider; //update_Progress_bar(pb_ins, total_commands);
                                mdi.Tariff1.TimeStamp = timeStamp;

                                mdi.QuantityIndex = mdi.QuantityIndex.Set_OBISCode_Feild_E(2);
                                mdi.Tariff2.Value = application_Controller.InstantaneousController.TryGET_Any(Instantaneous_Class_obj, mdi.QuantityIndex, 0, ref timeStamp) / kWDivider; //update_Progress_bar(pb_ins, total_commands);
                                mdi.Tariff2.TimeStamp = timeStamp;

                                mdi.QuantityIndex = mdi.QuantityIndex.Set_OBISCode_Feild_E(3);
                                mdi.Tariff3.Value = 0; // application_Controller.InstantaneousController.TryGET_Any(Instantaneous_Class_obj, mdi.QuantityIndex, 0, ref timeStamp) / kWDivider; //update_Progress_bar(pb_ins, total_commands);
                                mdi.Tariff3.TimeStamp = DateTime.MinValue; //timeStamp;

                                mdi.QuantityIndex = mdi.QuantityIndex.Set_OBISCode_Feild_E(4);
                                mdi.Tariff4.Value = 0; // application_Controller.InstantaneousController.TryGET_Any(Instantaneous_Class_obj, mdi.QuantityIndex, 0, ref timeStamp) / kWDivider; //update_Progress_bar(pb_ins, total_commands);
                                mdi.Tariff4.TimeStamp = DateTime.MinValue; // timeStamp;

                                #region Add To Data Set
                                //dataSet_INS.dataTable_T.Rows.Add();
                                //dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].Quantity = mdi.QuantityName;
                                //dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T1 = mdi.Tariff1.Value.ToString();
                                //dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T2 = mdi.Tariff2.Value.ToString();
                                //dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T3 = mdi.Tariff3.Value.ToString();
                                //dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].T4 = mdi.Tariff4.Value.ToString();
                                //dataSet_INS.dataTable_T[dataSet_INS.dataTable_T.Rows.Count - 1].TL = mdi.TariffTL.Value.ToString();
                                #endregion
                            }
                        }
                    }
                    //}
                }
                //}
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
                    //pb_billing.Visible = false;

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
                        //pb_billing.Visible = false;
                        //pb_billing.Enabled = false;
                        //AccurateOptocomSoftware.LocalCommon.clsStatus.ProgBarVisible(false);
                    }
                }
            }
            catch (Exception ex)
            {
                btn_newBillingbutton.Enabled = true;
                //pb_billing.Visible = false;
                //pb_billing.Enabled = false;
                //AccurateOptocomSoftware.LocalCommon.clsStatus.ProgBarVisible(false);


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
            if (IsReadingCumulativeBilling)
            {
                if (MyDataBase.SaveToDataBase(InsertLIST, "CB", 0))
                {
                    //Notification not = new Notification("Successfull Saving", "Data saved to database", 6000); //v4.8.30
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
                        //Notification not = new Notification("Successfull Saving", "Monthly Billing data saved to database", 6000); //v4.8.30
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

        private bool getReportData_new(bool isCumulative)
        {
            DataSet_Monthly_Billing.DataTable_Monthly_Billing.Clear(); //monthly
            DataSet_Billing.DataTable_Billing.Clear(); //cumulative
            //dTable.Clear();

            if (isCumulative)
            {
                int rowIndex = 0;
                foreach (DataGridViewRow row in grid_billing.Rows)
                {
                    DataSet_Billing.DataTable_Billing.Rows.Add();
                    int CurrentRowIndex = DataSet_Billing.DataTable_Billing.Rows.Count - 1;
                    DataSet_Billing.DataTable_Billing[CurrentRowIndex].Quantity = grid_billing.Rows[rowIndex++].HeaderCell.Value.ToString();
                    DataSet_Billing.DataTable_Billing[CurrentRowIndex].T1 = row.Cells[0].Value.ToString();  //T1
                    DataSet_Billing.DataTable_Billing[CurrentRowIndex].T2 = row.Cells[1].Value.ToString(); //T2
                    DataSet_Billing.DataTable_Billing[CurrentRowIndex].T3 = row.Cells[2].Value.ToString(); //T3
                    DataSet_Billing.DataTable_Billing[CurrentRowIndex].T4 = row.Cells[3].Value.ToString(); //T4
                    DataSet_Billing.DataTable_Billing[CurrentRowIndex].TL = row.Cells[4].Value.ToString(); //TL
                }
            }
            else
            {
                try
                {
                    btn_firstRecord.Enabled = btn_NextMonth.Enabled = btn_PreviousMonth.Enabled = btn_lastRecord.Enabled = false;

                    btn_firstRecord_Click(this, new EventArgs());

                    foreach (BillingData BillPeriodData in BillingData)
                    {
                        int rowIndex = 0;
                        foreach (DataGridViewRow row in grid_billing.Rows)
                        {

                            DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Add();
                            int CurrentRowIndex = DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1;
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].Quantity = grid_billing.Rows[rowIndex++].HeaderCell.Value.ToString();
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T1 = row.Cells[0].Value.ToString();  //T1
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T2 = row.Cells[1].Value.ToString(); //T2
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T3 = row.Cells[2].Value.ToString(); //T3
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T4 = row.Cells[3].Value.ToString(); //T4
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].TL = row.Cells[4].Value.ToString(); //TL
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].MDI_Counter = BillPeriodData.BillingCounter.ToString("000"); //TL
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].DateTime = BillPeriodData.TimeStamp.ToString("dd MMM, yyyy"); //TL
                        }
                        btn_NextMonth_Click(this, new EventArgs());
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    btn_firstRecord_Click(this, new EventArgs());
                    btn_firstRecord.Enabled = btn_NextMonth.Enabled = btn_PreviousMonth.Enabled = btn_lastRecord.Enabled = true;
                }
            }

            return true;
        }

        #region getReportData (No Used from v4.8.32)
        /*
        //Used for WAPDA format
        public bool getReportData()
        {
            //TODO: Cumulative report should be by adding gridview rows in datatable instead of again formatting
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

            //billingDP = 2;

            for (int billingCount = 0; billingCount < BillingData.Count; billingCount++)
            {
                var BillPeriodData = BillingData[billingCount];// we decide which month's data to display!

                foreach (var item in BillPeriodData.BillingItems)
                {
                    BillingItem _item = (BillingItem)(item);

                    string headerTitle = (item.Unit == Unit.UnitLess) ? String.Format("{0}", item.Name) : String.Format("{0} ( {1} )", item.Name, item.Unit);
                    item.Format = "f2";
                    //for Billing report

                    DataTable dtBilling = null;

                    #region Cumulative
                    if (CumulativeBillingSelected)
                    {

                        DataSet_Billing.DataTable_Billing.Rows.Add();
                        int CurrentRowIndex = DataSet_Billing.DataTable_Billing.Rows.Count - 1;
                        DataSet_Billing.DataTable_Billing[CurrentRowIndex].Quantity = headerTitle;
                        DataSet_Billing.DataTable_Billing[CurrentRowIndex].T1 = LocalCommon.Commons.notRoundingOff(Convert_to_Valid(item.Value.T1).ToString(), billingDP);  //T1
                        DataSet_Billing.DataTable_Billing[CurrentRowIndex].T2 = LocalCommon.Commons.notRoundingOff(Convert_to_Valid(item.Value.T2).ToString(), billingDP); //T2
                        DataSet_Billing.DataTable_Billing[CurrentRowIndex].T3 = LocalCommon.Commons.notRoundingOff(Convert_to_Valid(item.Value.T3).ToString(), billingDP); //T3
                        DataSet_Billing.DataTable_Billing[CurrentRowIndex].T4 = LocalCommon.Commons.notRoundingOff(Convert_to_Valid(item.Value.T4).ToString(), billingDP); //T4
                        DataSet_Billing.DataTable_Billing[CurrentRowIndex].TL = LocalCommon.Commons.notRoundingOff(Convert_to_Valid(item.Value.TL).ToString(), billingDP); //TL



                        /////////////////////////
#if Display_MdiOccurDateTime_FromPacket_CumBill
                        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                        //v4.8.29 New MDI Occurrence DateTime Implementation by Azeem
                        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                        #region IsDateTimeDefined

                        if (item.Value.CaptureTimeStamp != null && item.Value.CaptureTimeStamp.Count == 5)
                        {
                            DataSet_Billing.DataTable_Billing.Rows.Add();
                            CurrentRowIndex = DataSet_Billing.DataTable_Billing.Rows.Count - 1;
                            DateTime dt_Actual = DateTime.MinValue;

                            //v4.8.31
                            //headerTitle = String.Format("{0} {1} {2}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString(), "DateTime");
                            headerTitle = (item.Unit == Unit.kW) ? "Current Max kW Date and Time" :
                                            String.Format("{0} {1} {2}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString(), "DateTime");

                            DataSet_Billing.DataTable_Billing[CurrentRowIndex].Quantity = headerTitle;

                            #region T1 DateTime
                            try
                            {
                                dt_Actual = DateTime.MinValue;
                                dt_Actual = (item.Value.CaptureTimeStamp[Tariff.T1_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T1_Index] : DateTime.MinValue;    //T1
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].T1 = (dt_Actual == DateTime.MinValue) ? dt_dash : String.Format(DateTimeFormat, dt_Actual);
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].T1 = String.Format(DateTimeFormat, dt_Actual); //Just for Debug
                            }
                            catch (Exception ex)
                            {
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].T1 = dt_dash;
                            }
                            #endregion
                            #region T2 DateTime
                            try
                            {
                                dt_Actual = DateTime.MinValue;
                                dt_Actual = (item.Value.CaptureTimeStamp[Tariff.T2_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T2_Index] : DateTime.MinValue;    //T2
                                //DataSet_Billing.DataTable_Billing[CurrentRowIndex].T2 = (dt_Actual == DateTime.MinValue) ? dt_dash : String.Format(DateTimeFormat, dt_Actual);
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].T2 = String.Format(DateTimeFormat, dt_Actual); //Just for Debug
                            }
                            catch (Exception ex)
                            {
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].T2 = dt_dash;
                            }
                            #endregion
                            #region T3 DateTime
                            try
                            {
                                dt_Actual = DateTime.MinValue;
                                dt_Actual = (item.Value.CaptureTimeStamp[Tariff.T3_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T3_Index] : DateTime.MinValue;    //T3
                                //DataSet_Billing.DataTable_Billing[CurrentRowIndex].T3 = (dt_Actual == DateTime.MinValue) ? dt_dash : String.Format(DateTimeFormat, dt_Actual);
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].T3 = String.Format(DateTimeFormat, dt_Actual); //Just for Debug
                            }
                            catch (Exception ex)
                            {
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].T3 = dt_dash;
                            }
                            #endregion
                            #region T4 DateTime
                            try
                            {
                                dt_Actual = DateTime.MinValue;
                                dt_Actual = (item.Value.CaptureTimeStamp[Tariff.T4_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T4_Index] : DateTime.MinValue;    //T4
                                //DataSet_Billing.DataTable_Billing[CurrentRowIndex].T4 = (dt_Actual == DateTime.MinValue) ? dt_dash : String.Format(DateTimeFormat, dt_Actual);
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].T4 = String.Format(DateTimeFormat, dt_Actual); //Just for Debug
                            }
                            catch (Exception ex)
                            {
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].T4 = dt_dash;
                            }
                            #endregion
                            #region TL DateTime
                            try
                            {
                                dt_Actual = DateTime.MinValue;
                                dt_Actual = (item.Value.CaptureTimeStamp[Tariff.TL_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.TL_Index] : DateTime.MinValue;   //TL 
                                //DataSet_Billing.DataTable_Billing[CurrentRowIndex].TL = (dt_Actual == DateTime.MinValue) ? dt_dash : String.Format(DateTimeFormat, dt_Actual);
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].TL = String.Format(DateTimeFormat, dt_Actual); //Just for Debug
                            }
                            catch (Exception ex)
                            {
                                DataSet_Billing.DataTable_Billing[CurrentRowIndex].TL = dt_dash;
                            }
                            #endregion
                        }
                        #endregion
#endif
                        ///////////////////////////////
                    }
                    #endregion
                    //for monthly billing report

                    #region Monthly Billing
                    else
                    {
                        DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Add();
                        int CurrentRowIndex = DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1;
                        DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].DateTime = BillingData[billingCount].TimeStamp.ToString("dd MMM, yyyy");
                        DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].MDI_Counter = BillingData[billingCount].BillingCounter.ToString("000");
                        DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].Quantity = headerTitle;
                        DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T1 = LocalCommon.Commons.notRoundingOff((Convert_to_Valid(item.Value.T1)).ToString(), billingDP);  //T1
                        DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T2 = LocalCommon.Commons.notRoundingOff((Convert_to_Valid(item.Value.T2)).ToString(), billingDP); //T2
                        DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T3 = LocalCommon.Commons.notRoundingOff((Convert_to_Valid(item.Value.T3)).ToString(), billingDP); //T3
                        DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T4 = LocalCommon.Commons.notRoundingOff((Convert_to_Valid(item.Value.T4)).ToString(), billingDP); //T4
                        DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].TL = LocalCommon.Commons.notRoundingOff((Convert_to_Valid(item.Value.TL)).ToString(), billingDP); //TL
                        ////////////////
#if Display_MdiOccurDateTime_FromPacket_MonthlyBill
                        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                        //v4.8.29 New MDI Occurrence DateTime Implementation by Azeem
                        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                        #region IsDateTimeDefined

                        if (item.Value.CaptureTimeStamp != null && item.Value.CaptureTimeStamp.Count == 5)
                        {
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Add();
                            CurrentRowIndex = DataSet_Monthly_Billing.DataTable_Monthly_Billing.Rows.Count - 1;
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].DateTime = BillingData[billingCount].TimeStamp.ToString("dd MMM, yyyy");
                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].MDI_Counter = BillingData[billingCount].BillingCounter.ToString("000");

                            DateTime dt_Actual = DateTime.MinValue;
                            //v4.8.31
                            //headerTitle = String.Format("{0} {1} {2}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString(), "DateTime");
                            headerTitle = (item.Unit == Unit.kW) ? "Max kW Date and Time" :
                                            String.Format("{0} {1} {2}", item.Name, (item.Unit == Unit.UnitLess) ? "" : item.Unit.ToString(), "DateTime");
                            headerTitle = headerTitle.Replace(" MDI", "");

                            if (item.Unit == Unit.kW) headerTitle = "Max kW Date and Time"; //v4.8.31

                            DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].Quantity = headerTitle;

                            #region T1 DateTime
                            try
                            {
                                dt_Actual = DateTime.MinValue;
                                dt_Actual = (item.Value.CaptureTimeStamp[Tariff.T1_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T1_Index] : DateTime.MinValue;    //T1
                                DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T1 = (dt_Actual == DateTime.MinValue) ? dt_dash : String.Format(DateTimeFormat, dt_Actual);
                            }
                            catch (Exception ex)
                            {
                                DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T1 = dt_dash;
                            }
                            #endregion
                            #region T2 DateTime
                            try
                            {
                                dt_Actual = DateTime.MinValue;
                                dt_Actual = (item.Value.CaptureTimeStamp[Tariff.T2_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T2_Index] : DateTime.MinValue;    //T2
                                DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T2 = (dt_Actual == DateTime.MinValue) ? dt_dash : String.Format(DateTimeFormat, dt_Actual);
                            }
                            catch (Exception ex)
                            {
                                DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T2 = dt_dash;
                            }
                            #endregion
                            #region T3 DateTime
                            try
                            {
                                dt_Actual = DateTime.MinValue;
                                dt_Actual = (item.Value.CaptureTimeStamp[Tariff.T3_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T3_Index] : DateTime.MinValue;    //T3
                                DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T3 = (dt_Actual == DateTime.MinValue) ? dt_dash : String.Format(DateTimeFormat, dt_Actual);
                            }
                            catch (Exception ex)
                            {
                                DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T3 = dt_dash;
                            }
                            #endregion
                            #region T4 DateTime
                            try
                            {
                                dt_Actual = DateTime.MinValue;
                                dt_Actual = (item.Value.CaptureTimeStamp[Tariff.T4_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.T4_Index] : DateTime.MinValue;    //T4
                                DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T4 = (dt_Actual == DateTime.MinValue) ? dt_dash : String.Format(DateTimeFormat, dt_Actual);
                            }
                            catch (Exception ex)
                            {
                                DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].T4 = dt_dash;
                            }
                            #endregion
                            #region TL DateTime
                            try
                            {
                                dt_Actual = DateTime.MinValue;
                                dt_Actual = (item.Value.CaptureTimeStamp[Tariff.TL_Index] != null) ? item.Value.CaptureTimeStamp[Tariff.TL_Index] : DateTime.MinValue;   //TL 
                                DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].TL = (dt_Actual == DateTime.MinValue) ? dt_dash : String.Format(DateTimeFormat, dt_Actual);
                            }
                            catch (Exception ex)
                            {
                                DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].TL = dt_dash;
                            }
                            #endregion
                        }
                        #endregion;
#endif
                        ////////////////
                        //DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].DateTime = BillingData[billingCount].TimeStamp.ToString("dd MMM, yyyy");
                        //DataSet_Monthly_Billing.DataTable_Monthly_Billing[CurrentRowIndex].MDI_Counter = BillingData[billingCount].BillingCounter.ToString("000");
                    }
                    #endregion Monthly Billing
                    //}
                } //Foreach End

                if (CumulativeBillingSelected) //currentMonthMDIKW from instantaneous Packet
                {

#if Read_Display_CumMDI_CumBill_InstCls

                    DataSet_Billing.DataTable_Billing.Rows.Add();
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = "Current Month MDI Kw";
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw.ToString(), billingDP);  //T1
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKw.ToString(), billingDP); //T2
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKw.ToString(), billingDP); //T3
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKw.ToString(), billingDP); //T4
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKw.ToString(), billingDP); //TL
#endif  //v4.8.25

#if Read_Display_CumMdiOccurDateTime_CumBill_InstCls
                    //string dt = "--/--/----";
                    //string DateTimeFormat = "{0:dd/MM/yyyy HH:mm}";
                    //DateTime OcrDt = new DateTime();

                    DataSet_Billing.DataTable_Billing.Rows.Add();
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = "Current Max kW Date and Time";

                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 =
                        ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 =
                        ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 =
                        ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 =
                        ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());

                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL =
                        ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime());
#endif  //v4.8.25

                }
                else
                {
#if Read_Display_MDI_Delta_Monthly_InstCls

                    DataSet_Billing.DataTable_Billing.Rows.Add();
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = "Current Month MDI Kw";
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = 
                        LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKvar.ToString(), billingDP);  //T1
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = 
                        LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKvar.ToString(), billingDP); //T2
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = 
                        LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKvar.ToString(), billingDP); //T3
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = 
                        LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKvar.ToString(), billingDP); //T4
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = 
                        LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKvar.ToString(), billingDP); //TL
#endif

#if Read_Display_MdiOccurDateTime_Delta_Monthly_InstCls
                    string dt = "--/--/----";
                    //string DateTimeFormat = "{0:dd/MM/yyyy HH:mm}";
                    DateTime OcrDt = new DateTime();

                    DataSet_Billing.DataTable_Billing.Rows.Add();
                    DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].Quantity = "Current Max kW DateTime";

                    OcrDt = Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKvar.Date_Time_Stamp.GetDateTime();
                    if (OcrDt.Day == 1 && OcrDt.Month == 1 && OcrDt.Year == 2000)
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = dt;
                    else
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T1 = String.Format(DateTimeFormat, OcrDt);

                    OcrDt = Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKvar.Date_Time_Stamp.GetDateTime();
                    if (OcrDt.Day == 1 && OcrDt.Month == 1 && OcrDt.Year == 2000)
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = dt;
                    else
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T2 = String.Format(DateTimeFormat, OcrDt);

                    OcrDt = Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKvar.Date_Time_Stamp.GetDateTime();
                    if (OcrDt.Day == 1 && OcrDt.Month == 1 && OcrDt.Year == 2000)
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = dt;
                    else
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T3 = String.Format(DateTimeFormat, OcrDt);

                    OcrDt = Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKvar.Date_Time_Stamp.GetDateTime();
                    if (OcrDt.Day == 1 && OcrDt.Month == 1 && OcrDt.Year == 2000)
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = dt;
                    else
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].T4 = String.Format(DateTimeFormat, OcrDt);

                    OcrDt = Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKvar.Date_Time_Stamp.GetDateTime();
                    if (OcrDt.Day == 1 && OcrDt.Month == 1 && OcrDt.Year == 2000)
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = dt;
                    else
                        DataSet_Billing.DataTable_Billing[DataSet_Billing.DataTable_Billing.Rows.Count - 1].TL = String.Format(DateTimeFormat, OcrDt);

#endif

                }

            }
            return true;
        }
        */
        #endregion

        //Used for WEB format
        public bool getReportData(DataTable dTable)
        {
            dTable.Clear(); //cumulative
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

            //billingDP = 2;

            for (int billingCount = 0; billingCount < BillingData.Count; billingCount++)
            {
                var BillPeriodData = BillingData[billingCount];// we decide which month's data to display!

                foreach (var item in BillPeriodData.BillingItems)
                {
                    BillingItem _item = (BillingItem)(item);

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
                    if (CumulativeBillingSelected)
                    {
                        dTable.Rows.Add(
                            headerTitle,
                            LocalCommon.notRoundingOff(Convert_to_Valid(item.Value.T1).ToString(), billingDP),
                            LocalCommon.notRoundingOff(Convert_to_Valid(item.Value.T2).ToString(), billingDP),
                            LocalCommon.notRoundingOff(Convert_to_Valid(item.Value.T3).ToString(), billingDP),
                            LocalCommon.notRoundingOff(Convert_to_Valid(item.Value.T4).ToString(), billingDP),
                            LocalCommon.notRoundingOff(Convert_to_Valid(item.Value.TL).ToString(), billingDP)
                            );
                    }
                    //for monthly billing report
                    else
                    {
                        dTable.Rows.Add(
                            headerTitle,
                            LocalCommon.notRoundingOff((Convert_to_Valid(item.Value.T1)).ToString(), billingDP),  //T1
                            LocalCommon.notRoundingOff((Convert_to_Valid(item.Value.T2)).ToString(), billingDP), //T2
                            LocalCommon.notRoundingOff((Convert_to_Valid(item.Value.T3)).ToString(), billingDP), //T3
                            LocalCommon.notRoundingOff((Convert_to_Valid(item.Value.T4)).ToString(), billingDP), //T4
                            LocalCommon.notRoundingOff((Convert_to_Valid(item.Value.TL)).ToString(), billingDP), //TL
                            BillingData[billingCount].TimeStamp.ToString("dd MMM, yyyy"),
                            BillingData[billingCount].BillingCounter.ToString("000")
                        );
                    }
                }

                if (CumulativeBillingSelected) //currentMonthMDIKW from instantaneous Packet
                {

#if Read_Display_CumMDI_CumBill_InstCls

                    dTable.Rows.Add(
                            "Current Month MDI Kw",
                            LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff1_CurrentMonthMdiKw.ToString(), billingDP),  //T1
                            LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff2_CurrentMonthMdiKw.ToString(), billingDP), //T2
                            LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff3_CurrentMonthMdiKw.ToString(), billingDP), //T3
                            LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_Tariff4_CurrentMonthMdiKw.ToString(), billingDP), //T4
                            LocalCommon.Commons.notRoundingOff(Instantaneous_Class_obj.RPT_Cumulative_TariffTL_CurrentMonthMdiKw.ToString(), billingDP) //TL
                            );

#endif  //v4.8.25

#if Read_Display_CumMdiOccurDateTime_CumBill_InstCls

                    string OcrDt1 = "", OcrDt2 = "", OcrDt3 = "", OcrDt4 = "", OcrDtTL = "";

                    OcrDt1 = ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff1_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime(), Instantaneous_Class_obj.Last_MDI_REset_Date);
                    OcrDt2 = ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff2_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime(), Instantaneous_Class_obj.Last_MDI_REset_Date);
                    OcrDt3 = ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff3_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime(), Instantaneous_Class_obj.Last_MDI_REset_Date);
                    OcrDt4 = ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_Tariff4_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime(), Instantaneous_Class_obj.Last_MDI_REset_Date);
                    OcrDtTL = ParseMdiDateTime(Instantaneous_Class_obj.Cumulative_TariffTL_CurrentMonthMdiKw.Date_Time_Stamp.GetDateTime(), Instantaneous_Class_obj.Last_MDI_REset_Date);

                    dTable.Rows.Add(
                    "Current Max kW Date and Time",
                    OcrDt1,
                    OcrDt2,
                    OcrDt3,
                    OcrDt4,
                    OcrDtTL
                    );

#endif //v4.8.25


                }

            }
            return true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbBillingTypes.SelectedIndex == 0 || tbBillingTypes.SelectedIndex == -1)
            {
                rdbCumulativeBilling.Checked = true;
                // rdbMonthlyBilling.Checked = false;
                GUI_CurrentBilling();
                //if (grid_billing.Rows.Count > 1)
                //    btn_comm_verification_rpt.Visible = true;
                chkDetail.Visible = true;
                Label_lblDayResetCount.Visible = Label_lblDayResetDate.Visible =
                lbl_day_count.Visible = lbl_day_reset_date.Visible = false;
            }
            else if (tbBillingTypes.SelectedIndex == 1)
            {
                // rdbCumulativeBilling.Checked = false;
                rdbMonthlyBilling.Checked = true;
                btn_comm_verification_rpt.Visible = false;
                chkDetail.Visible = false;
                Label_lblDayResetCount.Visible = Label_lblDayResetDate.Visible =
                    lbl_day_count.Visible = lbl_day_reset_date.Visible = false;
                GUI_MonthlyBilling();

                AddFilterValue();
            }
            else if (tbBillingTypes.SelectedIndex == 2)
            {
                // rdbCumulativeBilling.Checked = false;
                rdbDailyMonthlyBilling.Checked = true;
                btn_comm_verification_rpt.Visible = false;
                chkDetail.Visible = false;
                Label_lblDayResetCount.Visible = Label_lblDayResetDate.Visible =
                   lbl_day_count.Visible = lbl_day_reset_date.Visible = true;
                GUI_MonthlyBilling();

                AddFilterValue();
            }

            // Read Operation In Continue
            if (Application_Controller != null &&
                Application_Controller.IsIOBusy)
            {
                Notification notifier = new Notification("Continued Read Opeartion", "Unable to update Billing Data,a data operation in continue");
                return;
            }

            UpdateBilling();
        }

        private void btn_BillingReport_Click(object sender, EventArgs e)
        {
            if (grid_billing.Rows.Count > 0)  //v4.8.31
            {
                MeterConfig meter_type_info = application_Controller.ConnectionManager.ConnectionInfo.MeterInfo;

                if (LocalCommon.GetReportFormat(application_Controller.CurrentUser.CurrentAccessRights) == ReportFormat.WAPDA_DDS)
                {
                    if (!getReportData_new(CumulativeBillingSelected))
                        return;

                    //if (!getReportData())
                    //{
                    //    return;
                    //}

                    int billCount = BillingData.Count;
                    if (CumulativeBillingSelected)
                    {
                        string Title_Billing = "Cumulative Read Report";
                        string DateTime_Billing = BillingData[0].TimeStamp.ToString("dd:MM:yyyy  HH:mm:ss");

                        ReportViewer viewer_Billing = new ReportViewer(Title_Billing, DataSet_Billing, BillingController.CurrentConnectionInfo.MSN,
                            Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.MeterModel, DateTime_Billing, obj_CustomerCode.Customer_Code_String,
                            Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(),
                            Instantaneous_Class_obj.MDI_reset_count.ToString(), Instantaneous_Class_obj.Last_MDI_REset_Date, meter_type_info,
                            application_Controller.CurrentUser.CurrentAccessRights); //,
                        //((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));
                        viewer_Billing.Show();
                    }
                    else //if (rdbMonthlyBilling.Checked)
                    {
                        string Title_Monthly_Billing = "Monthly Read Report";
                        ReportViewer monthly_Billing = new ReportViewer(Title_Monthly_Billing, DataSet_Monthly_Billing, BillingController.CurrentConnectionInfo.MSN,
                            Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.MeterModel, obj_CustomerCode.Customer_Code_String,
                            Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(), meter_type_info,
                            application_Controller.CurrentUser.CurrentAccessRights); //,
                        //((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));
                        monthly_Billing.Show();
                    }
                }
                else //if (IsWebFormat)
                {
                    if (CumulativeBillingSelected)
                    {
                        if (!getReportData(DataSet_Billing.DataTable_Billing_w))
                        {
                            return;
                        }

                        string Title_Billing = "Cumulative Read Report";
                        string DateTime_Billing = BillingData[0].TimeStamp.ToString("dd:MM:yyyy  HH:mm:ss");

                        ReportViewer viewer_Billing = new ReportViewer(Title_Billing, DataSet_Billing, BillingController.CurrentConnectionInfo.MSN,
                            Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.MeterModel, DateTime_Billing, obj_CustomerCode.Customer_Code_String,
                            Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(),
                            Instantaneous_Class_obj.MDI_reset_count.ToString(), Instantaneous_Class_obj.Last_MDI_REset_Date,
                            meter_type_info, application_Controller.CurrentUser.CurrentAccessRights); //,
                        //((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));
                        viewer_Billing.Show();
                    }
                    else
                    {
                        if (!getReportData(DataSet_Monthly_Billing.DataTable_Monthly_Billing))
                        {
                            return;
                        }
                        string Title_Monthly_Billing = "Monthly Read Report";
                        ReportViewer monthly_Billing = new ReportViewer(Title_Monthly_Billing, DataSet_Monthly_Billing, BillingController.CurrentConnectionInfo.MSN,
                            Application_Controller.ConnectionManager.ConnectionInfo.MeterInfo.MeterModel, obj_CustomerCode.Customer_Code_String,
                            Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(),
                            meter_type_info, application_Controller.CurrentUser.CurrentAccessRights); //,
                        //((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));
                        monthly_Billing.Show();
                    }
                }
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

                //IsWapdaFormat = true; //v4.8.31 true by default
                //IsWebFormat = false;

                // Three Lines Added by sajid for default behaviour without rights
                this.tbBillingTypes.TabPages.Clear();
                this.tbBillingTypes.Visible = false;
                this.chkDetail.Visible = false;
                this.btn_newBillingbutton.Enabled = false;

                #region Web Format Report Rights
                try
                {
                    var _OtherRights = Application_Controller.CurrentUser.CurrentAccessRights.OtherRights;
                    //var right_IsWebFormat = _OtherRights.Find((x) => x.QuantityName.Contains(OtherRights.IsWebFormat.ToString()));
                    //if (right_IsWebFormat != null && (right_IsWebFormat.Read)) IsWebFormat = true;
                }
                catch (Exception) { }
                #endregion

                #region Wapda Format Rights
                try
                {
                    var _GenRights = Application_Controller.CurrentUser.CurrentAccessRights.GeneralRights;
                    try
                    {
                        //var right_IsWapdaFormat = _GenRights.Find((x) => x.QuantityName.Contains(GeneralRights.Billing.ToString()));
                        //if (right_IsWapdaFormat != null && right_IsWapdaFormat.Write) IsWapdaFormat = false; //v4.8.31
                    }
                    catch (Exception) { }
                }
                catch (Exception) { }
                #endregion
                //if (IsWapdaFormat && IsWebFormat) IsWebFormat = false;  //v4.8.31 WapdaFormat important than Web Format
            }
            catch (Exception) { }
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
            MeterConfig meter_type_info = application_Controller.ConnectionController.SelectedMeter;
            ReportViewer rpt = new ReportViewer(kwh, kvarh, BillingController.CurrentConnectionInfo.MSN, meter_type_info.MeterModel, meter_type_info);
            rpt.Show();
        }
        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    //// Getting the graphics object
        //    //Graphics g = pevent.Graphics;
        //    //// Creating the rectangle for the gradient
        //    //Rectangle rBackground = new Rectangle(0, 0, this.Width, this.Height);

        //    //// Creating the lineargradient
        //    //System.Drawing.Drawing2D.LinearGradientBrush bBackground
        //    //    = new System.Drawing.Drawing2D.LinearGradientBrush(rBackground, Commons.BgColor1, Commons.BgColor2, Commons.BgColorAngle);
        //    //// Draw the gradient onto the form
        //    //g.FillRectangle(bBackground, rBackground);

        //    //// Disposing of the resources held by the brush
        //    //bBackground.Dispose();
        //}

        #region AccessControlMethods

        public bool ApplyAccessRights(ApplicationRight Rights)
        {
            try
            {
                this.SuspendLayout();
                this._Rights = Rights.BillingRights;
                if (Rights.BillingRights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights.BillingRights)
                    {
                        _HelperAccessRights((Billing)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    if (tbBillingTypes.TabPages.Count > 0)
                    {
                        this.btn_newBillingbutton.Enabled = true;
                        tbBillingTypes.Visible = true;
                        tbBillingTypes.SelectedIndex = 0;
                    }

                    return true;
                }
                return false;
            }
            finally
            {
                HidePrintReportButtons = false;
                if (Rights.GeneralRights.Find(x => x.QuantityName == GeneralRights.IgnoreReports.ToString() && x.Read == true) != null)
                {
                    HidePrintReportButtons = true;
                    this.btn_BillingReport.Visible = false;
                    this.btn_comm_verification_rpt.Visible = false;
                }
                else
                {
                    this.btn_BillingReport.Visible = true;
                    //this.btn_comm_verification_rpt.Visible = true;
                }
                this.ResumeLayout();
            }
        }

        private void _HelperAccessRights(Billing qty, bool read, bool write)
        {
            switch (qty)
            {
                case Billing.CummulativeBilling:
                    if (read)
                    {
                        if (!tbBillingTypes.TabPages.Contains(tpCumulative))
                            tbBillingTypes.TabPages.Add(tpCumulative);
                    }
                    break;
                case Billing.MonthlyBilling:
                    if (read)
                    {
                        if (!tbBillingTypes.TabPages.Contains(tpMonthly))
                            tbBillingTypes.TabPages.Add(tpMonthly);
                    }
                    break;
                case Billing.DailyEnergy_Profile:
                    if (read)
                    {
                        if (!tbBillingTypes.TabPages.Contains(tpDaily))
                            tbBillingTypes.TabPages.Add(tpDaily);
                    }
                    break;
                case Billing.Detail_Cumulative_Billing:
                    if (read)
                    {
                        this.chkDetail.Visible = read;
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
