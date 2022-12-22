using DLMS;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucLoadProfile : UserControl
    {
        #region Data_Members

        private List<LoadProfileChannelInfo> _AllSelectableChannels = null;
        private List<LoadProfileChannelInfo> _LoadProfileChannelsInfo = null;
        private List<LoadProfileChannelInfo> _LoadProfileChannelsInfo_2 = null;
        private TimeSpan _LoadProfilePeriod;
        private TimeSpan _LoadProfilePeriod_2;
        private TimeSpan _PQProfilePeriod;
        private LoadProfileController LoadProfile_Controller;
        private bool _enableLoadProfileChannelException = false;

        private static readonly Color bckColorError = Color.YellowGreen;
        private static readonly Color bckColor = Color.White;

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<LoadProfileChannelInfo> AllSelectableChannels
        {
            get { return _AllSelectableChannels; }
            set { _AllSelectableChannels = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<LoadProfileChannelInfo> LoadProfileChannelsInfo
        {
            get { return _LoadProfileChannelsInfo; }
            set { _LoadProfileChannelsInfo = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<LoadProfileChannelInfo> LoadProfileChannelsInfo_2
        {
            get { return _LoadProfileChannelsInfo_2; }
            set { _LoadProfileChannelsInfo_2 = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public TimeSpan LoadProfilePeriod
        {
            get { return _LoadProfilePeriod; }
            set { _LoadProfilePeriod = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public TimeSpan LoadProfilePeriod_2
        {
            get { return _LoadProfilePeriod_2; }
            set { _LoadProfilePeriod_2 = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public TimeSpan PQLoadProfilePeriod
        {
            get { return _PQProfilePeriod; }
            set { _PQProfilePeriod = value; }
        }

        public bool IsValidated
        {
            get
            {
                String ErrorMessage = String.Empty;
                if (errorProvider != null)
                {
                    foreach (Control itemCtr in gpLoadProfile.Controls)
                    {
                        if (itemCtr.GetType() == typeof(TextBox) ||
                            itemCtr.GetType() == typeof(ComboBox))
                        {
                            ErrorMessage = errorProvider.GetError(itemCtr);
                            if (String.IsNullOrEmpty(ErrorMessage) ||
                                String.IsNullOrWhiteSpace(ErrorMessage))
                                continue;
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<AccessRights> AccessRights { get; set; }

        #endregion

        public ucLoadProfile()
        {
            InitializeComponent();
            this.cmbLoadProfileScheme.Items.Add(LoadProfileScheme.Scheme_1);
            this.cmbLoadProfileScheme.Items.Add(LoadProfileScheme.Scheme_2);
            this.cmbLoadProfileScheme.Items.Add(LoadProfileScheme.PQ_Load_Profile);
            Init();
        }

        public ucLoadProfile(List<AccessRights> rights) : this()
        {
            AccessRights = rights;
            //ApplyAccessRights(AccessRights);
            Init();
        }

        private void ucLoadProfile_Load(object sender, EventArgs e)
        {
            this.cmbLoadProfileScheme.SelectedIndex = (this.cmbLoadProfileScheme.Items.Count > 0) ? 0 : -1;
            #region ///Apply Setting ErrorProvider

            if (errorProvider != null)
            {
                foreach (Control itemCtr in gpLoadProfile.Controls)
                {
                    if (itemCtr.GetType() == typeof(TextBox) ||
                        itemCtr.GetType() == typeof(ComboBox))
                        errorProvider.SetIconAlignment(itemCtr, ErrorIconAlignment.MiddleRight);
                }
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }

            #endregion
        }

        private void Init()
        {
            if (this._AllSelectableChannels == null)
                this._AllSelectableChannels = new List<LoadProfileChannelInfo>();
            if (this._LoadProfileChannelsInfo == null)
                this._LoadProfileChannelsInfo = new List<LoadProfileChannelInfo>();
            if (this._LoadProfileChannelsInfo_2 == null)
                this._LoadProfileChannelsInfo_2 = new List<LoadProfileChannelInfo>();
            //if (_LoadProfilePeriod == null)
                _LoadProfilePeriod = TimeSpan.FromMinutes(15.0);
            //if (_LoadProfilePeriod_2 == null)
                _LoadProfilePeriod_2 = TimeSpan.FromMinutes(30.0);
            _PQProfilePeriod = TimeSpan.FromMinutes(5.0);
            this.LoadProfile_Controller = new LoadProfileController();
        }

        public void Init_LoadProfiles(List<LoadProfileChannelInfo> channelsInfo)
        {
            try
            {
                this.AllSelectableChannels = channelsInfo;//LoadProfile_Controller.Get_SelectableLoadProfileChannels();
                lstAvailableChannels.Items.Clear();
                lstAvailableChannels.Items.AddRange(AllSelectableChannels.ToArray());

                //lstFixedChannels.Items.Clear();
                //lstFixedChannels.Items.AddRange(LoadProfile_Controller.Get_FixedChannels().ToArray());
                if (this.cmb_LP_Period.SelectedIndex < 0)
                    this.cmb_LP_Period.SelectedIndex = (this.cmb_LP_Period.Items.Count > 0) ? 0 : -1;
                _enableLoadProfileChannelException = true;
            }
            catch
            { }
        }

        public void ShowLoadProfile(List<LoadProfileChannelInfo> LPChannels, TimeSpan LoadProfilePeriod)
        {
            try
            {

                this.dgvLoadProfileChannels.DataSource = null;
                this.dgvLoadProfileChannels.DataSource = LPChannels;
                //this.dgvLoadProfileChannels.Columns["OBIS_Index"].ReadOnly = true;
                this.dgvLoadProfileChannels.Columns["Quantity_Name"].ReadOnly = true;
                this.dgvLoadProfileChannels.Columns["Unit"].ReadOnly = true;

                this.dgvLoadProfileChannels.Columns["CapturePeriod"].Visible = false;
                this.dgvLoadProfileChannels.Columns["Format"].Visible = false;
                this.dgvLoadProfileChannels.Columns["Multiplier"].Visible = false;
                this.dgvLoadProfileChannels.Columns["Quantity_Name"].Visible = true;
                this.dgvLoadProfileChannels.Columns["OBIS_Index"].Visible = false;
                this.dgvLoadProfileChannels.Columns["Channel_id"].Visible = false;
                this.dgvLoadProfileChannels.Columns["IsDataPresent"].Visible = false;
                //this.dgvLoadProfileChannels.Columns["IsDisplayData"].Visible = false;
                this.dgvLoadProfileChannels.Columns["CapturePeriodTicks"].Visible = false;

                this.lblChannelCount.Text = LPChannels.Count.ToString();
                UpdateLoadProfileInterval(LoadProfilePeriod);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private void SaveLoadProfile(List<LoadProfileChannelInfo> LPChannels, LoadProfileChannelInfo channelToAdd, LoadProfileScheme _scheme, ref TimeSpan LoadProfilePeriod)
        {
            try
            {
                TimeSpan tVal = SaveLoadProfileInterval();
                if (tVal.Ticks == 00)
                    throw new Exception("Load Profile Interval could not be set zero");

                if (channelToAdd == null)
                    throw new Exception("Load Profile Channel must be assigned quantity");

                LoadProfilePeriod = tVal;
                channelToAdd.CapturePeriod = LoadProfilePeriod;
                LPChannels.Add(channelToAdd);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private TimeSpan SaveLoadProfileInterval()
        {
            if (cmb_LP_Period.SelectedIndex == -1) return new TimeSpan(0, 0, 0);
            int loadProfileTimeInSec = 15;
            switch (cmb_LP_Period.SelectedIndex)
            {
                case 0:
                    loadProfileTimeInSec = 1 * 60;
                    break;
                case 1:
                    loadProfileTimeInSec = 5 * 60;
                    break;
                case 2:
                    loadProfileTimeInSec = 15 * 60;
                    break;
                case 3:
                    loadProfileTimeInSec = 30 * 60;
                    break;
                case 4:
                    loadProfileTimeInSec = 1 * (60 * 60);
                    break;
                case 5:
                    loadProfileTimeInSec = 2 * (60 * 60);
                    break;
                case 6:
                    loadProfileTimeInSec = 3 * (60 * 60);
                    break;
                case 7:
                    loadProfileTimeInSec = 4 * (60 * 60);
                    break;
                case 8:
                    loadProfileTimeInSec = 6 * (60 * 60);
                    break;
                case 9:
                    loadProfileTimeInSec = 8 * (60 * 60);
                    break;
                case 10:
                    loadProfileTimeInSec = 12 * (60 * 60);
                    break;
                case 11:
                    loadProfileTimeInSec = 24 * (60 * 60);
                    break;
                default:
                    throw new Exception("Invalid Load Profile Interval Value Selected");
                    break;
            }
            TimeSpan LoadProfilePeriod = new TimeSpan(0, 0, loadProfileTimeInSec);
            return LoadProfilePeriod;
        }

        private void UpdateLoadProfileInterval(TimeSpan value)
        {

            EventHandler SelectedIndexChaned = new EventHandler(txt_LoadProfile_Period_SelectedIndexChanged);
            try
            {
                /////Detach Event Handlers If Registered
                cmb_LP_Period.SelectedIndexChanged -= SelectedIndexChaned;

                int selectedIndex = 0;
                switch ((int)value.TotalSeconds)
                {
                    case (1 * 60):               ///1 Min
                        selectedIndex = 0;
                        break;
                    case (5 * 60):               ///5 Min
                        selectedIndex = 1;
                        break;
                    case (15 * 60):               ///15 Min
                        selectedIndex = 2;
                        break;
                    case (30 * 60):
                        selectedIndex = 3;      ///30 Min
                        break;
                    case (1 * 60 * 60):
                        selectedIndex = 4;      ///1 hour
                        break;
                    case (2 * 60 * 60):
                        selectedIndex = 5;      ///2 hour
                        break;
                    case (3 * 60 * 60):
                        selectedIndex = 6;      ///3 hour
                        break;
                    case (4 * 60 * 60):
                        selectedIndex = 7;      ///4 hour
                        break;
                    case (6 * 60 * 60):
                        selectedIndex = 8;      ///6 hour
                        break;
                    case (8 * 60 * 60):
                        selectedIndex = 9;      ///8 hour
                        break;

                    case (12 * 60 * 60):
                        selectedIndex = 10;      ///12 hour
                        break;
                    case (24 * 60 * 60):
                        selectedIndex = 11;      ///24 hour
                        break;
                    default:
                        {
                            ///throw new Exception(String.Format("Invalid Load Profile Interval Value {0}", value));
                            selectedIndex = -1;
                            break;
                        }
                }


                cmb_LP_Period.SelectedIndex = selectedIndex;
                if (cmb_LP_Period.SelectedIndex == -1)
                    cmb_LP_Period.BackColor = bckColorError;
                else
                    cmb_LP_Period.BackColor = bckColor;
            }
            finally
            {
                cmb_LP_Period.SelectedIndexChanged += SelectedIndexChaned;
            }

        }

        private void txt_LoadProfile_Period_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan LPPeriod = new TimeSpan();
                LoadProfileScheme lpScheme = (LoadProfileScheme)(cmbLoadProfileScheme.SelectedItem);
                LPPeriod = SaveLoadProfileInterval();
                if (lpScheme == LoadProfileScheme.Scheme_1)
                {
                    LoadProfilePeriod = LPPeriod;
                    foreach (var item in LoadProfileChannelsInfo)
                    {
                        item.CapturePeriod = LPPeriod;
                    }
                }
                else if (lpScheme == LoadProfileScheme.Scheme_2)
                {
                    LoadProfilePeriod_2 = LPPeriod;
                    foreach (var item in LoadProfileChannelsInfo_2)
                    {
                        item.CapturePeriod = LPPeriod;
                    }
                }
                else if (lpScheme == LoadProfileScheme.PQ_Load_Profile)
                {
                    PQLoadProfilePeriod = LPPeriod;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            this.cmbLoadProfileScheme.Items.Clear();
            cmb_LP_Period.Visible = false;
            lblInterval.Visible = false;

            if (Rights.Find(x => x.Read == true || x.Write == true) != null)
            {
                foreach (var item in Rights)
                {
                    _HelperAccessRights((LoadProfileParams)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                }
                if (this.cmbLoadProfileScheme.Items.Count > 0)
                    this.cmbLoadProfileScheme.SelectedIndex = 0;
                return true;
            }
            return false;
        }

        private void _HelperAccessRights(LoadProfileParams qty, bool read, bool write)
        {
            switch (qty)
            {
                case LoadProfileParams.LoadProfileChannels:
                    if (read || write)
                        this.cmbLoadProfileScheme.Items.Add(LoadProfileScheme.Scheme_1);
                    break;
                case LoadProfileParams.LoadProfileChannels2:
                    if (read || write)
                        this.cmbLoadProfileScheme.Items.Add(LoadProfileScheme.Scheme_2);
                    break;
                case LoadProfileParams.PQLoadProfile:
                    if (read || write)
                        this.cmbLoadProfileScheme.Items.Add(LoadProfileScheme.PQ_Load_Profile);
                    break;
                case LoadProfileParams.LoadProfileInterval:
                case LoadProfileParams.LoadProfileInterval2:
                    cmb_LP_Period.Visible = write || read;
                    lblInterval.Visible = write || read;
                    break;
                default:
                    break;
            }
        }

        public void showToGUI_LoadProfile()
        {
            try
            {
                if (cmbLoadProfileScheme.Items.Count > 0)
                {
                    LoadProfileScheme lpScheme = (LoadProfileScheme)(cmbLoadProfileScheme.SelectedItem);
                    switch (lpScheme)
                    {
                        case LoadProfileScheme.Scheme_1: ShowLoadProfile(LoadProfileChannelsInfo, LoadProfilePeriod); break;
                        case LoadProfileScheme.Scheme_2: ShowLoadProfile(LoadProfileChannelsInfo_2, LoadProfilePeriod_2); break;
                        case LoadProfileScheme.PQ_Load_Profile: ShowLoadProfile(new List<LoadProfileChannelInfo>(), PQLoadProfilePeriod); break;
                    }
                }
            }
            finally
            {

            }
        }

        #endregion

        //Flickering Reduction
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        private void btnAddChannel_Click(object sender, EventArgs e)
        {

            LoadProfileChannelInfo channel = (LoadProfileChannelInfo)((ListBox)sender).SelectedItem;
            LoadProfileChannelInfo ch = new LoadProfileChannelInfo()
            {
                Channel_id = channel.Channel_id,
                Multiplier = channel.Multiplier,
                Quantity_Name = channel.Quantity_Name,
                OBIS_Index = channel.OBIS_Index,
                SelectedAttribute = channel.SelectedAttribute,
                Unit = channel.Unit
            };
            LoadProfileScheme lpScheme = (LoadProfileScheme)(cmbLoadProfileScheme.SelectedItem);

            if (lpScheme == LoadProfileScheme.Scheme_1)
            {
                ch.Scheme = lpScheme;
                this.SaveLoadProfile(LoadProfileChannelsInfo, ch, lpScheme, ref _LoadProfilePeriod);
                this.ShowLoadProfile(LoadProfileChannelsInfo, LoadProfilePeriod);
            }
            else if (lpScheme == LoadProfileScheme.Scheme_2)
            {
                ch.Scheme = lpScheme;
                this.SaveLoadProfile(LoadProfileChannelsInfo_2, ch, lpScheme, ref _LoadProfilePeriod_2);
                this.ShowLoadProfile(LoadProfileChannelsInfo_2, LoadProfilePeriod_2);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LoadProfileScheme lpScheme = (LoadProfileScheme)(cmbLoadProfileScheme.SelectedItem);
            if (lpScheme == LoadProfileScheme.Scheme_1)
            {
                LoadProfileChannelsInfo.Clear();
            }
            else if (lpScheme == LoadProfileScheme.Scheme_2)
            {
                LoadProfileChannelsInfo_2.Clear();
            }
            this.dgvLoadProfileChannels.DataSource = null;
            this.lblChannelCount.Text = "0";
        }

        private void dgvLoadProfileChannels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Delete)
            {
                if (dgvLoadProfileChannels.Rows.Count > 0 && dgvLoadProfileChannels.SelectedRows.Count != 0)
                {
                    LoadProfileScheme lpScheme = (LoadProfileScheme)(cmbLoadProfileScheme.SelectedItem);
                    if (lpScheme == LoadProfileScheme.Scheme_1)
                    {
                        foreach (DataGridViewRow row in dgvLoadProfileChannels.SelectedRows)
                        {
                            Get_Index obisIndex = (Get_Index)Enum.Parse(typeof(Get_Index), row.Cells["OBIS_Index"].Value.ToString());
                            LoadProfileChannelInfo info = LoadProfileChannelsInfo.Find(x => x.OBIS_Index == obisIndex);
                            if (info != null)
                            {
                                LoadProfileChannelsInfo.Remove(info);
                            }
                        }
                        this.ShowLoadProfile(LoadProfileChannelsInfo, LoadProfilePeriod);
                    }
                    else if (lpScheme == LoadProfileScheme.Scheme_2)
                    {
                        foreach (DataGridViewRow row in dgvLoadProfileChannels.SelectedRows)
                        {
                            Get_Index obisIndex = (Get_Index)Enum.Parse(typeof(Get_Index), row.Cells["OBIS_Index"].Value.ToString());
                            LoadProfileChannelInfo info = LoadProfileChannelsInfo_2.Find(x => x.OBIS_Index == obisIndex);
                            if (info != null)
                            {
                                LoadProfileChannelsInfo_2.Remove(info);
                            }
                        }
                        this.ShowLoadProfile(LoadProfileChannelsInfo_2, LoadProfilePeriod_2);
                    }


                }
            }
        }

        private void cmbLoadProfileScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            showToGUI_LoadProfile();
        }

        private void lstAvailableChannels_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
