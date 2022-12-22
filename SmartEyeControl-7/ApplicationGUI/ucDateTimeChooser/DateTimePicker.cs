using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using DLMS;
using DLMS.Comm;

namespace ucDateTimeChooser
{
    [CLSCompliant(true), ToolboxItem(false)]
    public class DateTimePicker : System.Windows.Forms.DateTimePicker
    {
        private System.ComponentModel.IContainer components;
        private bool SetDate;
        private System.Windows.Forms.ToolTip Tooltip;
        private StDateTime localDateTimeObj = null;
        private StDateTime Prev_localDateTimeObj = null;
        private const int BTNWIDTH = 16;

        public event Action<bool, string> ValidationOutCome = delegate { };
        public event Action<object, EventArgs> ValueCustomChanged = delegate { };

        private string mvarLinkedTo;
        internal bool bDroppedDown;
        internal int ButtonWidth = BTNWIDTH;
        private bool mvarShowButtons = true;
        private dtpCustomExtensions mvarFormatEx;
        internal string mvarCustomFormatMessage;
        internal int CheckWidth = 0;

        private TextBox _txtDateTime;

        private ucDateTimeChooser.DateTimePicker LinkTo;
        private System.Collections.ArrayList LinkToArray = new System.Collections.ArrayList();
        private System.Collections.ArrayList LinkedArray = new System.Collections.ArrayList();

        #region Constructor and destructor

        public DateTimePicker()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
            //Initialize bas.Format to Custom, we only need Custom Format
            base.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            ///DateTimePicker_Resize(this, new EventArgs());
            this.localDateTimeObj = new StDateTime();
            this.Prev_localDateTimeObj = new StDateTime(localDateTimeObj);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion Constructor and destructor

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this._txtDateTime = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtDateTime
            // 
            // 
            // DateTimePicker
            // 
            this.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.BackColorChanged += new System.EventHandler(this.DateTimePicker_BackColorChanged);
            this.ForeColorChanged += new System.EventHandler(this.DateTimePicker_ForeColorChanged);
            this.FormatChanged += new System.EventHandler(this.FormatOrValueChanged);
            this.CloseUp += new System.EventHandler(this.DateTimePicker_CloseUp);
            this.ValueChanged += new System.EventHandler(this.FormatOrValueChanged);
            this.DropDown += new System.EventHandler(this.DateTimePicker_DropDown);
            this.FontChanged += new System.EventHandler(this.DateTimePicker_FontChanged);
            this.Enter += new System.EventHandler(this.DateTimePicker_Enter);
            this.Resize += new System.EventHandler(this.DateTimePicker_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        #region override and additional properties

        [Browsable(true), Category("Appearance"), Description("Represent text area to take input from user"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal TextBox txtDateTime
        {
            get
            {
                return this._txtDateTime;
            }
            set
            {
                this._txtDateTime = value;
                #region Process_Handlers

                ///Detach Handler If Already Attached
                this.txtDateTime.BackColorChanged -= this.txtDateTime_BackColorChanged;
                this.txtDateTime.Enter -= this.txtDateTime_Enter;
                this.txtDateTime.Leave -= this.txtDateTime_Leave;
                ///Attach Handlers
                this.txtDateTime.BackColorChanged += this.txtDateTime_BackColorChanged;
                this.txtDateTime.Enter += this.txtDateTime_Enter;
                this.txtDateTime.Leave += this.txtDateTime_Leave;

                #endregion
            }
        }

        [Browsable(true), Category("Behavior"), Description("Represent internal StDateTime Object"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StDateTime ValueCustom
        {
            get { return localDateTimeObj; }
            set
            {
                try
                {
                    localDateTimeObj = new StDateTime(value);
                    ///ToString Supplied DateTime
                    string _txtDateTime = localDateTimeObj.ToString(mvarFormatEx);
                    Text = _txtDateTime;
                }
                catch
                {
                    txtDateTime.Text = "";
                }
                finally
                {
                    RaiseValueCustomChanged();
                }
            }
        }

        //OverRide Formst and hide it by setting Browsable false, make it read only
        //so it can't be written to, it will always be Custom anyway
        [Browsable(false), Category("Appearance"), Description("Hides DropDown and Spin Buttons, Allows keyed entry only.")]
        public new System.Windows.Forms.DateTimePickerFormat Format
        {
            get
            {
                return base.Format;
            }
            internal set
            {
                base.Format = value;
            }
        }

        //FormatEx, extends the formatting options by allowing additional selections
        //Replaces Format
        [Browsable(true), Category("Appearance"), Description("Format Extensions replaces Format gets sets display Formats")]
        public dtpCustomExtensions FormatEx
        {
            get
            {
                return mvarFormatEx;
            }
            set
            {
                mvarFormatEx = value;
                //Set up the message that will display in the ToolTip
                //when the mouse is hovered over the control
                string _CustomFormat = CustomFormat;
                string _mvarFormatMesssageStr = String.Empty;
                StDateTime.StDateTimeHelper.GetCustomFormatMessage(mvarFormatEx, ref _CustomFormat, ref _mvarFormatMesssageStr);
                this.CustomFormat = _CustomFormat;
                this.mvarCustomFormatMessage = _mvarFormatMesssageStr;
                DateTimePicker_Resize(this, null); 
            }
        }

        //New Property, allows hiding of DropDown Button and Updown Button
        [Browsable(true), Category("Appearance"), Description("Hides DropDown and Spin Buttons, Allows keyed entry only.")]
        public bool ShowButtons
        {
            get
            {
                return mvarShowButtons;
            }
            set
            {
                //Do not allow Set Show Buttons when ReadOnly is true
                //all Buttons and Chexkbox are hidden when Control is Read Only
                if (!this.ReadOnly)
                {
                    mvarShowButtons = value;
                    if (mvarShowButtons)
                    {
                        ButtonWidth = BTNWIDTH;
                    }
                    else
                    {
                        ButtonWidth = 0;
                    }
                    DateTimePicker_Resize(this, null);
                }
            }
        }

        //Overrides base.ShowCheckBox
        [Browsable(true), Category("Appearance"), Description("Hides DropDown and Spin Buttons, Allows keyed entry only.")]
        public new bool ShowCheckBox
        {
            get
            {
                return base.ShowCheckBox;
            }
            set
            {
                //Do not allow set ShowCheckBox when ReadOnly is True
                //all Buttons and Chexkbox are hidden when Control is Read Only
                if (!this.ReadOnly)
                {
                    base.ShowCheckBox = value;
                    if (base.ShowCheckBox)
                    {
                        CheckWidth = BTNWIDTH;
                    }
                    else
                    {
                        CheckWidth = 0;
                    }
                    DateTimePicker_Resize(this, null);
                }
            }
        }

        //override Text, we want to set Get TextBox Text
        [Browsable(true), Category("Behavior"), Description("Date and Time displayed")]
        public new string Text
        {
            get
            {
                return txtDateTime.Text;
            }
            set
            {
                txtDateTime.Text = value;
                //Don't bother Formatting the TextBox if it's value is NullString
                //It will cause problems if you do
                if (value != "")
                {
                    string _txtDateTime = value;
                    DateTime _value = this.Value;
                    StDateTime.StDateTimeHelper.FormatStDateTime(ref _txtDateTime, ref localDateTimeObj, ref _value, FormatEx);
                    this.txtDateTime.Text = _txtDateTime;
                    this.Value = _value;
                }
            }
        }

        //Override bas.ShowUpDown
        [Browsable(true), Category("Appearance"), Description("Uses Updown control to select dates instead of Dropdown control")]
        public new bool ShowUpDown
        {
            get
            {
                return base.ShowUpDown;
            }
            set
            {
                //Do not allow set ShowUpDown when ReadOnly is True
                //All Buttons and Check box are hidden when Control is Read Only
                if (!this.ReadOnly)
                {
                    base.ShowUpDown = value;
                    txtDateTime.Text = "";
                }
            }
        }


        //New Property Read Only makes it possible to set TextBox to read only
        [Browsable(true), Category("Behavior"), Description("Used to set whether the control can be edited")]
        public bool ReadOnly
        {
            get
            {
                return txtDateTime.ReadOnly;
            }
            set
            {
                //If ReadOnly is true make sure ShowCheckBox, ShowUpDown and ShowButtons 
                //are false.
                //all Buttons and Check box are hidden when Control is Read Only
                //Be aware of the order these properties are set
                if (value)
                {
                    this.ShowCheckBox = false;
                    this.ShowUpDown = false;
                    this.ShowButtons = false;
                    this.txtDateTime.ReadOnly = value;
                }
                else
                {
                    txtDateTime.ReadOnly = value;
                    this.ShowButtons = true;
                }
            }
        }

        //New Property Makes it possible to link control to another DateTimePicker
        [Browsable(true), Category("Behavior"), Description("Set Get another Date Picker Control that this control receives data from.")]
        public string LinkedTo
        {
            get
            {
                return mvarLinkedTo;
            }
            set
            {
                mvarLinkedTo = value;
                LinkedArray.Clear();
                if (mvarLinkedTo != "" && mvarLinkedTo != null)
                {
                    string[] splitmvarLinkedTo = mvarLinkedTo.Split(",".ToCharArray());
                    for (int i = 0; i < splitmvarLinkedTo.Length; i++)
                    {
                        LinkedArray.Add(splitmvarLinkedTo[i].Trim());
                    }
                }
            }
        }

        #endregion

        #region DateTimePicker events

        private void DateTimePicker_Resize(object sender, System.EventArgs e)
        {
            try
            {
                ///Display txtDateTime
                //this.txtDateTime.Location = new System.Drawing.Point(-2 + CheckWidth, -2);
                //this.txtDateTime.Size = new System.Drawing.Size(this.Width -
                //    (4 + ButtonWidth) - CheckWidth, this.txtDateTime.Size.Height);
            }
            catch (Exception)
            {
                this.Enabled = false;
            }
        }

        private void DateTimePicker_FontChanged(Object sender, System.EventArgs e)
        {
            //Make sure TextBox Font =  Dtp Font
            txtDateTime.Font = this.Font;
        }

        private void DateTimePicker_BackColorChanged(Object sender, System.EventArgs e)
        {
            //Make sure TextBox BackColour =  Dtp Back Colour
            txtDateTime.BackColor = this.BackColor;
        }

        private void txtDateTime_BackColorChanged(Object sender, System.EventArgs e)
        {
            //Make sure DTP BackColour =  TextBox Back Colour
            if (txtDateTime.BackColor != this.BackColor)
            {
                this.BackColor = txtDateTime.BackColor;
            }
        }

        private void DateTimePicker_ForeColorChanged(Object sender, System.EventArgs e)
        {
            //Make sure TextBox Fore Colour =  Dtp Fore Colour
            txtDateTime.ForeColor = this.ForeColor;
        }

        private void FormatOrValueChanged(Object sender, System.EventArgs e)
        {
            ValidationOutCome(true, "");
            try
            {
                //if dtp Value changed 
                //Attempt to Format the TextBox String if Text is not NullString
                if (this.Text != "")
                {
                    #region Format_TextDateTime

                    string _txtDateTime = this.txtDateTime.Text;
                    DateTime _value = this.Value;
                    StDateTime.StDateTimeHelper.FormatStDateTime(ref _txtDateTime, ref localDateTimeObj, ref _value, FormatEx);
                    this.txtDateTime.Text = _txtDateTime;
                    this.Value = _value;
                    RaiseValueCustomChanged();
                    #endregion
                }
            }
            catch
            {
                ValidationOutCome(false, "Invalid Date - " + txtDateTime.Text + ", valid format is " + mvarCustomFormatMessage);
            }
        }

        private void txtDateTime_Enter(Object sender, System.EventArgs e)
        {
            SetDate = true;
            this.Value = DateTime.Now;
            SetDate = false;

        }

        private void txtDateTime_Leave(Object sender, System.EventArgs e)
        {
            if (!SetDate)
            {
                try
                {
                    SetDate = true;
                    ValidationOutCome(true, "");
                    //Attempt to Format the TextBox String if Text is not NullString
                    if (this.Text != "")
                    {
                        try
                        {
                            #region Format_TextDateTime

                            string _txtDateTime = Text;
                            DateTime _value = this.Value;
                            StDateTime.StDateTimeHelper.FormatStDateTime(
                                ref _txtDateTime,
                                ref localDateTimeObj,
                                ref _value, FormatEx);
                            this.txtDateTime.Text = _txtDateTime;
                            this.Value = _value;

                            #endregion
                            //if Link To is Not nullString
                            //Attempt to Link to the Specified LinkTo Controls
                            LinkToArray.Clear();
                            if (mvarLinkedTo != "" && mvarLinkedTo != null)
                            {
                                for (int j = 0; j < LinkedArray.Count; j++)
                                {
                                    for (int i = 0; i < this.Parent.Controls.Count; i++)
                                    {
                                        if (this.Parent.Controls[i].Name == LinkedArray[j].ToString() && this.Parent.Controls[i] is ucDateTimeChooser.DateTimePicker)
                                        {
                                            LinkTo = (ucDateTimeChooser.DateTimePicker)this.Parent.Controls[i];
                                            LinkToArray.Add(LinkTo);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            ValidationOutCome(false, "Invalid Date - " + txtDateTime.Text + ", valid format is " + mvarCustomFormatMessage);
                        }
                    }
                    //IF the LinkTo Object has been instantiated it's ok to attempt to set it's Text Value
                    for (int i = 0; i < LinkToArray.Count; i++)
                    {
                        if (this.LinkToArray[i] != null)
                        {
                            LinkTo = (ucDateTimeChooser.DateTimePicker)LinkToArray[i];
                            LinkTo.Text = this.Text;
                        }
                    }
                    SetDate = false;
                }
                finally
                {
                    RaiseValueCustomChanged();
                }
            }
        }

        private void DateTimePicker_Enter(Object sender, System.EventArgs e)
        {
            if (txtDateTime.Visible)
                this.txtDateTime.Focus();
        }

        private void DateTimePicker_DropDown(Object sender, System.EventArgs e)
        {
            bDroppedDown = true;
        }

        internal void DateTimePicker_CloseUp(object sender, System.EventArgs e)
        {
            try
            {
                if (bDroppedDown || this.ShowUpDown)
                {
                    if (!SetDate)
                    {
                        Process_CalendarDateChange();
                        bDroppedDown = false;
                        if (txtDateTime.Visible)
                            this.txtDateTime.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationOutCome(false, "Invalid DateTime Format" + ex.Message);
            }
        }

        protected override void OnValueChanged(System.EventArgs eventargs)
        {
            try
            {
                //if (bDroppedDown || this.ShowUpDown)
                {
                    if (!SetDate)
                    {
                        ValidationOutCome(true, "");
                        Process_CalendarDateChange();
                        if (txtDateTime.Visible)
                            this.txtDateTime.Focus();
                    }
                }
            }
            catch
            {
                ValidationOutCome(false, "Invalid Date - " + txtDateTime.Text + ", valid format is " + mvarCustomFormatMessage);
            }
        }

        internal void Process_CalendarDateChange()
        {
            try
            {
                string _txtDateTime = String.Empty;
                if (FormatEx == dtpCustomExtensions.dtpLongDateTimeWildCard)
                {
                    localDateTimeObj.Kind = StDateTime.DateTimeType.DateTime;
                    //if (!localDateTimeObj.IsDateValid)
                    //{
                    //    List<StDateTimeWildCards> WildCards = (List<StDateTimeWildCards>)Get_WildCardsChecked();
                    //    WildCards.Remove(StDateTimeWildCards.NullDay);
                    //    StDateTime.
                    //        StDateTimeHelper.Save_WildCards(WildCards, localDateTimeObj);
                    //}
                    if (!localDateTimeObj.IsTimeValid)
                    {
                        List<StDateTimeWildCards> WildCards = (List<StDateTimeWildCards>)Get_WildCardsChecked();
                        WildCards.Remove(StDateTimeWildCards.NullSecond);
                        StDateTime.
                            StDateTimeHelper.Save_WildCards(WildCards, localDateTimeObj);
                    }
                    StDateTime.StDateTimeHelper.StoreCustomDate_DateTime(Value, localDateTimeObj);
                    StDateTime.StDateTimeHelper.StoreCustomTime_DateTime(Value, localDateTimeObj);
                    _txtDateTime = StDateTime.StDateTimeHelper.ToStringCustomLongDateTime(localDateTimeObj);
                    this.txtDateTime.Text = _txtDateTime;
                }
                else if (FormatEx == dtpCustomExtensions.dtpLongDateWildCard)
                {
                    localDateTimeObj.Kind = StDateTime.DateTimeType.Date;
                    //if (!localDateTimeObj.IsDateValid)
                    //{
                    //    List<StDateTimeWildCards> WildCards = (List<StDateTimeWildCards>)Get_WildCardsChecked();
                    //    WildCards.Remove(StDateTimeWildCards.NullDay);
                    //    StDateTime.
                    //        StDateTimeHelper.Save_WildCards(WildCards, localDateTimeObj);
                    //}
                    StDateTime.StDateTimeHelper.StoreCustomDate_DateTime(Value, localDateTimeObj);
                    _txtDateTime = StDateTime.StDateTimeHelper.ToStringCustomLongDate(localDateTimeObj);
                    this.txtDateTime.Text = _txtDateTime;
                }
                else
                {
                    txtDateTime.Text = this.Value.ToString(CustomFormat, Application.CurrentCulture);
                }
                #region Format_TextDateTime

                _txtDateTime = this.txtDateTime.Text;
                DateTime _value = this.Value;
                StDateTime.
                    StDateTimeHelper.FormatStDateTime(ref _txtDateTime,
                    ref localDateTimeObj, ref _value, FormatEx);
                this.txtDateTime.Text = _txtDateTime;
                this.Value = _value;

                #endregion
            }
            finally
            {
                RaiseValueCustomChanged();
            }
        }

        #endregion

        internal bool Verify_ValueCustomChanged()
        {
            bool isVauleChanged = false;
            try
            {
                if (Prev_localDateTimeObj == null && localDateTimeObj != null)
                {
                    Prev_localDateTimeObj = new StDateTime(localDateTimeObj);
                    isVauleChanged = false;
                }
                else if (Prev_localDateTimeObj != null && localDateTimeObj == null)
                    isVauleChanged = false;
                else if (Prev_localDateTimeObj == localDateTimeObj)
                    isVauleChanged = false;
                else
                    isVauleChanged = Prev_localDateTimeObj.CompareTo(localDateTimeObj) != 0;
            }
            catch { }
            return isVauleChanged;
        }

        internal bool RaiseValueCustomChanged()
        {
            try
            {
                if (Verify_ValueCustomChanged())
                {
                    ValueCustomChanged(this, new EventArgs());
                    Prev_localDateTimeObj = new StDateTime(localDateTimeObj);
                    return true;
                }
                else
                    return false;
            }
            catch { }
            return false;
        }

        #region Save_WildCards

        public void Save_WildCards(IList<StDateTimeWildCards> WildCardsChecked)
        {
            DateTime Current_DateTime = DateTime.Now;
            try
            {
                StDateTime.StDateTimeHelper.Save_WildCards(WildCardsChecked, localDateTimeObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Saving WildCards", ex);
            }
        }

        public IList<StDateTimeWildCards> Get_WildCardsChecked()
        {
            IList<StDateTimeWildCards> WildCardsChecked = new List<StDateTimeWildCards>();
            try
            {
                WildCardsChecked = StDateTime.StDateTimeHelper.Get_WildCardsChecked(localDateTimeObj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading WildCards", ex);
            }
            return WildCardsChecked;
        }

        #endregion
    }
}
