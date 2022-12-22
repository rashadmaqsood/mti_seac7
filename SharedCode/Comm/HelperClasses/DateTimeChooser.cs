
namespace SharedCode.Comm.HelperClasses
{
    public class DateTimeChooser
    {
        int year, month, dayOfMonth, daysInMonth, dayOfWeek, hours, minutes, seconds, hundredth;

        bool Year_NS = true;
        bool month_NS = false, month_savingsBegin = false, month_savingsEnd = false;
        bool date_NS = false, date_lastDay = false, date_2ndLastDay = false, date_reserved = false;
        bool panel_date = true, panel_time = true;
        bool time_hourNS = false, time_minutesNS = false, time_secondsNS = false, time_hundredthNS = false;
        #region Time Properties

        public bool hour_NotSpecified
        {
            get
            {
                return this.time_hourNS;
            }
            set
            {
                if (value) this.time_hourNS = true;
                else this.time_hourNS = false;
            }
        }

        public bool minute_NotSpecified
        {
            get
            {
                return this.time_minutesNS;
            }
            set
            {
                if (value) this.time_minutesNS = true;
                else this.time_minutesNS = false;
            }
        }

        public bool second_NotSpecified
        {
            get
            {
                return this.time_secondsNS;
            }
            set
            {
                if (value) this.time_secondsNS = true;
                else this.time_secondsNS = false;
            }
        }

        public bool hundredth_NotSpecified
        {
            get
            {
                return this.time_hundredthNS;
            }
            set
            {
                if (value) this.time_hundredthNS = true;
                else this.time_hundredthNS = false;
            }
        }

        public int Hours
        {
            get
            {
                return this.hours;
            }
            set
            {
                this.hours = value;
            }
        }

        public int Minutes
        {
            get
            {
                return this.minutes;
            }
            set
            {
                this.minutes = value;
            }
        }

        public int Seconds
        {
            get
            {
                return this.seconds;
            }
            set
            {
                this.seconds = value;
            }
        }

        public int Hundredth
        {
            get
            {
                return this.hundredth;
            }
            set
            {
                this.hundredth = value;
            }
        }

        #endregion

        #region Date Properties

        #region Year
        public bool Year_NotSpecified
        {
            get
            {
                return this.Year_NS;
            }
            set
            {
                if (value) ///true
                    this.Year_NS = true;
                else
                    this.Year_NS = false;
            }
        }


        #endregion

        #region Month
        public bool Month_NotSpecified
        {
            get
            {
                return this.month_NS;
            }
            set
            {
                if (value) ///true
                    this.month_NS = true;
                else
                    this.month_NS = false;
            }
        }

        public bool Month_SavingBegins
        {
            get
            {
                return this.month_savingsBegin;
            }
            set
            {
                if (value) this.month_savingsBegin = true;
                else this.month_savingsBegin = false;
            }
        }

        public bool Month_SavingEnds
        {
            get
            {
                return this.month_savingsEnd;
            }
            set
            {
                if (value) this.month_savingsEnd = true;
                else this.month_savingsEnd = false;
            }
        }
        #endregion

        #region Date
        public bool Date_2ndLastDay
        {
            get
            {
                return this.date_2ndLastDay;
            }
            set
            {
                if (value)//true
                    this.date_2ndLastDay = true;
                else this.date_2ndLastDay = false;
            }
        }

        public bool Date_lastDay
        {
            get
            {
                return this.date_lastDay;
            }
            set
            {
                if (value) this.date_lastDay = true;
                else this.date_lastDay = false;
            }
        }

        public bool Date_NotSpecified
        {
            get
            {
                // return combo_Date.Items.Contains("Any");
                return this.date_NS;
            }
            set
            {
                if (value) ///true
                    //combo_Date.Items[0] = "Any";
                    this.date_NS = true;
                else
                    // combo_Date.Items.Remove("Any");
                    this.date_NS = false;

            }
        }

        public bool Date_reserved
        {
            get
            {
                return this.date_reserved;
            }
            set
            {
                if (value) this.date_reserved = true;
                else this.date_reserved = false;

            }
        }
        #endregion

        public int Year
        {
            get
            {
                return this.year;
            }
            set
            {
                this.year = value;
            }
        }

        public int Month
        {
            get
            {
                return this.month;
            }
            set
            {
                this.month = value;
            }
        }

        public int Date
        {
            get
            {
                return this.dayOfMonth;
            }
            set
            {
                this.dayOfMonth = value;
            }
        }
        public int DayOfWeek
        {
            get
            {
                return this.dayOfWeek;
            }
            set
            {
                this.dayOfWeek = value;
            }
        }
        public int DayOfMonth
        {
            get { return dayOfMonth; }
            set { dayOfMonth = value; }
        }

        #endregion
    }
}
