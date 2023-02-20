// #define Enable_DEBUG_RunMode

using comm.DataContainer;
using DatabaseConfiguration.DataSet;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharedCode.Controllers
{
    public class BillingController
    {
        #region Data_Member
        private ApplicationProcess_Controller _AP_Controller;
        private GenericProfileInfo BillingInfo = null;
        private GenericProfileInfo CumulativeBillingInfo = null;
        //private Configs configurations;
        private Configurator _Configurator;
        private ConnectionInfo connInfo;
        private Class_7 BillingCommObj;
        private Class_7 CumulativeBillingCommObj;
        private IBillingDataFormatter dtFormat = new MonthlyBillingDataFormatter();

        #region Billing_Filter

        public static readonly byte CumulativeBilling = 0xFF;
        public static readonly byte MonthlyBilling_All = 0xFF;

        public static readonly byte MonthlyBilling_Field = 0x01;
        public static readonly byte DailyEnergyProfile_Field = 0x02;

        public static readonly byte MonthlyBilling_01 = 00;
        public static readonly byte MonthlyBilling_02 = 01;
        public static readonly byte MonthlyBilling_03 = 02;
        public static readonly byte MonthlyBilling_04 = 03;
        public static readonly byte MonthlyBilling_05 = 04;
        public static readonly byte MonthlyBilling_06 = 05;
        public static readonly byte MonthlyBilling_07 = 06;
        public static readonly byte MonthlyBilling_08 = 07;
        public static readonly byte MonthlyBilling_09 = 08;
        public static readonly byte MonthlyBilling_10 = 09;
        public static readonly byte MonthlyBilling_11 = 10;
        public static readonly byte MonthlyBilling_12 = 11;
        public static readonly byte MonthlyBilling_13 = 12;
        public static readonly byte MonthlyBilling_14 = 13;
        public static readonly byte MonthlyBilling_15 = 14;
        public static readonly byte MonthlyBilling_16 = 15;
        public static readonly byte MonthlyBilling_17 = 16;
        public static readonly byte MonthlyBilling_18 = 17;
        public static readonly byte MonthlyBilling_19 = 18;
        public static readonly byte MonthlyBilling_20 = 19;
        public static readonly byte MonthlyBilling_21 = 20;
        public static readonly byte MonthlyBilling_22 = 21;
        public static readonly byte MonthlyBilling_23 = 22;
        public static readonly byte MonthlyBilling_24 = 23;

        public static readonly byte LastMonthlyBilling_01 = 101;
        public static readonly byte LastMonthlyBilling_02 = 102;
        public static readonly byte LastMonthlyBilling_03 = 103;
        public static readonly byte LastMonthlyBilling_04 = 104;
        public static readonly byte LastMonthlyBilling_05 = 105;
        public static readonly byte LastMonthlyBilling_06 = 106;
        public static readonly byte LastMonthlyBilling_07 = 107;
        public static readonly byte LastMonthlyBilling_08 = 108;
        public static readonly byte LastMonthlyBilling_09 = 109;
        public static readonly byte LastMonthlyBilling_10 = 110;
        public static readonly byte LastMonthlyBilling_11 = 111;
        public static readonly byte LastMonthlyBilling_12 = 112;
        public static readonly byte LastMonthlyBilling_13 = 113;
        public static readonly byte LastMonthlyBilling_14 = 114;
        public static readonly byte LastMonthlyBilling_15 = 115;
        public static readonly byte LastMonthlyBilling_16 = 116;
        public static readonly byte LastMonthlyBilling_17 = 117;
        public static readonly byte LastMonthlyBilling_18 = 118;
        public static readonly byte LastMonthlyBilling_19 = 119;
        public static readonly byte LastMonthlyBilling_20 = 120;
        public static readonly byte LastMonthlyBilling_21 = 121;
        public static readonly byte LastMonthlyBilling_22 = 122;
        public static readonly byte LastMonthlyBilling_23 = 123;
        public static readonly byte LastMonthlyBilling_24 = 124;

        #endregion
        private byte monthlyBillingFilter = MonthlyBilling_All;
        private byte monthlyBillingDFieldFilter = MonthlyBilling_Field;
        #endregion

        #region Properties

        public byte MonthlyBillingDFieldFilter
        {
            get { return monthlyBillingDFieldFilter; }
            set
            {
                // Validate Incoming Value
                if (value == MonthlyBilling_Field || value == DailyEnergyProfile_Field)
                    monthlyBillingDFieldFilter = value;
                else
                    throw new Exception(String.Format("Invalid Monthlty Billing D Field Filter value {0}", value));
            }
        }
        public ApplicationProcess_Controller AP_Controller
        {
            get { return _AP_Controller; }
            set { _AP_Controller = value; }
        }

        public byte MonthlyBillingFilter
        {
            get { return monthlyBillingFilter; }
            set
            {
                ///Validate Incoming Values
                if (value == CumulativeBilling || value == MonthlyBilling_All ||
                    (value >= MonthlyBilling_01 && value <= MonthlyBilling_24) ||
                    (value >= LastMonthlyBilling_01 && value <= LastMonthlyBilling_24))
                    monthlyBillingFilter = value;
                else
                    throw new Exception(String.Format("Invalid Monthly Billing Filter value {0}", value));
            }
        }

        //public Configs Configurations
        //{
        //    get { return configurations; }
        //    set { configurations = value; }
        //}

        public ConnectionInfo CurrentConnectionInfo
        {
            get { return connInfo; }
            set { connInfo = value; }
        }

        public Configurator Configurator
        {
            get { return _Configurator; }
            set { _Configurator = value; }
        }
        #endregion

        #region Constructur
        public BillingController()
        {
            BillingInfo = new GenericProfileInfo();
            CumulativeBillingInfo = new GenericProfileInfo();
            //Configurator.IsConfigRefreshRequired = true;
        }
        #endregion

        #region GET_Objects

        public Base_Class GetSAPEntry(Get_Index ObjIdentifier)
        {
            return AP_Controller.GetSAPEntry(ObjIdentifier);
        }
        public Base_Class GetSAPEntry(StOBISCode ObjIdentifier)
        {
            return this.AP_Controller.GetSAPEntry(ObjIdentifier);
        }

        public Base_Class GET_Object(StOBISCode ObjIdentifier, byte attribArg)
        {
            Base_Class objReceived = null;
            try
            {
                objReceived = GetSAPEntry(ObjIdentifier);
                objReceived.DecodingAttribute = attribArg;

                // Reset To DecodingResult To DataNotPresent
                if (objReceived != null)
                {
                    objReceived.ResetAttributeDecodingResults(objReceived.DecodingAttribute);
                }
                GET_Object(objReceived);

                return objReceived;
            }
            catch (Exception ex)  // Other Reason Errors
            {
                throw ex;
            }
        }

        public Base_Class GET_Object(Get_Index ObjIdentifier)
        {
            try
            {
                return GET_Object(ObjIdentifier, 0);
            }
            catch (Exception ex)               //Other Reason Errors
            {
                throw ex;
            }
        }

        public Base_Class GET_Object(Get_Index ObjIdentifier, byte attribArg)
        {
            Base_Class objReceived = null;
            try
            {
                objReceived = AP_Controller.GET(ObjIdentifier, attribArg);
                return objReceived;
            }
            catch (DLMSDecodingException ex)    ///Error Type 1 (Decoding Type Errors)
            {
                objReceived = AP_Controller.PreviousRequestedObject;
                //return objReceived;
                throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                objReceived = AP_Controller.PreviousRequestedObject;
                //return objReceived;
                throw ex;
            }
            catch (IOException ex)              ///Communication Data IO Errors
            {
                throw ex;
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                throw ex;
            }
            finally
            {
            }
        }

        public Base_Class GET_Object(Base_Class ObjIdentifier)
        {
            Base_Class objReceived = null;
            try
            {
                objReceived = AP_Controller.GET(ObjIdentifier);
                return objReceived;
            }
            catch (DLMSDecodingException ex)    ///Error Type 1 (Decoding Type Errors)
            {
                objReceived = AP_Controller.PreviousRequestedObject;
                ///return objReceived;
                throw ex;
            }
            catch (DLMSException ex)            ///Error Type 2 (DLMS Layer Errors)
            {
                objReceived = AP_Controller.PreviousRequestedObject;
                ///return objReceived;
                throw ex;
            }
            catch (IOException ex)              ///Communication Data IO Errors
            {
                throw ex;
            }
            catch (Exception ex)               ///Other Reason Errors
            {
                throw ex;
            }
            finally
            { }
        }

        public double GET_Any(ref Instantaneous_Class Instantaneous_Class_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return Instantaneous_Class_obj.Decode_Any(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public byte[] GET_Any_ByteArray(ref Instantaneous_Class Instantaneous_Class_obj, Get_Index arg, byte attribute, byte Class_ID)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return Instantaneous_Class_obj.Decode_Any_ByteArray(Comm_Obj, Class_ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        #endregion

        #region Get_Cummulative_Data

        public BillingData GetCumulativeBillingData(READ_METHOD read_CB)
        {
            try
            {
                IAccessSelector selector = null;

                IBillingDataFormatter dtFormat = new MonthlyBillingDataFormatter();
                //((MonthlyBillingDataFormatter)dtFormat).Configurations = this.Configurations;
                Class_7 Billing_CommObj = (Class_7)GetSAPEntry(Get_Index.CumulativeBilling);

                GetBillingInfo(ref CumulativeBillingInfo, Billing_CommObj);

                if (read_CB == READ_METHOD.ByDateTime)
                {
                    DateTime currentTime = DateTime.Now;
                    selector = ComputeRangeSelector(currentTime, currentTime);
                    ((RangeDescripter)selector).Capture_Object_Definition = Billing_CommObj.captureObjectsList;
                }

                List<ILValue[]> billPeriods = ReadBillingDataRaw(selector, Billing_CommObj);
                List<BillingData> formattedBillData = FormatBillingData(billPeriods);
                return formattedBillData[0];
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occurred reading Cumulative Monthly Billing data (Error Code:{0})", (int)MDCErrors.App_Cum_Billing_Read), ex);
            }
        }

        public BillingData GetCumulativeBillingData_KESC(GetTariff tariff)
        {
            try
            {
                BillingData billdata = new BillingData();
                InstantaneousController obj_instantaneousController = new InstantaneousController();
                Instantaneous_Class obj_instantaneousClass = new Instantaneous_Class();
                Param_Clock_Caliberation clock_param = new Param_Clock_Caliberation();
                ParameterController Param_Controller = new ParameterController();

                Param_Controller.AP_Controller = this.AP_Controller;
                obj_instantaneousController.AP_Controller = this.AP_Controller;
                Param_Controller.GET_MeterClock_Date_Time(ref clock_param);
                billdata.TimeStamp = clock_param.Set_Time;

                #region Active Energy Positive

                BillingItem item = new BillingItem();
                item.Name = "Active Energy Positive";

                if (tariff.T1)
                {
                    item.Value.T1 = (obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Active_Energy_Positive_T1, 2)) / 1000;
                }
                if (tariff.T2)
                {
                    item.Value.T2 = (obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Active_Energy_Positive_T2, 2)) / 1000;

                }
                if (tariff.T3)
                {
                    item.Value.T3 = (obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Active_Energy_Positive_T3, 2)) / 1000;

                }
                if (tariff.T4)
                {
                    item.Value.T4 = (obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Active_Energy_Positive_T4, 2)) / 1000;

                }
                if (tariff.TL)
                {
                    item.Value.TL = (obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Active_Energy_Positive_TL, 2)) / 1000;

                }

                billdata.BillingItems.Add(item);

                #endregion

                #region Active Energy Negative

                item = new BillingItem();
                item.Name = "Active Energy Negative";

                if (tariff.T1)
                {
                    item.Value.T1 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Active_Energy_Negative_T1, 2) / 1000;

                }
                if (tariff.T2)
                {
                    item.Value.T2 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Active_Energy_Negative_T2, 2) / 1000;

                }
                if (tariff.T3)
                {
                    item.Value.T3 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Active_Energy_Negative_T3, 2) / 1000;

                }
                if (tariff.T4)
                {
                    item.Value.T4 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Active_Energy_Negative_T4, 2) / 1000;

                }
                if (tariff.TL)
                {
                    item.Value.TL = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Active_Energy_Negative_TL, 2) / 1000;

                }

                billdata.BillingItems.Add(item);

                #endregion

                #region Reactive Energy Positive

                item = new BillingItem();
                item.Name = "Reactive Energy Positive";

                if (tariff.T1)
                {
                    item.Value.T1 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Reactive_Energy_Positive_T1, 2) / 1000;

                }
                if (tariff.T2)
                {
                    item.Value.T2 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Reactive_Energy_Positive_T2, 2) / 1000;

                }
                if (tariff.T3)
                {
                    item.Value.T3 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Reactive_Energy_Positive_T3, 2) / 1000;

                }
                if (tariff.T4)
                {
                    item.Value.T4 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Reactive_Energy_Positive_T4, 2) / 1000;

                }
                if (tariff.TL)
                {
                    item.Value.TL = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Reactive_Energy_Positive_TL, 2) / 1000;

                }

                billdata.BillingItems.Add(item);
                #endregion

                #region Reactive Energy Negative
                item = new BillingItem();
                item.Name = "Reactive Energy Negative";

                if (tariff.T1)
                {
                    item.Value.T1 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Reactive_Energy_Negative_T1, 2) / 1000;

                }
                if (tariff.T2)
                {
                    item.Value.T2 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Reactive_Energy_Negative_T2, 2) / 1000;

                }
                if (tariff.T3)
                {
                    item.Value.T3 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Reactive_Energy_Negative_T3, 2) / 1000;

                }
                if (tariff.T4)
                {
                    item.Value.T4 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Reactive_Energy_Negative_T4, 2) / 1000;

                }
                if (tariff.TL)
                {
                    item.Value.TL = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Reactive_Energy_Negative_TL, 2) / 1000;

                }

                billdata.BillingItems.Add(item);
                #endregion

                #region Active MDI
                item = new BillingItem();
                item.Name = "Active MDI";

                if (tariff.T1)
                {
                    item.Value.T1 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_MDI_Absolute_T1, 2) / 1000;

                }
                if (tariff.T2)
                {
                    item.Value.T2 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_MDI_Absolute_T2, 2) / 1000;

                }
                if (tariff.T3)
                {
                    item.Value.T3 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_MDI_Absolute_T3, 2) / 1000;

                }
                if (tariff.T4)
                {
                    item.Value.T4 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_Tariff4_MdiKw, 2) / 1000;

                }
                if (tariff.TL)
                {
                    item.Value.TL = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_TariffTL_MdiKw, 2) / 1000;

                }

                billdata.BillingItems.Add(item);
                #endregion

                #region Reactive MDI
                item = new BillingItem();
                item.Name = "Reactive MDI";

                if (tariff.T1)
                {
                    item.Value.T1 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_Tariff1_MdiKvar, 2) / 1000;

                }
                if (tariff.T2)
                {
                    item.Value.T2 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_Tariff2_MdiKvar, 2) / 1000;

                }
                if (tariff.T3)
                {
                    item.Value.T3 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_Tariff3_MdiKvar, 2) / 1000;

                }
                if (tariff.T4)
                {
                    item.Value.T4 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_Tariff4_MdiKvar, 2) / 1000;

                }
                if (tariff.TL)
                {
                    item.Value.TL = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_TariffTL_MdiKvar, 2) / 1000;

                }

                billdata.BillingItems.Add(item);
                #endregion

                #region  // Commented Code Region

                //#region Current Month Active MDI
                //item = new BillingItem();
                //item.Name = "Current Month Active MDI";
                //// Class_4 obj;
                //item.ValueInfo.Clear();
                //if (tariff.T1)
                //{
                //    item.Value.T1 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_Tariff1_CurrentMonthMdiKw, 2) / 1000;
                //    //item.Value.T1 = obj.Value;
                //    //item.Value.CaptureTimeStamp[0]=(obj.Date_Time_Stamp.GetDateTime());
                //}
                //if (tariff.T2)
                //{
                //    item.Value.T2 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_Tariff2_CurrentMonthMdiKw, 2) / 1000;
                //    // item.Value.T2 = obj.Value;
                //    // item.Value.CaptureTimeStamp[1]=(obj.Date_Time_StaetDateTime());

                //}
                //if (tariff.T3)
                //{
                //    item.Value.T3 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_Tariff3_CurrentMonthMdiKw, 2) / 1000;

                //    //item.Value.T3 = obj.Value;
                //    //item.Value.CaptureTimeStamp[2]=(obj.Date_Time_StamtDateTime());

                //}
                //if (tariff.T4)
                //{
                //    item.Value.T4 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_Tariff4_CurrentMonthMdiKw, 2) / 1000;

                //    //item.Value.T4 = obj.Value;
                //    //item.Value.CaptureTimeStamp[3]=(obj.Date_Time_StamtDateTime());

                //}
                //if (tariff.TL)
                //{
                //    item.Value.TL = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Cumulative_TariffTL_CurrentMonthMdiKw, 2) / 1000;

                //    //item.Value.TL = obj.Value;
                //    //item.Value.CaptureTimeStamp[4]=(obj.Date_Time_Stamp.GetDateTime());

                //}

                //billdata.BillingItems.Add(item);
                //#endregion

                #region Current Month Reactive MDI
                item = new BillingItem();
                item.Name = "Current Month Reactive MDI";

                if (tariff.T1)
                {
                    item.Value.T1 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Monthly_Reactive_MDI_Absolute_T1, 2) / 1000;

                    //item.Value.T1 = obj.Value;
                    //item.Value.CaptureTimeStamp[0]=(obj.Date_Time_StamtDateTime());

                }
                if (tariff.T2)
                {
                    item.Value.T2 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Monthly_Reactive_MDI_Absolute_T2, 2) / 1000;

                    //item.Value.T2 = obj.Value;
                    //item.Value.CaptureTimeStamp[1]=(obj.Date_Time_StamtDateTime());

                }
                if (tariff.T3)
                {
                    item.Value.T3 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Monthly_Reactive_MDI_Absolute_T3, 2) / 1000;

                    //item.Value.T3 = obj.Value;
                    //item.Value.CaptureTimeStamp[2]=(obj.Date_Time_StamtDateTime());

                }
                if (tariff.T4)
                {
                    item.Value.T4 = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Monthly_Reactive_MDI_Absolute_T4, 2) / 1000;

                    //item.Value.T4 = obj.Value;
                    //item.Value.CaptureTimeStamp[3]=(obj.Date_Time_StamtDateTime());

                }
                if (tariff.TL)
                {
                    item.Value.TL = obj_instantaneousController.GET_Any(obj_instantaneousClass, Get_Index.Monthly_Reactive_MDI_Absolute_TL, 2) / 1000;

                    //item.Value.TL = obj.Value;
                    //item.Value.CaptureTimeStamp[4]=(obj.Date_Time_Stamp.GetDateTime());

                }

                billdata.BillingItems.Add(item);
                #endregion
                #endregion

                return billdata;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception("Error occurred reading Cumulative Monthly Billing data", ex);
                else
                    throw new Exception("Error occurred reading Cumulative Monthly Billing data", ex);

            }
        }

        public Cumulative_billing_data GetCumulativeBillingDataDDS()
        {
            try
            {
                return GetBillingData(0xFF);
                //const int kWConversionConstant = 1000;
                //Cumulative_billing_data cummBilling = new Cumulative_billing_data();

                //cummBilling.date = GET_MeterClock(Get_Index.Meter_Clock);

                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff1_KwhAbsolute, 2);
                //cummBilling.activeEnergy_T1 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff1_KwhAbsolute, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff2_KwhAbsolute, 2);
                //cummBilling.activeEnergy_T2 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff2_KwhAbsolute, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff3_KwhAbsolute, 2);
                //cummBilling.activeEnergy_T3 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff3_KwhAbsolute, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff4_KwhAbsolute, 2);
                //cummBilling.activeEnergy_T4 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff4_KwhAbsolute, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_TariffTL_KwhAbsolute, 2);
                //cummBilling.activeEnergy_TL = TryGET_Any(cummBilling, Get_Index.Cumulative_TariffTL_KwhAbsolute, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff1_KvarhAbsolute, 2);
                //cummBilling.reactiveEnergy_T1 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff1_KvarhAbsolute, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff2_KvarhAbsolute, 2);
                //cummBilling.reactiveEnergy_T2 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff2_KvarhAbsolute, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff3_KvarhAbsolute, 2);
                //cummBilling.reactiveEnergy_T3 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff3_KvarhAbsolute, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff4_KvarhAbsolute, 2);
                //cummBilling.reactiveEnergy_T4 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff4_KvarhAbsolute, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_TariffTL_KvarhAbsolute, 2);
                //cummBilling.reactiveEnergy_TL = TryGET_Any(cummBilling, Get_Index.Cumulative_TariffTL_KvarhAbsolute, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff1_CurrentMonthMdiKw, 2);
                //cummBilling.activeMDI_T1 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff1_CurrentMonthMdiKw, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff2_CurrentMonthMdiKw, 2);
                //cummBilling.activeMDI_T2 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff2_CurrentMonthMdiKw, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff3_CurrentMonthMdiKw, 2);
                //cummBilling.activeMDI_T3 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff3_CurrentMonthMdiKw, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff4_CurrentMonthMdiKw, 2);
                //cummBilling.activeMDI_T4 = TryGET_Any(cummBilling, Get_Index.Cumulative_Tariff4_CurrentMonthMdiKw, 3) / kWConversionConstant;
                //TryGET_Any(cummBilling, Get_Index.Cumulative_TariffTL_CurrentMonthMdiKw, 2);
                //cummBilling.activeMDI_TL = TryGET_Any(cummBilling, Get_Index.Cumulative_TariffTL_CurrentMonthMdiKw, 3) / kWConversionConstant;

                //return cummBilling;
            }
            catch (Exception ex) { throw new Exception(string.Format("Error occurred reading Cumulative Billing data (Error Code:{0})", (MDCErrors.App_Cum_Billing_Read)), ex); }
        }

        public Cumulative_billing_data GetBillingData(byte F_Field)
        {
            try
            {
                const int kWConversionConstant = 1000;

                Cumulative_billing_data cummBilling = new Cumulative_billing_data();
                StOBISCode TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Meter_Clock);
                if (F_Field != CumulativeBilling)
                {
                    TargetObisIndex = AP_Controller.GetOBISCode(Get_Index._Last_MDI_Reset_Date_Time);
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                }
                cummBilling.date = GET_MeterClock(TargetObisIndex);

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Active_Energy_Absolute_T1);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.activeEnergy_T1 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Active_Energy_Absolute_T2);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.activeEnergy_T2 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Active_Energy_Absolute_T3);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.activeEnergy_T3 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Active_Energy_Absolute_T4);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.activeEnergy_T4 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Active_Energy_Absolute_TL);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.activeEnergy_TL = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Reactive_Energy_Absolute_T1);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.reactiveEnergy_T1 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Reactive_Energy_Absolute_T2);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.reactiveEnergy_T2 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Reactive_Energy_Absolute_T3);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.reactiveEnergy_T3 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Reactive_Energy_Absolute_T4);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.reactiveEnergy_T4 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Reactive_Energy_Absolute_TL);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.reactiveEnergy_TL = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Monthly_Active_MDI_Absolute_T1);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.activeMDI_T1 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Monthly_Active_MDI_Absolute_T2);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.activeMDI_T2 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Monthly_Active_MDI_Absolute_T3);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.activeMDI_T3 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Monthly_Active_MDI_Absolute_T4);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.activeMDI_T4 = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                TargetObisIndex = AP_Controller.GetOBISCode(Get_Index.Monthly_Active_MDI_Absolute_TL);
                if (F_Field != CumulativeBilling)
                    TargetObisIndex = TargetObisIndex.Set_OBISCode_Feild_F(F_Field);
                TryGET_Any(cummBilling, TargetObisIndex, 2);
                cummBilling.activeMDI_TL = TryGET_Any(cummBilling, TargetObisIndex, 3) / kWConversionConstant;

                return cummBilling;
            }
            catch (Exception ex) { throw new Exception(string.Format("Error occurred reading Cumulative Billing data (Error Code:{0})", (MDCErrors.App_Cum_Billing_Read)), ex); }
        }

        public Cumulative_billing_data ReadCummulativeBillingsDataDDS_SinglePhase()
        {
            const int DividerkW = 1000;
            Cumulative_billing_data BillingValues = new Cumulative_billing_data();

            BillingValues.activeMDI_T2 = BillingValues.activeMDI_T3 = BillingValues.activeMDI_T4 =
            BillingValues.activeEnergy_T2 = BillingValues.activeEnergy_T3 = BillingValues.activeEnergy_T4 =
            BillingValues.reactiveEnergy_T1 = BillingValues.reactiveEnergy_T2 = BillingValues.reactiveEnergy_T3 =
            BillingValues.reactiveEnergy_T4 = BillingValues.reactiveEnergy_TL = Double.NaN;


            BillingValues.date = GET_MeterClock(Get_Index.Meter_Clock);

            TryGET_Any(BillingValues, Get_Index.Active_Energy_Absolute_TL, 2);
            BillingValues.activeEnergy_T1 = BillingValues.activeEnergy_TL = TryGET_Any(BillingValues, Get_Index.Active_Energy_Absolute_TL, 3) / DividerkW;
            TryGET_Any(BillingValues, Get_Index.Monthly_Active_MDI_Absolute_TL, 2);
            BillingValues.activeMDI_T1 = BillingValues.activeMDI_TL = TryGET_Any(BillingValues, Get_Index.Current_Max_demand_KW, 3) / DividerkW;


            return BillingValues;
        }

        public Cumulative_billing_data ReadCummulativeBillingDataByOBISList()
        {
            Cumulative_billing_data cumulative_Billing_Data = new Cumulative_billing_data();
            Dictionary<string, double> ReadValues = new Dictionary<string, double>();
            List<CaptureObject> captureObjectsList = Configurator.GetProfileCaptureObjectList(CurrentConnectionInfo, Get_Index.CumulativeBilling);

            cumulative_Billing_Data.date = GET_MeterClock(Get_Index.Meter_Clock);

            foreach (var captureObj in captureObjectsList)
            {
                try
                {
                    if (captureObj.StOBISCode.OBISIndex.Equals(Get_Index.Meter_Clock)) continue;

                    if (string.IsNullOrEmpty(captureObj.DatabaseFieldName)) continue;

                    //GETDouble_Any(captureObj.StOBISCode, 2);
                    double val = (GETDouble_Any(captureObj.StOBISCode, 2));

                    val = Commons.ApplyMultiplier(val, captureObj.Multiplier);

                    if (ReadValues.ContainsKey(captureObj.DatabaseFieldName))
                        ReadValues[captureObj.DatabaseFieldName] += val;
                    else
                        ReadValues.Add(captureObj.DatabaseFieldName, val);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error While Reading Cumm data By OBIS Code", ex);
                }
            }

            if (ReadValues.Keys.Count > 0)
            {
                foreach (var key in ReadValues.Keys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        double val = ReadValues[key];
                        cumulative_Billing_Data.DBColumns.Append($"{key},");
                        cumulative_Billing_Data.DBValues.Append($"{val}, ");
                        cumulative_Billing_Data.DB_Columns_Values.Append($"{key} = {val}, ");
                    }
                }
            }

            return cumulative_Billing_Data;
        }

        #endregion

        #region GetDateTime

        public DateTime GET_MeterClock(StOBISCode OBIS_Index)
        {
            try
            {
                // Get RTC T                
                Param_Clock_Caliberation MeterClockParam = new Param_Clock_Caliberation();
                Base_Class MeterClock_CommObj = GET_Object(OBIS_Index.OBISIndex, 0x02);
                MeterClockParam.Decode_Date_Time(MeterClock_CommObj);
                return MeterClockParam.Set_Time;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting RTC Date Time", ex);
            }
        }

        #endregion

        #region Monthly_Billing_Data

        #region GetBillingData
        public Monthly_Billing_data GetBillingDataMethod6(uint DBCounter, uint MeterCounter, byte MaxBillingMonths = 12)
        {
            try
            {
                Monthly_Billing_data mBilling = new Monthly_Billing_data();
                m_data cummBilling = null;
                uint difference = MeterCounter - DBCounter;
                if (difference > MaxBillingMonths)
                {
                    DBCounter += (difference - MaxBillingMonths);
                }
                for (uint Month = 1; Month <= (MeterCounter - DBCounter); Month++)
                {
                    byte F_Field = (byte)(Month > MaxBillingMonths ? Month % MaxBillingMonths : Month);
                    cummBilling = new m_data();
                    cummBilling.Counter = DBCounter + Month;
                    cummBilling.billData_obj = GetBillingData(F_Field);
                    mBilling.monthly_billing_data.Add(cummBilling);
                }
                return mBilling;
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Reading Monthly Billing Data.", ex);
            }
        }
        public Monthly_Billing_data GetBillingDataCustomMethod(uint DBCounter, uint MeterCounter, byte MaxBillingMonths = 12)
        {
            try
            {
                Monthly_Billing_data mBilling = new Monthly_Billing_data();
                m_data cummBilling = null;
                uint difference = MeterCounter - DBCounter;
                if (difference > MaxBillingMonths)
                {
                    DBCounter += (difference - MaxBillingMonths);
                }
                for (uint Month = 1; Month <= (MeterCounter - DBCounter); Month++)
                {
                    byte F_Field = (byte)(Month > MaxBillingMonths ? Month % MaxBillingMonths : Month);
                    cummBilling = new m_data();
                    cummBilling.Counter = DBCounter + Month;
                    cummBilling.billData_obj = GetBillingData(F_Field);
                    mBilling.monthly_billing_data.Add(cummBilling);
                }
                return mBilling;
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Reading Monthly Billing Data.", ex);
            }
        }
        public Monthly_Billing_data GetBillingDataDetail(uint DBCounter, uint MeterCounter, byte MaxBillingMonths = 12)
        {
            try
            {
                Monthly_Billing_data mBilling = new Monthly_Billing_data();
                m_data cummBilling = null;
                uint difference = MeterCounter - DBCounter;
                if (difference > MaxBillingMonths)
                {
                    DBCounter += (difference - MaxBillingMonths);
                }
                for (uint Month = DBCounter + 1; Month <= MeterCounter; Month++)
                {
                    byte F_Field = (byte)(Month > MaxBillingMonths ? Month % MaxBillingMonths : Month);
                    cummBilling = new m_data();
                    cummBilling.Counter = DBCounter + Month;
                    cummBilling.billData_obj = GetBillingData(F_Field);
                    mBilling.monthly_billing_data.Add(cummBilling);
                }
                return mBilling;
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Reading Monthly Billing Data.", ex);
            }
        }
        public List<BillingData> GetBillingData(Profile_Counter MB_Counter, READ_METHOD read_MB)
        {

            try
            {
                Class_7 Billing_CommObj = (Class_7)GetSAPEntry(Get_Index.Billing_Periods_Data);
                GetBillingInfo(ref BillingInfo, Billing_CommObj);

                try
                {
                    BillingInfo.SortMethod = Get_Billing_SortMethod();
                }
                catch (Exception)
                {
                    BillingInfo.SortMethod = SortMethod.FIFO;
                }

                IAccessSelector BillingSelector = null;
                if (read_MB == READ_METHOD.ByCounter)
                {
                    uint maxMonthlyDataCount = MB_Counter.Max_Size;
                    uint currentMonthlyBillingCounter = MB_Counter.Meter_Counter;
                    uint lastMonthBillingCounter = MB_Counter.DB_Counter;

                    #region ///Tries to read monthly billing counter
                    // = Get_BillingCounter_Internal();
                    if (currentMonthlyBillingCounter == 0)
                    {
                        return new List<BillingData>();
                    }
                    #endregion
                    #region ///Hard Code Entries In Use Here
                    BillingInfo.EntriesInUse = (currentMonthlyBillingCounter >= maxMonthlyDataCount) ? maxMonthlyDataCount : currentMonthlyBillingCounter;
                    BillingSelector = ComputeAccessSelector(lastMonthBillingCounter, currentMonthlyBillingCounter);
                    ///*** Modification Comment Message
                    ///Communicator.MTI_MDC.Program.WriteLine(String.Format("{0} Access Selector", BillingSelector.ToString()));
                    #endregion
                }
                else
                {
                    BillingSelector = ComputeRangeSelector(MB_Counter.LastReadTime, DateTime.Now);
                }

                return ReadAndFormatBillingData(BillingSelector, Billing_CommObj);

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred reading Monthly Billing data,{0}", ex.Message), ex);
            }
        }

        public SortMethod Get_Billing_SortMethod()
        {
            try
            {
                SortMethod method = SortMethod.LIFO;
                Class_7 Billing_CommObj = (Class_7)GetSAPEntry(Get_Index.Billing_Periods_Data);

                /////Request Billing Sort Method
                if (Billing_CommObj.IsAttribReadable(0x05))
                {
                    if (Billing_CommObj.GetAttributeDecodingResult(0x05) != DecodingResult.Ready)
                    {
                        Billing_CommObj = (Class_7)GET_Object(Get_Index.Billing_Periods_Data, 0x05);
                    }
                    if (Billing_CommObj.GetAttributeDecodingResult(0x05) == DecodingResult.Ready)
                        method = Billing_CommObj.sortMethod;
                    else
                        throw new Exception(String.Format("Error reading Billing Sort Method {0}",
                            Billing_CommObj.GetAttributeDecodingResult(0x05)));
                }

                return method;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred reading Billing Info data", ex);
            }
        }

        private List<BillingData> ReadAndFormatBillingData(IAccessSelector AccessSelector, Class_7 Billing_CommObj)
        {
            #region ///Init Billing Capture List Objects
            if (Billing_CommObj.IsCaptureObjectListIntialized)
            {
                Billing_CommObj.captureObjectsList = new List<CaptureObject>(Billing_CommObj.captureObjectsList);
                Billing_CommObj.BaseClassCaptureObjectsList = Billing_CommObj.InitializeCaptureBuffer(1);

                #region CheckIfRangeSelector

                //if (AccessSelector.GetType() == typeof(RangeDescripter))
                //{
                //    ((RangeDescripter)AccessSelector).Capture_Object_Definition = Billing_CommObj.captureObjectsList;
                //}

                #endregion
            }
            #endregion

            List<ILValue[]> billPeriods = ReadBillingDataRaw(AccessSelector, Billing_CommObj);
            ///Save Incoming Raw Data
            List<BillingData> formattedCumulativeBillData = FormatBillingData(billPeriods);
            return formattedCumulativeBillData;
        }

        public List<BillingData> GetBillingData(IAccessSelector dataSelection)
        {
            try
            {
                IBillingDataFormatter dtFormat = new MonthlyBillingDataFormatter();
                //((MonthlyBillingDataFormatter)dtFormat).Configurations = this.Configurations;
                Class_7 Billing_CommObj = (Class_7)GetSAPEntry(Get_Index.Billing_Periods_Data);
                StOBISCode BillingObisCodes = Get_Index.Billing_Periods_Data;

                if (Billing_CommObj.OBISIndex.OBISCode_Feild_D != MonthlyBillingDFieldFilter)
                {
                    BillingObisCodes = BillingObisCodes.Set_OBISCode_Feild_D(MonthlyBillingDFieldFilter);
                    // Update SAP Object
                    Billing_CommObj = (Class_7)GetSAPEntry(BillingObisCodes.OBISIndex);
                }
                if (Billing_CommObj.DataRequestType != MonthlyBillingFilter)
                {
                    BillingObisCodes = BillingObisCodes.Set_OBISCode_Feild_F(MonthlyBillingFilter);
                }
                GetBillingInfo(ref BillingInfo, Billing_CommObj);
                Class_7 Billing_CommObjT = (Class_7)GetSAPEntry(BillingObisCodes.OBISIndex);
                ///Init Billing Capture List Objects
                if (Billing_CommObj.IsCaptureObjectListIntialized)
                {
                    Billing_CommObjT.captureObjectsList = new List<CaptureObject>(Billing_CommObj.captureObjectsList);
                    Billing_CommObj.BaseClassCaptureObjectsList = Billing_CommObjT.InitializeCaptureBuffer(1);
                }
                BillingCommObj = Billing_CommObjT;
                List<ILValue[]> billPeriods = ReadBillingDataRaw(dataSelection, Billing_CommObjT);
                ///Save Incoming Raw Data
                ///SaveRawBillingData(billPeriods, Environment.CurrentDirectory + String.Format(@"\RawMonthlyBillData.xml"));
                List<BillingData> formattedCumulativeBillData = FormatBillingData(billPeriods);
                return formattedCumulativeBillData;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occurred reading Cumulative Billing data Error Code:{0}", (int)MDCErrors.App_Monthly_Billing_Read), ex);
            }
        }

        public List<BillingData> GetBillingData()
        {
            return GetBillingData(null);
        }


        #endregion

        #region Read_BillingData

        public List<ILValue[]> ReadBillingDataRaw(IAccessSelector AccessSelector, Class_7 BillingCommObj)
        {
            try
            {
                if (BillingCommObj.IsAccessSelecterApplied(02) == SelectiveAccessType.Entry_Descripter ||
                    BillingCommObj.IsAccessSelecterApplied(02) == SelectiveAccessType.Both_Types ||
                    BillingCommObj.IsAccessSelecterApplied(02) == SelectiveAccessType.Range_Descripter)
                {
                    BillingCommObj.AccessSelector = AccessSelector;
                }
                else
                    BillingCommObj.AccessSelector = null;
                BillingCommObj.DecodingAttribute = 0x02;
                GET_Object(BillingCommObj);
                DecodingResult d = BillingCommObj.GetAttributeDecodingResult(0x02);
                // if (BillingCommObj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                if (d == DecodingResult.Ready)
                {
                    IDataGenerator BillDataGenerator = new BillingDataGenerator();
                    List<ILValue[]> billPeriods = BillDataGenerator.GetData(((Class_7)BillingCommObj).buffer);
                    return billPeriods;
                }
                else
                {
                    throw new Exception("Error occurred while reading billing data" + d);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred reading Billing data"), ex);
            }
            finally
            {
                try
                {
                    BillingCommObj.BaseClassCaptureObjectsList = null;
                    BillingCommObj.buffer.Clear();
                }
                catch (Exception)
                { }
            }
        }

        public uint Get_BillingCounter_Internal()
        {
            try
            {
                ///*** Commnet Code Temporarly Till Billing Counter Vz Works
                var RetValue = GETDouble_Any(Get_Index.Billing_Period_Counter_VZ, 0x02);

                if (RetValue == Double.NaN || RetValue == Double.PositiveInfinity || RetValue == Double.NegativeInfinity)
                {
                    throw new Exception("Error getting monthly billing internal counter");
                }
                else
                    return Convert.ToUInt32(RetValue);

            }
            catch (Exception ex)
            {
                throw new Exception("Error getting monthly billing internal counter", ex);
            }
        }

        public void GetBillingInfo(ref GenericProfileInfo BillingInfo, Class_7 Billing_CommObj)
        {
            try
            {
                if (BillingInfo == null)
                    BillingInfo = new GenericProfileInfo();
                #region Init_Billing_Capture_List
                try
                {
                    StOBISCode ProfileOBISCode = Billing_CommObj.INDEX;
                    Billing_CommObj.captureObjectsList = Configurator.GetProfileCaptureObjectList(CurrentConnectionInfo, ProfileOBISCode);
                    Billing_CommObj.BaseClassCaptureObjectsList = Billing_CommObj.InitializeCaptureBuffer(1);
                }
                catch (Exception)
                {
                    Billing_CommObj.captureObjectsList = new List<CaptureObject>();
                }
                #endregion

                // *** Temporary Running
                if (Billing_CommObj.IsAttribReadable(0x03) &&
                    !Billing_CommObj.IsCaptureObjectListIntialized)
                {
                    // Request Capture Object List
                    Billing_CommObj.DecodingAttribute = 0x03;
                    Billing_CommObj = (Class_7)GET_Object(Billing_CommObj);
                    if (Billing_CommObj.GetAttributeDecodingResult(0x03) == DecodingResult.Ready
                        && Billing_CommObj.captureObjectsList != null)
                    {
                        if (!Billing_CommObj.IsCaptureObjectListIntialized)
                        {
                            Billing_CommObj.BaseClassCaptureObjectsList = Billing_CommObj.InitializeCaptureBuffer(1);
                        }
                        Configurator.SaveProfileCaptureObjectList(CurrentConnectionInfo, Billing_CommObj.captureObjectsList, Billing_CommObj.OBISIndex, null);
                        //BillingInfo.CaptureBufferInfo = new List<Base_Class>();
                        //BillingInfo.CaptureBufferInfo.AddRange(Billing_CommObj.BaseClassCaptureObjectsList);
                    }
                    #region /// Save Billing Capture Buffer Info
                    #region Debugging & Logging
#if Enable_DEBUG_RunMode
                    try
                    {
                        StOBISCode OBIS_Index_PowerProfile = Billing_CommObj.INDEX;
                        Configurator.SaveProfileCaptureObjectList(CurrentConnectionInfo, Billing_CommObj.captureObjectsList, OBIS_Index_PowerProfile, null);

                    }
                    catch (Exception ex)
                    {
                        Commons.WriteError(String.Format("Error occurred while saving Billing Capture List Info,Error Details {0}", ex.Message));
                    }
#endif
                    #endregion
                    #endregion
                }
                else if (Billing_CommObj.IsCaptureObjectListIntialized)
                {
                    ;//BillingInfo.CaptureBufferInfo = new List<Base_Class>();
                    //BillingInfo.CaptureBufferInfo.AddRange(Billing_CommObj.BaseClassCaptureObjectsList);
                }
                else
                    throw new Exception("Error occurred reading BillingInfo,unable to read Capture ObjectInfo");

                /////Request Capture Period
                //if (Billing_CommObj.GetAttributeDecodingResult(0x04) == DecodingResult.Ready)
                //    BillingInfo.CapturePeriod = new TimeSpan(Convert.ToInt64(Billing_CommObj.capturePeriod * Math.Pow(10, 7)));
                //else if (Billing_CommObj.IsAttribReadable(0x04))
                //{
                //    Billing_CommObj.DecodingAttribute = 0x04;
                //    Billing_CommObj = (Class_7)GET_Object(Billing_CommObj);
                //    if (Billing_CommObj.GetAttributeDecodingResult(0x04) == DecodingResult.Ready)
                //        BillingInfo.CapturePeriod = new TimeSpan(Convert.ToInt64(Billing_CommObj.capturePeriod * Math.Pow(10, 7)));
                //}
                /////Request Sort Method
                //if (Billing_CommObj.GetAttributeDecodingResult(0x05) == DecodingResult.Ready)
                //    BillingInfo.SortMethod = (SortMethod)Billing_CommObj.sortMethod;
                //else if (Billing_CommObj.IsAttribReadable(0x05))
                //{
                //    Billing_CommObj.DecodingAttribute = 0x05;
                //    Billing_CommObj = (Class_7)GET_Object(Billing_CommObj);
                //    if (Billing_CommObj.GetAttributeDecodingResult(0x05) == DecodingResult.Ready)
                //        BillingInfo.SortMethod = (SortMethod)Billing_CommObj.sortMethod;
                //}
                /////Request entries_in_use
                //if (Billing_CommObj.IsAttribReadable(0x07))
                //{
                //    Billing_CommObj.DecodingAttribute = 0x07;
                //    Billing_CommObj = (Class_7)GET_Object(Billing_CommObj);
                //    if (Billing_CommObj.GetAttributeDecodingResult(0x07) == DecodingResult.Ready)
                //        BillingInfo.EntriesInUse = Billing_CommObj.entriesInUse;
                //    else
                //        BillingInfo.EntriesInUse = uint.MaxValue;
                //}
                /////Request Profile_Entries
                //if (Billing_CommObj.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                //    BillingInfo.MaxEntries = Billing_CommObj.MaxProfileEntries;
                //else if (Billing_CommObj.IsAttribReadable(0x08))
                //{
                //    Billing_CommObj.DecodingAttribute = 0x08;
                //    Billing_CommObj = (Class_7)GET_Object(Billing_CommObj);
                //    if (Billing_CommObj.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                //        BillingInfo.MaxEntries = Billing_CommObj.MaxProfileEntries;
                //    else
                //        BillingInfo.MaxEntries = uint.MaxValue;
                //}

                return;
            }
            catch (Exception ex)
            {
                throw new Exception(" Error occurred reading BillingInfo data " + ex.Message, ex);
            }
        }

        #endregion

        #region Read/Load/Save_BillingData

        /// <summary>
        /// To Store & Read Last Data Retrived From Meter
        /// </summary>
        /// <param name="CurrentDir"></param>
        /// <returns></returns>
        public List<BillingData> ReadBillingData(IAccessSelector Selector)
        {
            try
            {
                GetBillingInfo(ref BillingInfo, BillingCommObj);
                List<ILValue[]> RawBillingData = new List<ILValue[]>();
                List<ILValue[]> tList = ReadBillingDataRaw(Selector, BillingCommObj);
                uint tCurrentCount = MaxInternalBillCounter(tList);
                RawBillingData.AddRange(tList);
                List<BillingData> billData = FormatBillingData(RawBillingData);
                return billData;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to read monthly billing data", ex);
            }
        }

        public List<BillingData> FormatBillingData(List<ILValue[]> RawBillingData)
        {
            try
            {
                // Format Billing Data Object
                Func<Get_Index, StOBISCode> dlg = null;

                if (_AP_Controller != null && _AP_Controller.ApplicationProcessSAPTable != null)
                    dlg = new Func<Get_Index, StOBISCode>(_AP_Controller.GetOBISCode);

                IBillingDataFormatter dtFormat = new MonthlyBillingDataFormatter() { Configurations = this.Configurations, OBISLabelLookup = dlg };
                // ((MonthlyBillingDataFormatter)dtFormat).Configurations = this.Configurations;

                var Billing_Period_Counter_VZ = dtFormat.GetOBISCode(Get_Index.Billing_Period_Counter_VZ);
                var Daily_Billing_Period_Counter_VZ = dtFormat.GetOBISCode(Get_Index.Daily_Billing_Period_Counter_VZ);

                #region // Sort Value Based on Billing_Counter_Raw

                try
                {
                    List<uint> billingCounters = new List<uint>(24);

                    foreach (var billPeriodData in RawBillingData)
                    {
                        var IL_Value = Array.Find(billPeriodData, (w) => w.OBIS_Index == Billing_Period_Counter_VZ ||
                                                                         w.OBIS_Index == Daily_Billing_Period_Counter_VZ);
                        if (IL_Value != null)
                        {
                            uint CounterX = Convert.ToUInt32(IL_Value.Value);
                            billingCounters.Add(CounterX);
                        }
                    }

                    bool isDuplicate = false;
                    billingCounters.Sort();
                    for (int index = 0; index < billingCounters.Count - 1; index++)
                    {
                        if (billingCounters[index] == billingCounters[index + 1])
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    // Skip Sort Order
                    if (!isDuplicate)
                        RawBillingData.Sort((x, y) =>
                        {

                            var ILValX = Array.Find(x, (w) => w.OBIS_Index == Daily_Billing_Period_Counter_VZ);
                            var ILValY = Array.Find(y, (w) => w.OBIS_Index == Daily_Billing_Period_Counter_VZ);

                            if (ILValX == null || ILValY == null)
                            {
                                ILValX = Array.Find(x, (w) => w.OBIS_Index == Billing_Period_Counter_VZ);
                                ILValY = Array.Find(y, (w) => w.OBIS_Index == Billing_Period_Counter_VZ);
                            }

                            if (ILValX != null && ILValY != null)
                            {
                                uint CounterX = Convert.ToUInt32(ILValX.Value);
                                uint CounterY = Convert.ToUInt32(ILValY.Value);

                                var result = -1 * CounterX.CompareTo(CounterY);

                                return result;
                            }

                            return 0;
                        });
                }
                catch
                {
                }

                #endregion

                // ((MonthlyBillingDataFormatter)dtFormat).Configurations = this.Configurations;
                List<BillingData> formattedBillData = new List<BillingData>();
                List<BillingItem> BillingFormats = null;
                try
                {
                    BillingFormats = Configurator.GetBillingItemsFormat(CurrentConnectionInfo);
                }
                catch (Exception) { }
                finally
                {
                    if (BillingFormats == null || BillingFormats.Count < 0)
                        BillingFormats = ((MonthlyBillingDataFormatter)dtFormat).GetBillingFormatX();
                }

                // Make Bill Data In Order
                List<ILValue[]> tRawBillData = new List<ILValue[]>();
                switch (BillingInfo.SortMethod)
                {
                    case SortMethod.FIFO:
                        {
                            for (int index = RawBillingData.Count - 1; index >= 0; index--)
                            {
                                tRawBillData.Clear();
                                tRawBillData.Add(RawBillingData[index]);
                                List<BillingData> MonthlyBillPeriod = dtFormat.MakeBillingData(tRawBillData, BillingFormats);
                                formattedBillData.Add(MonthlyBillPeriod[0]);
                            }
                            break;
                        }
                    case SortMethod.LIFO:
                        {
                            for (int index = 0; index < RawBillingData.Count; index++)
                            {
                                tRawBillData.Clear();
                                tRawBillData.Add(RawBillingData[index]);
                                List<BillingData> MonthlyBillPeriod = dtFormat.MakeBillingData(tRawBillData, BillingFormats);
                                formattedBillData.Add(MonthlyBillPeriod[0]);
                            }
                            break;
                        }
                    default:
                        throw new Exception(String.Format("Sort Method {0} is not expected", BillingInfo.SortMethod));

                }
                return formattedBillData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while formatting raw monthly billing data" + ex.Message, ex);
            }
        }

        public uint MaxInternalBillCounter(List<ILValue[]> RawBillData)
        {
            try
            {
                uint Count = 0;
                foreach (var BillPeriod in RawBillData)
                {
                    ILValue Internal_Bill_Counter = Array.Find<ILValue>(BillPeriod, (x) => x.OBIS_Index == Get_Index.Billing_Period_Counter_VZ);
                    if (Internal_Bill_Counter != null)
                    {
                        uint t = Convert.ToUInt32(Internal_Bill_Counter.Value);
                        if (Count < t)
                            Count = t;
                    }
                }
                return Count;

            }
            catch
            {
                throw new Exception("Unable to calculate max Internal Billing Counter");
            }
        }

        public IAccessSelector ComputeAccessSelector(uint LastBillingCounter, uint CurrentBillCounter)
        {
            try
            {
                return ComputeAccessSelector(LastBillingCounter, CurrentBillCounter, BillingInfo);
            }
            catch
            {
                throw new Exception("Unable to compute Entry Access Selecter for billing data");
            }

        }

        public IAccessSelector ComputeAccessSelector(uint LastBillingCounter, uint CurrentBillCounter, GenericProfileInfo BillingInfo)
        {
            try
            {
                if (BillingInfo == null)
                    throw new Exception("Unable to compute Entry Access Selecter for billing data,billing Info not updated");
                EntryDescripter Selecter = new EntryDescripter();
                long DeltaBillCount = (CurrentBillCounter - LastBillingCounter);
                ///Very First Request
                if (DeltaBillCount == 0 && LastBillingCounter == 0)
                    return Selecter;
                ///Data Upto date,don't request
                if (DeltaBillCount == 0)
                    throw new Exception("Unable to compute Entry Access Selecter for billing data,records are updated");

                switch (BillingInfo.SortMethod)
                {
                    case SortMethod.FIFO:
                        {
                            Selecter.ToEntry = EntryDescripter.MaxPossibleValue;
                            long StartIndex = BillingInfo.EntriesInUse - DeltaBillCount;
                            if (StartIndex <= 0)
                            {
                                Selecter.FromEntry = 1;
                            }
                            else
                                Selecter.FromEntry = (uint)StartIndex + 1;
                            break;
                        }
                    case SortMethod.LIFO:
                        {
                            Selecter.FromEntry = 1;
                            long LastIndex = Selecter.FromEntry + DeltaBillCount;
                            if (LastIndex > BillingInfo.EntriesInUse)
                            {
                                Selecter.ToEntry = BillingInfo.EntriesInUse;
                            }
                            else
                                Selecter.ToEntry = (uint)(LastIndex - 1);
                            break;
                        }
                    default:
                        throw new Exception(String.Format("Sort Method is {0} not exptected", BillingInfo.SortMethod));
                        //break;

                }
                return Selecter;
            }
            catch
            {
                throw new Exception("Unable to compute Entry Access Selecter for billing data");
            }

        }

        public IAccessSelector ComputeRangeSelector(DateTime startTime, DateTime toTime)
        {
            RangeDescripter RSelector = new RangeDescripter()
            {
                FromEntry = startTime,
                ToEntry = toTime,
                EncodingDataType = DataTypes._A19_datetime
            };
            return RSelector;
        }

        #endregion


        #endregion

        #region Miscelleanous_Getter_Setter

        public double GETDouble_Any(StOBISCode arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DLMS_Common.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
                // return DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public double GETDouble_Any(Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DLMS_Common.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
                // return DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public double GETDouble_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public byte[] GETArray_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DataContainer_Class_obj.Decode_Any_ByteArray(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public byte[] GETArray_Any(Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DLMS_Common.Decode_Any_ByteArray(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public string GETString_Any(IDecodeAnyObject DataContainer_Class_obj, Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DataContainer_Class_obj.Decode_Any_string(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public string GETString_Any(Get_Index arg, byte attribute)
        {
            try
            {
                Base_Class Comm_Obj = GET_Object(arg, attribute);
                return DLMS_Common.Decode_Any_string(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
            }
        }

        public void GETValue_Any(IDecodeAnyObject DataContainer_Class_obj, string Data_Property, Get_Index arg, byte attribute)
        {
            bool isSuccess = false;

            try
            {
                if (string.IsNullOrEmpty(Data_Property))
                    throw new ArgumentNullException("Data_Property");
                if (DataContainer_Class_obj == null)
                    throw new ArgumentNullException("IDecodeAnyObject");

                Base_Class Comm_Obj = GET_Object(arg, attribute);
                isSuccess = DataContainer_Class_obj.TryDecode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID), DataContainer_Class_obj, Data_Property);

                isSuccess = false;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                // throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));

                throw ex;
            }
        }

        public bool TryGETValue_Any(IDecodeAnyObject DataContainer_Class_obj, string Data_Property, Get_Index arg, byte attribute)
        {
            bool isSuccess = false;

            try
            {
                if (string.IsNullOrEmpty(Data_Property))
                    throw new ArgumentNullException("Data_Property");
                if (DataContainer_Class_obj == null)
                    throw new ArgumentNullException("IDecodeAnyObject");

                Base_Class Comm_Obj = GET_Object(arg, attribute);
                isSuccess = DataContainer_Class_obj.TryDecode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID), DataContainer_Class_obj, Data_Property);


            }
            catch (Exception ex)
            {
                isSuccess = false;
                //  throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));
            }

            return isSuccess;
        }
        public double TryGET_Any(IDecodeAnyObject DataContainer_Class_obj, StOBISCode arg, byte attribute)
        {
            double temp = double.NaN;

            try
            {
                Base_Class Comm_Obj = GET_Object(arg.OBISIndex, attribute);
                temp = DataContainer_Class_obj.Decode_Any(Comm_Obj, Convert.ToByte(Comm_Obj.Class_ID));
            }
            catch (Exception ex)
            {
                temp = double.NaN;
                //  throw new Exception("Error occurred while Getting " + arg.ToString(), ex);
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Getting {0} {1}"
                                                    , arg.ToString(), ex.ToString()));
            }

            return temp;
        }

        #endregion

        #region Save to class methods
        public Cumulative_billing_data saveToClass(BillingData data, string msn)
        {
            Cumulative_billing_data obj = new Cumulative_billing_data();
            BillingItem item = new BillingItem();
            obj.msn = msn;
            //obj.reference_no = reference_no;
            obj.date = data.TimeStamp;

            foreach (BillingItem b_item in data.BillingItems)
            {
                if (!string.IsNullOrEmpty(b_item.DBColumnsString))
                {
                    obj.DBColumns.Append(b_item.DBColumnsString);
                    obj.DBValues.Append(b_item.DBValuesString);
                    obj.DB_Columns_Values.Append(b_item.DBUpdateString);
                }
            }

            item = data.BillingItems.Find(x => x.Name == "Active Energy Total");
            if (item != null)
            {
                if (!item.Value.TL.Equals(Double.NaN))
                {
                    obj.activeEnergy_T1 = item.Value.T1;
                    obj.activeEnergy_T2 = item.Value.T2;
                    obj.activeEnergy_T3 = item.Value.T3;
                    obj.activeEnergy_T4 = item.Value.T4;
                    obj.activeEnergy_TL = item.Value.TL;
                }
                else
                    obj.activeEnergy_T1 = obj.activeEnergy_TL = item.Value.T1;
            }

            item = data.BillingItems.Find(x => x.Name == "Reactive Energy Total");
            if (item != null)
            {
                if (!item.Value.TL.Equals(Double.NaN))
                {
                    obj.reactiveEnergy_T1 = item.Value.T1;
                    obj.reactiveEnergy_T2 = item.Value.T2;
                    obj.reactiveEnergy_T3 = item.Value.T3;
                    obj.reactiveEnergy_T4 = item.Value.T4;
                    obj.reactiveEnergy_TL = item.Value.TL;
                }
                else
                    obj.reactiveEnergy_T1 = obj.reactiveEnergy_TL = item.Value.T1;
            }


            item = data.BillingItems.Find(x => x.Name == "Active MDI");
            if (item != null)
            {
                if (!item.Value.TL.Equals(Double.NaN))
                {
                    obj.activeMDI_T1 = item.Value.T1;
                    obj.activeMDI_T2 = item.Value.T2;
                    obj.activeMDI_T3 = item.Value.T3;
                    obj.activeMDI_T4 = item.Value.T4;
                    obj.activeMDI_TL = item.Value.TL;
                }
                else
                    obj.activeMDI_T1 = obj.activeMDI_TL = item.Value.T1;
            }

            return obj;
        }

        public Monthly_Billing_data SaveToClass(List<BillingData> data, string msn)
        {
            Monthly_Billing_data obj = new Monthly_Billing_data();
            BillingItem item = null;

            m_data B_item = null;
            for (int i = 0; i < data.Count; i++)
            {
                item = new BillingItem();
                B_item = new m_data
                {
                    Counter = data[i].BillingCounter,
                    billData_obj =
                    {
                        msn = msn,
                        //reference_no = reference_no,
                        date = data[i].TimeStamp
                    }
                };

                foreach (BillingItem b_item in data[i].BillingItems)
                {
                    if (!string.IsNullOrEmpty(b_item.DBColumnsString))
                    {
                        B_item.billData_obj.DBColumns.Append(b_item.DBColumnsString);
                        B_item.billData_obj.DBValues.Append(b_item.DBValuesString);
                        B_item.billData_obj.DB_Columns_Values.Append(b_item.DBUpdateString);
                    }
                }

                item = data[i].BillingItems.Find(x => x.Name == "Active Energy Total");
                if (item != null)
                {
                    if (!item.Value.TL.Equals(Double.NaN))
                    {
                        B_item.billData_obj.activeEnergy_T1 = item.Value.T1;
                        B_item.billData_obj.activeEnergy_T2 = item.Value.T2;
                        B_item.billData_obj.activeEnergy_T3 = item.Value.T3;
                        B_item.billData_obj.activeEnergy_T4 = item.Value.T4;
                        B_item.billData_obj.activeEnergy_TL = item.Value.TL;
                    }
                    else
                        B_item.billData_obj.activeEnergy_T1 = B_item.billData_obj.activeEnergy_TL = item.Value.T1;
                }

                item = data[i].BillingItems.Find(x => x.Name == "Reactive Energy Total");
                if (item != null)
                {
                    if (!item.Value.TL.Equals(Double.NaN))
                    {
                        B_item.billData_obj.reactiveEnergy_T1 = item.Value.T1;
                        B_item.billData_obj.reactiveEnergy_T2 = item.Value.T2;
                        B_item.billData_obj.reactiveEnergy_T3 = item.Value.T3;
                        B_item.billData_obj.reactiveEnergy_T4 = item.Value.T4;
                        B_item.billData_obj.reactiveEnergy_TL = item.Value.TL;
                    }
                    else
                        B_item.billData_obj.reactiveEnergy_T1 = B_item.billData_obj.reactiveEnergy_TL = item.Value.T1;
                }

                item = data[i].BillingItems.Find(x => x.Name == "Active MDI");
                if (item != null)
                {
                    if (!item.Value.TL.Equals(Double.NaN))
                    {
                        B_item.billData_obj.activeMDI_T1 = item.Value.T1;
                        B_item.billData_obj.activeMDI_T2 = item.Value.T2;
                        B_item.billData_obj.activeMDI_T3 = item.Value.T3;
                        B_item.billData_obj.activeMDI_T4 = item.Value.T4;
                        B_item.billData_obj.activeMDI_TL = item.Value.TL;
                    }
                    else
                        B_item.billData_obj.activeMDI_T1 = B_item.billData_obj.activeMDI_TL = item.Value.T1;
                }
                obj.monthly_billing_data.Add(B_item);
            }

            return obj;

        }
        #endregion

        #region Single Phase
        //public Cumulative_billing_data saveToClass_SinglePhase(BillingData data, string msn)
        //{
        //    Cumulative_billing_data BillingValues = new Cumulative_billing_data();
        //    BillingItem item;
        //    BillingValues.msn = msn;
        //    foreach (BillingItem b_item in data.BillingItems)
        //    {
        //        if (!string.IsNullOrEmpty(b_item.DBColumnsString))
        //        {
        //            BillingValues.DBColumns.Append(b_item.DBColumnsString);
        //            BillingValues.DBValues.Append(b_item.DBValuesString);
        //        }
        //    }
        //    BillingValues.date = data.TimeStampRaw.GetDateTime();

        //    BillingValues.activeMDI_T2 = BillingValues.activeMDI_T3 = BillingValues.activeMDI_T4 =
        //    BillingValues.activeEnergy_T2 = BillingValues.activeEnergy_T3 = BillingValues.activeEnergy_T4 =
        //    BillingValues.reactiveEnergy_T1 = BillingValues.reactiveEnergy_T2 = BillingValues.reactiveEnergy_T3 =
        //    BillingValues.reactiveEnergy_T4 = BillingValues.reactiveEnergy_TL = Double.NaN;

        //    item = data.BillingItems.Find(x => x.Name.Equals("Cumulative Active Energy"));
        //    BillingValues.activeEnergy_T1 = BillingValues.activeEnergy_TL = item.Value.TL;
        //    item = data.BillingItems.Find(x => x.Name.Equals("Active MDI"));
        //    BillingValues.activeMDI_T1 = BillingValues.activeMDI_TL = item.Value.TL;

        //    return BillingValues;
        //}

        public Monthly_Billing_data_SinglePhase saveToClass_SinglePhase(List<BillingData> data, string msn)
        {
            Monthly_Billing_data_SinglePhase obj = new Monthly_Billing_data_SinglePhase();
            try
            {

                BillingItem item = null;

                m_data_singlePhase B_item = null;
                for (int i = 0; i < data.Count; i++)
                {
                    item = new BillingItem();
                    B_item = new m_data_singlePhase();
                    B_item.Counter = data[i].BillingCounter;
                    B_item.billData_obj.msn = msn;
                    // B_item.billData_obj.reference_no = reference_no;

                    B_item.billData_obj.date = data[i].TimeStamp;

                    //item = data[i].BillingItems.Find(x => x.Name == "Cumulative Active Energy");
                    item = data[i].BillingItems.Find(x => x.Name == "Active Energy Import");
                    if (item != null)
                        B_item.billData_obj.activeEnergy = item.Value.T1;

                    item = data[i].BillingItems.Find(x => x.Name == "Active MDI");
                    if (item != null)
                        B_item.billData_obj.activeMDI = item.Value.T1;

                    item = data[i].BillingItems.Find(x => x.Name == "Current_Max_demand_KW");
                    if (item != null)
                        B_item.billData_obj.activeMDI = item.Value.T1;

                    obj.monthly_billing_data.Add(B_item);
                }
            }
            catch
            {

                throw;
            }


            return obj;

        }

        #endregion

        #region SINGLE PHASE

        private Configs configurations;
        public Configs Configurations
        {
            get { return configurations; }
            set { configurations = value; }
        }
        public BillingData GetCumulativeBillingData(bool isSinglePhase)
        {
            try
            {
                IBillingDataFormatter dtFormat = new MonthlyBillingDataFormatter();
                ((MonthlyBillingDataFormatter)dtFormat).Configurations = Configurations;
                Class_7 Billing_CommObj = (Class_7)GetSAPEntry(Get_Index.CumulativeBilling);

                GetBillingInfo(ref CumulativeBillingInfo, Billing_CommObj);
                List<ILValue[]> billPeriods = ReadBillingDataRaw(null, Billing_CommObj);

                List<BillingData> formattedBillData = null;
                //if (isSinglePhase)
                //{
                //    formattedBillData = FormatBillingData(billPeriods, true);
                //}
                //else
                {
                    formattedBillData = FormatBillingData(billPeriods);
                }

                return formattedBillData[0];
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occurred reading Cumulative Monthly Billing data (Error Code:{0})", (MDCErrors.App_Cum_Billing_Read)), ex);
            }
        }

        public List<BillingData> FormatBillingData(List<ILValue[]> RawBillingData, bool isCumulative) //for Single Phase
        {
            try
            {
                List<BillingData> list_to_return = new List<BillingData>();
                BillingData billdata = new BillingData();
                List<BillingItem> List_items;
                BillingItem item = new BillingItem();
                ILValue temp = new ILValue();
                ILValue[] value = new ILValue[1];
                List_items = billdata.BillingItems;
                if (isCumulative)
                {

                    value = RawBillingData[0]; //Instantaneous data is received in one Packet

                    list_to_return.Add(billdata);
                    #region MeterDateTime
                    //Reading Date
                    temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Meter_Clock);
                    list_to_return[0].TimeStampRaw = (StDateTime)(temp.Value);
                    #endregion

                    #region Kwh Import
                    item = new BillingItem();
                    temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Active_Energy_Positive_TL);

                    item.Name = "Cumulative Active Energy";
                    item.Unit = Unit.kWh;
                    item.Value.TL = (makeValue(temp, item.Multiplier) / 1000); //convert from wh to kwh
                    List_items.Add(item);
                    #endregion

                    #region Current Max Demand Kw
                    item = new BillingItem();
                    temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Current_Max_demand_KW);
                    //Int64 t = Convert.ToInt64( Get_Index.Current_Max_demand_KW);
                    item.Name = "Active MDI";
                    item.Unit = Unit.kW;
                    item.Value.TL = makeValue(temp, item.Multiplier) / 1000;
                    StDateTime g = (StDateTime)(temp.ValueCollection[5]);

                    item.Value.CaptureTimeStamp.Add(g.GetDateTime());
                    List_items.Add(item);
                    #endregion

                    list_to_return[0].BillingItems = List_items;

                }
                else  //MonthlyBilling
                {
                    RawBillingData.Sort((x, y) => (Convert.ToUInt32(Array.Find(x, (w) => w.OBIS_Index == Get_Index.Billing_Period_Counter_VZ).Value).
                    CompareTo(Convert.ToUInt32(Array.Find(y, (v) => v.OBIS_Index == Get_Index.Billing_Period_Counter_VZ).Value))));
                    for (int i = 0; i < RawBillingData.Count; i++)
                    {
                        value = RawBillingData[i]; //Instantaneous data is received in one Packet
                        billdata = new BillingData();
                        #region MeterDateTime
                        //Reading Date
                        temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Meter_Clock);
                        billdata.TimeStampRaw = (StDateTime)(temp.Value);
                        #endregion

                        #region BillingCounterVZ
                        //Reading Date
                        temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Billing_Period_Counter_VZ);
                        billdata.BillingCounter = Convert.ToUInt16(temp.Value);
                        #endregion

                        #region Kwh Import
                        item = new BillingItem();
                        temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Active_Energy_Positive_TL);
                        item.Name = "Cumulative Active Energy";
                        item.Unit = Unit.kWh;
                        item.Value.TL = (makeValue(temp, item.Multiplier) / 1000); //convert from wh to kwh
                        List_items.Add(item);
                        #endregion

                        #region Current Max Demand Kw
                        temp = Array.Find(value, x => x.OBIS_Index == Get_Index.Current_Max_demand_KW);
                        if (temp != null)
                        {
                            item = new BillingItem();
                            item.Name = "Active MDI";
                            item.Unit = Unit.kW;
                            item.Value.TL = makeValue(temp, item.Multiplier) / 1000;
                            StDateTime g = (StDateTime)(temp.ValueCollection[5]);

                            item.Value.CaptureTimeStamp.Add(g.GetDateTime());
                            List_items.Add(item);
                        }
                        #endregion

                        List<BillingItem> tempList = new List<BillingItem>();
                        foreach (BillingItem itemX in List_items)
                        {
                            tempList.Add(itemX);
                        }
                        billdata.BillingItems = tempList;

                        list_to_return.Add(billdata);
                        List_items.Clear();
                    }
                }
                return list_to_return;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private double makeValue(ILValue val, float multiplier)
        {
            try
            {
                if (val != null)
                {
                    double rawVal = Convert.ToDouble(val.Value);
                    if (multiplier < 0)
                    {
                        rawVal = rawVal / (Math.Pow(10, Math.Abs(multiplier)));
                    }
                    else if (multiplier > 0)
                    {
                        rawVal = rawVal * (Math.Pow(10, Math.Abs(multiplier)));
                    }
                    return rawVal;
                }
                else
                    return double.NaN;

            }
            catch
            {
                return double.PositiveInfinity;
            }
        }

        public List<BillingData> GetMonthlyBillingData(IAccessSelector dataSelection, bool isSinglePhase)
        {
            try
            {
                IBillingDataFormatter dtFormat = new MonthlyBillingDataFormatter();
                ((MonthlyBillingDataFormatter)dtFormat).Configurations = this.Configurations;
                Class_7 Billing_CommObj = (Class_7)GetSAPEntry(Get_Index.Billing_Periods_Data);
                StOBISCode BillingObisCodes = Get_Index.Billing_Periods_Data;
                if (Billing_CommObj.DataRequestType != MonthlyBillingFilter)
                {
                    BillingObisCodes = BillingObisCodes.Set_OBISCode_Feild_F(MonthlyBillingFilter);
                }
                GetBillingInfo(ref BillingInfo, Billing_CommObj);
                Class_7 Billing_CommObjT = (Class_7)GetSAPEntry(BillingObisCodes.OBISIndex);
                ///Init Billing Capture List Objects
                if (Billing_CommObj.IsCaptureObjectListIntialized)
                {
                    Billing_CommObjT.captureObjectsList = new List<CaptureObject>(Billing_CommObj.captureObjectsList);
                    Billing_CommObjT.InitializeCaptureBuffer(1);
                }
                BillingCommObj = Billing_CommObjT;
                List<ILValue[]> billPeriods = ReadBillingDataRaw(dataSelection, Billing_CommObjT);
                ///Save Incoming Raw Data
                ///SaveRawBillingData(billPeriods, Environment.CurrentDirectory + String.Format(@"\RawMonthlyBillData.xml"));
                List<BillingData> formattedCumulativeBillData = null;

                formattedCumulativeBillData = FormatBillingData(billPeriods, false);

                return formattedCumulativeBillData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred reading Cumulative Billing data", ex);
            }
        }

        public List<BillingData> GetBillingData(bool isSinglePhase)
        {
            return GetBillingData(null); //GetMonthlyBillingData(null, isSinglePhase);
        }

        #endregion

        #region Hard_Code_Capture_Object_List

        public List<CaptureObject> InitializeBillingObjectList(bool isMonthly)
        {
            List<CaptureObject> listToReturn = new List<CaptureObject>();
            CaptureObject Obj_1 = new CaptureObject();

            StOBISCode OBis = Get_Index.Meter_Clock;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x02;
            Obj_1.DataIndex = 0;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            if (isMonthly)
            {
                Obj_1 = new CaptureObject();
                OBis = Get_Index.Billing_Period_Counter_VZ;
                Obj_1.ClassId = OBis.ClassId;
                Obj_1.AttributeIndex = 0x02;
                Obj_1.OBISCode = OBis.OBISCode;
                listToReturn.Add(Obj_1);
            }

            // Cumulative_TariffTL_KwhAbsolute              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Energy_Absolute_TL;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff1_KwhAbsolute              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Energy_Absolute_T1;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff2_KwhAbsolute              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Energy_Absolute_T2;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff3_KwhAbsolute              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Energy_Absolute_T3;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff4_KwhAbsolute              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Active_Energy_Absolute_T4;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_TariffTL_KvarhAbsolute              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Energy_Absolute_TL;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff1_KvarhAbsolute              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Energy_Absolute_T1;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff2_KvarhAbsolute              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Energy_Absolute_T2;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff3_KvarhAbsolute              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Energy_Absolute_T3;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff4_KvarhAbsolute              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Reactive_Energy_Absolute_T4;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_TariffTL_MdiKw              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Cumulative_TariffTL_MdiKw;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff1_MdiKw              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Cumulative_MDI_Absolute_T1;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff2_MdiKw              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Cumulative_MDI_Absolute_T2;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff3_MdiKw              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Cumulative_MDI_Absolute_T3;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            // Cumulative_Tariff4_MdiKw              Class id 3   Attribut 00  Data Index 00
            Obj_1 = new CaptureObject();
            OBis = Get_Index.Cumulative_Tariff4_MdiKw;
            Obj_1.ClassId = OBis.ClassId;
            Obj_1.AttributeIndex = 0x00;
            Obj_1.OBISCode = OBis.OBISCode;
            listToReturn.Add(Obj_1);

            return listToReturn;
        }

        #endregion
    }

}
