using System;
using System.Collections.Generic;
using DLMS;
using DLMS.Comm;
using DatabaseConfiguration.DataSet;
using SharedCode.Comm.DataContainer;
//using AccurateOptocomSoftware.comm;
//using SharedCode.Comm.DataContainer;
//using DatabaseConfiguration.DataSet;

namespace SharedCode.Comm.HelperClasses
{
    public class MonthlyBillingDataFormatter : IBillingDataFormatter
    {
        #region Data_Members

        private Configs Configurator;

        #endregion

        public Configs Configurations
        {
            get { return Configurator; }
            set { Configurator = value; }
        }

        public Func<Get_Index, StOBISCode> OBISLabelLookup;

        #region IBillingDataFormatter Members

        public StOBISCode GetOBISCode(Get_Index OBIS_Index)
        {
            StOBISCode obisCode = Get_Index.Dummy;

            try
            {
                if (OBISLabelLookup == null)
                    throw new NotImplementedException("OBISLabelLookup");

                obisCode = OBISLabelLookup.Invoke(OBIS_Index);
            }
            catch (Exception ex)
            {
#if DEBUG

                System.Diagnostics.Debug.WriteIf(true, string.Format("{0} OBIS Code Not Found {1}", OBIS_Index.ToString(), ex.Message));

#endif
                throw ex;
            }

            return obisCode;
        }

        public List<BillingData> MakeBillingData(List<ILValue[]> CommObjs)
        {
            return MakeBillingData(CommObjs, null);
        }

        public List<BillingData> MakeBillingData(List<ILValue[]> CommObjs, List<BillingItem> BillingFormats)
        {
            try
            {
                if (CommObjs == null || CommObjs.Count <= 0)
                    throw new Exception("XX");
                List<BillingData> BillData = new List<BillingData>();
                try
                {
                    if (BillingFormats.Count == 0)
                    {
                        BillingFormats = null;
                        BillingFormats = GetBillingFormatX();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #region OBIS_CodesLst_To_Match

                var Meter_Clock = GetOBISCode(Get_Index.Meter_Clock);
                var Billing_Period_Counter_VZ = GetOBISCode(Get_Index.Billing_Period_Counter_VZ);
                var Daily_Billing_Period_Counter_VZ = GetOBISCode(Get_Index.Daily_Billing_Period_Counter_VZ);

                StOBISCode Tariff_T1 = Get_Index.Dummy;
                StOBISCode Tariff_T2 = Get_Index.Dummy;
                StOBISCode Tariff_T3 = Get_Index.Dummy;
                StOBISCode Tariff_T4 = Get_Index.Dummy;
                StOBISCode Tariff_TL = Get_Index.Dummy;

                #endregion


                uint count = 1;
                foreach (var billingPeriod in CommObjs)
                {
                    BillingData billPeriodData = new BillingData();
                    billPeriodData.BillingCounter = count;
                    billPeriodData.RawBilling = billingPeriod;
                    
                    #region Store Fixed Objects

                    // Store Billing Date & Time Stamp
                    ILValue TVal = Array.Find<ILValue>(billingPeriod, (x) => x.OBIS_Index == Meter_Clock);
                    if (TVal != null && TVal.Value != null && TVal.Value.GetType() == typeof(StDateTime))
                    {
                        StDateTime timeStamp = (StDateTime)TVal.Value;
                        billPeriodData.TimeStampRaw = timeStamp;
                        if (timeStamp.IsDateTimeConvertible)
                            billPeriodData.TimeStamp = timeStamp.GetDateTime();
                        else if (timeStamp.IsDateConvertible)
                            billPeriodData.TimeStamp = timeStamp.GetDateTime();
                        else
                            billPeriodData.TimeStamp = DateTime.MinValue;

                    }
                    else
                        billPeriodData.TimeStamp = DateTime.MinValue;

                    // Store Billing Period Counter
                    TVal = Array.Find<ILValue>(billingPeriod, (x) => x.OBIS_Index == Billing_Period_Counter_VZ ||
                                                                     x.OBIS_Index == Daily_Billing_Period_Counter_VZ);
                    if (TVal != null && TVal.Value != null)
                    {
                        billPeriodData.BillingCounter = Convert.ToUInt32(TVal.Value);
                    }
                    else
                        billPeriodData.BillingCounter = 0;

                    #endregion
                    
                    // Make Billing Period Data
                    foreach (var item in BillingFormats)
                    {
                        try
                        {
                            List<ILValue> ilValues = new List<ILValue>();
                            List<Get_Index> Value_Info = new List<Get_Index>();
                            if (item.Value != null)
                            {

                                if (item.ValueInfo.Count > 1)
                                {
                                    Tariff_T1 = GetOBISCode((Get_Index)item.ValueInfo[Tariff.T1_Index]);
                                    Tariff_T2 = GetOBISCode((Get_Index)item.ValueInfo[Tariff.T2_Index]);
                                    Tariff_T3 = GetOBISCode((Get_Index)item.ValueInfo[Tariff.T3_Index]);
                                    Tariff_T4 = GetOBISCode((Get_Index)item.ValueInfo[Tariff.T4_Index]);
                                    Tariff_TL = GetOBISCode((Get_Index)item.ValueInfo[Tariff.TL_Index]);

                                    Value_Info = new List<Get_Index>() { Tariff_T1.OBISIndex, 
                                                                                 Tariff_T2.OBISIndex, 
                                                                                 Tariff_T3.OBISIndex, 
                                                                                 Tariff_T4.OBISIndex, 
                                                                                 Tariff_TL.OBISIndex };

                                    // Get_Index OBIS = (Get_Index)item.ValueInfo[Tariff.T1_Index];
                                    ILValue val = Array.Find<ILValue>(billingPeriod, (x) => x.OBIS_Index == Tariff_T1);
                                    if (val != null)
                                        ilValues.Add(val);


                                    // OBIS = (Get_Index)item.ValueInfo[Tariff.T2_Index];
                                    val = Array.Find<ILValue>(billingPeriod, (x) => x.OBIS_Index == Tariff_T2);
                                    if (val != null)
                                        ilValues.Add(val);


                                    // OBIS = (Get_Index)item.ValueInfo[Tariff.T3_Index];
                                    val = Array.Find<ILValue>(billingPeriod, (x) => x.OBIS_Index == Tariff_T3);
                                    if (val != null)
                                        ilValues.Add(val);


                                    // OBIS = (Get_Index)item.ValueInfo[Tariff.T4_Index];
                                    val = Array.Find<ILValue>(billingPeriod, (x) => x.OBIS_Index == Tariff_T4);
                                    if (val != null)
                                        ilValues.Add(val);

                                    // OBIS = (Get_Index)item.ValueInfo[Tariff.TL_Index];
                                    val = Array.Find<ILValue>(billingPeriod, (x) => x.OBIS_Index == Tariff_TL);
                                    if (val != null)
                                        ilValues.Add(val);

                                    
                                }
                                else
                                {
                                    Tariff_T1 = GetOBISCode((Get_Index)item.ValueInfo[Tariff.T1_Index]);
                                    Value_Info.Add(Tariff_T1.OBISIndex);

                                    ILValue val = Array.Find<ILValue>(billingPeriod, (x) => x.OBIS_Index == Tariff_T1);
                                    if (val != null)
                                        ilValues.Add(val);
                                }

                            }

                           
                            BillingItem itemFormat = (BillingItem)item.Clone();
                            itemFormat.ValueInfo = Value_Info;

                            BillingItem BillItem = makeBillingItem(ilValues, itemFormat);

                            if ((BillItem.Value.T1.Equals(Double.PositiveInfinity) || BillItem.Value.T1.Equals(Double.NaN)) &&
                                (BillItem.Value.T2.Equals(Double.PositiveInfinity) || BillItem.Value.T2.Equals(Double.NaN)) &&
                                (BillItem.Value.T3.Equals(Double.PositiveInfinity) || BillItem.Value.T3.Equals(Double.NaN)) &&
                                (BillItem.Value.T4.Equals(Double.PositiveInfinity) || BillItem.Value.T4.Equals(Double.NaN)) &&
                                (BillItem.Value.TL.Equals(Double.PositiveInfinity) || BillItem.Value.TL.Equals(Double.NaN)))
                                continue;
                            else
                            {
                                billPeriodData.BillingItems.Add(BillItem);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(String.Format("Error occurred while building data for Bill Item {0}", item), ex);
                        }
                    }

                    #region Hard_Code Compute Total Active Energy

                    // Tariff_T1 = GetOBISCode(Get_Index.Cumulative_Tariff1_KwhAbsolute);
                    // Tariff_T2 = GetOBISCode(Get_Index.Cumulative_Tariff2_KwhAbsolute);
                    // Tariff_T3 = GetOBISCode(Get_Index.Cumulative_Tariff3_KwhAbsolute);
                    // Tariff_T4 = GetOBISCode(Get_Index.Cumulative_Tariff4_KwhAbsolute);
                    // Tariff_TL = GetOBISCode(Get_Index.Cumulative_TariffTL_KwhAbsolute);
                       
                       
                    // BillingItem TotalEnergy_x = billPeriodData.BillingItems.Find((x) =>
                    //                                                                 (x.ValueInfo[0] == Tariff_T1) &&
                    //                                                                 (x.ValueInfo[1] == Tariff_T2) &&
                    //                                                                 (x.ValueInfo[2] == Tariff_T3) &&
                    //                                                                 (x.ValueInfo[3] == Tariff_T4) &&
                    //                                                                 (x.ValueInfo[4] == Tariff_TL));
                       
                    // Tariff_T1 = GetOBISCode(Get_Index.Cumulative_Tariff1_KwhImport);
                    // Tariff_T2 = GetOBISCode(Get_Index.Cumulative_Tariff2_KwhImport);
                    // Tariff_T3 = GetOBISCode(Get_Index.Cumulative_Tariff3_KwhImport);
                    // Tariff_T4 = GetOBISCode(Get_Index.Cumulative_Tariff4_KwhImport);
                    // Tariff_TL = GetOBISCode(Get_Index.Cumulative_TariffTL_KwhImport);
                       
                       
                    // BillingItem ActiveEnregyImport = billPeriodData.BillingItems.Find((x) =>
                    //                                                         (x.ValueInfo[0] == Tariff_T1) &&
                    //                                                         (x.ValueInfo[1] == Tariff_T2) &&
                    //                                                         (x.ValueInfo[2] == Tariff_T3) &&
                    //                                                         (x.ValueInfo[3] == Tariff_T4) &&
                    //                                                         (x.ValueInfo[4] == Tariff_TL));
                       
                    // Tariff_T1 = GetOBISCode(Get_Index.Cumulative_Tariff1_KwhExport);
                    // Tariff_T2 = GetOBISCode(Get_Index.Cumulative_Tariff2_KwhExport);
                    // Tariff_T3 = GetOBISCode(Get_Index.Cumulative_Tariff3_KwhExport);
                    // Tariff_T4 = GetOBISCode(Get_Index.Cumulative_Tariff4_KwhExport);
                    // Tariff_TL = GetOBISCode(Get_Index.Cumulative_TariffTL_KwhExport);
                       
                       
                    // BillingItem ActiveEnregyExport = billPeriodData.BillingItems.Find((x) =>
                    //                                                        (x.ValueInfo[0] == Tariff_T1) &&
                    //                                                        (x.ValueInfo[1] == Tariff_T2) &&
                    //                                                        (x.ValueInfo[2] == Tariff_T3) &&
                    //                                                        (x.ValueInfo[3] == Tariff_T4) &&
                    //                                                        (x.ValueInfo[4] == Tariff_TL));
                       
                    // // Calculate Active Energy Total Values
                    // if (TotalEnergy_x != null && ActiveEnregyImport != null && ActiveEnregyExport != null)
                    // {
                    //     if (double.IsNaN(TotalEnergy_x.Value.T1) || double.IsInfinity(TotalEnergy_x.Value.T1))
                    //         TotalEnergy_x.Value.T1 = ActiveEnregyImport.Value.T1 + ActiveEnregyExport.Value.T1;
                    //     if (double.IsNaN(TotalEnergy_x.Value.T2) || double.IsInfinity(TotalEnergy_x.Value.T2))
                    //         TotalEnergy_x.Value.T2 = ActiveEnregyImport.Value.T2 + ActiveEnregyExport.Value.T2;
                    //     if (double.IsNaN(TotalEnergy_x.Value.T3) || double.IsInfinity(TotalEnergy_x.Value.T3))
                    //         TotalEnergy_x.Value.T3 = ActiveEnregyImport.Value.T3 + ActiveEnregyExport.Value.T3;
                    //     if (double.IsNaN(TotalEnergy_x.Value.T4) || double.IsInfinity(TotalEnergy_x.Value.T4))
                    //         TotalEnergy_x.Value.T4 = ActiveEnregyImport.Value.T4 + ActiveEnregyExport.Value.T4;
                    //     if (double.IsNaN(TotalEnergy_x.Value.TL) || double.IsInfinity(TotalEnergy_x.Value.TL))
                    //         TotalEnergy_x.Value.TL = ActiveEnregyImport.Value.TL + ActiveEnregyExport.Value.TL;
                    // }

                    #endregion

                    #region Hard_Code Compute Total Reactive Energy

                    //Tariff_T1 = GetOBISCode(Get_Index.Cumulative_Tariff1_KvarhAbsolute);
                    //Tariff_T2 = GetOBISCode(Get_Index.Cumulative_Tariff2_KvarhAbsolute);
                    //Tariff_T3 = GetOBISCode(Get_Index.Cumulative_Tariff3_KvarhAbsolute);
                    //Tariff_T4 = GetOBISCode(Get_Index.Cumulative_Tariff4_KvarhAbsolute);
                    //Tariff_TL = GetOBISCode(Get_Index.Cumulative_TariffTL_KvarhAbsolute);


                    //BillingItem TotalReactiveEnergy_x = billPeriodData.BillingItems.Find((x) =>
                    //                                                       (x.ValueInfo[0] == Tariff_T1) &&
                    //                                                       (x.ValueInfo[1] == Tariff_T2) &&
                    //                                                       (x.ValueInfo[2] == Tariff_T3) &&
                    //                                                       (x.ValueInfo[3] == Tariff_T4) &&
                    //                                                       (x.ValueInfo[4] == Tariff_TL));


                    //Tariff_T1 = GetOBISCode(Get_Index.Cumulative_Tariff1_KvarhQ1);
                    //Tariff_T2 = GetOBISCode(Get_Index.Cumulative_Tariff2_KvarhQ1);
                    //Tariff_T3 = GetOBISCode(Get_Index.Cumulative_Tariff3_KvarhQ1);
                    //Tariff_T4 = GetOBISCode(Get_Index.Cumulative_Tariff4_KvarhQ1);
                    //Tariff_TL = GetOBISCode(Get_Index.Cumulative_TariffTL_KvarhQ1);

                    //BillingItem ReactiveEnergy_Q1 = billPeriodData.BillingItems.Find((x) =>
                    //                                                       (x.ValueInfo[0] == Tariff_T1) &&
                    //                                                       (x.ValueInfo[1] == Tariff_T2) &&
                    //                                                       (x.ValueInfo[2] == Tariff_T3) &&
                    //                                                       (x.ValueInfo[3] == Tariff_T4) &&
                    //                                                       (x.ValueInfo[4] == Tariff_TL));

                    //Tariff_T1 = GetOBISCode(Get_Index.Cumulative_Tariff1_KvarhQ2);
                    //Tariff_T2 = GetOBISCode(Get_Index.Cumulative_Tariff2_KvarhQ2);
                    //Tariff_T3 = GetOBISCode(Get_Index.Cumulative_Tariff3_KvarhQ2);
                    //Tariff_T4 = GetOBISCode(Get_Index.Cumulative_Tariff4_KvarhQ2);
                    //Tariff_TL = GetOBISCode(Get_Index.Cumulative_TariffTL_KvarhQ2);

                    //BillingItem ReactiveEnergy_Q2 = billPeriodData.BillingItems.Find((x) =>
                    //                                                       (x.ValueInfo[0] == Tariff_T1) &&
                    //                                                       (x.ValueInfo[1] == Tariff_T2) &&
                    //                                                       (x.ValueInfo[2] == Tariff_T3) &&
                    //                                                       (x.ValueInfo[3] == Tariff_T4) &&
                    //                                                       (x.ValueInfo[4] == Tariff_TL));


                    //Tariff_T1 = GetOBISCode(Get_Index.Cumulative_Tariff1_KvarhQ3);
                    //Tariff_T2 = GetOBISCode(Get_Index.Cumulative_Tariff2_KvarhQ3);
                    //Tariff_T3 = GetOBISCode(Get_Index.Cumulative_Tariff3_KvarhQ3);
                    //Tariff_T4 = GetOBISCode(Get_Index.Cumulative_Tariff4_KvarhQ3);
                    //Tariff_TL = GetOBISCode(Get_Index.Cumulative_TariffTL_KvarhQ3);


                    //BillingItem ReactiveEnergy_Q3 = billPeriodData.BillingItems.Find((x) =>
                    //                                                       (x.ValueInfo[0] == Tariff_T1) &&
                    //                                                       (x.ValueInfo[1] == Tariff_T2) &&
                    //                                                       (x.ValueInfo[2] == Tariff_T3) &&
                    //                                                       (x.ValueInfo[3] == Tariff_T4) &&
                    //                                                       (x.ValueInfo[4] == Tariff_TL));

                    //Tariff_T1 = GetOBISCode(Get_Index.Cumulative_Tariff1_KvarhQ4);
                    //Tariff_T2 = GetOBISCode(Get_Index.Cumulative_Tariff2_KvarhQ4);
                    //Tariff_T3 = GetOBISCode(Get_Index.Cumulative_Tariff3_KvarhQ4);
                    //Tariff_T4 = GetOBISCode(Get_Index.Cumulative_Tariff4_KvarhQ4);
                    //Tariff_TL = GetOBISCode(Get_Index.Cumulative_TariffTL_KvarhQ4);


                    //BillingItem ReactiveEnergy_Q4 = billPeriodData.BillingItems.Find((x) =>
                    //                                                       (x.ValueInfo[0] == Tariff_T1) &&
                    //                                                       (x.ValueInfo[1] == Tariff_T2) &&
                    //                                                       (x.ValueInfo[2] == Tariff_T3) &&
                    //                                                       (x.ValueInfo[3] == Tariff_T4) &&
                    //                                                       (x.ValueInfo[4] == Tariff_TL));


                    //// Calculate Active Energy Total Values
                    //if (TotalEnergy_x != null && ActiveEnregyImport != null && ActiveEnregyExport != null)
                    //{
                    //    if (double.IsNaN(TotalReactiveEnergy_x.Value.T1) || double.IsInfinity(TotalReactiveEnergy_x.Value.T1))
                    //        TotalReactiveEnergy_x.Value.T1 = ReactiveEnergy_Q1.Value.T1 + ReactiveEnergy_Q2.Value.T1 + ReactiveEnergy_Q3.Value.T1 + ReactiveEnergy_Q4.Value.T1;
                    //    if (double.IsNaN(TotalReactiveEnergy_x.Value.T2) || double.IsInfinity(TotalReactiveEnergy_x.Value.T2))
                    //        TotalReactiveEnergy_x.Value.T2 = ReactiveEnergy_Q1.Value.T2 + ReactiveEnergy_Q2.Value.T2 + ReactiveEnergy_Q3.Value.T2 + ReactiveEnergy_Q4.Value.T2;
                    //    if (double.IsNaN(TotalReactiveEnergy_x.Value.T3) || double.IsInfinity(TotalReactiveEnergy_x.Value.T3))
                    //        TotalReactiveEnergy_x.Value.T3 = ReactiveEnergy_Q1.Value.T3 + ReactiveEnergy_Q2.Value.T3 + ReactiveEnergy_Q3.Value.T3 + ReactiveEnergy_Q4.Value.T3;
                    //    if (double.IsNaN(TotalReactiveEnergy_x.Value.T4) || double.IsInfinity(TotalReactiveEnergy_x.Value.T4))
                    //        TotalReactiveEnergy_x.Value.T4 = ReactiveEnergy_Q1.Value.T4 + ReactiveEnergy_Q2.Value.T4 + ReactiveEnergy_Q3.Value.T4 + ReactiveEnergy_Q4.Value.T4;
                    //    if (double.IsNaN(TotalReactiveEnergy_x.Value.TL) || double.IsInfinity(TotalReactiveEnergy_x.Value.TL))
                    //        TotalReactiveEnergy_x.Value.TL = ReactiveEnergy_Q1.Value.TL + ReactiveEnergy_Q2.Value.TL + ReactiveEnergy_Q3.Value.TL + ReactiveEnergy_Q4.Value.TL;
                    //}

                    #endregion

                    BillData.Add(billPeriodData);
                }
                return BillData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred generating Billing data " + ex.Message, ex);
            }
        }

        #endregion

        #region Billing Formats


        public List<BillingItem> GetBillingFormatX()
        {
            List<BillingItem> billList = new List<BillingItem>();
            ///Make Billing Data Format Fix May Load Lator from file 
            ///Cumulative_TariffTL_KwhImport
            BillingItem KwhImport = new BillingItem();
            KwhImport.Name = "Active Energy Import";
            KwhImport.Unit = Unit.kWh;
            KwhImport.Multiplier = -3f;
            KwhImport.Format = "F3";
            //KwhImport.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_KwhImport,
            //    (double)Get_Index.Cumulative_Tariff2_KwhImport,
            //    (double)Get_Index.Cumulative_Tariff3_KwhImport,
            //    (double)Get_Index.Cumulative_Tariff4_KwhImport,
            //    (double)Get_Index.Cumulative_TariffTL_KwhImport);

            KwhImport.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Active_Energy_Positive_T1,Get_Index.Active_Energy_Positive_T2,
                                                                  Get_Index.Active_Energy_Positive_T3,Get_Index.Active_Energy_Positive_T4,
                                                                  Get_Index.Active_Energy_Positive_TL
                                                                  });
            ///Cumulative_TariffTL_KwhExport
            BillingItem KwhExport = new BillingItem();
            KwhExport.Name = "Active Energy Export";
            KwhExport.Unit = Unit.kWh;
            KwhExport.Multiplier = -3f;
            KwhExport.Format = "F3";
            //KwhExport.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_KwhExport,
            //    (double)Get_Index.Cumulative_Tariff2_KwhExport,
            //    (double)Get_Index.Cumulative_Tariff3_KwhExport,
            //    (double)Get_Index.Cumulative_Tariff4_KwhExport,
            //    (double)Get_Index.Cumulative_TariffTL_KwhExport);
            KwhExport.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Active_Energy_Negative_T1,Get_Index.Active_Energy_Negative_T2,
                                                                  Get_Index.Active_Energy_Negative_T3,Get_Index.Active_Energy_Negative_T4,
                                                                  Get_Index.Active_Energy_Negative_TL
                                                                  });



            BillingItem KwhTotal = new BillingItem();
            KwhTotal.Name = "Active Energy Total";
            KwhTotal.Unit = Unit.kWh;
            KwhTotal.Multiplier = -3.0f;
            KwhTotal.Format = "F3";
            //KwhTotal.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_KwhAbsolute,
            //    (double)Get_Index.Cumulative_Tariff2_KwhAbsolute,
            //    (double)Get_Index.Cumulative_Tariff3_KwhAbsolute,
            //    (double)Get_Index.Cumulative_Tariff4_KwhAbsolute,
            //    (double)Get_Index.Cumulative_TariffTL_KwhAbsolute);
            KwhTotal.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Active_Energy_Absolute_T1,Get_Index.Active_Energy_Absolute_T2,
                                                                  Get_Index.Active_Energy_Absolute_T3,Get_Index.Active_Energy_Absolute_T4,
                                                                  Get_Index.Active_Energy_Absolute_TL
                                                                  });

            ///Cumulative_Tariff1_Kvah	Apparent Energy
            BillingItem Kvah = new BillingItem();
            Kvah.Name = "Apparent Energy";
            Kvah.Unit = Unit.kVarh;
            Kvah.Multiplier = -3.0f;
            Kvah.Format = "F3";
            //Kvah.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_Kvah,
            //    (double)Get_Index.Cumulative_Tariff2_Kvah,
            //    (double)Get_Index.Cumulative_Tariff3_Kvah,
            //    (double)Get_Index.Cumulative_Tariff4_Kvah,
            //    (double)Get_Index.Cumulative_TariffTL_Kvah);
            Kvah.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Cumulative_Tariff1_Kvah,Get_Index.Cumulative_Tariff2_Kvah,
                                                                  Get_Index.Cumulative_Tariff3_Kvah,Get_Index.Cumulative_Tariff4_Kvah,
                                                                  Get_Index.Cumulative_TariffTL_Kvah
                                                                  });


            ///Cumulative_TariffTL_KvarhQ1
            BillingItem KvarhQ1 = new BillingItem();
            KvarhQ1.Name = "Reactive Energy Quard1";
            KvarhQ1.Unit = Unit.kVarh;
            KvarhQ1.Multiplier = -3.0f;
            KvarhQ1.Format = "F3";
            //KvarhQ1.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_KvarhQ1,
            //    (double)Get_Index.Cumulative_Tariff2_KvarhQ1,
            //    (double)Get_Index.Cumulative_Tariff3_KvarhQ1,
            //    (double)Get_Index.Cumulative_Tariff4_KvarhQ1,
            //    (double)Get_Index.Cumulative_TariffTL_KvarhQ1);
            KvarhQ1.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Cumulative_Tariff1_KvarhQ1,Get_Index.Cumulative_Tariff2_KvarhQ1,
                                                                  Get_Index.Cumulative_Tariff3_KvarhQ1,Get_Index.Cumulative_Tariff4_KvarhQ1,
                                                                  Get_Index.Cumulative_TariffTL_KvarhQ1
                                                                  });

            ///Cumulative_TariffTL_KvarhQ2
            BillingItem KvarhQ2 = new BillingItem();
            KvarhQ2.Name = "Reactive Energy Quard2";
            KvarhQ2.Unit = Unit.kVarh;
            KvarhQ2.Multiplier = -3.0f;
            KvarhQ2.Format = "F3";
            //KvarhQ2.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_KvarhQ2,
            //    (double)Get_Index.Cumulative_Tariff2_KvarhQ2,
            //    (double)Get_Index.Cumulative_Tariff3_KvarhQ2,
            //    (double)Get_Index.Cumulative_Tariff4_KvarhQ2,
            //    (double)Get_Index.Cumulative_TariffTL_KvarhQ2);

            KvarhQ2.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Cumulative_Tariff1_KvarhQ2,Get_Index.Cumulative_Tariff2_KvarhQ2,
                                                                  Get_Index.Cumulative_Tariff3_KvarhQ2,Get_Index.Cumulative_Tariff4_KvarhQ2,
                                                                  Get_Index.Cumulative_TariffTL_KvarhQ2
                                                                  });

            ///Cumulative_TariffTL_KvarhQ3
            BillingItem KvarhQ3 = new BillingItem();
            KvarhQ3.Name = "Reactive Energy Quard3";
            KvarhQ3.Unit = Unit.kVarh;
            KvarhQ3.Multiplier = -3.0f;
            KvarhQ3.Format = "F3";
            //KvarhQ3.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_KvarhQ3,
            //    (double)Get_Index.Cumulative_Tariff2_KvarhQ3,
            //    (double)Get_Index.Cumulative_Tariff3_KvarhQ3,
            //    (double)Get_Index.Cumulative_Tariff4_KvarhQ3,
            //    (double)Get_Index.Cumulative_TariffTL_KvarhQ3);

            KvarhQ3.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Cumulative_Tariff1_KvarhQ3,Get_Index.Cumulative_Tariff2_KvarhQ3,
                                                                  Get_Index.Cumulative_Tariff3_KvarhQ3,Get_Index.Cumulative_Tariff4_KvarhQ3,
                                                                  Get_Index.Cumulative_TariffTL_KvarhQ3
                                                                  });

            ///Cumulative_TariffTL_KvarhQ4
            BillingItem KvarhQ4 = new BillingItem();
            KvarhQ4.Name = "Reactive Energy Quard4";
            KvarhQ4.Unit = Unit.kVarh;
            KvarhQ4.Multiplier = -3.0f;
            KvarhQ4.Format = "F3";
            //KvarhQ4.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_KvarhQ4,
            //    (double)Get_Index.Cumulative_Tariff2_KvarhQ4,
            //    (double)Get_Index.Cumulative_Tariff3_KvarhQ4,
            //    (double)Get_Index.Cumulative_Tariff4_KvarhQ4,
            //    (double)Get_Index.Cumulative_TariffTL_KvarhQ4);

            KvarhQ4.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Cumulative_Tariff1_KvarhQ4,Get_Index.Cumulative_Tariff2_KvarhQ4,
                                                                  Get_Index.Cumulative_Tariff3_KvarhQ4,Get_Index.Cumulative_Tariff4_KvarhQ4,
                                                                  Get_Index.Cumulative_TariffTL_KvarhQ4
                                                                  });

            BillingItem KvarhTotal = new BillingItem();
            KvarhTotal.Name = "Reactive Energy Total";
            KvarhTotal.Unit = Unit.kWh;
            KvarhTotal.Multiplier = -3.0f;
            KvarhTotal.Format = "F3";
            //KwhTotal.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_KwhAbsolute,
            //    (double)Get_Index.Cumulative_Tariff2_KwhAbsolute,
            //    (double)Get_Index.Cumulative_Tariff3_KwhAbsolute,
            //    (double)Get_Index.Cumulative_Tariff4_KwhAbsolute,
            //    (double)Get_Index.Cumulative_TariffTL_KwhAbsolute);
            KvarhTotal.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Reactive_Energy_Absolute_T1,Get_Index.Reactive_Energy_Absolute_T2,
                                                                  Get_Index.Reactive_Energy_Absolute_T3,Get_Index.Reactive_Energy_Absolute_T4,
                                                                  Get_Index.Reactive_Energy_Absolute_TL
                                                                  });



            ///Cumulative_TariffTL_TamperKwh Tamper Energy 
            BillingItem TamperKwh = new BillingItem();
            TamperKwh.Name = "Tamper Energy";
            TamperKwh.Unit = Unit.kWh;
            TamperKwh.Multiplier = -3.0f;
            TamperKwh.Format = "F3";
            //TamperKwh.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_TamperKwh,
            //    (double)Get_Index.Cumulative_Tariff2_TamperKwh,
            //    (double)Get_Index.Cumulative_Tariff3_TamperKwh,
            //    (double)Get_Index.Cumulative_Tariff4_TamperKwh,
            //    (double)Get_Index.Cumulative_TariffTL_TamperKwh);
            TamperKwh.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Cumulative_Tariff1_TamperKwh,Get_Index.Cumulative_Tariff2_TamperKwh,
                                                                  Get_Index.Cumulative_Tariff3_TamperKwh,Get_Index.Cumulative_Tariff4_TamperKwh,
                                                                  Get_Index.Cumulative_TariffTL_TamperKwh
                                                                  });

            ///Cumulative_TariffTL_MdiKw Active MDI 
            BillingItem MdiKw = new BillingItem();
            MdiKw.Name = "Active MDI";
            MdiKw.Unit = Unit.kW;
            MdiKw.Multiplier = -3.0f;
            MdiKw.Format = "F3";
            //MdiKw.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_MdiKw,
            //    (double)Get_Index.Cumulative_Tariff2_MdiKw,
            //    (double)Get_Index.Cumulative_Tariff3_MdiKw,
            //    (double)Get_Index.Cumulative_Tariff4_MdiKw,
            //    (double)Get_Index.Cumulative_TariffTL_MdiKw);
            MdiKw.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Cumulative_MDI_Absolute_T1,Get_Index.Cumulative_MDI_Absolute_T2,
                                                                  Get_Index.Cumulative_MDI_Absolute_T3,Get_Index.Cumulative_Tariff4_MdiKw,
                                                                  Get_Index.Cumulative_TariffTL_MdiKw
                                                                  });


            ///Cumulative_TariffTL_MdiKvar	Reactive MDI
            BillingItem MdiKvar = new BillingItem();
            MdiKvar.Name = "Reactive MDI";
            MdiKvar.Unit = Unit.kVar;
            MdiKvar.Multiplier = -3.0f;
            MdiKvar.Format = "F3";
            //MdiKvar.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_MdiKvar,
            //    (double)Get_Index.Cumulative_Tariff2_MdiKvar,
            //    (double)Get_Index.Cumulative_Tariff3_MdiKvar,
            //    (double)Get_Index.Cumulative_Tariff4_MdiKvar,
            //    (double)Get_Index.Cumulative_TariffTL_MdiKvar);
            MdiKvar.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Cumulative_Tariff1_MdiKvar,Get_Index.Cumulative_Tariff2_MdiKvar,
                                                                  Get_Index.Cumulative_Tariff3_MdiKvar,Get_Index.Cumulative_Tariff4_MdiKvar,
                                                                  Get_Index.Cumulative_TariffTL_MdiKvar
                                                                  });

            ///Cumulative_TariffTL_PowerFactor	    Power Factor	
            BillingItem PowerFactor = new BillingItem();
            PowerFactor.Name = " Power Factor";
            PowerFactor.Unit = Unit.UnitLess;
            PowerFactor.Multiplier = 1;
            PowerFactor.Format = "F3";
            //PowerFactor.Value = new Tariff((double)Get_Index.Cumulative_Tariff1_PowerFactor,
            //    (double)Get_Index.Cumulative_Tariff2_PowerFactor,
            //    (double)Get_Index.Cumulative_Tariff3_PowerFactor,
            //    (double)Get_Index.Cumulative_Tariff4_PowerFactor,
            //    (double)Get_Index.Cumulative_TariffTL_PowerFactor);
            PowerFactor.ValueInfo = new List<Get_Index>(new Get_Index[]{Get_Index.Cumulative_Tariff1_PowerFactor,Get_Index.Cumulative_Tariff2_PowerFactor,
                                                                  Get_Index.Cumulative_Tariff3_PowerFactor,Get_Index.Cumulative_Tariff4_PowerFactor,
                                                                  Get_Index.Cumulative_TariffTL_PowerFactor
                                                                  });

            ///Adding Billing Format Objects In Sequences
            billList.Add(KwhImport);
            billList.Add(KwhExport);
            billList.Add(KwhTotal);
            billList.Add(Kvah);
            billList.Add(KvarhQ1);
            billList.Add(KvarhQ2);
            billList.Add(KvarhQ3);
            billList.Add(KvarhQ4);
            billList.Add(KvarhTotal);
            billList.Add(TamperKwh);
            billList.Add(MdiKw);
            billList.Add(MdiKvar);
            billList.Add(PowerFactor);
            return billList;
        }

        //public List<BillingItem> GetBillingFormatX(int dummy) 
        //{
        //    return GetBillingFormatX(Configurations);    
        //}

        //public List<BillingItem> GetBillingFormatX(Configs Configurator)
        //{
        //    try
        //    {
        //        ConfigsHelper ConfigGenerator = new ConfigsHelper(Configurations);
        //        return ConfigGenerator.GetBillingItemsFormat();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<BillingItem> GetBillingFormat(Configurator ConfiguratorObj, ConnectionInfo ConnectionInfo)
        {
            try
            {
                return ConfiguratorObj.GetBillingItemsFormat(ConnectionInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        private BillingItem makeBillingItem(List<ILValue> values, BillingItem billItemFormat)
        {
            try
            {
                BillingItem toRet = (BillingItem)billItemFormat.Clone();
                StOBISCode ValInfo = Get_Index.Dummy;
                if (billItemFormat.ValueInfo.Count > 1)
                {
                    ///----------------------Tariff_1--------------------------
                    ILValue Val = values.Find((x) => x.OBIS_Index == (Get_Index)billItemFormat.ValueInfo[Tariff.T1_Index]);
                    toRet.Value.T1 = makeValue(Val, toRet.Multiplier);
                    
                    #region Store Capture DateTime Values
                    try
                    {
                        ValInfo = (Get_Index)billItemFormat.ValueInfo[Tariff.T1_Index];
                        if (ValInfo.ClassId == 4)          ///Copy Capture Date Time
                        {
                            toRet.Value.CaptureTimeStamp.AddRange(new DateTime[] { DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue });
                            Object rawTimeInstance = null;
                            if (Val != null)
                                if (Val.ValueCollection.ContainsKey(5))
                                    rawTimeInstance = Val.ValueCollection[5];
                            if (rawTimeInstance != null && rawTimeInstance.GetType() == typeof(StDateTime))
                            {
                                StDateTime stInstanceCaptureTime = (StDateTime)rawTimeInstance;
                                if (stInstanceCaptureTime != null && stInstanceCaptureTime.IsDateTimeConvertible)
                                    toRet.Value.CaptureTimeStamp[Tariff.T1_Index] = stInstanceCaptureTime.GetDateTime();
                                else
                                    toRet.Value.CaptureTimeStamp[Tariff.T1_Index] = DateTime.MinValue;
                            }
                            else
                            {
                                ///Store Nullable/MinDate
                                toRet.Value.CaptureTimeStamp[Tariff.T1_Index] = DateTime.MinValue;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        ///Store Nullable/MinDate
                        toRet.Value.CaptureTimeStamp[Tariff.T1_Index] = DateTime.MinValue;
                    }
                    #endregion
                    ///---------------------Tariff_2----------------------------
                    Val = values.Find((x) => x.OBIS_Index == (Get_Index)billItemFormat.ValueInfo[Tariff.T2_Index]);
                    toRet.Value.T2 = makeValue(Val, toRet.Multiplier);
                    #region Store Capture DateTime Values
                    try
                    {
                        ValInfo = (Get_Index)billItemFormat.ValueInfo[Tariff.T2_Index];
                        if (ValInfo.ClassId == 4)          ///Copy Capture Date Time
                        {
                            Object rawTimeInstance = null;
                            if (Val != null)
                                if (Val.ValueCollection.ContainsKey(5))
                                    rawTimeInstance = Val.ValueCollection[5];
                            if (rawTimeInstance != null && rawTimeInstance.GetType() == typeof(StDateTime))
                            {
                                StDateTime stInstanceCaptureTime = (StDateTime)rawTimeInstance;
                                if (stInstanceCaptureTime != null && stInstanceCaptureTime.IsDateTimeConvertible)
                                    toRet.Value.CaptureTimeStamp[Tariff.T2_Index] = stInstanceCaptureTime.GetDateTime();
                                else
                                    toRet.Value.CaptureTimeStamp[Tariff.T2_Index] = DateTime.MinValue;
                            }
                            else
                            {
                                ///Store Nullable/MinDate
                                toRet.Value.CaptureTimeStamp[Tariff.T2_Index] = DateTime.MinValue;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        ///Store Nullable/MinDate
                        toRet.Value.CaptureTimeStamp[Tariff.T2_Index] = DateTime.MinValue;
                    }
                    #endregion
                    ///---------------------Tariff_3----------------------------
                    Val = values.Find((x) => x.OBIS_Index == (Get_Index)billItemFormat.ValueInfo[Tariff.T3_Index]);
                    toRet.Value.T3 = makeValue(Val, toRet.Multiplier);

                    #region Store Capture DateTime Values
                    try
                    {
                        ValInfo = (Get_Index)billItemFormat.ValueInfo[Tariff.T3_Index];
                        if (ValInfo.ClassId == 4)          ///Copy Capture Date Time
                        {
                            Object rawTimeInstance = null;
                            if (Val != null)
                                if (Val.ValueCollection.ContainsKey(5))
                                    rawTimeInstance = Val.ValueCollection[5];
                            if (rawTimeInstance != null && rawTimeInstance.GetType() == typeof(StDateTime))
                            {
                                StDateTime stInstanceCaptureTime = (StDateTime)rawTimeInstance;
                                if (stInstanceCaptureTime != null && stInstanceCaptureTime.IsDateTimeConvertible)
                                    toRet.Value.CaptureTimeStamp[Tariff.T3_Index] = stInstanceCaptureTime.GetDateTime();
                                else
                                    toRet.Value.CaptureTimeStamp[Tariff.T3_Index] = DateTime.MinValue;
                            }
                            else
                            {
                                ///Store Nullable/MinDate
                                toRet.Value.CaptureTimeStamp[Tariff.T3_Index] = DateTime.MinValue;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        ///Store Nullable/MinDate
                        toRet.Value.CaptureTimeStamp[Tariff.T3_Index] = DateTime.MinValue;

                    }
                    #endregion

                    ///---------------------Tariff_4----------------------------
                    Val = values.Find((x) => x.OBIS_Index == (Get_Index)billItemFormat.ValueInfo[Tariff.T4_Index]);
                    toRet.Value.T4 = makeValue(Val, toRet.Multiplier);

                    #region Store Capture DateTime Values
                    try
                    {
                        ValInfo = (Get_Index)billItemFormat.ValueInfo[Tariff.T4_Index];
                        if (ValInfo.ClassId == 4)          ///Copy Capture Date Time
                        {
                            Object rawTimeInstance = null;
                            if (Val != null)
                                if (Val.ValueCollection.ContainsKey(5))
                                    rawTimeInstance = Val.ValueCollection[5];
                            if (rawTimeInstance != null && rawTimeInstance.GetType() == typeof(StDateTime))
                            {
                                StDateTime stInstanceCaptureTime = (StDateTime)rawTimeInstance;
                                if (stInstanceCaptureTime != null && stInstanceCaptureTime.IsDateTimeConvertible)
                                    toRet.Value.CaptureTimeStamp[Tariff.T4_Index] = stInstanceCaptureTime.GetDateTime();
                                else
                                    toRet.Value.CaptureTimeStamp[Tariff.T4_Index] = DateTime.MinValue;
                            }
                            else
                            {
                                ///Store Nullable/MinDate
                                toRet.Value.CaptureTimeStamp[Tariff.T4_Index] = DateTime.MinValue;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        ///Store Nullable/MinDate
                        toRet.Value.CaptureTimeStamp[Tariff.T4_Index] = DateTime.MinValue;

                    }
                    #endregion
                    ///---------------------Tariff_TL---------------------------
                    Val = values.Find((x) => x.OBIS_Index == (Get_Index)billItemFormat.ValueInfo[Tariff.TL_Index]);
                    toRet.Value.TL = makeValue(Val, toRet.Multiplier);

                    #region Store Capture DateTime Values
                    try
                    {
                        ValInfo = (Get_Index)billItemFormat.ValueInfo[Tariff.TL_Index];
                        if (ValInfo.ClassId == 4)          ///Copy Capture Date Time
                        {
                            Object rawTimeInstance = null;
                            if (Val != null)
                                if (Val.ValueCollection.ContainsKey(5))
                                    rawTimeInstance = Val.ValueCollection[5];
                            if (rawTimeInstance != null && rawTimeInstance.GetType() == typeof(StDateTime))
                            {
                                StDateTime stInstanceCaptureTime = (StDateTime)rawTimeInstance;
                                if (stInstanceCaptureTime != null && stInstanceCaptureTime.IsDateTimeConvertible)
                                    toRet.Value.CaptureTimeStamp[Tariff.TL_Index] = stInstanceCaptureTime.GetDateTime();
                                else
                                    toRet.Value.CaptureTimeStamp[Tariff.TL_Index] = DateTime.MinValue;
                            }
                            else
                            {
                                ///Store Nullable/MinDate
                                toRet.Value.CaptureTimeStamp[Tariff.TL_Index] = DateTime.MinValue;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        ///Store Nullable/MinDate
                        toRet.Value.CaptureTimeStamp[Tariff.TL_Index] = DateTime.MinValue;

                    }
                    #endregion
                    ///--------------------------------------------------------- 
                    
                }
                else 
                {
                    toRet.Value.CaptureTimeStamp.AddRange(new DateTime[] { DateTime.MinValue});
                    var  Val = values.Find((x) => x.OBIS_Index == (Get_Index)billItemFormat.ValueInfo[Tariff.T1_Index]);
                    toRet.Value.T1 = makeValue(Val, toRet.Multiplier);
                    #region Store Capture DateTime Values
                    try
                    {
                        ValInfo = (Get_Index)billItemFormat.ValueInfo[Tariff.T1_Index];
                        if (ValInfo.ClassId == 4)          ///Copy Capture Date Time
                        {
                            Object rawTimeInstance = null;
                            if (Val != null)
                                if (Val.ValueCollection.ContainsKey(5))
                                    rawTimeInstance = Val.ValueCollection[5];
                            if (rawTimeInstance != null && rawTimeInstance.GetType() == typeof(StDateTime))
                            {
                                StDateTime stInstanceCaptureTime = (StDateTime)rawTimeInstance;
                                if (stInstanceCaptureTime != null && stInstanceCaptureTime.IsDateTimeConvertible)
                                    toRet.Value.CaptureTimeStamp[Tariff.T1_Index] = stInstanceCaptureTime.GetDateTime();
                                else
                                    toRet.Value.CaptureTimeStamp[Tariff.T1_Index] = DateTime.MinValue;
                            }
                            else
                            {
                                ///Store Nullable/MinDate
                                toRet.Value.CaptureTimeStamp[Tariff.T1_Index] = DateTime.MinValue;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        ///Store Nullable/MinDate
                        toRet.Value.CaptureTimeStamp[Tariff.T1_Index] = DateTime.MinValue;

                    }
                    #endregion
                }
                return toRet;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to format billing data", ex);
            }

        }

        private double makeValue(ILValue val, Unit _valUnit, float multiplier)
        {
            try
            {
                if (val != null)
                {
                    double rawVal = Convert.ToDouble(val.Value);
                    switch (_valUnit)
                    {
                        case Unit.kVarh:
                        case Unit.kVah:
                        case Unit.kWh:
                            {
                                return rawVal / 1000;
                            }
                        case Unit.kW:
                        case Unit.kVa:
                        case Unit.kVar:
                            {
                                return rawVal / 1000;
                            }
                        case Unit.UnitLess:
                            return rawVal;
                    }
                    return rawVal;
                }
                else
                    return double.NaN;

            }
            catch (Exception ex)
            {
                return double.PositiveInfinity;
            }
        }

        public static double makeValue(ILValue val, float multiplier)
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
            catch (Exception ex)
            {
                return double.PositiveInfinity;
            }
        }
    }
}
