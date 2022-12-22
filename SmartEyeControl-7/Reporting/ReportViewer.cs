using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
//using SmartEyeControl_7.Reporting.WapdaFormat;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SharedCode.Comm.HelperClasses;
using SEAC.Common;
using SmartEyeControl_7.Reporting.WapdaFormat;
using OptocomSoftware.Reporting;
using OptocomSoftware.Reporting.WapdaFormat;

namespace SmartEyeControl_7.Reporting
{
    public partial class ReportViewer : Form
    {
        #region WapdaReportConstants
        ReportFormat rptFormat = ReportFormat.WAPDA_DDS;

        //bool IsWapdaFormat = true;
        //bool IsWebFormat = false;

        string LoadProfile_MainTitle = "Billing Report",
            CumBilling_MainTitle = "Billing Report",
            MonthlyBilling_MainTitle = "Billing Report",
            Parameterization_MainTitle = "Programming Report",
            EventIndividual_MainTitle = "Event Log Report",
            EventLogBook_MainTitle = "Event Log Report",
            Instantaneous_MainTitle = "Instantaneous Report",
            Instantaneous_new_MainTitle = "Instant Read Report",
            SecurityData_MainTitle = "Security Report",
            DisplayWindow_MainTitle = "LIST OF DISPLAY ITEMS IN TOD/TOU METER",

            LoadProfile_SubTitle = "(Load Profile)",
            CumBilling_SubTitle = "(Cumulative)",
            MonthlyBilling_SubTitle = "(Monthly)",
            Parameterization_SubTitle = "",
            EventIndividual_SubTitle = "",
            EventLogBook_SubTitle = "(Log Book)",
            Instantaneous_SubTitle = "",
            Instantaneous_new_SubTitle = "",
            SecurityData_SubTitle = "",
            DisplayWinNor_SubTitle = "NORMAL MODE",
            DisplayWinAlt_SubTitle = "ALTERNATE MODE",
            DisplayWinTest_SubTitle = "TEST MODE";
        #endregion

        #region Report Constants
        string txt_customerCode = "txt_customerCode",
                txt_customerName = "txt_customerName",
                txt_customerAddress = "txt_customerAddress", 
                meter_Model = "meter_Model",
                meter_Model_lbl = "meter_Model_lbl",
                meter_type = "meter_type",
                meter_type_lbl = "meter_type_lbl",
                MeterCompanyTypeCode = "MeterCompanyTypeCode",
                MeterManufacturedBy = "MeterManufacturedBy",
                Msn = "MSN",
                txt_Msn = "txt_MSN",
                txt_pid = "txt_pid",
                txt_season = "txt_season",
                txt_date = "txt_date",
                txt_time = "txt_time",
                RPT_headerForAll = "RPT_headerForAll.rpt",
                RPT_headerForAll_w = "RPT_headerForAll_w.rpt",
                RPT_headerForParam_w = "RPT_headerForParam_w.rpt";
        #endregion

        //v4.8.23 added by M.Azeem Inayat
        private void Init_ReportViewer(ApplicationRight _currentAccessRights, AccessRights _rptType)
        {
            InitializeComponent();

            //try
            //{
            //    var right_genRights = _currentAccessRights.GeneralRights.Find((x) => x == _rptType);
            //    IsWapdaFormat = (right_genRights == null || right_genRights.Write) ? false : true; //v4.8.31
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            rptFormat = LocalCommon.GetReportFormat(_currentAccessRights);
            //try
            //{
            //    var _OtherRights = _currentAccessRights.OtherRights.Find((x) => x.QuantityName.Contains(OtherRights.IsWebFormat.ToString()));

            //    if (!_OtherRights.Read)
            //    {
            //        rptFormat = ReportFormat.WAPDA_DDS;
            //    }
            //    else if (_OtherRights.Read && _OtherRights.Write)
            //    {
            //        rptFormat = ReportFormat.ADVANCE_MTI;
            //    }
            //    else
            //    {
            //        rptFormat = ReportFormat.WEB_GALAXY;
            //    }
            //    //IsWebFormat = (_OtherRights != null && _OtherRights.Read) ? true : false;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        #region Fill Report Header
        private bool GetReportHeader(ReportDocument doc, string MSN, string customerCode, string custName, string custAddress, string pid, string active_season, MeterConfig meter_type_info, bool isFusionParam)
        {
            try
            {
                if (isFusionParam)
                {
                    CrystalDecisions.CrystalReports.Engine.TextObject txt_field;

                    //Customer Name
                    txt_field = doc.ReportDefinition.ReportObjects[txt_customerName] as TextObject;
                    if(custName != null)
                        txt_field.Text = custName;
                    else
                        txt_field.Text = "";
                    //Customer Address
                    txt_field = doc.ReportDefinition.ReportObjects[txt_customerAddress] as TextObject;
                    if(custAddress != null)
                        txt_field.Text = custAddress;
                    else
                        txt_field.Text = "";

                    GetReportHeader(doc, MSN, customerCode, pid, active_season, meter_type_info);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Header containing meter information for All Reports
        private bool GetReportHeader(ReportDocument doc, string MSN, string customerCode, string pid, string active_season, MeterConfig meter_type_info)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.TextObject txt_field;

                if (meter_type_info != null)
                {
                    //Manufacturer
                    txt_field = doc.ReportDefinition.ReportObjects[MeterManufacturedBy] as TextObject;
                    txt_field.Text = meter_type_info.Manufacturer.ManufacturerName;
                }

                //MSN
                string _msn = MSN.Substring(4);
                txt_field = doc.ReportDefinition.ReportObjects[Msn] as TextObject;
                txt_field.Text = _msn;

                //Meter Type Code 
                string _meterTypeCode = MSN.Substring(0, 4);
                txt_field = doc.ReportDefinition.ReportObjects[MeterCompanyTypeCode] as TextObject;
                txt_field.Text = _meterTypeCode;

                //TO:DO Should be Load from Database as in v4.8.32
                if (MSN.ToString().StartsWith("3698"))
                {
                    txt_field = doc.ReportDefinition.ReportObjects[meter_Model] as TextObject;
                    txt_field.Text = "R326";

                    txt_field = doc.ReportDefinition.ReportObjects[meter_type] as TextObject;
                    txt_field.Text = "3-Phase Whole Current TOD/TOU Smart Energy Meter";
                }
                else if (MSN.ToString().StartsWith("3699"))
                {
                    txt_field = doc.ReportDefinition.ReportObjects[meter_Model] as TextObject;
                    txt_field.Text = "R421";

                    txt_field = doc.ReportDefinition.ReportObjects[meter_type] as TextObject;
                    txt_field.Text = "3-Phase LT CT Operated TOD/TOU Smart Energy Meter";
                }

                //Meter Model / Type
                //txt_field = doc.ReportDefinition.ReportObjects[meter_Model] as TextObject;
                //string metermodel = meter_type_info.MeterModel; //v4.8.39
                //if (metermodel.Length > 0)
                //    txt_field.Text = metermodel;
                //else
                //{
                //    txt_field.Text = "";
                //    txt_field = doc.ReportDefinition.ReportObjects[meter_Model_lbl] as TextObject;
                //    txt_field.Text = "";
                //}

                ////Meter Type
                //string _meterTypeStr = (MSN.ToString().StartsWith("3698")) ?  //4.8.39 //TO:DO Should be Load from Database as in v4.8.32
                //    "3-Phase Whole Current TOD/TOU Smart Energy Meter" :
                //    "3-Phase LT CT Operated TOD/TOU Smart Energy Meter";
                //txt_field = doc.ReportDefinition.ReportObjects[meter_type] as TextObject;
                //txt_field.Text = _meterTypeStr;
                //if (meter_type_info.MeterType.Trim().Length > 0)
                //    txt_field.Text = meter_type_info.MeterType;
                //else
                //{
                //    txt_field.Text = "";
                //    txt_field = doc.ReportDefinition.ReportObjects[meter_type_lbl] as TextObject;
                //    txt_field.Text = "";
                //}

                //Customer Code
                txt_field = doc.ReportDefinition.ReportObjects[txt_customerCode] as TextObject;
                txt_field.Text = customerCode;

                //Programmers ID
                txt_field = doc.ReportDefinition.ReportObjects[txt_pid] as TextObject;
                txt_field.Text = pid;

                //Active Season
                txt_field = doc.ReportDefinition.ReportObjects[txt_season] as TextObject;
                txt_field.Text = active_season;

                //Current Date
                txt_field = doc.ReportDefinition.ReportObjects[txt_date] as TextObject;
                txt_field.Text = DateTime.Now.ToString("dd MMM, yyyy");

                //Current Time
                txt_field = doc.ReportDefinition.ReportObjects[txt_time] as TextObject;
                txt_field.Text = DateTime.Now.ToString("HH:mm:ss");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Header containing meter information for All Reports (According to DDS-50 and DDS-60)
        private bool GetReportHeader_w(ReportDocument doc, string MSN, string customerCode, string pid, string active_season, MeterConfig meter_type_info, string txt_RPT_MainTitle, string txt_RPT_SubTitle, bool _Msn_on_Each_page)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.TextObject txt_field;

                //Report's Main Title (According to WAPDA Specs)
                txt_field = doc.Subreports[RPT_headerForAll_w].ReportDefinition.ReportObjects["txt_RPT_MainTitle"] as TextObject;
                txt_field.Text = txt_RPT_MainTitle;

                //Report's Sub Title (MTI Internal)
                txt_field = doc.Subreports[RPT_headerForAll_w].ReportDefinition.ReportObjects["txt_RPT_SubTitle"] as TextObject;
                txt_field.Text = txt_RPT_SubTitle;

                if (_Msn_on_Each_page)  //v4.8.25
                {
                    //txt_MSN
                    try
                    {
                        txt_field = doc.ReportDefinition.ReportObjects[txt_Msn] as TextObject;
                        txt_field.Text = MSN.Substring(0, 4) + " " + MSN.Substring(4);
                    }
                    catch (Exception)
                    {
                        if (txt_field.Name.Contains(txt_Msn))
                            txt_field.Text = MSN.Substring(4);
                    }
                }

                bool result = false;

                if (GetReportHeader(doc.Subreports[RPT_headerForAll_w], MSN, customerCode, pid, active_season, meter_type_info))
                    result = true;
                else
                    result = false;
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        
        //Header containing meter information for All Reports (According to DDS-50 and DDS-60)
        private bool GetReportHeader_w(ReportDocument doc, string MSN, string customerCode, string custName, string CustAddress, string pid, string active_season, MeterConfig meter_type_info, string txt_RPT_MainTitle, string txt_RPT_SubTitle, bool _Msn_on_Each_page)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.TextObject txt_field;

                //Report's Main Title (According to WAPDA Specs)
                txt_field = doc.Subreports[RPT_headerForParam_w].ReportDefinition.ReportObjects["txt_RPT_MainTitle"] as TextObject;
                txt_field.Text = txt_RPT_MainTitle;

                //Report's Sub Title (MTI Internal)
                txt_field = doc.Subreports[RPT_headerForParam_w].ReportDefinition.ReportObjects["txt_RPT_SubTitle"] as TextObject;
                txt_field.Text = txt_RPT_SubTitle;

                if (_Msn_on_Each_page)  //v4.8.25
                {
                    //txt_MSN
                    try
                    {
                        txt_field = doc.ReportDefinition.ReportObjects[txt_Msn] as TextObject;
                        txt_field.Text = MSN.Substring(0, 4) + " " + MSN.Substring(4);
                    }
                    catch (Exception)
                    {
                        if (txt_field.Name.Contains(txt_Msn))
                            txt_field.Text = MSN.Substring(4);
                    }
                }

                bool result = false;

                if (GetReportHeader(doc.Subreports[RPT_headerForParam_w], MSN, customerCode, custName, CustAddress, pid, active_season, meter_type_info, true))
                    result = true;
                else
                    result = false;
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion

        #region Load Profile

        //for load profile
        public ReportViewer(ds_LoadProfile tempDS_loadProfile, string MSN, string Meter_Model, string[] Quantities, string meter_DateTime, string customerCode, ushort pid, string active_season, MeterConfig meter_type_info, ApplicationRight _currentAccessRights, bool IsPerDay) //, ReportFormat rptFormat)
        {
            Init_ReportViewer(_currentAccessRights, _currentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.LoadProfile))));
            CrystalDecisions.CrystalReports.Engine.TextObject txtBillingReportHeader;

            //RPT_LoadProfile loadProfile_Rpt = new RPT_LoadProfile();

            if (rptFormat == ReportFormat.WEB_GALAXY)
            {
                RPT_LoadProfile_WebFormat loadProfile_Rpt = new RPT_LoadProfile_WebFormat();
                loadProfile_Rpt.Database.Tables[0].SetDataSource(tempDS_loadProfile.Tables[0]);

                //CrystalDecisions.CrystalReports.Engine.TextObject txtBillingReportHeader;

                //Added by Azeem Inayat
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["MeterManufacturedBy"] as TextObject;
                txtBillingReportHeader.Text = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName.ToString();

                //Channel 1 quantity
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["Channel_1"] as TextObject;
                txtBillingReportHeader.Text = "kW";

                //Channel 2 quantity
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["Channel_2"] as TextObject;
                txtBillingReportHeader.Text = "kvar";

                //Channel 3 quantity
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["Channel_3"] as TextObject;
                txtBillingReportHeader.Text = "kWh";

                //Channel 4 quantity
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["Channel_4"] as TextObject;
                txtBillingReportHeader.Text = "kvarh";

                //Receive date
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["txt_date"] as TextObject;
                txtBillingReportHeader.Text = DateTime.Now.ToString("dd MMM, yyyy");

                //Receive Time
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["txt_time"] as TextObject;
                txtBillingReportHeader.Text = DateTime.Now.ToString("HH:mm:ss");

                //Customer Code
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["txt_customerCode"] as TextObject;
                txtBillingReportHeader.Text = customerCode;

                //Programmers ID
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["txt_pid"] as TextObject;
                txtBillingReportHeader.Text = pid.ToString();

                //Active Season
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["txt_season"] as TextObject;
                txtBillingReportHeader.Text = active_season;

                //meter Model
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["meter_Model"] as TextObject;
                txtBillingReportHeader.Text = "ACT-";// Meter_Model + "_";

                //MSN
                string _msn = MSN.Substring(4);
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["MSN"] as TextObject;
                txtBillingReportHeader.Text = _msn;

                //Added by Azeem
                //Meter Type Code 
                string _meterTypeCode = MSN.Substring(0, 4);
                txtBillingReportHeader = loadProfile_Rpt.ReportDefinition.ReportObjects["MeterTypeCode"] as TextObject;
                txtBillingReportHeader.Text = _meterTypeCode;

                //loadProfile_Rpt.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\Load Profile.pdf");
                crystalReportViewer1.ReportSource = loadProfile_Rpt;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.RefreshReport();
                crystalReportViewer1.Zoom(100);
            }
            else
            {
                if (rptFormat == ReportFormat.WAPDA_DDS)
                {
                    RPT_LoadProfile_w main_rpt = new RPT_LoadProfile_w();
                    GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, LoadProfile_MainTitle, LoadProfile_SubTitle, true);
                    main_rpt.Database.Tables[0].SetDataSource(tempDS_loadProfile.Tables[0]);

                    //Report Days
                    txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["txt_ReportDays"] as TextObject;
                    txtBillingReportHeader.Text = IsPerDay? "(Per Day)": "(for Ninety Days)";

                    int indx = 0;
                    if (Quantities.Length > 4 && Quantities.Length >= 7)
                        indx += 3;

                    //Channel 1 quantity
                    txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["Channel_1"] as TextObject;
                    txtBillingReportHeader.Text = Quantities[indx++];

                    //Channel 2 quantity
                    txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["Channel_2"] as TextObject;
                    txtBillingReportHeader.Text = Quantities[indx++];

                    //Channel 3 quantity
                    txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["Channel_3"] as TextObject;
                    txtBillingReportHeader.Text = Quantities[indx++];

                    //Channel 4 quantity
                    txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["Channel_4"] as TextObject;
                    txtBillingReportHeader.Text = Quantities[indx++];

                    crystalReportViewer1.ReportSource = main_rpt;
                }
                else
                {
                    RPT_LoadProfile main_rpt = new RPT_LoadProfile();
                    GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                    main_rpt.Database.Tables[0].SetDataSource(tempDS_loadProfile.Tables[0]);

                    //CrystalDecisions.CrystalReports.Engine.TextObject txtBillingReportHeader;

                    //Commented for Testing perpose //v4.8.22
                    //Channel 1 quantity
                    txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["Channel_1"] as TextObject;
                    txtBillingReportHeader.Text = Quantities[0];

                    //Channel 2 quantity
                    txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["Channel_2"] as TextObject;
                    txtBillingReportHeader.Text = Quantities[1];

                    //Channel 3 quantity
                    txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["Channel_3"] as TextObject;
                    txtBillingReportHeader.Text = Quantities[2];

                    //Channel 4 quantity
                    txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["Channel_4"] as TextObject;
                    txtBillingReportHeader.Text = Quantities[3];

                    crystalReportViewer1.ReportSource = main_rpt;
                }
                crystalReportViewer1.Refresh();
                crystalReportViewer1.RefreshReport();
                crystalReportViewer1.Zoom(100);
            }
        }

        //Load Profile Graph
        public ReportViewer(ds_LoadProfile tempDS_loadProfile, string MSN, string Meter_Model, string[] Quantities, string meter_DateTime, string customerCode, ushort pid, string active_season, int type, string startEntry, string EndEntry, MeterConfig meter_type_info, ApplicationRight _currentAccessRights)
        {
            Init_ReportViewer(_currentAccessRights, _currentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.LoadProfile))));

            if (type <= 1)
            {
                rptLoadProfileChannelWiseChart main_rpt = new rptLoadProfileChannelWiseChart();

                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);

                main_rpt.Database.Tables[0].SetDataSource((DataTable)tempDS_loadProfile.DataTable_LoadProfile);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            else if (type == 2)
            {
                rptLoadProfileChannel2Chart main_rpt = new rptLoadProfileChannel2Chart();

                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                main_rpt.Database.Tables[0].SetDataSource((DataTable)tempDS_loadProfile.DataTable_LoadProfile);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            else if (type == 3)
            {
                rptLoadProfileChannel3Chart main_rpt = new rptLoadProfileChannel3Chart();

                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                main_rpt.Database.Tables[0].SetDataSource((DataTable)tempDS_loadProfile.DataTable_LoadProfile);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            else if (type == 4)
            {
                rptLoadProfileChannel4Chart main_rpt = new rptLoadProfileChannel4Chart();

                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                main_rpt.Database.Tables[0].SetDataSource((DataTable)tempDS_loadProfile.DataTable_LoadProfile);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }

        //Load Profile Graph NEW
        public ReportViewer(ds_LoadProfile.dtLoadProfileGraphDataTable temp_dtLoadProfileGraph, string MSN, string Channel_Name, string customerCode, ushort pid, string active_season, string startEntry, string EndEntry, MeterConfig meter_type_info, ApplicationRight _currentAccessRights)
        {
            Init_ReportViewer(_currentAccessRights, _currentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.LoadProfile))));


            RPT_LoadProfileLineGraph_w main_rpt = new RPT_LoadProfileLineGraph_w();
            if (GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, LoadProfile_MainTitle, LoadProfile_SubTitle,false))
            {
                CrystalDecisions.CrystalReports.Engine.TextObject txt_field;

                //Manufacturer
                txt_field = main_rpt.Subreports["RPT_LP_Graph"].ReportDefinition.ReportObjects["txt_Channel"] as TextObject;
                txt_field.Text = Channel_Name;

                main_rpt.Database.Tables[0].SetDataSource((DataTable)temp_dtLoadProfileGraph);
                crystalReportViewer1.ReportSource = main_rpt;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.RefreshReport();
                crystalReportViewer1.Zoom(100);
            }
            else
            {
                MessageBox.Show("Header Fail");
            }
        }

        #endregion

        #region Events and Major Alarms

        //major alarms
        public ReportViewer(DataTable datatable, string MSN, string customerCode, ushort pid, string active_season, string Model, int report_type, MeterConfig meter_type_info)
        {
            InitializeComponent();
            //CrystalDecisions.CrystalReports.Engine.TextObject txtEventReportHeader;
            if (report_type == 1)
            {
                RPT_MAJOR_ALARM main_rpt = new RPT_MAJOR_ALARM();
                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                main_rpt.SetDataSource(datatable);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            else if (report_type == 2)
            {
                temp main_rpt = new temp();
                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                //main_rpt.SetDataSource(datatable);
                //crystalReportViewer1.ReportSource = main_rpt;
                main_rpt.SetDataSource(datatable);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            else if (report_type == 3)
            {
                RPT_Events main_rpt = new RPT_Events();
                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                //main_rpt.SetDataSource(datatable);
                //crystalReportViewer1.ReportSource = main_rpt;
                main_rpt.SetDataSource(datatable);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }

        //events complete log
        public ReportViewer(DataTable datatable, string MSN, string customerCode, string pid, string active_season, MeterConfig meter_type_info, int _reportType, ApplicationRight _currentAccessRights, string TotalEventCount) //, ReportFormat rptFormat)
        {
            Init_ReportViewer(_currentAccessRights, _currentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.Events))));

            if (_reportType == 1) //LogBook
            {
                if (rptFormat == ReportFormat.WAPDA_DDS)
                {
                    RPT_EventsLogBook_w main_rpt = new RPT_EventsLogBook_w(); // RPT_EventsAll_w();
                    GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, EventIndividual_MainTitle, EventIndividual_SubTitle,true);
                    try
                    {
                        CrystalDecisions.CrystalReports.Engine.TextObject txt_field;
                        txt_field = main_rpt.ReportDefinition.ReportObjects["txt_TotalCount"] as TextObject;
                        txt_field.Text = TotalEventCount;
                    }
                    catch (Exception ex) { }
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                }
                else
                {
                    RPT_EventsAll main_rpt = new RPT_EventsAll();
                    GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                }
            }
            else if (_reportType == 2)  //Start with End
            {
                if (rptFormat == ReportFormat.WAPDA_DDS)
                {
                    RPT_EventsStartEnd_w main_rpt = new RPT_EventsStartEnd_w(); // RPT_EventsAll2_w();
                    GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, EventIndividual_MainTitle, EventIndividual_SubTitle, true);
                    try
                    {
                        CrystalDecisions.CrystalReports.Engine.TextObject txt_field;
                        txt_field = main_rpt.ReportDefinition.ReportObjects["txt_TotalCount"] as TextObject;
                        txt_field.Text = TotalEventCount;
                    }
                    catch (Exception ex) { }
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                }
                else
                {
                    RPT_EventsAll2 main_rpt = new RPT_EventsAll2();
                    GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                }
            }
            else if (_reportType == 3) //Individual
            {
                if (rptFormat == ReportFormat.WAPDA_DDS)
                {
                    RPT_EventsSingle_w main_rpt = new RPT_EventsSingle_w(); // RPT_EventsAll_w();
                    GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, EventIndividual_MainTitle, EventIndividual_SubTitle, true);
                    try
                    {
                        CrystalDecisions.CrystalReports.Engine.TextObject txt_field;
                        txt_field = main_rpt.ReportDefinition.ReportObjects["txt_TotalCount"] as TextObject;
                        txt_field.Text = TotalEventCount;
                    }
                    catch (Exception ex) { }
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                }
                else
                {
                    RPT_EventsAll main_rpt = new RPT_EventsAll();
                    GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                    main_rpt.SetDataSource(datatable);
                    crystalReportViewer1.ReportSource = main_rpt;
                }
            }
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }
        #endregion

        #region for communication verification
        //for communication verification
        public ReportViewer(string kwh, string kvarh, string MSN, string Model, MeterConfig meter_type_info)
        {
            InitializeComponent();
            RPT_Billing_Data main_rpt = new RPT_Billing_Data();
            //GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);

            CrystalDecisions.CrystalReports.Engine.TextObject txtBillingReportHeader;
            //Added by Azeem Inayat
            txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["MeterManufacturedBy"] as TextObject;
            txtBillingReportHeader.Text = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName.ToString();

            //MSN
            //MSN = MSN.Substring(4);
            txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["MSN"] as TextObject;
            txtBillingReportHeader.Text = MSN;
            //Meter Model
            txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["Model"] as TextObject;
            txtBillingReportHeader.Text = "ACT-";// Model + "_";

            //Receiving Date
            txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["ReceivingDate"] as TextObject;
            DateTime DT = DateTime.Now;
            txtBillingReportHeader.Text = DT.ToString("MMM dd, yyyy");

            //txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["ReceivingTime"] as TextObject;
            //txtBillingReportHeader.Text = DT.ToString("HH:mm");

            //kwh
            txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["kwh"] as TextObject;
            txtBillingReportHeader.Text = kwh;

            //kvarh
            txtBillingReportHeader = main_rpt.ReportDefinition.ReportObjects["kvarh"] as TextObject;
            txtBillingReportHeader.Text = kvarh;

            String fileDirectoryPath = @"D:\VCD_Reports";
            if (!Directory.Exists(fileDirectoryPath))
                Directory.CreateDirectory(fileDirectoryPath);
            main_rpt.ExportToDisk(ExportFormatType.PortableDocFormat, fileDirectoryPath + "\\VCD_" + DT.Day.ToString() + "_" + DT.Month.ToString() + "_" + DT.Year.ToString() + "  " + DT.Hour.ToString() + "_" + DT.Minute.ToString() + ".pdf");
            crystalReportViewer1.ReportSource = main_rpt;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }
        #endregion

        #region Instantaneous
        //For Instantaneous_WebFormat

        public ReportViewer(ds_Ins tempDS, string MSN, string MeterDT, string Model, string frequency)
        {
            InitializeComponent();

            #region Quantities Selected

            // ds_Ins selectedDS = new ds_Ins();
            // int i = 0;
            // int k = 0;
            // bool match;
            // if(elec_quantities.Count>0)
            // {
            // foreach (var item in elec_quantities)
            // {
            //     match = false;
            //     if (!String.IsNullOrEmpty(item))
            //     {
            //         while (!match)
            //         {
            //             string tempQty="";
            //             if(tempDS.dataTable_P[i].Quantity.Contains('('))
            //             {
            //                 tempQty = tempDS.dataTable_P[i].Quantity.Substring(0, tempDS.dataTable_P[i].Quantity.IndexOf(" ("));
            //             }
            //             else
            //                 tempQty=tempDS.dataTable_P[i].Quantity;
            //             if (tempQty == item)
            //             {
            //                 selectedDS.dataTable_P.Rows.Add();
            //                 selectedDS.dataTable_P[k].Quantity = tempDS.dataTable_P[i].Quantity;
            //                 selectedDS.dataTable_P[k].Phase_A = tempDS.dataTable_P[i].Phase_A;
            //                 selectedDS.dataTable_P[k].Phase_B = tempDS.dataTable_P[i].Phase_B;
            //                 selectedDS.dataTable_P[k].Phase_C = tempDS.dataTable_P[i].Phase_C;
            //                 selectedDS.dataTable_P[k].Phase_Total = tempDS.dataTable_P[i].Phase_Total;
            //                 i++;
            //                 k++;
            //                 match = true;
            //             }
            //             else
            //                 i++;
            //         }
            //     }
            // } 
            // }

            // MDI
            // i = 0;
            // k = 0;
            // if(mdi.Count>0)
            // {
            // foreach (var item in mdi)
            // {
            //     match = false;
            //     if (!String.IsNullOrEmpty(item))
            //     {
            //         while (!match)
            //         {
            //             if (tempDS.dataTable_T[i].Quantity == item)
            //             {
            //                 selectedDS.dataTable_T.Rows.Add();
            //                 selectedDS.dataTable_T[k].Quantity = tempDS.dataTable_T[i].Quantity;
            //                 selectedDS.dataTable_T[k].T1 = tempDS.dataTable_T[i].T1;
            //                 selectedDS.dataTable_T[k].T2 = tempDS.dataTable_T[i].T2;
            //                 selectedDS.dataTable_T[k].T3 = tempDS.dataTable_T[i].T3;
            //                 selectedDS.dataTable_T[k].T4 = tempDS.dataTable_T[i].T4;
            //                 selectedDS.dataTable_T[k].TL = tempDS.dataTable_T[i].TL;
            //                 i++;
            //                 k++;
            //                 match = true;
            //             }
            //             else
            //                 i++;
            //         }
            //     }
            // }
            // }
            // MISC
            // i = 0;
            // k = 0;
            // if(misc.Count>0)
            // {
            // foreach (var item in misc)
            // {
            //     match = false;
            //     if (!String.IsNullOrEmpty(item))
            //     {
            //         while (!match)
            //         {
            //             string tempQty = "";
            //             if (tempDS.dataTable_M[i].Quantity.Contains('('))
            //             {
            //                 tempQty = tempDS.dataTable_M[i].Quantity.Substring(0, tempDS.dataTable_M[i].Quantity.IndexOf(" ("));
            //             }
            //             else
            //                 tempQty = tempDS.dataTable_M[i].Quantity;
            //             if (tempQty == item)
            //             {
            //                 selectedDS.dataTable_M.Rows.Add();
            //                 selectedDS.dataTable_M[k].Quantity = tempDS.dataTable_M[i].Quantity;
            //                 selectedDS.dataTable_M[k].Value = tempDS.dataTable_M[i].Value;
            //                 i++;
            //                 k++;
            //                 match = true;
            //             }
            //             else
            //             {
            //                 i++;
            //                 //match = false;
            //             }
            //         }
            //     }
            // } 
            // }

            #endregion

            #region original report
            RPT_Instantaneous_new objRpt = new RPT_Instantaneous_new();//new RPT_Instantaneous();

            #region MISC rows solution
            //int rcount = selectedDS.Tables[2].Rows.Count;
            //if (rcount % 2 == 1)
            //{
            //    selectedDS.Tables[2].Rows.Add();
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
            //}
            //if (rcount == 0)
            //{
            //    selectedDS.Tables[2].Rows.Add();
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
            //    selectedDS.Tables[2].Rows.Add();
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
            //} 
            #endregion
            //objRpt.Database.Tables[2].SetDataSource(selectedDS.Tables[1]);//6 COLUMNS(2--1)
            objRpt.Database.Tables[1].SetDataSource(tempDS.Tables[0]);//4 COLUMNS(1--0)
            //objRpt.Database.Tables[0].SetDataSource(selectedDS.Tables[2]);//2 COLUMNS(0--2)


            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
            //Meter Manufacturing ID
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["MSN"] as TextObject;
            txtReportHeader.Text = MSN.Substring(4);
            //Meter Date Time
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["meter_DT"] as TextObject;
            txtReportHeader.Text = MeterDT;
            //Meter Frequency
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["supply_freq"] as TextObject;
            txtReportHeader.Text = frequency + " Hz";

            //Meter Manufacturing ID
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["Meter_Model"] as TextObject;
            txtReportHeader.Text = Model + "_";

            //Meter receive datetime
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["receivedDT"] as TextObject;
            txtReportHeader.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //if (misc.Count == 0)
            //    objRpt.Sec_msic.SectionFormat.EnableSuppress = true;
            //if (mdi.Count == 0)
            //    objRpt.Sec_tariff.SectionFormat.EnableSuppress = true;
            //if (elec_quantities.Count == 0)
            //    objRpt.Sec_phases.SectionFormat.EnableSuppress = true;
            crystalReportViewer1.ReportSource = objRpt;
            crystalReportViewer1.Refresh();
            this.crystalReportViewer1.RefreshReport();
            //ExportOptions exp = new ExportOptions();
            //PdfRtfWordFormatOptions pdfOptions = new PdfRtfWordFormatOptions();
            //exp.
            //DiskFileDestinationOptions options = new DiskFileDestinationOptions();
            //options.DiskFileName = "C:\Instantaneous Report";
            //export to PDF format
            //objRpt.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\Instantaneous.pdf"); 
            #endregion
        }
        public ReportViewer(ds_Ins tempDS, string MSN, string MeterDT, string Model, string customerCode, ushort pid, string active_season, MeterConfig meter_type_info, ApplicationRight _currentAccessRights) //, ReportFormat rptFormat)
        {
            Init_ReportViewer(_currentAccessRights, _currentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.Instataneous))));

            if (rptFormat == ReportFormat.WEB_GALAXY)
            {
                RPT_Instantaneous_WebFormat main_rpt = new RPT_Instantaneous_WebFormat();//new RPT_Instantaneous();
                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                main_rpt.Database.Tables[1].SetDataSource(tempDS.Tables[0]);
                CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;

                //Meter Date Time
                txtReportHeader = main_rpt.ReportDefinition.ReportObjects["meter_DT"] as TextObject;
                txtReportHeader.Text = MeterDT;
                crystalReportViewer1.ReportSource = main_rpt;

                #region OldHeader
                if (false)
                {
                    //Added by Azeem Inayat
                    txtReportHeader = main_rpt.ReportDefinition.ReportObjects["MeterManufacturedBy"] as TextObject;
                    txtReportHeader.Text = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName.ToString();

                    //Meter Manufacturing ID
                    //MSN
                    string _msn = MSN.Substring(4);
                    txtReportHeader = main_rpt.ReportDefinition.ReportObjects["MSN"] as TextObject;
                    txtReportHeader.Text = _msn;

                    //Added by Azeem
                    //Meter Type Code 
                    string _meterTypeCode = MSN.Substring(0, 4);
                    txtReportHeader = main_rpt.ReportDefinition.ReportObjects["MeterTypeCode"] as TextObject;
                    txtReportHeader.Text = _meterTypeCode;

                    //Meter Frequency
                    //txtReportHeader = objRpt.ReportDefinition.ReportObjects["supply_freq"] as TextObject;
                    //txtReportHeader.Text = frequency + " Hz";

                    //Meter Manufacturing ID
                    txtReportHeader = main_rpt.ReportDefinition.ReportObjects["Meter_Model"] as TextObject;
                    txtReportHeader.Text = "ACT-";// Model + "_";

                    //Meter receive datetime
                    txtReportHeader = main_rpt.ReportDefinition.ReportObjects["receivedDT"] as TextObject;
                    txtReportHeader.Text = DateTime.Now.ToString("dd MMM, yyyy");
                }
                #endregion
            }
            else
            {
                RPT_Instantaneous_new_w main_rpt = new RPT_Instantaneous_new_w();
                GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, Instantaneous_new_MainTitle, Instantaneous_new_SubTitle,false);
                //main_rpt.Database.Tables[1].SetDataSource(tempDS.Tables[0]);
                Fill_New_Instantaneous_RPT(main_rpt, tempDS, MeterDT, _currentAccessRights.InstantenousDataRights);  //v4.8.25
                crystalReportViewer1.ReportSource = main_rpt;
            }
            crystalReportViewer1.Refresh();
            //this.crystalReportViewer1.RefreshReport();
        }

        private void Fill_New_Instantaneous_RPT(ReportDocument main_rpt, ds_Ins tempDS, string MeterDT, List<AccessRights> rights)
        {
            try
            {
                bool isTotalPower = false;
                float kw_Total = 0, kvar_Total = 0;
                CrystalDecisions.CrystalReports.Engine.TextObject txtValues;
                int Volt_DP = 1, Current_DP = 1, Power_DP = 2;
                foreach (DataRow r in tempDS.dataTable_P)
                {
                    if (r["Quantity"].ToString().Contains("Voltage"))
                    {
                        txtValues = main_rpt.ReportDefinition.ReportObjects["V1"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[1].ToString(), Volt_DP);
                        txtValues = main_rpt.ReportDefinition.ReportObjects["V2"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[2].ToString(), Volt_DP);
                        txtValues = main_rpt.ReportDefinition.ReportObjects["V3"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[3].ToString(), Volt_DP);
                    }
                    else if (r["Quantity"].ToString().Contains("Current"))
                    {
                        txtValues = main_rpt.ReportDefinition.ReportObjects["I1"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[1].ToString(), Current_DP);
                        txtValues = main_rpt.ReportDefinition.ReportObjects["I2"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[2].ToString(), Current_DP);
                        txtValues = main_rpt.ReportDefinition.ReportObjects["I3"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[3].ToString(), Current_DP);
                    }
                    else if (r["Quantity"].ToString().Contains("PF"))
                    {
                        txtValues = main_rpt.ReportDefinition.ReportObjects["PF_Avg"] as TextObject;
                        txtValues.Text = r[4].ToString();
                    }
                    else if (r["Quantity"].ToString().Contains("Active Power Import"))
                    {
                        kw_Total += float.Parse(r[4].ToString());
                        txtValues = main_rpt.ReportDefinition.ReportObjects["kW_Import"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[4].ToString(), Power_DP);
                    }
                    else if (r["Quantity"].ToString().Contains("Active Power Export"))
                    {
                        kw_Total += float.Parse(r[4].ToString());
                        txtValues = main_rpt.ReportDefinition.ReportObjects["kW_Export"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[4].ToString(), Power_DP);
                    }
                    else if (r["Quantity"].ToString().Contains("Reactive Power Import"))
                    {
                        kvar_Total += float.Parse(r[4].ToString());
                        txtValues = main_rpt.ReportDefinition.ReportObjects["kvar_Import"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[4].ToString(), Power_DP);
                    }
                    else if (r["Quantity"].ToString().Contains("Reactive Power Export"))
                    {
                        kvar_Total += float.Parse(r[4].ToString());
                        txtValues = main_rpt.ReportDefinition.ReportObjects["kvar_Export"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[4].ToString(), Power_DP);
                    }
                    else if (r["Quantity"].ToString().Contains("Active Power")) //Total
                    {
                        isTotalPower = true;
                        txtValues = main_rpt.ReportDefinition.ReportObjects["kW_Total"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[4].ToString(), Power_DP);
                    }
                    else if (r["Quantity"].ToString().Contains("Reactive Power")) //Total
                    {
                        txtValues = main_rpt.ReportDefinition.ReportObjects["kVar_Total"] as TextObject;
                        txtValues.Text = LocalCommon.notRoundingOff(r[4].ToString(), Power_DP);
                    }
                    else
                    {

                    }

                    //Meter Date Time
                    txtValues = main_rpt.ReportDefinition.ReportObjects["meter_DT"] as TextObject;
                    txtValues.Text = MeterDT;
                }
                if (!isTotalPower)
                {
                    txtValues = main_rpt.ReportDefinition.ReportObjects["kW_Total"] as TextObject;
                    txtValues.Text = LocalCommon.notRoundingOff(kw_Total.ToString(), Power_DP);

                    txtValues = main_rpt.ReportDefinition.ReportObjects["kVar_Total"] as TextObject;
                    txtValues.Text = LocalCommon.notRoundingOff(kvar_Total.ToString(), Power_DP);
                }

                if (    rights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Powerkvar_Export.ToString())).Read ||
                        rights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Powerkvar_Import.ToString())).Read ||
                        rights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_PowerkW_Export.ToString())).Read ||
                        rights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_PowerkW_Import.ToString())).Read)
                {
                    ((RPT_Instantaneous_new_w)main_rpt).Sec_ImportExport.SectionFormat.EnableSuppress = false;

                    if ((rights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Powerkvar_Export.ToString()) && x.Read) == null))
                    {
                        txtValues = main_rpt.ReportDefinition.ReportObjects["kvar_Export"] as TextObject;
                        txtValues.Text = "";
                        txtValues = main_rpt.ReportDefinition.ReportObjects["lbl_kvar_Export"] as TextObject;
                        txtValues.Text = "";
                    }
                    if ((rights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_Powerkvar_Import.ToString()) && x.Read) == null))
                    {
                        txtValues = main_rpt.ReportDefinition.ReportObjects["kvar_Import"] as TextObject;
                        txtValues.Text = "";
                        txtValues = main_rpt.ReportDefinition.ReportObjects["lbl_kvar_Import"] as TextObject;
                        txtValues.Text = "";
                    }
                    if ((rights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_PowerkW_Export.ToString()) && x.Read) == null))
                    {
                        txtValues = main_rpt.ReportDefinition.ReportObjects["kW_Export"] as TextObject;
                        txtValues.Text = "";
                        txtValues = main_rpt.ReportDefinition.ReportObjects["lbl_kW_Export"] as TextObject;
                        txtValues.Text = "";
                    }
                    if ((rights.Find(x => x.QuantityName.Equals(eInstantaneousData.NewInst_PowerkW_Import.ToString()) && x.Read) == null))
                    {
                        txtValues = main_rpt.ReportDefinition.ReportObjects["kW_Import"] as TextObject;
                        txtValues.Text = "";
                        txtValues = main_rpt.ReportDefinition.ReportObjects["lbl_kW_Import"] as TextObject;
                        txtValues.Text = "";
                    }

                }
                else
                {
                    //suppress section
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //for instantaneous
        public ReportViewer(ds_Ins tempDS, string MSN, string DT, string Model, List<string> elec_quantities, List<string> mdi, List<string> misc, string customerCode, string pid, string active_season, MeterConfig meter_type_info, ApplicationRight _currentAccessRights) //, ReportFormat rptFormat)
        {
            InitializeComponent();
            if(_currentAccessRights != null)
                Init_ReportViewer(_currentAccessRights, _currentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.Instataneous))));

            #region Quantities selected

            ds_Ins selectedDS = new ds_Ins();
            if (rptFormat == ReportFormat.WAPDA_DDS)
            {
                RPT_Instantaneous_w main_rpt = new RPT_Instantaneous_w();//new RPT_Instantaneous();
                GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, Instantaneous_MainTitle, Instantaneous_SubTitle,false);

                if (elec_quantities.Count > 0)
                {
                    ds_Ins.dataTable_PRow _rowRet = null;

                    foreach (var item in elec_quantities)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            try
                            {
                                _rowRet = (from ds_Ins.dataTable_PRow _row in tempDS.dataTable_P.Rows
                                           where _row.Quantity.Contains(item)
                                           select _row).First<ds_Ins.dataTable_PRow>();
                            }
                            catch { _rowRet = null; }

                            //if (objRpt.Sec_phases.SectionFormat.) //check suppress property here: ToDO
                            //{
                            if (_rowRet != null)
                            {
                                var dtRow = selectedDS.dataTable_P.NewdataTable_PRow();
                                dtRow.Quantity = _rowRet.Quantity;
                                dtRow.Phase_A = _rowRet.Phase_A;
                                dtRow.Phase_B = _rowRet.Phase_B;
                                dtRow.Phase_C = _rowRet.Phase_C;
                                dtRow.Phase_Total = _rowRet.Phase_Total;
                                //Add New Row here
                                selectedDS.dataTable_P.AdddataTable_PRow(dtRow);
                            }
                            //}
                            //else
                            //{

                            //}

                        }
                    }
                }

                //MDI
                if (mdi.Count > 0)
                {
                    ds_Ins.dataTable_TRow _rowRet = null;
                    foreach (var item in mdi)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            try
                            {
                                _rowRet = (from  OptocomSoftware.Reporting.ds_Ins.dataTable_TRow _row in tempDS.dataTable_T.Rows
                                           where _row.Quantity.Contains(item) || _row.Quantity == "Current Month MDI KW"
                                           select _row).First<OptocomSoftware.Reporting.ds_Ins.dataTable_TRow>();
                            }
                            catch { _rowRet = null; }

                            if (_rowRet != null)
                            {
                                var dtRow = selectedDS.dataTable_T.NewdataTable_TRow();
                                dtRow.Quantity = _rowRet.Quantity;
                                dtRow.T1 = _rowRet.T1;
                                dtRow.T2 = _rowRet.T2;
                                dtRow.T3 = _rowRet.T3;
                                dtRow.T4 = _rowRet.T4;
                                dtRow.TL = _rowRet.TL;
                                //Add New Row here
                                selectedDS.dataTable_T.AdddataTable_TRow(dtRow);
                            }
                        }
                    }
                }
                //MISC
                if (misc.Count > 0)
                {
                    ds_Ins.dataTable_MRow _rowRet = null;

                    foreach (var item in misc)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            try
                            {
                                _rowRet = (from ds_Ins.dataTable_MRow _row in tempDS.dataTable_M.Rows
                                           where _row.Quantity.Contains(item)
                                           select _row).First<ds_Ins.dataTable_MRow>();
                            }
                            catch { _rowRet = null; }

                            if (_rowRet != null)
                            {
                                var dtRow = selectedDS.dataTable_M.NewdataTable_MRow();
                                dtRow.Quantity = _rowRet.Quantity;
                                dtRow.Value = _rowRet.Value;
                                //Add New Row here
                                selectedDS.dataTable_M.AdddataTable_MRow(dtRow);
                            }
                        }
                    }
                }
            #endregion

                #region original report

                //RPT_Instantaneous objRpt = new RPT_Instantaneous();//new RPT_Instantaneous(); //Commented by Azeem Inayat

                #region MISC rows solution

                int rcount = selectedDS.Tables[2].Rows.Count;
                if (rcount % 2 == 1)
                {
                    selectedDS.Tables[2].Rows.Add();
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
                }
                if (rcount == 0)
                {
                    selectedDS.Tables[2].Rows.Add();
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
                    selectedDS.Tables[2].Rows.Add();
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
                }
                #endregion
                main_rpt.Database.Tables[2].SetDataSource(selectedDS.Tables[1]);//6 COLUMNS(2--1)
                main_rpt.Database.Tables[1].SetDataSource(selectedDS.Tables[0]);//4 COLUMNS(1--0)
                main_rpt.Database.Tables[0].SetDataSource(selectedDS.Tables[2]);//2 COLUMNS(0--2)

                if (misc.Count == 0)
                    main_rpt.Sec_msic.SectionFormat.EnableSuppress = true;
                if (mdi.Count == 0)
                    main_rpt.Sec_tariff.SectionFormat.EnableSuppress = true;
                if (elec_quantities.Count == 0)
                    main_rpt.Sec_phases.SectionFormat.EnableSuppress = true;
                crystalReportViewer1.ReportSource = main_rpt;
            }
            else
            {
                RPT_Instantaneous main_rpt = new RPT_Instantaneous();//new RPT_Instantaneous();
                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);

                if (elec_quantities.Count > 0)
                {
                    ds_Ins.dataTable_PRow _rowRet = null;

                    foreach (var item in elec_quantities)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            try
                            {
                                _rowRet = (from ds_Ins.dataTable_PRow _row in tempDS.dataTable_P.Rows
                                           where _row.Quantity.Contains(item)
                                           select _row).First<ds_Ins.dataTable_PRow>();
                            }
                            catch { _rowRet = null; }

                            //if (objRpt.Sec_phases.SectionFormat.) //check suppress property here: ToDO
                            //{
                            if (_rowRet != null)
                            {
                                var dtRow = selectedDS.dataTable_P.NewdataTable_PRow();
                                dtRow.Quantity = _rowRet.Quantity;
                                dtRow.Phase_A = _rowRet.Phase_A;
                                dtRow.Phase_B = _rowRet.Phase_B;
                                dtRow.Phase_C = _rowRet.Phase_C;
                                dtRow.Phase_Total = _rowRet.Phase_Total;
                                //Add New Row here
                                selectedDS.dataTable_P.AdddataTable_PRow(dtRow);
                            }
                            //}
                            //else
                            //{

                            //}

                        }
                    }
                }

                //MDI
                if (mdi.Count > 0)
                {
                    ds_Ins.dataTable_TRow _rowRet = null;
                    foreach (var item in mdi)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            try
                            {
                                _rowRet = (from ds_Ins.dataTable_TRow _row in tempDS.dataTable_T.Rows
                                           where _row.Quantity.Contains(item) || _row.Quantity == "Current Month MDI KW"
                                           select _row).First<ds_Ins.dataTable_TRow>();
                            }
                            catch { _rowRet = null; }

                            if (_rowRet != null)
                            {
                                var dtRow = selectedDS.dataTable_T.NewdataTable_TRow();
                                dtRow.Quantity = _rowRet.Quantity;
                                dtRow.T1 = _rowRet.T1;
                                dtRow.T2 = _rowRet.T2;
                                dtRow.T3 = _rowRet.T3;
                                dtRow.T4 = _rowRet.T4;
                                dtRow.TL = _rowRet.TL;
                                //Add New Row here
                                selectedDS.dataTable_T.AdddataTable_TRow(dtRow);
                            }
                        }
                    }
                }
                //MISC
                if (misc.Count > 0)
                {
                    ds_Ins.dataTable_MRow _rowRet = null;

                    foreach (var item in misc)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            try
                            {
                                _rowRet = (from ds_Ins.dataTable_MRow _row in tempDS.dataTable_M.Rows
                                           where _row.Quantity.Contains(item)
                                           select _row).First<ds_Ins.dataTable_MRow>();
                            }
                            catch { _rowRet = null; }

                            if (_rowRet != null)
                            {
                                var dtRow = selectedDS.dataTable_M.NewdataTable_MRow();
                                dtRow.Quantity = _rowRet.Quantity;
                                dtRow.Value = _rowRet.Value;
                                //Add New Row here
                                selectedDS.dataTable_M.AdddataTable_MRow(dtRow);
                            }
                        }
                    }
                }
                #endregion

                #region original report

                //RPT_Instantaneous objRpt = new RPT_Instantaneous();//new RPT_Instantaneous(); //Commented by Azeem Inayat

                #region MISC rows solution

                int rcount = selectedDS.Tables[2].Rows.Count;
                if (rcount % 2 == 1)
                {
                    selectedDS.Tables[2].Rows.Add();
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
                }
                if (rcount == 0)
                {
                    selectedDS.Tables[2].Rows.Add();
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
                    selectedDS.Tables[2].Rows.Add();
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
                    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
                }
                #endregion
                main_rpt.Database.Tables[2].SetDataSource(selectedDS.Tables[1]);//6 COLUMNS(2--1)
                main_rpt.Database.Tables[1].SetDataSource(selectedDS.Tables[0]);//4 COLUMNS(1--0)
                main_rpt.Database.Tables[0].SetDataSource(selectedDS.Tables[2]);//2 COLUMNS(0--2)

                if (misc.Count == 0)
                    main_rpt.Sec_msic.SectionFormat.EnableSuppress = true;
                if (mdi.Count == 0)
                    main_rpt.Sec_tariff.SectionFormat.EnableSuppress = true;
                if (elec_quantities.Count == 0)
                    main_rpt.Sec_phases.SectionFormat.EnableSuppress = true;
                crystalReportViewer1.ReportSource = main_rpt;
            }
            crystalReportViewer1.Refresh();
            this.crystalReportViewer1.RefreshReport();
                #endregion
        }
        #endregion

        #region Cumulative Billing  AND  Monthly Billing
        //for cumulative billing
        public ReportViewer(string report_Title, ds_Billing tempDS_Billing, string MSN, string Meter_Model, string BillingDate, string customerCode, ushort pid, string active_season, string MdiResetCount, DateTime LastMdiResetDateTime, MeterConfig meter_type_info, ApplicationRight _currentAccessRights) //, ReportFormat rptFormat)
        {
            Init_ReportViewer(_currentAccessRights, _currentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.Billing))));

            if (rptFormat == ReportFormat.WAPDA_DDS)
            {
                RPT_Billing_w main_rpt = new RPT_Billing_w();
                GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, CumBilling_MainTitle, CumBilling_SubTitle,false);

                //v4.8.19
                CrystalDecisions.CrystalReports.Engine.TextObject txt_field;

                //Total MDI Reset Count
                txt_field = main_rpt.ReportDefinition.ReportObjects["txt_TotalMdiResetCount"] as TextObject;
                txt_field.Text = MdiResetCount.ToString();

                //Last MDI Reset Date Time
                txt_field = main_rpt.ReportDefinition.ReportObjects["txt_LastMdiResetDateTime"] as TextObject;
                txt_field.Text = LastMdiResetDateTime.ToString("dd MMM, yyyy HH:mm:ss");

                main_rpt.Database.Tables[0].SetDataSource(tempDS_Billing.Tables[0]);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            else
            {
                RPT_Billing main_rpt = new RPT_Billing();
                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);

                //v4.8.19
                CrystalDecisions.CrystalReports.Engine.TextObject txt_field;
                //Manufacturer
                txt_field = main_rpt.ReportDefinition.ReportObjects["txt_TotalMdiResetCount"] as TextObject;
                txt_field.Text = MdiResetCount.ToString();
                //

                main_rpt.Database.Tables[0].SetDataSource(tempDS_Billing.Tables[0]);

                crystalReportViewer1.ReportSource = main_rpt;
            }
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }

        //for monthly billing
        public ReportViewer(string report_Title, ds_Monthly_Billing tempDS_Monthly_Billing, string MSN, string Meter_Model, string customerCode, ushort pid, string active_season, MeterConfig meter_type_info, ApplicationRight _currentAccessRights) //, ReportFormat rptFormat)
        {
            Init_ReportViewer(_currentAccessRights, _currentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.Billing))));

            if (rptFormat == ReportFormat.WAPDA_DDS)
            {
                RPT_Monthly_Billing_w main_rpt = new RPT_Monthly_Billing_w();
                GetReportHeader_w(main_rpt , MSN, customerCode, pid.ToString(), active_season, meter_type_info, MonthlyBilling_MainTitle, MonthlyBilling_SubTitle,true);
                main_rpt.Database.Tables[0].SetDataSource(tempDS_Monthly_Billing.Tables[0]);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            else
            {
                RPT_Monthly_Billing main_rpt = new RPT_Monthly_Billing();
                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                main_rpt.Database.Tables[0].SetDataSource(tempDS_Monthly_Billing.Tables[0]);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }
        #endregion

        #region Security Report
        //Security Report
        public ReportViewer(ds_security dataset_security, string MSN, MeterConfig meter_type_info, string customerCode, ushort pid, string active_season, ApplicationRight _currentAccessRights) //, ReportFormat rptFormat)
        {
            Init_ReportViewer(_currentAccessRights, _currentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.Events))));

            if (rptFormat == ReportFormat.WAPDA_DDS)
            {
                RPT_security_w main_rpt = new RPT_security_w();
                GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, SecurityData_MainTitle, SecurityData_SubTitle,false);
                main_rpt.Database.Tables[0].SetDataSource(dataset_security.Tables[0]);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            else
            {
                RPT_security main_rpt = new RPT_security();
                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);
                main_rpt.Database.Tables[0].SetDataSource(dataset_security.Tables[0]);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }
        #endregion

        #region Parameterization
        //DisplayWindow for Wapda Format
        public ReportViewer(DataTable _displayWindows, string MSN, string Model, string customerCode, ushort pid, string active_season, int rptType, MeterConfig meter_type_info, ApplicationRight _currentAccessRights)
        {
            Init_ReportViewer(_currentAccessRights, _currentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.Parameter))));

            RPT_DisplayWindow_w main_rpt = new RPT_DisplayWindow_w();
            switch (rptType)
            {
                case 2:
                    GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, DisplayWindow_MainTitle, DisplayWinNor_SubTitle,true);
                    break;
                case 3:
                    GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, DisplayWindow_MainTitle, DisplayWinAlt_SubTitle, true);
                    break;
                case 4:
                    GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, DisplayWindow_MainTitle, DisplayWinTest_SubTitle, true);
                    break;
            }
            main_rpt.Database.Tables[0].SetDataSource(_displayWindows);
            crystalReportViewer1.ReportSource = main_rpt;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }

        //Parameterization for WAPDA format
        public ReportViewer(DataTable _Meter_Parameters, string MSN, string Model, string customerCode, ushort pid, string active_season, MeterConfig meter_type_info)
        {
            InitializeComponent();
            RPT_Param_w main_rpt = new RPT_Param_w();
            main_rpt.PageHeaderSection4.SectionFormat.EnableSuppress = true;
            GetReportHeader_w(main_rpt, MSN, customerCode, pid.ToString(), active_season, meter_type_info, Parameterization_MainTitle, Parameterization_SubTitle, false);

            if (_Meter_Parameters.Rows.Count > 0)
            {
                main_rpt.Subreports["meterparameter"].SetDataSource(_Meter_Parameters);
            }

            crystalReportViewer1.ReportSource = main_rpt;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }
        //Parameterization for WAPDA format (Fusion)
        public ReportViewer(DataTable _Meter_Parameters, string MSN, string Model, string customerCode, string custName, string CustAdress, ushort pid, string active_season, MeterConfig meter_type_info)
        {
            InitializeComponent();
            RPT_Param_w main_rpt = new RPT_Param_w();
            main_rpt.Section2.SectionFormat.EnableSuppress = true;
            GetReportHeader_w(main_rpt, MSN, customerCode, custName, CustAdress, pid.ToString(), active_season, meter_type_info, Parameterization_MainTitle, Parameterization_SubTitle, false);

            if (_Meter_Parameters.Rows.Count > 0)
            {
                main_rpt.Subreports["meterparameter"].SetDataSource(_Meter_Parameters);
            }

            crystalReportViewer1.ReportSource = main_rpt;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }

        //for Parameterization
        public ReportViewer(ds_Param Param_Dataset, ds_events dataset_events, dataset_Calendar calendar, string MSN, string Model, string cat, string customerCode, ushort pid, string active_season, MeterConfig meter_type_info)
        {
            InitializeComponent();
            {
                RPT_Param main_rpt = new RPT_Param();
                GetReportHeader(main_rpt.Subreports[RPT_headerForAll], MSN, customerCode, pid.ToString(), active_season, meter_type_info);

                main_rpt.ReportFooterSection1.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection2.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection3.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection4.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection5.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection6.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection7.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection8.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection10.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection16.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection19.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection20.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection21.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection22.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection23.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection24.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection25.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection26.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection27.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection28.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection29.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection30.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection31.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection32.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection33.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection36.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection37.SectionFormat.EnableSuppress = true;
                main_rpt.ReportFooterSection38.SectionFormat.EnableSuppress = true;


                if (Param_Dataset.Tables["Calender"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection3.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Calender"].SetDataSource(Param_Dataset.Tables["Calender"]);
                }
                if (calendar.Tables["table_day"].Rows.Count > 0)
                {
                    main_rpt.ReportFooterSection4.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Day Profile"].SetDataSource(calendar.Tables["table_day"]);
                }
                if (calendar.Tables["table_week"].Rows.Count > 0)
                {
                    main_rpt.ReportFooterSection23.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Week Profile"].SetDataSource(calendar.Tables["table_week"]);
                }
                if (calendar.Tables["table_season"].Rows.Count > 0)
                {
                    main_rpt.ReportFooterSection36.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Season Profile"].SetDataSource(calendar.Tables["table_season"]);
                }
                if (Param_Dataset.Tables["CT & PT Ratio"].Rows.Count > 0)
                {
                    main_rpt.ReportFooterSection38.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["CT & PT Ratio"].SetDataSource(Param_Dataset.Tables["CT & PT Ratio"]);
                }
                if (Param_Dataset.Tables["Clock Calibration"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection10.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Clock Calibration"].SetDataSource(Param_Dataset.Tables["Clock Calibration"]);
                }
                if (Param_Dataset.Tables["Contactor"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection19.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Clock Calibration"].SetDataSource(Param_Dataset.Tables["Clock Calibration"]);
                }
                if (Param_Dataset.Tables["Customer Code"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection20.SectionFormat.EnableSuppress = false;
                    //Param_Rpt.ReportFooterSection20.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Customer Code"].SetDataSource(Param_Dataset.Tables["Customer Code"]);
                }
                if (Param_Dataset.Tables["Decimal Points"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection21.SectionFormat.EnableSuppress = false;
                    //Param_Rpt.ReportFooterSection21.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Decimal Points"].SetDataSource(Param_Dataset.Tables["Decimal Points"]);
                }
                if (Param_Dataset.Tables["Display Windows Alternate"].Rows.Count > 0)
                {
                    main_rpt.ReportFooterSection5.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Display Windows Alternate"].SetDataSource(Param_Dataset.Tables["Display Windows Alternate"]);
                }
                if (Param_Dataset.Tables["Display Windows Normal"].Rows.Count > 0)
                {
                    main_rpt.ReportFooterSection22.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Display Windows Normal"].SetDataSource(Param_Dataset.Tables["Display Windows Normal"]);
                }
                if (Param_Dataset.Tables["Display Windows TestMode"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection16.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Display Windows TestMode"].SetDataSource(Param_Dataset.Tables["Display Windows TestMode"]);
                }
                if (Param_Dataset.Tables["Energy Parameters"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection6.SectionFormat.EnableSuppress = false;
                    //Param_Rpt.ReportFooterSection6.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Energy Parameters"].SetDataSource(Param_Dataset.Tables["Energy Parameters"]);
                }
                if (Param_Dataset.Tables["Limits"].Rows.Count > 0)
                {
                    main_rpt.ReportFooterSection7.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Limits"].SetDataSource(Param_Dataset.Tables["Limits"]);
                }
                if (Param_Dataset.Tables["Load Profile"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection1.SectionFormat.EnableSuppress = false;
                    //Param_Rpt.ReportFooterSection1.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Load Profile"].SetDataSource(Param_Dataset.Tables["Load Profile"]);
                }
                if (Param_Dataset.Tables["MDI Parameters"].Rows.Count > 0)
                {
                    main_rpt.ReportFooterSection8.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["MDI Parameters"].SetDataSource(Param_Dataset.Tables["MDI Parameters"]);
                }
                if (Param_Dataset.Tables["Monitoring Times"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection24.SectionFormat.EnableSuppress = false;
                    //Param_Rpt.ReportFooterSection24.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Monitoring Times"].SetDataSource(Param_Dataset.Tables["Monitoring Times"]);
                }
                if (Param_Dataset.Tables["Password"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection25.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Password"].SetDataSource(Param_Dataset.Tables["Password"]);
                }
                if (Param_Dataset.Tables["TCP UDP"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection26.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["TCP UDP"].SetDataSource(Param_Dataset.Tables["TCP UDP"]);
                }
                if (Param_Dataset.Tables["IP Profile"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection27.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["IP Profile"].SetDataSource(Param_Dataset.Tables["IP Profile"]);
                }
                if (Param_Dataset.Tables["Wakeup Profile"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection28.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Wakeup Profile"].SetDataSource(Param_Dataset.Tables["Wakeup Profile"]);
                }
                if (Param_Dataset.Tables["Number Profile"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection29.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Number Profile"].SetDataSource(Param_Dataset.Tables["Number Profile"]);
                }
                if (Param_Dataset.Tables["Communication Profile"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection30.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Communication Profile"].SetDataSource(Param_Dataset.Tables["Communication Profile"]);
                }
                if (Param_Dataset.Tables["Keep Alive"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection31.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Keep Alive"].SetDataSource(Param_Dataset.Tables["Keep Alive"]);
                }
                if (Param_Dataset.Tables["Modem Limits & Times"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection32.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Modem Limits & Times"].SetDataSource(Param_Dataset.Tables["Modem Limits & Times"]);
                }
                if (Param_Dataset.Tables["Modem Initialize"].Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection33.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["Modem Initialize"].SetDataSource(Param_Dataset.Tables["Modem Initialize"]);
                }
                if (dataset_events.DataTable_Time_Based_Events.Rows.Count > 0)
                {
                    //Param_Rpt.ReportFooterSection2.SectionFormat.EnableSuppress = false;
                    main_rpt.Subreports["temp.rpt"].SetDataSource(dataset_events.Tables["DataTable_Time_Based_Events"]);
                }

                //// change section
                //if (Param_Dataset.Tables["Communication Profile"].Rows.Count > 0)
                //{
                //    Param_Rpt.ReportFooterSection20.SectionFormat.EnableSuppress = false;
                //    Param_Rpt.Subreports["Communication Profile"].SetDataSource(Param_Dataset.Tables["Communication Profile"]);
                //}

                //Param_Rpt.Subreports["Modem Initialize"].SetDataSource(Param_Dataset.Tables["Modem Initialize"]);
                //Param_Rpt.Subreports["Modem Initialize NEW"].SetDataSource(Param_Dataset.Tables["Modem Initialize NEW"]);
                //Param_Rpt.Subreports["Modem Limits & Times"].SetDataSource(Param_Dataset.Tables["Modem Limits & Times"]);
                //

                //Param_Rpt.ReportFooterSection10.SectionFormat.EnableSuppress = true;
                //Param_Rpt.Subreports["Controller"].SetDataSource(Param_Dataset.Tables["Controller"]);
                //Param_Rpt.Subreports["Customer Code"].SetDataSource(Param_Dataset.Tables["Customer Code"]);
                //Param_Rpt.Subreports["Display Windows"].SetDataSource(Param_Dataset.Tables["Displaw Windows"]);
                //Param_Rpt.Subreports["IPV4"].SetDataSource(Param_Dataset.Tables["IPV4"]);
                //Param_Rpt.Subreports["Season Profile"].SetDataSource(Param_Dataset.Tables["Season Profile"]);
                //Param_Rpt.Subreports["TCP UDP"].SetDataSource(Param_Dataset.Tables["TCP UDP"]);

                //Param_Rpt.SetDataSource(Param_Dataset);
                crystalReportViewer1.ReportSource = main_rpt;
            }
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }

        public ReportViewer(ds_displayWindow tempDS_dw, string MSN, string Model, string meter_DateTime, string scrolltime, string windowType, string customercode, ushort pid, string active_season, MeterConfig meter_type_info)
        {
            InitializeComponent();
            RPT_DisplayWindow displayWIndow_RPT = new RPT_DisplayWindow();
            displayWIndow_RPT.Database.Tables[0].SetDataSource(tempDS_dw.Tables[0]);

            CrystalDecisions.CrystalReports.Engine.TextObject txtDisplayWindowReportHeader;

            //Added by Azeem Inayat
            txtDisplayWindowReportHeader = displayWIndow_RPT.ReportDefinition.ReportObjects["MeterManufacturedBy"] as TextObject;
            txtDisplayWindowReportHeader.Text = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName.ToString();

            //Receive date
            txtDisplayWindowReportHeader = displayWIndow_RPT.ReportDefinition.ReportObjects["txt_date"] as TextObject;
            txtDisplayWindowReportHeader.Text = DateTime.Now.ToString("dd MMM, yyyy");

            //Receive Time
            txtDisplayWindowReportHeader = displayWIndow_RPT.ReportDefinition.ReportObjects["txt_time"] as TextObject;
            txtDisplayWindowReportHeader.Text = DateTime.Now.ToString("HH:mm:ss");

            //Customer Code
            txtDisplayWindowReportHeader = displayWIndow_RPT.ReportDefinition.ReportObjects["txt_CustomerCode"] as TextObject;
            txtDisplayWindowReportHeader.Text = customercode;

            //Programmers ID
            txtDisplayWindowReportHeader = displayWIndow_RPT.ReportDefinition.ReportObjects["txt_pid"] as TextObject;
            txtDisplayWindowReportHeader.Text = pid.ToString();

            //Active Season
            txtDisplayWindowReportHeader = displayWIndow_RPT.ReportDefinition.ReportObjects["txt_season"] as TextObject;
            txtDisplayWindowReportHeader.Text = active_season;

            txtDisplayWindowReportHeader = displayWIndow_RPT.ReportDefinition.ReportObjects["txt_WindowType"] as TextObject;
            txtDisplayWindowReportHeader.Text = windowType;

            txtDisplayWindowReportHeader = displayWIndow_RPT.ReportDefinition.ReportObjects["txt_ScrollTime"] as TextObject;
            txtDisplayWindowReportHeader.Text = scrolltime;

            //meter Model
            txtDisplayWindowReportHeader = displayWIndow_RPT.ReportDefinition.ReportObjects["Model"] as TextObject;
            txtDisplayWindowReportHeader.Text = "ACT-";// Model + "_";

            //MSN
            txtDisplayWindowReportHeader = displayWIndow_RPT.ReportDefinition.ReportObjects["MSN"] as TextObject;
            txtDisplayWindowReportHeader.Text = MSN;

            //loadProfile_Rpt.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\Load Profile.pdf");
            crystalReportViewer1.ReportSource = displayWIndow_RPT;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            crystalReportViewer1.Zoom(100);
        }

        public ReportViewer(dataset_Calendar calendar, ds_Param dataset_param, string MSN, string Model, string customercode, ushort pid, string active_season, string customerName, string customerAddress, MeterConfig meter_type_info)
        {
            try
            {
                InitializeComponent();
                temp report = new temp();
                DataTable table_season = calendar.table_season;
                DataTable table_week = calendar.table_week;
                DataTable table_specialday = calendar.table_specialDays;
                DataTable table_dayProfile = calendar.table_day;

                CrystalDecisions.CrystalReports.Engine.TextObject txtParameterizationReportHeader;

                //Added by Azeem Inayat
                txtParameterizationReportHeader = report.ReportDefinition.ReportObjects["MeterManufacturedBy"] as TextObject;
                txtParameterizationReportHeader.Text = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName.ToString();

                //MSN
                MSN = MSN.Substring(4);
                txtParameterizationReportHeader = report.ReportDefinition.ReportObjects["MSN"] as TextObject;
                txtParameterizationReportHeader.Text = MSN;
                //Meter Model
                txtParameterizationReportHeader = report.ReportDefinition.ReportObjects["meter_Model"] as TextObject;
                txtParameterizationReportHeader.Text = "ACT-";// Model + "_";
                //Receive date
                txtParameterizationReportHeader = report.ReportDefinition.ReportObjects["txt_date"] as TextObject;
                txtParameterizationReportHeader.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //Receive Time
                txtParameterizationReportHeader = report.ReportDefinition.ReportObjects["txt_time"] as TextObject;
                txtParameterizationReportHeader.Text = DateTime.Now.ToString("HH:mm:ss");

                //Customer Code
                txtParameterizationReportHeader = report.ReportDefinition.ReportObjects["txt_CustomerCode"] as TextObject;
                txtParameterizationReportHeader.Text = customercode;

                //Programmers ID
                txtParameterizationReportHeader = report.ReportDefinition.ReportObjects["txt_pid"] as TextObject;
                txtParameterizationReportHeader.Text = pid.ToString();

                //Active Season
                txtParameterizationReportHeader = report.ReportDefinition.ReportObjects["txt_season"] as TextObject;
                txtParameterizationReportHeader.Text = active_season;

                //Customer Name
                txtParameterizationReportHeader = report.ReportDefinition.ReportObjects["txt_name"] as TextObject;
                txtParameterizationReportHeader.Text = customerName;

                //Customer Address
                txtParameterizationReportHeader = report.ReportDefinition.ReportObjects["txt_address"] as TextObject;
                txtParameterizationReportHeader.Text = customerAddress;


                report.SetDataSource(dataset_param);
                for (int i = 0; i < report.Subreports.Count; i++)
                {
                    if (report.Subreports[i].Name.Equals("Season Profile"))
                    {
                        report.Subreports[i].SetDataSource(table_season);
                        //MSN
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["MSN"] as TextObject;
                        txtParameterizationReportHeader.Text = MSN;
                        //Meter Model
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["meter_Model"] as TextObject;
                        txtParameterizationReportHeader.Text = "ACT-";// Model + "_";

                        //Receive date
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_date"] as TextObject;
                        txtParameterizationReportHeader.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        //Receive Time
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_time"] as TextObject;
                        txtParameterizationReportHeader.Text = DateTime.Now.ToString("HH:mm:ss");

                        //Customer Code
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_CustomerCode"] as TextObject;
                        txtParameterizationReportHeader.Text = customercode;

                        //Programmers ID
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_pid"] as TextObject;
                        txtParameterizationReportHeader.Text = pid.ToString();

                        //Active Season
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_season"] as TextObject;
                        txtParameterizationReportHeader.Text = active_season;

                        //Customer Name
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_name"] as TextObject;
                        txtParameterizationReportHeader.Text = customerName;

                        //Customer Address
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_address"] as TextObject;
                        txtParameterizationReportHeader.Text = customerAddress;
                    }
                    else if (report.Subreports[i].Name.Equals("Week Profile"))
                    {
                        report.Subreports[i].SetDataSource(table_week);
                        //MSN
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["MSN"] as TextObject;
                        txtParameterizationReportHeader.Text = MSN;
                        //Meter Model
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["meter_Model"] as TextObject;
                        txtParameterizationReportHeader.Text = "ACT-";// Model + "_";

                        //Receive date
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_date"] as TextObject;
                        txtParameterizationReportHeader.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        //Receive Time
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_time"] as TextObject;
                        txtParameterizationReportHeader.Text = DateTime.Now.ToString("HH:mm:ss");

                        //Customer Code
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_CustomerCode"] as TextObject;
                        txtParameterizationReportHeader.Text = customercode;

                        //Programmers ID
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_pid"] as TextObject;
                        txtParameterizationReportHeader.Text = pid.ToString();

                        //Active Season
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_season"] as TextObject;
                        txtParameterizationReportHeader.Text = active_season;

                        //Customer Name
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_name"] as TextObject;
                        txtParameterizationReportHeader.Text = customerName;

                        //Customer Address
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_address"] as TextObject;
                        txtParameterizationReportHeader.Text = customerAddress;

                    }
                    else if (report.Subreports[i].Name.Equals("Special Day Profile"))
                    {
                        report.Subreports[i].SetDataSource(table_specialday);
                        //MSN
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["MSN"] as TextObject;
                        txtParameterizationReportHeader.Text = MSN;
                        //Meter Model
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["meter_Model"] as TextObject;
                        txtParameterizationReportHeader.Text = "ACT-";// Model + "_";

                        //Receive date
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_date"] as TextObject;
                        txtParameterizationReportHeader.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        //Receive Time
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_time"] as TextObject;
                        txtParameterizationReportHeader.Text = DateTime.Now.ToString("HH:mm:ss");

                        //Customer Code
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_CustomerCode"] as TextObject;
                        txtParameterizationReportHeader.Text = customercode;

                        //Programmers ID
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_pid"] as TextObject;
                        txtParameterizationReportHeader.Text = pid.ToString();

                        //Active Season
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_season"] as TextObject;
                        txtParameterizationReportHeader.Text = active_season;

                        //Customer Name
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_name"] as TextObject;
                        txtParameterizationReportHeader.Text = customerName;

                        //Customer Address
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_address"] as TextObject;
                        txtParameterizationReportHeader.Text = customerAddress;

                    }
                    else
                    {
                        report.Subreports[i].SetDataSource(table_dayProfile);
                        //MSN
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["MSN"] as TextObject;
                        txtParameterizationReportHeader.Text = MSN;
                        //Meter Model
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["meter_Model"] as TextObject;
                        txtParameterizationReportHeader.Text = "ACT-";// Model + "_";

                        //Receive date
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_date"] as TextObject;
                        txtParameterizationReportHeader.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        //Receive Time
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_time"] as TextObject;
                        txtParameterizationReportHeader.Text = DateTime.Now.ToString("HH:mm:ss");

                        //Customer Code
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_CustomerCode"] as TextObject;
                        txtParameterizationReportHeader.Text = customercode;

                        //Programmers ID
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_pid"] as TextObject;
                        txtParameterizationReportHeader.Text = pid.ToString();

                        //Active Season
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_season"] as TextObject;
                        txtParameterizationReportHeader.Text = active_season;

                        //Customer Name
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_name"] as TextObject;
                        txtParameterizationReportHeader.Text = customerName;

                        //Customer Address
                        txtParameterizationReportHeader = report.Subreports[i].ReportDefinition.ReportObjects["txt_address"] as TextObject;
                        txtParameterizationReportHeader.Text = customerAddress;

                    }

                }


                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.RefreshReport();
                crystalReportViewer1.Zoom(100);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region Not Used
        private void ReportViewer_Load(object sender, EventArgs e)
        {
        }
        //Not Used
        public ReportViewer(ds_Ins tempDS, string MSN, string MeterDT, string Model, string frequency, MeterConfig meter_type_info)
        {
            InitializeComponent();
            #region Quantities selected
            //ds_Ins selectedDS = new ds_Ins();
            //int i = 0;
            //int k = 0;
            //bool match;
            //if(elec_quantities.Count>0)
            //{
            //foreach (var item in elec_quantities)
            //{
            //    match = false;
            //    if (!String.IsNullOrEmpty(item))
            //    {
            //        while (!match)
            //        {
            //            string tempQty="";
            //            if(tempDS.dataTable_P[i].Quantity.Contains('('))
            //            {
            //                tempQty = tempDS.dataTable_P[i].Quantity.Substring(0, tempDS.dataTable_P[i].Quantity.IndexOf(" ("));
            //            }
            //            else
            //                tempQty=tempDS.dataTable_P[i].Quantity;
            //            if (tempQty == item)
            //            {
            //                selectedDS.dataTable_P.Rows.Add();
            //                selectedDS.dataTable_P[k].Quantity = tempDS.dataTable_P[i].Quantity;
            //                selectedDS.dataTable_P[k].Phase_A = tempDS.dataTable_P[i].Phase_A;
            //                selectedDS.dataTable_P[k].Phase_B = tempDS.dataTable_P[i].Phase_B;
            //                selectedDS.dataTable_P[k].Phase_C = tempDS.dataTable_P[i].Phase_C;
            //                selectedDS.dataTable_P[k].Phase_Total = tempDS.dataTable_P[i].Phase_Total;
            //                i++;
            //                k++;
            //                match = true;
            //            }
            //            else
            //                i++;
            //        }
            //    }
            //} 
            //}

            ////MDI
            //i = 0;
            //k = 0;
            //if(mdi.Count>0)
            //{
            //foreach (var item in mdi)
            //{
            //    match = false;
            //    if (!String.IsNullOrEmpty(item))
            //    {
            //        while (!match)
            //        {
            //            if (tempDS.dataTable_T[i].Quantity == item)
            //            {
            //                selectedDS.dataTable_T.Rows.Add();
            //                selectedDS.dataTable_T[k].Quantity = tempDS.dataTable_T[i].Quantity;
            //                selectedDS.dataTable_T[k].T1 = tempDS.dataTable_T[i].T1;
            //                selectedDS.dataTable_T[k].T2 = tempDS.dataTable_T[i].T2;
            //                selectedDS.dataTable_T[k].T3 = tempDS.dataTable_T[i].T3;
            //                selectedDS.dataTable_T[k].T4 = tempDS.dataTable_T[i].T4;
            //                selectedDS.dataTable_T[k].TL = tempDS.dataTable_T[i].TL;
            //                i++;
            //                k++;
            //                match = true;
            //            }
            //            else
            //                i++;
            //        }
            //    }
            //}
            //}
            ////MISC
            //i = 0;
            //k = 0;
            //if(misc.Count>0)
            //{
            //foreach (var item in misc)
            //{
            //    match = false;
            //    if (!String.IsNullOrEmpty(item))
            //    {
            //        while (!match)
            //        {
            //            string tempQty = "";
            //            if (tempDS.dataTable_M[i].Quantity.Contains('('))
            //            {
            //                tempQty = tempDS.dataTable_M[i].Quantity.Substring(0, tempDS.dataTable_M[i].Quantity.IndexOf(" ("));
            //            }
            //            else
            //                tempQty = tempDS.dataTable_M[i].Quantity;
            //            if (tempQty == item)
            //            {
            //                selectedDS.dataTable_M.Rows.Add();
            //                selectedDS.dataTable_M[k].Quantity = tempDS.dataTable_M[i].Quantity;
            //                selectedDS.dataTable_M[k].Value = tempDS.dataTable_M[i].Value;
            //                i++;
            //                k++;
            //                match = true;
            //            }
            //            else
            //            {
            //                i++;
            //                //match = false;
            //            }
            //        }
            //    }
            //} 
            //}
            #endregion

            #region original report
            RPT_Instantaneous_new objRpt = new RPT_Instantaneous_new();//new RPT_Instantaneous();

            //#region MISC rows solution
            //int rcount = selectedDS.Tables[2].Rows.Count;
            //if (rcount % 2 == 1)
            //{
            //    selectedDS.Tables[2].Rows.Add();
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
            //}
            //if (rcount == 0)
            //{
            //    selectedDS.Tables[2].Rows.Add();
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
            //    selectedDS.Tables[2].Rows.Add();
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][0] = "";
            //    selectedDS.Tables[2].Rows[selectedDS.Tables[2].Rows.Count - 1][1] = "";
            //} 
            //#endregion
            //objRpt.Database.Tables[2].SetDataSource(selectedDS.Tables[1]);//6 COLUMNS(2--1)
            objRpt.Database.Tables[1].SetDataSource(tempDS.Tables[0]);//4 COLUMNS(1--0)
            //objRpt.Database.Tables[0].SetDataSource(selectedDS.Tables[2]);//2 COLUMNS(0--2)


            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
            //Added by Azeem Inayat
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["MeterManufacturedBy"] as TextObject;
            txtReportHeader.Text = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName.ToString();

            //Meter Manufacturing ID
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["MSN"] as TextObject;
            txtReportHeader.Text = MSN;
            //Meter Date Time
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["meter_DT"] as TextObject;
            txtReportHeader.Text = MeterDT;
            //Meter Frequency
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["supply_freq"] as TextObject;
            txtReportHeader.Text = frequency + " Hz";

            //Meter Manufacturing ID
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["Meter_Model"] as TextObject;
            txtReportHeader.Text = "ACT-";// Model + "_";

            //Meter receive datetime
            txtReportHeader = objRpt.ReportDefinition.ReportObjects["receivedDT"] as TextObject;
            txtReportHeader.Text = DateTime.Now.ToString("dd MMM, yyyy");

            //if (misc.Count == 0)
            //    objRpt.Sec_msic.SectionFormat.EnableSuppress = true;
            //if (mdi.Count == 0)
            //    objRpt.Sec_tariff.SectionFormat.EnableSuppress = true;
            //if (elec_quantities.Count == 0)
            //    objRpt.Sec_phases.SectionFormat.EnableSuppress = true;
            crystalReportViewer1.ReportSource = objRpt;
            crystalReportViewer1.Refresh();
            this.crystalReportViewer1.RefreshReport();
            //ExportOptions exp = new ExportOptions();
            //PdfRtfWordFormatOptions pdfOptions = new PdfRtfWordFormatOptions();
            //exp.
            //DiskFileDestinationOptions options = new DiskFileDestinationOptions();
            //options.DiskFileName = "C:\Instantaneous Report";
            //export to PDF format
            //objRpt.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\Instantaneous.pdf"); 
            #endregion
        }
        #endregion
    }
    enum RptType : byte
    {
        Billing,
        Parameter,
        Events,
        Instantaneous,
        SecurityData,
        LoadProfile

    }
}

/*
 Prepared by M. Azeem Inayat
 * Dated: 03 March, 20106
 * ---------------------------------
 * NOT USED REPORTS
 * --------------------------------
 * param_RPT
 * RPT_day
 * rpt_season
 * rpt_specialdays
 * rpt_week
 * 
 
 
 
 */