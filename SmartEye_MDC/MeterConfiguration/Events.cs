using System;
using System.Data;
using comm;
using DatabaseManager.Database;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.Param;
using SharedCode.Comm.DataContainer;
using SharedCode.Controllers;
using SharedCode.Comm.HelperClasses;

namespace Communicator.MeterConfiguration
{
    public class Events
    {
        #region Data Members
        private Param_TimeBaseEvents TBE1 = null;
        private Param_TimeBaseEvents TBE2 = null;


        public ParameterController Param_Controller
        {
            get { return _Param_Controller; }
            set { _Param_Controller = value; }
        }
        private DatabaseController _DBController = null;
        private TB_Events TB_Events_1 = null;
        private TB_Events TB_Events_2 = null;


        const int Tb_Disable = 0;
        const int Tb_DateTime = 1;
        const int Tb_Interval = 2;
        const int Tb_IntervalTimeSink = 3;
        const int Tb_Fixed = 4;

        public ushort tbe1_min = 0;
        public ushort tbe1_sec = 0;
        public ushort tbe2_min = 0;
        public ushort tbe2_sec = 0;
        #endregion

        #region Properties

        private ParameterController _Param_Controller = null;

        public DatabaseController DBController
        {
            get { return _DBController; }
            set { _DBController = value; }
        }
        #endregion

        public Events()
        {
            TB_Events_1 = new TB_Events();
            TB_Events_2 = new TB_Events();
            TBE1 = new Param_TimeBaseEvents();
            TBE2 = new Param_TimeBaseEvents();
        }

        #region Member Functions
        #region SET
        public void SET_TimeBaseEvent_1(long TBEDetailID)
        {
            try
            {
                ReadTBE(TBEDetailID, TB_Events_1);
                if (TB_Events_1.Control.Equals(Tb_DateTime)) //dateTime selected
                {
                    if (isDateTimeValid(TB_Events_1.DT_Chooser))
                    {

                        TBE1.DateTime.Year = (ushort)TB_Events_1.DT_Chooser.Year;
                        TBE1.DateTime.Month = (byte)TB_Events_1.DT_Chooser.Month;
                        TBE1.DateTime.DayOfMonth = (byte)TB_Events_1.DT_Chooser.Date;
                        TBE1.DateTime.DayOfWeek = (byte)TB_Events_1.DT_Chooser.DayOfWeek;
                        TBE1.DateTime.Hour = (byte)TB_Events_1.DT_Chooser.Hours;
                        TBE1.DateTime.Minute = (byte)TB_Events_1.DT_Chooser.Minutes;
                        TBE1.DateTime.Second = (byte)TB_Events_1.DT_Chooser.Seconds;
                    }
                    else
                    {
                        //Notification n = new Notification("Error", "Date time entered is INVALID. Year, Month and DayofMonth can not be Wild card entries");
                        return;
                    }
                }
                else if (TB_Events_1.Control.Equals(Tb_Interval))
                {
                    TBE1.Interval = (ushort)(TB_Events_1.Interval_TimeSpan.Hours * 60 * 60 + TB_Events_1.Interval_TimeSpan.Minutes * 60 + TB_Events_1.Interval_TimeSpan.Seconds);
                }
                else if (TB_Events_1.Control.Equals(Tb_IntervalTimeSink))
                {
                    tbe1_min = (ushort)(TB_Events_1.Interval_Sink_Minutes << 8);
                    tbe1_sec = (ushort)(TB_Events_1.Interval_Sink_Seconds);
                    TBE1.Interval = (ushort)(tbe1_min + tbe1_sec);
                }
                else if (TB_Events_1.Control.Equals(Tb_Fixed)) //fixed
                {
                    tbe1_min = (ushort)(TB_Events_1.Interval_Fixed_Minutes << 8);
                    tbe1_sec = (ushort)(TB_Events_1.Interval_Fixed_Seconds);
                    TBE1.Interval = (ushort)(tbe1_min + tbe1_sec);
                }
                else if (TB_Events_1.Control.Equals(Tb_Disable)) //disabled
                {
                    ;
                }
                TBE1.Control_Enum = (byte)TB_Events_1.Control;
                Data_Access_Result temp = Param_Controller.SET_TimeBaseEvents(TBE1, Get_Index._Time_Based_Event_1);

                //Notification Notifier = new Notification(String.Format("{0},Process Completed", this.Event_Controller.CurrentConnectionInfo.MSN),
                //String.Format("SET Time Based Event 1 Successful", temp));
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Datetime entered is INVALID");
            }
            finally
            {
            }
        }

        public void SET_TimeBaseEvent_2(long TBEDetailID)
        {
            try
            {
                ReadTBE(TBEDetailID, TB_Events_2);
                if (TB_Events_2.Control.Equals(Tb_DateTime)) //dateTime selected
                {
                    if (isDateTimeValid(TB_Events_2.DT_Chooser))
                    {

                        TBE2.DateTime.Year = (ushort)TB_Events_2.DT_Chooser.Year;
                        TBE2.DateTime.Month = (byte)TB_Events_2.DT_Chooser.Month;
                        TBE2.DateTime.DayOfMonth = (byte)TB_Events_2.DT_Chooser.Date;
                        TBE2.DateTime.DayOfWeek = (byte)TB_Events_2.DT_Chooser.DayOfWeek;
                        TBE2.DateTime.Hour = (byte)TB_Events_2.DT_Chooser.Hours;
                        TBE2.DateTime.Minute = (byte)TB_Events_2.DT_Chooser.Minutes;
                        TBE2.DateTime.Second = (byte)TB_Events_2.DT_Chooser.Seconds;
                    }
                    else
                    {
                        //Notification n = new Notification("Error", "Datetime entered is INVALID. Year, Month and DayofMonth can not be Wild card entries");
                        return;
                    }
                }
                else if (TB_Events_2.Control.Equals(Tb_Interval))
                {
                    TBE2.Interval = (ushort)(TB_Events_2.Interval_TimeSpan.Hours  * 60 * 60 + TB_Events_2.Interval_TimeSpan.Minutes * 60 + TB_Events_2.Interval_TimeSpan.Seconds);
                }
                else if (TB_Events_2.Control.Equals(Tb_IntervalTimeSink))
                {
                    tbe2_min = (ushort)(TB_Events_2.Interval_Sink_Minutes << 8);
                    tbe2_sec = (ushort)(TB_Events_2.Interval_Sink_Seconds);
                    TBE2.Interval = (ushort)(tbe2_min + tbe2_sec);
                }
                else if (TB_Events_2.Control.Equals(Tb_Fixed)) //fixed
                {
                    tbe2_min = (ushort)(TB_Events_2.Interval_Fixed_Minutes << 8);
                    tbe2_sec = (ushort)(TB_Events_2.Interval_Fixed_Seconds);
                    TBE2.Interval = (ushort)(tbe2_min + tbe2_sec);
                }
                else if (TB_Events_2.Control.Equals(Tb_Disable)) //disabled
                {
                    ;
                }
                TBE2.Control_Enum = (byte)TB_Events_2.Control;
                Data_Access_Result temp = Param_Controller.SET_TimeBaseEvents(TBE2, Get_Index._Time_Based_Event_2);

                //Notification Notifier = new Notification(String.Format("{0},Process Completed", this.Event_Controller.CurrentConnectionInfo.MSN),
                //String.Format("SET Time Based Event 2 Successful", temp));
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Datetime entered is INVALID");
            }
            finally
            {
            }
        }

        public bool SET_Disable_TBE_on_PowerFail(TBE_PowerFail objTBEPowerFail) 
        {
            var result =Param_Controller.SET_TBE_PowerFAil(objTBEPowerFail);
            return (result == Data_Access_Result.Success);
        }

        #endregion

        #region READ
        public void ReadTBE(long TBEDetailID, TB_Events TBEEvent)
        {
            DataTable DT = DBController.GetTBERequestDetailFromDatabases(TBEDetailID);
            TBEEvent.Control = Convert.ToInt16(DT.Rows[0][DT.Columns["control"]]);
            TBEEvent.DT_Chooser.Year = Convert.ToInt32(DT.Rows[0][DT.Columns["datetime_year"]]);
            TBEEvent.DT_Chooser.Month = Convert.ToInt32(DT.Rows[0][DT.Columns["datetime_month"]]);
            TBEEvent.DT_Chooser.DayOfMonth = Convert.ToInt32(DT.Rows[0][DT.Columns["datetime_day_of_month"]]);
            TBEEvent.DT_Chooser.DayOfWeek = Convert.ToInt32(DT.Rows[0][DT.Columns["datetime_day_of_week"]]);
            TBEEvent.DT_Chooser.Hours = Convert.ToInt32(DT.Rows[0][DT.Columns["datetime_hours"]]);
            TBEEvent.DT_Chooser.Minutes = Convert.ToInt32(DT.Rows[0][DT.Columns["datetime_minutes"]]);
            TBEEvent.DT_Chooser.Seconds = Convert.ToInt32(DT.Rows[0][DT.Columns["datetime_seconds"]]);
            TBEEvent.Interval_TimeSpan = TimeSpan.Parse(DT.Rows[0][DT.Columns["interval_timespan"]].ToString());
            TBEEvent.Interval_Sink_Minutes = Convert.ToUInt16(DT.Rows[0][DT.Columns["interval_sink_minutes"]]);
            TBEEvent.Interval_Sink_Seconds = Convert.ToUInt16(DT.Rows[0][DT.Columns["interval_sink_seconds"]]);
            TBEEvent.Interval_Fixed_Minutes = Convert.ToUInt16(DT.Rows[0][DT.Columns["interval_fixed_minutes"]]);
            TBEEvent.Interval_Fixed_Seconds = Convert.ToUInt16(DT.Rows[0][DT.Columns["interval_fixed_seconds"]]);
        }
        #endregion
        #endregion

        #region Support Functions

        private bool isDateTimeValid(DateTimeChooser Dt)
        {
            //if (Dt.Year == StDateTime.NullYear && Dt.Month == StDateTime.Null && Dt.Date == StDateTime.Null && Dt.DayOfWeek == StDateTime.Null)
            //{
            //    return false;
            //}
            if (Dt.Year != StDateTime.NullYear && Dt.Month != StDateTime.Null && Dt.Month != StDateTime.DaylightSavingBegin && Dt.Month != StDateTime.DaylightSavingEnd && Dt.Date != StDateTime.LastDayOfMonth
                && Dt.Date != StDateTime.SecondLastDayOfMonth && Dt.Date != StDateTime.Null)
            {
                DateTime actualDate = new DateTime(Dt.Year, Dt.Month, Dt.Date);

                if (Dt.DayOfWeek == StDateTime.Null)
                {
                    return true;
                }
                else
                {
                    switch (Dt.DayOfWeek)
                    {
                        case 1:
                            if (actualDate.DayOfWeek == DayOfWeek.Monday)
                                return true;
                            else return false;
                        case 2:
                            if (actualDate.DayOfWeek == DayOfWeek.Tuesday)
                                return true;
                            else return false;
                        case 3:
                            if (actualDate.DayOfWeek == DayOfWeek.Wednesday)
                                return true;
                            else return false;
                        case 4:
                            if (actualDate.DayOfWeek == DayOfWeek.Thursday)
                                return true;
                            else return false;
                        case 5:
                            if (actualDate.DayOfWeek == DayOfWeek.Friday)
                                return true;
                            else return false;
                        case 6:
                            if (actualDate.DayOfWeek == DayOfWeek.Saturday)
                                return true;
                            else return false;
                        case 7:
                            if (actualDate.DayOfWeek == DayOfWeek.Sunday)
                                return true;
                            else return false;
                    }

                    return false;

                }
            }
            else
            {
                return true;
            }
        }
        
        #endregion
    }
}
