using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PopupControl;
using DLMS;
using DLMS.Comm;

namespace ucDateTimeChooser
{
    public partial class ucCustomWildCards : UserControl
    {
        public const int Date_Time_WildCards = 0;
        public const int Monthly_WildCards = 1;
        public const int Time_WildCards = 2;

        #region Data_Members

        private IList<StDateTimeWildCards> _WildCardsChecked = null;
        private int current_gpVisible = 0;

        #endregion

        #region public Properties

        public int Current_WindowVisible
        {
            get { return current_gpVisible; }
            internal
            set
            {
                current_gpVisible = value;
                if (current_gpVisible < 0)
                    current_gpVisible = 2;
                else if (current_gpVisible > 2)
                    current_gpVisible = 0;
            }
        }

        public bool Win_Misc_Flags_Enabled
        {
            get { return gp_Misc_Flags.Enabled; }
            set { gp_Misc_Flags.Enabled = value; }
        }

        public bool Win_Monthly_Flags_Enabled
        {
            get { return gp_Monthly_Flags.Enabled; }
            set { gp_Monthly_Flags.Enabled = value; }
        }

        public bool Win_Time_Flags_Enabled
        {
            get { return gp_TimeFlags.Enabled; }
            set { gp_TimeFlags.Enabled = value; }
        }


        [Browsable(false), Category("Behavior"), Description("None"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<StDateTimeWildCards> WildCardsChecked
        {
            get
            {
                var t = Get_WildCardsChecked();
                if (_WildCardsChecked == null)
                    _WildCardsChecked = t;
                else
                {
                    _WildCardsChecked.Clear();
                    foreach (var wldCard in t)
                    {
                        _WildCardsChecked.Add(wldCard);
                    }
                }
                return _WildCardsChecked;
            }
            set { _WildCardsChecked = value; }
        }

        #endregion

        public ucCustomWildCards()
        {
            InitializeComponent();
            gp_Misc_Flags.Enabled = true;
            gp_Monthly_Flags.Enabled = true;
            gp_TimeFlags.Enabled = true;
        }

        private void ucCustomWildCards_Load(object sender, EventArgs e)
        {
            ///Select Heading Background color
            var pos = this.PointToScreen(this.lbl_Heading.Location);
            pos = this.header.PointToClient(pos);
            this.lbl_Heading.Parent = this.header;
            this.lbl_Heading.Location = pos;
            this.lbl_Heading.BackColor = Color.Transparent;
            if (_WildCardsChecked == null)
                _WildCardsChecked = new List<StDateTimeWildCards>();
            ///Show Wild Card Windows
            Show_WildCardWindow(Current_WindowVisible);
            Show_WildCards(_WildCardsChecked);
        }

        public void Show_WildCardWindow(int current_gpVisible)
        {
            #region ///Disable All WildCard Windows

            gp_Misc_Flags.Visible = false;
            gp_Monthly_Flags.Visible = false;
            gp_TimeFlags.Visible = false;

            #endregion
            #region Display WildCard Window

            if (current_gpVisible == Date_Time_WildCards)
            {
                gp_Misc_Flags.BringToFront();
                gp_Misc_Flags.Visible = true;
                this.lbl_Heading.Text = "DateTime WildCards";
            }
            else if (current_gpVisible == Monthly_WildCards)
            {
                gp_Monthly_Flags.BringToFront();
                gp_Monthly_Flags.Visible = true;
                this.lbl_Heading.Text = "Day Of Month WildCards";
            }
            else if (current_gpVisible == Time_WildCards)
            {
                gp_TimeFlags.BringToFront();
                gp_TimeFlags.Visible = true;
                this.lbl_Heading.Text = "Time WildCards";
            }

            #endregion
        }

        public void Show_WildCards(IList<StDateTimeWildCards> WildCardsChecked)
        {
            try
            {
                ///Disable Time WildCards
                chk_repeat_Hour.Checked = chk_repeat_Minute.Checked = chk_repeat_sec.Checked = false;
                ///Disable Monthly WildCards
                chk_DLS_BEGIN.Checked = chk_DLS_END.Checked = chk_repeat_mon.Checked
                    = chk_lastDay.Checked = chk_2ndLastDay.Checked = false;
                ///Disable Misc WildCards
                chk_repeat_year.Checked = chk_repeat_day.Checked = chk_repeatDOW.Checked = 
                    chk_gmt.Checked = false;

                if (WildCardsChecked == null)
                    return;
                ///Display Time WildCards
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullHour))
                    chk_repeat_Hour.Checked = true;
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullMinute))
                    chk_repeat_Minute.Checked = true;
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullSecond))
                    chk_repeat_sec.Checked = true;
                ///Display Monthly WildCards
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullMonth))
                    chk_repeat_mon.Checked = true;
                if (WildCardsChecked.Contains(StDateTimeWildCards.LSTDayOfMonth))
                    chk_lastDay.Checked = true;
                if (WildCardsChecked.Contains(StDateTimeWildCards._2LSTDayOfMonth))
                    chk_2ndLastDay.Checked = true;
                if (WildCardsChecked.Contains(StDateTimeWildCards.DLSBEGIN))
                    chk_DLS_BEGIN.Checked = true;
                if (WildCardsChecked.Contains(StDateTimeWildCards.DLSEND))
                    chk_DLS_END.Checked = true;
                ///Display Miscelleanous WildCards
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullYear))
                    chk_repeat_year.Checked = true;
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullDay))
                    chk_repeat_day.Checked = true;
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullGMT))
                    chk_gmt.Checked = true;
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullDayOfWeek))
                    chk_repeatDOW.Checked = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while displaying WildCards", ex);
            }
        }

        public IList<StDateTimeWildCards> Get_WildCardsChecked()
        {
            IList<StDateTimeWildCards> WildCardsChecked = new List<StDateTimeWildCards>();
            try
            {
                if (WildCardsChecked == null)
                    return WildCardsChecked;
                ///Display Time WildCards
                if (chk_repeat_Hour.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.NullHour);
                if (chk_repeat_Minute.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.NullMinute);
                if (chk_repeat_sec.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.NullSecond);
                ///Display Monthly WildCards
                if (chk_repeat_mon.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.NullMonth);
                if (chk_DLS_BEGIN.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.DLSBEGIN);
                if (chk_DLS_END.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.DLSEND);
                if (chk_repeat_day.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.NullDay);
                if (chk_lastDay.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.LSTDayOfMonth);
                if (chk_2ndLastDay.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards._2LSTDayOfMonth);

                ///Display Miscelleanous WildCards
                if (chk_repeat_year.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.NullYear);
                if (chk_gmt.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.NullGMT);
                if (chk_repeatDOW.Checked)
                    WildCardsChecked.Add(StDateTimeWildCards.NullDayOfWeek);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while reading WildCards", ex);
            }
            return WildCardsChecked;
        }

        public void Set_WildCardsCheckDisabled(IList<StDateTimeWildCards> WildCardsChecked)
        {
            try
            {
                if (Win_Misc_Flags_Enabled)
                {
                    ///Enable Time WildCards
                    chk_repeat_Hour.Enabled = chk_repeat_Minute.Enabled = chk_repeat_sec.Enabled = true;
                }
                if (Win_Monthly_Flags_Enabled)
                {
                    ///Enable Monthly WildCards
                    chk_lastDay.Enabled = chk_2ndLastDay.Enabled = chk_repeat_day.Enabled
                        = true;
                }
                if (Win_Misc_Flags_Enabled)
                {
                    ///Disable Misc WildCards
                    chk_repeat_year.Enabled = chk_repeat_day.Enabled = chk_repeatDOW.Enabled = chk_repeat_mon.Enabled = chk_DLS_BEGIN.Enabled = chk_DLS_END.Enabled =
                       chk_gmt.Enabled = true;
                }
                if (WildCardsChecked == null)
                    return;
                ///Display Time WildCards
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullHour))
                    chk_repeat_Hour.Enabled = false;
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullMinute))
                    chk_repeat_Minute.Enabled = false;
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullSecond))
                    chk_repeat_sec.Enabled = false;
                ///Display Monthly WildCards
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullMonth))
                    chk_repeat_mon.Enabled = false;
                if (WildCardsChecked.Contains(StDateTimeWildCards.LSTDayOfMonth))
                    chk_lastDay.Enabled = false;
                if (WildCardsChecked.Contains(StDateTimeWildCards._2LSTDayOfMonth))
                    chk_2ndLastDay.Enabled = false;
                if (WildCardsChecked.Contains(StDateTimeWildCards.DLSBEGIN))
                    chk_DLS_BEGIN.Enabled = false;
                if (WildCardsChecked.Contains(StDateTimeWildCards.DLSEND))
                    chk_DLS_END.Enabled = false;
                ///Display Miscellaneous WildCards
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullYear))
                    chk_repeat_year.Enabled = false;
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullDay))
                    chk_repeat_day.Enabled = false;
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullGMT))
                    chk_gmt.Enabled = false;
                if (WildCardsChecked.Contains(StDateTimeWildCards.NullDayOfWeek))
                    chk_repeatDOW.Enabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while displaying WildCards", ex);
            }
        }

        #region btn_Backword_NextHandlers

        private void btn_Prev_Click(object sender, EventArgs e)
        {
            Current_WindowVisible--;
            Show_WildCardWindow(Current_WindowVisible);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            Current_WindowVisible++;
            Show_WildCardWindow(Current_WindowVisible);
        }

        #endregion

        ///To make control resize-able
        //protected override void WndProc(ref Message m)
        //{
        //    if (Parent != null && (Parent as Popup).ProcessResizing(ref m)) return;
        //    base.WndProc(ref m);
        //}
    }
}
