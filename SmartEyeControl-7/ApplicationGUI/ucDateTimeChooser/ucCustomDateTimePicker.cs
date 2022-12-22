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

namespace ucDateTimeChooser
{
    public partial class ucCustomDateTimePicker : UserControl
    {
        public const int MaxHeight = 22;
        public const int btnWidth = 20;
        private PopupControl.Popup ucPop = null;
        private IList<StDateTimeWildCards> customWildCards = null;
        /// private ErrorProvider _ErrorMessage = null;
        public readonly static DateTime MaxDateAllowed = DateTime.Parse("12/12/9998 00:00:00");
        public readonly static DateTime MinDateAllowed = DateTime.Parse("1/1/1753 00:00:00");

        #region TxtDateTime_WordSelected

        private string txtValPrev = String.Empty;
        private List<String> txtValPrevWords = null;
        private int selectedWordCount = -1;

        #endregion

        #region Properties

        [Browsable(true), Category("Behavior"), Description("Button to display WildCardWindow to choose Various DateTime WildCards")]
        public bool ShowWildCardWinButton
        {
            get
            {
                return btn_dropDown.Visible && btn_dropDown.Enabled;
            }
            set
            {
                btn_dropDown.Visible = btn_dropDown.Enabled = true;
                #region Hide_Panel2_btnContainer
                if (value)
                {
                    this.splitContainer.Panel2Collapsed = false;
                }
                else
                {
                    this.splitContainer.Panel2Collapsed = true;
                }
                #endregion
            }
        }

        [Browsable(true), Category("Appearance"), Description("DateTimePicker control choose Date&Time from visible Calendar")]
        public DateTimePicker ContentControl
        {
            get
            {
                return this.DatetimePicker;
            }
            set
            {
                this.DatetimePicker = value;
            }
        }

        [Browsable(true), Category("Appearance"), Description("ErrorProvider Control to display Validation Errors From Control")]
        public System.Windows.Forms.ErrorProvider ErrorMessage
        {
            get { return this._ErrorMessage; }
            set { this._ErrorMessage = value; }
        }

        [Browsable(true), Category("Appearance"), Description("Controls Numeric Up_Down Button Show_Hide")]
        public bool ShowUpDownButton
        {
            get { return ContentControl.ShowUpDown; }
            set { this.ContentControl.ShowUpDown = value; }
        }

        [Browsable(true), Category("Appearance"), Description("Controls Annual Calendar Show_Hide")]
        public bool ShowButtons
        {
            get { return ContentControl.ShowButtons; }
            set { this.ContentControl.ShowButtons = value; }
        }

        //FormatEx, extends the formatting options by allowing additional selections
        //Replaces Format
        [Browsable(true), Category("Appearance"), Description("Format Extensions replaces Format gets sets display Formats")]
        public dtpCustomExtensions FormatEx
        {
            get
            {
                return DatetimePicker.FormatEx;
            }
            set
            {
                DatetimePicker.FormatEx = value;
                ucCustomDateTimePicker_Resize(this, new EventArgs());
            }
        }

        //Override Textbox back Colour so we can add it to the Appearance List
        //and use it to set the BG colour
        [Browsable(true), Category("Appearance"), Description("The Background Colour user to display Text and Graphics in this Control")]
        public new System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                ContentControl.BackColor = value;
                txtDateTime.BackColor = value;
            }
        }

        //Override Textbox back Colour so we can add it to the Appearance List
        //and use it to set the BG colour
        [Browsable(true), Category("Appearance"), Description("The Background Colour user to display Text and Graphics in this Control")]
        public new System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                txtDateTime.ForeColor = value;
            }
        }

        public bool IsValidated
        {
            get
            {
                String errorMessage = _ErrorMessage.GetError(this);
                if (String.IsNullOrEmpty(errorMessage) || String.IsNullOrWhiteSpace(errorMessage))
                    return true;
                else
                    return false;
            }
        }

        #endregion

        public ucCustomDateTimePicker()
        {
            InitializeComponent();
            ShowWildCardWinButton = true;
            if (customWildCards == null)
                customWildCards = new List<StDateTimeWildCards>();
            DatetimePicker.txtDateTime = this.txtDateTime;
            DatetimePicker.ValidationOutCome += new Action<bool, string>(DateTimePicker_ValidationOutCome);
            ///DateTimePicker.Parent = this;
            ucCustomDateTimePicker_Resize(this, new EventArgs());
            txtValPrevWords = new List<string>();
        }

        void DateTimePicker_ValidationOutCome(bool validated, string Error_String)
        {
            if (_ErrorMessage != null)
            {
                if (!validated)
                    _ErrorMessage.SetError(this, Error_String);
                else
                    _ErrorMessage.SetError(this, String.Empty);
            }
        }

        private void ucCustomDateTimePicker_Resize(object sender, EventArgs e)
        {
            if (this.Height != MaxHeight)
                base.Size = new Size(this.Size.Width, MaxHeight);
            System.Drawing.Size _parentContainerSize = this.splitContainer.Panel1.ClientRectangle.Size;
            ///Adjust txtDateTime location & Size
            this.txtDateTime.Location = new System.Drawing.Point(-2 + DatetimePicker.CheckWidth, 0);
            this.txtDateTime.Size = new System.Drawing.Size(_parentContainerSize.Width -
                (DatetimePicker.ButtonWidth) - DatetimePicker.CheckWidth,
                this.txtDateTime.Size.Height);
            #region Apply_UcControl_Apperance For dtpCustomExtensions cases

            //if (ContentControl.ShowUpDown &&
            //        ((FormatEx == dtpCustomExtensions.dtpShortTime24Hour ||
            //        FormatEx == dtpCustomExtensions.dtpShortInterval ||
            //                   FormatEx == dtpCustomExtensions.dtpShortTimeAMPM ||
            //                   FormatEx == dtpCustomExtensions.dtpTime ||
            //                   FormatEx == dtpCustomExtensions.dtpLongTime ||
            //                   FormatEx == dtpCustomExtensions.dtpShortIntervalFixed ||
            //                   FormatEx == dtpCustomExtensions.dtpShortIntervalTimeSink)))
            //{
            //    txtDateTime.Enabled = txtDateTime.Visible = false;
            //}

            if (FormatEx == dtpCustomExtensions.dtpLongDateTimeWildCard ||
                FormatEx == dtpCustomExtensions.dtpLongDateWildCard)
            {
                txtDateTime.Enabled = txtDateTime.Visible = true;
            }
            else
                txtDateTime.Enabled = txtDateTime.Visible = false;

            ///Disable Wild_Card Window
            if (ShowWildCardWinButton &&
                FormatEx != dtpCustomExtensions.dtpLongDateTimeWildCard &&
                FormatEx != dtpCustomExtensions.dtpLongDateWildCard)
            {
                ShowWildCardWinButton = false;
            }

            #endregion
        }

        private void btn_dropDown_Click(object sender, EventArgs e)
        {
            var ucWildCards = new ucCustomWildCards();
            customWildCards = ContentControl.Get_WildCardsChecked();
            ucWildCards.WildCardsChecked = customWildCards;
            ucPop = new PopupControl.Popup(ucWildCards);
            ///Initialize Popup Animation Control
            ucPop.AnimationDuration = 1500;      ///1 Second
            ucPop.ShowingAnimation = PopupControl.PopupAnimations.TopToBottom;
            ucPop.HidingAnimation = PopupControl.PopupAnimations.BottomToTop;
            ucPop.Closing += new ToolStripDropDownClosingEventHandler(ucPop_Closing);
            ucPop.Size = new System.Drawing.Size(200, 170);
            ucPop.Show(this);
        }

        private void ucPop_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            try
            {
                if (ucPop != null && ucPop.Content is ucCustomWildCards)
                {
                    ucCustomWildCards ucWildCards = (ucCustomWildCards)ucPop.Content;
                    customWildCards = (List<StDateTimeWildCards>)ucWildCards.Get_WildCardsChecked();
                    if (customWildCards != null)
                    {
                        ContentControl.Save_WildCards(customWildCards);
                        ContentControl.bDroppedDown = true;
                        ContentControl.DateTimePicker_CloseUp(ContentControl, new EventArgs());
                    }
                }
            }
            catch
            {
                DateTimePicker_ValidationOutCome(false, "Error occurred while Saving Custom Wild Cards");
            }
        }

        private void txtDateTime_Click(object sender, EventArgs e)
        {

        }

        private void txtDateTime_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string selectedTxt = GetNextSelection();
                if (selectedTxt != String.Empty)
                {
                    int indexOf = txtDateTime.Text.IndexOf(selectedTxt);
                    if (indexOf != -1)
                        txtDateTime.Select(indexOf, selectedTxt.Length);
                }
            }
            catch
            {
                txtDateTime.Select();
            }
        }

        private void txtDateTime_TextChanged(object sender, EventArgs e)
        {
            ///txtDateTime.Text <> txtValPrevWords
            if (!txtValPrevWords.Equals(txtDateTime.Text))
            {
                txtChanged(txtDateTime.Text);
            }
        }

        private void ucCustomDateTimePicker_Enter(object sender, EventArgs e)
        {
            string customFormat = String.Empty;
            string mvarCustomFormatMessage = String.Empty;
            StDateTime.StDateTimeHelper.GetCustomFormatMessage(FormatEx, ref customFormat, ref mvarCustomFormatMessage);
            Tooltip.SetToolTip(this, mvarCustomFormatMessage);
            Tooltip.SetToolTip(ContentControl, mvarCustomFormatMessage);
            Tooltip.SetToolTip(txtDateTime, mvarCustomFormatMessage);
        }

        #region WordSelections

        internal void txtChanged(string txtVal)
        {
            try
            {
                txtValPrev = txtDateTime.Text;
                txtValPrevWords = RegexExtensions.GetWordsList(txtValPrev);
            }
            catch
            {
                txtValPrev = String.Empty;
                txtValPrevWords = new List<string>();
            }
            selectedWordCount = -1;
        }

        internal string GetNextSelection()
        {
            String selectedWord = string.Empty;
            try
            {
                selectedWordCount = ++selectedWordCount % txtValPrevWords.Count;
                selectedWord = txtValPrevWords[selectedWordCount];
            }
            catch
            {
                selectedWordCount = -1;
            }
            return selectedWord;
        }

        #endregion

    }
}
