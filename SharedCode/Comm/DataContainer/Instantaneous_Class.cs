﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using DLMS.Comm;

namespace SharedCode.Comm.DataContainer
{
    public class TariffMDI
    {
        public double Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public TariffMDI()
        {
            Value = Double.NaN;
            TimeStamp = DateTime.MinValue;
        }
        public override string ToString()
        {
            if (Value.Equals(Double.NaN)) return "---";
            return Value.ToString() + (IsValidTimeStamp ? " [" + TimeStamp.ToString("dd-MM-yyyy HH:mm:ss") + "]" : "");
        }

        public bool IsValidTimeStamp
        {
            get
            {
                return (!(TimeStamp == DateTime.MinValue || TimeStamp.Year < 2005));
            }
        }
    }
    public class QuantityMDI
    {
        public string QuantityName { get; set; }
        public StOBISCode QuantityIndex { get; set; }
        public TariffMDI Tariff1 { get; set; }
        public TariffMDI Tariff2 { get; set; }
        public TariffMDI Tariff3 { get; set; }
        public TariffMDI Tariff4 { get; set; }
        public TariffMDI TariffTL { get; set; }

        public QuantityMDI(string quantityName, Get_Index quantityIndex)
        {
            QuantityName = quantityName;
            QuantityIndex = quantityIndex;
            Tariff1 = new TariffMDI();
            Tariff2 = new TariffMDI();
            Tariff3 = new TariffMDI();
            Tariff4 = new TariffMDI();
            TariffTL = new TariffMDI();

        }
    }

    public class Instantaneous_Class : IDecodeAnyObject
    {
        public List<QuantityMDI> MDIsToShow;

        public string MSN;
        public StDateTime dateTime;

        public DateTime TimeStamp_Cumulative_TariffTL_CurrentMonthMdiKw;
        public DateTime TimeStamp_Cumulative_Tariff1_CurrentMonthMdiKw;
        public DateTime TimeStamp_Cumulative_Tariff2_CurrentMonthMdiKw;
        public DateTime TimeStamp_Cumulative_Tariff3_CurrentMonthMdiKw;
        public DateTime TimeStamp_Cumulative_Tariff4_CurrentMonthMdiKw;

        public Class_4 Cumulative_Tariff1_CurrentMonthMdiKw;
        public Class_4 Cumulative_Tariff2_CurrentMonthMdiKw;
        public Class_4 Cumulative_Tariff3_CurrentMonthMdiKw;
        public Class_4 Cumulative_Tariff4_CurrentMonthMdiKw;
        public Class_4 Cumulative_TariffTL_CurrentMonthMdiKw;

        public Class_4 Cumulative_Tariff1_CurrentMonthMdiKvar;
        public Class_4 Cumulative_Tariff2_CurrentMonthMdiKvar;
        public Class_4 Cumulative_Tariff3_CurrentMonthMdiKvar;
        public Class_4 Cumulative_Tariff4_CurrentMonthMdiKvar;
        public Class_4 Cumulative_TariffTL_CurrentMonthMdiKvar;

        public double RPT_Cumulative_Tariff1_CurrentMonthMdiKw;
        public double RPT_Cumulative_Tariff2_CurrentMonthMdiKw;
        public double RPT_Cumulative_Tariff3_CurrentMonthMdiKw;
        public double RPT_Cumulative_Tariff4_CurrentMonthMdiKw;
        public double RPT_Cumulative_TariffTL_CurrentMonthMdiKw;

        public double RPT_Cumulative_Tariff1_CurrentMonthMdiKvar;
        public double RPT_Cumulative_Tariff2_CurrentMonthMdiKvar;
        public double RPT_Cumulative_Tariff3_CurrentMonthMdiKvar;
        public double RPT_Cumulative_Tariff4_CurrentMonthMdiKvar;
        public double RPT_Cumulative_TariffTL_CurrentMonthMdiKvar;

        public double Current_on_neutral;
        public double Active_power_on_neutral;
        public double Active_power_on_live;
        public DateTime Last_MDI_REset_Date;
        public int MDI_reset_count;

        #region Defining Properties
        public double voltage_PhaseTL;
        public double voltage_PhaseA;
        public double voltage_PhaseB;
        public double voltage_PhaseC;
        public double current_PhaseTL;
        public double current_PhaseA;
        public double current_PhaseB;
        public double current_PhaseC;
        public double voltage_total;
        public double current_total;
        public double Apparent_Power_Ph_A;
        public double Apparent_Power_Ph_B;
        public double Apparent_Power_Ph_C;
        public double Apparent_Power_Tot;
        public double CT_Ratio_Numerator;
        public double CT_Ratio_Denominator;
        public double PT_Ratio_Numerator;
        public double PT_Ratio_Denominator;
        public double active_powerPositive_PhaseTL;
        public double active_powerNegative_PhaseTL;
        public double active_powerPositive_PhaseA;
        public double active_powerNegative_PhaseA;
        public double active_powerPositive_PhaseB;
        public double active_powerNegative_PhaseB;
        public double active_powerPositive_PhaseC;
        public double active_powerNegative_PhaseC;
        public double reactive_powerPositive_PhaseTL;
        public double reactive_powerNegative_PhaseTL;
        public double reactive_powerPositive_PhaseA;
        public double reactive_powerNegative_PhaseA;
        public double reactive_powerPositive_PhaseB;
        public double reactive_powerNegative_PhaseB;
        public double reactive_powerPositive_PhaseC;
        public double reactive_powerNegative_PhaseC;

        public double Power_Factor_All;
        public double Power_Factor_Ph_A;
        public double Power_Factor_Ph_B;
        public double Power_Factor_Ph_C;
        public double Supply_Frequency;
        public double frequency;
        public double Frequency_PhA;
        public double Frequency_PhB;
        public double Frequency_PhC;
        public double Battery_Volts;
        public double Available_Billing_Periods_No;

        public double Apparent_Power_Total;
        public double Active_Energy_Total_Pos;
        public double Active_Energy_Total_Neg;
        public double Active_Energy_Tariff_1_Pos;
        public double Active_Energy_Tariff_1_Neg;
        public double Active_Energy_Tariff_2_Pos;
        public double Active_Energy_Tariff_2_Neg;
        public double Active_Energy_Tariff_3_Pos;
        public double Active_Energy_Tariff_3_Neg;
        public double Active_Energy_Tariff_4_Pos;
        public double Active_Energy_Tariff_4_Neg;
        public double KWH_Absolute_Total;
        public double KWH_Absolute_Tariff_1;
        public double KWH_Absolute_Tariff_2;
        public double KWH_Absolute_Tariff_3;
        public double KWH_Absolute_Tariff_4;
        public double Reactive_Energy_Total_Pos;
        public double Reactive_Energy_Tariff_1_Pos;
        public double Reactive_Energy_Tariff_2_Pos;
        public double Reactive_Energy_Tariff_3_Pos;
        public double Reactive_Energy_Tariff_4_Pos;
        public double Reactive_Energy_Total_Neg;
        public double Reactive_Energy_Tariff_1_Neg;
        public double Reactive_Energy_Tariff_2_Neg;
        public double Reactive_Energy_Tariff_3_Neg;
        public double Reactive_Energy_Tariff_4_Neg;
        public double Cumulative_MDI_KW_Total;
        public double Cumulative_MDI_KW_Tariff_1;
        public double Cumulative_MDI_KW_Tariff_2;
        public double Cumulative_MDI_KW_Tariff_3;
        public double Cumulative_MDI_KW_Tariff_4;
        public double Monthly_MDI_KW_Total;
        public double Monthly_MDI_KW_Tariff_1;
        public double Monthly_MDI_KW_Tariff_2;
        public double Monthly_MDI_KW_Tariff_3;
        public double Monthly_MDI_KW_Tariff_4;
        public double Cumulative_MDI_KVar_Total;
        public double Cumulative_MDI_KVar_Tariff1;
        public double Cumulative_MDI_KVar_Tariff2;
        public double Cumulative_MDI_KVar_Tariff3;
        public double Cumulative_MDI_KVar_Tariff4;
        public double Monthly_MDI_KVar_Total;
        public double Monthly_MDI_KVar_Tariff1;
        public double Monthly_MDI_KVar_Tariff2;
        public double Monthly_MDI_KVar_Tariff3;
        public double Monthly_MDI_KVar_Tariff4;
        public double Monthly_Avg_PF;
        //<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        //<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        public double Cumulative_Tariff1_PowerFactor;
        public double Cumulative_Tariff2_PowerFactor;
        public double Cumulative_Tariff3_PowerFactor;
        public double Cumulative_Tariff4_PowerFactor;
        public double Cumulative_TariffTL_PowerFactor;

        //public double Cumulative_Tariff1_CurrentMonthMdiKw;
        //public double Cumulative_Tariff2_CurrentMonthMdiKw;
        //public double Cumulative_Tariff3_CurrentMonthMdiKw;
        //public double Cumulative_Tariff4_CurrentMonthMdiKw;
        //public double Cumulative_TariffTL_CurrentMonthMdiKw;

        //public double Cumulative_Tariff1_CurrentMonthMdiKvar;
        //public double Cumulative_Tariff2_CurrentMonthMdiKvar;
        //public double Cumulative_Tariff3_CurrentMonthMdiKvar;
        //public double Cumulative_Tariff4_CurrentMonthMdiKvar;
        //public double Cumulative_TariffTL_CurrentMonthMdiKvar;

        public double PB_MDI_KW_T1;
        public double PB_MDI_KW_T2;
        public double PB_MDI_KW_T3;
        public double PB_MDI_KW_T4;
        public double PB_MDI_KW_TL;

        public double PB_MDI_KVAR_T1;
        public double PB_MDI_KVAR_T2;
        public double PB_MDI_KVAR_T3;
        public double PB_MDI_KVAR_T4;
        public double PB_MDI_KVAR_TL;

        public double PB_PREVIOUS_MONTH_MDI_KVAR_T1;
        public double PB_PREVIOUS_MONTH_MDI_KVAR_T2;
        public double PB_PREVIOUS_MONTH_MDI_KVAR_T3;
        public double PB_PREVIOUS_MONTH_MDI_KVAR_T4;
        public double PB_PREVIOUS_MONTH_MDI_KVAR_TL;

        public double PB_PREVIOUS_MONTH_MDI_KW_T1;
        public double PB_PREVIOUS_MONTH_MDI_KW_T2;
        public double PB_PREVIOUS_MONTH_MDI_KW_T3;
        public double PB_PREVIOUS_MONTH_MDI_KW_T4;
        public double PB_PREVIOUS_MONTH_MDI_KW_TL;

        public double Tamper_Power;
        public double RSSI;

        //<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
        //<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

        //Display Windows Scroll Time
        public string Manufacturing_ID;
        public string Active_Firmware_ID;
        public byte Active_Season;
        public byte Active_Tariff;
        public byte Active_Day_Profile;
        public bool? RelayStatus = null;
        public DateTime Local_Date_Time;
        public DateTime Date_of_Last_ClockChange;
        //**********************************************
        public double Load_Profile_Counter;
        public double Event_Counter;
        public double Power_Failures_No;
        public double Billing_Period_Counter_VZ;
        public byte Power_Qardant_Phase_A;
        public byte Power_Qardant_Phase_B;
        public byte Power_Qardant_Phase_C;
        public byte Power_Qardant_Phase_TL;

        public string Dummy_String;
        //Event Log
        //Power Failure Log
        //Load Profile 1
        //Event Counter
        //Event Code
        //Power Failures No
        //Last Clock Change Date
        //Configuration Changes No
        //Connection Profile
        //Over Voltage Occurrence Counter
        //Under Voltage Occurrence Counter
        //Over Current Occurrence Counter
        //Power Failure Duration
        //Power Failure Any Ph Duration
        //Power Failure Time
        //Power Failure Time Any Ph
        //Over Voltage Duration
        //Under Voltage Duration
        //Over Current Duration
        //Over Load Duration
        //Power Failure Threshold
        //Customer Reference No
        //Reading_Factor_Power
        //Reading_Factor_Energy
        //Measurement_Algo_Power_Active
        //Measurement_Algo_Energy_Active
        //Measurement_Algo_Power_Reactive
        //Measurement_Algo_Energy_Reactive
        //Measurement_Algo_Power_Apparent
        //Measurement_Algo_Energy_Apparent
        //Measurement_Algo_PF
        //Billing_Periods_Data 

        public StringBuilder DBColumns = new StringBuilder();
        public StringBuilder DBValues = new StringBuilder();
        public StringBuilder DB_Columns_Values = new StringBuilder();

        #endregion

        #region Decoding Functions
        public void Decode_Voltage_Ph_A(Base_Class arg)
        {
            try
            {
                Class_3 Voltage_Ph_A_obj = (Class_3)arg;
                voltage_PhaseA = Convert.ToDouble(Voltage_Ph_A_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Voltage_Ph_B(Base_Class arg)
        {
            try
            {
                Class_3 Voltage_Ph_B_obj = (Class_3)arg;
                voltage_PhaseB = Convert.ToDouble(Voltage_Ph_B_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Voltage_Ph_C(Base_Class arg)
        {
            try
            {
                Class_3 Voltage_Ph_C_obj = (Class_3)arg;
                voltage_PhaseC = Convert.ToDouble(Voltage_Ph_C_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Current_Ph_A(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Current_Ph_A_obj = (Class_3)arg;
                current_PhaseA = Convert.ToDouble(Decode_Current_Ph_A_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Current_Ph_B(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Current_Ph_B_obj = (Class_3)arg;
                current_PhaseB = Convert.ToDouble(Decode_Current_Ph_B_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Current_Ph_C(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Current_Ph_C_obj = (Class_3)arg;
                current_PhaseC = Convert.ToDouble(Decode_Current_Ph_C_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Active_Power_Pos_Phase_A(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Active_Power_Pos_Phase_A_obj = (Class_3)arg;
                active_powerPositive_PhaseA = Convert.ToDouble(Decode_Active_Power_Pos_Phase_A_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Active_Power_Pos_Phase_B(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Active_Power_Pos_Phase_B_obj = (Class_3)arg;
                active_powerPositive_PhaseB = Convert.ToDouble(Decode_Active_Power_Pos_Phase_B_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Active_Power_Pos_Phase_C(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Active_Power_Pos_Phase_C_obj = (Class_3)arg;
                active_powerPositive_PhaseC = Convert.ToDouble(Decode_Active_Power_Pos_Phase_C_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Active_Power_Neg_Phase_A(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Active_Power_Neg_Phase_A_obj = (Class_3)arg;
                active_powerNegative_PhaseA = Convert.ToDouble(Decode_Active_Power_Neg_Phase_A_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Active_Power_Neg_Phase_B(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Active_Power_Neg_Phase_B_obj = (Class_3)arg;
                active_powerNegative_PhaseB = Convert.ToDouble(Decode_Active_Power_Neg_Phase_B_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Active_Power_Neg_Phase_C(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Active_Power_Neg_Phase_C_obj = (Class_3)arg;
                active_powerNegative_PhaseC = Convert.ToDouble(Decode_Active_Power_Neg_Phase_C_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Active_Power_Pos_Total(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Active_Power_Pos_Total_obj = (Class_3)arg;
                active_powerPositive_PhaseTL = Convert.ToDouble(Decode_Active_Power_Pos_Total_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Active_Power_Neg_Total(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Active_Power_Neg_Total_obj = (Class_3)arg;
                active_powerNegative_PhaseTL = Convert.ToDouble(Decode_Active_Power_Neg_Total_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Reactive_Power_Pos_Phase_A(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Reactive_Power_Pos_Phase_A_obj = (Class_3)arg;
                reactive_powerPositive_PhaseA = Convert.ToDouble(Decode_Reactive_Power_Pos_Phase_A_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Reactive_Power_Pos_Phase_B(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Reactive_Power_Pos_Phase_B_obj = (Class_3)arg;
                reactive_powerPositive_PhaseB = Convert.ToDouble(Decode_Reactive_Power_Pos_Phase_B_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Reactive_Power_Pos_Phase_C(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Reactive_Power_Pos_Phase_C_obj = (Class_3)arg;
                reactive_powerPositive_PhaseC = Convert.ToDouble(Decode_Reactive_Power_Pos_Phase_C_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Reactive_Power_Neg_Phase_A(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Reactive_Power_Neg_Phase_A_obj = (Class_3)arg;
                reactive_powerNegative_PhaseA = Convert.ToDouble(Decode_Reactive_Power_Neg_Phase_A_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Reactive_Power_Neg_Phase_B(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Reactive_Power_Neg_Phase_B_obj = (Class_3)arg;
                reactive_powerNegative_PhaseB = Convert.ToDouble(Decode_Reactive_Power_Neg_Phase_B_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Reactive_Power_Neg_Phase_C(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Reactive_Power_Neg_Phase_C_obj = (Class_3)arg;
                reactive_powerNegative_PhaseC = Convert.ToDouble(Decode_Reactive_Power_Neg_Phase_C_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Reactive_Power_Pos_Total(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Reactive_Power_Pos_Total_obj = (Class_3)arg;
                reactive_powerPositive_PhaseTL = Convert.ToDouble(Decode_Reactive_Power_Pos_Total_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Reactive_Power_Neg_Total(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Reactive_Power_Neg_Total_obj = (Class_3)arg;
                reactive_powerNegative_PhaseTL = Convert.ToDouble(Decode_Reactive_Power_Neg_Total_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Supply_Frequency(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Supply_Frequency_obj = (Class_3)arg;
                Supply_Frequency = Convert.ToDouble(Decode_Supply_Frequency_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Decode_Power_Factor_A(Base_Class arg)
        {
            try
            {
                Class_1 Decode_Power_Factor_A_obj = (Class_1)arg;
                Power_Factor_Ph_A = Convert.ToDouble(Decode_Power_Factor_A_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Power_Factor_B(Base_Class arg)
        {
            try
            {
                Class_1 Decode_Power_Factor_B_obj = (Class_1)arg;
                Power_Factor_Ph_B = Convert.ToDouble(Decode_Power_Factor_B_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Power_Factor_C(Base_Class arg)
        {
            try
            {
                Class_1 Decode_Power_Factor_C_obj = (Class_1)arg;
                Power_Factor_Ph_C = Convert.ToDouble(Decode_Power_Factor_C_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Power_Factor_All(Base_Class arg)
        {
            try
            {
                Class_1 Decode_Power_Factor_All_obj = (Class_1)arg;
                Power_Factor_All = Convert.ToDouble(Decode_Power_Factor_All_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Battery_Volts(Base_Class arg)
        {
            try
            {
                Class_3 Decode_Battery_Volts_obj = (Class_3)arg;
                Battery_Volts = Convert.ToDouble(Decode_Battery_Volts_obj.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        #endregion

        //public static double Decode_Any(Base_Class arg, byte Class_ID)
        //{
        //    try
        //    {
        //        if (Class_ID == 1)
        //        {
        //            Class_1 temp_obj = (Class_1)arg;
        //            double temp = Convert.ToDouble(temp_obj.Value);

        //            if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
        //                temp = double.NaN;
        //            else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
        //                temp = double.PositiveInfinity;
        //            else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
        //                temp = double.NegativeInfinity;
        //            return temp;
        //        }
        //        if (Class_ID == 3)
        //        {
        //            Class_3 temp_obj = (Class_3)arg;
        //            double temp = Convert.ToDouble(temp_obj.Value);
        //            if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
        //                temp = double.NaN;
        //            else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
        //                temp = double.PositiveInfinity;
        //            else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
        //                temp = double.NegativeInfinity;
        //            return temp;


        //            //return temp;
        //        }
        //        if (Class_ID == 4)
        //        {
        //            Class_4 temp_obj = (Class_4)arg;
        //            double temp = Convert.ToDouble(temp_obj.Value);
        //            if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
        //                temp = double.NaN;
        //            else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
        //                temp = double.PositiveInfinity;
        //            else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
        //                temp = double.NegativeInfinity;
        //            return temp;
        //        }
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public static string Decode_Any_string(Base_Class arg, byte Class_ID)
        //{

        //    if (Class_ID == 1)
        //    {
        //        Class_1 temp_obj = (Class_1)arg;
        //        byte[] dtArray = (byte[])temp_obj.Value_Array;
        //        string temp = "";
        //        if (dtArray != null)
        //            temp = (ASCIIEncoding.ASCII.GetString(dtArray));
        //        else
        //        {
        //            string asd = temp_obj.Value.ToString();
        //            return asd;
        //        }
        //        return temp;
        //    }
        //    if (Class_ID == 3)
        //    {
        //        Class_3 temp_obj = (Class_3)arg;
        //        byte[] dtArray = (byte[])temp_obj.Value_Array;
        //        string temp = "";
        //        if (dtArray != null)
        //            temp = (ASCIIEncoding.ASCII.GetString(dtArray));
        //        return temp;
        //    }
        //    return "---";
        //}

        //public static byte[] Decode_Any_ByteArray(Base_Class arg, byte Class_ID)
        //{

        //    try
        //    {
        //        Class_1 temp_obj = (Class_1)arg;
        //        byte[] dtArray = (byte[])temp_obj.Value_Array;
        //        return dtArray;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        #region Decode_Any Members

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any(arg, Class_ID);
        }

        public double Decode_Any(Base_Class arg, byte Class_ID, ref StDateTime TimeStamp)
        {
            return DLMS_Common.Decode_Any(arg, Class_ID, ref TimeStamp);
        }

        public string Decode_Any_string(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any_string(arg, Class_ID);
        }

        public byte[] Decode_Any_ByteArray(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any_ByteArray(arg, Class_ID);
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property)
        {
            return DLMS_Common.TryDecode_Any(arg, Class_ID, DataContainer_Class_obj, Data_Property);
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property, string CaptureTimeStamp_DataProperty)
        {
            return DLMS_Common.TryDecode_Any(arg, Class_ID, DataContainer_Class_obj, Data_Property, CaptureTimeStamp_DataProperty);
        }

        #endregion
    }

    public class Instantaneous_Live
    {
        public string MSN;
        public DateTime dateTime;
        public double active_powerPositive_PhaseTL;
        public double active_powerNegative_PhaseTL;
        public double reactive_powerPositive_PhaseTL;
        public double reactive_powerNegative_PhaseTL;
    }

    public class Instantaneous_SinglePhase:IDecodeAnyObject
    {
        public string MSN;
        //public string reference_no;
        public StDateTime dateTime;
        public double current;
        public double voltage;
        public double powerFactor;
        public double instantaneousActivePower;
        public double frequency;

        #region IDecodable Members
        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any(arg, Class_ID);
        }

        public double Decode_Any(Base_Class arg, byte Class_ID, ref StDateTime TimeStamp)
        {
            throw new NotImplementedException();
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property)
        {
            return DLMS_Common.TryDecode_Any(arg, Class_ID, DataContainer_Class_obj, Data_Property);
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property, string CaptureTimeStamp_DataProperty)
        {
            throw new NotImplementedException();
        }

        public string Decode_Any_string(Base_Class arg, byte Class_ID)
        {
            throw new NotImplementedException();
        }

        public byte[] Decode_Any_ByteArray(Base_Class arg, byte Class_ID)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }

}
