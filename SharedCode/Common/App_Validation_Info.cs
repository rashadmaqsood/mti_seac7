using comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using System;
using System.Drawing;

namespace SharedCode.Common
{
    public class App_Validation_Info
    {
        #region Data_Members

        ///Param_Modem_Initialize
        private int _PinCode_Length = 9999;
        ///Param_ModemBasics_NEW
        private int _ModemBasics_Max_PasswordLength1 = 20;
        private int _ModemBasics_Min_PasswordLength1 = 0;
        ///Param_Keep_Alive_IP
        private ushort _Param_KA_Max_Ping_time = ushort.MaxValue;
        private ushort _Param_KA_Min_Ping_time = 30;
        ///Param_Communication_Profile
        private byte _Param_Comm_Profile_MinSelectedMode = 0;
        private byte _Param_Comm_Profile_MaxSelectedMode = 2;
        /// Param_Number_Profile_MinID
        public byte _Param_Number_Profile_MinId = 01;
        public byte _Param_Number_Profile_MaxId = 05;
        public byte _Param_Number_Profile_WakeUpType = 02;
        public String _Param_Number_DefaultProfile = "+920000000000";
        /// Param_Wakeup_Profile_MinID
        public byte _Param_WakeUp_Profile_MinId = 1;
        public byte _Param_WakeUp_Profile_MaxId = 4;
        /// Param_IP_Profile_MinID
        public byte _Param_IP_Profile_MinID = 1;
        public byte _Param_IP_Profile_MaxID = 4;
        public ushort _Param_IP_Profile_MinPortNumber = 0;
        public ushort _Param_IP_Profile_MaxPortNumber = ushort.MaxValue;
        #region ///Param_ModemLimitsAndTime

        ///Param_ModemLimitsAndTime
        ///Retry
        public byte Retry_SMS_Max = 255;
        public byte Retry_Max = 255;
        public byte Retry_IP_connection_Max = 255;
        public byte Retry_TCP_Max = 255;
        public byte Retry_UDP_Max = 255;
        ///Time b/w Retries
        public ushort RSSI_LEVEL_TCP_UDP_Connection_Max = ushort.MaxValue;
        public ushort RSSI_LEVEL_SMS_Max = ushort.MaxValue;
        public ushort RSSI_LEVEL_Data_Call_Max = ushort.MaxValue;
        public ushort Time_between_Retries_SMS_Max = ushort.MaxValue;
        public ushort Time_between_Retries_TCP_Max = ushort.MaxValue;
        public ushort Time_between_Retries_IP_connection_Max = ushort.MaxValue;
        public ushort Time_between_Retries_UDP_Max = ushort.MaxValue;
        public ushort Time_between_Retries_Data_Call_Max = ushort.MaxValue;

        public ushort TimeRetriesAlwaysOnCycle_Min = 20;
        public ushort TimeRetriesAlwaysOnCycle_Max = ushort.MaxValue;

        public ushort TCP_Inactivity_Min = 60;
        public ushort TCP_Inactivity_Max = ushort.MaxValue;

        public ushort TimeOut_CipSend_Min = 30;
        public ushort TimeOut_CipSend_Max = 300;

        #endregion
        ///Param_TimeBaseEvents
        public ushort _Param_TBE_MinInterval = 0;
        public ushort _Param_TBE_MaxInterval = 64800;
        public ushort _Param_TBE_MaxIntervalMisc = ushort.MaxValue;
        ///Param_Contactor
        public ushort Contactor_ON_Max_Pulse_Time = ushort.MaxValue;
        public ushort Contactor_OFF_Max_Pulse_Time = ushort.MaxValue;
        public ushort Minimum_Interval_Bw_Contactor_State_Change_Max = ushort.MaxValue;
        public ushort Power_Up_Delay_To_State_Change_Max = ushort.MaxValue;
        public uint Interval_Between_Retries_Max = 4000000000;
        public byte RetryCount_Max = 255;
        public byte Control_Mode_Max = 255;
        ///Param_Clock_Caliberation
        public double Xtal_freq_max = 32.982744;
        public double Xtal_freq_min = 32.553256;
        public const double Xtal_freq_Const = 32.768;
        public static readonly DateTime Param_Clock_Calib_DateMin = DateTime.Parse("1/1/1950 00:00:00");
        public static readonly DateTime Param_Clock_Calib_DateMax = DateTime.Parse("1/1/2551 00:00:00");
        ///Param_Decimal_Point
        ///<summary>
        ///DecimalPoint_BillingKWH Left4,Right3,Total7 Digit
        /// </summary>
        public byte[] DecimalPoint_BillingKWH = new byte[] { 4, 3, 7 };///Left4,Right3,Total7 Digit
        public byte[] DecimalPoint_BillingKWHMDI = new byte[] { 4, 3, 7 };///Left4,Right3,Total7 Digit
        public byte[] DecimalPoint_InstantaneousVoltage = new byte[] { 3, 2, 5 };///Left3,Right2,Total5 Digit
        public byte[] DecimalPoint_InstantaneousCurrent = new byte[] { 3, 2, 5 };///Left3,Right2,Total5 Digit
        public byte[] DecimalPoint_InstantaneousPower = new byte[] { 2, 3, 5 };///Left2,Right3,Total5 Digit
        public byte[] DecimalPoint_InstantaneousMDI = new byte[] { 2, 3, 5 };///Left2,Right3,Total5 Digit
        ///Param_CTPT_Ratio
        public ushort CTratio_Numerator_Max = 3600;
        public ushort CTratio_Denominator_Max = 3600;
        public ushort PTratio_Numerator_Max = 3600;
        public ushort PTratio_Denominator_Max = 3600;
        /// Param_password
        public int PasswordLength_Max = 19;
        public int PasswordLength_Min = 06;
        /// Param_Customer_Code
        public int CustomerCodeLength_Max = 16;
        public int CustomerCodeLength_Min = 06;
        /// Param_MDI_parameters
        public byte Interval_ManualReset_Max = 255;
        public byte Time_Unit_Max = 04;
        public uint MDI_Interval_Min = 01;
        public uint MDI_Interval_Max = 60;
        public byte[] MDI_Intervals = { 1, 5, 6, 10, 15, 30, 60 };
        public ushort Roll_slide_count_Min = 01;
        public ushort Roll_slide_count_Max = 06;
        ///DisplayWindowItem
        public byte ClassAttribute_Max = 20;
        public byte scrollTime_Min = 05;
        public byte scrollTime_Max = 255;
        public int WidowCount_Max = 50;
        ///LoadProfileChannelInfo
        public TimeSpan LP_IntervalMin = TimeSpan.FromMinutes(15);
        public TimeSpan LP_IntervalMax = TimeSpan.FromHours(24);
        /// LoadProfile Intervals(Minutes)
        public ushort[] LP_Intervals = new ushort[] { 15, 30, 60, 120, 180, 240, 360, 480, 720, 1440 };
        public int MaxLPChannelCount = 0x04;
        /// LimitValues
        public LimitValues _LimitValues = new LimitValues();///Init Default Limit Values;R326 meter
        public MonitoringTimeValues _MonitorTimeValues = new MonitoringTimeValues();///Init Default MonitorTime Values;R326 meter

        public Color ValidatedColorScheme = Color.Black;
        public Color InValidatedColorScheme = Color.Red;


        #endregion

        #region Properties

        ///Param_ModemBasics_NEW
        public int ModemBasics_Max_PasswordLength1
        {
            get { return _ModemBasics_Max_PasswordLength1; }
            internal set { _ModemBasics_Max_PasswordLength1 = value; }
        }

        public int ModemBasics_Min_PasswordLength1
        {
            get { return _ModemBasics_Min_PasswordLength1; }
            internal set { _ModemBasics_Min_PasswordLength1 = value; }
        }

        ///Param_Modem_Initialize
        public int PinCode_Length
        {
            get { return _PinCode_Length; }
            internal set { _PinCode_Length = value; }
        }

        ///Param_Keep_Alive_IP
        public ushort Param_KA_MaxPingtime
        {
            get { return _Param_KA_Max_Ping_time; }
            set { _Param_KA_Max_Ping_time = value; }
        }

        public ushort Param_KA_MinPingtime
        {
            get { return _Param_KA_Min_Ping_time; }
            set { _Param_KA_Min_Ping_time = value; }
        }

        ///Param_Communication_Profile
        public byte Param_Comm_Profile_MaxSelectedMode
        {
            get { return _Param_Comm_Profile_MaxSelectedMode; }
            set { _Param_Comm_Profile_MaxSelectedMode = value; }
        }

        public byte Param_Comm_Profile_MinSelectedMode
        {
            get { return _Param_Comm_Profile_MinSelectedMode; }
            set { _Param_Comm_Profile_MinSelectedMode = value; }
        }

        public ushort PPM_Max
        {
            get
            {
                ushort ppm_calc = 0;
                try
                {
                    ///formula to convert XTAL into PPM
                    double ppm = ((Xtal_freq_max - (Xtal_freq_Const + 00.000001)) / Xtal_freq_Const) * 1000000 * 10;
                    ppm_calc = Convert.ToUInt16(Math.Abs(ppm));
                }
                catch (Exception ex)
                {
                    throw new OverflowException("PPM_Max", ex);
                }
                return ppm_calc;
            }
        }

        #endregion

    }
}
