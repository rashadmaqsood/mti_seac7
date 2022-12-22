using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DLMS;
using DLMS.Comm;

namespace datetime
{
    public partial class DateTimeChooser : UserControl
    {
        public event EventHandler ValueCustomChanged = delegate { };
        bool isInvokeAction = false;

        int year, month, dayOfMonth, daysInMonth, dayOfWeek, hours, minutes, seconds, hundredth;
        bool Year_NS = true;
        bool month_NS = true, month_savingsBegin = true, month_savingsEnd = true;
        bool date_NS = true, date_lastDay = true, date_2ndLastDay = true, date_reserved = true;
        bool time_hourNS = true, time_minutesNS = true, time_secondsNS = true, time_hundredthNS = true;
        bool panel_date = true, panel_time = true;

        public DateTimeChooser()
        {
            InitializeComponent();
            Apply_VisualFormat();
        }

        #region Time Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool hour_NotSpecified
        {
            get
            {
                return this.time_hourNS;
            }
            set
            {
                if (time_hourNS != value)
                {
                    this.time_hourNS = value;
                    Apply_VisualFormat();
                }
                this.time_hourNS = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool minute_NotSpecified
        {
            get
            {
                return this.time_minutesNS;
            }
            set
            {
                if (time_minutesNS != value)
                {
                    this.time_minutesNS = value;
                    Apply_VisualFormat();
                }
                this.time_minutesNS = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool second_NotSpecified
        {
            get
            {
                return this.time_secondsNS;
            }
            set
            {
                if (time_secondsNS != value)
                {
                    this.time_secondsNS = value;
                    Apply_VisualFormat();
                }
                this.time_secondsNS = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool hundredth_NotSpecified
        {
            get
            {
                return this.time_hundredthNS;
            }
            set
            {
                if (time_hundredthNS != value)
                {
                    this.time_hundredthNS = value;
                    Apply_VisualFormat();
                }
                this.time_hundredthNS = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        public bool showHundredth
        {
            get
            {
                if (this.combo_hundredth.Visible == true)
                {
                    this.label_hudredth.Visible = true;
                    return true;
                }
                else
                {
                    this.label_hudredth.Visible = false;
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.combo_hundredth.Visible = true;
                    this.label_hudredth.Visible = true;
                }
                else
                {
                    this.combo_hundredth.Visible = false;
                    this.label_hudredth.Visible = false;
                }
            }
        }

        public bool showSeconds
        {
            get
            {
                if (this.combo_second.Visible == true)
                {
                    this.label_seconds.Visible = true;
                    return true;
                }
                else
                {
                    this.label_seconds.Visible = false;
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.label_seconds.Visible = true;
                    this.combo_second.Visible = true;
                }
                else
                {
                    this.label_seconds.Visible = false;
                    this.combo_second.Visible = false;
                }
            }
        }

        public bool showMinute
        {
            get
            {
                if (this.combo_minute.Visible == true)
                {
                    this.label_minutes.Visible = true;
                    return true;
                }
                else
                {
                    this.label_minutes.Visible = false;
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.combo_minute.Visible = true;
                    this.label_minutes.Visible = true;
                }
                else
                {
                    this.combo_minute.Visible = false;
                    this.label_minutes.Visible = false;
                }
            }
        }

        public bool showHour
        {
            get
            {
                if (this.combo_hour.Visible == true)
                {
                    this.label_hours.Visible = true;
                    return true;
                }
                else
                {
                    this.label_hours.Visible = false;
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.label_hours.Visible = true;
                    this.combo_hour.Visible = true;
                }
                else
                {
                    this.combo_hour.Visible = false;
                    this.label_hours.Visible = false;
                }
            }
        }

        #endregion

        #region Date Properties

        #region Year

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Year_NotSpecified
        {
            get
            {
                return this.Year_NS;
            }
            set
            {
                if (Year_NS != value)
                {
                    this.Year_NS = value;
                    Apply_VisualFormat();
                }
                this.Year_NS = value;
            }
        }

        #endregion

        #region Month
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Month_NotSpecified
        {
            get
            {
                return this.month_NS;
            }
            set
            {
                if (month_NS != value)
                {
                    this.month_NS = value;
                    Apply_VisualFormat();
                }
                this.month_NS = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Month_SavingBegins
        {
            get
            {
                return this.month_savingsBegin;
            }
            set
            {
                if (month_savingsBegin != value)
                {
                    this.month_savingsBegin = value;
                    Apply_VisualFormat();
                }
                this.month_savingsBegin = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Month_SavingEnds
        {
            get
            {
                return this.month_savingsEnd;
            }
            set
            {
                if (month_savingsEnd != value)
                {
                    this.month_savingsEnd = value;
                    Apply_VisualFormat();
                }
                this.month_savingsEnd = value;
            }
        }
        #endregion

        #region Date
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Date_2ndLastDay
        {
            get
            {
                return this.date_2ndLastDay;
            }
            set
            {
                if (date_2ndLastDay != value)
                {
                    this.date_2ndLastDay = value;
                    Apply_VisualFormat();
                }
                this.date_2ndLastDay = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Date_lastDay
        {
            get
            {
                return this.date_lastDay;
            }
            set
            {
                if (date_lastDay != value)
                {
                    this.date_lastDay = value;
                    Apply_VisualFormat();
                }
                this.date_lastDay = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Date_NotSpecified
        {
            get
            {
                // return combo_Date.Items.Contains("Any");
                return this.date_NS;
            }
            set
            {
                if (date_NS != value)
                {
                    this.date_NS = value;
                    Apply_VisualFormat();
                }
                this.date_NS = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        public bool showYear
        {
            get
            {
                if (this.combo_Year.Visible == true)
                {
                    this.label_year.Visible = true;
                    return true;
                }
                else
                {
                    this.label_year.Visible = false;
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.combo_Year.Visible = true;
                    this.label_year.Visible = true;
                }
                else
                {
                    this.combo_Year.Visible = false;
                    this.label_year.Visible = false;
                }
            }
        }

        public bool showMonth
        {
            get
            {
                if (this.combo_Month.Visible == true)
                {
                    this.label_month.Visible = true;
                    return true;
                }

                else
                {
                    this.label_month.Visible = false;
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.combo_Month.Visible = true;
                    this.label_month.Visible = true;
                }
                else
                {
                    this.combo_Month.Visible = false;
                    this.label_month.Visible = false;
                }
            }
        }

        public bool showDate
        {
            get
            {
                if (this.combo_Date.Visible == true)
                {
                    this.label_Date.Visible = true;
                    return true;
                }

                else
                {
                    this.label_Date.Visible = false;
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.combo_Date.Visible = true;
                    this.label_Date.Visible = true;
                }

                else
                {
                    this.combo_Date.Visible = false;
                    this.label_Date.Visible = false;
                }
            }
        }

        public bool showDayOfWeek
        {
            get
            {
                if (this.combo_DayofWeek.Visible == true)
                {
                    this.lbl_DayofWeek.Visible = true;
                    return true;
                }

                else
                {
                    this.lbl_DayofWeek.Visible = false;
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    this.combo_DayofWeek.Visible = true;
                    this.lbl_DayofWeek.Visible = true;
                }

                else
                {
                    this.combo_DayofWeek.Visible = false;
                    this.lbl_DayofWeek.Visible = false;
                }
            }
        }

        public bool VisibleDate
        {
            get
            {
                return this.panel_date;
            }
            set
            {
                if (value) this.panel_date = true;
                else this.panel_date = false;
            }
        }

        public bool VisibleTime
        {
            get
            {
                return this.panel_time;
            }
            set
            {
                if (value) this.panel_time = true;
                else this.panel_time = false;
            }
        }

        #endregion

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StDateTime DateTimeStruct
        {
            get
            {
                StDateTime DateTimeStruct = new StDateTime();
                try
                {
                    DateTimeStruct.Year = Convert.ToUInt16(Year);
                    DateTimeStruct.Month = Convert.ToByte(Month);
                    DateTimeStruct.DayOfWeek = Convert.ToByte(DayOfWeek);
                    DateTimeStruct.DayOfMonth = Convert.ToByte(Date);
                    DateTimeStruct.Hour = Convert.ToByte(Hours);
                    DateTimeStruct.Minute = Convert.ToByte(Minutes);
                    DateTimeStruct.Second = Convert.ToByte(Seconds);
                    DateTimeStruct.Hundred = 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error Invalid Date Time Structure", ex);
                }
                return DateTimeStruct;
            }
            set
            {
                try
                {
                    Year = value.Year;
                    Month = value.Month;
                    Date = value.DayOfMonth;
                    DayOfWeek = value.DayOfWeek;
                    Hours = value.Hour;
                    Minutes = value.Minute;
                    Seconds = value.Second;
                    showDatetime();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error Invalid Date Time Structure", ex);
                }
            }
        }

        public void showDatetime()
        {
            try
            {
                isInvokeAction = false;
                if (this.year == StDateTime.NullYear)
                {
                    if (combo_Year.Items.Count > 0)
                    {
                        this.combo_Year.SelectedIndex = 0;
                    }
                }
                else
                {
                    this.combo_Year.SelectedItem = this.year;
                }

                if (this.month == StDateTime.Null)
                {
                    if (combo_Month.Items.Count > 0)
                    {
                        this.combo_Month.SelectedIndex = 0;
                    }
                }
                else if (this.month == StDateTime.DaylightSavingBegin)
                {
                    combo_Month.SelectedIndex = 1;
                }
                else if (this.month == StDateTime.DaylightSavingEnd)
                {
                    combo_Month.SelectedIndex = 2;
                }
                else
                {
                    this.combo_Month.SelectedItem = this.month;
                }

                if (this.dayOfMonth == StDateTime.Null)
                {
                    if (combo_Date.Items.Count > 0)
                        this.combo_Date.SelectedIndex = 0;
                }
                else if (this.dayOfMonth == StDateTime.LastDayOfMonth)
                {
                    if (combo_Date.Items.Count > 0)
                        this.combo_Date.SelectedIndex = 1;
                }
                else if (this.dayOfMonth == StDateTime.SecondLastDayOfMonth)
                {
                    if (combo_Date.Items.Count > 0)
                        this.combo_Date.SelectedIndex = 2;
                }
                else
                {
                    this.combo_Date.SelectedItem = this.dayOfMonth;
                }
                if (this.dayOfWeek == StDateTime.Null)
                {
                    this.combo_DayofWeek.SelectedIndex = 0;
                }
                else
                {
                    this.combo_DayofWeek.SelectedIndex = this.dayOfWeek;
                }
                if (this.dayOfWeek != 0xff)
                    this.combo_DayofWeek.SelectedIndex = this.dayOfWeek;
                else
                {
                    this.combo_DayofWeek.SelectedIndex = 0;
                }
                if (this.hours == 0xff)
                    this.combo_hour.SelectedItem = "Any";
                else
                    this.combo_hour.SelectedItem = this.hours;
                if (this.minutes == 0xff)
                    this.combo_minute.SelectedItem = "Any";
                else
                    this.combo_minute.SelectedItem = this.minutes;
                if (this.seconds == 0xff)
                    this.combo_second.SelectedItem = "Any";
                else
                    this.combo_second.SelectedItem = this.seconds;
                if (this.hundredth == 0xff)
                    this.combo_hundredth.SelectedItem = "Any";
                else
                    this.combo_hundredth.SelectedItem = this.hundredth;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                isInvokeAction = true;
            }
        }

        public void DateTimeChooser_Load(object sender, EventArgs e)
        {
            try
            {
                Apply_VisualFormat();
                showDatetime();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void parent_pnl_Enter(object sender, EventArgs e)
        {
            this.SelectDateTimeVisible();
        }

        internal void SelectDateTimeVisible()
        {
            #region Panel_Visiblity_Initialization

            parent_pnl.Panel1.Enabled = true;           //Key Panel1 -->PnlDate
            parent_pnl.Panel2.Enabled = true;           //Key Panel2 -->PnlTime

            if (this.panel_date && this.panel_time)
            {
                parent_pnl.Panel1Collapsed = false;
                parent_pnl.Panel2Collapsed = false;
            }
            else if (this.panel_date && !this.panel_time)
            {
                parent_pnl.Panel1Collapsed = false;
                parent_pnl.Panel2Collapsed = true;

                parent_pnl.Panel2.Enabled = false;
            }
            else if (!this.panel_date && this.panel_time)
            {

                parent_pnl.Panel1Collapsed = true;
                parent_pnl.Panel2Collapsed = false;

                parent_pnl.Panel1.Enabled = false;
            }
            else
            {
                parent_pnl.Panel1Collapsed = true;
                parent_pnl.Panel2Collapsed = false;

                parent_pnl.Panel1.Enabled = false;           //Key Panel1 -->PnlDate
                parent_pnl.Panel2.Enabled = false;           //Key Panel2 -->PnlTime
            }

            #endregion
        }

        private void DateTimeChooser_Paint(object sender, PaintEventArgs e)
        {
            this.SelectDateTimeVisible();
        }

        private void combo_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.combo_Year.SelectedIndex == -1) return;

                if (this.combo_Year.SelectedItem.Equals("Any"))
                {
                    this.year = StDateTime.NullYear;
                }
                else
                {
                    this.year = Convert.ToInt16(this.combo_Year.SelectedItem.ToString());
                }
            }
            finally
            {
                if (isInvokeAction)
                    ValueCustomChanged(this, new EventArgs());
            }
        }

        private void combo_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.combo_Month.SelectedIndex == -1) return;

                if (this.combo_Month.SelectedItem.Equals("Any"))               //NS is selected from the combo
                {
                    this.month = StDateTime.Null;
                }
                else if (this.combo_Month.SelectedItem.Equals("Saving Begins"))//saving begins
                {
                    this.month = StDateTime.DaylightSavingBegin;
                }
                else if (this.combo_Month.SelectedItem.Equals("Saving Ends")) //saving ends
                {
                    this.month = StDateTime.DaylightSavingEnd;
                }
                else
                    this.month = Convert.ToInt16(this.combo_Month.SelectedItem.ToString());
            }
            finally
            {
                if (isInvokeAction)
                    ValueCustomChanged(this, new EventArgs());
            }

        }

        private void combo_Date_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.combo_Date.SelectedItem.Equals("Any"))               //NS
                {
                    this.dayOfMonth = StDateTime.Null;
                }
                else if (this.combo_Date.SelectedItem.Equals("Last Day"))     //Last Day
                {
                    this.dayOfMonth = StDateTime.LastDayOfMonth;
                }
                else if (this.combo_Date.SelectedItem.Equals("2nd Last Day")) //2nd Last day
                {
                    this.dayOfMonth = StDateTime.SecondLastDayOfMonth;
                }
                else if (this.combo_Date.SelectedItem.Equals("Reserved"))    //Reserved
                {
                    this.dayOfMonth = 0xFC;
                }
                else
                {
                    this.dayOfMonth = Convert.ToUInt16(this.combo_Date.SelectedItem.ToString());
                }
            }
            finally
            {
                if (isInvokeAction)
                    ValueCustomChanged(this, new EventArgs());
            }
        }

        private void combo_hour_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_hour.SelectedIndex == -1) return;
                if (combo_hour.SelectedIndex == 0 && this.hour_NotSpecified)
                {
                    this.hours = 0xff;
                }
                else
                {
                    this.hours = Convert.ToUInt16(this.combo_hour.SelectedItem.ToString());
                }
            }
            finally
            {
                if (isInvokeAction)
                    ValueCustomChanged(this, new EventArgs());
            }
        }

        private void combo_minute_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_minute.SelectedIndex == -1) return;
                if (combo_minute.SelectedIndex == 0 && this.minute_NotSpecified)
                {
                    this.minutes = 0xff;
                }
                else
                {
                    this.minutes = Convert.ToUInt16(this.combo_minute.SelectedItem.ToString());
                }
            }
            finally
            {
                if (isInvokeAction)
                    ValueCustomChanged(this, new EventArgs());
            }
        }

        private void combo_second_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_second.SelectedIndex == -1) return;
                if (combo_second.SelectedIndex == 0 && this.second_NotSpecified)
                {
                    this.minutes = 0xff;
                }
                else
                {
                    this.seconds = Convert.ToUInt16(this.combo_second.SelectedItem.ToString());
                }
            }
            finally
            {
                if (isInvokeAction)
                    ValueCustomChanged(this, new EventArgs());

            }
        }

        private void combo_hundredth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_hundredth.SelectedIndex == -1) return;
                this.hundredth = Convert.ToUInt16(this.combo_hundredth.SelectedItem.ToString());
            }
            finally
            {
                if (isInvokeAction)
                    ValueCustomChanged(this, new EventArgs());
            }
        }

        private void combo_DayofWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_DayofWeek.SelectedIndex == -1) return;

                if (combo_DayofWeek.SelectedIndex == 0)
                    this.dayOfWeek = 0xff;
                else
                {
                    this.dayOfWeek = Convert.ToUInt16(this.combo_DayofWeek.SelectedIndex);
                }
            }
            finally
            {
                if (isInvokeAction)
                    ValueCustomChanged(this, new EventArgs());
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                foreach (Control contrl in parent_pnl.Panel1.Controls)
                {
                    if (contrl != null && typeof(Label) == contrl.GetType())
                        contrl.ForeColor = value;
                }
                foreach (Control contrl in parent_pnl.Panel2.Controls)
                {
                    if (contrl != null && typeof(Label) == contrl.GetType())
                        contrl.ForeColor = value;
                }
            }
        }

        internal void Apply_VisualFormat()
        {
            try
            {
                #region Date_initialization

                //Add items to the Year_Combo
                this.combo_Year.Items.Clear();
                int traverser_year = 0;
                if (this.Year_NS) //if this is true, ADD "NS" to the list
                {
                    this.combo_Year.Items.Insert(traverser_year, "Any"); traverser_year++;
                }
                for (int j = 2010; j <= 2030; j++) //this loop adds the year to the list
                {
                    this.combo_Year.Items.Insert(traverser_year, j); traverser_year++;
                }

                //Add items to the combo_Month
                this.combo_Month.Items.Clear();
                int traverser_month = 0;
                if (this.month_NS) //if this property is true, show "NS" in the list
                {
                    this.combo_Month.Items.Insert(traverser_month, "Any"); traverser_month++;
                }
                if (this.Month_SavingBegins)
                {
                    this.combo_Month.Items.Insert(traverser_month, "Saving Begins"); traverser_month++;
                }
                if (this.month_savingsEnd)
                {
                    this.combo_Month.Items.Insert(traverser_month, "Saving Ends"); traverser_month++;
                }
                for (int i = 1; i < 13; i++) //this loop inserts all the months
                {
                    this.combo_Month.Items.Insert(traverser_month, i); traverser_month++;
                }

                //Add Items to the combo_Date
                combo_Date.Items.Clear();
                int traverser_Date = 0;
                daysInMonth = 31;

                if (this.Date_NotSpecified) //if this property is true, show "Any" in the list
                {
                    this.combo_Date.Items.Insert(traverser_Date, "Any"); traverser_Date++;
                }
                if (this.date_lastDay)
                {
                    this.combo_Date.Items.Insert(traverser_Date, "Last Day"); traverser_Date++;
                }
                if (this.date_2ndLastDay)
                {
                    this.combo_Date.Items.Insert(traverser_Date, "2nd Last Day"); traverser_Date++;
                }

                //if wild card is specified, display maximum days of a month i.e. 31
                //if (this.combo_Month.SelectedItem.Equals("Any") || this.combo_Month.SelectedItem.Equals("Saving Begins") ||
                //    this.combo_Month.SelectedItem.Equals("Saving Ends"))
                //{
                //}
                //else
                //{
                //    if (this.showYear && this.combo_Year.SelectedItem.Equals("Any"))
                //    {
                //        this.daysInMonth = DateTime.DaysInMonth(2012, month);
                //    }
                //    else if (!this.showYear || this.combo_Year.SelectedItem.Equals("Any"))
                //    {
                //        this.daysInMonth = DateTime.DaysInMonth(2012, month);
                //    }
                //    else
                //    {
                //        this.daysInMonth = DateTime.DaysInMonth(year, month);
                //    }
                //}

                int index = this.combo_Date.Items.Count;
                for (int i = 1; i <= this.daysInMonth; i++)
                {
                    this.combo_Date.Items.Insert(index, i);
                    index++;
                }

                //add DayofWeek
                this.combo_DayofWeek.Items.Clear();
                this.combo_DayofWeek.Items.AddRange(new string[]
                {"Any",
                 "MON",
                 "TUE",
                 "WED",
                 "THU",
                 "FRI",
                 "SAT",
                 "SUN"});

                #endregion

                #region Time_initialization

                //Add Hours
                combo_hour.Items.Clear();
                int traverser_hour = 0;
                if (this.hour_NotSpecified)
                {
                    this.combo_hour.Items.Insert(traverser_hour, "Any"); traverser_hour++;
                }
                for (int i = 0; i < 24; i++)
                {
                    if (this.combo_hour.Items.Count > i)
                    {
                        if (this.combo_hour.Items[i].ToString() != i.ToString())
                            this.combo_hour.Items.Insert(traverser_hour, i); traverser_hour++;
                    }
                    else
                    {
                        this.combo_hour.Items.Insert(traverser_hour, i); traverser_hour++;
                    }
                }

                //Add Minute
                this.combo_minute.Items.Clear();
                int traverser_minute = 0;
                if (this.minute_NotSpecified)
                {
                    this.combo_minute.Items.Insert(traverser_minute, "Any");
                    traverser_minute++;
                }
                for (int i = 0; i < 60; i++)
                {
                    if (this.combo_minute.Items.Count > i)
                    {
                        if (this.combo_minute.Items[i].ToString() != i.ToString())
                            this.combo_minute.Items.Insert(traverser_minute, i); traverser_minute++;
                    }
                    else
                    {
                        this.combo_minute.Items.Insert(traverser_minute, i); traverser_minute++;
                    }
                }

                //Add Seconds
                this.combo_second.Items.Clear();
                int traverser_second = 0;
                if (this.second_NotSpecified)
                {
                    this.combo_second.Items.Insert(traverser_second, "Any");
                    traverser_second++;
                }
                for (int i = 0; i < 60; i++)
                {
                    if (this.combo_second.Items.Count > i)
                    {
                        if (this.combo_second.Items[i].ToString() != i.ToString())
                            this.combo_second.Items.Insert(traverser_second, i); traverser_second++;
                    }
                    else
                    {
                        this.combo_second.Items.Insert(traverser_second, i); traverser_second++;
                    }
                }

                //Add Hundred
                this.combo_hundredth.Items.Clear();

                int traverser_hundredth = 0;
                if (this.hundredth_NotSpecified)
                {
                    this.combo_hundredth.Items.Insert(traverser_hundredth, "Any");
                    traverser_hundredth++;
                }
                for (int i = 0; i < 100; i++)
                {
                    if (this.combo_hundredth.Items.Count > i)
                    {
                        if (this.combo_hundredth.Items[i].ToString() != i.ToString())
                            this.combo_hundredth.Items.Insert(traverser_hundredth, i); traverser_hundredth++;
                    }
                    else
                    {
                        this.combo_hundredth.Items.Insert(traverser_hundredth, i); traverser_hundredth++;
                    }
                }

                #endregion

                this.SelectDateTimeVisible();
                //****************************************************************
                //****************************************************************
                showYear = this.showYear;
                showMonth = this.showMonth;
                showDate = this.showDate;
                showDayOfWeek = this.showDayOfWeek;
                showHour = this.showHour;
                showMinute = this.showMinute;
                showSeconds = this.showSeconds;
                showHundredth = this.showHundredth;
                //****************************************************************
                //****************************************************************
                ForeColor = base.ForeColor;
            }
            catch { }
        }
    }
}
