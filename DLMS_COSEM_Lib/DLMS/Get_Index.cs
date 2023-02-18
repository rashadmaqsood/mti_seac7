using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS
{
    /// <summary>
    /// Get_Index File has All OBIS Codes
    /// </summary>
    public enum Get_Index : ulong
    {

        ///<summary>
        /// Description: Quantity_Name= Dummy  Class ID 0 
        /// OBIS_CODE = 0.0.0.0.0.0
        /// Description
        ///</summary>
        Dummy = 0x0000000000000000,

        ///<summary>
        /// Description: Quantity_Name= Dummy  Class ID 1 
        /// OBIS_CODE = 0.0.0.0.0.0
        /// Description
        ///</summary>
        Dummy_CLS01 = 0x0001000000000000,

        ///<summary>
        /// Description: Quantity_Name= Dummy  Class ID 3 
        /// OBIS_CODE = 0.0.0.0.0.0
        /// Description
        ///</summary>
        Dummy_CLS03 = 0x0003000000000000,

        ///<summary>
        /// Description: Quantity_Name= Dummy  Class ID 4 
        /// OBIS_CODE = 0.0.0.0.0.0
        /// Description
        ///</summary>
        Dummy_CLS04 = 0x0004000000000000,

        ///<summary>
        /// Description: Quantity_Name= Dummy  Class ID 5 
        /// OBIS_CODE = 0.0.0.0.0.0
        /// Description
        ///</summary>
        Dummy_CLS05 = 0x0005000000000000,

        ///<summary>
        /// Description: Quantity_Name= Dummy  Class ID 6 
        /// OBIS_CODE = 0.0.0.0.0.0
        /// Description
        ///</summary>
        Dummy_CLS06 = 0x0006000000000000,

        ///<summary>
        /// Description: Quantity_Name= Dummy  Class ID 8 
        /// OBIS_CODE = 0.0.0.0.0.0
        /// Description
        ///</summary>
        Dummy_CLS08 = 0x0008000000000000,

        ///<summary>
        ///Description: Quantity_Name= Voltage_Any  Class ID 3 
        /// OBIS_CODE = 1.0.12.7.0.255
        /// Description
        ///</summary>
        Voltage_Any = 0x000301000C0700FF,

        ///<summary>
        ///Description: Quantity_Name= Voltage_Ph_A  Class ID 3 
        /// OBIS_CODE = 1.0.32.7.0.255
        /// Description
        ///</summary>
        Voltage_Ph_A = 0x00030100200700FF,



        ///<summary>
        ///Description: Quantity_Name= Voltage_Ph_B  Class ID 3 
        /// OBIS_CODE = 1.0.52.7.0.255
        /// Description
        ///</summary>
        Voltage_Ph_B = 0x00030100340700FF,



        ///<summary>
        ///Description: Quantity_Name= Voltage_Ph_C  Class ID 3 
        /// OBIS_CODE = 1.0.72.7.0.255
        /// Description
        ///</summary>
        Voltage_Ph_C = 0x00030100480700FF,



        ///<summary>
        ///Description: Quantity_Name= Current_Any  Class ID 3 
        /// OBIS_CODE = 1.0.11.7.0.255
        /// Description
        ///</summary>
        Current_Any = 0x000301000B0700FF,



        ///<summary>
        ///Description: Quantity_Name= Current_Ph_A  Class ID 3 
        /// OBIS_CODE = 1.0.31.7.0.255
        /// Description
        ///</summary>
        Current_Ph_A = 0x000301001F0700FF,



        ///<summary>
        ///Description: Quantity_Name= Current_Ph_B  Class ID 3 
        /// OBIS_CODE = 1.0.51.7.0.255
        /// Description
        ///</summary>
        Current_Ph_B = 0x00030100330700FF,



        ///<summary>
        ///Description: Quantity_Name= Current_Ph_C  Class ID 3 
        /// OBIS_CODE = 1.0.71.7.0.255
        /// Description
        ///</summary>
        Current_Ph_C = 0x00030100470700FF,



        ///<summary>
        ///Description: Quantity_Name= _Apparent_Power_Total  Class ID 3 
        /// OBIS_CODE = 1.0.9.7.0.255
        /// Description
        ///</summary>
        _Apparent_Power_Total = 0x00030100090700FF,



        ///<summary>
        ///Description: Quantity_Name= _Apparent_Power_Phase1  Class ID 3 
        /// OBIS_CODE = 1.0.29.7.0.255
        /// Description
        ///</summary>
        _Apparent_Power_Phase1 = 0x000301001D0700FF,



        ///<summary>
        ///Description: Quantity_Name= _Apparent_Power_Phase2  Class ID 3 
        /// OBIS_CODE = 1.0.49.7.0.255
        /// Description
        ///</summary>
        _Apparent_Power_Phase2 = 0x00030100310700FF,



        ///<summary>
        ///Description: Quantity_Name= _Apparent_Power_Phase3  Class ID 3 
        /// OBIS_CODE = 1.0.69.7.0.255
        /// Description
        ///</summary>
        _Apparent_Power_Phase3 = 0x00030100450700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Total_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.1.7.0.255
        /// Description
        ///</summary>
        Active_Power_Total_Pos = 0x00030100010700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Total_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.2.7.0.255
        /// Description
        ///</summary>
        Active_Power_Total_Neg = 0x00030100020700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Ph_A_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.21.7.0.255
        /// Description
        ///</summary>
        Active_Power_Ph_A_Pos = 0x00030100150700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Ph_A_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.22.7.0.255
        /// Description
        ///</summary>
        Active_Power_Ph_A_Neg = 0x00030100160700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Ph_A_Sign  Class ID 3 
        /// OBIS_CODE = 1.0.36.7.0.255
        /// Description
        ///</summary>
        Active_Power_Ph_A_Sign = 0x00030100240700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Ph_B_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.41.7.0.255
        /// Description
        ///</summary>
        Active_Power_Ph_B_Pos = 0x00030100290700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Ph_B_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.42.7.0.255
        /// Description
        ///</summary>
        Active_Power_Ph_B_Neg = 0x000301002A0700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Ph_B_Sign  Class ID 3 
        /// OBIS_CODE = 1.0.56.7.0.255
        /// Description
        ///</summary>
        Active_Power_Ph_B_Sign = 0x00030100380700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Ph_C_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.61.7.0.255
        /// Description
        ///</summary>
        Active_Power_Ph_C_Pos = 0x000301003D0700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Ph_C_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.62.7.0.255
        /// Description
        ///</summary>
        Active_Power_Ph_C_Neg = 0x000301003E0700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_Ph_C_Sign  Class ID 3 
        /// OBIS_CODE = 1.0.76.7.0.255
        /// Description
        ///</summary>
        Active_Power_Ph_C_Sign = 0x000301004C0700FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Total_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.3.7.0.255
        /// Description
        ///</summary>
        Reactive_Power_Total_Pos = 0x00030100030700FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Total_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.4.7.0.255
        /// Description
        ///</summary>
        Reactive_Power_Total_Neg = 0x00030100040700FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Ph_A_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.23.7.0.255
        /// Description
        ///</summary>
        Reactive_Power_Ph_A_Pos = 0x00030100170700FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Ph_A_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.24.7.0.255
        /// Description
        ///</summary>
        Reactive_Power_Ph_A_Neg = 0x00030100180700FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Ph_A_Sign  Class ID 3 
        /// OBIS_CODE = 1.0.96.98.1.255
        /// Description
        ///</summary>
        Reactive_Power_Ph_A_Sign = 0x00030100606201FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Ph_B_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.43.7.0.255
        /// Description
        ///</summary>
        Reactive_Power_Ph_B_Pos = 0x000301002B0700FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Ph_B_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.44.7.0.255
        /// Description
        ///</summary>
        Reactive_Power_Ph_B_Neg = 0x000301002C0700FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Ph_B_Sign  Class ID 3 
        /// OBIS_CODE = 1.0.96.98.2.255
        /// Description
        ///</summary>
        Reactive_Power_Ph_B_Sign = 0x00030100606202FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Ph_C_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.63.7.0.255
        /// Description
        ///</summary>
        Reactive_Power_Ph_C_Pos = 0x000301003F0700FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Ph_C_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.64.7.0.255
        /// Description
        ///</summary>
        Reactive_Power_Ph_C_Neg = 0x00030100400700FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Power_Ph_C_Sign  Class ID 3 
        /// OBIS_CODE = 1.0.96.98.3.255
        /// Description
        ///</summary>
        Reactive_Power_Ph_C_Sign = 0x00030100606203FF,



        ///<summary>
        ///Description: Quantity_Name= Power_Factor_All  Class ID 3 
        /// OBIS_CODE = 1.0.13.7.0.255
        /// Description
        ///</summary>
        Power_Factor_All = 0x000301000D0700FF,



        ///<summary>
        ///Description: Quantity_Name= Power_Factor_Ph_A  Class ID 3 
        /// OBIS_CODE = 1.0.33.7.0.255
        /// Description
        ///</summary>
        Power_Factor_Ph_A = 0x00030100210700FF,



        ///<summary>
        ///Description: Quantity_Name= Power_Factor_Ph_B  Class ID 3 
        /// OBIS_CODE = 1.0.53.7.0.255
        /// Description
        ///</summary>
        Power_Factor_Ph_B = 0x00030100350700FF,



        ///<summary>
        ///Description: Quantity_Name= Power_Factor_Ph_C  Class ID 3 
        /// OBIS_CODE = 1.0.73.7.0.255
        /// Description
        ///</summary>
        Power_Factor_Ph_C = 0x00030100490700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Tariff  Class ID 1 
        /// OBIS_CODE = 0.0.96.14.0.255
        /// Description
        ///</summary>
        Active_Tariff = 0x00010000600E00FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Season  Class ID 1 
        /// OBIS_CODE = 0.0.96.14.128.255
        /// Description
        ///</summary>
        Active_Season = 0x00010000600E80FF,



        ///<summary>
        ///Description: Quantity_Name= Registry_Activation_For_Active_Tariff  Class ID 6 
        /// OBIS_CODE = 0.0.14.0.0.255
        /// Description
        ///</summary>
        Registry_Activation_For_Active_Tariff = 0x000600000E0000FF,



        ///<summary>
        ///Description: Quantity_Name= Supply_Frequency  Class ID 3 
        /// OBIS_CODE = 1.0.14.7.0.255
        /// Description
        ///</summary>
        Supply_Frequency = 0x000301000E0700FF,

        ///<summary>
        ///Description: Quantity_Name= Supply_Frequency  Class ID 3 
        /// OBIS_CODE = 1.0.14.27.0.255
        /// Description
        ///</summary>
        Average_Supply_Frequency = 0x000301000E1B00FF,


        ///<summary>
        ///Description: Quantity_Name= Frequency_Ph_A  Class ID 3 
        /// OBIS_CODE = 1.0.34.7.0.255
        /// Description
        ///</summary>
        Frequency_Ph_A = 0x00030100220700FF,



        ///<summary>
        ///Description: Quantity_Name= Frequency_Ph_B  Class ID 3 
        /// OBIS_CODE = 1.0.54.7.0.255
        /// Description
        ///</summary>
        Frequency_Ph_B = 0x00030100360700FF,



        ///<summary>
        ///Description: Quantity_Name= Frequency_Ph_C  Class ID 3 
        /// OBIS_CODE = 1.0.74.7.0.255
        /// Description
        ///</summary>
        Frequency_Ph_C = 0x000301004A0700FF,



        ///<summary>
        ///Description: Quantity_Name= Battery_Volts  Class ID 3 
        /// OBIS_CODE = 0.0.96.6.3.255
        /// Description
        ///</summary>
        Battery_Volts = 0x00030000600603FF,



        ///<summary>
        ///Description: Quantity_Name= Available_Billing_Periods_No  Class ID 1 
        /// OBIS_CODE = 1.0.0.1.1.255
        /// Description
        ///</summary>
        Available_Billing_Periods_No = 0x00010100000101FF,



        ///<summary>
        ///Description: Quantity_Name= Billing_Period_Counter_VZ  Class ID 1 
        /// OBIS_CODE = 1.0.0.1.0.255
        /// Description
        ///</summary>
        Billing_Period_Counter_VZ = 0x00010100000100FF,

        ///<summary>
        /// Description: Quantity_Name= Daily_Billing_Period_Counter_VZ  Class ID 1 
        /// OBIS_CODE = 1:1.0.0.1.3.255
        /// Description
        ///</summary>
        Daily_Billing_Period_Counter_VZ = 0x00010100000103FF,


        ///<summary>
        ///Description: Quantity_Name= Data_Last_1_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.101
        /// Description
        ///</summary>
        Data_Last_1_Billing_Periods = 0x0007010062010065,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_2_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.102
        /// Description
        ///</summary>
        Data_Last_2_Billing_Periods = 0x0007010062010066,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_3_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.103
        /// Description
        ///</summary>
        Data_Last_3_Billing_Periods = 0x0007010062010067,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_4_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.104
        /// Description
        ///</summary>
        Data_Last_4_Billing_Periods = 0x0007010062010068,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_5_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.105
        /// Description
        ///</summary>
        Data_Last_5_Billing_Periods = 0x0007010062010069,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_6_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.106
        /// Description
        ///</summary>
        Data_Last_6_Billing_Periods = 0x000701006201006A,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_7_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.107
        /// Description
        ///</summary>
        Data_Last_7_Billing_Periods = 0x000701006201006B,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_8_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.108
        /// Description
        ///</summary>
        Data_Last_8_Billing_Periods = 0x000701006201006C,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_9_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.109
        /// Description
        ///</summary>
        Data_Last_9_Billing_Periods = 0x000701006201006D,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_10_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.110
        /// Description
        ///</summary>
        Data_Last_10_Billing_Periods = 0x000701006201006E,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_11_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.111
        /// Description
        ///</summary>
        Data_Last_11_Billing_Periods = 0x000701006201006F,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_12_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.112
        /// Description
        ///</summary>
        Data_Last_12_Billing_Periods = 0x0007010062010070,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_13_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.113
        /// Description
        ///</summary>
        Data_Last_13_Billing_Periods = 0x0007010062010071,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_14_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.114
        /// Description
        ///</summary>
        Data_Last_14_Billing_Periods = 0x0007010062010072,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_15_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.115
        /// Description
        ///</summary>
        Data_Last_15_Billing_Periods = 0x0007010062010073,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_16_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.116
        /// Description
        ///</summary>
        Data_Last_16_Billing_Periods = 0x0007010062010074,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_17_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.117
        /// Description
        ///</summary>
        Data_Last_17_Billing_Periods = 0x0007010062010075,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_18_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.118
        /// Description
        ///</summary>
        Data_Last_18_Billing_Periods = 0x0007010062010076,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_19_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.119
        /// Description
        ///</summary>
        Data_Last_19_Billing_Periods = 0x0007010062010077,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_20_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.120
        /// Description
        ///</summary>
        Data_Last_20_Billing_Periods = 0x0007010062010078,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_21_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.121
        /// Description
        ///</summary>
        Data_Last_21_Billing_Periods = 0x0007010062010079,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_22_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.122
        /// Description
        ///</summary>
        Data_Last_22_Billing_Periods = 0x000701006201007A,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_23_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.123
        /// Description
        ///</summary>
        Data_Last_23_Billing_Periods = 0x000701006201007B,



        ///<summary>
        ///Description: Quantity_Name= Data_Last_24_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.124
        /// Description
        ///</summary>
        Data_Last_24_Billing_Periods = 0x000701006201007C,



        ///<summary>
        ///Description: Quantity_Name= Data_Vzth_Billing_Period  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.23
        /// Description
        ///</summary>
        Data_Vzth_Billing_Period = 0x0007010062010017,


        ///<summary>
        ///Description: Quantity_Name= Daily_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.2.0.255
        /// Description
        ///</summary>
        Daily_Billing_Periods = 0x00070100620200FF,


        ///<summary>
        ///Description: Quantity_Name = Data_Last_1_Daily_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.2.0.101
        /// Description
        ///</summary>
        Data_Last_1_Daily_Billing_Periods = 0x0007010062020065,



        ///<summary>
        ///Description: Quantity_Name = Data_Last_2_Daily_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.2.0.102
        /// Description
        ///</summary>
        Data_Last_2_Daily_Billing_Periods = 0x0007010062020066,



        ///<summary>
        /// Description: Quantity_Name = Data_Last_3_Daily_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.2.0.103
        /// Description
        ///</summary>
        Data_Last_3_Daily_Billing_Periods = 0x0007010062020067,



        ///<summary>
        /// Description: Quantity_Name = Data_Last_4_Daily_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.2.0.104
        /// Description
        ///</summary>
        Data_Last_4_Daily_Billing_Periods = 0x0007010062020068,



        ///<summary>
        /// Description: Quantity_Name = Data_Last_5_Daily_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.2.0.105
        /// Description
        ///</summary>
        Data_Last_5_Daily_Billing_Periods = 0x0007010062020069,



        ///<summary>
        /// Description: Quantity_Name = Data_Last_6_Daily_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.2.0.106
        /// Description
        ///</summary>
        Data_Last_6_Daily_Billing_Periods = 0x000701006202006A,



        ///<summary>
        /// Description: Quantity_Name = Data_Last_7_Daily_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.2.0.107
        /// Description
        ///</summary>
        Data_Last_7_Daily_Billing_Periods = 0x000701006202006B,


        ///<summary>
        /// Description: Quantity_Name = Data_Last_8_Daily_Billing_Periods  Class ID 7 
        /// OBIS_CODE = 1.0.98.2.0.108
        /// Description
        ///</summary>
        Data_Last_8_Daily_Billing_Periods = 0x000701006202006C,


        ///<summary>
        ///Description: Quantity_Name= Reactive_Energy_Total_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.3.8.0.255
        /// Description
        ///</summary>
        Reactive_Energy_Positive_TL = 0x00030100030800FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Energy_Tariff_1_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.3.8.1.255
        /// Description
        ///</summary>
        Reactive_Energy_Positive_T1 = 0x00030100030801FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Energy_Tariff_2_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.3.8.2.255
        /// Description
        ///</summary>
        Reactive_Energy_Positive_T2 = 0x00030100030802FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Energy_Tariff_3_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.3.8.3.255
        /// Description
        ///</summary>
        Reactive_Energy_Positive_T3 = 0x00030100030803FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Energy_Tariff_4_Pos  Class ID 3 
        /// OBIS_CODE = 1.0.3.8.4.255
        /// Description
        ///</summary>
        Reactive_Energy_Positive_T4 = 0x00030100030804FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Energy_Total_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.4.8.0.255
        /// Description
        ///</summary>
        Reactive_Energy_Negative_TL = 0x00030100040800FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Energy_Tariff_1_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.4.8.1.255
        /// Description
        ///</summary>
        Reactive_Energy_Negative_T1 = 0x00030100040801FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Energy_Tariff_2_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.4.8.2.255
        /// Description
        ///</summary>
        Reactive_Energy_Negative_T2 = 0x00030100040802FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Energy_Tariff_3_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.4.8.3.255
        /// Description
        ///</summary>
        Reactive_Energy_Negative_T3 = 0x00030100040803FF,



        ///<summary>
        ///Description: Quantity_Name= Reactive_Energy_Tariff_4_Neg  Class ID 3 
        /// OBIS_CODE = 1.0.4.8.4.255
        /// Description
        ///</summary>
        Reactive_Energy_Negative_T4 = 0x00030100040804FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Avg_PF  Class ID 3 
        /// OBIS_CODE = 1.0.13.0.0.255
        /// Description
        ///</summary>
        Monthly_Avg_PF = 0x000301000D0000FF,

        ///<summary>
        ///Description: Quantity_Name= Average_PF  Class ID 3 
        /// OBIS_CODE = 1.0.13.0.0.255
        /// Description
        ///</summary>
        Average_PF = 0x000301000D1B00FF,


        ///<summary>
        ///Description: Quantity_Name= Over_Voltage_Limit  Class ID 3 
        /// OBIS_CODE = 1.0.12.35.0.255
        /// Description
        ///</summary>
        Over_Voltage_Limit = 0x000301000C2300FF,



        ///<summary>
        ///Description: Quantity_Name= Under_Voltage_Limit  Class ID 3 
        /// OBIS_CODE = 1.0.12.31.0.255
        /// Description
        ///</summary>
        Under_Voltage_Limit = 0x000301000C1F00FF,



        ///<summary>
        ///Description: Quantity_Name= Over_Current_Limit  Class ID 3 
        /// OBIS_CODE = 1.0.11.35.0.255
        /// Description
        ///</summary>
        Over_Current_Limit = 0x000301000B2300FF,



        ///<summary>
        ///Description: Quantity_Name= Over_Power_Limit  Class ID 3 
        /// OBIS_CODE = 1.0.1.35.0.255
        /// Description
        ///</summary>
        Over_Power_Limit = 0x00030100012300FF,



        ///<summary>
        ///Description: Quantity_Name= Display_Windows_No  Class ID 1 
        /// OBIS_CODE = 0.0.21.0.0.255
        /// Description
        ///</summary>
        Display_Windows_No = 0x00010000150000FF,



        ///<summary>
        ///Description: Quantity_Name= DisplayWindows_ALT_7  Class ID 7 
        /// OBIS_CODE = 0.0.21.0.2.255
        /// Description
        ///</summary>
        DisplayWindows_ALT_7 = 0x00070000150002FF,



        ///<summary>
        ///Description: Quantity_Name= DisplayWindows_NOR_7  Class ID 7 
        /// OBIS_CODE = 0.0.21.0.1.255
        /// Description
        ///</summary>
        DisplayWindows_NOR_7 = 0x00070000150001FF,

        /////<summary>
        /////Description: Quantity_Name= DisplayWindows_NOR_7  Class ID 7 
        ///// OBIS_CODE = 0.0.21.0.3.255
        ///// Description
        /////</summary>
        //DisplayWindows_TestMode_7 = 0x00070000150003FF,

        ///<summary>
        ///Description: Quantity_Name= Manufacturing_ID  Class ID 1 
        /// OBIS_CODE = 0.0.96.1.0.255
        /// Description
        ///</summary>
        Manufacturing_ID = 0x00010000600100FF,



        ///<summary>
        ///Description: Quantity_Name= Current_Association  Class ID 15 
        /// OBIS_CODE = 0.0.40.0.0.255
        /// Description
        ///</summary>
        Current_Association = 0x000F0000280000FF,



        ///<summary>
        ///Description: Quantity_Name= Current_Association1  Class ID 15 
        /// OBIS_CODE = 0.0.40.0.1.255
        /// Description
        ///</summary>
        Current_Association1 = 0x000F0000280001FF,



        ///<summary>
        ///Description: Quantity_Name= Current_Association2  Class ID 15 
        /// OBIS_CODE = 0.0.40.0.2.255
        /// Description
        ///</summary>
        Current_Association2 = 0x000F0000280002FF,



        ///<summary>
        ///Description: Quantity_Name= Current_Association3  Class ID 15 
        /// OBIS_CODE = 0.0.40.0.3.255
        /// Description
        ///</summary>
        Current_Association3 = 0x000F0000280003FF,

        /////<summary>
        /////Description: Quantity_Name= Security_Setup  Class ID 64 
        ///// OBIS_CODE = 0.0.43.0.0.255
        ///// Description
        /////</summary>
        //Security_Setup_Current = 0x004000002B0000FF,


        ///<summary>
        ///Description: Quantity_Name= COSEM_Logical_Device_Name  Class ID 1 
        /// OBIS_CODE = 0.0.42.0.0.255
        /// Description
        ///</summary>
        COSEM_Logical_Device_Name = 0x000100002A0000FF,



        ///<summary>
        ///Description: Quantity_Name= SAP_Assignment_Obj  Class ID 17 
        /// OBIS_CODE = 0.0.41.0.0.255
        /// Description
        ///</summary>
        SAP_Assignment_Obj = 0x00110000290000FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Firmware_ID  Class ID 1 
        /// OBIS_CODE = 1.0.0.2.0.255
        /// Description
        ///</summary>
        Active_Firmware_ID = 0x00010100000200FF,



        ///<summary>
        ///Description: Quantity_Name= Local_Date  Class ID 1 
        /// OBIS_CODE = 1.0.0.9.2.255
        /// Description
        ///</summary>
        Local_Date = 0x00010100000902FF,



        ///<summary>
        ///Description: Quantity_Name= Local_Time  Class ID 1 
        /// OBIS_CODE = 1.0.0.9.1.255
        /// Description
        ///</summary>
        Local_Time = 0x00010100000901FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Log  Class ID 7 
        /// OBIS_CODE = 1.0.99.98.0.255
        /// Description
        ///</summary>
        Event_Log = 0x00070100636200FF,



        ///<summary>
        ///Description: Quantity_Name= Power_Failure_Log  Class ID 7 
        /// OBIS_CODE = 1.0.99.97.0.255
        /// Description
        ///</summary>
        Power_Failure_Log = 0x00070100636100FF,


        ///<summary>
        ///Description: Quantity_Name= Load_Profile  Class ID 7 
        /// OBIS_CODE = 1.0.99.1.0.255
        /// Description
        ///</summary>
        Load_Profile = 0x00070100630100FF,

        ///<summary>
        ///Description: Quantity_Name= Load_Profile_Channel_2  Class ID 7 
        /// OBIS_CODE = 1.0.99.1.1.255
        /// Description
        ///</summary>
        Load_Profile_Channel_2 = 0x00070100630101FF,


        ///<summary>
        ///Description: Quantity_Name= Load_Profile_Counter  Class ID 1 
        /// OBIS_CODE = 1.0.99.1.128.255
        /// Description
        ///</summary>
        Load_Profile_Counter = 0x00010100630180FF,


        ///<summary>
        ///Description: Quantity_Name= Load_Profile_2  Class ID 7 
        /// OBIS_CODE = 1.0.99.2.0.255
        /// Description
        ///</summary>
        Daily_Load_Profile = 0x00070100630200FF,

        ///<summary>
        ///Description: Quantity_Name= Daily_Load_Profile_Channel_2  Class ID 7 
        /// OBIS_CODE = 1.0.99.2.1.255
        /// Description
        ///</summary>
        Daily_Load_Profile_Channel_2 = 0x00070100630201FF,

        ///<summary>
        ///Description: Quantity_Name= Load_Profile_Counter_2  Class ID 1 
        /// OBIS_CODE = 1.0.99.2.128.255
        /// Description
        ///</summary>
        Load_Profile_Counter_2 = 0x00010100630280FF,

        ///<summary>
        ///Description: Quantity_Name= Load_Profile_Capture_Period_2  Class ID 1 
        /// OBIS_CODE = 1.0.99.2.132.255
        /// Description
        ///</summary>
        Load_Profile_Capture_Period_2 = 0x00010100630284FF,

        ///<summary>
        ///Description: Quantity_Name= PQ_Load_Profile  Class ID 7 
        /// OBIS_CODE = 1.0.99.128.0.255
        /// Description
        ///</summary>
        PQ_Load_Profile = 0x00070100638000FF,

        ///<summary>
        ///Description: Quantity_Name= PQ_Load_Profile_Counter  Class ID 1 
        /// OBIS_CODE = 1.0.99.128.128.255
        /// Description
        ///</summary>
        PQ_Load_Profile_Counter = 0x00010100638080FF,

        ///<summary>
        ///Description: Quantity_Name= PQ_Load_Profile_Capture_Period  Class ID 1 
        /// OBIS_CODE = 1.0.99.128.132.255
        /// Description
        ///</summary>
        PQ_Load_Profile_Capture_Period = 0x00010100638084FF,

        ///<summary>
        /// Description: Quantity_Name= MODEM_STATUS  Class ID 1 
        /// OBIS_CODE = 0.0.96.12.128.255
        /// Description
        ///</summary>
        MODEM_STATUS = 0x00010000600C80FF,


        ///<summary>
        /// Description: Quantity_Name= Event_Counter  Class ID 1 
        /// OBIS_CODE = 0.0.96.15.0.255
        /// Description
        ///</summary>
        Event_Counter = 0x00010000600F00FF,


        ///<summary>
        /// Description: Quantity_Name= Event_Counter_01  Class ID 1 
        /// OBIS_CODE = 0.0.96.15.1.255
        /// Description
        ///</summary>
        Event_Counter_001 = 0x00010000600F01FF,

        ///<summary>
        /// Description: Quantity_Name= Event_Counter_01  Class ID 1 
        /// OBIS_CODE = 0.0.96.15.2.255
        /// Description
        ///</summary>
        Event_Counter_002 = 0x00010000600F02FF,


        ///<summary>
        /// Description: Quantity_Name= Event_Counter_01  Class ID 1 
        /// OBIS_CODE = 0.0.96.15.3.255
        /// Description
        ///</summary>
        Event_Counter_003 = 0x00010000600F03FF,

        ///<summary>
        /// Description: Quantity_Name= Event_Counter_01  Class ID 1 
        /// OBIS_CODE = 0.0.96.15.4.255
        /// Description
        ///</summary>
        Event_Counter_004 = 0x00010000600F04FF,

        ///<summary>
        /// Description: Quantity_Name= Event_Counter_01  Class ID 1 
        /// OBIS_CODE = 0.0.96.15.5.255
        /// Description
        ///</summary>
        Event_Counter_005 = 0x00010000600F05FF,


        ///<summary>
        ///Description: Quantity_Name= Power_Failures_No  Class ID 1 
        /// OBIS_CODE = 0.0.96.7.5.255
        /// Description
        ///</summary>
        Power_Failures_No = 0x00010000600705FF,



        ///<summary>
        ///Description: Quantity_Name= Last_Clock_Change_Date  Class ID 1 
        /// OBIS_CODE = 0.0.96.2.12.255
        /// Description
        ///</summary>
        Last_Clock_Change_Date = 0x0001000060020CFF,


        ///<summary>
        ///Description: Quantity_Name= Configuration_Changes_No  Class ID 1 
        /// OBIS_CODE = 0.0.96.2.0.255
        /// Description
        ///</summary>
        Configuration_Changes_No = 0x00010000600200FF,


        ///<summary>
        ///Description: Quantity_Name= Connection_Profile  Class ID 1 
        /// OBIS_CODE = 0.0.99.12.1.255
        /// Description
        ///</summary>
        Connection_Profile = 0x00010000630C01FF,



        ///<summary>
        ///Description: Quantity_Name= Over_Voltage_Occurrence_Counter  Class ID 1 
        /// OBIS_CODE = 1.0.12.36.0.255
        /// Description
        ///</summary>
        Over_Voltage_Occurrence_Counter = 0x000101000C2400FF,



        ///<summary>
        ///Description: Quantity_Name= Under_Voltage_Occurrence_Counter  Class ID 1 
        /// OBIS_CODE = 1.0.12.32.0.255
        /// Description
        ///</summary>
        Under_Voltage_Occurrence_Counter = 0x000101000C2000FF,


        ///<summary>
        /// Description: Quantity_Name= Over_Current_Occurrence_Counter  Class ID 1 
        /// OBIS_CODE = 1.0.11.36.0.255
        /// Description
        ///</summary>
        Over_Current_Occurrence_Counter = 0x000101000B2400FF,


        ///<summary>
        /// Description: Quantity_Name= Power_Failure_Any_Ph_Duration  Class ID 1 
        /// OBIS_CODE = 0.0.96.7.19.255
        /// Description
        ///</summary>
        Power_Failure_Any_Ph_Duration = 0x00010000600713FF,


        ///<summary>
        /// Description: Quantity_Name= Power_Failure_Time  Class ID 1 
        /// OBIS_CODE = 0.0.96.7.10.255
        /// Description
        ///</summary>
        Power_Failure_Time = 0x0001000060070AFF,


        ///<summary>
        /// Description: Quantity_Name= Power_Failure_Time_Any_Ph  Class ID 3 
        /// OBIS_CODE = 0.0.96.7.15.255
        /// Description
        ///</summary>
        Power_Failure_Time_Any_Ph = 0x0003000060070FFF,


        ///<summary>
        /// Description: Quantity_Name= Over_Voltage_Duration  Class ID 3 
        /// OBIS_CODE = 1.0.12.37.0.255
        /// Description
        ///</summary>
        Over_Voltage_Duration = 0x000301000C2500FF,


        ///<summary>
        /// Description: Quantity_Name= Under_Voltage_Duration  Class ID 3 
        /// OBIS_CODE = 1.0.12.33.0.255
        /// Description
        ///</summary>
        Under_Voltage_Duration = 0x000301000C2100FF,


        ///<summary>
        /// Description: Quantity_Name= Over_Current_Duration  Class ID 3 
        /// OBIS_CODE = 1.0.11.37.0.255
        /// Description
        ///</summary>
        Over_Current_Duration = 0x000301000B2500FF,


        ///<summary>
        /// Description: Quantity_Name= Over_Load_Duration  Class ID 3 
        /// OBIS_CODE = 1.0.1.37.0.255
        /// Description
        ///</summary>
        Over_Load_Duration = 0x00030100012500FF,


        ///<summary>
        /// Description: Quantity_Name= Customer_Reference_No  Class ID 1 
        /// OBIS_CODE = 0.0.96.1.10.255
        /// Description
        ///</summary>
        Customer_Reference_No = 0x0001000060010AFF,


        ///<summary>
        /// Description: Quantity_Name= Customer_Name  Class ID 1 
        /// OBIS_CODE = 0.0.96.1.1.255
        /// Description
        ///</summary>
        Customer_Name = 0x00010000600101FF,


        ///<summary>
        /// Description: Quantity_Name= Customer_Address  Class ID 1 
        /// OBIS_CODE = 0.0.96.1.2.255
        /// Description
        ///</summary>
        Customer_Address = 0x00010000600102FF,


        ///<summary>
        /// Description: Quantity_Name= GlobalMeterReset_Script_Table  Class ID 9 
        /// OBIS_CODE = 0.0.10.0.0.255
        /// Description
        ///</summary>
        GlobalMeterReset_Script_Table = 0x000900000A0000FF,


        /////<summary>
        ///// Description: Quantity_Name= MDIReset_Script_Table  Class ID 9 
        ///// OBIS_CODE = 0.0.10.0.1.255
        ///// Description
        /////</summary>
        //MDIReset_Script_Table = 0x000900000A0064FF,


        ///<summary>
        /// Description: Quantity_Name= Tariffication_Script_Table  Class ID 9 
        /// OBIS_CODE = 0.0.10.0.100.255
        /// Description
        ///</summary>
        Tariffication_Script_Table = 0x000900000A0064FF,


        ///<summary>
        /// Description: Quantity_Name= ActivateTestMode_Script_Table  Class ID 9 
        /// OBIS_CODE = 0.0.10.0.101.255
        /// Description
        ///</summary>
        ActivateTestMode_Script_Table = 0x000900000A0065FF,


        ///<summary>
        /// Description: Quantity_Name= ActivateNormalMode_Script_Table  Class ID 9 
        /// OBIS_CODE = 0.0.10.0.102.255
        /// Description
        ///</summary>
        ActivateNormalMode_Script_Table = 0x000900000A0066FF,


        ///<summary>
        /// Description: Quantity_Name= SetOutputSignals_Script_Table  Class ID 9 
        /// OBIS_CODE = 0.0.10.0.103.255
        /// Description
        ///</summary>
        SetOutputSignals_Script_Table = 0x000900000A0067FF,

        ///<summary>
        /// Description: Quantity_Name= SwitchOpticalTestOutput_Script_Table  Class ID 9 
        /// OBIS_CODE = 0.0.10.0.104.255
        /// Description
        ///</summary>
        SwitchOpticalTestOutput_Script_Table = 0x000900000A0068FF,

        ///<summary>
        /// Description: Quantity_Name= PowerQualityMeasurementManagement_Script_Table  Class ID 9 
        /// OBIS_CODE = 0.0.10.0.105.255
        /// Description
        ///</summary>
        PowerQualityMeasurementManagement_Script_Table = 0x000900000A0069FF,

        ///<summary>
        /// Description: Quantity_Name= Contactor_Script_Table  Class ID 9 
        /// OBIS_CODE = 0.0.10.0.106.255
        /// Description
        ///</summary>
        Contactor_Script_Table = 0x000900000A006AFF,

        ///<summary>
        /// Description: Quantity_Name= ImageActivation_Script_Table  Class ID 9 
        /// OBIS_CODE = 0.0.10.0.107.255
        /// Description
        ///</summary>
        ImageActivation_Script_Table = 0x000900000A006BFF,

        ///<summary>
        /// Description: Quantity_Name= Broadcast_Script_Table  Class ID 9 
        /// OBIS_CODE = 0.0.10.0.108.255
        /// Description
        ///</summary>
        Broadcast_Script_Table = 0x000900000A006CFF,

        // Schedule 0.b.12.0.e.255 => 0.0.12.0.x.255

        /////<summary>
        ///// Description: Quantity_Name= Schedule  Class ID 10 
        ///// OBIS_CODE = 0.0.12.0.0.255
        ///// Description
        /////</summary>
        //Schedule = 0x000A00000C0000FF,


        ///<summary>
        /// Description: Quantity_Name= Schedule_001  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.1.255
        /// Description
        ///</summary>
        Schedule_001 = 0x000A00000C0001FF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_002  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.2.255
        /// Description
        ///</summary>
        Schedule_002 = 0x000A00000C0002FF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_003  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.3.255
        /// Description
        ///</summary>
        Schedule_003 = 0x000A00000C0003FF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_004  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.4.255
        /// Description
        ///</summary>
        Schedule_004 = 0x000A00000C0004FF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_005  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.5.255
        /// Description
        ///</summary>
        Schedule_005 = 0x000A00000C0005FF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_006  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.6.255
        /// Description
        ///</summary>
        Schedule_006 = 0x000A00000C0006FF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_007  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.7.255
        /// Description
        ///</summary>
        Schedule_007 = 0x000A00000C0007FF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_008  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.8.255
        /// Description
        ///</summary>
        Schedule_008 = 0x000A00000C0008FF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_009  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.9.255
        /// Description
        ///</summary>
        Schedule_009 = 0x000A00000C0009FF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_010  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.10.255
        /// Description
        ///</summary>
        Schedule_010 = 0x000A00000C000AFF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_011  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.11.255
        /// Description
        ///</summary>
        Schedule_011 = 0x000A00000C000BFF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_012  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.12.255
        /// Description
        ///</summary>
        Schedule_012 = 0x000A00000C000CFF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_013  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.13.255
        /// Description
        ///</summary>
        Schedule_013 = 0x000A00000C000DFF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_014  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.14.255
        /// Description
        ///</summary>
        Schedule_014 = 0x000A00000C000EFF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_015  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.15.255
        /// Description
        ///</summary>
        Schedule_015 = 0x000A00000C000FFF,

        ///<summary>
        /// Description: Quantity_Name= Schedule_016  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.16.255
        /// Description
        ///</summary>
        Schedule_016 = 0x000A00000C0010FF,



        ///<summary>
        /// Description: Quantity_Name= Activity_Calendar  Class ID 20 
        /// OBIS_CODE = 0.0.13.0.0.255
        /// Description
        ///</summary>
        Activity_Calendar = 0x001400000D0000FF,



        ///<summary>
        /// Description: Quantity_Name= Special_Days_Table  Class ID 11 
        /// OBIS_CODE = 0.0.11.0.0.255
        /// Description
        ///</summary>
        Special_Days_Table = 0x000B00000B0000FF,



        ///<summary>
        /// Description: Quantity_Name= TCP_UDP_Setup  Class ID 41 
        /// OBIS_CODE = 0.0.25.0.0.255
        /// Description
        ///</summary>
        TCP_UDP_Setup = 0x00290000190000FF,



        ///<summary>
        /// Description: Quantity_Name= IPv4  Class ID 42 
        /// OBIS_CODE = 0.0.25.1.0.255
        /// Description
        ///</summary>
        IPv4 = 0x002A0000190100FF,



        ///<summary>
        /// Description: Quantity_Name= GPRS_Modem_Configuration  Class ID 45 
        /// OBIS_CODE = 0.0.25.4.0.255
        /// Description
        ///</summary>
        GPRS_Modem_Configuration = 0x002D0000190400FF,



        ///<summary>
        /// Description: Quantity_Name= Reading_Factor_Power  Class ID 3 
        /// OBIS_CODE = 1.0.0.4.0.255
        /// Description
        ///</summary>
        Reading_Factor_Power = 0x00030100000400FF,



        ///<summary>
        /// Description: Quantity_Name= Reading_Factor_Energy  Class ID 1 
        /// OBIS_CODE = 1.0.0.4.1.255
        /// Description
        ///</summary>
        Reading_Factor_Energy = 0x00010100000401FF,



        ///<summary>
        /// Description: Quantity_Name= CT_Ratio_Numerator  Class ID 1 
        /// OBIS_CODE = 1.0.0.4.2.255
        /// Description
        ///</summary>
        CT_Ratio_Numerator = 0x00010100000402FF,



        ///<summary>
        /// Description: Quantity_Name= PT_Ratio_Numerator  Class ID 1 
        /// OBIS_CODE = 1.0.0.4.3.255
        /// Description
        ///</summary>
        PT_Ratio_Numerator = 0x00010100000403FF,



        ///<summary>
        /// Description: Quantity_Name= CT_Ratio_Denominator  Class ID 1 
        /// OBIS_CODE = 1.0.0.4.5.255
        /// Description
        ///</summary>
        CT_Ratio_Denominator = 0x00010100000405FF,



        ///<summary>
        /// Description: Quantity_Name= PT_Ratio_Denominator  Class ID 1 
        /// OBIS_CODE = 1.0.0.4.6.255
        /// Description
        ///</summary>
        PT_Ratio_Denominator = 0x00010100000406FF,



        ///<summary>
        /// Description: Quantity_Name= Load_Profile_Capture_Period  Class ID 1 
        /// OBIS_CODE = 1.0.99.1.132.255
        /// Description
        ///</summary>
        Load_Profile_Capture_Period = 0x00010100630184FF,



        ///<summary>
        /// Description: Quantity_Name= Measurement_Algo_Power_Active  Class ID 1 
        /// OBIS_CODE = 1.0.0.11.1.255
        /// Description
        ///</summary>
        Measurement_Algo_Power_Active = 0x00010100000B01FF,



        ///<summary>
        /// Description: Quantity_Name= Measurement_Algo_Energy_Active  Class ID 1 
        /// OBIS_CODE = 1.0.0.11.2.255
        /// Description
        ///</summary>
        Measurement_Algo_Energy_Active = 0x00010100000B02FF,



        ///<summary>
        /// Description: Quantity_Name= Measurement_Algo_Power_Reactive  Class ID 1 
        /// OBIS_CODE = 1.0.0.11.3.255
        /// Description
        ///</summary>
        Measurement_Algo_Power_Reactive = 0x00010100000B03FF,



        ///<summary>
        /// Description: Quantity_Name= Measurement_Algo_Energy_Reactive  Class ID 1 
        /// OBIS_CODE = 1.0.0.11.4.255
        /// Description
        ///</summary>
        Measurement_Algo_Energy_Reactive = 0x00010100000B04FF,



        ///<summary>
        /// Description: Quantity_Name= Measurement_Algo_Power_Apparent  Class ID 1 
        /// OBIS_CODE = 1.0.0.11.5.255
        /// Description
        ///</summary>
        Measurement_Algo_Power_Apparent = 0x00010100000B05FF,



        ///<summary>
        /// Description: Quantity_Name= Measurement_Algo_Energy_Apparent  Class ID 1 
        /// OBIS_CODE = 1.0.0.11.6.255
        /// Description
        ///</summary>
        Measurement_Algo_Energy_Apparent = 0x00010100000B06FF,



        ///<summary>
        /// Description: Quantity_Name= Measurement_Algo_PF  Class ID 1 
        /// OBIS_CODE = 1.0.0.11.7.255
        /// Description
        ///</summary>
        Measurement_Algo_PF = 0x00010100000B07FF,



        ///<summary>
        /// Description: Quantity_Name= Billing_Periods_Data  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.0.255
        /// Description
        ///</summary>
        Billing_Periods_Data = 0x00070100620100FF,



        /////<summary>
        ///// Description: Quantity_Name= MDI_Parameters  Class ID 5 
        ///// OBIS_CODE = 1.0.1.4.0.255
        ///// Description
        /////</summary>
        //MDI_Parameters = 0x00050100010400FF,



        ///<summary>
        /// Description: Quantity_Name= Instantaneous_MDI  Class ID 1 
        /// OBIS_CODE = 1.0.1.142.0.255
        /// Description
        ///</summary>
        Instantaneous_MDI = 0x00010100018E00FF,



        ///<summary>
        ///Description: Quantity_Name= _DecimalPoint  Class ID 1 
        /// OBIS_CODE = 0.0.21.128.0.255
        /// Description
        ///</summary>
        _DecimalPoint = 0x00010000158000FF,



        ///<summary>
        ///Description: Quantity_Name= _EneryParams  Class ID 1 
        /// OBIS_CODE = 1.0.128.129.1.255
        /// Description
        ///</summary>
        _EneryParams = 0x00010100808101FF,



        ///<summary>
        ///Description: Quantity_Name= Meter_Clock  Class ID 8 
        /// OBIS_CODE = 0.0.1.0.0.255
        /// Description
        ///</summary>
        Meter_Clock = 0x00080000010000FF,



        ///<summary>
        ///Description: Quantity_Name= Clock_Caliberation_Flags  Class ID 1 
        /// OBIS_CODE = 0.0.1.129.0.255
        /// Description
        ///</summary>
        Clock_Caliberation_Flags = 0x00010000018100FF,



        ///<summary>
        ///Description: Quantity_Name= Clock_Caliberation  Class ID 1 
        /// OBIS_CODE = 0.0.1.128.0.255
        /// Description
        ///</summary>
        Clock_Caliberation = 0x00010000018000FF,



        ///<summary>
        ///Description: Quantity_Name= ContactorParameters  Class ID 1 
        /// OBIS_CODE = 0.0.96.3.128.255
        /// Description
        ///</summary>
        ContactorParameters = 0x00010000600380FF,


        /////<summary>
        /////Description: Quantity_Name= RELAY_STATUS  Class ID 70 
        ///// OBIS_CODE = 0.0.96.3.10.255
        ///// Description
        /////</summary>
        //RELAY_STATUS = 0x00460000600310FF,

        ///<summary>
        ///Description: Quantity_Name= DataProfilesWithEvents_1_Unique_ID  Class ID 1 
        /// OBIS_CODE = 0.0.97.143.1.255
        /// Description
        ///</summary>
        DataProfilesWithEvents_1_Unique_ID = 0x00010000618F01FF,



        ///<summary>
        ///Description: Quantity_Name= DataProfilesWithEvents_2_Unique_ID  Class ID 1 
        /// OBIS_CODE = 0.0.97.143.2.255
        /// Description
        ///</summary>
        DataProfilesWithEvents_2_Unique_ID = 0x00010000618F02FF,



        ///<summary>
        ///Description: Quantity_Name= DataProfilesWithEvents_3_Unique_ID  Class ID 1 
        /// OBIS_CODE = 0.0.97.143.3.255
        /// Description
        ///</summary>
        DataProfilesWithEvents_3_Unique_ID = 0x00010000618F03FF,



        ///<summary>
        ///Description: Quantity_Name= DataProfilesWithEvents_4_Unique_ID  Class ID 1 
        /// OBIS_CODE = 0.0.97.143.4.255
        /// Description
        ///</summary>
        DataProfilesWithEvents_4_Unique_ID = 0x00010000618F04FF,



        ///<summary>
        ///Description: Quantity_Name= DataProfilesWithEvents_5_Unique_ID  Class ID 1 
        /// OBIS_CODE = 0.0.97.143.5.255
        /// Description
        ///</summary>
        DataProfilesWithEvents_5_Unique_ID = 0x00010000618F05FF,



        ///<summary>
        ///Description: Quantity_Name= DataProfilesWithEvents_6_Unique_ID  Class ID 1 
        /// OBIS_CODE = 0.0.97.143.6.255
        /// Description
        ///</summary>
        DataProfilesWithEvents_6_Unique_ID = 0x00010000618F06FF,



        ///<summary>
        ///Description: Quantity_Name= DataProfilesWithEvents_7_Unique_ID  Class ID 1 
        /// OBIS_CODE = 0.0.97.143.7.255
        /// Description
        ///</summary>
        DataProfilesWithEvents_7_Unique_ID = 0x00010000618F07FF,



        ///<summary>
        ///Description: Quantity_Name= DataProfilesWithEvents_8_Unique_ID  Class ID 1 
        /// OBIS_CODE = 0.0.97.143.8.255
        /// Description
        ///</summary>
        DataProfilesWithEvents_8_Unique_ID = 0x00010000618F08FF,



        ///<summary>
        ///Description: Quantity_Name= EventsCautions_Event_Code  Class ID 1 
        /// OBIS_CODE = 0.0.96.11.0.255
        /// Description
        ///</summary>
        //  EventsCautions_Event_Code = 0x00010000600B00FF,
        //ahmed


        ///<summary>
        ///Description: Quantity_Name= EventsCautions_Caution_Number  Class ID 1 
        /// OBIS_CODE = 0.0.96.11.129.255
        /// Description
        ///</summary>
        EventsCautions_Caution_Number = 0x00010000600B81FF,



        ///<summary>
        ///Description: Quantity_Name= EventsCautions_Flag  Class ID 1 
        /// OBIS_CODE = 0.0.96.5.0.255
        /// Description
        ///</summary>
        EventsCautions_Flag = 0x00010000600500FF,



        ///<summary>
        ///Description: Quantity_Name= KeepAliveIP_1_IP_Profile_ID  Class ID 1 
        /// OBIS_CODE = 0.0.25.167.1.255
        /// Description
        ///</summary>
        //KeepAliveIP_1_IP_Profile_ID = 0x0001000019A701FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_CT_Fail__Amp_Limit  Class ID 3 
        /// OBIS_CODE = 1.0.130.35.0.255
        /// Description
        ///</summary>
        Limits_CT_Fail__Amp_Limit = 0x00030100822300FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Demand_Over_Load_T1  Class ID 21 
        /// OBIS_CODE = 1.0.1.5.1.255
        /// Description
        ///</summary>
        Limits_Demand_Over_Load_T1 = 0x00150100010501FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Demand_Over_Load_T2  Class ID 21 
        /// OBIS_CODE = 1.0.1.5.2.255
        /// Description
        ///</summary>
        Limits_Demand_Over_Load_T2 = 0x00150100010502FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Demand_Over_Load_T3  Class ID 21 
        /// OBIS_CODE = 1.0.1.5.3.255
        /// Description
        ///</summary>
        Limits_Demand_Over_Load_T3 = 0x00150100010503FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Demand_Over_Load_T4  Class ID 21 
        /// OBIS_CODE = 1.0.1.5.4.255
        /// Description
        ///</summary>
        Limits_Demand_Over_Load_T4 = 0x00150100010504FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_High_Neutral_Current  Class ID 3 
        /// OBIS_CODE = 1.0.91.35.0.255
        /// Description
        ///</summary>
        Limits_High_Neutral_Current = 0x000301005B2300FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Imbalance_Volt  Class ID 3 
        /// OBIS_CODE = 1.0.128.35.0.255
        /// Description
        ///</summary>
        Limits_Imbalance_Volt = 0x00030100802300FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Current_By_Phase_T1  Class ID 3 
        /// OBIS_CODE = 1.0.11.35.1.255
        /// Description
        ///</summary>
        Limits_Over_Current_By_Phase_T1 = 0x000301000B2301FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Current_By_Phase_T2  Class ID 3 
        /// OBIS_CODE = 1.0.11.35.2.255
        /// Description
        ///</summary>
        Limits_Over_Current_By_Phase_T2 = 0x000301000B2302FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Current_By_Phase_T3  Class ID 3 
        /// OBIS_CODE = 1.0.11.35.3.255
        /// Description
        ///</summary>
        Limits_Over_Current_By_Phase_T3 = 0x000301000B2303FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Current_By_Phase_T4  Class ID 3 
        /// OBIS_CODE = 1.0.11.35.4.255
        /// Description
        ///</summary>
        Limits_Over_Current_By_Phase_T4 = 0x000301000B2304FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase1_T1  Class ID 3 
        /// OBIS_CODE = 1.0.21.35.1.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase1_T1 = 0x00030100152301FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase1_T2  Class ID 3 
        /// OBIS_CODE = 1.0.21.35.2.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase1_T2 = 0x00030100152302FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase1_T3  Class ID 3 
        /// OBIS_CODE = 1.0.21.35.3.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase1_T3 = 0x00030100152303FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase1_T4  Class ID 3 
        /// OBIS_CODE = 1.0.21.35.4.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase1_T4 = 0x00030100152304FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase2_T1  Class ID 3 
        /// OBIS_CODE = 1.0.41.35.1.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase2_T1 = 0x00030100292301FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase2_T2  Class ID 3 
        /// OBIS_CODE = 1.0.41.35.2.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase2_T2 = 0x00030100292302FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase2_T3  Class ID 3 
        /// OBIS_CODE = 1.0.41.35.3.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase2_T3 = 0x00030100292303FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase2_T4  Class ID 3 
        /// OBIS_CODE = 1.0.41.35.4.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase2_T4 = 0x00030100292304FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase3_T1  Class ID 3 
        /// OBIS_CODE = 1.0.61.35.1.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase3_T1 = 0x000301003D2301FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase3_T2  Class ID 3 
        /// OBIS_CODE = 1.0.61.35.2.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase3_T2 = 0x000301003D2302FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase3_T3  Class ID 3 
        /// OBIS_CODE = 1.0.61.35.3.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase3_T3 = 0x000301003D2303FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_By_Phase3_T4  Class ID 3 
        /// OBIS_CODE = 1.0.61.35.4.255
        /// Description
        ///</summary>
        Limits_Over_Load_By_Phase3_T4 = 0x000301003D2304FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_Total_T1  Class ID 3 
        /// OBIS_CODE = 1.0.1.35.1.255
        /// Description
        ///</summary>
        Limits_Over_Load_Total_T1 = 0x00030100012301FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_Total_T2  Class ID 3 
        /// OBIS_CODE = 1.0.1.35.2.255
        /// Description
        ///</summary>
        Limits_Over_Load_Total_T2 = 0x00030100012302FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_Total_T3  Class ID 3 
        /// OBIS_CODE = 1.0.1.35.3.255
        /// Description
        ///</summary>
        Limits_Over_Load_Total_T3 = 0x00030100012303FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Over_Load_Total_T4  Class ID 3 
        /// OBIS_CODE = 1.0.1.35.4.255
        /// Description
        ///</summary>
        Limits_Over_Load_Total_T4 = 0x00030100012304FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_PT_Fail_Amp_Limit  Class ID 3 
        /// OBIS_CODE = 1.0.131.35.0.255
        /// Description
        ///</summary>
        Limits_PT_Fail_Amp_Limit = 0x00030100832300FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_PT_Fail_Volt_Limit  Class ID 3 
        /// OBIS_CODE = 1.0.132.35.0.255
        /// Description
        ///</summary>
        Limits_PT_Fail_Volt_Limit = 0x00030100842300FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Reverse_Energy_  Class ID 3 
        /// OBIS_CODE = 1.0.2.35.0.255
        /// Description
        ///</summary>
        Limits_Reverse_Energy_ = 0x00030100022300FF,



        ///<summary>
        ///Description: Quantity_Name= Limits_Tamper_Energy  Class ID 3 
        /// OBIS_CODE = 1.0.129.35.0.255
        /// Description
        ///</summary>
        Limits_Tamper_Energy = 0x00030100812300FF,


        ///<summary>
        ///Description: Quantity_Name= AlarmUserStatus  Class ID 1 
        /// OBIS_CODE = 0.0.97.98.17.255
        /// Description
        ///</summary>
        AlarmUserStatus = 0x00010000616211FF,

        ///<summary>
        ///Description: Quantity_Name= AlarmStatus  Class ID 1 
        /// OBIS_CODE = 0.0.97.98.0.255
        /// Description
        ///</summary>
        AlarmStatus = 0x00010000616200FF,


        ///<summary>
        ///Description: Quantity_Name= MajorAlarmFilter  Class ID 1 
        /// OBIS_CODE = 0.0.97.98.10.255
        /// Description
        ///</summary>
        MajorAlarmFilter = 0x0001000061620AFF,



        ///<summary>
        ///Description: Quantity_Name= MajorAlarmProfile  Class ID 1 
        /// OBIS_CODE = 0.0.97.141.1.255
        /// Description
        ///</summary>
        MajorAlarmProfile = 0x00010000618D01FF,



        ///<summary>
        ///Description: Quantity_Name= MajorAlarmProfile_Event_Code  Class ID 1 
        /// OBIS_CODE = 0.0.96.11.0.255
        /// Description
        ///</summary>
        MajorAlarmProfile_Event_Code = 0x00010000600B00FF,



        ///<summary>
        ///Description: Quantity_Name= MDIParams_Auto_Reset_Date  Class ID 22 
        /// OBIS_CODE = 0.0.15.0.0.255
        /// Description
        ///</summary>
        MDIParams_Auto_Reset_Date = 0x001600000F0000FF,



        ///<summary>
        ///Description: Quantity_Name= MDIParams_Flag  Class ID 1 
        /// OBIS_CODE = 1.0.1.140.0.255
        /// Description
        ///</summary>
        MDIParams_Flag = 0x00010100018C00FF,



        ///<summary>
        ///Description: Quantity_Name= MDIParams_MinTimeIntervalBetweenResetsIncaseofManualReset  Class ID 1 
        /// OBIS_CODE = 1.0.1.141.0.255
        /// Description
        ///</summary>
        MDIParams_MinTimeIntervalBetweenResetsIncaseofManualReset = 0x00010100018D00FF,



        ///<summary>
        ///Description: Quantity_Name= ModemBasics_Password  Class ID 1 
        /// OBIS_CODE = 0.0.25.170.1.255
        /// Description
        ///</summary>
        ModemBasics_Password = 0x0001000019AA01FF,



        ///<summary>
        ///Description: Quantity_Name= ModemBasics_User_Name  Class ID 1 
        /// OBIS_CODE = 0.0.25.170.0.255
        /// Description
        ///</summary>
        ModemBasics_User_Name = 0x0001000019AA00FF,



        ///<summary>
        ///Description: Quantity_Name= ModemLimitsAndTime  Class ID 1 
        /// OBIS_CODE = 0.0.25.170.2.255
        /// Description
        ///</summary>
        //ModemLimitsAndTime = 0x0001000019AA02FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_CT_Fail  Class ID 3 
        /// OBIS_CODE = 1.0.132.43.0.255
        /// Description
        ///</summary>
        MonitoringTime_CT_Fail = 0x00030100842B00FF,


        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Earth  Class ID 3 
        /// OBIS_CODE = 1.0.165.44.0.255
        /// Description
        ///</summary>
        MonitoringTime_Earth = 0x00030100A52C00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Flag  Class ID 1 
        /// OBIS_CODE = 1.0.96.4.1.255
        /// Description
        ///</summary>
        MonitoringTime_Flag = 0x00010100600401FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_High_Neutral_Current  Class ID 3 
        /// OBIS_CODE = 1.0.91.44.0.255
        /// Description
        ///</summary>
        MonitoringTime_High_Neutral_Current = 0x000301005B2C00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Imbalance_Volt  Class ID 3 
        /// OBIS_CODE = 1.0.128.43.0.255
        /// Description
        ///</summary>
        MonitoringTime_Imbalance_Volt = 0x00030100802B00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Over_Current  Class ID 3 
        /// OBIS_CODE = 1.0.11.44.0.255
        /// Description
        ///</summary>
        MonitoringTime_Over_Current = 0x000301000B2C00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Over_Load  Class ID 3 
        /// OBIS_CODE = 1.0.1.44.0.255
        /// Description
        ///</summary>
        MonitoringTime_Over_Load = 0x00030100012C00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Over_Volt_  Class ID 3 
        /// OBIS_CODE = 1.0.12.44.0.255
        /// Description
        ///</summary>
        MonitoringTime_Over_Volt_ = 0x000301000C2C00FF,



        /////<summary>
        /////Description: Quantity_Name= MonitoringTime_Phase_Fail  Class ID 3 
        ///// OBIS_CODE = 0.0.96.7.14.255
        ///// Description
        /////</summary>
        // MonitoringTime_Phase_Fail = 0x0003000060070EFF, // FOR PITC

        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Phase_Fail  Class ID 3 
        /// OBIS_CODE = 0.0.96.7.128.255
        /// Description
        ///</summary>
        MonitoringTime_Phase_Fail = 0x00030000600780FF,  // FOR IRG



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Phase_Sequence  Class ID 3 
        /// OBIS_CODE = 1.0.130.43.0.255
        /// Description
        ///</summary>
        MonitoringTime_Phase_Sequence = 0x00030100822B00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Power_Fail  Class ID 3 
        /// OBIS_CODE = 0.0.96.7.20.255
        /// Description
        ///</summary>
        MonitoringTime_Power_Fail = 0x00030000600714FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Power_Up_Delay_For_Energy_Recording  Class ID 1 
        /// OBIS_CODE = 1.0.135.43.0.255
        /// Description
        ///</summary>
        MonitoringTime_Power_Up_Delay_For_Energy_Recording = 0x00030100872B00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Power_Up_Delay_To_Monitor  Class ID 3 
        /// OBIS_CODE = 1.0.134.43.0.255
        /// Description
        ///</summary>
        MonitoringTime_Power_Up_Delay_To_Monitor = 0x00030100862B00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_PT_Fail  Class ID 3 
        /// OBIS_CODE = 1.0.133.43.0.255
        /// Description
        ///</summary>
        MonitoringTime_PT_Fail = 0x00030100852B00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Reverse_Energy  Class ID 1 
        /// OBIS_CODE = 1.0.2.44.0.255
        /// Description
        ///</summary>
        MonitoringTime_Reverse_Energy = 0x00030100022C00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Reverse_Polarity  Class ID 3 
        /// OBIS_CODE = 1.0.129.43.0.255
        /// Description
        ///</summary>
        MonitoringTime_Reverse_Polarity = 0x00030100812B00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Tamper_Energy  Class ID 3 
        /// OBIS_CODE = 1.0.131.43.0.255
        /// Description
        ///</summary>
        MonitoringTime_Tamper_Energy = 0x00030100832B00FF,



        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_Under_Volt Class ID 3 
        /// OBIS_CODE = 1.0.12.43.0.255
        /// Description
        ///</summary>
        MonitoringTime_Under_Volt = 0x000301000C2B00FF,

        ///<summary>
        ///Description: Quantity_Name= MonitoringTime_HALL_Sensor Class ID 3 
        /// OBIS_CODE = 1.0.136.43.0.255
        /// Description
        ///</summary>
        MonitoringTime_HALL_Sensor = 0x00030100882B00FF,

        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_KwhImport  Class ID 3 
        /// OBIS_CODE = 1.0.1.8.1.255
        /// Description
        ///</summary>
        Active_Energy_Positive_T1 = 0x00030100010801FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_KwhExport  Class ID 3 
        /// OBIS_CODE = 1.0.2.8.1.255
        /// Description
        ///</summary>
        Active_Energy_Negative_T1 = 0x00030100020801FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_KwhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.15.8.1.255
        /// Description
        ///</summary>
        Active_Energy_Absolute_T1 = 0x000301000F0801FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_KvarhQ1  Class ID 3 
        /// OBIS_CODE = 1.0.5.8.1.255
        /// Description
        ///</summary>
        Cumulative_Tariff1_KvarhQ1 = 0x00030100050801FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_KvarhQ2  Class ID 3 
        /// OBIS_CODE = 1.0.6.8.1.255
        /// Description
        ///</summary>
        Cumulative_Tariff1_KvarhQ2 = 0x00030100060801FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_KvarhQ3  Class ID 3 
        /// OBIS_CODE = 1.0.7.8.1.255
        /// Description
        ///</summary>
        Cumulative_Tariff1_KvarhQ3 = 0x00030100070801FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_KvarhQ4  Class ID 3 
        /// OBIS_CODE = 1.0.8.8.1.255
        /// Description
        ///</summary>
        Cumulative_Tariff1_KvarhQ4 = 0x00030100080801FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_KvarhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.94.92.1.255
        /// Description
        ///</summary>
        Reactive_Energy_Absolute_T1 = 0x00030100800801FF,

        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_KvarhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.94.92.2.255
        /// Description
        ///</summary>
        Reactive_Energy_Absolute_T2 = 0x00030100800802FF,

        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_KvarhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.94.92.3.255
        /// Description
        ///</summary>
        Reactive_Energy_Absolute_T3 = 0x00030100800803FF,

        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_KvarhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.94.92.4.255
        /// Description
        ///</summary>
        Reactive_Energy_Absolute_T4 = 0x00030100800804FF,

        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_KvarhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.94.92.4.255
        /// Description
        ///</summary>
        Reactive_Energy_Absolute_TL = 0x00030100800800FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_Kvah  Class ID 3 
        /// OBIS_CODE = 1.0.9.8.1.255
        /// Description
        ///</summary>
        Cumulative_Tariff1_Kvah = 0x00030100090801FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_TamperKwh  Class ID 3 
        /// OBIS_CODE = 1.0.98.225.1.255
        /// Description
        ///</summary>
        Cumulative_Tariff1_TamperKwh = 0x0003010062E101FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_MdiKw  Class ID 4 
        ///OBIS_CODE = 1.0.15.2.1.255
        ///Description
        ///</summary>
        Cumulative_MDI_Absolute_T1 = 0x000401000F0201FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_MdiKvar  Class ID 4
        /// OBIS_CODE = 1.0.3.2.1.255
        /// Description
        ///</summary>
        Cumulative_Tariff1_MdiKvar = 0x00040100030201FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_PowerFactor  Class ID 3 
        /// OBIS_CODE = 1.0.13.1.1.255
        /// Description
        ///</summary>
        Cumulative_Tariff1_PowerFactor = 0x000301000D0101FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_CurrentMonthMdiKw  Class ID 4 
        /// OBIS_CODE = 1.0.15.6.1.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Absolute_T1 = 0x000401000F0601FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff1_CurrentMonthMdiKvar  Class ID 4 
        /// OBIS_CODE = 1.0.3.6.1.255
        /// Description
        ///</summary>
        Monthly_Reactive_MDI_Absolute_T1 = 0x00040100030601FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_KwhImport  Class ID 3 
        /// OBIS_CODE = 1.0.1.8.2.255
        /// Description
        ///</summary>
        Active_Energy_Positive_T2 = 0x00030100010802FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_KwhExport  Class ID 3 
        /// OBIS_CODE = 1.0.2.8.2.255
        /// Description
        ///</summary>
        Active_Energy_Negative_T2 = 0x00030100020802FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_KwhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.15.8.2.255
        /// Description
        ///</summary>
        Active_Energy_Absolute_T2 = 0x000301000F0802FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_KvarhQ1  Class ID 3 
        /// OBIS_CODE = 1.0.5.8.2.255
        /// Description
        ///</summary>
        Cumulative_Tariff2_KvarhQ1 = 0x00030100050802FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_KvarhQ2  Class ID 3 
        /// OBIS_CODE = 1.0.6.8.2.255
        /// Description
        ///</summary>
        Cumulative_Tariff2_KvarhQ2 = 0x00030100060802FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_KvarhQ3  Class ID 3 
        /// OBIS_CODE = 1.0.7.8.2.255
        /// Description
        ///</summary>
        Cumulative_Tariff2_KvarhQ3 = 0x00030100070802FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_KvarhQ4  Class ID 3 
        /// OBIS_CODE = 1.0.8.8.2.255
        /// Description
        ///</summary>
        Cumulative_Tariff2_KvarhQ4 = 0x00030100080802FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_Kvah  Class ID 3 
        /// OBIS_CODE = 1.0.9.8.2.255
        /// Description
        ///</summary>
        Cumulative_Tariff2_Kvah = 0x00030100090802FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_TamperKwh  Class ID 3 
        /// OBIS_CODE = 1.0.98.225.2.255
        /// Description
        ///</summary>
        Cumulative_Tariff2_TamperKwh = 0x0003010062E102FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_MdiKw  Class ID 4 
        /// OBIS_CODE = 1.0.15.2.2.255
        /// Description
        ///</summary>
        Cumulative_MDI_Absolute_T2 = 0x000401000F0202FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_MdiKvar  Class ID 4 
        /// OBIS_CODE = 1.0.3.2.2.255
        /// Description
        ///</summary>
        Cumulative_Tariff2_MdiKvar = 0x00040100030202FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_PowerFactor  Class ID 3 
        /// OBIS_CODE = 1.0.13.1.2.255
        /// Description
        ///</summary>
        Cumulative_Tariff2_PowerFactor = 0x000301000D0102FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff2_CurrentMonthMdiKw  Class ID 4
        /// OBIS_CODE = 1.0.15.6.2.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Absolute_T2 = 0x000401000F0602FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff2_CurrentMonthMdiKvar  Class ID 4 
        /// OBIS_CODE = 1.0.3.6.2.255
        /// Description
        ///</summary>
        Monthly_Reactive_MDI_Absolute_T2 = 0x00040100030602FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_KwhImport  Class ID 3 
        /// OBIS_CODE = 1.0.1.8.3.255
        /// Description
        ///</summary>
        Active_Energy_Positive_T3 = 0x00030100010803FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_KwhExport  Class ID 3 
        /// OBIS_CODE = 1.0.2.8.3.255
        /// Description
        ///</summary>
        Active_Energy_Negative_T3 = 0x00030100020803FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_KwhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.15.8.3.255
        /// Description
        ///</summary>
        Active_Energy_Absolute_T3 = 0x000301000F0803FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_KvarhQ1  Class ID 3 
        /// OBIS_CODE = 1.0.5.8.3.255
        /// Description
        ///</summary>
        Cumulative_Tariff3_KvarhQ1 = 0x00030100050803FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff3_KvarhQ2  Class ID 3 
        /// OBIS_CODE = 1.0.6.8.3.255
        /// Description
        ///</summary>
        Cumulative_Tariff3_KvarhQ2 = 0x00030100060803FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_KvarhQ3  Class ID 3 
        /// OBIS_CODE = 1.0.7.8.3.255
        /// Description
        ///</summary>
        Cumulative_Tariff3_KvarhQ3 = 0x00030100070803FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_KvarhQ4  Class ID 3 
        /// OBIS_CODE = 1.0.8.8.3.255
        /// Description
        ///</summary>
        Cumulative_Tariff3_KvarhQ4 = 0x00030100080803FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_Kvah  Class ID 3 
        /// OBIS_CODE = 1.0.9.8.3.255
        /// Description
        ///</summary>
        Cumulative_Tariff3_Kvah = 0x00030100090803FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_TamperKwh  Class ID 3 
        /// OBIS_CODE = 1.0.98.225.3.255
        /// Description
        ///</summary>
        Cumulative_Tariff3_TamperKwh = 0x0003010062E103FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_MdiKw  Class ID 4 
        /// OBIS_CODE = 1.0.15.2.3.255
        /// Description
        ///</summary>
        Cumulative_MDI_Absolute_T3 = 0x000401000F0203FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_MdiKvar  Class ID 4 
        /// OBIS_CODE = 1.0.3.2.3.255
        /// Description
        ///</summary>
        Cumulative_Tariff3_MdiKvar = 0x00040100030203FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_PowerFactor  Class ID 3 
        /// OBIS_CODE = 1.0.13.1.3.255
        /// Description
        ///</summary>
        Cumulative_Tariff3_PowerFactor = 0x000301000D0103FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff3_CurrentMonthMdiKw  Class ID 4
        /// OBIS_CODE = 1.0.15.6.3.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Absolute_T3 = 0x000401000F0603FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff3_CurrentMonthMdiKvar  Class ID 4 
        /// OBIS_CODE = 1.0.3.6.3.255
        /// Description
        ///</summary>
        Monthly_Reactive_MDI_Absolute_T3 = 0x00040100030603FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff4_KwhImport  Class ID 3 
        /// OBIS_CODE = 1.0.1.8.4.255
        /// Description
        ///</summary>
        Active_Energy_Positive_T4 = 0x00030100010804FF,



        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff4_KwhExport  Class ID 3 
        /// OBIS_CODE = 1.0.2.8.4.255
        /// Description
        ///</summary>
        Active_Energy_Negative_T4 = 0x00030100020804FF,


        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff4_KwhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.15.8.4.255
        /// Description
        ///</summary>
        Active_Energy_Absolute_T4 = 0x000301000F0804FF,


        ///<summary>
        /// Description: Quantity_Name= Cumulative_Tariff4_KvarhQ1  Class ID 3
        /// OBIS_CODE = 1.0.5.8.4.255
        /// Description
        ///</summary>
        Cumulative_Tariff4_KvarhQ1 = 0x00030100050804FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_KvarhQ2  Class ID 3 
        /// OBIS_CODE = 1.0.6.8.4.255
        /// Description
        ///</summary>
        Cumulative_Tariff4_KvarhQ2 = 0x00030100060804FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_KvarhQ3  Class ID 3 
        /// OBIS_CODE = 1.0.7.8.4.255
        /// Description
        ///</summary>
        Cumulative_Tariff4_KvarhQ3 = 0x00030100070804FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_KvarhQ4  Class ID 3
        /// OBIS_CODE = 1.0.8.8.4.255
        /// Description
        ///</summary>
        Cumulative_Tariff4_KvarhQ4 = 0x00030100080804FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_Kvah  Class ID 3
        /// OBIS_CODE = 1.0.9.8.4.255
        /// Description
        ///</summary>
        Cumulative_Tariff4_Kvah = 0x00030100090804FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_TamperKwh  Class ID 3 
        /// OBIS_CODE = 1.0.98.225.4.255
        /// Description
        ///</summary>
        Cumulative_Tariff4_TamperKwh = 0x0003010062E104FF,

        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_MdiKw  Class ID 4 
        /// OBIS_CODE = 1.0.15.2.0.255
        /// Description
        ///</summary>
        Cumulative_Tariff4_MdiKw = 0x000401000F0204FF,



        ///<summary>
        ///Description: Quantity_Name = Cumulative_TariffTL_MdiKw  Class ID 4 
        /// OBIS_CODE = 1.0.15.2.0.255
        /// Description
        ///</summary>
        Cumulative_TariffTL_MdiKw = 0x000401000F0200FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_MdiKvar  Class ID 4 
        /// OBIS_CODE = 1.0.3.2.4.255
        /// Description
        ///</summary>
        Cumulative_Tariff4_MdiKvar = 0x00040100030204FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_PowerFactor  Class ID 3 
        /// OBIS_CODE = 1.0.13.1.4.255
        /// Description
        ///</summary>
        Cumulative_Tariff4_PowerFactor = 0x000301000D0104FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_CurrentMonthMdiKw  Class ID 4
        /// OBIS_CODE = 1.0.15.6.4.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Absolute_T4 = 0x000401000F0604FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_Tariff4_CurrentMonthMdiKvar  Class ID 4 
        /// OBIS_CODE = 1.0.3.6.4.255
        /// Description
        ///</summary>
        Monthly_Reactive_MDI_Absolute_T4 = 0x00040100030604FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_KwhImport  Class ID 3 
        /// OBIS_CODE = 1.0.1.8.0.255
        /// Description
        ///</summary>
        Active_Energy_Positive_TL = 0x00030100010800FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_KwhExport  Class ID 3 
        /// OBIS_CODE = 1.0.2.8.0.255
        /// Description
        ///</summary>
        Active_Energy_Negative_TL = 0x00030100020800FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_KwhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.15.8.0.255
        /// Description
        ///</summary>
        Active_Energy_Absolute_TL = 0x000301000F0800FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_KvarhQ1Q3  Class ID 3 
        /// OBIS_CODE = 1.0.94.92.0.255
        /// Description
        ///</summary>
        Cumulative_TariffTL_KvarhQ1Q3 = 0x00030100050800FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffT1_KvarhQ1Q3  Class ID 3 
        /// OBIS_CODE = 1.0.6.8.0.255
        /// Description
        ///</summary>
        Cumulative_TariffT1_KvarhQ1Q3 = 0x000301005E5C01FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffT2_KvarhQ1Q3  Class ID 3 
        /// OBIS_CODE = 1.0.7.8.0.255
        /// Description
        ///</summary>
        Cumulative_TariffT2_KvarhQ1Q3 = 0x000301005E5C02FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffT3_KvarhQ1Q3  Class ID 3 
        /// OBIS_CODE = 1.0.8.8.0.255
        /// Description
        ///</summary>
        Cumulative_TariffT3_KvarhQ1Q3 = 0x000301005E5C03FF,

        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_KvarhQ1Q3  Class ID 3 
        /// OBIS_CODE = 1.0.94.92.0.255
        /// Description
        ///</summary>
        Reactive_Energy_TL_Q1Q3 = 0x000301005E5C00FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffT1_KvarhQ1Q3  Class ID 3 
        /// OBIS_CODE = 1.0.6.8.0.255
        /// Description
        ///</summary>
        Reactive_Energy_T1_Q1Q3 = 0x000301005E5C01FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffT2_Q1Q3  Class ID 3 
        /// OBIS_CODE = 1.0.7.8.0.255
        /// Description
        ///</summary>
        Reactive_Energy_T2_Q1Q3 = 0x000301005E5C02FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffT3_Q1Q3  Class ID 3 
        /// OBIS_CODE = 1.0.8.8.0.255
        /// Description
        ///</summary>
        Reactive_Energy_T3_Q1Q3 = 0x000301005E5C03FF,

        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffT4_Q1Q3  Class ID 3 
        /// OBIS_CODE = 1.0.8.8.0.255
        /// Description
        ///</summary>
        Reactive_Energy_T4_Q1Q3 = 0x000301005E5C04FF,

        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_KvarhQ1  Class ID 3 
        /// OBIS_CODE = 1.0.5.8.0.255
        /// Description
        ///</summary>
        Cumulative_TariffTL_KvarhQ1 = 0x00030100050800FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_KvarhQ2  Class ID 3 
        /// OBIS_CODE = 1.0.6.8.0.255
        /// Description
        ///</summary>
        Cumulative_TariffTL_KvarhQ2 = 0x00030100060800FF,



        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_KvarhQ3  Class ID 3 
        /// OBIS_CODE = 1.0.7.8.0.255
        /// Description
        ///</summary>
        Cumulative_TariffTL_KvarhQ3 = 0x00030100070800FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_KvarhQ4  Class ID 3 
        /// OBIS_CODE = 1.0.8.8.0.255
        /// Description
        ///</summary>
        Cumulative_TariffTL_KvarhQ4 = 0x00030100080800FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_Kvah  Class ID 3 
        /// OBIS_CODE = 1.0.9.8.0.255
        /// Description
        ///</summary>
        Cumulative_TariffTL_Kvah = 0x00030100090800FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_TamperKwh  Class ID 3 
        /// OBIS_CODE = 1.0.98.225.0.255
        /// Description
        ///</summary>
        Cumulative_TariffTL_TamperKwh = 0x0003010062E100FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_MdiKvar  Class ID 4 
        /// OBIS_CODE = 1.0.3.2.0.255
        /// Description
        ///</summary>
        Cumulative_TariffTL_MdiKvar = 0x00040100030200FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_PowerFactor  Class ID 3 
        /// OBIS_CODE = 1.0.13.1.0.255
        /// Description
        ///</summary>
        Cumulative_TariffTL_PowerFactor = 0x000301000D0100FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_CurrentMonthMdiKw  Class ID 4 
        /// OBIS_CODE = 1.0.15.6.0.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Absolute_TL = 0x000401000F0600FF,


        ///<summary>
        ///Description: Quantity_Name= Cumulative_TariffTL_CurrentMonthMdiKvar  Class ID 4 
        /// OBIS_CODE = 1.0.3.6.0.255
        /// Description
        ///</summary>
        Monthly_Reactive_MDI_Absolute_TL = 0x00040100030600FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff1_KwhImport  Class ID 3 
        /// OBIS_CODE = 1.0.1.9.1.255
        /// Description
        ///</summary>
        Monthly_Tariff1_KwhImport = 0x00030100010901FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff1_KwhExport  Class ID 3 
        /// OBIS_CODE = 1.0.2.9.1.255
        /// Description
        ///</summary>
        Monthly_Tariff1_KwhExport = 0x00030100020901FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff1_KwhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.15.9.1.255
        /// Description
        ///</summary>
        Monthly_Tariff1_KwhAbsolute = 0x000301000F0901FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff1_KvarhQ1  Class ID 3 
        /// OBIS_CODE = 1.0.5.9.1.255
        /// Description
        ///</summary>
        Monthly_Tariff1_KvarhQ1 = 0x00030100050901FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff1_KvarhQ2  Class ID 3 
        /// OBIS_CODE = 1.0.6.9.1.255
        /// Description
        ///</summary>
        Monthly_Tariff1_KvarhQ2 = 0x00030100060901FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff1_KvarhQ3  Class ID 3 
        /// OBIS_CODE = 1.0.7.9.1.255
        /// Description
        ///</summary>
        Monthly_Tariff1_KvarhQ3 = 0x00030100070901FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff1_KvarhQ4  Class ID 3 
        /// OBIS_CODE = 1.0.8.9.1.255
        /// Description
        ///</summary>
        Monthly_Tariff1_KvarhQ4 = 0x00030100080901FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff1_KvarhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.98.126.1.255
        /// Description
        ///</summary>
        Monthly_Tariff1_KvarhAbsolute = 0x00030100627E01FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff1_Kvah  Class ID 3 
        /// OBIS_CODE = 1.0.9.9.1.255
        /// Description
        ///</summary>
        Monthly_Tariff1_Kvah = 0x00030100090901FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff1_TamperKwh  Class ID 3 
        /// OBIS_CODE = 1.0.98.127.1.255
        /// Description
        ///</summary>
        Monthly_Tariff1_TamperKwh = 0x00030100627F01FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff2_KwhImport  Class ID 3 
        /// OBIS_CODE = 1.0.1.9.2.255
        /// Description
        ///</summary>
        Monthly_Tariff2_KwhImport = 0x00030100010902FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff2_KwhExport  Class ID 3 
        /// OBIS_CODE = 1.0.2.9.2.255
        /// Description
        ///</summary>
        Monthly_Tariff2_KwhExport = 0x00030100020902FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff2_KwhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.15.9.2.255
        /// Description
        ///</summary>
        Monthly_Tariff2_KwhAbsolute = 0x000301000F0902FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff2_KvarhQ1  Class ID 3 
        /// OBIS_CODE = 1.0.5.9.2.255
        /// Description
        ///</summary>
        Monthly_Tariff2_KvarhQ1 = 0x00030100050902FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff2_KvarhQ2  Class ID 3 
        /// OBIS_CODE = 1.0.6.9.2.255
        /// Description
        ///</summary>
        Monthly_Tariff2_KvarhQ2 = 0x00030100060902FF,


        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff2_KvarhQ3  Class ID 3 
        /// OBIS_CODE = 1.0.7.9.2.255
        /// Description
        ///</summary>
        Monthly_Tariff2_KvarhQ3 = 0x00030100070902FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff2_KvarhQ4  Class ID 3 
        /// OBIS_CODE = 1.0.8.9.2.255
        /// Description
        ///</summary>
        Monthly_Tariff2_KvarhQ4 = 0x00030100080902FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff2_KvarhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.98.126.2.255
        /// Description
        ///</summary>
        Monthly_Tariff2_KvarhAbsolute = 0x00030100627E02FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff2_Kvah  Class ID 3 
        /// OBIS_CODE = 1.0.9.9.2.255
        /// Description
        ///</summary>
        Monthly_Tariff2_Kvah = 0x00030100090902FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff2_TamperKwh  Class ID 3 
        /// OBIS_CODE = 1.0.98.127.2.255
        /// Description
        ///</summary>
        Monthly_Tariff2_TamperKwh = 0x00030100627F02FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff3_KwhImport  Class ID 3 
        /// OBIS_CODE = 1.0.1.9.3.255
        /// Description
        ///</summary>
        Monthly_Tariff3_KwhImport = 0x00030100010903FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff3_KwhExport  Class ID 3 
        /// OBIS_CODE = 1.0.2.9.3.255
        /// Description
        ///</summary>
        Monthly_Tariff3_KwhExport = 0x00030100020903FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff3_KwhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.15.9.3.255
        /// Description
        ///</summary>
        Monthly_Tariff3_KwhAbsolute = 0x000301000F0903FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff3_KvarhQ1  Class ID 3 
        /// OBIS_CODE = 1.0.5.9.3.255
        /// Description
        ///</summary>
        Monthly_Tariff3_KvarhQ1 = 0x00030100050903FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff3_KvarhQ2  Class ID 3 
        /// OBIS_CODE = 1.0.6.9.3.255
        /// Description
        ///</summary>
        Monthly_Tariff3_KvarhQ2 = 0x00030100060903FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff3_KvarhQ3  Class ID 3 
        /// OBIS_CODE = 1.0.7.9.3.255
        /// Description
        ///</summary>
        Monthly_Tariff3_KvarhQ3 = 0x00030100070903FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff3_KvarhQ4  Class ID 3 
        /// OBIS_CODE = 1.0.8.9.3.255
        /// Description
        ///</summary>
        Monthly_Tariff3_KvarhQ4 = 0x00030100080903FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff3_KvarhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.98.126.3.255
        /// Description
        ///</summary>
        Monthly_Tariff3_KvarhAbsolute = 0x00030100627E03FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff3_Kvah  Class ID 3 
        /// OBIS_CODE = 1.0.9.9.3.255
        /// Description
        ///</summary>
        Monthly_Tariff3_Kvah = 0x00030100090903FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff3_TamperKwh  Class ID 3 
        /// OBIS_CODE = 1.0.98.127.3.255
        /// Description
        ///</summary>
        Monthly_Tariff3_TamperKwh = 0x00030100627F03FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff4_KwhImport  Class ID 3 
        /// OBIS_CODE = 1.0.1.9.4.255
        /// Description
        ///</summary>
        Monthly_Tariff4_KwhImport = 0x00030100010904FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff4_KwhExport  Class ID 3 
        /// OBIS_CODE = 1.0.2.9.4.255
        /// Description
        ///</summary>
        Monthly_Tariff4_KwhExport = 0x00030100020904FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff4_KwhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.15.9.4.255
        /// Description
        ///</summary>
        Monthly_Tariff4_KwhAbsolute = 0x000301000F0904FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff4_KvarhQ1  Class ID 3 
        /// OBIS_CODE = 1.0.5.9.4.255
        /// Description
        ///</summary>
        Monthly_Tariff4_KvarhQ1 = 0x00030100050904FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff4_KvarhQ2  Class ID 3 
        /// OBIS_CODE = 1.0.6.9.4.255
        /// Description
        ///</summary>
        Monthly_Tariff4_KvarhQ2 = 0x00030100060904FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff4_KvarhQ3  Class ID 3 
        /// OBIS_CODE = 1.0.7.9.4.255
        /// Description
        ///</summary>
        Monthly_Tariff4_KvarhQ3 = 0x00030100070904FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff4_KvarhQ4  Class ID 3 
        /// OBIS_CODE = 1.0.8.9.4.255
        /// Description
        ///</summary>
        Monthly_Tariff4_KvarhQ4 = 0x00030100080904FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff4_KvarhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.98.126.4.255
        /// Description
        ///</summary>
        Monthly_Tariff4_KvarhAbsolute = 0x00030100627E04FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff4_Kvah  Class ID 3 
        /// OBIS_CODE = 1.0.9.9.4.255
        /// Description
        ///</summary>
        Monthly_Tariff4_Kvah = 0x00030100090904FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_Tariff4_TamperKwh  Class ID 3 
        /// OBIS_CODE = 1.0.98.127.4.255
        /// Description
        ///</summary>
        Monthly_Tariff4_TamperKwh = 0x00030100627F04FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_TariffTL_KwhImport  Class ID 3 
        /// OBIS_CODE = 1.0.1.9.0.255
        /// Description
        ///</summary>
        Monthly_TariffTL_KwhImport = 0x00030100010900FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_TariffTL_KwhExport  Class ID 3 
        /// OBIS_CODE = 1.0.2.9.0.255
        /// Description
        ///</summary>
        Monthly_TariffTL_KwhExport = 0x00030100020900FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_TariffTL_KwhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.15.9.0.255
        /// Description
        ///</summary>
        Monthly_TariffTL_KwhAbsolute = 0x000301000F0900FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_TariffTL_KvarhQ1  Class ID 3 
        /// OBIS_CODE = 1.0.5.9.0.255
        /// Description
        ///</summary>
        Monthly_TariffTL_KvarhQ1 = 0x00030100050900FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_TariffTL_KvarhQ2  Class ID 3 
        /// OBIS_CODE = 1.0.6.9.0.255
        /// Description
        ///</summary>
        Monthly_TariffTL_KvarhQ2 = 0x00030100060900FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_TariffTL_KvarhQ3  Class ID 3 
        /// OBIS_CODE = 1.0.7.9.0.255
        /// Description
        ///</summary>
        Monthly_TariffTL_KvarhQ3 = 0x00030100070900FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_TariffTL_KvarhQ4  Class ID 3 
        /// OBIS_CODE = 1.0.8.9.0.255
        /// Description
        ///</summary>
        Monthly_TariffTL_KvarhQ4 = 0x00030100080900FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_TariffTL_KvarhAbsolute  Class ID 3 
        /// OBIS_CODE = 1.0.98.126.0.255
        /// Description
        ///</summary>
        Monthly_TariffTL_KvarhAbsolute = 0x00030100627E00FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_TariffTL_Kvah  Class ID 3 
        /// OBIS_CODE = 1.0.9.9.0.255
        /// Description
        ///</summary>
        Monthly_TariffTL_Kvah = 0x00030100090900FF,



        ///<summary>
        ///Description: Quantity_Name= Monthly_TariffTL_TamperKwh  Class ID 3 
        /// OBIS_CODE = 1.0.98.127.0.255
        /// Description
        ///</summary>
        Monthly_TariffTL_TamperKwh = 0x00030100627F00FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_ABSOLUTE_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.50.0.255
        /// Description
        ///</summary>
        PB_KWH_ABSOLUTE_TL = 0x00030100603200FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_ABSOLUTE_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.50.1.255
        /// Description
        ///</summary>
        PB_KWH_ABSOLUTE_T1 = 0x00030100603201FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_ABSOLUTE_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.50.2.255
        /// Description
        ///</summary>
        PB_KWH_ABSOLUTE_T2 = 0x00030100603202FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_ABSOLUTE_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.50.3.255
        /// Description
        ///</summary>
        PB_KWH_ABSOLUTE_T3 = 0x00030100603203FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_ABSOLUTE_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.50.4.255
        /// Description
        ///</summary>
        PB_KWH_ABSOLUTE_T4 = 0x00030100603204FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_IMPORT_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.51.0.255
        /// Description
        ///</summary>
        PB_KWH_IMPORT_TL = 0x00030100603300FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_IMPORT_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.51.1.255
        /// Description
        ///</summary>
        PB_KWH_IMPORT_T1 = 0x00030100603301FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_IMPORT_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.51.2.255
        /// Description
        ///</summary>
        PB_KWH_IMPORT_T2 = 0x00030100603302FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_IMPORT_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.51.3.255
        /// Description
        ///</summary>
        PB_KWH_IMPORT_T3 = 0x00030100603303FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_IMPORT_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.51.4.255
        /// Description
        ///</summary>
        PB_KWH_IMPORT_T4 = 0x00030100603304FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_EXPORT_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.52.0.255
        /// Description
        ///</summary>
        PB_KWH_EXPORT_TL = 0x00030100603400FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_EXPORT_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.52.1.255
        /// Description
        ///</summary>
        PB_KWH_EXPORT_T1 = 0x00030100603401FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_EXPORT_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.52.2.255
        /// Description
        ///</summary>
        PB_KWH_EXPORT_T2 = 0x00030100603402FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_EXPORT_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.52.3.255
        /// Description
        ///</summary>
        PB_KWH_EXPORT_T3 = 0x00030100603403FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KWH_EXPORT_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.52.4.255
        /// Description
        ///</summary>
        PB_KWH_EXPORT_T4 = 0x00030100603404FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_ABSOLUTE_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.54.0.255
        /// Description
        ///</summary>
        PB_KVARH_ABSOLUTE_TL = 0x00030100603600FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_ABSOLUTE_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.54.1.255
        /// Description
        ///</summary>
        PB_KVARH_ABSOLUTE_T1 = 0x00030100603601FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_ABSOLUTE_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.54.2.255
        /// Description
        ///</summary>
        PB_KVARH_ABSOLUTE_T2 = 0x00030100603602FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_ABSOLUTE_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.54.3.255
        /// Description
        ///</summary>
        PB_KVARH_ABSOLUTE_T3 = 0x00030100603603FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_ABSOLUTE_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.54.4.255
        /// Description
        ///</summary>
        PB_KVARH_ABSOLUTE_T4 = 0x00030100603604FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q1_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.56.0.255
        /// Description
        ///</summary>
        PB_KVARH_Q1_TL = 0x00030100603800FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q1_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.56.1.255
        /// Description
        ///</summary>
        PB_KVARH_Q1_T1 = 0x00030100603801FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q1_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.56.2.255
        /// Description
        ///</summary>
        PB_KVARH_Q1_T2 = 0x00030100603802FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q1_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.56.3.255
        /// Description
        ///</summary>
        PB_KVARH_Q1_T3 = 0x00030100603803FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q1_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.56.4.255
        /// Description
        ///</summary>
        PB_KVARH_Q1_T4 = 0x00030100603804FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q2_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.57.0.255
        /// Description
        ///</summary>
        PB_KVARH_Q2_TL = 0x00030100603900FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q2_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.57.1.255
        /// Description
        ///</summary>
        PB_KVARH_Q2_T1 = 0x00030100603901FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q2_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.57.2.255
        /// Description
        ///</summary>
        PB_KVARH_Q2_T2 = 0x00030100603902FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q2_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.57.3.255
        /// Description
        ///</summary>
        PB_KVARH_Q2_T3 = 0x00030100603903FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q2_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.57.4.255
        /// Description
        ///</summary>
        PB_KVARH_Q2_T4 = 0x00030100603904FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q3_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.58.0.255
        /// Description
        ///</summary>
        PB_KVARH_Q3_TL = 0x00030100603A00FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q3_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.58.1.255
        /// Description
        ///</summary>
        PB_KVARH_Q3_T1 = 0x00030100603A01FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q3_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.58.2.255
        /// Description
        ///</summary>
        PB_KVARH_Q3_T2 = 0x00030100603A02FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q3_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.58.3.255
        /// Description
        ///</summary>
        PB_KVARH_Q3_T3 = 0x00030100603A03FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q3_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.58.4.255
        /// Description
        ///</summary>
        PB_KVARH_Q3_T4 = 0x00030100603A04FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q4_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.59.0.255
        /// Description
        ///</summary>
        PB_KVARH_Q4_TL = 0x00030100603B00FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q4_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.59.1.255
        /// Description
        ///</summary>
        PB_KVARH_Q4_T1 = 0x00030100603B01FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q4_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.59.2.255
        /// Description
        ///</summary>
        PB_KVARH_Q4_T2 = 0x00030100603B02FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q4_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.59.3.255
        /// Description
        ///</summary>
        PB_KVARH_Q4_T3 = 0x00030100603B03FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVARH_Q4_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.59.4.255
        /// Description
        ///</summary>
        PB_KVARH_Q4_T4 = 0x00030100603B04FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVAH_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.60.0.255
        /// Description
        ///</summary>
        PB_KVAH_TL = 0x00030100603C00FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVAH_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.60.1.255
        /// Description
        ///</summary>
        PB_KVAH_T1 = 0x00030100603C01FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVAH_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.60.2.255
        /// Description
        ///</summary>
        PB_KVAH_T2 = 0x00030100603C02FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVAH_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.60.3.255
        /// Description
        ///</summary>
        PB_KVAH_T3 = 0x00030100603C03FF,



        ///<summary>
        ///Description: Quantity_Name= PB_KVAH_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.60.4.255
        /// Description
        ///</summary>
        PB_KVAH_T4 = 0x00030100603C04FF,



        ///<summary>
        ///Description: Quantity_Name= PB_POWER_FACTOR_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.63.0.255
        /// Description
        ///</summary>
        PB_POWER_FACTOR_TL = 0x00030100603F00FF,



        ///<summary>
        ///Description: Quantity_Name= PB_POWER_FACTOR_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.63.1.255
        /// Description
        ///</summary>
        PB_POWER_FACTOR_T1 = 0x00030100603F01FF,



        ///<summary>
        ///Description: Quantity_Name= PB_POWER_FACTOR_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.63.2.255
        /// Description
        ///</summary>
        PB_POWER_FACTOR_T2 = 0x00030100603F02FF,



        ///<summary>
        ///Description: Quantity_Name= PB_POWER_FACTOR_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.63.3.255
        /// Description
        ///</summary>
        PB_POWER_FACTOR_T3 = 0x00030100603F03FF,



        ///<summary>
        ///Description: Quantity_Name= PB_POWER_FACTOR_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.63.4.255
        /// Description
        ///</summary>
        PB_POWER_FACTOR_T4 = 0x00030100603F04FF,



        ///<summary>
        ///Description: Quantity_Name= PB_TAMPER_KWH_TL  Class ID 3 
        /// OBIS_CODE = 1.0.96.64.0.255
        /// Description
        ///</summary>
        PB_TAMPER_KWH_TL = 0x00030100604000FF,



        ///<summary>
        ///Description: Quantity_Name= PB_TAMPER_KWH_T1  Class ID 3 
        /// OBIS_CODE = 1.0.96.64.1.255
        /// Description
        ///</summary>
        PB_TAMPER_KWH_T1 = 0x00030100604001FF,



        ///<summary>
        ///Description: Quantity_Name= PB_TAMPER_KWH_T2  Class ID 3 
        /// OBIS_CODE = 1.0.96.64.2.255
        /// Description
        ///</summary>
        PB_TAMPER_KWH_T2 = 0x00030100604002FF,



        ///<summary>
        ///Description: Quantity_Name= PB_TAMPER_KWH_T3  Class ID 3 
        /// OBIS_CODE = 1.0.96.64.3.255
        /// Description
        ///</summary>
        PB_TAMPER_KWH_T3 = 0x00030100604003FF,



        ///<summary>
        ///Description: Quantity_Name= PB_TAMPER_KWH_T4  Class ID 3 
        /// OBIS_CODE = 1.0.96.64.4.255
        /// Description
        ///</summary>
        PB_TAMPER_KWH_T4 = 0x00030100604004FF,



        ///<summary>
        ///Description: Quantity_Name= PB_MDI_KW_TL  Class ID 4 
        /// OBIS_CODE = 1.0.96.65.0.255
        /// Description
        ///</summary>
        PB_MDI_KW_TL = 0x00040100604100FF,



        ///<summary>
        ///Description: Quantity_Name= PB_MDI_KW_T1  Class ID 4 
        /// OBIS_CODE = 1.0.96.65.1.255
        /// Description
        ///</summary>
        PB_MDI_KW_T1 = 0x00040100604101FF,



        ///<summary>
        ///Description: Quantity_Name= PB_MDI_KW_T2  Class ID 4 
        /// OBIS_CODE = 1.0.96.65.2.255
        /// Description
        ///</summary>
        PB_MDI_KW_T2 = 0x00040100604102FF,



        ///<summary>
        ///Description: Quantity_Name= PB_MDI_KW_T3  Class ID 4
        /// OBIS_CODE = 1.0.96.65.3.255
        /// Description
        ///</summary>
        PB_MDI_KW_T3 = 0x00040100604103FF,



        ///<summary>
        ///Description: Quantity_Name= PB_MDI_KW_T4  Class ID 4
        /// OBIS_CODE = 1.0.96.65.4.255
        /// Description
        ///</summary>
        PB_MDI_KW_T4 = 0x00040100604104FF,



        ///<summary>
        ///Description: Quantity_Name= PB_MDI_KVAR_TL  Class ID 4 
        /// OBIS_CODE = 1.0.96.66.0.255
        /// Description
        ///</summary>
        PB_MDI_KVAR_TL = 0x00040100604200FF,



        ///<summary>
        ///Description: Quantity_Name= PB_MDI_KVAR_T1  Class ID 4 
        /// OBIS_CODE = 1.0.96.66.1.255
        /// Description
        ///</summary>
        PB_MDI_KVAR_T1 = 0x00040100604201FF,



        ///<summary>
        ///Description: Quantity_Name= PB_MDI_KVAR_T2  Class ID 4 
        /// OBIS_CODE = 1.0.96.66.2.255
        /// Description
        ///</summary>
        PB_MDI_KVAR_T2 = 0x00040100604202FF,



        ///<summary>
        ///Description: Quantity_Name= PB_MDI_KVAR_T3  Class ID 4 
        /// OBIS_CODE = 1.0.96.66.3.255
        /// Description
        ///</summary>
        PB_MDI_KVAR_T3 = 0x00040100604203FF,



        ///<summary>
        ///Description: Quantity_Name= PB_MDI_KVAR_T4  Class ID 4 
        /// OBIS_CODE = 1.0.96.66.4.255
        /// Description
        ///</summary>
        PB_MDI_KVAR_T4 = 0x00040100604204FF,



        ///<summary>
        ///Description: Quantity_Name= PB_PREVIOUS_MONTH_MDI_KVAR_TL  Class ID 4 
        /// OBIS_CODE = 1.0.96.72.0.255
        /// Description
        ///</summary>
        PB_PREVIOUS_MONTH_MDI_KVAR_TL = 0x00040100604800FF,



        ///<summary>
        ///Description: Quantity_Name= PB_PREVIOUS_MONTH_MDI_KVAR_T1  Class ID 4 
        /// OBIS_CODE = 1.0.96.72.1.255
        /// Description
        ///</summary>
        PB_PREVIOUS_MONTH_MDI_KVAR_T1 = 0x00040100604801FF,



        ///<summary>
        ///Description: Quantity_Name= PB_PREVIOUS_MONTH_MDI_KVAR_T2  Class ID 4 
        /// OBIS_CODE = 1.0.96.72.2.255
        /// Description
        ///</summary>
        PB_PREVIOUS_MONTH_MDI_KVAR_T2 = 0x00040100604802FF,



        ///<summary>
        ///Description: Quantity_Name= PB_PREVIOUS_MONTH_MDI_KVAR_T3  Class ID 4 
        /// OBIS_CODE = 1.0.96.72.3.255
        /// Description
        ///</summary>
        PB_PREVIOUS_MONTH_MDI_KVAR_T3 = 0x00040100604803FF,



        ///<summary>
        ///Description: Quantity_Name= PB_PREVIOUS_MONTH_MDI_KVAR_T4  Class ID 4 
        /// OBIS_CODE = 1.0.96.72.4.255
        /// Description
        ///</summary>
        PB_PREVIOUS_MONTH_MDI_KVAR_T4 = 0x00040100604804FF,



        ///<summary>
        ///Description: Quantity_Name= PB_PREVIOUS_MONTH_MDI_KW_TL  Class ID 4 
        /// OBIS_CODE = 1.0.96.71.0.255
        /// Description
        ///</summary>                      
        PB_PREVIOUS_MONTH_MDI_KW_TL = 0x00040100604700FF,



        ///<summary>
        ///Description: Quantity_Name= PB_PREVIOUS_MONTH_MDI_KW_T1  Class ID 4 
        /// OBIS_CODE = 1.0.96.71.1.255
        /// Description
        ///</summary>
        PB_PREVIOUS_MONTH_MDI_KW_T1 = 0x00040100604701FF,



        ///<summary>
        ///Description: Quantity_Name= PB_PREVIOUS_MONTH_MDI_KW_T2  Class ID 4 
        /// OBIS_CODE = 1.0.96.71.2.255
        /// Description
        ///</summary>
        PB_PREVIOUS_MONTH_MDI_KW_T2 = 0x00040100604702FF,



        ///<summary>
        ///Description: Quantity_Name= PB_PREVIOUS_MONTH_MDI_KW_T3  Class ID 4 
        /// OBIS_CODE = 1.0.96.71.3.255
        /// Description
        ///</summary>
        PB_PREVIOUS_MONTH_MDI_KW_T3 = 0x00040100604703FF,



        ///<summary>
        ///Description: Quantity_Name= PB_PREVIOUS_MONTH_MDI_KW_T4  Class ID 4 
        /// OBIS_CODE = 1.0.96.71.4.255
        /// Description
        ///</summary>
        PB_PREVIOUS_MONTH_MDI_KW_T4 = 0x00040100604704FF,

        ///<summary>
        /// Description: Quantity_Name= AutoConnect  Class ID 29 
        /// OBIS_CODE = 0.0.2.1.0.255
        /// Description
        ///</summary>
        AutoConnect = 0x001D0000020100FF,

        ///<summary>
        /// Description: Quantity_Name= AutoAnswer  Class ID 28 
        /// OBIS_CODE = 0.0.2.2.0.255
        /// Description
        ///</summary>
        AutoAnswer = 0x001C0000020200FF,

        ///<summary>
        ///Description: Quantity_Name= _IP_Profile  Class ID 1 
        /// OBIS_CODE = 0.0.25.160.0.255
        /// Description
        ///</summary>
        _IP_Profile = 0x0001000019A000FF,



        ///<summary>
        ///Description: Quantity_Name= _Number_Profile  Class ID 1 
        /// OBIS_CODE = 0.0.96.133.0.255
        /// Description
        ///</summary>
        _Number_Profile = 0x00010000608500FF,



        ///<summary>
        ///Description: Quantity_Name= _WakeUp_Profile  Class ID 1 
        /// OBIS_CODE = 0.0.96.145.0.255
        /// Description
        ///</summary>
        _WakeUp_Profile = 0x00010000609100FF,



        ///<summary>
        ///Description: Quantity_Name= _Communication_Profile  Class ID 1 
        /// OBIS_CODE = 0.0.96.151.0.255
        /// Description
        ///</summary>
        _Communication_Profile = 0x00010000609700FF,



        ///<summary>
        ///Description: Quantity_Name= DisplayWindows_NOR  Class ID 1 
        /// OBIS_CODE = 0.0.21.129.1.255
        /// Description
        ///</summary>
        DisplayWindows_NOR = 0x00010000158101FF,



        ///<summary>
        ///Description: Quantity_Name= DisplayWIndows_ALT  Class ID 1 
        /// OBIS_CODE = 0.0.21.129.2.255
        /// Description
        ///</summary>
        DisplayWIndows_ALT = 0x00010000158102FF,



        ///<summary>
        ///Description: Quantity_Name= ContactorFlags_70  Class ID 70 
        /// OBIS_CODE = 0.0.96.3.10.255
        /// Description
        ///</summary>
        ContactorFlags_70 = 0x0046000060030AFF,


        ///<summary>
        ///Description: Quantity_Name= CumulativeBilling  Class ID 7 
        /// OBIS_CODE = 0.0.98.133.0.255
        /// Description
        ///</summary>
        CumulativeBilling = 0x00070000628500FF,



        ///<summary>
        ///Description: Quantity_Name= CurrentPowerQualityDataProfile  Class ID 7 
        /// OBIS_CODE = 0.0.98.134.0.255
        /// Description
        ///</summary>
        CurrentPowerQualityDataProfile = 0x00070000628600FF,


        ///<summary>
        /// Description: Quantity_Name= _Event_Log_All  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.0.255
        /// Description
        ///</summary>
        _Event_Log_All = 0x00070000636200FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_01  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.1.255
        /// Description
        ///</summary>
        _Event_Log_01 = 0x00070000636201FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_02  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.2.255
        /// Description
        ///</summary>
        _Event_Log_02 = 0x00070000636202FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_03  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.3.255
        /// Description
        ///</summary>
        _Event_Log_03 = 0x00070000636203FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_04  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.4.255
        /// Description
        ///</summary>
        _Event_Log_04 = 0x00070000636204FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_05  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.5.255
        /// Description
        ///</summary>
        _Event_Log_05 = 0x00070000636205FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_06  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.6.255
        /// Description
        ///</summary>
        _Event_Log_06 = 0x00070000636206FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_07  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.7.255
        /// Description
        ///</summary>
        _Event_Log_07 = 0x00070000636207FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_08  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.8.255
        /// Description
        ///</summary>
        _Event_Log_08 = 0x00070000636208FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_09  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.9.255
        /// Description
        ///</summary>
        _Event_Log_09 = 0x00070000636209FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_10  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.10.255
        /// Description
        ///</summary>
        _Event_Log_10 = 0x0007000063620AFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_11  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.11.255
        /// Description
        ///</summary>
        _Event_Log_11 = 0x0007000063620BFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_12  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.12.255
        /// Description
        ///</summary>
        _Event_Log_12 = 0x0007000063620CFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_13  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.13.255
        /// Description
        ///</summary>
        _Event_Log_13 = 0x0007000063620DFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_14  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.14.255
        /// Description
        ///</summary>
        _Event_Log_14 = 0x0007000063620EFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_15  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.15.255
        /// Description
        ///</summary>
        _Event_Log_15 = 0x0007000063620FFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_16  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.16.255
        /// Description
        ///</summary>
        _Event_Log_16 = 0x00070000636210FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_17  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.17.255
        /// Description
        ///</summary>
        _Event_Log_17 = 0x00070000636211FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_18  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.18.255
        /// Description
        ///</summary>
        _Event_Log_18 = 0x00070000636212FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_19  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.19.255
        /// Description
        ///</summary>
        _Event_Log_19 = 0x00070000636213FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_20  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.20.255
        /// Description
        ///</summary>
        _Event_Log_20 = 0x00070000636214FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_21  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.21.255
        /// Description
        ///</summary>
        _Event_Log_21 = 0x00070000636215FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_22  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.22.255
        /// Description
        ///</summary>
        _Event_Log_22 = 0x00070000636216FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_23  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.23.255
        /// Description
        ///</summary>
        _Event_Log_23 = 0x00070000636217FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_24  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.24.255
        /// Description
        ///</summary>
        _Event_Log_24 = 0x00070000636218FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_25  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.25.255
        /// Description
        ///</summary>
        _Event_Log_25 = 0x00070000636219FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_26  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.26.255
        /// Description
        ///</summary>
        _Event_Log_26 = 0x0007000063621AFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_27  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.27.255
        /// Description
        ///</summary>
        _Event_Log_27 = 0x0007000063621BFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_28  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.28.255
        /// Description
        ///</summary>
        _Event_Log_28 = 0x0007000063621CFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_29  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.29.255
        /// Description
        ///</summary>
        _Event_Log_29 = 0x0007000063621DFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_30  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.30.255
        /// Description
        ///</summary>
        _Event_Log_30 = 0x0007000063621EFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_31  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.31.255
        /// Description
        ///</summary>
        _Event_Log_31 = 0x0007000063621FFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_32  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.32.255
        /// Description
        ///</summary>
        _Event_Log_32 = 0x00070000636220FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_33  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.33.255
        /// Description
        ///</summary>
        _Event_Log_33 = 0x00070000636221FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_34  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.34.255
        /// Description
        ///</summary>
        _Event_Log_34 = 0x00070000636222FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_35  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.35.255
        /// Description
        ///</summary>
        _Event_Log_35 = 0x00070000636223FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_36  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.36.255
        /// Description
        ///</summary>
        _Event_Log_36 = 0x00070000636224FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_37  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.37.255
        /// Description
        ///</summary>
        _Event_Log_37 = 0x00070000636225FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_38  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.38.255
        /// Description
        ///</summary>
        _Event_Log_38 = 0x00070000636226FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_39  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.39.255
        /// Description
        ///</summary>
        _Event_Log_39 = 0x00070000636227FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_40  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.40.255
        /// Description
        ///</summary>
        _Event_Log_40 = 0x00070000636228FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_41  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.41.255
        /// Description
        ///</summary>
        _Event_Log_41 = 0x00070000636229FF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_42  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.42.255
        /// Description
        ///</summary>
        _Event_Log_42 = 0x0007000063622AFF,


        ///<summary>
        ///Description: Quantity_Name= _Event_Log_43  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.43.255
        /// Description
        ///</summary>
        _Event_Log_43 = 0x0007000063622BFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_44  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.44.255
        /// Description
        ///</summary>
        _Event_Log_44 = 0x0007000063622CFF,



        ///<summary>
        ///Description: Quantity_Name= _Event_Log_45  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.45.255
        /// Description
        ///</summary>
        _Event_Log_45 = 0x0007000063622DFF,


        ///<summary>
        ///Description: Quantity_Name= _Event_Log_46  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.46.255
        /// Description
        ///</summary>
        _Event_Log_46 = 0x0007000063622EFF,


        ///<summary>
        ///Description: Quantity_Name= _Event_Log_47  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.47.255
        /// Description
        ///</summary>
        _Event_Log_47 = 0x0007000063622FFF,


        ///<summary>
        ///Description: Quantity_Name= _Event_Log_48  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.48.255
        /// Description
        ///</summary>
        _Event_Log_48 = 0x00070000636230FF,


        ///<summary>
        /// Description: Quantity_Name= _Event_Log_49  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.49.255
        /// Description
        ///</summary>
        _Event_Log_49 = 0x00070000636231FF,


        ///<summary>
        ///Description: Quantity_Name= _Event_Log_50  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.50.255
        /// Description
        ///</summary>
        _Event_Log_50 = 0x00070000636232FF,


        ///<summary>
        ///Description: Quantity_Name= _Event_Log_51  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.51.255
        /// Description
        ///</summary>
        _Event_Log_51 = 0x00070000636233FF,


        ///<summary>
        ///Description: Quantity_Name= _Event_Log_52  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.52.255
        /// Description
        ///</summary>
        _Event_Log_52 = 0x00070000636234FF,


        ///<summary>
        /// Description: Quantity_Name= _Event_Log_53  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.53.255
        /// Description
        ///</summary>
        _Event_Log_53 = 0x00070000636235FF,


        /// <summary>
        /// Description: Quantity_Name= _Event_Log_54  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.54.255
        /// Description
        ///</summary>
        _Event_Log_54 = 0x00070000636236FF,

        /// <summary>
        /// Description: Quantity_Name= _Event_Log_55  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.55.255
        /// Description
        ///</summary>
        _Event_Log_55 = 0x00070000636237FF,

        ///<summary>
        ///Description: Quantity_Name= _Event_Log_56  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.56.255
        /// Description
        ///</summary>
        _Event_Log_56 = 0x00070000636238FF,

        ///<summary>
        /// Description: Quantity_Name= _Event_Log_57  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.57.255
        /// Description
        ///</summary>
        _Event_Log_57 = 0x00070000636239FF,

        ///<summary>
        /// Description: Quantity_Name= _Event_Log_58  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.58.255
        /// Description
        ///</summary>
        _Event_Log_58 = 0x0007000063623AFF,

        ///<summary>
        /// Description: Quantity_Name= _Event_Log_59  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.59.255
        /// Description
        ///</summary>
        _Event_Log_59 = 0x0007000063623BFF,

        ///<summary>
        /// Description: Quantity_Name= _Event_Log_60  Class ID 7 
        /// OBIS_CODE = 0.0.99.98.60.255
        /// Description
        ///</summary>
        _Event_Log_60 = 0x0007000063623CFF,


        /// <summary>
        /// Description: Quantity_Name= Event_Detail  Class ID 1 
        /// OBIS_CODE = 0.0.97.215.0.255
        /// Description
        /// </summary>
        Event_Detail = 0x0001000061D700FF,


        ///<summary>
        ///Description: Quantity_Name= Event_Detail_SP  Class ID 1 
        /// OBIS_CODE = 0.0.97.215.46.255
        /// Description
        ///</summary>
        Event_Detail_SP = 0x0001000061D72EFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Count_Total  Class ID 1 
        /// OBIS_CODE = 0.0.96.15.40.255
        /// Description
        ///</summary>
        Event_Count_Total = 0x00010000600F28FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_00  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.0.255
        /// Description
        ///</summary>
        Event_Counter_00 = 0x0001000060D700FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_01  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.1.255
        /// Description
        ///</summary>
        Event_Counter_01 = 0x0001000060D701FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_02  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.2.255
        /// Description
        ///</summary>
        Event_Counter_02 = 0x0001000060D702FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_03  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.3.255
        /// Description
        ///</summary>
        Event_Counter_03 = 0x0001000060D703FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_04  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.4.255
        /// Description
        ///</summary>
        Event_Counter_04 = 0x0001000060D704FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_05  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.5.255
        /// Description
        ///</summary>
        Event_Counter_05 = 0x0001000060D705FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_06  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.6.255
        /// Description
        ///</summary>
        Event_Counter_06 = 0x0001000060D706FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_07  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.7.255
        /// Description
        ///</summary>
        Event_Counter_07 = 0x0001000060D707FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_08  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.8.255
        /// Description
        ///</summary>
        Event_Counter_08 = 0x0001000060D708FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_09  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.9.255
        /// Description
        ///</summary>
        Event_Counter_09 = 0x0001000060D709FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_10  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.10.255
        /// Description
        ///</summary>
        Event_Counter_10 = 0x0001000060D70AFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_11  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.11.255
        /// Description
        ///</summary>
        Event_Counter_11 = 0x0001000060D70BFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_12  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.12.255
        /// Description
        ///</summary>
        Event_Counter_12 = 0x0001000060D70CFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_13  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.13.255
        /// Description
        ///</summary>
        Event_Counter_13 = 0x0001000060D70DFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_14  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.14.255
        /// Description
        ///</summary>
        Event_Counter_14 = 0x0001000060D70EFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_15  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.15.255
        /// Description
        ///</summary>
        Event_Counter_15 = 0x0001000060D70FFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_16  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.16.255
        /// Description
        ///</summary>
        Event_Counter_16 = 0x0001000060D710FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_17  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.17.255
        /// Description
        ///</summary>
        Event_Counter_17 = 0x0001000060D711FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_18  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.18.255
        /// Description
        ///</summary>
        Event_Counter_18 = 0x0001000060D712FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_19  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.19.255
        /// Description
        ///</summary>
        Event_Counter_19 = 0x0001000060D713FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_20  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.20.255
        /// Description
        ///</summary>
        Event_Counter_20 = 0x0001000060D714FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_21  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.21.255
        /// Description
        ///</summary>
        Event_Counter_21 = 0x0001000060D715FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_22  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.22.255
        /// Description
        ///</summary>
        Event_Counter_22 = 0x0001000060D716FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_23  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.23.255
        /// Description
        ///</summary>
        Event_Counter_23 = 0x0001000060D717FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_24  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.24.255
        /// Description
        ///</summary>
        Event_Counter_24 = 0x0001000060D718FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_25  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.25.255
        /// Description
        ///</summary>
        Event_Counter_25 = 0x0001000060D719FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_26  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.26.255
        /// Description
        ///</summary>
        Event_Counter_26 = 0x0001000060D71AFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_27  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.27.255
        /// Description
        ///</summary>
        Event_Counter_27 = 0x0001000060D71BFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_28  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.28.255
        /// Description
        ///</summary>
        Event_Counter_28 = 0x0001000060D71CFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_29  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.29.255
        /// Description
        ///</summary>
        Event_Counter_29 = 0x0001000060D71DFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_30  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.30.255
        /// Description
        ///</summary>
        Event_Counter_30 = 0x0001000060D71EFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_31  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.31.255
        /// Description
        ///</summary>
        Event_Counter_31 = 0x0001000060D71FFF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_32  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.32.255
        /// Description
        ///</summary>
        Event_Counter_32 = 0x0001000060D720FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_33  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.33.255
        /// Description
        ///</summary>
        Event_Counter_33 = 0x0001000060D721FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_34  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.34.255
        /// Description
        ///</summary>
        Event_Counter_34 = 0x0001000060D722FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_35  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.35.255
        /// Description
        ///</summary>
        Event_Counter_35 = 0x0001000060D723FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_36  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.36.255
        /// Description
        ///</summary>
        Event_Counter_36 = 0x0001000060D724FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_37  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.37.255
        /// Description
        ///</summary>
        Event_Counter_37 = 0x0001000060D725FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_38  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.38.255
        /// Description
        ///</summary>
        Event_Counter_38 = 0x0001000060D726FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_39  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.39.255
        /// Description
        ///</summary>
        Event_Counter_39 = 0x0001000060D727FF,



        ///<summary>
        ///Description: Quantity_Name= Event_Counter_40  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.40.255
        /// Description
        ///</summary>
        Event_Counter_40 = 0x0001000060D728FF,


        ///<summary>
        ///Description: Quantity_Name= Event_Counter_41  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.41.255
        /// Description
        ///</summary>
        Event_Counter_41 = 0x0001000060D729FF,


        ///<summary>
        ///Description: Quantity_Name= Event_Counter_42  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.42.255
        /// Description
        ///</summary>
        Event_Counter_42 = 0x0001000060D72AFF,


        ///<summary>
        ///Description: Quantity_Name= Event_Counter_43  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.43.255
        /// Description
        ///</summary>
        Event_Counter_43 = 0x0001000060D72BFF,


        ///<summary>
        ///Description: Quantity_Name= Event_Counter_44  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.44.255
        /// Description
        ///</summary>
        Event_Counter_44 = 0x0001000060D72CFF,


        ///<summary>
        ///Description: Quantity_Name= Event_Counter_45  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.45.255
        /// Description
        ///</summary>
        Event_Counter_45 = 0x0001000060D72DFF,


        ///<summary>
        ///Description: Quantity_Name= Event_Counter_46  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.46.255
        /// Description
        ///</summary>
        Event_Counter_46 = 0x0001000060D72EFF,


        ///<summary>
        ///Description: Quantity_Name= Event_Counter_47  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.47.255
        /// Description
        ///</summary>
        Event_Counter_47 = 0x0001000060D72FFF,


        ///<summary>
        ///Description: Quantity_Name= Event_Counter_48  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.48.255
        /// Description
        ///</summary>
        Event_Counter_48 = 0x0001000060D730FF,


        ///<summary>
        ///Description: Quantity_Name= Event_Counter_49  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.49.255
        /// Description
        ///</summary>
        Event_Counter_49 = 0x0001000060D731FF,


        ///<summary>
        ///Description: Quantity_Name= Event_Counter_50  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.50.255
        /// Description
        ///</summary>
        Event_Counter_50 = 0x0001000060D732FF,


        ///<summary>
        /// Description: Quantity_Name= Event_Counter_51  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.51.255
        /// Description
        ///</summary>
        Event_Counter_51 = 0x0001000060D733FF,


        ///<summary>
        /// Description: Quantity_Name= Event_Counter_52  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.52.255
        /// Description
        ///</summary>
        Event_Counter_52 = 0x0001000060D734FF,


        ///<summary>
        /// Description: Quantity_Name= Event_Counter_53  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.53.255
        /// Description
        ///</summary>
        Event_Counter_53 = 0x0001000060D735FF,


        ///<summary>
        /// Description: Quantity_Name= Event_Counter_54  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.54.255
        /// Description
        ///</summary>
        Event_Counter_54 = 0x0001000060D736FF,


        ///<summary>
        /// Description: Quantity_Name= Event_Counter_55  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.55.255
        /// Description
        ///</summary>
        Event_Counter_55 = 0x0001000060D737FF,


        ///<summary>
        /// Description: Quantity_Name= Event_Counter_56  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.56.255
        /// Description
        ///</summary>
        Event_Counter_56 = 0x0001000060D738FF,


        ///<summary>
        /// Description: Quantity_Name= Event_Counter_57  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.57.255
        /// Description
        ///</summary>
        Event_Counter_57 = 0x0001000060D739FF,


        ///<summary>
        /// Description: Quantity_Name = Event_Counter_58  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.58.255
        /// Description
        ///</summary>
        Event_Counter_58 = 0x0001000060D73AFF,


        ///<summary>
        /// Description: Quantity_Name = Event_Counter_59  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.59.255
        /// Description
        ///</summary>
        Event_Counter_59 = 0x0001000060D73BFF,


        ///<summary>
        ///Description: Quantity_Name = Event_Counter_60  Class ID 1 
        /// OBIS_CODE = 0.0.96.215.60.255
        /// Description
        ///</summary>
        Event_Counter_60 = 0x0001000060D73CFF,


        ///<summary>
        ///Description: Quantity_Name= Debugging  Class ID 1 
        /// OBIS_CODE = 0.0.199.200.50.255
        /// Description
        ///</summary>
        Debugging = 0x00010000C7C832FF,


        ///<summary>
        ///Description: Quantity_Name= _CB_Last_MDI_Reset_Date  Class ID 1 
        /// OBIS_CODE = 1.0.0.9.7.255
        /// Description
        ///</summary>
        _CB_Last_MDI_Reset_Date = 0x00010100000907FF,



        ///<summary>
        ///Description: Quantity_Name= _CB_Last_MDI_Reset_Time  Class ID 1 
        /// OBIS_CODE = 1.0.0.9.6.255
        /// Description
        ///</summary>
        _CB_Last_MDI_Reset_Time = 0x00010100000906FF,



        ///<summary>
        ///Description: Quantity_Name= _DW_Blink_All_Segments  Class ID 1 
        /// OBIS_CODE = 0.0.200.200.200.255
        /// Description
        ///</summary>
        // _DW_Blink_All_Segments = 0x00010000C8C8C8FF,

        ///<summary>
        ///Description: Quantity_Name= _DW_Blink_All_Segments  Class ID 1 
        /// OBIS_CODE = 0.0.199.200.200.255
        /// Description
        ///</summary>
        _DW_Blink_All_Segments = 0x00010000C7C8C8FF,



        ///<summary>
        ///Description: Quantity_Name= _Last_MDI_Reset_Date_Time  Class ID 1 
        /// OBIS_CODE = 1.0.0.1.2.255
        /// Description
        ///</summary>
        _Last_MDI_Reset_Date_Time = 0x00010100000102FF,



        ///<summary>
        ///Description: Quantity_Name= _Time_Based_Event_2  Class ID 1 
        /// OBIS_CODE = 0.0.97.142.2.255
        /// Description
        ///</summary>
        _Time_Based_Event_2 = 0x00010000618E02FF,



        ///<summary>
        ///Description: Quantity_Name= _Time_Based_Event_1  Class ID 1 
        /// OBIS_CODE = 0.0.97.142.1.255
        /// Description
        ///</summary>
        _Time_Based_Event_1 = 0x00010000618E01FF,



        ///<summary>
        ///Description: Quantity_Name= DATA_PROFILE_WITH_EVENTS_1  Class ID 1 
        /// OBIS_CODE = 0.0.0.143.1.255 
        ///
        ///</summary>
        DATA_PROFILE_WITH_EVENTS_1 = 0x00010000008F01FF,



        ///<summary>
        ///Description: Quantity_Name= DATA_PROFILE_WITH_EVENTS_2  Class ID 1 
        /// OBIS_CODE = 0.0.0.143.2.255 
        ///
        ///</summary>
        DATA_PROFILE_WITH_EVENTS_2 = 0x00010000008F02FF,



        ///<summary>
        ///Description: Quantity_Name= DATA_PROFILE_WITH_EVENTS_3  Class ID 1 
        /// OBIS_CODE = 0.0.0.143.3.255 
        ///
        ///</summary>
        DATA_PROFILE_WITH_EVENTS_3 = 0x00010000008F03FF,



        ///<summary>
        ///Description: Quantity_Name= DATA_PROFILE_WITH_EVENTS_4  Class ID 1 
        /// OBIS_CODE = 0.0.0.143.4.255 
        ///
        ///</summary>
        DATA_PROFILE_WITH_EVENTS_4 = 0x00010000008F04FF,



        ///<summary>
        ///Description: Quantity_Name= DATA_PROFILE_WITH_EVENTS_5  Class ID 1 
        /// OBIS_CODE = 0.0.0.143.5.255 
        ///
        ///</summary>
        DATA_PROFILE_WITH_EVENTS_5 = 0x00010000008F05FF,



        ///<summary>
        ///Description: Quantity_Name= DATA_PROFILE_WITH_EVENTS_6  Class ID 1 
        /// OBIS_CODE = 0.0.0.143.6.255 
        ///
        ///</summary>
        DATA_PROFILE_WITH_EVENTS_6 = 0x00010000008F06FF,



        ///<summary>
        ///Description: Quantity_Name= DATA_PROFILE_WITH_EVENTS_7  Class ID 1 
        /// OBIS_CODE = 0.0.0.143.7.255 
        ///
        ///</summary>
        DATA_PROFILE_WITH_EVENTS_7 = 0x00010000008F07FF,



        ///<summary>
        ///Description: Quantity_Name= DATA_PROFILE_WITH_EVENTS_8  Class ID 1 
        /// OBIS_CODE = 0.0.0.143.8.255 
        ///
        ///</summary>
        DATA_PROFILE_WITH_EVENTS_8 = 0x00010000008F08FF,



        ///<summary>
        ///Description: Quantity_Name= Absolute_Active_Power_Phase_1  Class ID 3 
        /// OBIS_CODE = 1.0.0.7.0.255 
        ///
        ///</summary>
        Absolute_Active_Power_Phase_1 = 0x00030100230700FF,



        ///<summary>
        ///Description: Quantity_Name= Absolute_Active_Power_Phase_2  Class ID 3 
        /// OBIS_CODE = 1.0.0.7.0.255 
        ///
        ///</summary>
        Absolute_Active_Power_Phase_2 = 0x00030100370700FF,



        ///<summary>
        ///Description: Quantity_Name= Absolute_Active_Power_Phase_3  Class ID 3 
        /// OBIS_CODE = 1.0.0.7.0.255 
        ///
        ///</summary>
        Absolute_Active_Power_Phase_3 = 0x000301004B0700FF,



        ///<summary>
        ///Description: Quantity_Name= Absolute_Active_Power_Total  Class ID 3 
        /// OBIS_CODE = 1.0.16.7.0.255 
        ///
        ///</summary>
        Absolute_Active_Power_Total = 0x00030100100700FF,



        ///<summary>
        ///Description: Quantity_Name= Tamper_Power  Class ID 3 
        /// OBIS_CODE = 1.0.98.226.0.255 
        ///
        ///</summary>
        Tamper_Power = 0x0003010062E200FF,



        ///<summary>
        ///Description: Quantity_Name= All_Segments_Window  Class ID 1 
        /// OBIS_CODE = 0.0.96.99.0.255 
        ///
        ///</summary>
        All_Segments_Window = 0x00010000606300FF,



        ///<summary>
        ///Description: Quantity_Name= Major_Alarm_Counter  Class ID 1 
        /// OBIS_CODE = 0.0.0.216.0.255 
        ///
        ///</summary>
        Major_Alarm_Counter = 0x0001000060D800FF,



        ///<summary>
        ///Description: Quantity_Name= Total_Current  Class ID 3 
        /// OBIS_CODE = 1.0.0.7.0.255 
        ///
        ///</summary>
        Total_Current = 0x000301005A0700FF,



        ///<summary>
        ///Description: Quantity_Name= MDI_Time_Left  Class ID 1 
        /// OBIS_CODE = 1.0.96.73.0.255 
        ///
        ///</summary>
        MDI_Time_Left = 0x00010100604900FF,



        ///<summary>
        ///Description: Quantity_Name= Current_Association4  Class ID 15 
        /// OBIS_CODE = 0.0.40.0.4.255 
        ///
        ///</summary>
        Current_Association4 = 0x000F0000280004FF,



        ///<summary>
        ///Description: Quantity_Name= RSSI_SignalStrength  Class ID 1 
        /// OBIS_CODE = 0.0.0.12.5.255 
        ///
        ///</summary>
        RSSI_SignalStrength = 0x00010000600C05FF,

        ///<summary>
        ///Description: Quantity_Name= Last Interval MDI KW  Class ID 3 
        /// OBIS_CODE = 1.0.15.5.0.255
        ///
        ///</summary>
        Last_Interval_MDI_KW = 0x000301000f0500FF,


        ///<summary>
        ///Description: Quantity_Name= Display_Windows_TestMode_Get  Class ID 7 
        /// OBIS_CODE = 0.0.21.0.3.255 
        ///Only Get
        ///</summary>
        Display_Windows_TestMode_Get = 0x00070000150003FF,



        ///<summary>
        ///Description: Quantity_Name= Diplay_Windows_TestMode_GetSet  Class ID 1 
        /// OBIS_CODE = 0.0.0.129.3.255 
        ///Get and Set
        ///</summary>
        Diplay_Windows_TestMode_GetSet = 0x00010000158103FF,



        ///<summary>
        ///Description: Quantity_Name= Instantaneous_ALL_IN_ONE  Class ID 1 
        /// OBIS_CODE = 0.0.200.1.0.255 
        ///Get and Set
        ///</summary>
        //Instantaneous_ALL_IN_ONE = 0x00010000C80100FF,

        ///<summary>
        ///Description: Quantity_Name= Instantaneous_ALL_IN_ONE  Class ID 1 
        /// OBIS_CODE = 0.0.199.1.0.255 
        ///Get and Set
        ///</summary>
        Instantaneous_ALL_IN_ONE = 0x00010000C70100FF,



        ///<summary>
        ///Description: Quantity_Name= RW_ALL_IN_ONE  Class ID 1 
        /// OBIS_CODE = 0.0.200.1.1.255 
        ///Get and Set
        ///</summary>
        //RW_ALL_IN_ONE = 0x00010000C80101FF,

        ///<summary>
        ///Description: Quantity_Name= RW_ALL_IN_ONE  Class ID 1 
        /// OBIS_CODE = 0.0.199.1.1.255 
        ///Get and Set
        ///</summary>
        RW_ALL_IN_ONE = 0x00010000C70101FF,



        ///<summary>
        ///Description: Quantity_Name= Read_Raw_data  Class ID 1 
        /// OBIS_CODE = 0.0.200.2.0.255 
        /////
        ///</summary>
        // Read_Raw_data = 0x00010000C80200FF,

        ///<summary>
        ///Description: Quantity_Name= Read_Raw_data  Class ID 1 
        /// OBIS_CODE = 0.0.199.2.0.255 
        /////
        ///</summary>
        Read_Raw_data = 0x00010000C70200FF,




        ///<summary>
        ///Description: Quantity_Name= Get_Set_Raw_Data_address  Class ID 1 
        /// OBIS_CODE = 0.0.200.2.1.255 
        ///
        ///</summary>
        // Get_Set_Raw_Data_address = 0x00010000C80201FF,

        ///<summary>
        ///Description: Quantity_Name= Get_Set_Raw_Data_address  Class ID 1 
        /// OBIS_CODE = 0.0.199.2.1.255 
        ///
        ///</summary>
        Get_Set_Raw_Data_address = 0x00010000C70201FF,



        ///<summary>
        ///Description: Quantity_Name= Make_Error  Class ID 1 
        /// OBIS_CODE = 0.0.200.3.0.255 
        ///
        ///</summary>
        // Make_Error = 0x00010000C80300FF,

        ///<summary>
        ///Description: Quantity_Name= Make_Error  Class ID 1 
        /// OBIS_CODE = 0.0.199.3.0.255 
        ///
        ///</summary>
        Make_Error = 0x00010000C70300FF,



        ///<summary>
        ///Description: Quantity_Name= Read_errors  Class ID 1 
        /// OBIS_CODE = 0.0.200.4.0.255 
        ///
        ///</summary>
        // Read_errors = 0x00010000C80400FF,

        ///<summary>
        ///Description: Quantity_Name= Read_errors  Class ID 1 
        /// OBIS_CODE = 0.0.199.4.0.255 
        ///
        ///</summary>
        Read_errors = 0x00010000C70400FF,


        ///<summary>
        ///Description: Quantity_Name= Read_Cautions  Class ID 1 
        /// OBIS_CODE = 0.0.200.4.1.255 
        ///
        ///</summary>
        /// Read_Cautions = 0x00010000C80401FF,


        ///<summary>
        ///Description: Quantity_Name= Read_Cautions  Class ID 1 
        /// OBIS_CODE = 0.0.199.4.1.255 
        ///
        ///</summary>
        Read_Cautions = 0x00010000C70401FF,


        ///<summary>
        ///Description: Quantity_Name= Firmware_Update_PASSWORD  Class ID 1 
        /// OBIS_CODE = 0.0.199.10.0.255 
        ///
        ///</summary>
        Firmware_Update_PASSWORD = 0x00010000C70A00FF,


        ///<summary>
        ///Description: Quantity_Name= Firmware_Update_SWITCHFOR  Class ID 1 
        /// OBIS_CODE = 0.0.199.10.1.255 
        ///
        ///</summary>
        Firmware_Update_SWITCHFOR = 0x00010000C70A01FF,


        ///<summary>
        ///Description: Quantity_Name= TL_Load_Profile  Class ID 7 
        /// OBIS_CODE = 1.0.99.1.200.255 
        ///
        ///</summary>
        TL_Load_Profile = 0x000701006301C8FF,


        ///<summary>
        ///Description: Quantity_Name= Dummmyyy_TL_Load_Profile  Class ID 1 
        /// OBIS_CODE = 1.0.99.200.200.255 
        ///
        ///</summary>
        Dummy_TL_Load_Profile = 0x0001010063C8C8FF,


        ///<summary>
        ///Description: Quantity_Name= CB_Day_Record  Class ID 7 
        /// OBIS_CODE = 1.0.98.1.200.255 
        ///
        ///</summary>
        CB_Day_Record = 0x000701006201C8FF,


        ///<summary>
        ///Description: Quantity_Name= DUMMMMYYYY_CB_Day_Record  Class ID 1 
        /// OBIS_CODE = 1.0.98.200.200.255 
        ///
        ///</summary>
        Dummy_CB_DAY_RECORD = 0x0001010062C8C8FF,

        ///<summary>
        ///Description: Quantity_Name= Firmware_ID_2  Class ID 1 
        /// OBIS_CODE = 1.0.0.2.200.255 
        ///
        ///</summary>
        Firmware_ID_2 = 0x000101000002C8FF,


        ///<summary>
        ///Description: Quantity_Name= FIRMWARE_UPDATE_PASSWORD  Class ID 1 
        /// OBIS_CODE = 0.0.199.10.0.255 
        ///
        ///</summary>
        ///FIRMWARE_UPDATE_PASSWORD = 0x00010000C70A00FF,

        ///<summary>
        ///Description: Quantity_Name= SWITCH_FOR_FIRMWARE_UPDATE  Class ID 1 
        /// OBIS_CODE = 0.0.199.10.1.255 
        ///
        ///</summary>
        ///SWITCH_FOR_FIRMWARE_UPDATE = 0x00010000C70A01FF,

        //FIRMWARE_UPDATE_PASSWORD       Class 1(0.0..10.0.255)octet string write only
        //SWITCH_FOR_FIRMWARE_UPDATE    Class 1(0.0..10.0.255)octet string write only


        //____________________________________________________________________________________________________

        //_____________________________________SINGLE PHASE QUANTITIES________________________________________

        //____________________________________________________________________________________________________
        ///10/3/2012 3:38:11 PM Version 1.0
        ///Warning: This is a autogenerated code. Any addition to this code is not recommended.



        /////<summary>
        /////Description: Quantity_Name= Number_of_Power_Failures  Class ID 1 
        ///// OBIS_CODE = 0.0.96.7.9.255 
        /////Here goes the Description
        /////</summary>
        //Number_of_Power_Failures = 0x00010000600709FF,



        /////<summary>
        /////Description: Quantity_Name= Meter_Firmware_number  Class ID 1 
        ///// OBIS_CODE = 1.0.0.2.0.255 
        /////
        /////</summary>
        //Meter_Firmware_number = 0x00010100000200FF,

        ///<summary>
        ///Description: Quantity_Name= Number_of_Power_Failures  Class ID 1 
        /// OBIS_CODE = 0.0.96.7.9.255 
        ///Here goes the Description
        ///</summary>
        Number_of_Power_Failures_SP = 0x00010000600709FF,

        /////<summary>
        /////Description: Quantity_Name= Cumulative_billing_energy_KWh  Class ID 3 
        ///// OBIS_CODE = 1.0.0.8.0.255 
        /////
        /////</summary>
        //Cumulative_billing_energy_KWh = 0x00030100010800FF,



        /////<summary>
        /////Description: Quantity_Name= Instantaneous_Current  Class ID 3 
        ///// OBIS_CODE = 1.0.31.7.0.255 
        /////
        /////</summary>
        //Instantaneous_Current = 0x000301001F0700FF,



        /////<summary>
        /////Description: Quantity_Name= Instantaneous_Voltage  Class ID 3 
        ///// OBIS_CODE = 1.0.32.7.0.255 
        /////
        /////</summary>
        //Instantaneous_Voltage = 0x00030100200700FF,



        /////<summary>
        /////Description: Quantity_Name= Power_Factor  Class ID 3 
        ///// OBIS_CODE = 1.0.13.7.0.255 
        /////
        /////</summary>
        //Power_Factor = 0x000301000D0700FF,



        ///<summary>
        ///Description: Quantity_Name= Current_Max_demand_KW  Class ID 4 
        /// OBIS_CODE = 1.0.21.6.0.255 
        ///
        ///</summary>
        Current_Max_demand_KW = 0x00040100150600FF,



        ///<summary>
        ///Description: Quantity_Name= Current_on_Neutral  Class ID 3 
        /// OBIS_CODE = 1.0.91.7.0.255 
        ///
        ///</summary>
        Current_on_Neutral = 0x000301005B0700FF,



        ///<summary>
        ///Description: Quantity_Name= Active_Power_on_Neutral  Class ID 3 
        /// OBIS_CODE = 1.0.91.129.0.255 
        ///
        ///</summary>
        Active_Power_on_Neutral = 0x000301005B8100FF,



        ///<summary>
        ///Description: Quantity_Name= Active_power_on_Live  Class ID 3 
        /// OBIS_CODE = 1.0.91.130.0.255 
        ///
        ///</summary>
        Active_power_on_Live = 0x000301005B8200FF,


        /// NEW CODES UPDATED JANUARY 24, 2013
        /// 
        ///<summary>
        ///Description: Quantity_Name= ReadRawData  Class ID 1 
        /// OBIS_CODE = 0.0.200.2.0.255 
        ///
        ///</summary>
        //  ReadRawData = 0x00010000C80200FF,


        ///<summary>
        ///Description: Quantity_Name= ReadRawDataAddresses  Class ID 1 
        /// OBIS_CODE = 0.0.200.2.1.255 
        ///
        ///</summary>
        //  ReadRawDataAddresses = 0x00010000C80201FF,


        /////<summary>
        /////Description: Quantity_Name= ReadRawDataAddresses  Class ID 1 
        ///// OBIS_CODE = 0.0.199.2.1.255 
        /////
        /////</summary>
        //ReadRawDataAddresses = 0x00010000C70201FF,



        ///<summary>
        ///Description: Quantity_Name= MakeError  Class ID 1 
        /// OBIS_CODE = 0.0.200.3.0.255 
        ///
        ///</summary>
        //  MakeError = 0x00010000C80300FF,



        ///<summary>
        ///Description: Quantity_Name= TimeBaseEvent on PowerFail Class ID 1
        /// OBIS_CODE = 0.0.97.142.3.255 
        ///
        ///</summary>
        TBE_PowerFail = 0x00010000618E03FF,

        /////<summary>
        /////Description: Quantity_Name= CB_DayRecord  Class ID 7 
        ///// OBIS_CODE = 1.0.98.1.200.255 
        /////
        /////</summary>
        //CB_DayRecord = 0x000701006201C8FF,

        ///<summary>
        ///Description: Quantity_Name= ContactorRegister  Class ID 1
        /// OBIS_CODE = 0.0.96.3.129.255 
        ///
        ///</summary>
        I_Contactor_Flag = 0x00010000600381FF,

        ///<summary>
        ///Description: Quantity_Name= Contactor_connect_through_Swicth  Class ID 1
        /// OBIS_CODE = 0.0.96.3.130.255 
        ///Data Type : enum
        ///Value: 0 no command pending
        ///1 Contactor Connect by switch	
        ///</summary>
        Contactor_connect_through_Switch = 0x00010000600382FF,

        ///<summary>
        ///Description: Quantity_Name= Display_At_Power_Down_Mode  Class ID 1
        /// OBIS_CODE = 0.0.21.129.0.255 
        ///Data Type : Octet String 16 byte
        ///Value: 0 no command pending
        ///1 Display at Power Down Mode	
        ///</summary>
        Display_At_Power_Down_Mode = 0x00010000158100FF,

        ///<summary>
        ///Description: Quantity_Name= General Process Param  Class ID 1
        /// OBIS_CODE = 0.0.199.11.0.255 
        ///Data Type : Octet String 16 byte
        ///Value: 0 no command pending
        ///1 General Process Param 
        ///</summary>
        General_Process_Param = 0x00010000C70B00FF,

        ///<summary>
        ///Description: Quantity_Name= Current_Association5  Class ID 15 
        /// OBIS_CODE = 0.0.40.0.5.255
        /// Description
        ///</summary>
        Current_Association5 = 0x000F0000280005FF,


        ///<summary>
        /// Description: Quantity_Name= Current_Association6  Class ID 15 
        /// OBIS_CODE = 0.0.40.0.6.255
        /// Description
        ///</summary>
        Current_Association6 = 0x000F0000280006FF,


        ///<summary>
        /// Description: Quantity_Name= Current_Association7  Class ID 15 
        /// OBIS_CODE = 0.0.40.0.7.255
        /// Description
        ///</summary>
        Current_Association7 = 0x000F0000280007FF,


        ///<summary>
        ///Description: Quantity_Name= Current_Association8  Class ID 15 
        /// OBIS_CODE = 0.0.40.0.8.255
        /// Description
        ///</summary>
        Current_Association8 = 0x000F0000280008FF,


        ///<summary>
        /// Description: Quantity_Name= Current_Association9  Class ID 15 
        /// OBIS_CODE = 0.0.40.0.9.255
        /// Description
        ///</summary>
        Current_Association9 = 0x000F0000280009FF,


        ///<summary>
        /// Description: Quantity_Name= Security_Setup_CurrentAA  Class ID 64 
        /// OBIS_CODE = 0.0.43.0.0.255
        /// Description
        ///</summary>
        Security_Setup_CurrentAA = 0x004000002B0000FF,

        ///<summary>
        /// Description: Quantity_Name= Security_Setup_AA_1  Class ID 64 
        /// OBIS_CODE = 0.0.43.0.1.255
        /// Description
        ///</summary>
        Security_Setup_AA_1 = 0x004000002B0001FF,

        ///<summary>
        /// Description: Quantity_Name= Security_Setup_AA_2  Class ID 64 
        /// OBIS_CODE = 0.0.43.0.2.255
        /// Description
        ///</summary>
        Security_Setup_AA_2 = 0x004000002B0002FF,

        ///<summary>
        /// Description: Quantity_Name= Security_Setup_AA_3  Class ID 64 
        /// OBIS_CODE = 0.0.43.0.3.255
        /// Description
        ///</summary>
        Security_Setup_AA_3 = 0x004000002B0003FF,

        ///<summary>
        /// Description: Quantity_Name= Security_Setup_AA_4  Class ID 64 
        /// OBIS_CODE = 0.0.43.0.4.255
        /// Description
        ///</summary>
        Security_Setup_AA_4 = 0x004000002B0004FF,

        ///<summary>
        /// Description: Quantity_Name= Security_Setup_AA_5  Class ID 64 
        /// OBIS_CODE = 0.0.43.0.5.255
        /// Description
        ///</summary>
        Security_Setup_AA_5 = 0x004000002B0005FF,

        ///<summary>
        /// Description: Quantity_Name= Security_Setup_AA_6  Class ID 64 
        /// OBIS_CODE = 0.0.43.0.6.255
        /// Description
        ///</summary>
        Security_Setup_AA_6 = 0x004000002B0006FF,

        ///<summary>
        /// Description: Quantity_Name= Security_Setup_AA_7  Class ID 64 
        /// OBIS_CODE = 0.0.43.0.7.255
        /// Description
        ///</summary>
        Security_Setup_AA_7 = 0x004000002B0007FF,

        ///<summary>
        /// Description: Quantity_Name= Security_Setup_AA_8  Class ID 64 
        /// OBIS_CODE = 0.0.43.0.8.255
        /// Description
        ///</summary>
        Security_Setup_AA_8 = 0x004000002B0008FF,

        ///<summary>
        /// Description: Quantity_Name= Security_Setup_AA_9  Class ID 64 
        /// OBIS_CODE = 0.0.43.0.9.255
        /// Description
        ///</summary>
        Security_Setup_AA_9 = 0x004000002B0009FF,

        ///<summary>
        /// Description: Quantity_Name= Activate Door Open Class ID 1 
        /// OBIS_CODE = 0.0.199.11.1.255
        /// Description
        ///</summary>
        Activate_Door_Open = 0x00010000C70B01FF,

        ///<summary>
        /// Description: Quantity_Name= Calibration Mode Active or Deactive  Class ID 1 
        /// OBIS_CODE = 0.0.199.12.1.255
        /// Description
        ///</summary>
        Calibraion_Mode = 0x00010000C70C01FF,

        ///<summary>
        /// Description: Quantity_Name= Status Word  Class ID 1 
        /// OBIS_CODE = 0.0.96.10.1.255
        /// Description
        ///</summary>
        OT_STATUS_WORD_LP1 = 0x00010000600A01FF,
        ///<summary>
        /// Description: Quantity_Name= Status Word Map Class ID 1 
        /// OBIS_CODE = 0.0.96.10.128.255
        /// Description
        ///</summary>
        Status_Word_Map = 0x00010000600A80FF,

        ///<summary>
        /// Description: Quantity_Name= Internal Object Status Word  Class ID 63 
        /// OBIS_CODE = 0.0.96.05.1.255
        /// Description
        ///</summary>
        Internal_Object_Status_Word = 0x00630000600501FF,

        /////<summary>
        ///// Description: Quantity_Name= Status Word  Class ID 1 
        ///// OBIS_CODE = 0.0.96.10.2.255
        ///// Description
        /////</summary>
        //Status_Word2 = 0x00010000600A02FF,

        ///<summary>
        /// Description: Quantity_Name= Status Word Map Class ID 1 
        /// OBIS_CODE = 0.0.96.10.129.255
        /// Description
        ///</summary>
        Status_Word_Map2 = 0x00010000600A81FF,


        ///<summary>
        /// Description: Quantity_Name= Clock Synchronization Method  Class ID 1 
        /// OBIS_CODE = 1.0.0.9.10.255
        /// Description
        ///</summary>
        Clock_Synchronization_Method = 0x0001010000090AFF,

        ///<summary>
        /// Description: Quantity_Name= Clock Time Shift Limit
        /// OBIS_CODE = 1.0.0.9.11.255
        /// Description
        ///</summary>
        Clock_Time_Shift_Limit = 0x0001010000090BFF,

        ///<summary>
        /// Description: Quantity_Name= Clock Synchronization Window
        /// oldOBIS_CODE = 1.0.0.9.9.255  newOBIS_CODE = 1.199.0.9.9.255
        /// Description
        ///</summary>
        Clock_Synchronization_Window = 0x00010100C70909FF, // 0x00010100000909FF,

        ///<summary>
        /// Description: Quantity_Name= Watch Dog Reset  Class ID 1 
        /// OBIS_CODE = 0.0.10.151.0.255
        /// Description
        ///</summary>
        Watch_Dog_Reset = 0x000100000A9700FF,

        ///<summary>
        /// Description: Quantity_Name= Timer Reset  Class ID 1 
        /// OBIS_CODE = 0.0.11.151.0.255
        /// Description
        ///</summary>
        Timer_Reset = 0x000100000B9700FF,

        ///<summary>
        /// Description: Quantity_Name= Power Up Reset  Class ID 1 
        /// OBIS_CODE = 0.0.12.151.0.255
        /// Description
        ///</summary>
        Power_Up_Reset = 0x000100000C9700FF,

        // Newly Added

        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI IMPORT TOTAL 
        /// OBIS_CODE = 1.0.1.2.0.255
        /// Description
        ///</summary>
        Active_MDI_Positive_TL = 0x00030100010200FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI IMPORT T1
        /// OBIS_CODE = 1.0.1.2.1.255
        /// Description
        ///</summary>
        Active_MDI_Positive_T1 = 0x00030100010201FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI IMPORT T2 
        /// OBIS_CODE = 1.0.1.2.2.255
        /// Description
        ///</summary>
        Active_MDI_Positive_T2 = 0x00030100010202FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI IMPORT T3 
        /// OBIS_CODE = 1.0.1.2.3.255
        /// Description
        ///</summary>
        Active_MDI_Positive_T3 = 0x00030100010203FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI IMPORT T4 
        /// OBIS_CODE = 1.0.1.2.4.255
        /// Description
        ///</summary>
        Active_MDI_Positive_T4 = 0x00030100010204FF,

        ///<summary>
        /// Description: Quantity_Name= MONTHLY ACTIVE MDI IMPORT TOTAL 
        /// OBIS_CODE = 1.0.1.6.0.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Positive_TL = 0x00040100010600FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY ACTIVE MDI IMPORT T1 
        /// OBIS_CODE = 1.0.1.6.1.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Positive_T1 = 0x00040100010601FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY ACTIVE MDI IMPORT T2 
        /// OBIS_CODE = 1.0.1.6.2.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Positive_T2 = 0x00040100010602FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY ACTIVE MDI IMPORT T3
        /// OBIS_CODE = 1.0.1.6.3.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Positive_T3 = 0x00040100010603FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY ACTIVE MDI IMPORT T4 
        /// OBIS_CODE = 1.0.1.6.4.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Positive_T4 = 0x00040100010604FF,

        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER IMPORT AGGREGATE 
        /// OBIS_CODE = 1.0.1.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_IMPORT_AGGREGATE = 0x00030100011B00FF,

        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI EXPORT TOTAL 
        /// OBIS_CODE = 1.0.2.2.0.255
        /// Description
        ///</summary>
        Active_MDI_Negative_TL = 0x00030100020200FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI EXPORT T1 
        /// OBIS_CODE = 1.0.2.2.1.255
        /// Description
        ///</summary>
        Active_MDI_Negative_T1 = 0x00030100020201FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI EXPORT T2 
        /// OBIS_CODE = 1.0.2.2.2.255
        /// Description
        ///</summary>
        Active_MDI_Negative_T2 = 0x00030100020202FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI EXPORT T3
        /// OBIS_CODE = 1.0.2.2.3.255
        /// Description
        ///</summary>
        Active_MDI_Negative_T3 = 0x00030100020203FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI EXPORT T4 
        /// OBIS_CODE = 1.0.2.2.4.255
        /// Description
        ///</summary>
        Active_MDI_Negative_T4 = 0x00030100020204FF,

        ///<summary>
        /// Description: Quantity_Name= DEMAND ACTIVE EXPORT 
        /// OBIS_CODE = 1.0.2.4.0.255
        /// Description
        ///</summary>
        DEMAND_ACTIVE_EXPORT = 0x00050100020400FF,

        ///<summary>
        /// Description: Quantity_Name= DEMAND_ACTIVE_IMPORT  Class ID 5  DEMAND_ACTIVE_IMPORT
        /// OBIS_CODE = 1.0.1.4.0.255
        /// Description
        ///</summary>
        DEMAND_ACTIVE_IMPORT = 0x00050100010400FF,

        ///<summary>
        /// Description: Quantity_Name= MONTHLY ACTIVE MDI EXPORT TOTAL 
        /// OBIS_CODE = 1.0.2.6.0.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Negative_TL = 0x00040100020600FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY ACTIVE MDI EXPORT T1 
        /// OBIS_CODE = 1.0.2.6.1.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Negative_T1 = 0x00040100020601FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY ACTIVE MDI EXPORT T2
        /// OBIS_CODE = 1.0.2.6.2.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Negative_T2 = 0x00040100020602FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY ACTIVE MDI EXPORT T3 
        /// OBIS_CODE = 1.0.2.6.3.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Negative_T3 = 0x00040100020603FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY ACTIVE MDI EXPORT T4 
        /// OBIS_CODE = 1.0.2.6.4.255
        /// Description
        ///</summary>
        Monthly_Active_MDI_Negative_T4 = 0x00040100020604FF,

        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER EXPORT AGGREGATE
        /// OBIS_CODE = 1.0.2.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_EXPORT_AGGREGATE = 0x00030100021B00FF,

        ///<summary>
        /// Description: Quantity_Name= Cumulative Reactive MDI Import TOTAL
        /// OBIS_CODE = 1.0.3.2.0.255
        /// Description
        ///</summary>
        Reactive_MDI_Positive_TL = 0x00030100030200FF,
        ///<summary>
        /// Description: Quantity_Name= Cumulative Reactive MDI Import T1
        /// OBIS_CODE = 1.0.3.2.1.255
        /// Description
        ///</summary>
        Reactive_MDI_Positive_T1 = 0x00030100030201FF,
        ///<summary>
        /// Description: Quantity_Name= Cumulative Reactive MDI Import T2
        /// OBIS_CODE = 1.0.3.2.2.255
        /// Description
        ///</summary>
        Reactive_MDI_Positive_T2 = 0x00030100030202FF,
        ///<summary>
        /// Description: Quantity_Name= Cumulative Reactive MDI Import T3
        /// OBIS_CODE = 1.0.3.2.3.255
        /// Description
        ///</summary>
        Reactive_MDI_Positive_T3 = 0x00030100030203FF,
        ///<summary>
        /// Description: Quantity_Name= Cumulative Reactive MDI Import T4
        /// OBIS_CODE = 1.0.3.2.4.255
        /// Description
        ///</summary>
        Reactive_MDI_Positive_T4 = 0x00030100030204FF,

        ///<summary>
        /// Description: Quantity_Name= DEMAND REACTIVE IMPORT
        /// OBIS_CODE = 1.0.3.4.0.255
        /// Description
        ///</summary>
        DEMAND_REACTIVE_IMPORT = 0x00050100030400FF,
        ///<summary>
        /// Description: Quantity_Name= REACTIVE POWER IMPORT AGGREGATE
        /// OBIS_CODE = 1.0.3.27.0.255
        /// Description
        ///</summary>
        REACTIVE_POWER_IMPORT_AGGREGATE = 0x00030100031B00FF,

        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE REACTIVE MDI EXPORT TOTAL
        /// OBIS_CODE = 1.0.4.2.0.255
        /// Description
        ///</summary>
        Reactive_MDI_Negative_TL = 0x00030100040200FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE REACTIVE MDI EXPORT T1
        /// OBIS_CODE = 1.0.4.2.1.255
        /// Description
        ///</summary>
        Reactive_MDI_Negative_T1 = 0x00030100040201FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE REACTIVE MDI EXPORT T2
        /// OBIS_CODE = 1.0.4.2.2.255
        /// Description
        ///</summary>
        Reactive_MDI_Negative_T2 = 0x00030100040202FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE REACTIVE MDI EXPORT T3
        /// OBIS_CODE = 1.0.4.2.3.255
        /// Description
        ///</summary>
        Reactive_MDI_Negative_T3 = 0x00030100040203FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE REACTIVE MDI EXPORT T4
        /// OBIS_CODE = 1.0.4.2.4.255
        /// Description
        ///</summary>
        Reactive_MDI_Negative_T4 = 0x00030100040204FF,
        ///<summary>
        /// Description: Quantity_Name= DEMAND REACTIVE EXPORT
        /// OBIS_CODE = 1.0.4.4.0.255
        /// Description
        ///</summary>
        DEMAND_REACTIVE_EXPORT = 0x00050100040400FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY REACTIVE MDI EXPORT TOTAL
        /// OBIS_CODE = 1.0.4.6.0.255
        /// Description
        ///</summary>
        Monthly_Reactive_MDI_Negative_TL = 0x00040100040600FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY REACTIVE MDI EXPORT T1
        /// OBIS_CODE = 1.0.4.6.1.255
        /// Description
        ///</summary>
        Monthly_Reactive_MDI_Negative_T1 = 0x00040100040601FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY REACTIVE MDI EXPORT T2
        /// OBIS_CODE = 1.0.4.6.2.255
        /// Description
        ///</summary>
        Monthly_Reactive_MDI_Negative_T2 = 0x00040100040602FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY REACTIVE MDI EXPORT T3
        /// OBIS_CODE = 1.0.4.6.3.255
        /// Description
        ///</summary>
        Monthly_Reactive_MDI_Negative_T3 = 0x00040100040603FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY REACTIVE MDI EXPORT T4
        /// OBIS_CODE = 1.0.4.6.4.255
        /// Description
        ///</summary>
        Monthly_Reactive_MDI_Negative_T4 = 0x00040100040604FF,
        ///<summary>
        /// Description: Quantity_Name= REACTIVE POWER EXPORT AGGREGATE
        /// OBIS_CODE = 1.0.4.27.0.255
        /// Description
        ///</summary>
        REACTIVE_POWER_EXPORT_AGGREGATE = 0x00030100041B00FF,

        ///<summary>
        /// Description: Quantity_Name= REACTIVE POWER EXPORT AGGREGATE
        /// OBIS_CODE = 1.0.128.27.0.255
        /// Description
        ///</summary>
        REACTIVE_POWER_ABS_AGGREGATE = 0x00030100801B00FF,

        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI ABSOLUTE TOTAL
        /// OBIS_CODE = 1.0.15.2.0.255
        /// Description
        ///</summary>
        Active_MDI_Absolute_TL = 0x000301000F0200FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI ABSOLUTE T1
        /// OBIS_CODE = 1.0.15.2.1.255
        /// Description
        ///</summary>
        Active_MDI_Absolute_T1 = 0x000301000F0201FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI ABSOLUTE T2
        /// OBIS_CODE = 1.0.15.2.2.255
        /// Description
        ///</summary>
        Active_MDI_Absolute_T2 = 0x000301000F0202FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI ABSOLUTE T3
        /// OBIS_CODE = 1.0.15.2.3.255
        /// Description
        ///</summary>
        Active_MDI_Absolute_T3 = 0x000301000F0203FF,
        ///<summary>
        /// Description: Quantity_Name= CUMULATIVE ACTIVE MDI ABSOLUTE T4
        /// OBIS_CODE = 1.0.15.2.4.255
        /// Description
        ///</summary>
        Active_MDI_Absolute_T4 = 0x000301000F0204FF,

        ///<summary>
        /// Description: Quantity_Name= DEMAND ACTIVE ABSOLUTE
        /// OBIS_CODE = 1.0.15.4.0.255
        /// Description
        ///</summary>
        DEMAND_ACTIVE_ABSOLUTE = 0x000501000F0400FF,

        ///<summary>
        /// Description: Quantity_Name= CUM MDI KVAR TOTAL
        /// OBIS_CODE = 1.0.128.2.0.255
        /// Description
        ///</summary>
        CUM_MDI_KVAR_TOTAL = 0x00030100800200FF,
        ///<summary>
        /// Description: Quantity_Name= CUM MDI KVAR T1
        /// OBIS_CODE = 1.0.128.2.1.255
        /// Description
        ///</summary>
        CUM_MDI_KVAR_T1 = 0x00030100800201FF,
        ///<summary>
        /// Description: Quantity_Name= CUM MDI KVAR T2
        /// OBIS_CODE = 1.0.128.2.2.255
        /// Description
        ///</summary>
        CUM_MDI_KVAR_T2 = 0x00030100800202FF,
        ///<summary>
        /// Description: Quantity_Name= CUM MDI KVAR T3
        /// OBIS_CODE = 1.0.128.2.3.255
        /// Description
        ///</summary>
        CUM_MDI_KVAR_T3 = 0x00030100800203FF,
        ///<summary>
        /// Description: Quantity_Name= CUM MDI KVAR T4
        /// OBIS_CODE = 1.0.128.2.4.255
        /// Description
        ///</summary>
        CUM_MDI_KVAR_T4 = 0x00030100800204FF,

        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER ABS AGGREGATE
        /// OBIS_CODE = 1.0.15.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_ABS_AGGREGATE = 0x000301000F1B00FF,
        ///<summary>
        /// Description: Quantity_Name= Aggregate_Reactive_Power_Absolute
        /// OBIS_CODE = 1.0.3.180.0.255
        /// Description
        ///</summary>
        Aggregate_Reactive_Power_Absolute = 0x00030100038000FF,
        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER IMPORT PHASE_1
        /// OBIS_CODE = 1.0.21.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_IMPORT_PHASE_1 = 0x00030100151B00FF,
        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER EXPORT PHASE_1
        /// OBIS_CODE = 1.0.22.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_EXPORT_PHASE_1 = 0x00030100161B00FF,
        ///<summary>
        /// Description: Quantity_Name= REACTIVE POWER IMPORT PHASE_1
        /// OBIS_CODE = 1.0.23.27.0.255
        /// Description
        ///</summary>
        REACTIVE_POWER_IMPORT_PHASE_1 = 0x00030100171B00FF,
        ///<summary>
        /// Description: Quantity_Name= REACTIVE POWER EXPORT PHASE_1
        /// OBIS_CODE = 1.0.24.27.0.255
        /// Description
        ///</summary>
        REACTIVE_POWER_EXPORT_PHASE_1 = 0x00030100181B00FF,
        ///<summary>
        /// Description: Quantity_Name= CURRENT PHASE_1
        /// OBIS_CODE = 1.0.31.27.0.255
        /// Description
        ///</summary>
        CURRENT_PHASE_1 = 0x000301001F1B00FF,
        ///<summary>
        /// Description: Quantity_Name= VOLTAGE PHASE_1
        /// OBIS_CODE = 1.0.32.27.0.255
        /// Description
        ///</summary>
        VOLTAGE_PHASE_1 = 0x00030100201B00FF,
        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER ABS PHASE_1
        /// OBIS_CODE = 1.0.35.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_ABS_PHASE_1 = 0x00030100231B00FF,
        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER IMPORT PHASE_2
        /// OBIS_CODE = 1.0.41.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_IMPORT_PHASE_2 = 0x00030100291B00FF,
        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER EXPORT PHASE_2
        /// OBIS_CODE = 1.0.42.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_EXPORT_PHASE_2 = 0x000301002A1B00FF,
        ///<summary>
        /// Description: Quantity_Name= REACTIVE POWER IMPORT PHASE_2
        /// OBIS_CODE = 1.0.43.27.0.255
        /// Description
        ///</summary>
        REACTIVE_POWER_IMPORT_PHASE_2 = 0x000301002B1B00FF,
        ///<summary>
        /// Description: Quantity_Name= REACTIVE POWER EXPORT PHASE_2
        /// OBIS_CODE = 1.0.44.27.0.255
        /// Description
        ///</summary>
        REACTIVE_POWER_EXPORT_PHASE_2 = 0x000301002C1B00FF,
        ///<summary>
        /// Description: Quantity_Name= CURRENT PHASE_2
        /// OBIS_CODE = 1.0.51.27.0.255
        /// Description
        ///</summary>
        CURRENT_PHASE_2 = 0x00030100331B00FF,
        ///<summary>
        /// Description: Quantity_Name= VOLTAGE PHASE_2
        /// OBIS_CODE = 1.0.52.27.0.255
        /// Description
        ///</summary>
        VOLTAGE_PHASE_2 = 0x00030100341B00FF,
        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER ABS PHASE_2
        /// OBIS_CODE = 1.0.55.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_ABS_PHASE_2 = 0x00030100371B00FF,
        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER IMPORT PHASE_3
        /// OBIS_CODE = 1.0.61.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_IMPORT_PHASE_3 = 0x000301003D1B00FF,
        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER EXPORT PHASE_3
        /// OBIS_CODE = 1.0.62.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_EXPORT_PHASE_3 = 0x000301003E1B00FF,
        ///<summary>
        /// Description: Quantity_Name= REACTIVE POWER IMPORT PHASE_3
        /// OBIS_CODE = 1.0.63.27.0.255
        /// Description
        ///</summary>
        REACTIVE_POWER_IMPORT_PHASE_3 = 0x000301003F1B00FF,
        ///<summary>
        /// Description: Quantity_Name= REACTIVE POWER EXPORT PHASE_3
        /// OBIS_CODE = 1.0.64.27.0.255
        /// Description
        ///</summary>
        REACTIVE_POWER_EXPORT_PHASE_3 = 0x00030100401B00FF,
        ///<summary>
        /// Description: Quantity_Name= CURRENT PHASE_3
        /// OBIS_CODE = 1.0.71.27.0.255
        /// Description
        ///</summary>
        CURRENT_PHASE_3 = 0x00030100471B00FF,
        ///<summary>
        /// Description: Quantity_Name= VOLTAGE PHASE_3
        /// OBIS_CODE = 1.0.72.27.0.255
        /// Description
        ///</summary>
        VOLTAGE_PHASE_3 = 0x00030100481B00FF,
        ///<summary>
        /// Description: Quantity_Name= ACTIVE POWER ABS PHASE_3
        /// OBIS_CODE = 1.0.75.27.0.255
        /// Description
        ///</summary>
        ACTIVE_POWER_ABS_PHASE_3 = 0x000301004B1B00FF,
        ///<summary>
        /// Description: Quantity_Name= AGGREGATE_CURRENT
        /// OBIS_CODE = 1.0.90.27.0.255
        /// Description
        ///</summary>
        AGGREGATE_CURRENT = 0x000301005A1B00FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KW P TOTAL
        /// OBIS_CODE = 1.0.96.67.0.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KW_P_TOTAL = 0x00040100604300FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KW P T1
        /// OBIS_CODE = 1.0.96.67.1.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KW_P_T1 = 0x00040100604301FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KW P T2
        /// OBIS_CODE = 1.0.96.67.2.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KW_P_T2 = 0x00040100604302FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KW P T3
        /// OBIS_CODE = 1.0.96.67.3.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KW_P_T3 = 0x00040100604303FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KW P T4
        /// OBIS_CODE = 1.0.96.67.4.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KW_P_T4 = 0x00040100604304FF,

        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KVAR P TOTAL
        /// OBIS_CODE = 1.0.96.68.0.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KVAR_P_TOTAL = 0x00040100604400FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KVAR P T1
        /// OBIS_CODE = 1.0.96.68.1.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KVAR_P_T1 = 0x00040100604401FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KVAR P T2
        /// OBIS_CODE = 1.0.96.68.2.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KVAR_P_T2 = 0x00040100604402FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KVAR P T3
        /// OBIS_CODE = 1.0.96.68.3.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KVAR_P_T3 = 0x00040100604403FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KVAR P T4
        /// OBIS_CODE = 1.0.96.68.4.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KVAR_P_T4 = 0x00040100604404FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KW N TOTAL
        /// OBIS_CODE = 1.0.96.69.0.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KW_N_TOTAL = 0x00040100604500FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KW N T1
        /// OBIS_CODE = 1.0.96.69.1.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KW_N_T1 = 0x00040100604501FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KW N T2
        /// OBIS_CODE = 1.0.96.69.2.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KW_N_T2 = 0x00040100604502FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KW N T3
        /// OBIS_CODE = 1.0.96.69.3.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KW_N_T3 = 0x00040100604503FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KW N T4
        /// OBIS_CODE = 1.0.96.69.4.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KW_N_T4 = 0x00040100604504FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KVAR N TOTAL
        /// OBIS_CODE = 1.0.96.70.0.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KVAR_N_TOTAL = 0x00040100604600FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KVAR N T1
        /// OBIS_CODE = 1.0.96.70.1.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KVAR_N_T1 = 0x00040100604601FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KVAR N T2
        /// OBIS_CODE = 1.0.96.70.2.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KVAR_N_T2 = 0x00040100604602FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KVAR N T3
        /// OBIS_CODE = 1.0.96.70.3.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KVAR_N_T3 = 0x00040100604603FF,
        ///<summary>
        /// Description: Quantity_Name= PREVIOUS MONTH MDI KVAR N T4
        /// OBIS_CODE = 1.0.96.70.4.255
        /// Description
        ///</summary>
        PREVIOUS_MONTH_MDI_KVAR_N_T4 = 0x00040100604604FF,

        ///<summary>
        /// Description: Quantity_Name= DEMAND ALL
        /// OBIS_CODE = 1.0.129.4.0.255
        /// Description
        ///</summary>
        DEMAND_ALL = 0x00050100810400FF,

        ///<summary>
        /// Description: Quantity_Name= MONTHLY REACTIVE MDI ABSOLUTE TOTAL
        /// OBIS_CODE = 1.0.128.6.0.255
        /// Description
        ///</summary>
        MONTHLY_REACTIVE_MDI_ABSOLUTE_TOTAL = 0x00040100800600FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY REACTIVE MDI ABSOLUTE T1
        /// OBIS_CODE = 1.0.128.6.1.255
        /// Description
        ///</summary>
        MONTHLY_REACTIVE_MDI_ABSOLUTE_T1 = 0x00040100800601FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY REACTIVE MDI ABSOLUTE T2
        /// OBIS_CODE = 1.0.128.6.2.255
        /// Description
        ///</summary>
        MONTHLY_REACTIVE_MDI_ABSOLUTE_T2 = 0x00040100800602FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY REACTIVE MDI ABSOLUTE T3
        /// OBIS_CODE = 1.0.128.6.3.255
        /// Description
        ///</summary>
        MONTHLY_REACTIVE_MDI_ABSOLUTE_T3 = 0x00040100800603FF,
        ///<summary>
        /// Description: Quantity_Name= MONTHLY REACTIVE MDI ABSOLUTE T4
        /// OBIS_CODE = 1.0.128.6.4.255
        /// Description
        ///</summary>
        MONTHLY_REACTIVE_MDI_ABSOLUTE_T4 = 0x00040100800604FF,

        ///<summary>
        /// Description: Quantity_Name= DEMAND REACTIVE ABSOLUTE
        /// OBIS_CODE = 1.0.128.4.0.255
        /// Description
        ///</summary>
        DEMAND_REACTIVE_ABSOLUTE = 0x00050100800400FF,

        ///<summary>
        /// Description: Quantity_Name= Grid_Input_Status  Class ID 1 
        /// OBIS_CODE = 0.0.97.98.129.255
        /// Description
        /// Data Type: Array of Structures
        ///</summary>
        Grid_Input_Status = 0x00010000616281FF,

        ///<summary>
        /// Description: Quantity_Name= HDLC_Setup  Class ID 23 
        /// OBIS_CODE = 0.0.22.0.0.255
        /// Description
        /// Data Type: long unsigned
        ///</summary>
        HDLC_Setup = 0x00170000160000FF,

        ///<summary>
        /// Description: Quantity_Name= PROTOCOL_GATEWAY  Class ID 1 
        /// OBIS_CODE = 0.0.199.11.3.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        PROTOCOL_GATEWAY = 0x00010000C70B03FF,

        ///<summary>
        /// Description: Quantity_Name= CONTACTOR_FAILURE_POWER_LIMIT  Class ID 3 
        /// OBIS_CODE = 0.0.96.3.131.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CONTACTOR_FAILURE_POWER_LIMIT = 0x00030000600383FF,

        ///<summary>
        /// Description: Quantity_Name= CONTACTOR_FAILURE_MONITORING_TIME  Class ID 3 
        /// OBIS_CODE = 0.0.96.3.132.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CONTACTOR_FAILURE_MONITORING_TIME = 0x00030000600384FF,

        ///<summary>
        /// Description: Quantity_Name= GENERATOR_START_MONITORING_TIME Class ID 3 
        /// OBIS_CODE = 0.0.128.7.20.255
        /// Description
        /// Data Type: double long unsigned
        ///</summary>
        MonitoringTime_GENERATOR_START = 0x00030000800714FF,

        ///<summary>
        /// Description: Quantity_Name= TARRIF_ON_GENERATOR Class ID 1 
        /// OBIS_CODE = 0.0.128.7.21.255
        /// Description
        /// Data Type: unsigned
        ///</summary>
        TARIFF_ON_GENERATOR = 0x00010000800715FF,

        ///<summary>
        /// Description: Quantity_Name= OT_STATUS_WORD_LP2  Class ID 1 
        /// OBIS_CODE = 0.0.96.10.2.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        OT_STATUS_WORD_LP2 = 0x00010000600A02FF,

        ///<summary>
        /// Description: Quantity_Name= HDLC_PHYSICAL_ADDRESS  Class ID 1 
        /// OBIS_CODE = 0.0.199.11.2.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        HDLC_PHYSICAL_ADDRESS = 0x00010000C70B02FF,

        ///<summary>
        /// Description: Quantity_Name= LAST_BILL_PERIOD_DATETIME_2  Class ID 1 
        /// OBIS_CODE = 1.0.0.1.5.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        LAST_BILL_PERIOD_DATETIME_2 = 0x00010100000105FF,

        ///<summary>
        /// Description: Quantity_Name= CURRENT_MONTH_PW_FACT_IMPORT_T1  Class ID 3 
        /// OBIS_CODE = 1.0.13.0.1.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CURRENT_MONTH_PW_FACT_IMPORT_T1 = 0x000301000D0001FF,

        ///<summary>
        /// Description: Quantity_Name= CURRENT_MONTH_PW_FACT_IMPORT_T2  Class ID 3 
        /// OBIS_CODE = 1.0.13.0.2.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CURRENT_MONTH_PW_FACT_IMPORT_T2 = 0x000301000D0002FF,

        ///<summary>
        /// Description: Quantity_Name= CURRENT_MONTH_PW_FACT_IMPORT_T3  Class ID 3 
        /// OBIS_CODE = 1.0.13.0.3.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CURRENT_MONTH_PW_FACT_IMPORT_T3 = 0x000301000D0003FF,

        ///<summary>
        /// Description: Quantity_Name= CURRENT_MONTH_PW_FACT_IMPORT_T4  Class ID 3 
        /// OBIS_CODE = 1.0.13.0.4.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CURRENT_MONTH_PW_FACT_IMPORT_T4 = 0x000301000D0004FF,


        ///<summary>
        /// Description: Quantity_Name= CURRENT_MONTH_PW_FACT_EXPORT_TL  Class ID 3 
        /// OBIS_CODE = 1.0.84.0.0.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CURRENT_MONTH_PW_FACT_EXPORT_TL = 0x00030100540000FF,

        ///<summary>
        /// Description: Quantity_Name= CURRENT_MONTH_PW_FACT_EXPORT_T1  Class ID 3 
        /// OBIS_CODE = 1.0.84.0.1.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CURRENT_MONTH_PW_FACT_EXPORT_T1 = 0x00030100540001FF,

        ///<summary>
        /// Description: Quantity_Name= CURRENT_MONTH_PW_FACT_EXPORT_T2  Class ID 3 
        /// OBIS_CODE = 1.0.84.0.2.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CURRENT_MONTH_PW_FACT_EXPORT_T2 = 0x00030100540002FF,

        ///<summary>
        /// Description: Quantity_Name= CURRENT_MONTH_PW_FACT_EXPORT_T3  Class ID 3 
        /// OBIS_CODE = 1.0.84.0.3.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CURRENT_MONTH_PW_FACT_EXPORT_T3 = 0x00030100540003FF,

        ///<summary>
        /// Description: Quantity_Name= CURRENT_MONTH_PW_FACT_EXPORT_T4  Class ID 3 
        /// OBIS_CODE = 1.0.84.0.4.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        CURRENT_MONTH_PW_FACT_EXPORT_T4 = 0x00030100540004FF,
        ///<summary>
        /// Description: Quantity_Name= CONTROLLER_IO_STATE  Class ID 1 
        /// OBIS_CODE = 0.0.140.1.0.255
        /// Description
        /// Data Type: Array of Structures
        ///</summary>
        CONTROLLER_IO_STATE = 0x000100008C0100FF,

        ///<summary>
        /// Description: Quantity_Name= ICC_GATE  Class ID 1 
        /// OBIS_CODE = 0.0.199.11.4.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        ICC_GATE = 0x00010000C70B04FF,

        ///<summary>
        /// Description: Quantity_Name= SCHEDULE_TABLE  Class ID 10 
        /// OBIS_CODE = 0.0.12.0.0.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        SCHEDULE_TABLE = 0x000A00000C0000FF,

        ///<summary>
        /// Description: Quantity_Name= CONSUMPTION_DATA_NOW  Class ID 1
        /// OBIS_CODE = 1.0.199.3.1.255
        /// Description
        /// Data Type: Octet String 
        ///</summary>
        CONSUMPTION_DATA_NOW = 0x00010100C70301FF,

        ///<summary>
        /// Description: Quantity_Name= CONSUMPTION_DATA_MONTHLY  Class ID 1
        /// OBIS_CODE = 1.0.199.2.1.257
        /// Description
        /// Data Type: Octet String 
        ///</summary>
        CONSUMPTION_DATA_MONTHLY = 0x00010100C70201FF,

        ///<summary>
        /// Description: Quantity_Name= CONSUMPTION_DATA_WEEKLY  Class ID 1
        /// OBIS_CODE = 1.0.199.1.1.255
        /// Description
        /// Data Type: Octet String 
        ///</summary>
        CONSUMPTION_DATA_WEEKLY = 0x00010100C70101FF,

        //TODO: FAHAD ADDED OBIS CODES here In 'Get_Index' class (By Auto Generated)

        #region Baad mein add hony waly OBIS Codes

        ///<summary>  1
        /// Description: Quantity_Name= RF_CHANNELS  Class ID 1
        /// OBIS_CODE = 0.0.24.1.128.255
        /// Description = 
        /// Data Type: unsigned
        ///</summary>
        RF_CHANNELS = 0x00010000180180FF,

        ///<summary>  2
        /// Description: Quantity_Name= CHANNEL_FILTER_BW  Class ID 3
        /// OBIS_CODE = 0.0.24.1.129.255
        /// Description = 
        /// Data Type: double-long-unsigned (unit--Hz)
        ///</summary>
        CHANNEL_FILTER_BW = 0x00030000180181FF,

        ///<summary>  3
        /// Description: Quantity_Name= TRANSMIT_CARRIER_FREQUENCY  Class ID 3
        /// OBIS_CODE = 0.0.24.1.130.255
        /// Description = 
        /// Data Type: double-long-unsigned (unit--Hz)
        ///</summary>
        TRANSMIT_CARRIER_FREQUENCY = 0x00030000180182FF,

        ///<summary>  4
        /// Description: Quantity_Name= RECEIVE_CARRIER_FREQUENCY  Class ID 3
        /// OBIS_CODE = 0.0.24.1.131.255
        /// Description = 
        /// Data Type: double-long-unsigned (unit--Hz)
        ///</summary>
        RECEIVE_CARRIER_FREQUENCY = 0x00030000180183FF,

        ///<summary>  5
        /// Description: Quantity_Name= RF_BAUD_RATE  Class ID 1
        /// OBIS_CODE = 0.0.24.1.132.255
        /// Description = 
        /// Data Type: long-unsigned (in bps)
        ///</summary>
        RF_BAUD_RATE = 0x00010000180184FF,

        ///<summary>  6
        /// Description: Quantity_Name= RF_POWER  Class ID 3
        /// OBIS_CODE = 0.0.24.1.133.255
        /// Description = 
        /// Data Type: unsigned (unit -- dbm)
        ///</summary>
        RF_POWER = 0x00030000180185FF,

        ///<summary>  7
        /// Description: Quantity_Name= PACKET_MODE  Class ID 1
        /// OBIS_CODE = 0.0.24.1.134.255
        /// Description = 0-Packet mode ,  1-Continuous mode
        /// Data Type: Enum
        ///</summary>
        PACKET_MODE = 0x00010000180186FF,

        ///<summary>  8
        /// Description: Quantity_Name= PACKET_FORMAT  Class ID 1
        /// OBIS_CODE = 0.0.24.1.135.255
        /// Description = 0-Fixed length ,  1-Variable length (only readable)
        /// Data Type: Enum
        ///</summary>
        PACKET_FORMAT = 0x00010000180187FF,

        ///<summary>  9
        /// Description: Quantity_Name= PACKET_LENGTH  Class ID 1
        /// OBIS_CODE = 0.0.24.1.136.255
        /// Description = 
        /// Data Type: Unsigned
        ///</summary>
        PACKET_LENGTH = 0x00010000180188FF,

        ///<summary>  10
        /// Description: Quantity_Name= MODULATION_TYPE  Class ID 1
        /// OBIS_CODE = 0.0.24.1.137.255
        /// Description = 
        /// Data Type: Enum
        ///</summary>
        MODULATION_TYPE = 0x00010000180189FF,

        ///<summary>  11
        /// Description: Quantity_Name= FREQUENCY_DEVIATION  Class ID 3
        /// OBIS_CODE = 0.0.24.1.138.255
        /// Description = 
        /// Data Type: double-long-unisgned (unit -- Hz)
        ///</summary>
        FREQUENCY_DEVIATION = 0x0003000018018AFF,

        ///<summary>  12
        /// Description: Quantity_Name= RECEIVER_BANDWIDTH  Class ID 3
        /// OBIS_CODE = 0.0.24.1.139.255
        /// Description = 
        /// Data Type: double-long-unsigned (unit -- Hz)
        ///</summary>
        RECEIVER_BANDWIDTH = 0x0003000018018BFF,

        ///<summary>  13
        /// Description: Quantity_Name= PREAMBLE  Class ID 1
        /// OBIS_CODE = 0.0.24.1.140.255
        /// Description = 
        /// Data Type: Unsigned
        ///</summary>
        PREAMBLE = 0x0001000018018CFF,

        ///<summary>  14
        /// Description: Quantity_Name= SYNC_WORD  Class ID 1
        /// OBIS_CODE = 0.0.24.1.141.255
        /// Description = 
        /// Data Type: long-unsigned
        ///</summary>
        SYNC_WORD = 0x0001000018018DFF,

        ///<summary>  15
        /// Description: Quantity_Name= DATA_WHITENING  Class ID 1
        /// OBIS_CODE = 0.0.24.1.142.255
        /// Description = 
        /// Data Type: Boolean
        ///</summary>
        DATA_WHITENING = 0x0001000018018EFF,

        ///<summary>  16
        /// Description: Quantity_Name= PACKET_ENCODING  Class ID 1
        /// OBIS_CODE = 0.0.24.1.143.255
        /// Description = 
        /// Data Type: Boolean
        ///</summary>
        PACKET_ENCODING = 0x0001000018018FFF,

        ///<summary>  17
        /// Description: Quantity_Name= ADDRESS_FILTERING  Class ID 1
        /// OBIS_CODE = 0.0.24.1.144.255
        /// Description = 
        /// Data Type: Boolean
        ///</summary>
        ADDRESS_FILTERING = 0x00010000180190FF,

        ///<summary>  18
        /// Description: Quantity_Name= NODE_ADDRESS  Class ID 1
        /// OBIS_CODE = 0.0.24.1.145.255
        /// Description = 
        /// Data Type: Unsigned
        ///</summary>
        NODE_ADDRESS = 0x00010000180191FF,

        ///<summary>  19
        /// Description: Quantity_Name= BROADCAST_ADDRESS  Class ID 1
        /// OBIS_CODE = 0.0.24.1.146.255
        /// Description = 
        /// Data Type: Unisgned
        ///</summary>
        BROADCAST_ADDRESS = 0x00010000180192FF,

        ///<summary>  20
        /// Description: Quantity_Name= AES_ENCRYPTION  Class ID 1
        /// OBIS_CODE = 0.0.24.1.147.255
        /// Description = 
        /// Data Type: Boolean
        ///</summary>
        AES_ENCRYPTION = 0x00010000180193FF,

        ///<summary>  21
        /// Description: Quantity_Name= AES_ENCRYPTION_(KEY_SIZE)  Class ID 1
        /// OBIS_CODE = 0.0.24.1.148.255
        /// Description = 
        /// Data Type: Octet string
        ///</summary>
        AES_ENCRYPTION_KEY_SIZE = 0x00010000180194FF,

        ///<summary>  22
        /// Description: Quantity_Name= RF_COMMAND_DELAY  Class ID 3
        /// OBIS_CODE = 0.0.24.1.149.255
        /// Description = 
        /// Data Type: Unsigned (unit -sec)
        ///</summary>
        RF_COMMAND_DELAY = 0x00030000180195FF,

        /////<summary>  24
        ///// Description: Quantity_Name= SERIAL_NUMBER  Class ID 1
        ///// OBIS_CODE = 0.0.96.1.0.255
        ///// Description = Same as fusion (Only read able)
        ///// Data Type: Double-long-unsigned
        /////</summary>
        //SERIAL_NUMBER = 0x00010000600100FF,

        ///<summary>  25
        /// Description: Quantity_Name= LCD_CONTRAST  Class ID 1
        /// OBIS_CODE = 0.0.21.0.4.255
        /// Description = Display Parameter
        /// Data Type: long-unsigned
        ///</summary>
        LCD_CONTRAST = 0x00010000150004FF,

        ///<summary>  27
        /// Description: Quantity_Name= USB_PARAMETERS  Class ID 1
        /// OBIS_CODE = 0.0.20.128.0.255
        /// Description = USB read mode setting GSM log/VLCD/Disable/SMT
        /// Data Type: Enum
        ///</summary>
        USB_PARAMETERS = 0x00010000148000FF,

        ///<summary>  28
        /// Description: Quantity_Name= COST_PARAMETERS  Class ID 3
        /// OBIS_CODE = 0.0.19.128.0.255
        /// Description = For Display.Cost of electricity. Can be done via Prepayment classes but avoided because of complexity.
        /// Data Type: long-unsigned
        ///</summary>
        COST_PARAMETERS = 0x00030000138000FF,

        ///<summary>  29
        /// Description: Quantity_Name= METER_TO_READ  Class ID 1
        /// OBIS_CODE = 0.0.96.1.3.255
        /// Description = MSN of meter to read. Similar to M-Bus client object attribute 6. But m-bus clients not used.
        /// Data Type: Octet string
        ///</summary>
        METER_TO_READ = 0x00010000600103FF,

        ///<summary>  30
        /// Description: Quantity_Name= METER_PASSWORD  Class ID 1
        /// OBIS_CODE = 0.0.96.128.3.255
        /// Description = Password of meter to read
        /// Data Type: Octet string
        ///</summary>
        METER_PASSWORD = 0x00010000608003FF,

        ///<summary>  31
        /// Description: Quantity_Name= DATA_TO_READ  Class ID 1
        /// OBIS_CODE = 0.0.44.1.128.255
        /// Description = Data to read from MTU via RF
        /// Data Type: Bit string (with each for each packet type supported)
        ///</summary>
        DATA_TO_READ = 0x000100002C0180FF,

        ///<summary>  33
        /// Description: Quantity_Name= BUZZER_SETTINGS  Class ID 1
        /// OBIS_CODE = 0.0.96.3.2.255
        /// Description = Enable /disable buzzer
        /// Data Type: Boolean
        ///</summary>
        BUZZER_SETTINGS = 0x00010000600302FF,

        /////<summary>  34
        ///// Description: Quantity_Name= READ_HUMIDITY_SETTING  Class ID 1
        ///// OBIS_CODE = 0.0.96.3.1.255
        ///// Description = 
        ///// Data Type: long-unsigned
        /////</summary>
        //READ_HUMIDITY_SETTING = 0x00010000600301FF,

        ///<summary>  35
        /// Description: Quantity_Name= HUMIDITY  Class ID 1
        /// OBIS_CODE = 0.0.96.3.1.255
        /// Description = Enable /disable humidity read
        /// Data Type: Double long (tag 5)
        ///</summary>
        HUMIDITY = 0x00010000600301FF,

        ///<summary>  36
        /// Description: Quantity_Name= TEMPERATURE_SETTING  Class ID 3
        /// OBIS_CODE = 0.0.96.9.0.255
        /// Description = Temperature Scale Setting
        /// Data Type: Enum
        ///</summary>
        TEMPERATURE_SETTING = 0x00030000600900FF,

        ///<summary>  37
        /// Description: Quantity_Name= WIFI_SSID  Class ID 1
        /// OBIS_CODE = 0.0.25.2.128.255
        /// Description = Structure {SSID,username,password}
        /// Data Type: Structure {SSID--> octet string,username-->octet string,password --> octet string}
        ///</summary>
        WIFI_SSID = 0x00010000190280FF,

        /////<summary>  38
        ///// Description: Quantity_Name= WIFI_USERNAME  Class ID 1
        ///// OBIS_CODE = 0.0.25.2.128.255
        ///// Description = 
        ///// Data Type: 
        /////</summary>
        //WIFI_USERNAME = 0x00010000190280FF,

        /////<summary>  39
        ///// Description: Quantity_Name= WIFI_PASSWORD  Class ID 1
        ///// OBIS_CODE = 0.0.25.2.128.255
        ///// Description = 
        ///// Data Type: 
        /////</summary>
        //WIFI_PASSWORD = 0x00010000190280FF,

        ///<summary>  40
        /// Description: Quantity_Name= SETTINGS_DEFAULT_WIFI__MODE  Class ID 1
        /// OBIS_CODE = 0.0.25.2.129.255
        /// Description = 
        /// Data Type: Enum
        ///</summary>
        SETTINGS_DEFAULT_WIFI_MODE = 0x00010000190281FF,

        /////<summary>  41
        ///// Description: Quantity_Name= SETTINGS_WIFI_DHCP  Class ID 42
        ///// OBIS_CODE = 0.0.25.1.0.255
        ///// Description = 
        ///// Data Type: 
        /////</summary>
        //SETTINGS_WIFI_DHCP = 0x002A0000190100FF,

        /////<summary>  42
        ///// Description: Quantity_Name= SETTINGS_IP__(STATION_OR_AP)  Class ID 42
        ///// OBIS_CODE = 0.0.25.1.0.255
        ///// Description = When DHCP is enabled this parameter is read only. If DHCP is disabled this parameter is writeable and static.
        ///// Data Type: 
        /////</summary>
        //SETTINGS_IP_STATION_OR_AP = 0x002A0000190100FF,

        /////<summary>  43
        ///// Description: Quantity_Name= SETTINGS_IP_GATEWAY_(STATION_OR_AP)  Class ID 42
        ///// OBIS_CODE = 0.0.25.1.0.255
        ///// Description = 
        ///// Data Type: 
        /////</summary>
        //SETTINGS_IP_GATEWAY_STATION_OR_AP = 0x002A0000190100FF,

        /////<summary>  44
        ///// Description: Quantity_Name= SETTINGS_IP_NETWORK_MASK_(STATION_OR_AP)  Class ID 42
        ///// OBIS_CODE = 0.0.25.1.0.255
        ///// Description = 
        ///// Data Type: 
        /////</summary>
        //SETTINGS_IP_NETWORK_MASK_STATION_OR_AP = 0x002A0000190100FF,

        ///<summary>  45
        /// Description: Quantity_Name= AUTO_CONNECT_OBJECT_{IP_+_PORT}  Class ID 29
        /// OBIS_CODE = 0.0.2.1.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        //AUTO_CONNECT_OBJECT_IP_PORT = 0x001D0000020100FF,

        ///<summary>  46
        /// Description: Quantity_Name= KEEPALIVE_PARAMS  Class ID 1
        /// OBIS_CODE = 0.0.25.167.1.255
        /// Description = 
        /// Data Type: Structure
        ///</summary>
        KEEPALIVE_PARAMS = 0x0001000019A701FF,

        ///<summary>  47
        /// Description: Quantity_Name= MODEM_LIMITS_AND_TIME  Class ID 1
        /// OBIS_CODE = 0.0.25.170.2.255
        /// Description = 
        /// Data Type: Structure
        ///</summary>
        MODEM_LIMITS_AND_TIME = 0x0001000019AA02FF,

        ///<summary>  48
        /// Description: Quantity_Name= WIFI_MODEM_MODE  Class ID 1
        /// OBIS_CODE = 0.0.25.2.130.255
        /// Description = 
        /// Data Type: Enum {0-TCP client, 1-TCP server and 2- Web server}
        ///</summary>
        WIFI_MODEM_MODE = 0x00010000190282FF,

        /////<summary>  49
        ///// Description: Quantity_Name= WIFI__IP_(CLIENT,_TCP_SERVER_OR_WEB_SERVER)  Class ID 42
        ///// OBIS_CODE = 0.0.25.1.0.255
        ///// Description = TCP SERVER IP
        ///// Data Type: 
        /////</summary>
        //WIFI_IP_CLIENT_TCP_SERVER_OR_WEB_SERVER = 0x002A0000190100FF,

        ///<summary>  50
        /// Description: Quantity_Name= WIFI_SERVER_PORT  Class ID 41
        /// OBIS_CODE = 0.0.25.0.0.255
        /// Description = TCP SERVER Port(Different from web server)
        /// Data Type: 
        ///</summary>
        //WIFI_SERVER_PORT = 0x00290000190000FF,

        ///<summary>  51
        /// Description: Quantity_Name= WIFI_WEB_SERVER_PORT  Class ID 1
        /// OBIS_CODE = 0.0.25.2.131.255
        /// Description = IP of  server in WiFi Basic Configuration
        /// Data Type: long-unsigned
        ///</summary>
        WIFI_WEB_SERVER_PORT = 0x00010000190283FF,

        ///<summary>  52
        /// Description: Quantity_Name= RF_COMMAND_TIMEOUT  Class ID 3
        /// OBIS_CODE = 0.0.24.1.150.255
        /// Description = 
        /// Data Type: unsigned (unit -sec)
        ///</summary>
        RF_COMMAND_TIMEOUT = 0x00030000180196FF,

        ///<summary>  53
        /// Description: Quantity_Name= TEMPERATURE  Class ID 3
        /// OBIS_CODE = 0.0.96.9.2.255
        /// Description =
        /// Data Type: long-unsigned (unit--Temperature)
        ///</summary>
        TEMPERATURE = 0x00030000600902FF,

        #endregion Baad mein add hony waly OBIS Codes

        #region Al-Buraq OBIS Codes

        #region --001 CB_MDI_ACTIVE_POS_TL[1-8] --

        //**************//Index[1][0] Match OBIS => 0x00030100010200FF 

        //**************//Index[1][1] Match OBIS => 0x00030100010201FF 

        //**************//Index[1][2] Match OBIS => 0x00030100010202FF 

        //**************//Index[1][3] Match OBIS => 0x00030100010203FF 

        //**************//Index[1][4] Match OBIS => 0x00030100010204FF 

        ///<summary>  param[1] -> value[5]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_POS_T5  Class ID 3
        /// OBIS_CODE = 1.0.1.2.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_POS_T5 = 0x00030100010205FF,

        ///<summary>  param[1] -> value[6]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_POS_T6  Class ID 3
        /// OBIS_CODE = 1.0.1.2.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_POS_T6 = 0x00030100010206FF,

        ///<summary>  param[1] -> value[7]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_POS_T7  Class ID 3
        /// OBIS_CODE = 1.0.1.2.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_POS_T7 = 0x00030100010207FF,

        ///<summary>  param[1] -> value[8]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_POS_T8  Class ID 3
        /// OBIS_CODE = 1.0.1.2.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_POS_T8 = 0x00030100010208FF,

        #endregion // --001  CB_MDI_ACTIVE_POS_TL[1-8] --

        //===============================================================================

        #region --002 CB_ACTIVE_ENERGY_POS_TL[1-8] --

        //**************//Index[2][0] Match OBIS => 0x00030100010800FF 

        //**************//Index[2][1] Match OBIS => 0x00030100010801FF 

        //**************//Index[2][2] Match OBIS => 0x00030100010802FF 

        //**************//Index[2][3] Match OBIS => 0x00030100010803FF 

        //**************//Index[2][4] Match OBIS => 0x00030100010804FF 

        ///<summary>  param[2] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_T5  Class ID 3
        /// OBIS_CODE = 1.0.1.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_T5 = 0x00030100010805FF,

        ///<summary>  param[2] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_T6  Class ID 3
        /// OBIS_CODE = 1.0.1.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_T6 = 0x00030100010806FF,

        ///<summary>  param[2] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_T7  Class ID 3
        /// OBIS_CODE = 1.0.1.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_T7 = 0x00030100010807FF,

        ///<summary>  param[2] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_T8  Class ID 3
        /// OBIS_CODE = 1.0.1.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_T8 = 0x00030100010808FF,

        #endregion // --002  CB_ACTIVE_ENERGY_POS_TL[1-8] --

        //===============================================================================

        #region --003 CB_MDI_ACTIVE_NEG_TL[1-8] --

        //**************//Index[3][0] Match OBIS => 0x00030100020200FF 

        //**************//Index[3][1] Match OBIS => 0x00030100020201FF 

        //**************//Index[3][2] Match OBIS => 0x00030100020202FF 

        //**************//Index[3][3] Match OBIS => 0x00030100020203FF 

        //**************//Index[3][4] Match OBIS => 0x00030100020204FF 

        ///<summary>  param[3] -> value[5]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_NEG_T5  Class ID 3
        /// OBIS_CODE = 1.0.2.2.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_NEG_T5 = 0x00030100020205FF,

        ///<summary>  param[3] -> value[6]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_NEG_T6  Class ID 3
        /// OBIS_CODE = 1.0.2.2.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_NEG_T6 = 0x00030100020206FF,

        ///<summary>  param[3] -> value[7]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_NEG_T7  Class ID 3
        /// OBIS_CODE = 1.0.2.2.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_NEG_T7 = 0x00030100020207FF,

        ///<summary>  param[3] -> value[8]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_NEG_T8  Class ID 3
        /// OBIS_CODE = 1.0.2.2.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_NEG_T8 = 0x00030100020208FF,

        #endregion // --003  CB_MDI_ACTIVE_NEG_TL[1-8] --

        //===============================================================================

        #region --004 CB_ACTIVE_ENERGY_NEG_TL[1-8] --

        //**************//Index[4][0] Match OBIS => 0x00030100020800FF 

        //**************//Index[4][1] Match OBIS => 0x00030100020801FF 

        //**************//Index[4][2] Match OBIS => 0x00030100020802FF 

        //**************//Index[4][3] Match OBIS => 0x00030100020803FF 

        //**************//Index[4][4] Match OBIS => 0x00030100020804FF 

        ///<summary>  param[4] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_T5  Class ID 3
        /// OBIS_CODE = 1.0.2.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_T5 = 0x00030100020805FF,

        ///<summary>  param[4] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_T6  Class ID 3
        /// OBIS_CODE = 1.0.2.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_T6 = 0x00030100020806FF,

        ///<summary>  param[4] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_T7  Class ID 3
        /// OBIS_CODE = 1.0.2.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_T7 = 0x00030100020807FF,

        ///<summary>  param[4] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_T8  Class ID 3
        /// OBIS_CODE = 1.0.2.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_T8 = 0x00030100020808FF,

        #endregion // --004  CB_ACTIVE_ENERGY_NEG_TL[1-8] --

        //===============================================================================

        #region --005 CB_MDI_REACTIVE_POS_TL[1-8] --

        //**************//Index[5][0] Match OBIS => 0x00030100030200FF 

        //**************//Index[5][1] Match OBIS => 0x00030100030201FF 

        //**************//Index[5][2] Match OBIS => 0x00030100030202FF 

        //**************//Index[5][3] Match OBIS => 0x00030100030203FF 

        //**************//Index[5][4] Match OBIS => 0x00030100030204FF 

        ///<summary>  param[5] -> value[5]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_POS_T5  Class ID 3
        /// OBIS_CODE = 1.0.3.2.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_POS_T5 = 0x00030100030205FF,

        ///<summary>  param[5] -> value[6]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_POS_T6  Class ID 3
        /// OBIS_CODE = 1.0.3.2.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_POS_T6 = 0x00030100030206FF,

        ///<summary>  param[5] -> value[7]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_POS_T7  Class ID 3
        /// OBIS_CODE = 1.0.3.2.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_POS_T7 = 0x00030100030207FF,

        ///<summary>  param[5] -> value[8]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_POS_T8  Class ID 3
        /// OBIS_CODE = 1.0.3.2.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_POS_T8 = 0x00030100030208FF,

        #endregion // --005  CB_MDI_REACTIVE_POS_TL[1-8] --

        //===============================================================================

        #region --006 CB_REACTIVE_ENERGY_POS_TL[1-8] --

        //**************//Index[6][0] Match OBIS => 0x00030100030800FF 

        //**************//Index[6][1] Match OBIS => 0x00030100030801FF 

        //**************//Index[6][2] Match OBIS => 0x00030100030802FF 

        //**************//Index[6][3] Match OBIS => 0x00030100030803FF 

        //**************//Index[6][4] Match OBIS => 0x00030100030804FF 

        ///<summary>  param[6] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_T5  Class ID 3
        /// OBIS_CODE = 1.0.3.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_T5 = 0x00030100030805FF,

        ///<summary>  param[6] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_T6  Class ID 3
        /// OBIS_CODE = 1.0.3.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_T6 = 0x00030100030806FF,

        ///<summary>  param[6] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_T7  Class ID 3
        /// OBIS_CODE = 1.0.3.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_T7 = 0x00030100030807FF,

        ///<summary>  param[6] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_T8  Class ID 3
        /// OBIS_CODE = 1.0.3.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_T8 = 0x00030100030808FF,

        #endregion // --006  CB_REACTIVE_ENERGY_POS_TL[1-8] --

        //===============================================================================

        #region --007 CB_MDI_REACTIVE_NEG_TL[1-8] --

        //**************//Index[7][0] Match OBIS => 0x00030100040200FF 

        //**************//Index[7][1] Match OBIS => 0x00030100040201FF 

        //**************//Index[7][2] Match OBIS => 0x00030100040202FF 

        //**************//Index[7][3] Match OBIS => 0x00030100040203FF 

        //**************//Index[7][4] Match OBIS => 0x00030100040204FF 

        ///<summary>  param[7] -> value[5]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_NEG_T5  Class ID 3
        /// OBIS_CODE = 1.0.4.2.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_NEG_T5 = 0x00030100040205FF,

        ///<summary>  param[7] -> value[6]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_NEG_T6  Class ID 3
        /// OBIS_CODE = 1.0.4.2.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_NEG_T6 = 0x00030100040206FF,

        ///<summary>  param[7] -> value[7]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_NEG_T7  Class ID 3
        /// OBIS_CODE = 1.0.4.2.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_NEG_T7 = 0x00030100040207FF,

        ///<summary>  param[7] -> value[8]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_NEG_T8  Class ID 3
        /// OBIS_CODE = 1.0.4.2.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_NEG_T8 = 0x00030100040208FF,

        #endregion // --007  CB_MDI_REACTIVE_NEG_TL[1-8] --

        //===============================================================================

        #region --008 CB_REACTIVE_ENERGY_NEG_TL[1-8] --

        //**************//Index[8][0] Match OBIS => 0x00030100040800FF 

        //**************//Index[8][1] Match OBIS => 0x00030100040801FF 

        //**************//Index[8][2] Match OBIS => 0x00030100040802FF 

        //**************//Index[8][3] Match OBIS => 0x00030100040803FF 

        //**************//Index[8][4] Match OBIS => 0x00030100040804FF 

        ///<summary>  param[8] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_T5  Class ID 3
        /// OBIS_CODE = 1.0.4.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_T5 = 0x00030100040805FF,

        ///<summary>  param[8] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_T6  Class ID 3
        /// OBIS_CODE = 1.0.4.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_T6 = 0x00030100040806FF,

        ///<summary>  param[8] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_T7  Class ID 3
        /// OBIS_CODE = 1.0.4.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_T7 = 0x00030100040807FF,

        ///<summary>  param[8] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_T8  Class ID 3
        /// OBIS_CODE = 1.0.4.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_T8 = 0x00030100040808FF,

        #endregion // --008  CB_REACTIVE_ENERGY_NEG_TL[1-8] --

        //===============================================================================

        #region --009 CB_APPARENT_ENERGY_POS_TL[1-8] --

        //**************//Index[9][0] Match OBIS => 0x00030100090800FF 

        //**************//Index[9][1] Match OBIS => 0x00030100090801FF 

        //**************//Index[9][2] Match OBIS => 0x00030100090802FF 

        //**************//Index[9][3] Match OBIS => 0x00030100090803FF 

        //**************//Index[9][4] Match OBIS => 0x00030100090804FF 

        ///<summary>  param[9] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_T5  Class ID 3
        /// OBIS_CODE = 1.0.9.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_T5 = 0x00030100090805FF,

        ///<summary>  param[9] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_T6  Class ID 3
        /// OBIS_CODE = 1.0.9.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_T6 = 0x00030100090806FF,

        ///<summary>  param[9] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_T7  Class ID 3
        /// OBIS_CODE = 1.0.9.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_T7 = 0x00030100090807FF,

        ///<summary>  param[9] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_T8  Class ID 3
        /// OBIS_CODE = 1.0.9.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_T8 = 0x00030100090808FF,

        #endregion // --009  CB_APPARENT_ENERGY_POS_TL[1-8] --

        //===============================================================================

        #region --010 CB_APPARENT_ENERGY_NEG_TL[1-8] --

        ///<summary>  param[10] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_TL  Class ID 3
        /// OBIS_CODE = 1.0.10.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_TL = 0x000301000A0800FF,

        ///<summary>  param[10] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_T1  Class ID 3
        /// OBIS_CODE = 1.0.10.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_T1 = 0x000301000A0801FF,

        ///<summary>  param[10] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_T2  Class ID 3
        /// OBIS_CODE = 1.0.10.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_T2 = 0x000301000A0802FF,

        ///<summary>  param[10] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_T3  Class ID 3
        /// OBIS_CODE = 1.0.10.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_T3 = 0x000301000A0803FF,

        ///<summary>  param[10] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_T4  Class ID 3
        /// OBIS_CODE = 1.0.10.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_T4 = 0x000301000A0804FF,

        ///<summary>  param[10] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_T5  Class ID 3
        /// OBIS_CODE = 1.0.10.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_T5 = 0x000301000A0805FF,

        ///<summary>  param[10] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_T6  Class ID 3
        /// OBIS_CODE = 1.0.10.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_T6 = 0x000301000A0806FF,

        ///<summary>  param[10] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_T7  Class ID 3
        /// OBIS_CODE = 1.0.10.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_T7 = 0x000301000A0807FF,

        ///<summary>  param[10] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_T8  Class ID 3
        /// OBIS_CODE = 1.0.10.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_T8 = 0x000301000A0808FF,

        #endregion // --010  CB_APPARENT_ENERGY_NEG_TL[1-8] --

        //===============================================================================

        #region --011 CB_MDI_ACTIVE_ABS_TL[1-8] --

        //**************//Index[11][0] Match OBIS => 0x000301000F0200FF 

        //**************//Index[11][1] Match OBIS => 0x000301000F0201FF 

        //**************//Index[11][2] Match OBIS => 0x000301000F0202FF 

        //**************//Index[11][3] Match OBIS => 0x000301000F0203FF 

        //**************//Index[11][4] Match OBIS => 0x000301000F0204FF 

        ///<summary>  param[11] -> value[5]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_ABS_T5  Class ID 3
        /// OBIS_CODE = 1.0.15.2.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_ABS_T5 = 0x000301000F0205FF,

        ///<summary>  param[11] -> value[6]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_ABS_T6  Class ID 3
        /// OBIS_CODE = 1.0.15.2.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_ABS_T6 = 0x000301000F0206FF,

        ///<summary>  param[11] -> value[7]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_ABS_T7  Class ID 3
        /// OBIS_CODE = 1.0.15.2.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_ABS_T7 = 0x000301000F0207FF,

        ///<summary>  param[11] -> value[8]
        /// Description: Quantity_Name= CB_MDI_ACTIVE_ABS_T8  Class ID 3
        /// OBIS_CODE = 1.0.15.2.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_ACTIVE_ABS_T8 = 0x000301000F0208FF,

        #endregion // --011  CB_MDI_ACTIVE_ABS_TL[1-8] --

        //===============================================================================

        #region --012 CB_ACTIVE_ENERGY_ABS_TL[1-8] --

        //**************//Index[12][0] Match OBIS => 0x000301000F0800FF 

        //**************//Index[12][1] Match OBIS => 0x000301000F0801FF 

        //**************//Index[12][2] Match OBIS => 0x000301000F0802FF 

        //**************//Index[12][3] Match OBIS => 0x000301000F0803FF 

        //**************//Index[12][4] Match OBIS => 0x000301000F0804FF 

        ///<summary>  param[12] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_T5  Class ID 3
        /// OBIS_CODE = 1.0.15.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_T5 = 0x000301000F0805FF,

        ///<summary>  param[12] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_T6  Class ID 3
        /// OBIS_CODE = 1.0.15.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_T6 = 0x000301000F0806FF,

        ///<summary>  param[12] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_T7  Class ID 3
        /// OBIS_CODE = 1.0.15.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_T7 = 0x000301000F0807FF,

        ///<summary>  param[12] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_T8  Class ID 3
        /// OBIS_CODE = 1.0.15.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_T8 = 0x000301000F0808FF,

        #endregion // --012  CB_ACTIVE_ENERGY_ABS_TL[1-8] --

        //===============================================================================

        #region --013 CB_ACTIVE_ENERGY_POS_P1_TL[1-8] --

        ///<summary>  param[13] -> value[TOTAL]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.21.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P1_TL = 0x00030100150800FF,

        ///<summary>  param[13] -> value[1]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.21.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P1_T1 = 0x00030100150801FF,

        ///<summary>  param[13] -> value[2]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.21.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P1_T2 = 0x00030100150802FF,

        ///<summary>  param[13] -> value[3]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.21.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P1_T3 = 0x00030100150803FF,

        ///<summary>  param[13] -> value[4]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.21.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P1_T4 = 0x00030100150804FF,

        ///<summary>  param[13] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.21.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P1_T5 = 0x00030100150805FF,

        ///<summary>  param[13] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.21.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P1_T6 = 0x00030100150806FF,

        ///<summary>  param[13] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.21.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P1_T7 = 0x00030100150807FF,

        ///<summary>  param[13] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.21.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P1_T8 = 0x00030100150808FF,

        #endregion // --013  CB_ACTIVE_ENERGY_POS_P1_TL[1-8] --

        //===============================================================================

        #region --014 CB_ACTIVE_ENERGY_NEG_P1_TL[1-8] --

        ///<summary>  param[14] -> value[TOTAL]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.22.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P1_TL = 0x00030100160800FF,

        ///<summary>  param[14] -> value[1]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.22.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P1_T1 = 0x00030100160801FF,

        ///<summary>  param[14] -> value[2]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.22.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P1_T2 = 0x00030100160802FF,

        ///<summary>  param[14] -> value[3]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.22.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P1_T3 = 0x00030100160803FF,

        ///<summary>  param[14] -> value[4]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.22.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P1_T4 = 0x00030100160804FF,

        ///<summary>  param[14] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.22.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P1_T5 = 0x00030100160805FF,

        ///<summary>  param[14] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.22.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P1_T6 = 0x00030100160806FF,

        ///<summary>  param[14] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.22.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P1_T7 = 0x00030100160807FF,

        ///<summary>  param[14] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.22.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P1_T8 = 0x00030100160808FF,

        #endregion // --014  CB_ACTIVE_ENERGY_NEG_P1_TL[1-8] --

        //===============================================================================

        #region --015 CB_REACTIVE_ENERGY_POS_P1_TL[1-8] --

        ///<summary>  param[15] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.23.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P1_TL = 0x00030100170800FF,

        ///<summary>  param[15] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.23.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P1_T1 = 0x00030100170801FF,

        ///<summary>  param[15] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.23.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P1_T2 = 0x00030100170802FF,

        ///<summary>  param[15] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.23.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P1_T3 = 0x00030100170803FF,

        ///<summary>  param[15] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.23.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P1_T4 = 0x00030100170804FF,

        ///<summary>  param[15] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.23.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P1_T5 = 0x00030100170805FF,

        ///<summary>  param[15] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.23.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P1_T6 = 0x00030100170806FF,

        ///<summary>  param[15] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.23.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P1_T7 = 0x00030100170807FF,

        ///<summary>  param[15] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.23.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P1_T8 = 0x00030100170808FF,

        #endregion // --015  CB_REACTIVE_ENERGY_POS_P1_TL[1-8] --

        //===============================================================================

        #region --016 CB_REACTIVE_ENERGY_Q1_P1_TL[1-8] --

        ///<summary>  param[16] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.25.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P1_TL = 0x00030100190800FF,

        ///<summary>  param[16] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.25.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P1_T1 = 0x00030100190801FF,

        ///<summary>  param[16] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.25.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P1_T2 = 0x00030100190802FF,

        ///<summary>  param[16] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.25.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P1_T3 = 0x00030100190803FF,

        ///<summary>  param[16] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.25.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P1_T4 = 0x00030100190804FF,

        ///<summary>  param[16] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.25.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P1_T5 = 0x00030100190805FF,

        ///<summary>  param[16] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.25.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P1_T6 = 0x00030100190806FF,

        ///<summary>  param[16] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.25.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P1_T7 = 0x00030100190807FF,

        ///<summary>  param[16] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.25.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P1_T8 = 0x00030100190808FF,

        #endregion // --016  CB_REACTIVE_ENERGY_Q1_P1_TL[1-8] --

        //===============================================================================

        #region --017 CB_REACTIVE_ENERGY_Q2_P1_TL[1-8] --

        ///<summary>  param[17] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.26.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P1_TL = 0x000301001A0800FF,

        ///<summary>  param[17] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.26.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P1_T1 = 0x000301001A0801FF,

        ///<summary>  param[17] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.26.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P1_T2 = 0x000301001A0802FF,

        ///<summary>  param[17] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.26.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P1_T3 = 0x000301001A0803FF,

        ///<summary>  param[17] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.26.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P1_T4 = 0x000301001A0804FF,

        ///<summary>  param[17] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.26.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P1_T5 = 0x000301001A0805FF,

        ///<summary>  param[17] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.26.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P1_T6 = 0x000301001A0806FF,

        ///<summary>  param[17] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.26.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P1_T7 = 0x000301001A0807FF,

        ///<summary>  param[17] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.26.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P1_T8 = 0x000301001A0808FF,

        #endregion // --017  CB_REACTIVE_ENERGY_Q2_P1_TL[1-8] --

        //===============================================================================

        #region --018 CB_REACTIVE_ENERGY_Q3_P1_TL[1-8] --

        ///<summary>  param[18] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.27.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P1_TL = 0x000301001B0800FF,

        ///<summary>  param[18] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.27.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P1_T1 = 0x000301001B0801FF,

        ///<summary>  param[18] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.27.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P1_T2 = 0x000301001B0802FF,

        ///<summary>  param[18] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.27.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P1_T3 = 0x000301001B0803FF,

        ///<summary>  param[18] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.27.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P1_T4 = 0x000301001B0804FF,

        ///<summary>  param[18] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.27.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P1_T5 = 0x000301001B0805FF,

        ///<summary>  param[18] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.27.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P1_T6 = 0x000301001B0806FF,

        ///<summary>  param[18] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.27.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P1_T7 = 0x000301001B0807FF,

        ///<summary>  param[18] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.27.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P1_T8 = 0x000301001B0808FF,

        #endregion // --018  CB_REACTIVE_ENERGY_Q3_P1_TL[1-8] --

        //===============================================================================

        #region --019 CB_REACTIVE_ENERGY_Q4_P1_TL[1-8] --

        ///<summary>  param[19] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.28.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P1_TL = 0x000301001C0800FF,

        ///<summary>  param[19] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.28.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P1_T1 = 0x000301001C0801FF,

        ///<summary>  param[19] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.28.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P1_T2 = 0x000301001C0802FF,

        ///<summary>  param[19] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.28.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P1_T3 = 0x000301001C0803FF,

        ///<summary>  param[19] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.28.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P1_T4 = 0x000301001C0804FF,

        ///<summary>  param[19] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.28.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P1_T5 = 0x000301001C0805FF,

        ///<summary>  param[19] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.28.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P1_T6 = 0x000301001C0806FF,

        ///<summary>  param[19] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.28.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P1_T7 = 0x000301001C0807FF,

        ///<summary>  param[19] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.28.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P1_T8 = 0x000301001C0808FF,

        #endregion // --019  CB_REACTIVE_ENERGY_Q4_P1_TL[1-8] --

        //===============================================================================

        #region --020 CB_APPARENT_ENERGY_POS_P1_TL[1-8] --

        ///<summary>  param[20] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.29.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P1_TL = 0x000301001D0800FF,

        ///<summary>  param[20] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.29.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P1_T1 = 0x000301001D0801FF,

        ///<summary>  param[20] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.29.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P1_T2 = 0x000301001D0802FF,

        ///<summary>  param[20] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.29.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P1_T3 = 0x000301001D0803FF,

        ///<summary>  param[20] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.29.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P1_T4 = 0x000301001D0804FF,

        ///<summary>  param[20] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.29.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P1_T5 = 0x000301001D0805FF,

        ///<summary>  param[20] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.29.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P1_T6 = 0x000301001D0806FF,

        ///<summary>  param[20] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.29.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P1_T7 = 0x000301001D0807FF,

        ///<summary>  param[20] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.29.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P1_T8 = 0x000301001D0808FF,

        #endregion // --020  CB_APPARENT_ENERGY_POS_P1_TL[1-8] --

        //===============================================================================

        #region --021 CB_APPARENT_ENERGY_NEG_P1_TL[1-8] --

        ///<summary>  param[21] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.30.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P1_TL = 0x000301001E0800FF,

        ///<summary>  param[21] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.30.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P1_T1 = 0x000301001E0801FF,

        ///<summary>  param[21] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.30.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P1_T2 = 0x000301001E0802FF,

        ///<summary>  param[21] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.30.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P1_T3 = 0x000301001E0803FF,

        ///<summary>  param[21] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.30.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P1_T4 = 0x000301001E0804FF,

        ///<summary>  param[21] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.30.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P1_T5 = 0x000301001E0805FF,

        ///<summary>  param[21] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.30.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P1_T6 = 0x000301001E0806FF,

        ///<summary>  param[21] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.30.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P1_T7 = 0x000301001E0807FF,

        ///<summary>  param[21] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.30.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P1_T8 = 0x000301001E0808FF,

        #endregion // --021  CB_APPARENT_ENERGY_NEG_P1_TL[1-8] --

        //===============================================================================

        #region --022 CB_REACTIVE_ENERGY_NEG_P1_TL[1-8] --

        ///<summary>  param[22] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.34.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P1_TL = 0x00030100220800FF,

        ///<summary>  param[22] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.34.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P1_T1 = 0x00030100220801FF,

        ///<summary>  param[22] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.34.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P1_T2 = 0x00030100220802FF,

        ///<summary>  param[22] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.34.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P1_T3 = 0x00030100220803FF,

        ///<summary>  param[22] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.34.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P1_T4 = 0x00030100220804FF,

        ///<summary>  param[22] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.34.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P1_T5 = 0x00030100220805FF,

        ///<summary>  param[22] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.34.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P1_T6 = 0x00030100220806FF,

        ///<summary>  param[22] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.34.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P1_T7 = 0x00030100220807FF,

        ///<summary>  param[22] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.34.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P1_T8 = 0x00030100220808FF,

        #endregion // --022  CB_REACTIVE_ENERGY_NEG_P1_TL[1-8] --

        //===============================================================================

        #region --023 CB_ACTIVE_ENERGY_ABS_P1_TL[1-8] --

        ///<summary>  param[23] -> value[TOTAL]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.35.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P1_TL = 0x00030100230800FF,

        ///<summary>  param[23] -> value[1]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.35.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P1_T1 = 0x00030100230801FF,

        ///<summary>  param[23] -> value[2]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.35.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P1_T2 = 0x00030100230802FF,

        ///<summary>  param[23] -> value[3]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.35.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P1_T3 = 0x00030100230803FF,

        ///<summary>  param[23] -> value[4]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.35.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P1_T4 = 0x00030100230804FF,

        ///<summary>  param[23] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.35.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P1_T5 = 0x00030100230805FF,

        ///<summary>  param[23] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.35.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P1_T6 = 0x00030100230806FF,

        ///<summary>  param[23] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.35.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P1_T7 = 0x00030100230807FF,

        ///<summary>  param[23] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.35.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P1_T8 = 0x00030100230808FF,

        #endregion // --023  CB_ACTIVE_ENERGY_ABS_P1_TL[1-8] --

        //===============================================================================

        #region --024 CB_ACTIVE_ENERGY_POS_P2_TL[1-8] --

        ///<summary>  param[24] -> value[TOTAL]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.41.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P2_TL = 0x00030100290800FF,

        ///<summary>  param[24] -> value[1]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.41.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P2_T1 = 0x00030100290801FF,

        ///<summary>  param[24] -> value[2]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.41.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P2_T2 = 0x00030100290802FF,

        ///<summary>  param[24] -> value[3]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.41.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P2_T3 = 0x00030100290803FF,

        ///<summary>  param[24] -> value[4]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.41.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P2_T4 = 0x00030100290804FF,

        ///<summary>  param[24] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.41.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P2_T5 = 0x00030100290805FF,

        ///<summary>  param[24] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.41.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P2_T6 = 0x00030100290806FF,

        ///<summary>  param[24] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.41.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P2_T7 = 0x00030100290807FF,

        ///<summary>  param[24] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.41.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P2_T8 = 0x00030100290808FF,

        #endregion // --024  CB_ACTIVE_ENERGY_POS_P2_TL[1-8] --

        //===============================================================================

        #region --025 CB_ACTIVE_ENERGY_NEG_P2_TL[1-8] --

        ///<summary>  param[25] -> value[TOTAL]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.42.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P2_TL = 0x000301002A0800FF,

        ///<summary>  param[25] -> value[1]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.42.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P2_T1 = 0x000301002A0801FF,

        ///<summary>  param[25] -> value[2]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.42.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P2_T2 = 0x000301002A0802FF,

        ///<summary>  param[25] -> value[3]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.42.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P2_T3 = 0x000301002A0803FF,

        ///<summary>  param[25] -> value[4]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.42.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P2_T4 = 0x000301002A0804FF,

        ///<summary>  param[25] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.42.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P2_T5 = 0x000301002A0805FF,

        ///<summary>  param[25] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.42.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P2_T6 = 0x000301002A0806FF,

        ///<summary>  param[25] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.42.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P2_T7 = 0x000301002A0807FF,

        ///<summary>  param[25] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.42.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P2_T8 = 0x000301002A0808FF,

        #endregion // --025  CB_ACTIVE_ENERGY_NEG_P2_TL[1-8] --

        //===============================================================================

        #region --026 CB_REACTIVE_ENERGY_POS_P2_TL[1-8] --

        ///<summary>  param[26] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.43.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P2_TL = 0x000301002B0800FF,

        ///<summary>  param[26] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.43.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P2_T1 = 0x000301002B0801FF,

        ///<summary>  param[26] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.43.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P2_T2 = 0x000301002B0802FF,

        ///<summary>  param[26] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.43.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P2_T3 = 0x000301002B0803FF,

        ///<summary>  param[26] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.43.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P2_T4 = 0x000301002B0804FF,

        ///<summary>  param[26] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.43.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P2_T5 = 0x000301002B0805FF,

        ///<summary>  param[26] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.43.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P2_T6 = 0x000301002B0806FF,

        ///<summary>  param[26] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.43.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P2_T7 = 0x000301002B0807FF,

        ///<summary>  param[26] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.43.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P2_T8 = 0x000301002B0808FF,

        #endregion // --026  CB_REACTIVE_ENERGY_POS_P2_TL[1-8] --

        //===============================================================================

        #region --027 CB_REACTIVE_ENERGY_Q1_P2_TL[1-8] --

        ///<summary>  param[27] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.45.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P2_TL = 0x000301002D0800FF,

        ///<summary>  param[27] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.45.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P2_T1 = 0x000301002D0801FF,

        ///<summary>  param[27] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.45.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P2_T2 = 0x000301002D0802FF,

        ///<summary>  param[27] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.45.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P2_T3 = 0x000301002D0803FF,

        ///<summary>  param[27] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.45.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P2_T4 = 0x000301002D0804FF,

        ///<summary>  param[27] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.45.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P2_T5 = 0x000301002D0805FF,

        ///<summary>  param[27] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.45.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P2_T6 = 0x000301002D0806FF,

        ///<summary>  param[27] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.45.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P2_T7 = 0x000301002D0807FF,

        ///<summary>  param[27] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.45.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P2_T8 = 0x000301002D0808FF,

        #endregion // --027  CB_REACTIVE_ENERGY_Q1_P2_TL[1-8] --

        //===============================================================================

        #region --028 CB_REACTIVE_ENERGY_Q2_P2_TL[1-8] --

        ///<summary>  param[28] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.46.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P2_TL = 0x000301002E0800FF,

        ///<summary>  param[28] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.46.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P2_T1 = 0x000301002E0801FF,

        ///<summary>  param[28] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.46.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P2_T2 = 0x000301002E0802FF,

        ///<summary>  param[28] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.46.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P2_T3 = 0x000301002E0803FF,

        ///<summary>  param[28] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.46.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P2_T4 = 0x000301002E0804FF,

        ///<summary>  param[28] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.46.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P2_T5 = 0x000301002E0805FF,

        ///<summary>  param[28] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.46.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P2_T6 = 0x000301002E0806FF,

        ///<summary>  param[28] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.46.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P2_T7 = 0x000301002E0807FF,

        ///<summary>  param[28] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.46.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P2_T8 = 0x000301002E0808FF,

        #endregion // --028  CB_REACTIVE_ENERGY_Q2_P2_TL[1-8] --

        //===============================================================================

        #region --029 CB_REACTIVE_ENERGY_Q3_P2_TL[1-8] --

        ///<summary>  param[29] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.47.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P2_TL = 0x000301002F0800FF,

        ///<summary>  param[29] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.47.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P2_T1 = 0x000301002F0801FF,

        ///<summary>  param[29] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.47.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P2_T2 = 0x000301002F0802FF,

        ///<summary>  param[29] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.47.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P2_T3 = 0x000301002F0803FF,

        ///<summary>  param[29] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.47.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P2_T4 = 0x000301002F0804FF,

        ///<summary>  param[29] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.47.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P2_T5 = 0x000301002F0805FF,

        ///<summary>  param[29] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.47.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P2_T6 = 0x000301002F0806FF,

        ///<summary>  param[29] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.47.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P2_T7 = 0x000301002F0807FF,

        ///<summary>  param[29] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.47.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P2_T8 = 0x000301002F0808FF,

        #endregion // --029  CB_REACTIVE_ENERGY_Q3_P2_TL[1-8] --

        //===============================================================================

        #region --030 CB_REACTIVE_ENERGY_Q4_P2_TL[1-8] --

        ///<summary>  param[30] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.48.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P2_TL = 0x00030100300800FF,

        ///<summary>  param[30] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.48.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P2_T1 = 0x00030100300801FF,

        ///<summary>  param[30] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.48.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P2_T2 = 0x00030100300802FF,

        ///<summary>  param[30] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.48.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P2_T3 = 0x00030100300803FF,

        ///<summary>  param[30] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.48.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P2_T4 = 0x00030100300804FF,

        ///<summary>  param[30] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.48.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P2_T5 = 0x00030100300805FF,

        ///<summary>  param[30] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.48.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P2_T6 = 0x00030100300806FF,

        ///<summary>  param[30] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.48.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P2_T7 = 0x00030100300807FF,

        ///<summary>  param[30] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.48.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P2_T8 = 0x00030100300808FF,

        #endregion // --030  CB_REACTIVE_ENERGY_Q4_P2_TL[1-8] --

        //===============================================================================

        #region --031 CB_APPARENT_ENERGY_POS_P2_TL[1-8] --

        ///<summary>  param[31] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.49.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P2_TL = 0x00030100310800FF,

        ///<summary>  param[31] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.49.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P2_T1 = 0x00030100310801FF,

        ///<summary>  param[31] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.49.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P2_T2 = 0x00030100310802FF,

        ///<summary>  param[31] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.49.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P2_T3 = 0x00030100310803FF,

        ///<summary>  param[31] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.49.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P2_T4 = 0x00030100310804FF,

        ///<summary>  param[31] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.49.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P2_T5 = 0x00030100310805FF,

        ///<summary>  param[31] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.49.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P2_T6 = 0x00030100310806FF,

        ///<summary>  param[31] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.49.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P2_T7 = 0x00030100310807FF,

        ///<summary>  param[31] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.49.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P2_T8 = 0x00030100310808FF,

        #endregion // --031  CB_APPARENT_ENERGY_POS_P2_TL[1-8] --

        //===============================================================================

        #region --032 CB_APPARENT_ENERGY_NEG_P2_TL[1-8] --

        ///<summary>  param[32] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.50.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P2_TL = 0x00030100320800FF,

        ///<summary>  param[32] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.50.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P2_T1 = 0x00030100320801FF,

        ///<summary>  param[32] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.50.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P2_T2 = 0x00030100320802FF,

        ///<summary>  param[32] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.50.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P2_T3 = 0x00030100320803FF,

        ///<summary>  param[32] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.50.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P2_T4 = 0x00030100320804FF,

        ///<summary>  param[32] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.50.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P2_T5 = 0x00030100320805FF,

        ///<summary>  param[32] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.50.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P2_T6 = 0x00030100320806FF,

        ///<summary>  param[32] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.50.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P2_T7 = 0x00030100320807FF,

        ///<summary>  param[32] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.50.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P2_T8 = 0x00030100320808FF,

        #endregion // --032  CB_APPARENT_ENERGY_NEG_P2_TL[1-8] --

        //===============================================================================

        #region --033 CB_REACTIVE_ENERGY_NEG_P2_TL[1-8] --

        ///<summary>  param[33] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.54.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P2_TL = 0x00030100360800FF,

        ///<summary>  param[33] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.54.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P2_T1 = 0x00030100360801FF,

        ///<summary>  param[33] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.54.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P2_T2 = 0x00030100360802FF,

        ///<summary>  param[33] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.54.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P2_T3 = 0x00030100360803FF,

        ///<summary>  param[33] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.54.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P2_T4 = 0x00030100360804FF,

        ///<summary>  param[33] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.54.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P2_T5 = 0x00030100360805FF,

        ///<summary>  param[33] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.54.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P2_T6 = 0x00030100360806FF,

        ///<summary>  param[33] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.54.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P2_T7 = 0x00030100360807FF,

        ///<summary>  param[33] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.54.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P2_T8 = 0x00030100360808FF,

        #endregion // --033  CB_REACTIVE_ENERGY_NEG_P2_TL[1-8] --

        //===============================================================================

        #region --034 CB_ACTIVE_ENERGY_ABS_P2_TL[1-8] --

        ///<summary>  param[34] -> value[TOTAL]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.55.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P2_TL = 0x00030100370800FF,

        ///<summary>  param[34] -> value[1]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.55.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P2_T1 = 0x00030100370801FF,

        ///<summary>  param[34] -> value[2]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.55.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P2_T2 = 0x00030100370802FF,

        ///<summary>  param[34] -> value[3]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.55.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P2_T3 = 0x00030100370803FF,

        ///<summary>  param[34] -> value[4]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.55.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P2_T4 = 0x00030100370804FF,

        ///<summary>  param[34] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.55.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P2_T5 = 0x00030100370805FF,

        ///<summary>  param[34] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.55.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P2_T6 = 0x00030100370806FF,

        ///<summary>  param[34] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.55.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P2_T7 = 0x00030100370807FF,

        ///<summary>  param[34] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.55.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P2_T8 = 0x00030100370808FF,

        #endregion // --034  CB_ACTIVE_ENERGY_ABS_P2_TL[1-8] --

        //===============================================================================

        #region --035 CB_ACTIVE_ENERGY_POS_P3_TL[1-8] --

        ///<summary>  param[35] -> value[TOTAL]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.61.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P3_TL = 0x000301003D0800FF,

        ///<summary>  param[35] -> value[1]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.61.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P3_T1 = 0x000301003D0801FF,

        ///<summary>  param[35] -> value[2]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.61.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P3_T2 = 0x000301003D0802FF,

        ///<summary>  param[35] -> value[3]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.61.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P3_T3 = 0x000301003D0803FF,

        ///<summary>  param[35] -> value[4]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.61.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P3_T4 = 0x000301003D0804FF,

        ///<summary>  param[35] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.61.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P3_T5 = 0x000301003D0805FF,

        ///<summary>  param[35] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.61.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P3_T6 = 0x000301003D0806FF,

        ///<summary>  param[35] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.61.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P3_T7 = 0x000301003D0807FF,

        ///<summary>  param[35] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_POS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.61.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_POS_P3_T8 = 0x000301003D0808FF,

        #endregion // --035  CB_ACTIVE_ENERGY_POS_P3_TL[1-8] --

        //===============================================================================

        #region --036 CB_ACTIVE_ENERGY_NEG_P3_TL[1-8] --

        ///<summary>  param[36] -> value[TOTAL]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.62.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P3_TL = 0x000301003E0800FF,

        ///<summary>  param[36] -> value[1]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.62.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P3_T1 = 0x000301003E0801FF,

        ///<summary>  param[36] -> value[2]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.62.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P3_T2 = 0x000301003E0802FF,

        ///<summary>  param[36] -> value[3]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.62.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P3_T3 = 0x000301003E0803FF,

        ///<summary>  param[36] -> value[4]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.62.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P3_T4 = 0x000301003E0804FF,

        ///<summary>  param[36] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.62.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P3_T5 = 0x000301003E0805FF,

        ///<summary>  param[36] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.62.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P3_T6 = 0x000301003E0806FF,

        ///<summary>  param[36] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.62.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P3_T7 = 0x000301003E0807FF,

        ///<summary>  param[36] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_NEG_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.62.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_NEG_P3_T8 = 0x000301003E0808FF,

        #endregion // --036  CB_ACTIVE_ENERGY_NEG_P3_TL[1-8] --

        //===============================================================================

        #region --037 CB_REACTIVE_ENERGY_POS_P3_TL[1-8] --

        ///<summary>  param[37] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.63.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P3_TL = 0x000301003F0800FF,

        ///<summary>  param[37] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.63.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P3_T1 = 0x000301003F0801FF,

        ///<summary>  param[37] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.63.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P3_T2 = 0x000301003F0802FF,

        ///<summary>  param[37] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.63.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P3_T3 = 0x000301003F0803FF,

        ///<summary>  param[37] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.63.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P3_T4 = 0x000301003F0804FF,

        ///<summary>  param[37] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.63.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P3_T5 = 0x000301003F0805FF,

        ///<summary>  param[37] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.63.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P3_T6 = 0x000301003F0806FF,

        ///<summary>  param[37] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.63.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P3_T7 = 0x000301003F0807FF,

        ///<summary>  param[37] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_POS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.63.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_POS_P3_T8 = 0x000301003F0808FF,

        #endregion // --037  CB_REACTIVE_ENERGY_POS_P3_TL[1-8] --

        //===============================================================================

        #region --038 CB_REACTIVE_ENERGY_Q1_P3_TL[1-8] --

        ///<summary>  param[38] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.65.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P3_TL = 0x00030100410800FF,

        ///<summary>  param[38] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.65.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P3_T1 = 0x00030100410801FF,

        ///<summary>  param[38] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.65.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P3_T2 = 0x00030100410802FF,

        ///<summary>  param[38] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.65.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P3_T3 = 0x00030100410803FF,

        ///<summary>  param[38] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.65.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P3_T4 = 0x00030100410804FF,

        ///<summary>  param[38] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.65.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P3_T5 = 0x00030100410805FF,

        ///<summary>  param[38] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.65.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P3_T6 = 0x00030100410806FF,

        ///<summary>  param[38] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.65.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P3_T7 = 0x00030100410807FF,

        ///<summary>  param[38] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q1_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.65.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q1_P3_T8 = 0x00030100410808FF,

        #endregion // --038  CB_REACTIVE_ENERGY_Q1_P3_TL[1-8] --

        //===============================================================================

        #region --039 CB_REACTIVE_ENERGY_Q2_P3_TL[1-8] --

        ///<summary>  param[39] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.66.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P3_TL = 0x00030100420800FF,

        ///<summary>  param[39] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.66.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P3_T1 = 0x00030100420801FF,

        ///<summary>  param[39] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.66.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P3_T2 = 0x00030100420802FF,

        ///<summary>  param[39] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.66.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P3_T3 = 0x00030100420803FF,

        ///<summary>  param[39] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.66.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P3_T4 = 0x00030100420804FF,

        ///<summary>  param[39] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.66.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P3_T5 = 0x00030100420805FF,

        ///<summary>  param[39] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.66.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P3_T6 = 0x00030100420806FF,

        ///<summary>  param[39] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.66.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P3_T7 = 0x00030100420807FF,

        ///<summary>  param[39] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q2_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.66.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q2_P3_T8 = 0x00030100420808FF,

        #endregion // --039  CB_REACTIVE_ENERGY_Q2_P3_TL[1-8] --

        //===============================================================================

        #region --040 CB_REACTIVE_ENERGY_Q3_P3_TL[1-8] --

        ///<summary>  param[40] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.67.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P3_TL = 0x00030100430800FF,

        ///<summary>  param[40] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.67.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P3_T1 = 0x00030100430801FF,

        ///<summary>  param[40] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.67.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P3_T2 = 0x00030100430802FF,

        ///<summary>  param[40] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.67.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P3_T3 = 0x00030100430803FF,

        ///<summary>  param[40] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.67.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P3_T4 = 0x00030100430804FF,

        ///<summary>  param[40] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.67.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P3_T5 = 0x00030100430805FF,

        ///<summary>  param[40] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.67.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P3_T6 = 0x00030100430806FF,

        ///<summary>  param[40] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.67.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P3_T7 = 0x00030100430807FF,

        ///<summary>  param[40] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q3_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.67.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q3_P3_T8 = 0x00030100430808FF,

        #endregion // --040  CB_REACTIVE_ENERGY_Q3_P3_TL[1-8] --

        //===============================================================================

        #region --041 CB_REACTIVE_ENERGY_Q4_P3_TL[1-8] --

        ///<summary>  param[41] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.68.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P3_TL = 0x00030100440800FF,

        ///<summary>  param[41] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.68.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P3_T1 = 0x00030100440801FF,

        ///<summary>  param[41] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.68.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P3_T2 = 0x00030100440802FF,

        ///<summary>  param[41] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.68.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P3_T3 = 0x00030100440803FF,

        ///<summary>  param[41] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.68.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P3_T4 = 0x00030100440804FF,

        ///<summary>  param[41] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.68.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P3_T5 = 0x00030100440805FF,

        ///<summary>  param[41] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.68.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P3_T6 = 0x00030100440806FF,

        ///<summary>  param[41] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.68.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P3_T7 = 0x00030100440807FF,

        ///<summary>  param[41] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_Q4_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.68.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_Q4_P3_T8 = 0x00030100440808FF,

        #endregion // --041  CB_REACTIVE_ENERGY_Q4_P3_TL[1-8] --

        //===============================================================================

        #region --042 CB_APPARENT_ENERGY_POS_P3_TL[1-8] --

        ///<summary>  param[42] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.69.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P3_TL = 0x00030100450800FF,

        ///<summary>  param[42] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.69.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P3_T1 = 0x00030100450801FF,

        ///<summary>  param[42] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.69.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P3_T2 = 0x00030100450802FF,

        ///<summary>  param[42] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.69.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P3_T3 = 0x00030100450803FF,

        ///<summary>  param[42] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.69.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P3_T4 = 0x00030100450804FF,

        ///<summary>  param[42] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.69.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P3_T5 = 0x00030100450805FF,

        ///<summary>  param[42] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.69.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P3_T6 = 0x00030100450806FF,

        ///<summary>  param[42] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.69.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P3_T7 = 0x00030100450807FF,

        ///<summary>  param[42] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_POS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.69.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_POS_P3_T8 = 0x00030100450808FF,

        #endregion // --042  CB_APPARENT_ENERGY_POS_P3_TL[1-8] --

        //===============================================================================

        #region --043 CB_APPARENT_ENERGY_NEG_P3_TL[1-8] --

        ///<summary>  param[43] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.70.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P3_TL = 0x00030100460800FF,

        ///<summary>  param[43] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.70.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P3_T1 = 0x00030100460801FF,

        ///<summary>  param[43] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.70.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P3_T2 = 0x00030100460802FF,

        ///<summary>  param[43] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.70.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P3_T3 = 0x00030100460803FF,

        ///<summary>  param[43] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.70.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P3_T4 = 0x00030100460804FF,

        ///<summary>  param[43] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.70.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P3_T5 = 0x00030100460805FF,

        ///<summary>  param[43] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.70.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P3_T6 = 0x00030100460806FF,

        ///<summary>  param[43] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.70.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P3_T7 = 0x00030100460807FF,

        ///<summary>  param[43] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_NEG_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.70.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_NEG_P3_T8 = 0x00030100460808FF,

        #endregion // --043  CB_APPARENT_ENERGY_NEG_P3_TL[1-8] --

        //===============================================================================

        #region --044 CB_REACTIVE_ENERGY_NEG_P3_TL[1-8] --

        ///<summary>  param[44] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.74.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P3_TL = 0x000301004A0800FF,

        ///<summary>  param[44] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.74.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P3_T1 = 0x000301004A0801FF,

        ///<summary>  param[44] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.74.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P3_T2 = 0x000301004A0802FF,

        ///<summary>  param[44] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.74.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P3_T3 = 0x000301004A0803FF,

        ///<summary>  param[44] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.74.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P3_T4 = 0x000301004A0804FF,

        ///<summary>  param[44] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.74.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P3_T5 = 0x000301004A0805FF,

        ///<summary>  param[44] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.74.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P3_T6 = 0x000301004A0806FF,

        ///<summary>  param[44] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.74.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P3_T7 = 0x000301004A0807FF,

        ///<summary>  param[44] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_NEG_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.74.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_NEG_P3_T8 = 0x000301004A0808FF,

        #endregion // --044  CB_REACTIVE_ENERGY_NEG_P3_TL[1-8] --

        //===============================================================================

        #region --045 CB_ACTIVE_ENERGY_ABS_P3_TL[1-8] --

        ///<summary>  param[45] -> value[TOTAL]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.75.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P3_TL = 0x000301004B0800FF,

        ///<summary>  param[45] -> value[1]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.75.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P3_T1 = 0x000301004B0801FF,

        ///<summary>  param[45] -> value[2]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.75.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P3_T2 = 0x000301004B0802FF,

        ///<summary>  param[45] -> value[3]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.75.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P3_T3 = 0x000301004B0803FF,

        ///<summary>  param[45] -> value[4]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.75.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P3_T4 = 0x000301004B0804FF,

        ///<summary>  param[45] -> value[5]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.75.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P3_T5 = 0x000301004B0805FF,

        ///<summary>  param[45] -> value[6]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.75.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P3_T6 = 0x000301004B0806FF,

        ///<summary>  param[45] -> value[7]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.75.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P3_T7 = 0x000301004B0807FF,

        ///<summary>  param[45] -> value[8]
        /// Description: Quantity_Name= CB_ACTIVE_ENERGY_ABS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.75.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_ACTIVE_ENERGY_ABS_P3_T8 = 0x000301004B0808FF,

        #endregion // --045  CB_ACTIVE_ENERGY_ABS_P3_TL[1-8] --

        //===============================================================================

        #region --046 CB_MDI_REACTIVE_ABS_TL[1-8] --

        //**************//Index[46][0] Match OBIS => 0x00030100800200FF 

        //**************//Index[46][1] Match OBIS => 0x00030100800201FF 

        //**************//Index[46][2] Match OBIS => 0x00030100800202FF 

        //**************//Index[46][3] Match OBIS => 0x00030100800203FF 

        //**************//Index[46][4] Match OBIS => 0x00030100800204FF 

        ///<summary>  param[46] -> value[5]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_ABS_T5  Class ID 3
        /// OBIS_CODE = 1.0.128.2.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_ABS_T5 = 0x00030100800205FF,

        ///<summary>  param[46] -> value[6]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_ABS_T6  Class ID 3
        /// OBIS_CODE = 1.0.128.2.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_ABS_T6 = 0x00030100800206FF,

        ///<summary>  param[46] -> value[7]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_ABS_T7  Class ID 3
        /// OBIS_CODE = 1.0.128.2.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_ABS_T7 = 0x00030100800207FF,

        ///<summary>  param[46] -> value[8]
        /// Description: Quantity_Name= CB_MDI_REACTIVE_ABS_T8  Class ID 3
        /// OBIS_CODE = 1.0.128.2.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_MDI_REACTIVE_ABS_T8 = 0x00030100800208FF,

        #endregion // --046  CB_MDI_REACTIVE_ABS_TL[1-8] --

        //===============================================================================

        #region --047 CB_APPARENT_ENERGY_ABS_P1_TL[1-8] --

        ///<summary>  param[47] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.151.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P1_TL = 0x00030100970800FF,

        ///<summary>  param[47] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.151.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P1_T1 = 0x00030100970801FF,

        ///<summary>  param[47] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.151.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P1_T2 = 0x00030100970802FF,

        ///<summary>  param[47] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.151.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P1_T3 = 0x00030100970803FF,

        ///<summary>  param[47] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.151.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P1_T4 = 0x00030100970804FF,

        ///<summary>  param[47] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.151.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P1_T5 = 0x00030100970805FF,

        ///<summary>  param[47] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.151.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P1_T6 = 0x00030100970806FF,

        ///<summary>  param[47] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.151.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P1_T7 = 0x00030100970807FF,

        ///<summary>  param[47] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.151.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P1_T8 = 0x00030100970808FF,

        #endregion // --047  CB_APPARENT_ENERGY_ABS_P1_TL[1-8] --

        //===============================================================================

        #region --048 CB_APPARENT_ENERGY_ABS_P2_TL[1-8] --

        ///<summary>  param[48] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.152.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P2_TL = 0x00030100980800FF,

        ///<summary>  param[48] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.152.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P2_T1 = 0x00030100980801FF,

        ///<summary>  param[48] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.152.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P2_T2 = 0x00030100980802FF,

        ///<summary>  param[48] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.152.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P2_T3 = 0x00030100980803FF,

        ///<summary>  param[48] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.152.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P2_T4 = 0x00030100980804FF,

        ///<summary>  param[48] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.152.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P2_T5 = 0x00030100980805FF,

        ///<summary>  param[48] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.152.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P2_T6 = 0x00030100980806FF,

        ///<summary>  param[48] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.152.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P2_T7 = 0x00030100980807FF,

        ///<summary>  param[48] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.152.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P2_T8 = 0x00030100980808FF,

        #endregion // --048  CB_APPARENT_ENERGY_ABS_P2_TL[1-8] --

        //===============================================================================

        #region --049 CB_APPARENT_ENERGY_ABS_P3_TL[1-8] --

        ///<summary>  param[49] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.153.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P3_TL = 0x00030100990800FF,

        ///<summary>  param[49] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.153.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P3_T1 = 0x00030100990801FF,

        ///<summary>  param[49] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.153.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P3_T2 = 0x00030100990802FF,

        ///<summary>  param[49] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.153.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P3_T3 = 0x00030100990803FF,

        ///<summary>  param[49] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.153.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P3_T4 = 0x00030100990804FF,

        ///<summary>  param[49] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.153.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P3_T5 = 0x00030100990805FF,

        ///<summary>  param[49] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.153.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P3_T6 = 0x00030100990806FF,

        ///<summary>  param[49] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.153.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P3_T7 = 0x00030100990807FF,

        ///<summary>  param[49] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.153.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_P3_T8 = 0x00030100990808FF,

        #endregion // --049  CB_APPARENT_ENERGY_ABS_P3_TL[1-8] --

        //===============================================================================

        #region --050 CB_APPARENT_ENERGY_ABS_TL[1-8] --

        ///<summary>  param[50] -> value[TOTAL]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_TL  Class ID 3
        /// OBIS_CODE = 1.0.154.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_TL = 0x000301009A0800FF,

        ///<summary>  param[50] -> value[1]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_T1  Class ID 3
        /// OBIS_CODE = 1.0.154.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_T1 = 0x000301009A0801FF,

        ///<summary>  param[50] -> value[2]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_T2  Class ID 3
        /// OBIS_CODE = 1.0.154.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_T2 = 0x000301009A0802FF,

        ///<summary>  param[50] -> value[3]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_T3  Class ID 3
        /// OBIS_CODE = 1.0.154.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_T3 = 0x000301009A0803FF,

        ///<summary>  param[50] -> value[4]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_T4  Class ID 3
        /// OBIS_CODE = 1.0.154.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_T4 = 0x000301009A0804FF,

        ///<summary>  param[50] -> value[5]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_T5  Class ID 3
        /// OBIS_CODE = 1.0.154.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_T5 = 0x000301009A0805FF,

        ///<summary>  param[50] -> value[6]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_T6  Class ID 3
        /// OBIS_CODE = 1.0.154.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_T6 = 0x000301009A0806FF,

        ///<summary>  param[50] -> value[7]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_T7  Class ID 3
        /// OBIS_CODE = 1.0.154.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_T7 = 0x000301009A0807FF,

        ///<summary>  param[50] -> value[8]
        /// Description: Quantity_Name= CB_APPARENT_ENERGY_ABS_T8  Class ID 3
        /// OBIS_CODE = 1.0.154.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_APPARENT_ENERGY_ABS_T8 = 0x000301009A0808FF,

        #endregion // --050  CB_APPARENT_ENERGY_ABS_TL[1-8] --

        //===============================================================================

        #region --051 CB_REACTIVE_ENERGY_ABS_P1_TL[1-8] --

        ///<summary>  param[51] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.155.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P1_TL = 0x000301009B0800FF,

        ///<summary>  param[51] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.155.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P1_T1 = 0x000301009B0801FF,

        ///<summary>  param[51] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.155.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P1_T2 = 0x000301009B0802FF,

        ///<summary>  param[51] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.155.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P1_T3 = 0x000301009B0803FF,

        ///<summary>  param[51] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.155.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P1_T4 = 0x000301009B0804FF,

        ///<summary>  param[51] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.155.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P1_T5 = 0x000301009B0805FF,

        ///<summary>  param[51] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.155.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P1_T6 = 0x000301009B0806FF,

        ///<summary>  param[51] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.155.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P1_T7 = 0x000301009B0807FF,

        ///<summary>  param[51] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.155.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P1_T8 = 0x000301009B0808FF,

        #endregion // --051  CB_REACTIVE_ENERGY_ABS_P1_TL[1-8] --

        //===============================================================================

        #region --052 CB_REACTIVE_ENERGY_ABS_P2_TL[1-8] --

        ///<summary>  param[52] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.156.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P2_TL = 0x000301009C0800FF,

        ///<summary>  param[52] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.156.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P2_T1 = 0x000301009C0801FF,

        ///<summary>  param[52] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.156.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P2_T2 = 0x000301009C0802FF,

        ///<summary>  param[52] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.156.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P2_T3 = 0x000301009C0803FF,

        ///<summary>  param[52] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.156.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P2_T4 = 0x000301009C0804FF,

        ///<summary>  param[52] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.156.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P2_T5 = 0x000301009C0805FF,

        ///<summary>  param[52] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.156.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P2_T6 = 0x000301009C0806FF,

        ///<summary>  param[52] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.156.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P2_T7 = 0x000301009C0807FF,

        ///<summary>  param[52] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.156.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P2_T8 = 0x000301009C0808FF,

        #endregion // --052  CB_REACTIVE_ENERGY_ABS_P2_TL[1-8] --

        //===============================================================================

        #region --053 CB_REACTIVE_ENERGY_ABS_P3_TL[1-8] --

        ///<summary>  param[53] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.157.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P3_TL = 0x000301009D0800FF,

        ///<summary>  param[53] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.157.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P3_T1 = 0x000301009D0801FF,

        ///<summary>  param[53] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.157.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P3_T2 = 0x000301009D0802FF,

        ///<summary>  param[53] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.157.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P3_T3 = 0x000301009D0803FF,

        ///<summary>  param[53] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.157.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P3_T4 = 0x000301009D0804FF,

        ///<summary>  param[53] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.157.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P3_T5 = 0x000301009D0805FF,

        ///<summary>  param[53] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.157.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P3_T6 = 0x000301009D0806FF,

        ///<summary>  param[53] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.157.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P3_T7 = 0x000301009D0807FF,

        ///<summary>  param[53] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.157.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_P3_T8 = 0x000301009D0808FF,

        #endregion // --053  CB_REACTIVE_ENERGY_ABS_P3_TL[1-8] --

        //===============================================================================

        #region --054 CB_REACTIVE_ENERGY_ABS_TL[1-8] --

        ///<summary>  param[54] -> value[TOTAL]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_TL  Class ID 3
        /// OBIS_CODE = 1.0.158.8.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_TL = 0x000301009E0800FF,

        ///<summary>  param[54] -> value[1]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_T1  Class ID 3
        /// OBIS_CODE = 1.0.158.8.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_T1 = 0x000301009E0801FF,

        ///<summary>  param[54] -> value[2]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_T2  Class ID 3
        /// OBIS_CODE = 1.0.158.8.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_T2 = 0x000301009E0802FF,

        ///<summary>  param[54] -> value[3]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_T3  Class ID 3
        /// OBIS_CODE = 1.0.158.8.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_T3 = 0x000301009E0803FF,

        ///<summary>  param[54] -> value[4]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_T4  Class ID 3
        /// OBIS_CODE = 1.0.158.8.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_T4 = 0x000301009E0804FF,

        ///<summary>  param[54] -> value[5]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_T5  Class ID 3
        /// OBIS_CODE = 1.0.158.8.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_T5 = 0x000301009E0805FF,

        ///<summary>  param[54] -> value[6]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_T6  Class ID 3
        /// OBIS_CODE = 1.0.158.8.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_T6 = 0x000301009E0806FF,

        ///<summary>  param[54] -> value[7]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_T7  Class ID 3
        /// OBIS_CODE = 1.0.158.8.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_T7 = 0x000301009E0807FF,

        ///<summary>  param[54] -> value[8]
        /// Description: Quantity_Name= CB_REACTIVE_ENERGY_ABS_T8  Class ID 3
        /// OBIS_CODE = 1.0.158.8.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        CB_REACTIVE_ENERGY_ABS_T8 = 0x000301009E0808FF,

        #endregion // --054  CB_REACTIVE_ENERGY_ABS_TL[1-8] --

        //===============================================================================

        #region --055 DB_ACTIVE_ENERGY_POS_TL[1-8] --

        //**************//Index[55][0] Match OBIS => 0x00030100010900FF 

        //**************//Index[55][1] Match OBIS => 0x00030100010901FF 

        //**************//Index[55][2] Match OBIS => 0x00030100010902FF 

        //**************//Index[55][3] Match OBIS => 0x00030100010903FF 

        //**************//Index[55][4] Match OBIS => 0x00030100010904FF 

        ///<summary>  param[55] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_T5  Class ID 3
        /// OBIS_CODE = 1.0.1.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_T5 = 0x00030100010905FF,

        ///<summary>  param[55] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_T6  Class ID 3
        /// OBIS_CODE = 1.0.1.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_T6 = 0x00030100010906FF,

        ///<summary>  param[55] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_T7  Class ID 3
        /// OBIS_CODE = 1.0.1.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_T7 = 0x00030100010907FF,

        ///<summary>  param[55] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_T8  Class ID 3
        /// OBIS_CODE = 1.0.1.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_T8 = 0x00030100010908FF,

        #endregion // --055  DB_ACTIVE_ENERGY_POS_TL[1-8] --

        //===============================================================================

        #region --056 DB_ACTIVE_ENERGY_NEG_TL[1-8] --

        //**************//Index[56][0] Match OBIS => 0x00030100020900FF 

        //**************//Index[56][1] Match OBIS => 0x00030100020901FF 

        //**************//Index[56][2] Match OBIS => 0x00030100020902FF 

        //**************//Index[56][3] Match OBIS => 0x00030100020903FF 

        //**************//Index[56][4] Match OBIS => 0x00030100020904FF 

        ///<summary>  param[56] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_T5  Class ID 3
        /// OBIS_CODE = 1.0.2.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_T5 = 0x00030100020905FF,

        ///<summary>  param[56] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_T6  Class ID 3
        /// OBIS_CODE = 1.0.2.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_T6 = 0x00030100020906FF,

        ///<summary>  param[56] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_T7  Class ID 3
        /// OBIS_CODE = 1.0.2.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_T7 = 0x00030100020907FF,

        ///<summary>  param[56] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_T8  Class ID 3
        /// OBIS_CODE = 1.0.2.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_T8 = 0x00030100020908FF,

        #endregion // --056  DB_ACTIVE_ENERGY_NEG_TL[1-8] --

        //===============================================================================

        #region --057 DB_REACTIVE_ENERGY_POS_TL[1-8] --

        ///<summary>  param[57] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_TL  Class ID 3
        /// OBIS_CODE = 1.0.3.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_TL = 0x00030100030900FF,

        ///<summary>  param[57] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_T1  Class ID 3
        /// OBIS_CODE = 1.0.3.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_T1 = 0x00030100030901FF,

        ///<summary>  param[57] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_T2  Class ID 3
        /// OBIS_CODE = 1.0.3.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_T2 = 0x00030100030902FF,

        ///<summary>  param[57] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_T3  Class ID 3
        /// OBIS_CODE = 1.0.3.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_T3 = 0x00030100030903FF,

        ///<summary>  param[57] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_T4  Class ID 3
        /// OBIS_CODE = 1.0.3.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_T4 = 0x00030100030904FF,

        ///<summary>  param[57] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_T5  Class ID 3
        /// OBIS_CODE = 1.0.3.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_T5 = 0x00030100030905FF,

        ///<summary>  param[57] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_T6  Class ID 3
        /// OBIS_CODE = 1.0.3.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_T6 = 0x00030100030906FF,

        ///<summary>  param[57] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_T7  Class ID 3
        /// OBIS_CODE = 1.0.3.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_T7 = 0x00030100030907FF,

        ///<summary>  param[57] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_T8  Class ID 3
        /// OBIS_CODE = 1.0.3.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_T8 = 0x00030100030908FF,

        #endregion // --057  DB_REACTIVE_ENERGY_POS_TL[1-8] --

        //===============================================================================

        #region --058 DB_REACTIVE_ENERGY_NEG_TL[1-8] --

        ///<summary>  param[58] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_TL  Class ID 3
        /// OBIS_CODE = 1.0.4.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_TL = 0x00030100040900FF,

        ///<summary>  param[58] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_T1  Class ID 3
        /// OBIS_CODE = 1.0.4.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_T1 = 0x00030100040901FF,

        ///<summary>  param[58] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_T2  Class ID 3
        /// OBIS_CODE = 1.0.4.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_T2 = 0x00030100040902FF,

        ///<summary>  param[58] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_T3  Class ID 3
        /// OBIS_CODE = 1.0.4.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_T3 = 0x00030100040903FF,

        ///<summary>  param[58] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_T4  Class ID 3
        /// OBIS_CODE = 1.0.4.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_T4 = 0x00030100040904FF,

        ///<summary>  param[58] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_T5  Class ID 3
        /// OBIS_CODE = 1.0.4.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_T5 = 0x00030100040905FF,

        ///<summary>  param[58] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_T6  Class ID 3
        /// OBIS_CODE = 1.0.4.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_T6 = 0x00030100040906FF,

        ///<summary>  param[58] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_T7  Class ID 3
        /// OBIS_CODE = 1.0.4.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_T7 = 0x00030100040907FF,

        ///<summary>  param[58] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_T8  Class ID 3
        /// OBIS_CODE = 1.0.4.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_T8 = 0x00030100040908FF,

        #endregion // --058  DB_REACTIVE_ENERGY_NEG_TL[1-8] --

        //===============================================================================

        #region --059 DB_APPARENT_ENERGY_POS_TL[1-8] --

        //**************//Index[59][0] Match OBIS => 0x00030100090900FF 

        //**************//Index[59][1] Match OBIS => 0x00030100090901FF 

        //**************//Index[59][2] Match OBIS => 0x00030100090902FF 

        //**************//Index[59][3] Match OBIS => 0x00030100090903FF 

        //**************//Index[59][4] Match OBIS => 0x00030100090904FF 

        ///<summary>  param[59] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_T5  Class ID 3
        /// OBIS_CODE = 1.0.9.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_T5 = 0x00030100090905FF,

        ///<summary>  param[59] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_T6  Class ID 3
        /// OBIS_CODE = 1.0.9.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_T6 = 0x00030100090906FF,

        ///<summary>  param[59] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_T7  Class ID 3
        /// OBIS_CODE = 1.0.9.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_T7 = 0x00030100090907FF,

        ///<summary>  param[59] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_T8  Class ID 3
        /// OBIS_CODE = 1.0.9.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_T8 = 0x00030100090908FF,

        #endregion // --059  DB_APPARENT_ENERGY_POS_TL[1-8] --

        //===============================================================================

        #region --060 DB_APPARENT_ENERGY_NEG_TL[1-8] --

        ///<summary>  param[60] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_TL  Class ID 3
        /// OBIS_CODE = 1.0.10.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_TL = 0x000301000A0900FF,

        ///<summary>  param[60] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_T1  Class ID 3
        /// OBIS_CODE = 1.0.10.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_T1 = 0x000301000A0901FF,

        ///<summary>  param[60] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_T2  Class ID 3
        /// OBIS_CODE = 1.0.10.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_T2 = 0x000301000A0902FF,

        ///<summary>  param[60] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_T3  Class ID 3
        /// OBIS_CODE = 1.0.10.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_T3 = 0x000301000A0903FF,

        ///<summary>  param[60] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_T4  Class ID 3
        /// OBIS_CODE = 1.0.10.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_T4 = 0x000301000A0904FF,

        ///<summary>  param[60] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_T5  Class ID 3
        /// OBIS_CODE = 1.0.10.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_T5 = 0x000301000A0905FF,

        ///<summary>  param[60] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_T6  Class ID 3
        /// OBIS_CODE = 1.0.10.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_T6 = 0x000301000A0906FF,

        ///<summary>  param[60] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_T7  Class ID 3
        /// OBIS_CODE = 1.0.10.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_T7 = 0x000301000A0907FF,

        ///<summary>  param[60] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_T8  Class ID 3
        /// OBIS_CODE = 1.0.10.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_T8 = 0x000301000A0908FF,

        #endregion // --060  DB_APPARENT_ENERGY_NEG_TL[1-8] --

        //===============================================================================

        #region --061 DB_ACTIVE_ENERGY_ABS_TL[1-8] --

        //**************//Index[61][0] Match OBIS => 0x000301000F0900FF 

        //**************//Index[61][1] Match OBIS => 0x000301000F0901FF 

        //**************//Index[61][2] Match OBIS => 0x000301000F0902FF 

        //**************//Index[61][3] Match OBIS => 0x000301000F0903FF 

        //**************//Index[61][4] Match OBIS => 0x000301000F0904FF 

        ///<summary>  param[61] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_T5  Class ID 3
        /// OBIS_CODE = 1.0.15.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_T5 = 0x000301000F0905FF,

        ///<summary>  param[61] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_T6  Class ID 3
        /// OBIS_CODE = 1.0.15.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_T6 = 0x000301000F0906FF,

        ///<summary>  param[61] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_T7  Class ID 3
        /// OBIS_CODE = 1.0.15.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_T7 = 0x000301000F0907FF,

        ///<summary>  param[61] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_T8  Class ID 3
        /// OBIS_CODE = 1.0.15.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_T8 = 0x000301000F0908FF,

        #endregion // --061  DB_ACTIVE_ENERGY_ABS_TL[1-8] --

        //===============================================================================

        #region --062 DB_ACTIVE_ENERGY_POS_P1_TL[1-8] --

        ///<summary>  param[62] -> value[TOTAL]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.21.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P1_TL = 0x00030100150900FF,

        ///<summary>  param[62] -> value[1]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.21.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P1_T1 = 0x00030100150901FF,

        ///<summary>  param[62] -> value[2]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.21.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P1_T2 = 0x00030100150902FF,

        ///<summary>  param[62] -> value[3]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.21.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P1_T3 = 0x00030100150903FF,

        ///<summary>  param[62] -> value[4]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.21.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P1_T4 = 0x00030100150904FF,

        ///<summary>  param[62] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.21.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P1_T5 = 0x00030100150905FF,

        ///<summary>  param[62] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.21.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P1_T6 = 0x00030100150906FF,

        ///<summary>  param[62] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.21.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P1_T7 = 0x00030100150907FF,

        ///<summary>  param[62] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.21.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P1_T8 = 0x00030100150908FF,

        #endregion // --062  DB_ACTIVE_ENERGY_POS_P1_TL[1-8] --

        //===============================================================================

        #region --063 DB_ACTIVE_ENERGY_NEG_P1_TL[1-8] --

        ///<summary>  param[63] -> value[TOTAL]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.22.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P1_TL = 0x00030100160900FF,

        ///<summary>  param[63] -> value[1]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.22.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P1_T1 = 0x00030100160901FF,

        ///<summary>  param[63] -> value[2]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.22.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P1_T2 = 0x00030100160902FF,

        ///<summary>  param[63] -> value[3]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.22.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P1_T3 = 0x00030100160903FF,

        ///<summary>  param[63] -> value[4]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.22.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P1_T4 = 0x00030100160904FF,

        ///<summary>  param[63] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.22.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P1_T5 = 0x00030100160905FF,

        ///<summary>  param[63] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.22.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P1_T6 = 0x00030100160906FF,

        ///<summary>  param[63] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.22.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P1_T7 = 0x00030100160907FF,

        ///<summary>  param[63] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.22.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P1_T8 = 0x00030100160908FF,

        #endregion // --063  DB_ACTIVE_ENERGY_NEG_P1_TL[1-8] --

        //===============================================================================

        #region --064 DB_REACTIVE_ENERGY_POS_P1_TL[1-8] --

        ///<summary>  param[64] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.23.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P1_TL = 0x00030100170900FF,

        ///<summary>  param[64] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.23.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P1_T1 = 0x00030100170901FF,

        ///<summary>  param[64] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.23.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P1_T2 = 0x00030100170902FF,

        ///<summary>  param[64] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.23.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P1_T3 = 0x00030100170903FF,

        ///<summary>  param[64] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.23.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P1_T4 = 0x00030100170904FF,

        ///<summary>  param[64] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.23.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P1_T5 = 0x00030100170905FF,

        ///<summary>  param[64] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.23.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P1_T6 = 0x00030100170906FF,

        ///<summary>  param[64] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.23.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P1_T7 = 0x00030100170907FF,

        ///<summary>  param[64] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.23.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P1_T8 = 0x00030100170908FF,

        #endregion // --064  DB_REACTIVE_ENERGY_POS_P1_TL[1-8] --

        //===============================================================================

        #region --065 DB_REACTIVE_ENERGY_Q1_P1_TL[1-8] --

        ///<summary>  param[65] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.25.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P1_TL = 0x00030100190900FF,

        ///<summary>  param[65] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.25.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P1_T1 = 0x00030100190901FF,

        ///<summary>  param[65] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.25.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P1_T2 = 0x00030100190902FF,

        ///<summary>  param[65] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.25.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P1_T3 = 0x00030100190903FF,

        ///<summary>  param[65] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.25.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P1_T4 = 0x00030100190904FF,

        ///<summary>  param[65] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.25.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P1_T5 = 0x00030100190905FF,

        ///<summary>  param[65] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.25.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P1_T6 = 0x00030100190906FF,

        ///<summary>  param[65] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.25.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P1_T7 = 0x00030100190907FF,

        ///<summary>  param[65] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.25.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P1_T8 = 0x00030100190908FF,

        #endregion // --065  DB_REACTIVE_ENERGY_Q1_P1_TL[1-8] --

        //===============================================================================

        #region --066 DB_REACTIVE_ENERGY_Q2_P1_TL[1-8] --

        ///<summary>  param[66] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.26.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P1_TL = 0x000301001A0900FF,

        ///<summary>  param[66] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.26.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P1_T1 = 0x000301001A0901FF,

        ///<summary>  param[66] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.26.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P1_T2 = 0x000301001A0902FF,

        ///<summary>  param[66] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.26.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P1_T3 = 0x000301001A0903FF,

        ///<summary>  param[66] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.26.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P1_T4 = 0x000301001A0904FF,

        ///<summary>  param[66] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.26.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P1_T5 = 0x000301001A0905FF,

        ///<summary>  param[66] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.26.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P1_T6 = 0x000301001A0906FF,

        ///<summary>  param[66] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.26.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P1_T7 = 0x000301001A0907FF,

        ///<summary>  param[66] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.26.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P1_T8 = 0x000301001A0908FF,

        #endregion // --066  DB_REACTIVE_ENERGY_Q2_P1_TL[1-8] --

        //===============================================================================

        #region --067 DB_REACTIVE_ENERGY_Q3_P1_TL[1-8] --

        ///<summary>  param[67] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.27.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P1_TL = 0x000301001B0900FF,

        ///<summary>  param[67] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.27.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P1_T1 = 0x000301001B0901FF,

        ///<summary>  param[67] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.27.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P1_T2 = 0x000301001B0902FF,

        ///<summary>  param[67] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.27.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P1_T3 = 0x000301001B0903FF,

        ///<summary>  param[67] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.27.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P1_T4 = 0x000301001B0904FF,

        ///<summary>  param[67] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.27.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P1_T5 = 0x000301001B0905FF,

        ///<summary>  param[67] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.27.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P1_T6 = 0x000301001B0906FF,

        ///<summary>  param[67] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.27.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P1_T7 = 0x000301001B0907FF,

        ///<summary>  param[67] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.27.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P1_T8 = 0x000301001B0908FF,

        #endregion // --067  DB_REACTIVE_ENERGY_Q3_P1_TL[1-8] --

        //===============================================================================

        #region --068 DB_REACTIVE_ENERGY_Q4_P1_TL[1-8] --

        ///<summary>  param[68] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.28.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P1_TL = 0x000301001C0900FF,

        ///<summary>  param[68] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.28.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P1_T1 = 0x000301001C0901FF,

        ///<summary>  param[68] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.28.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P1_T2 = 0x000301001C0902FF,

        ///<summary>  param[68] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.28.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P1_T3 = 0x000301001C0903FF,

        ///<summary>  param[68] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.28.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P1_T4 = 0x000301001C0904FF,

        ///<summary>  param[68] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.28.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P1_T5 = 0x000301001C0905FF,

        ///<summary>  param[68] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.28.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P1_T6 = 0x000301001C0906FF,

        ///<summary>  param[68] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.28.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P1_T7 = 0x000301001C0907FF,

        ///<summary>  param[68] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.28.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P1_T8 = 0x000301001C0908FF,

        #endregion // --068  DB_REACTIVE_ENERGY_Q4_P1_TL[1-8] --

        //===============================================================================

        #region --069 DB_APPARENT_ENERGY_POS_P1_TL[1-8] --

        ///<summary>  param[69] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.29.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P1_TL = 0x000301001D0900FF,

        ///<summary>  param[69] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.29.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P1_T1 = 0x000301001D0901FF,

        ///<summary>  param[69] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.29.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P1_T2 = 0x000301001D0902FF,

        ///<summary>  param[69] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.29.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P1_T3 = 0x000301001D0903FF,

        ///<summary>  param[69] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.29.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P1_T4 = 0x000301001D0904FF,

        ///<summary>  param[69] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.29.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P1_T5 = 0x000301001D0905FF,

        ///<summary>  param[69] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.29.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P1_T6 = 0x000301001D0906FF,

        ///<summary>  param[69] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.29.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P1_T7 = 0x000301001D0907FF,

        ///<summary>  param[69] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.29.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P1_T8 = 0x000301001D0908FF,

        #endregion // --069  DB_APPARENT_ENERGY_POS_P1_TL[1-8] --

        //===============================================================================

        #region --070 DB_APPARENT_ENERGY_NEG_P1_TL[1-8] --

        ///<summary>  param[70] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.30.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P1_TL = 0x000301001E0900FF,

        ///<summary>  param[70] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.30.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P1_T1 = 0x000301001E0901FF,

        ///<summary>  param[70] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.30.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P1_T2 = 0x000301001E0902FF,

        ///<summary>  param[70] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.30.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P1_T3 = 0x000301001E0903FF,

        ///<summary>  param[70] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.30.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P1_T4 = 0x000301001E0904FF,

        ///<summary>  param[70] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.30.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P1_T5 = 0x000301001E0905FF,

        ///<summary>  param[70] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.30.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P1_T6 = 0x000301001E0906FF,

        ///<summary>  param[70] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.30.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P1_T7 = 0x000301001E0907FF,

        ///<summary>  param[70] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.30.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P1_T8 = 0x000301001E0908FF,

        #endregion // --070  DB_APPARENT_ENERGY_NEG_P1_TL[1-8] --

        //===============================================================================

        #region --071 DB_POWERFACTOR_POS_P1_TL[1-8] --

        ///<summary>  param[71] -> value[TOTAL]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.33.0.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P1_TL = 0x00030100210000FF,

        ///<summary>  param[71] -> value[1]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.33.0.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P1_T1 = 0x00030100210001FF,

        ///<summary>  param[71] -> value[2]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.33.0.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P1_T2 = 0x00030100210002FF,

        ///<summary>  param[71] -> value[3]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.33.0.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P1_T3 = 0x00030100210003FF,

        ///<summary>  param[71] -> value[4]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.33.0.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P1_T4 = 0x00030100210004FF,

        ///<summary>  param[71] -> value[5]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.33.0.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P1_T5 = 0x00030100210005FF,

        ///<summary>  param[71] -> value[6]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.33.0.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P1_T6 = 0x00030100210006FF,

        ///<summary>  param[71] -> value[7]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.33.0.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P1_T7 = 0x00030100210007FF,

        ///<summary>  param[71] -> value[8]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.33.0.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P1_T8 = 0x00030100210008FF,

        #endregion // --071  DB_POWERFACTOR_POS_P1_TL[1-8] --

        //===============================================================================

        #region --072 DB_REACTIVE_ENERGY_NEG_P1_TL[1-8] --

        ///<summary>  param[72] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.34.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P1_TL = 0x00030100220900FF,

        ///<summary>  param[72] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.34.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P1_T1 = 0x00030100220901FF,

        ///<summary>  param[72] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.34.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P1_T2 = 0x00030100220902FF,

        ///<summary>  param[72] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.34.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P1_T3 = 0x00030100220903FF,

        ///<summary>  param[72] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.34.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P1_T4 = 0x00030100220904FF,

        ///<summary>  param[72] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.34.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P1_T5 = 0x00030100220905FF,

        ///<summary>  param[72] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.34.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P1_T6 = 0x00030100220906FF,

        ///<summary>  param[72] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.34.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P1_T7 = 0x00030100220907FF,

        ///<summary>  param[72] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.34.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P1_T8 = 0x00030100220908FF,

        #endregion // --072  DB_REACTIVE_ENERGY_NEG_P1_TL[1-8] --

        //===============================================================================

        #region --073 DB_ACTIVE_ENERGY_ABS_P1_TL[1-8] --

        ///<summary>  param[73] -> value[TOTAL]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.35.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P1_TL = 0x00030100230900FF,

        ///<summary>  param[73] -> value[1]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.35.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P1_T1 = 0x00030100230901FF,

        ///<summary>  param[73] -> value[2]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.35.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P1_T2 = 0x00030100230902FF,

        ///<summary>  param[73] -> value[3]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.35.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P1_T3 = 0x00030100230903FF,

        ///<summary>  param[73] -> value[4]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.35.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P1_T4 = 0x00030100230904FF,

        ///<summary>  param[73] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.35.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P1_T5 = 0x00030100230905FF,

        ///<summary>  param[73] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.35.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P1_T6 = 0x00030100230906FF,

        ///<summary>  param[73] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.35.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P1_T7 = 0x00030100230907FF,

        ///<summary>  param[73] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.35.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P1_T8 = 0x00030100230908FF,

        #endregion // --073  DB_ACTIVE_ENERGY_ABS_P1_TL[1-8] --

        //===============================================================================

        #region --074 DB_ACTIVE_ENERGY_POS_P2_TL[1-8] --

        ///<summary>  param[74] -> value[TOTAL]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.41.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P2_TL = 0x00030100290900FF,

        ///<summary>  param[74] -> value[1]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.41.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P2_T1 = 0x00030100290901FF,

        ///<summary>  param[74] -> value[2]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.41.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P2_T2 = 0x00030100290902FF,

        ///<summary>  param[74] -> value[3]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.41.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P2_T3 = 0x00030100290903FF,

        ///<summary>  param[74] -> value[4]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.41.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P2_T4 = 0x00030100290904FF,

        ///<summary>  param[74] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.41.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P2_T5 = 0x00030100290905FF,

        ///<summary>  param[74] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.41.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P2_T6 = 0x00030100290906FF,

        ///<summary>  param[74] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.41.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P2_T7 = 0x00030100290907FF,

        ///<summary>  param[74] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.41.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P2_T8 = 0x00030100290908FF,

        #endregion // --074  DB_ACTIVE_ENERGY_POS_P2_TL[1-8] --

        //===============================================================================

        #region --075 DB_ACTIVE_ENERGY_NEG_P2_TL[1-8] --

        ///<summary>  param[75] -> value[TOTAL]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.42.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P2_TL = 0x000301002A0900FF,

        ///<summary>  param[75] -> value[1]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.42.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P2_T1 = 0x000301002A0901FF,

        ///<summary>  param[75] -> value[2]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.42.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P2_T2 = 0x000301002A0902FF,

        ///<summary>  param[75] -> value[3]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.42.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P2_T3 = 0x000301002A0903FF,

        ///<summary>  param[75] -> value[4]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.42.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P2_T4 = 0x000301002A0904FF,

        ///<summary>  param[75] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.42.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P2_T5 = 0x000301002A0905FF,

        ///<summary>  param[75] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.42.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P2_T6 = 0x000301002A0906FF,

        ///<summary>  param[75] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.42.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P2_T7 = 0x000301002A0907FF,

        ///<summary>  param[75] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.42.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P2_T8 = 0x000301002A0908FF,

        #endregion // --075  DB_ACTIVE_ENERGY_NEG_P2_TL[1-8] --

        //===============================================================================

        #region --076 DB_REACTIVE_ENERGY_POS_P2_TL[1-8] --

        ///<summary>  param[76] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.43.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P2_TL = 0x000301002B0900FF,

        ///<summary>  param[76] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.43.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P2_T1 = 0x000301002B0901FF,

        ///<summary>  param[76] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.43.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P2_T2 = 0x000301002B0902FF,

        ///<summary>  param[76] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.43.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P2_T3 = 0x000301002B0903FF,

        ///<summary>  param[76] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.43.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P2_T4 = 0x000301002B0904FF,

        ///<summary>  param[76] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.43.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P2_T5 = 0x000301002B0905FF,

        ///<summary>  param[76] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.43.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P2_T6 = 0x000301002B0906FF,

        ///<summary>  param[76] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.43.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P2_T7 = 0x000301002B0907FF,

        ///<summary>  param[76] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.43.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P2_T8 = 0x000301002B0908FF,

        #endregion // --076  DB_REACTIVE_ENERGY_POS_P2_TL[1-8] --

        //===============================================================================

        #region --077 DB_REACTIVE_ENERGY_Q1_P2_TL[1-8] --

        ///<summary>  param[77] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.45.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P2_TL = 0x000301002D0900FF,

        ///<summary>  param[77] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.45.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P2_T1 = 0x000301002D0901FF,

        ///<summary>  param[77] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.45.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P2_T2 = 0x000301002D0902FF,

        ///<summary>  param[77] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.45.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P2_T3 = 0x000301002D0903FF,

        ///<summary>  param[77] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.45.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P2_T4 = 0x000301002D0904FF,

        ///<summary>  param[77] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.45.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P2_T5 = 0x000301002D0905FF,

        ///<summary>  param[77] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.45.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P2_T6 = 0x000301002D0906FF,

        ///<summary>  param[77] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.45.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P2_T7 = 0x000301002D0907FF,

        ///<summary>  param[77] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.45.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P2_T8 = 0x000301002D0908FF,

        #endregion // --077  DB_REACTIVE_ENERGY_Q1_P2_TL[1-8] --

        //===============================================================================

        #region --078 DB_REACTIVE_ENERGY_Q2_P2_TL[1-8] --

        ///<summary>  param[78] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.46.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P2_TL = 0x000301002E0900FF,

        ///<summary>  param[78] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.46.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P2_T1 = 0x000301002E0901FF,

        ///<summary>  param[78] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.46.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P2_T2 = 0x000301002E0902FF,

        ///<summary>  param[78] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.46.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P2_T3 = 0x000301002E0903FF,

        ///<summary>  param[78] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.46.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P2_T4 = 0x000301002E0904FF,

        ///<summary>  param[78] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.46.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P2_T5 = 0x000301002E0905FF,

        ///<summary>  param[78] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.46.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P2_T6 = 0x000301002E0906FF,

        ///<summary>  param[78] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.46.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P2_T7 = 0x000301002E0907FF,

        ///<summary>  param[78] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.46.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P2_T8 = 0x000301002E0908FF,

        #endregion // --078  DB_REACTIVE_ENERGY_Q2_P2_TL[1-8] --

        //===============================================================================

        #region --079 DB_REACTIVE_ENERGY_Q3_P2_TL[1-8] --

        ///<summary>  param[79] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.47.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P2_TL = 0x000301002F0900FF,

        ///<summary>  param[79] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.47.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P2_T1 = 0x000301002F0901FF,

        ///<summary>  param[79] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.47.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P2_T2 = 0x000301002F0902FF,

        ///<summary>  param[79] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.47.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P2_T3 = 0x000301002F0903FF,

        ///<summary>  param[79] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.47.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P2_T4 = 0x000301002F0904FF,

        ///<summary>  param[79] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.47.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P2_T5 = 0x000301002F0905FF,

        ///<summary>  param[79] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.47.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P2_T6 = 0x000301002F0906FF,

        ///<summary>  param[79] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.47.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P2_T7 = 0x000301002F0907FF,

        ///<summary>  param[79] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.47.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P2_T8 = 0x000301002F0908FF,

        #endregion // --079  DB_REACTIVE_ENERGY_Q3_P2_TL[1-8] --

        //===============================================================================

        #region --080 DB_REACTIVE_ENERGY_Q4_P2_TL[1-8] --

        ///<summary>  param[80] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.48.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P2_TL = 0x00030100300900FF,

        ///<summary>  param[80] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.48.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P2_T1 = 0x00030100300901FF,

        ///<summary>  param[80] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.48.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P2_T2 = 0x00030100300902FF,

        ///<summary>  param[80] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.48.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P2_T3 = 0x00030100300903FF,

        ///<summary>  param[80] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.48.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P2_T4 = 0x00030100300904FF,

        ///<summary>  param[80] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.48.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P2_T5 = 0x00030100300905FF,

        ///<summary>  param[80] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.48.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P2_T6 = 0x00030100300906FF,

        ///<summary>  param[80] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.48.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P2_T7 = 0x00030100300907FF,

        ///<summary>  param[80] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.48.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P2_T8 = 0x00030100300908FF,

        #endregion // --080  DB_REACTIVE_ENERGY_Q4_P2_TL[1-8] --

        //===============================================================================

        #region --081 DB_APPARENT_ENERGY_POS_P2_TL[1-8] --

        ///<summary>  param[81] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.49.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P2_TL = 0x00030100310900FF,

        ///<summary>  param[81] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.49.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P2_T1 = 0x00030100310901FF,

        ///<summary>  param[81] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.49.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P2_T2 = 0x00030100310902FF,

        ///<summary>  param[81] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.49.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P2_T3 = 0x00030100310903FF,

        ///<summary>  param[81] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.49.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P2_T4 = 0x00030100310904FF,

        ///<summary>  param[81] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.49.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P2_T5 = 0x00030100310905FF,

        ///<summary>  param[81] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.49.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P2_T6 = 0x00030100310906FF,

        ///<summary>  param[81] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.49.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P2_T7 = 0x00030100310907FF,

        ///<summary>  param[81] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.49.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P2_T8 = 0x00030100310908FF,

        #endregion // --081  DB_APPARENT_ENERGY_POS_P2_TL[1-8] --

        //===============================================================================

        #region --082 DB_APPARENT_ENERGY_NEG_P2_TL[1-8] --

        ///<summary>  param[82] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.50.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P2_TL = 0x00030100320900FF,

        ///<summary>  param[82] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.50.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P2_T1 = 0x00030100320901FF,

        ///<summary>  param[82] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.50.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P2_T2 = 0x00030100320902FF,

        ///<summary>  param[82] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.50.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P2_T3 = 0x00030100320903FF,

        ///<summary>  param[82] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.50.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P2_T4 = 0x00030100320904FF,

        ///<summary>  param[82] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.50.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P2_T5 = 0x00030100320905FF,

        ///<summary>  param[82] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.50.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P2_T6 = 0x00030100320906FF,

        ///<summary>  param[82] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.50.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P2_T7 = 0x00030100320907FF,

        ///<summary>  param[82] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.50.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P2_T8 = 0x00030100320908FF,

        #endregion // --082  DB_APPARENT_ENERGY_NEG_P2_TL[1-8] --

        //===============================================================================

        #region --083 DB_POWERFACTOR_POS_P2_TL[1-8] --

        ///<summary>  param[83] -> value[TOTAL]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.53.0.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P2_TL = 0x00030100350000FF,

        ///<summary>  param[83] -> value[1]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.53.0.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P2_T1 = 0x00030100350001FF,

        ///<summary>  param[83] -> value[2]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.53.0.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P2_T2 = 0x00030100350002FF,

        ///<summary>  param[83] -> value[3]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.53.0.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P2_T3 = 0x00030100350003FF,

        ///<summary>  param[83] -> value[4]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.53.0.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P2_T4 = 0x00030100350004FF,

        ///<summary>  param[83] -> value[5]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.53.0.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P2_T5 = 0x00030100350005FF,

        ///<summary>  param[83] -> value[6]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.53.0.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P2_T6 = 0x00030100350006FF,

        ///<summary>  param[83] -> value[7]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.53.0.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P2_T7 = 0x00030100350007FF,

        ///<summary>  param[83] -> value[8]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.53.0.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P2_T8 = 0x00030100350008FF,

        #endregion // --083  DB_POWERFACTOR_POS_P2_TL[1-8] --

        //===============================================================================

        #region --084 DB_REACTIVE_ENERGY_NEG_P2_TL[1-8] --

        ///<summary>  param[84] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.54.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P2_TL = 0x00030100360900FF,

        ///<summary>  param[84] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.54.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P2_T1 = 0x00030100360901FF,

        ///<summary>  param[84] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.54.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P2_T2 = 0x00030100360902FF,

        ///<summary>  param[84] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.54.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P2_T3 = 0x00030100360903FF,

        ///<summary>  param[84] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.54.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P2_T4 = 0x00030100360904FF,

        ///<summary>  param[84] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.54.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P2_T5 = 0x00030100360905FF,

        ///<summary>  param[84] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.54.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P2_T6 = 0x00030100360906FF,

        ///<summary>  param[84] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.54.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P2_T7 = 0x00030100360907FF,

        ///<summary>  param[84] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.54.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P2_T8 = 0x00030100360908FF,

        #endregion // --084  DB_REACTIVE_ENERGY_NEG_P2_TL[1-8] --

        //===============================================================================

        #region --085 DB_ACTIVE_ENERGY_ABS_P2_TL[1-8] --

        ///<summary>  param[85] -> value[TOTAL]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.55.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P2_TL = 0x00030100370900FF,

        ///<summary>  param[85] -> value[1]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.55.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P2_T1 = 0x00030100370901FF,

        ///<summary>  param[85] -> value[2]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.55.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P2_T2 = 0x00030100370902FF,

        ///<summary>  param[85] -> value[3]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.55.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P2_T3 = 0x00030100370903FF,

        ///<summary>  param[85] -> value[4]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.55.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P2_T4 = 0x00030100370904FF,

        ///<summary>  param[85] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.55.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P2_T5 = 0x00030100370905FF,

        ///<summary>  param[85] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.55.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P2_T6 = 0x00030100370906FF,

        ///<summary>  param[85] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.55.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P2_T7 = 0x00030100370907FF,

        ///<summary>  param[85] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.55.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P2_T8 = 0x00030100370908FF,

        #endregion // --085  DB_ACTIVE_ENERGY_ABS_P2_TL[1-8] --

        //===============================================================================

        #region --086 DB_ACTIVE_ENERGY_POS_P3_TL[1-8] --

        ///<summary>  param[86] -> value[TOTAL]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.61.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P3_TL = 0x000301003D0900FF,

        ///<summary>  param[86] -> value[1]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.61.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P3_T1 = 0x000301003D0901FF,

        ///<summary>  param[86] -> value[2]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.61.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P3_T2 = 0x000301003D0902FF,

        ///<summary>  param[86] -> value[3]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.61.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P3_T3 = 0x000301003D0903FF,

        ///<summary>  param[86] -> value[4]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.61.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P3_T4 = 0x000301003D0904FF,

        ///<summary>  param[86] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.61.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P3_T5 = 0x000301003D0905FF,

        ///<summary>  param[86] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.61.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P3_T6 = 0x000301003D0906FF,

        ///<summary>  param[86] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.61.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P3_T7 = 0x000301003D0907FF,

        ///<summary>  param[86] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_POS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.61.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_POS_P3_T8 = 0x000301003D0908FF,

        #endregion // --086  DB_ACTIVE_ENERGY_POS_P3_TL[1-8] --

        //===============================================================================

        #region --087 DB_ACTIVE_ENERGY_NEG_P3_TL[1-8] --

        ///<summary>  param[87] -> value[TOTAL]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.62.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P3_TL = 0x000301003E0900FF,

        ///<summary>  param[87] -> value[1]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.62.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P3_T1 = 0x000301003E0901FF,

        ///<summary>  param[87] -> value[2]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.62.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P3_T2 = 0x000301003E0902FF,

        ///<summary>  param[87] -> value[3]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.62.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P3_T3 = 0x000301003E0903FF,

        ///<summary>  param[87] -> value[4]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.62.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P3_T4 = 0x000301003E0904FF,

        ///<summary>  param[87] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.62.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P3_T5 = 0x000301003E0905FF,

        ///<summary>  param[87] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.62.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P3_T6 = 0x000301003E0906FF,

        ///<summary>  param[87] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.62.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P3_T7 = 0x000301003E0907FF,

        ///<summary>  param[87] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_NEG_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.62.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_NEG_P3_T8 = 0x000301003E0908FF,

        #endregion // --087  DB_ACTIVE_ENERGY_NEG_P3_TL[1-8] --

        //===============================================================================

        #region --088 DB_REACTIVE_ENERGY_POS_P3_TL[1-8] --

        ///<summary>  param[88] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.63.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P3_TL = 0x000301003F0900FF,

        ///<summary>  param[88] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.63.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P3_T1 = 0x000301003F0901FF,

        ///<summary>  param[88] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.63.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P3_T2 = 0x000301003F0902FF,

        ///<summary>  param[88] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.63.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P3_T3 = 0x000301003F0903FF,

        ///<summary>  param[88] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.63.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P3_T4 = 0x000301003F0904FF,

        ///<summary>  param[88] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.63.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P3_T5 = 0x000301003F0905FF,

        ///<summary>  param[88] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.63.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P3_T6 = 0x000301003F0906FF,

        ///<summary>  param[88] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.63.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P3_T7 = 0x000301003F0907FF,

        ///<summary>  param[88] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_POS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.63.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_POS_P3_T8 = 0x000301003F0908FF,

        #endregion // --088  DB_REACTIVE_ENERGY_POS_P3_TL[1-8] --

        //===============================================================================

        #region --089 DB_REACTIVE_ENERGY_Q1_P3_TL[1-8] --

        ///<summary>  param[89] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.65.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P3_TL = 0x00030100410900FF,

        ///<summary>  param[89] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.65.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P3_T1 = 0x00030100410901FF,

        ///<summary>  param[89] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.65.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P3_T2 = 0x00030100410902FF,

        ///<summary>  param[89] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.65.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P3_T3 = 0x00030100410903FF,

        ///<summary>  param[89] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.65.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P3_T4 = 0x00030100410904FF,

        ///<summary>  param[89] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.65.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P3_T5 = 0x00030100410905FF,

        ///<summary>  param[89] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.65.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P3_T6 = 0x00030100410906FF,

        ///<summary>  param[89] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.65.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P3_T7 = 0x00030100410907FF,

        ///<summary>  param[89] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q1_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.65.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q1_P3_T8 = 0x00030100410908FF,

        #endregion // --089  DB_REACTIVE_ENERGY_Q1_P3_TL[1-8] --

        //===============================================================================

        #region --090 DB_REACTIVE_ENERGY_Q2_P3_TL[1-8] --

        ///<summary>  param[90] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.66.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P3_TL = 0x00030100420900FF,

        ///<summary>  param[90] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.66.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P3_T1 = 0x00030100420901FF,

        ///<summary>  param[90] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.66.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P3_T2 = 0x00030100420902FF,

        ///<summary>  param[90] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.66.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P3_T3 = 0x00030100420903FF,

        ///<summary>  param[90] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.66.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P3_T4 = 0x00030100420904FF,

        ///<summary>  param[90] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.66.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P3_T5 = 0x00030100420905FF,

        ///<summary>  param[90] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.66.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P3_T6 = 0x00030100420906FF,

        ///<summary>  param[90] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.66.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P3_T7 = 0x00030100420907FF,

        ///<summary>  param[90] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q2_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.66.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q2_P3_T8 = 0x00030100420908FF,

        #endregion // --090  DB_REACTIVE_ENERGY_Q2_P3_TL[1-8] --

        //===============================================================================

        #region --091 DB_REACTIVE_ENERGY_Q3_P3_TL[1-8] --

        ///<summary>  param[91] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.67.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P3_TL = 0x00030100430900FF,

        ///<summary>  param[91] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.67.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P3_T1 = 0x00030100430901FF,

        ///<summary>  param[91] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.67.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P3_T2 = 0x00030100430902FF,

        ///<summary>  param[91] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.67.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P3_T3 = 0x00030100430903FF,

        ///<summary>  param[91] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.67.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P3_T4 = 0x00030100430904FF,

        ///<summary>  param[91] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.67.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P3_T5 = 0x00030100430905FF,

        ///<summary>  param[91] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.67.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P3_T6 = 0x00030100430906FF,

        ///<summary>  param[91] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.67.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P3_T7 = 0x00030100430907FF,

        ///<summary>  param[91] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q3_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.67.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q3_P3_T8 = 0x00030100430908FF,

        #endregion // --091  DB_REACTIVE_ENERGY_Q3_P3_TL[1-8] --

        //===============================================================================

        #region --092 DB_REACTIVE_ENERGY_Q4_P3_TL[1-8] --

        ///<summary>  param[92] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.68.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P3_TL = 0x00030100440900FF,

        ///<summary>  param[92] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.68.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P3_T1 = 0x00030100440901FF,

        ///<summary>  param[92] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.68.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P3_T2 = 0x00030100440902FF,

        ///<summary>  param[92] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.68.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P3_T3 = 0x00030100440903FF,

        ///<summary>  param[92] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.68.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P3_T4 = 0x00030100440904FF,

        ///<summary>  param[92] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.68.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P3_T5 = 0x00030100440905FF,

        ///<summary>  param[92] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.68.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P3_T6 = 0x00030100440906FF,

        ///<summary>  param[92] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.68.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P3_T7 = 0x00030100440907FF,

        ///<summary>  param[92] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_Q4_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.68.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_Q4_P3_T8 = 0x00030100440908FF,

        #endregion // --092  DB_REACTIVE_ENERGY_Q4_P3_TL[1-8] --

        //===============================================================================

        #region --093 DB_APPARENT_ENERGY_POS_P3_TL[1-8] --

        ///<summary>  param[93] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.69.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P3_TL = 0x00030100450900FF,

        ///<summary>  param[93] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.69.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P3_T1 = 0x00030100450901FF,

        ///<summary>  param[93] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.69.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P3_T2 = 0x00030100450902FF,

        ///<summary>  param[93] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.69.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P3_T3 = 0x00030100450903FF,

        ///<summary>  param[93] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.69.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P3_T4 = 0x00030100450904FF,

        ///<summary>  param[93] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.69.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P3_T5 = 0x00030100450905FF,

        ///<summary>  param[93] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.69.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P3_T6 = 0x00030100450906FF,

        ///<summary>  param[93] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.69.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P3_T7 = 0x00030100450907FF,

        ///<summary>  param[93] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_POS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.69.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_POS_P3_T8 = 0x00030100450908FF,

        #endregion // --093  DB_APPARENT_ENERGY_POS_P3_TL[1-8] --

        //===============================================================================

        #region --094 DB_APPARENT_ENERGY_NEG_P3_TL[1-8] --

        ///<summary>  param[94] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.70.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P3_TL = 0x00030100460900FF,

        ///<summary>  param[94] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.70.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P3_T1 = 0x00030100460901FF,

        ///<summary>  param[94] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.70.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P3_T2 = 0x00030100460902FF,

        ///<summary>  param[94] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.70.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P3_T3 = 0x00030100460903FF,

        ///<summary>  param[94] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.70.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P3_T4 = 0x00030100460904FF,

        ///<summary>  param[94] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.70.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P3_T5 = 0x00030100460905FF,

        ///<summary>  param[94] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.70.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P3_T6 = 0x00030100460906FF,

        ///<summary>  param[94] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.70.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P3_T7 = 0x00030100460907FF,

        ///<summary>  param[94] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_NEG_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.70.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_NEG_P3_T8 = 0x00030100460908FF,

        #endregion // --094  DB_APPARENT_ENERGY_NEG_P3_TL[1-8] --

        //===============================================================================

        #region --095 DB_POWERFACTOR_POS_P3_TL[1-8] --

        ///<summary>  param[95] -> value[TOTAL]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.73.0.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P3_TL = 0x00030100490000FF,

        ///<summary>  param[95] -> value[1]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.73.0.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P3_T1 = 0x00030100490001FF,

        ///<summary>  param[95] -> value[2]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.73.0.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P3_T2 = 0x00030100490002FF,

        ///<summary>  param[95] -> value[3]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.73.0.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P3_T3 = 0x00030100490003FF,

        ///<summary>  param[95] -> value[4]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.73.0.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P3_T4 = 0x00030100490004FF,

        ///<summary>  param[95] -> value[5]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.73.0.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P3_T5 = 0x00030100490005FF,

        ///<summary>  param[95] -> value[6]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.73.0.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P3_T6 = 0x00030100490006FF,

        ///<summary>  param[95] -> value[7]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.73.0.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P3_T7 = 0x00030100490007FF,

        ///<summary>  param[95] -> value[8]
        /// Description: Quantity_Name= DB_POWERFACTOR_POS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.73.0.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_POS_P3_T8 = 0x00030100490008FF,

        #endregion // --095  DB_POWERFACTOR_POS_P3_TL[1-8] --

        //===============================================================================

        #region --096 DB_REACTIVE_ENERGY_NEG_P3_TL[1-8] --

        ///<summary>  param[96] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.74.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P3_TL = 0x000301004A0900FF,

        ///<summary>  param[96] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.74.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P3_T1 = 0x000301004A0901FF,

        ///<summary>  param[96] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.74.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P3_T2 = 0x000301004A0902FF,

        ///<summary>  param[96] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.74.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P3_T3 = 0x000301004A0903FF,

        ///<summary>  param[96] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.74.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P3_T4 = 0x000301004A0904FF,

        ///<summary>  param[96] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.74.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P3_T5 = 0x000301004A0905FF,

        ///<summary>  param[96] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.74.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P3_T6 = 0x000301004A0906FF,

        ///<summary>  param[96] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.74.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P3_T7 = 0x000301004A0907FF,

        ///<summary>  param[96] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_NEG_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.74.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_NEG_P3_T8 = 0x000301004A0908FF,

        #endregion // --096  DB_REACTIVE_ENERGY_NEG_P3_TL[1-8] --

        //===============================================================================

        #region --097 DB_ACTIVE_ENERGY_ABS_P3_TL[1-8] --

        ///<summary>  param[97] -> value[TOTAL]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.75.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P3_TL = 0x000301004B0900FF,

        ///<summary>  param[97] -> value[1]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.75.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P3_T1 = 0x000301004B0901FF,

        ///<summary>  param[97] -> value[2]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.75.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P3_T2 = 0x000301004B0902FF,

        ///<summary>  param[97] -> value[3]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.75.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P3_T3 = 0x000301004B0903FF,

        ///<summary>  param[97] -> value[4]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.75.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P3_T4 = 0x000301004B0904FF,

        ///<summary>  param[97] -> value[5]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.75.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P3_T5 = 0x000301004B0905FF,

        ///<summary>  param[97] -> value[6]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.75.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P3_T6 = 0x000301004B0906FF,

        ///<summary>  param[97] -> value[7]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.75.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P3_T7 = 0x000301004B0907FF,

        ///<summary>  param[97] -> value[8]
        /// Description: Quantity_Name= DB_ACTIVE_ENERGY_ABS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.75.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_ACTIVE_ENERGY_ABS_P3_T8 = 0x000301004B0908FF,

        #endregion // --097  DB_ACTIVE_ENERGY_ABS_P3_TL[1-8] --

        //===============================================================================

        #region --098 DB_POWERFACTOR_NEG_P1_TL[1-8] --

        ///<summary>  param[98] -> value[TOTAL]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.85.0.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P1_TL = 0x00030100550000FF,

        ///<summary>  param[98] -> value[1]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.85.0.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P1_T1 = 0x00030100550001FF,

        ///<summary>  param[98] -> value[2]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.85.0.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P1_T2 = 0x00030100550002FF,

        ///<summary>  param[98] -> value[3]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.85.0.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P1_T3 = 0x00030100550003FF,

        ///<summary>  param[98] -> value[4]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.85.0.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P1_T4 = 0x00030100550004FF,

        ///<summary>  param[98] -> value[5]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.85.0.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P1_T5 = 0x00030100550005FF,

        ///<summary>  param[98] -> value[6]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.85.0.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P1_T6 = 0x00030100550006FF,

        ///<summary>  param[98] -> value[7]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.85.0.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P1_T7 = 0x00030100550007FF,

        ///<summary>  param[98] -> value[8]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.85.0.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P1_T8 = 0x00030100550008FF,

        #endregion // --098  DB_POWERFACTOR_NEG_P1_TL[1-8] --

        //===============================================================================

        #region --099 DB_POWERFACTOR_NEG_P2_TL[1-8] --

        ///<summary>  param[99] -> value[TOTAL]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.86.0.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P2_TL = 0x00030100560000FF,

        ///<summary>  param[99] -> value[1]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.86.0.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P2_T1 = 0x00030100560001FF,

        ///<summary>  param[99] -> value[2]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.86.0.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P2_T2 = 0x00030100560002FF,

        ///<summary>  param[99] -> value[3]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.86.0.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P2_T3 = 0x00030100560003FF,

        ///<summary>  param[99] -> value[4]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.86.0.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P2_T4 = 0x00030100560004FF,

        ///<summary>  param[99] -> value[5]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.86.0.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P2_T5 = 0x00030100560005FF,

        ///<summary>  param[99] -> value[6]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.86.0.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P2_T6 = 0x00030100560006FF,

        ///<summary>  param[99] -> value[7]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.86.0.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P2_T7 = 0x00030100560007FF,

        ///<summary>  param[99] -> value[8]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.86.0.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P2_T8 = 0x00030100560008FF,

        #endregion // --099  DB_POWERFACTOR_NEG_P2_TL[1-8] --

        //===============================================================================

        #region --100 DB_POWERFACTOR_NEG_P3_TL[1-8] --

        ///<summary>  param[100] -> value[TOTAL]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.87.0.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P3_TL = 0x00030100570000FF,

        ///<summary>  param[100] -> value[1]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.87.0.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P3_T1 = 0x00030100570001FF,

        ///<summary>  param[100] -> value[2]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.87.0.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P3_T2 = 0x00030100570002FF,

        ///<summary>  param[100] -> value[3]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.87.0.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P3_T3 = 0x00030100570003FF,

        ///<summary>  param[100] -> value[4]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.87.0.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P3_T4 = 0x00030100570004FF,

        ///<summary>  param[100] -> value[5]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.87.0.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P3_T5 = 0x00030100570005FF,

        ///<summary>  param[100] -> value[6]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.87.0.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P3_T6 = 0x00030100570006FF,

        ///<summary>  param[100] -> value[7]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.87.0.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P3_T7 = 0x00030100570007FF,

        ///<summary>  param[100] -> value[8]
        /// Description: Quantity_Name= DB_POWERFACTOR_NEG_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.87.0.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_POWERFACTOR_NEG_P3_T8 = 0x00030100570008FF,

        #endregion // --100  DB_POWERFACTOR_NEG_P3_TL[1-8] --

        //===============================================================================

        #region --101 DB_APPARENT_ENERGY_ABS_P1_TL[1-8] --

        ///<summary>  param[101] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.151.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P1_TL = 0x00030100970900FF,

        ///<summary>  param[101] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.151.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P1_T1 = 0x00030100970901FF,

        ///<summary>  param[101] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.151.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P1_T2 = 0x00030100970902FF,

        ///<summary>  param[101] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.151.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P1_T3 = 0x00030100970903FF,

        ///<summary>  param[101] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.151.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P1_T4 = 0x00030100970904FF,

        ///<summary>  param[101] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.151.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P1_T5 = 0x00030100970905FF,

        ///<summary>  param[101] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.151.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P1_T6 = 0x00030100970906FF,

        ///<summary>  param[101] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.151.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P1_T7 = 0x00030100970907FF,

        ///<summary>  param[101] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.151.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P1_T8 = 0x00030100970908FF,

        #endregion // --101  DB_APPARENT_ENERGY_ABS_P1_TL[1-8] --

        //===============================================================================

        #region --102 DB_APPARENT_ENERGY_ABS_P2_TL[1-8] --

        ///<summary>  param[102] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.152.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P2_TL = 0x00030100980900FF,

        ///<summary>  param[102] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.152.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P2_T1 = 0x00030100980901FF,

        ///<summary>  param[102] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.152.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P2_T2 = 0x00030100980902FF,

        ///<summary>  param[102] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.152.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P2_T3 = 0x00030100980903FF,

        ///<summary>  param[102] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.152.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P2_T4 = 0x00030100980904FF,

        ///<summary>  param[102] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.152.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P2_T5 = 0x00030100980905FF,

        ///<summary>  param[102] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.152.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P2_T6 = 0x00030100980906FF,

        ///<summary>  param[102] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.152.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P2_T7 = 0x00030100980907FF,

        ///<summary>  param[102] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.152.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P2_T8 = 0x00030100980908FF,

        #endregion // --102  DB_APPARENT_ENERGY_ABS_P2_TL[1-8] --

        //===============================================================================

        #region --103 DB_APPARENT_ENERGY_ABS_P3_TL[1-8] --

        ///<summary>  param[103] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.153.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P3_TL = 0x00030100990900FF,

        ///<summary>  param[103] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.153.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P3_T1 = 0x00030100990901FF,

        ///<summary>  param[103] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.153.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P3_T2 = 0x00030100990902FF,

        ///<summary>  param[103] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.153.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P3_T3 = 0x00030100990903FF,

        ///<summary>  param[103] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.153.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P3_T4 = 0x00030100990904FF,

        ///<summary>  param[103] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.153.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P3_T5 = 0x00030100990905FF,

        ///<summary>  param[103] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.153.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P3_T6 = 0x00030100990906FF,

        ///<summary>  param[103] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.153.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P3_T7 = 0x00030100990907FF,

        ///<summary>  param[103] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.153.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_P3_T8 = 0x00030100990908FF,

        #endregion // --103  DB_APPARENT_ENERGY_ABS_P3_TL[1-8] --

        //===============================================================================

        #region --104 DB_APPARENT_ENERGY_ABS_TL[1-8] --

        ///<summary>  param[104] -> value[TOTAL]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_TL  Class ID 3
        /// OBIS_CODE = 1.0.154.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_TL = 0x000301009A0900FF,

        ///<summary>  param[104] -> value[1]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_T1  Class ID 3
        /// OBIS_CODE = 1.0.154.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_T1 = 0x000301009A0901FF,

        ///<summary>  param[104] -> value[2]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_T2  Class ID 3
        /// OBIS_CODE = 1.0.154.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_T2 = 0x000301009A0902FF,

        ///<summary>  param[104] -> value[3]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_T3  Class ID 3
        /// OBIS_CODE = 1.0.154.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_T3 = 0x000301009A0903FF,

        ///<summary>  param[104] -> value[4]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_T4  Class ID 3
        /// OBIS_CODE = 1.0.154.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_T4 = 0x000301009A0904FF,

        ///<summary>  param[104] -> value[5]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_T5  Class ID 3
        /// OBIS_CODE = 1.0.154.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_T5 = 0x000301009A0905FF,

        ///<summary>  param[104] -> value[6]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_T6  Class ID 3
        /// OBIS_CODE = 1.0.154.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_T6 = 0x000301009A0906FF,

        ///<summary>  param[104] -> value[7]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_T7  Class ID 3
        /// OBIS_CODE = 1.0.154.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_T7 = 0x000301009A0907FF,

        ///<summary>  param[104] -> value[8]
        /// Description: Quantity_Name= DB_APPARENT_ENERGY_ABS_T8  Class ID 3
        /// OBIS_CODE = 1.0.154.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_APPARENT_ENERGY_ABS_T8 = 0x000301009A0908FF,

        #endregion // --104  DB_APPARENT_ENERGY_ABS_TL[1-8] --

        //===============================================================================

        #region --105 DB_REACTIVE_ENERGY_ABS_P1_TL[1-8] --

        ///<summary>  param[105] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P1_TL  Class ID 3
        /// OBIS_CODE = 1.0.155.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P1_TL = 0x000301009B0900FF,

        ///<summary>  param[105] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P1_T1  Class ID 3
        /// OBIS_CODE = 1.0.155.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P1_T1 = 0x000301009B0901FF,

        ///<summary>  param[105] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P1_T2  Class ID 3
        /// OBIS_CODE = 1.0.155.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P1_T2 = 0x000301009B0902FF,

        ///<summary>  param[105] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P1_T3  Class ID 3
        /// OBIS_CODE = 1.0.155.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P1_T3 = 0x000301009B0903FF,

        ///<summary>  param[105] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P1_T4  Class ID 3
        /// OBIS_CODE = 1.0.155.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P1_T4 = 0x000301009B0904FF,

        ///<summary>  param[105] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P1_T5  Class ID 3
        /// OBIS_CODE = 1.0.155.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P1_T5 = 0x000301009B0905FF,

        ///<summary>  param[105] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P1_T6  Class ID 3
        /// OBIS_CODE = 1.0.155.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P1_T6 = 0x000301009B0906FF,

        ///<summary>  param[105] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P1_T7  Class ID 3
        /// OBIS_CODE = 1.0.155.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P1_T7 = 0x000301009B0907FF,

        ///<summary>  param[105] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P1_T8  Class ID 3
        /// OBIS_CODE = 1.0.155.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P1_T8 = 0x000301009B0908FF,

        #endregion // --105  DB_REACTIVE_ENERGY_ABS_P1_TL[1-8] --

        //===============================================================================

        #region --106 DB_REACTIVE_ENERGY_ABS_P2_TL[1-8] --

        ///<summary>  param[106] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P2_TL  Class ID 3
        /// OBIS_CODE = 1.0.156.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P2_TL = 0x000301009C0900FF,

        ///<summary>  param[106] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P2_T1  Class ID 3
        /// OBIS_CODE = 1.0.156.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P2_T1 = 0x000301009C0901FF,

        ///<summary>  param[106] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P2_T2  Class ID 3
        /// OBIS_CODE = 1.0.156.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P2_T2 = 0x000301009C0902FF,

        ///<summary>  param[106] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P2_T3  Class ID 3
        /// OBIS_CODE = 1.0.156.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P2_T3 = 0x000301009C0903FF,

        ///<summary>  param[106] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P2_T4  Class ID 3
        /// OBIS_CODE = 1.0.156.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P2_T4 = 0x000301009C0904FF,

        ///<summary>  param[106] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P2_T5  Class ID 3
        /// OBIS_CODE = 1.0.156.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P2_T5 = 0x000301009C0905FF,

        ///<summary>  param[106] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P2_T6  Class ID 3
        /// OBIS_CODE = 1.0.156.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P2_T6 = 0x000301009C0906FF,

        ///<summary>  param[106] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P2_T7  Class ID 3
        /// OBIS_CODE = 1.0.156.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P2_T7 = 0x000301009C0907FF,

        ///<summary>  param[106] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P2_T8  Class ID 3
        /// OBIS_CODE = 1.0.156.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P2_T8 = 0x000301009C0908FF,

        #endregion // --106  DB_REACTIVE_ENERGY_ABS_P2_TL[1-8] --

        //===============================================================================

        #region --107 DB_REACTIVE_ENERGY_ABS_P3_TL[1-8] --

        ///<summary>  param[107] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P3_TL  Class ID 3
        /// OBIS_CODE = 1.0.157.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P3_TL = 0x000301009D0900FF,

        ///<summary>  param[107] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P3_T1  Class ID 3
        /// OBIS_CODE = 1.0.157.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P3_T1 = 0x000301009D0901FF,

        ///<summary>  param[107] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P3_T2  Class ID 3
        /// OBIS_CODE = 1.0.157.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P3_T2 = 0x000301009D0902FF,

        ///<summary>  param[107] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P3_T3  Class ID 3
        /// OBIS_CODE = 1.0.157.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P3_T3 = 0x000301009D0903FF,

        ///<summary>  param[107] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P3_T4  Class ID 3
        /// OBIS_CODE = 1.0.157.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P3_T4 = 0x000301009D0904FF,

        ///<summary>  param[107] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P3_T5  Class ID 3
        /// OBIS_CODE = 1.0.157.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P3_T5 = 0x000301009D0905FF,

        ///<summary>  param[107] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P3_T6  Class ID 3
        /// OBIS_CODE = 1.0.157.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P3_T6 = 0x000301009D0906FF,

        ///<summary>  param[107] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P3_T7  Class ID 3
        /// OBIS_CODE = 1.0.157.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P3_T7 = 0x000301009D0907FF,

        ///<summary>  param[107] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_P3_T8  Class ID 3
        /// OBIS_CODE = 1.0.157.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_P3_T8 = 0x000301009D0908FF,

        #endregion // --107  DB_REACTIVE_ENERGY_ABS_P3_TL[1-8] --

        //===============================================================================

        #region --108 DB_REACTIVE_ENERGY_ABS_TL[1-8] --

        ///<summary>  param[108] -> value[TOTAL]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_TL  Class ID 3
        /// OBIS_CODE = 1.0.158.9.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_TL = 0x000301009E0900FF,

        ///<summary>  param[108] -> value[1]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_T1  Class ID 3
        /// OBIS_CODE = 1.0.158.9.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_T1 = 0x000301009E0901FF,

        ///<summary>  param[108] -> value[2]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_T2  Class ID 3
        /// OBIS_CODE = 1.0.158.9.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_T2 = 0x000301009E0902FF,

        ///<summary>  param[108] -> value[3]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_T3  Class ID 3
        /// OBIS_CODE = 1.0.158.9.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_T3 = 0x000301009E0903FF,

        ///<summary>  param[108] -> value[4]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_T4  Class ID 3
        /// OBIS_CODE = 1.0.158.9.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_T4 = 0x000301009E0904FF,

        ///<summary>  param[108] -> value[5]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_T5  Class ID 3
        /// OBIS_CODE = 1.0.158.9.5.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_T5 = 0x000301009E0905FF,

        ///<summary>  param[108] -> value[6]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_T6  Class ID 3
        /// OBIS_CODE = 1.0.158.9.6.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_T6 = 0x000301009E0906FF,

        ///<summary>  param[108] -> value[7]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_T7  Class ID 3
        /// OBIS_CODE = 1.0.158.9.7.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_T7 = 0x000301009E0907FF,

        ///<summary>  param[108] -> value[8]
        /// Description: Quantity_Name= DB_REACTIVE_ENERGY_ABS_T8  Class ID 3
        /// OBIS_CODE = 1.0.158.9.8.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        DB_REACTIVE_ENERGY_ABS_T8 = 0x000301009E0908FF,

        #endregion // --108  DB_REACTIVE_ENERGY_ABS_TL[1-8] --

        //===============================================================================



        #endregion

        #region MDI Class 3 4 5 OBIS Codes

        #region --01 PM_DEMAND_ACTIVE_IMPORT --

        //**************//Index[1][PM_DEMAND_ACTIVE_IMPORT] Match OBIS => 0x00050100010400FF 

        #endregion

        #region --02 PM_DEMAND_ACTIVE_EXPORT --

        //**************//Index[2][PM_DEMAND_ACTIVE_EXPORT] Match OBIS => 0x00050100020400FF 

        #endregion

        #region --03 PM_DEMAND_REACTIVE_IMPORT --

        //**************//Index[3][PM_DEMAND_REACTIVE_IMPORT] Match OBIS => 0x00050100030400FF 

        #endregion

        #region --04 PM_DEMAND_REACTIVE_EXPORT --

        //**************//Index[4][PM_DEMAND_REACTIVE_EXPORT] Match OBIS => 0x00050100040400FF 

        #endregion

        #region --05 PM_DEMAND_ACTIVE_ABSOLUTE --

        //**************//Index[5][PM_DEMAND_ACTIVE_ABSOLUTE] Match OBIS => 0x000501000F0400FF 

        #endregion

        #region --06 PM_DEMAND_ALL --

        //**************//Index[6][PM_DEMAND_ALL] Match OBIS => 0x00050100810400FF 

        #endregion

        #region --07 PM_DEMAND_REACTIVE_ABSOLUTE --

        //**************//Index[7][PM_DEMAND_REACTIVE_ABSOLUTE] Match OBIS => 0x00050100800400FF 

        #endregion

        #region --08 IB_MONTHLY_ACTIVE_MDI_IMPORT_TOTAL[1-4] --

        //**************//Index[8][IB_MONTHLY_ACTIVE_MDI_IMPORT_TOTAL] Match OBIS => 0x00040100010600FF 

        //**************//Index[9][IB_MONTHLY_ACTIVE_MDI_IMPORT_T1] Match OBIS => 0x00040100010601FF 

        //**************//Index[10][IB_MONTHLY_ACTIVE_MDI_IMPORT_T2] Match OBIS => 0x00040100010602FF 

        //**************//Index[11][IB_MONTHLY_ACTIVE_MDI_IMPORT_T3] Match OBIS => 0x00040100010603FF 

        //**************//Index[12][IB_MONTHLY_ACTIVE_MDI_IMPORT_T4] Match OBIS => 0x00040100010604FF 

        #endregion

        #region --09 PB_MONTHLY_ACTIVE_MDI_IMPORT_TOTAL[1-4] --

        //**************//Index[13][PB_MONTHLY_ACTIVE_MDI_IMPORT_TOTAL] Match OBIS => 0x00040100604300FF 

        //**************//Index[14][PB_MONTHLY_ACTIVE_MDI_IMPORT_T1] Match OBIS => 0x00040100604301FF 

        //**************//Index[15][PB_MONTHLY_ACTIVE_MDI_IMPORT_T2] Match OBIS => 0x00040100604302FF 

        //**************//Index[16][PB_MONTHLY_ACTIVE_MDI_IMPORT_T3] Match OBIS => 0x00040100604303FF 

        //**************//Index[17][PB_MONTHLY_ACTIVE_MDI_IMPORT_T4] Match OBIS => 0x00040100604304FF 

        #endregion

        #region --10 IB_MONTHLY_ACTIVE_MDI_EXPORT_TOTAL[1-4] --

        //**************//Index[18][IB_MONTHLY_ACTIVE_MDI_EXPORT_TOTAL] Match OBIS => 0x00040100020600FF 

        //**************//Index[19][IB_MONTHLY_ACTIVE_MDI_EXPORT_T1] Match OBIS => 0x00040100020601FF 

        //**************//Index[20][IB_MONTHLY_ACTIVE_MDI_EXPORT_T2] Match OBIS => 0x00040100020602FF 

        //**************//Index[21][IB_MONTHLY_ACTIVE_MDI_EXPORT_T3] Match OBIS => 0x00040100020603FF 

        //**************//Index[22][IB_MONTHLY_ACTIVE_MDI_EXPORT_T4] Match OBIS => 0x00040100020604FF 

        #endregion

        #region --11 PB_MONTHLY_ACTIVE_MDI_EXPORT_TOTAL[1-4] --

        //**************//Index[23][PB_MONTHLY_ACTIVE_MDI_EXPORT_TOTAL] Match OBIS => 0x00040100604500FF 

        //**************//Index[24][PB_MONTHLY_ACTIVE_MDI_EXPORT_T1] Match OBIS => 0x00040100604501FF 

        //**************//Index[25][PB_MONTHLY_ACTIVE_MDI_EXPORT_T2] Match OBIS => 0x00040100604502FF 

        //**************//Index[26][PB_MONTHLY_ACTIVE_MDI_EXPORT_T3] Match OBIS => 0x00040100604503FF 

        //**************//Index[27][PB_MONTHLY_ACTIVE_MDI_EXPORT_T4] Match OBIS => 0x00040100604504FF 

        #endregion

        #region --12 IB_MONTHLY_ACTIVE_MDI_ABSOLUTE_TOTAL[1-4] --

        //**************//Index[28][IB_MONTHLY_ACTIVE_MDI_ABSOLUTE_TOTAL] Match OBIS => 0x000401000F0600FF 

        //**************//Index[29][IB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T1] Match OBIS => 0x000401000F0601FF 

        //**************//Index[30][IB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T2] Match OBIS => 0x000401000F0602FF 

        //**************//Index[31][IB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T3] Match OBIS => 0x000401000F0603FF 

        //**************//Index[32][IB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T4] Match OBIS => 0x000401000F0604FF 

        #endregion

        #region --13 PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_TOTAL[1-4] --

        //**************//Index[33][PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_TOTAL] Match OBIS => 0x00040100604700FF 

        //**************//Index[34][PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T1] Match OBIS => 0x00040100604701FF 

        //**************//Index[35][PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T2] Match OBIS => 0x00040100604702FF 

        //**************//Index[36][PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T3] Match OBIS => 0x00040100604703FF 

        //**************//Index[37][PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T4] Match OBIS => 0x00040100604704FF 

        #endregion

        #region --14 CB_ACTIVE_MDI_IMPORT_TOTAL[1-4] --

        //**************//Index[38][CB_ACTIVE_MDI_IMPORT_TOTAL] Match OBIS => 0x00030100010200FF 

        //**************//Index[39][CB_ACTIVE_MDI_IMPORT_T1] Match OBIS => 0x00030100010201FF 

        //**************//Index[40][CB_ACTIVE_MDI_IMPORT_T2] Match OBIS => 0x00030100010202FF 

        //**************//Index[41][CB_ACTIVE_MDI_IMPORT_T3] Match OBIS => 0x00030100010203FF 

        //**************//Index[42][CB_ACTIVE_MDI_IMPORT_T4] Match OBIS => 0x00030100010204FF 

        #endregion

        #region --15 CB_ACTIVE_MDI_EXPORT_TOTAL[1-4] --

        //**************//Index[43][CB_ACTIVE_MDI_EXPORT_TOTAL] Match OBIS => 0x00030100020200FF 

        //**************//Index[44][CB_ACTIVE_MDI_EXPORT_T1] Match OBIS => 0x00030100020201FF 

        //**************//Index[45][CB_ACTIVE_MDI_EXPORT_T2] Match OBIS => 0x00030100020202FF 

        //**************//Index[46][CB_ACTIVE_MDI_EXPORT_T3] Match OBIS => 0x00030100020203FF 

        //**************//Index[47][CB_ACTIVE_MDI_EXPORT_T4] Match OBIS => 0x00030100020204FF 

        #endregion

        #region --16 CB_REACTIVE_MDI_IMPORT_TOTAL[1-4] --

        //**************//Index[48][CB_REACTIVE_MDI_IMPORT_TOTAL] Match OBIS => 0x00030100030200FF 

        //**************//Index[49][CB_REACTIVE_MDI_IMPORT_T1] Match OBIS => 0x00030100030201FF 

        //**************//Index[50][CB_REACTIVE_MDI_IMPORT_T2] Match OBIS => 0x00030100030202FF 

        //**************//Index[51][CB_REACTIVE_MDI_IMPORT_T3] Match OBIS => 0x00030100030203FF 

        //**************//Index[52][CB_REACTIVE_MDI_IMPORT_T4] Match OBIS => 0x00030100030204FF 

        #endregion

        #region --17 CB_REACTIVE_MDI_EXPORT_TOTAL[1-4] --

        //**************//Index[53][CB_REACTIVE_MDI_EXPORT_TOTAL] Match OBIS => 0x00030100040200FF 

        //**************//Index[54][CB_REACTIVE_MDI_EXPORT_T1] Match OBIS => 0x00030100040201FF 

        //**************//Index[55][CB_REACTIVE_MDI_EXPORT_T2] Match OBIS => 0x00030100040202FF 

        //**************//Index[56][CB_REACTIVE_MDI_EXPORT_T3] Match OBIS => 0x00030100040203FF 

        //**************//Index[57][CB_REACTIVE_MDI_EXPORT_T4] Match OBIS => 0x00030100040204FF 

        #endregion

        #region --18 CB_ACTIVE_MDI_ABSOLUTE_TOTAL[1-4] --

        //**************//Index[58][CB_ACTIVE_MDI_ABSOLUTE_TOTAL] Match OBIS => 0x000301000F0200FF 

        //**************//Index[59][CB_ACTIVE_MDI_ABSOLUTE_T1] Match OBIS => 0x000301000F0201FF 

        //**************//Index[60][CB_ACTIVE_MDI_ABSOLUTE_T2] Match OBIS => 0x000301000F0202FF 

        //**************//Index[61][CB_ACTIVE_MDI_ABSOLUTE_T3] Match OBIS => 0x000301000F0203FF 

        //**************//Index[62][CB_ACTIVE_MDI_ABSOLUTE_T4] Match OBIS => 0x000301000F0204FF 

        #endregion

        #region --19 CB_REACTIVE_MDI_ABSOLUTE_TOTAL[1-4] --

        //**************//Index[63][CB_REACTIVE_MDI_ABSOLUTE_TOTAL] Match OBIS => 0x00030100800200FF 

        //**************//Index[64][CB_REACTIVE_MDI_ABSOLUTE_T1] Match OBIS => 0x00030100800201FF 

        //**************//Index[65][CB_REACTIVE_MDI_ABSOLUTE_T2] Match OBIS => 0x00030100800202FF 

        //**************//Index[66][CB_REACTIVE_MDI_ABSOLUTE_T3] Match OBIS => 0x00030100800203FF 

        //**************//Index[67][CB_REACTIVE_MDI_ABSOLUTE_T4] Match OBIS => 0x00030100800204FF 

        #endregion

        #region --20 PB_ACTIVE_MDI_ABSOLUTE_TOTAL[1-4] --

        ///<summary>  
        /// Description: Quantity_Name= PB_ACTIVE_MDI_ABSOLUTE_TOTAL  Class ID 3
        /// OBIS_CODE = 1.0.96.65.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        PB_ACTIVE_MDI_ABSOLUTE_TOTAL = 0x00030100604100FF,

        ///<summary>  
        /// Description: Quantity_Name= PB_ACTIVE_MDI_ABSOLUTE_T1  Class ID 3
        /// OBIS_CODE = 1.0.96.65.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        PB_ACTIVE_MDI_ABSOLUTE_T1 = 0x00030100604101FF,

        ///<summary>  
        /// Description: Quantity_Name= PB_ACTIVE_MDI_ABSOLUTE_T2  Class ID 3
        /// OBIS_CODE = 1.0.96.65.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        PB_ACTIVE_MDI_ABSOLUTE_T2 = 0x00030100604102FF,

        ///<summary>  
        /// Description: Quantity_Name= PB_ACTIVE_MDI_ABSOLUTE_T3  Class ID 3
        /// OBIS_CODE = 1.0.96.65.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        PB_ACTIVE_MDI_ABSOLUTE_T3 = 0x00030100604103FF,

        ///<summary>  
        /// Description: Quantity_Name= PB_ACTIVE_MDI_ABSOLUTE_T4  Class ID 3
        /// OBIS_CODE = 1.0.96.65.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        PB_ACTIVE_MDI_ABSOLUTE_T4 = 0x00030100604104FF,

        #endregion // --20  PB_ACTIVE_MDI_ABSOLUTE_TOTAL[1-4] --

        //===============================================================================

        #region --21 PB_REACTIVE_MDI_ABSOLUTE_TOTAL[1-4] --

        ///<summary>  
        /// Description: Quantity_Name= PB_REACTIVE_MDI_ABSOLUTE_TOTAL  Class ID 3
        /// OBIS_CODE = 1.0.96.66.0.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        PB_REACTIVE_MDI_ABSOLUTE_TOTAL = 0x00030100604200FF,

        ///<summary>  
        /// Description: Quantity_Name= PB_REACTIVE_MDI_ABSOLUTE_T1  Class ID 3
        /// OBIS_CODE = 1.0.96.66.1.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        PB_REACTIVE_MDI_ABSOLUTE_T1 = 0x00030100604201FF,

        ///<summary>  
        /// Description: Quantity_Name= PB_REACTIVE_MDI_ABSOLUTE_T2  Class ID 3
        /// OBIS_CODE = 1.0.96.66.2.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        PB_REACTIVE_MDI_ABSOLUTE_T2 = 0x00030100604202FF,

        ///<summary>  
        /// Description: Quantity_Name= PB_REACTIVE_MDI_ABSOLUTE_T3  Class ID 3
        /// OBIS_CODE = 1.0.96.66.3.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        PB_REACTIVE_MDI_ABSOLUTE_T3 = 0x00030100604203FF,

        ///<summary>  
        /// Description: Quantity_Name= PB_REACTIVE_MDI_ABSOLUTE_T4  Class ID 3
        /// OBIS_CODE = 1.0.96.66.4.255
        /// Description = 
        /// Data Type: 
        ///</summary>
        PB_REACTIVE_MDI_ABSOLUTE_T4 = 0x00030100604204FF,

        #endregion // --21  PB_REACTIVE_MDI_ABSOLUTE_TOTAL[1-4] --

        //===============================================================================

        #region --22 PB_MONTHLY_ACTIVE_MDI_IMPORT_TOTAL[1-4] --

        //**************//Index[78][PB_MONTHLY_ACTIVE_MDI_IMPORT_TOTAL] Match OBIS => 0x00040100604300FF 

        //**************//Index[79][PB_MONTHLY_ACTIVE_MDI_IMPORT_T1] Match OBIS => 0x00040100604301FF 

        //**************//Index[80][PB_MONTHLY_ACTIVE_MDI_IMPORT_T2] Match OBIS => 0x00040100604302FF 

        //**************//Index[81][PB_MONTHLY_ACTIVE_MDI_IMPORT_T3] Match OBIS => 0x00040100604303FF 

        //**************//Index[82][PB_MONTHLY_ACTIVE_MDI_IMPORT_T4] Match OBIS => 0x00040100604304FF 

        #endregion

        #region --23 PB_MONTHLY_REACTIVE_MDI_IMPORT_TOTAL[1-4] --

        //**************//Index[83][PB_MONTHLY_REACTIVE_MDI_IMPORT_TOTAL] Match OBIS => 0x00040100604400FF 

        //**************//Index[84][PB_MONTHLY_REACTIVE_MDI_IMPORT_T1] Match OBIS => 0x00040100604401FF 

        //**************//Index[85][PB_MONTHLY_REACTIVE_MDI_IMPORT_T2] Match OBIS => 0x00040100604402FF 

        //**************//Index[86][PB_MONTHLY_REACTIVE_MDI_IMPORT_T3] Match OBIS => 0x00040100604403FF 

        //**************//Index[87][PB_MONTHLY_REACTIVE_MDI_IMPORT_T4] Match OBIS => 0x00040100604404FF 

        #endregion

        #region --24 PB_MONTHLY_ACTIVE_MDI_EXPORT_TOTAL[1-4] --

        //**************//Index[88][PB_MONTHLY_ACTIVE_MDI_EXPORT_TOTAL] Match OBIS => 0x00040100604500FF 

        //**************//Index[89][PB_MONTHLY_ACTIVE_MDI_EXPORT_T1] Match OBIS => 0x00040100604501FF 

        //**************//Index[90][PB_MONTHLY_ACTIVE_MDI_EXPORT_T2] Match OBIS => 0x00040100604502FF 

        //**************//Index[91][PB_MONTHLY_ACTIVE_MDI_EXPORT_T3] Match OBIS => 0x00040100604503FF 

        //**************//Index[92][PB_MONTHLY_ACTIVE_MDI_EXPORT_T4] Match OBIS => 0x00040100604504FF 

        #endregion

        #region --25 PB_MONTHLY_REACTIVE_MDI_EXPORT_TOTAL[1-4] --

        //**************//Index[93][PB_MONTHLY_REACTIVE_MDI_EXPORT_TOTAL] Match OBIS => 0x00040100604600FF 

        //**************//Index[94][PB_MONTHLY_REACTIVE_MDI_EXPORT_T1] Match OBIS => 0x00040100604601FF 

        //**************//Index[95][PB_MONTHLY_REACTIVE_MDI_EXPORT_T2] Match OBIS => 0x00040100604602FF 

        //**************//Index[96][PB_MONTHLY_REACTIVE_MDI_EXPORT_T3] Match OBIS => 0x00040100604603FF 

        //**************//Index[97][PB_MONTHLY_REACTIVE_MDI_EXPORT_T4] Match OBIS => 0x00040100604604FF 

        #endregion

        #region --26 PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_TOTAL[1-4] --

        //**************//Index[98][PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_TOTAL] Match OBIS => 0x00040100604700FF 

        //**************//Index[99][PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T1] Match OBIS => 0x00040100604701FF 

        //**************//Index[100][PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T2] Match OBIS => 0x00040100604702FF 

        //**************//Index[101][PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T3] Match OBIS => 0x00040100604703FF 

        //**************//Index[102][PB_MONTHLY_ACTIVE_MDI_ABSOLUTE_T4] Match OBIS => 0x00040100604704FF 

        #endregion

        #region --27 PB_MONTHLY_REACTIVE_MDI_ABSOLUTE_TOTAL[1-4] --

        //**************//Index[103][PB_MONTHLY_REACTIVE_MDI_ABSOLUTE_TOTAL] Match OBIS => 0x00040100604800FF 

        //**************//Index[104][PB_MONTHLY_REACTIVE_MDI_ABSOLUTE_T1] Match OBIS => 0x00040100604801FF 

        //**************//Index[105][PB_MONTHLY_REACTIVE_MDI_ABSOLUTE_T2] Match OBIS => 0x00040100604802FF 

        //**************//Index[106][PB_MONTHLY_REACTIVE_MDI_ABSOLUTE_T3] Match OBIS => 0x00040100604803FF 

        //**************//Index[107][PB_MONTHLY_REACTIVE_MDI_ABSOLUTE_T4] Match OBIS => 0x00040100604804FF 


        #endregion

        #endregion

        ///<summary>
        /// Description: Quantity_Name= IN_REACTIVE_POWER_LIVE_CURRENT CLASS ID 3
        /// OBIS_CODE = 1.0.91.131.0.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        IN_REACTIVE_POWER_LIVE_CURRENT = 0x0301005B8300FF,

        ///<summary>
        /// Description: Quantity_Name= IN_REACTIVE_POWER_NEUTRAL_CURRENT CLASS ID 3
        /// OBIS_CODE = 1.0.91.132.0.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        IN_REACTIVE_POWER_NEUTRAL_CURRENT = 0x0301005B8400FF,

        ///<summary>
        /// Description: Quantity_Name= IN_TEMPERATURE CLASS ID 3
        /// OBIS_CODE = 1.0.96.9.0.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        IN_TEMPERATURE = 0x030100600900FF,

        ///<summary>
        /// Description: Quantity_Name= _OBIS_CB_MDI_RESET_COUNT2  CLASS ID 1
        /// OBIS_CODE = 1.0.0.1.6.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        _OBIS_CB_MDI_RESET_COUNT2 = 0x010100000106FF,


        ///<summary>
        /// Description: Quantity_Name= _OBIS_IN_LINE_CURRENT_RMS        CLASS ID 3
        /// OBIS_CODE = 1.0.90.7.0.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        _OBIS_IN_LINE_CURRENT_RMS = 0x0301005A0700FF,


        ///<summary>
        /// Description: Quantity_Name= Limits_Meter_ON_LOAD        CLASS ID 3
        /// OBIS_CODE = 1.0.1.128.0.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        Limits_Meter_ON_LOAD = 0x030100018000FF,

        ///<summary>
        /// Description: Quantity_Name= Limits_POWER_FACTOR_CHANGE        CLASS ID 3
        /// OBIS_CODE = 1.0.84.31.0.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        Limits_POWER_FACTOR_CHANGE = 0x030100541F00FF,

        ///<summary>
        /// Description: Quantity_Name= Limits_Crest_Factor_Low        CLASS ID 3
        /// OBIS_CODE = 1.0.133.31.0.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        Limits_Crest_Factor_Low = 0x030100851F00FF,

        ///<summary>
        /// Description: Quantity_Name= Limits_Crest_Factor_High        CLASS ID 3
        /// OBIS_CODE = 1.0.133.35.0.255
        /// Description
        /// Data Type:Enum
        ///</summary>
        Limits_Crest_Factor_High = 0x030100852300FF,

        ///<summary>
        /// Description: Quantity_Name= Param_LDR_Setting        CLASS ID 1
        /// OBIS_CODE = 0.0.26.1.128.255
        /// Description
        /// Data Type: Octet String
        ///</summary>
        Param_Ldr_Setting = 0x0100001A0180FF,

        //Event_Counter_14_F = 0x10000600F0EFF,
    }

}