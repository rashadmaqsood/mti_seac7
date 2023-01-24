using comm.DataContainer;
using DLMS;
using DLMS.Comm;
using OptocomSoftware.Reporting;
using SEAC.Common;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Controllers;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SmartEyeControl_7.DB;
using SmartEyeControl_7.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ucCustomControl
{
    enum LPRequestType
    {
        GetEntries,
        GetChannels
    }
    public partial class pnlLoadProfile : UserControl
    {
        bool IsHideReportButton;
        ApplicationRight _Rights;
        LoadProfileController loadProfileController;
        public const int MAX_StatusWordCodeCount = 05;

        LoadProfileType LP_Data;
        LoadPRofile_1 loadData1;
        LoadPRofile_1 loadData2;
        LoadPRofile_2 PQ_LoadProfile;

        DataBaseController dbController;
        GenericProfileInfo loadProfileInfo;
        //LoadProfileData loadData; //commented in v4.8.39
        DLMS_Application_Process Application_Process;
        private ConnectionManager connectionManager;
        private ConnectionController ConnController;
        private ApplicationProcess_Controller AP_Controller;
        private ApplicationController application_Controller;
        private ParameterController Param_Controller;
        List<DBConnect.Insert_Record_LoadProfile> List_Records_LoadProfile = new List<DBConnect.Insert_Record_LoadProfile>();
        private DBConnect MyDataBase = new DBConnect();
        ds_LoadProfile DataSet_LoadProfile = new ds_LoadProfile();
        ReportViewer viewer_LoadProfile;
        private bool GetCompleted = false;
        private EntryDescripter entry = new EntryDescripter();
        private IAccessSelector selector;
        private Profile_Counter _lpCounter;
        Param_Customer_Code obj_CustomerCode;
        int unsuccess_count = 0;

        LPRequestType Param_RequestType = LPRequestType.GetEntries;
        private List<LoadProfileChannelInfo> ChannelsInfo;

        Instantaneous_Class Instantaneous_Class_obj;
        const int LP_DP = 1;//1; //2 set by M.Azeem Inayat
        const string LP_DP_s = "0.0";
        //bool IsWebFormat = false;
        //bool IsWapdaFormat = true;

        uint TotalLpCountInMeter = 0;
        int maxCounter = -1;
        const int MaxLP_Entries = 4632;
        private bool _isDateTimeWise;
        string Index_col = "Index", DateTime_col = "DateTime", Counter_col = "Counter", Interval_col = "Interval", StatusWord_col = "Status_Word";

        List<StatusWord> statusWordItems;

        public List<StatusWord> List_AvailableStatausWord
        {
            get;
            set;
        }

        public List<StatusWord> StatusWordItems
        {
            get { return statusWordItems; }
            private set { statusWordItems = value; }
        }

        public bool IsStatusWordMap_Available
        {
            get
            {
                return (statusWordItems != null && statusWordItems.Count > 0);
            }
        }

        public bool IsDateTimeWise
        {
            get { return _isDateTimeWise; }
            set { _isDateTimeWise = value; }
        }
        public int MaxCounter
        {
            get
            {
                try
                {
                    return MyDataBase.get_Counter("L", loadProfileController.CurrentConnectionInfo.MSN);
                }
                catch (Exception)
                {

                    return -1;
                }
            }
            set { maxCounter = value; }
        }
        public ApplicationController Application_Controller
        {
            get
            {
                //if (application_Controller == null)
                //    throw new Exception("Application not Initialized properly");
                return application_Controller;
            }
            set
            {
                if (value != application_Controller)
                {
                    application_Controller = value;
                    Application_Controller.PropertyChanged += new PropertyChangedEventHandler(Application_Controller_PropertyChanged);
                }
            }
        }

        private void Application_Controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                ///Okay IsIOBusy Status
                if ("IsIOBusy".Equals(e.PropertyName) && sender is ApplicationController)
                {
                    UpdateReadWriteStatus(Application_Controller.IsIOBusy);
                }

            }
            catch (Exception ex)
            {
            }
        }

        public LoadProfileScheme LP_Scheme { get; set; }

        public pnlLoadProfile()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            obj_CustomerCode = new Param_Customer_Code();
            Instantaneous_Class_obj = new Instantaneous_Class();
            dbController = new DataBaseController();

            loadData1 = new LoadPRofile_1();
            loadData2 = new LoadPRofile_1();
            PQ_LoadProfile = new LoadPRofile_2();
        }

        private void pnlLoadProfile_Load(object sender, EventArgs e)
        {
            try
            {
                if (Application_Controller != null)
                {
                    //cmbChannelNumber.SelectedIndex = 0; //v4.8.25
                    ///Load Profile Interface Init Work
                    //Application_Controller = ApplicationController.GetInstance();
                    Param_Controller = Application_Controller.Param_Controller;
                    Application_Process = Application_Controller.Applicationprocess_Controller.ApplicationProcess;
                    connectionManager = Application_Controller.ConnectionManager;
                    AP_Controller = Application_Controller.Applicationprocess_Controller;
                    ConnController = Application_Controller.ConnectionController;
                    loadProfileController = Application_Controller.LoadProfile_Controller;
                    this.IsDateTimeWise = false;
                    //cmbLoadProfileType.SelectedIndex = 0;

                    #region DataGrid Sorting Disable
                    foreach (ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn column in grid_LoadProfile.Columns)
                    { column.SortMode = DataGridViewColumnSortMode.NotSortable; }
                    #endregion
                    //combo_FromEntry.Items.Add(1);
                    //combo_ToEntry.Items.Add("Last");
                    //combo_FromEntry.SelectedIndex = 0;
                    //combo_ToEntry.SelectedIndex = 0;
                    entry.FromEntry = 1;
                    entry.ToEntry = 0;

                    InitObjectsStatusWord();

                }
            }
            catch (Exception ex)
            {
                ///throw new Exception("Error Initializing the Load Profile Interface Properly", ex);
                Notification Notifier = new Notification("Error Initializing", String.Format("Error Initializing the Load Profile Interface Properly\r\n{0}", ex.Message));
            }
        }

        private void btn_GetLoadProfile_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeLoadProfileEvents();

                bool IsEntryDiscriptor = !rbDateWiseLP.Checked;

                if (btn_GetLoadProfile.Text == "Get Load Profile")
                {
                    if (!Application_Process.Is_Association_Developed)
                    {
                        Notification Notifier = new Notification("Error", "Please connect to meter"); return;
                    }
                    else if (IsEntryDiscriptor &&
                             (combo_FromEntry.SelectedIndex == -1 || combo_ToEntry.SelectedIndex == -1))
                    {
                        Notification Notifier = new Notification("Error", "Please select entries to read"); return;
                    }
                    else if (!IsEntryDiscriptor && dtpFrom.Value > dtpTo.Value)
                    {
                        Notification Notifier = new Notification("Error", "Please select valid to and from date"); return;
                    }

                    DataSet_LoadProfile.DataTable_LoadProfile.Clear();
                    List_Records_LoadProfile.Clear();
                    grid_LoadProfile.Rows.Clear();
                    LP_Data.loadData = new LoadProfileData();
                    //computeAccessSelector(LP_Data.LoadProfileCounter, IsEntryDiscriptor);
                    _lpCounter = ComputeLoadProfileCounter(LP_Data.LoadProfileCounter);
                    Application_Controller.IsIOBusy = true;
                    loadProfileBgW.RunWorkerAsync();
                    progressBar1.Visible = true;
                }
                else if (btn_GetLoadProfile.Text == "Cancel")
                {
                    if (loadProfileBgW.IsBusy)
                    {
                        loadProfileBgW.CancelAsync();

                        if (loadProfileController != null)
                            loadProfileController.IsStop = true;

                        progressBar1.Visible = false;
                        btn_GetLoadProfile.Enabled = false;

                        loadProfileBgW.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error LP Data Read", ex.Message);
            }
        }

        //private void btn_GetLoadProfile_Click(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //    InitializeLoadProfileEvents();

        //    //    DataSet_LoadProfile.DataTable_LoadProfile.Clear();
        //    //    DataSet_LoadProfile.DataTable_LoadProfile2.Clear();
        //    //    DataSet_LoadProfile.dtLoadProfileGraph.Clear(); //v4.8.21
        //    //    List_Records_LoadProfile.Clear();
        //    //    loadData = new LoadProfileData();

        //    //    computeAccessSelector(loadProfileInfo.EntriesInUse);

        //    //    if (Application_Controller.IsIOBusy || loadProfileBgW.IsBusy)
        //    //    {
        //    //        loadProfileBgW.CancelAsync();
        //    //        loadProfileBgW.Dispose();

        //    //        throw new Exception("Data Read Operation in Progress");
        //    //    }

        //    //    if (Application_Process.Is_Association_Developed)
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        loadProfileBgW.RunWorkerAsync();

        //    //        // progressBar1.Visible = true;
        //    //        // v4.8.26
        //    //        AccurateOptocomSoftware.LocalCommon.clsStatus.ProgBarVisible(true);
        //    //        btnGenerateChart.Visible = btnGeneratePerDayReport.Visible =    //v4.8.25
        //    //        cmbChannelNumber.Visible = cmbPerDayDates.Visible =
        //    //        btn_Rpt_LoadProfile.Visible = false;
        //    //    }
        //    //    else
        //    //    {
        //    //        ///MessageBox.Show("Create Association to electrical via Management"); 
        //    //        Notification Notifier = new Notification("Error", "Please connect to meter");
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Notification Notifier = new Notification("Error", ex.Message);
        //    //}
        //    //finally
        //    //{

        //    //}


        //}

        private void InitializeLoadProfileEvents()
        {
            if (string.Equals(btn_GetLoadProfile.Text, "Get Load Profile", StringComparison.OrdinalIgnoreCase))
            {
                if (loadProfileBgW != null)
                {
                    if (loadProfileBgW.IsBusy)
                        loadProfileBgW.CancelAsync();
                    loadProfileBgW.Dispose();
                }

                this.loadProfileBgW = new BackgroundWorker();
                this.loadProfileBgW.WorkerSupportsCancellation = true;
                this.loadProfileBgW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loadProfileBgWorker_DoWork);
                this.loadProfileBgW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loadProfileBgWorker_RunWorkerCompleted);
            }
        }

        private List<DBConnect.Insert_Record_LoadProfile> Quantities_With_Difference_of_COunter(List<DBConnect.Insert_Record_LoadProfile> list)
        {
            //read counter
            int lastCounter = -1;
            lastCounter = this.MaxCounter;
            int j = 0;
            List<DBConnect.Insert_Record_LoadProfile> temp = new List<DBConnect.Insert_Record_LoadProfile>();
            for (int i = lastCounter * 4; i < list.Count; i++, j++)
            {
                temp.Add(list[i]);

            }
            return temp;
        }

        private void btn_ClearGrid_Click(object sender, EventArgs e)
        {
            ClearLoadProfileGrid();
        }

        private void loadProfileBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bwAsync = null;

            try
            {
                bwAsync = sender as BackgroundWorker;

                btn_Rpt_LoadProfile.Enabled = false;
                lblLPReadEntries.Text = "0";
                //LP_Data.loadProfileInfo = loadProfileController.Get_LoadProfileInfo();
                // selector = (IAccessSelector)entry; // By Rashad
                // loadData = loadProfileController.Get_LoadProfileData(selector);

                //bool fil_Missing_Count = false;

                //if (!rbDateWiseLP.Checked)
                //    fil_Missing_Count = true;
                LP_Data.loadData = new LoadProfileData();
                //List<LoadProfileChannelInfo> channelsList = loadProfileController.Get_LoadProfileChannelsInfo(_lpCounter,LP_Scheme);
                List<LoadProfileChannelInfo> channelsList = loadProfileController.Get_LoadProfileChannels(LP_Scheme);
                if (this.IsDateTimeWise)
                {
                    if (channelsList != null && channelsList.Count > 0)
                    {
                        _lpCounter.Period = channelsList[0].CapturePeriod;
                        _lpCounter.ChunkSize = Convert.ToUInt32(((_lpCounter.ToTime - _lpCounter.LastReadTime).TotalMinutes) / _lpCounter.Period.TotalMinutes);
                    }
                    loadProfileController.TryGet_LoadProfileDataByDateTime(LP_Scheme, _lpCounter, LP_Data.loadData, channelsList);
                }
                else
                {
                    _lpCounter.Max_Size = loadProfileController.LoadProfileInformation.MaxEntries;
                    loadProfileController.TryGet_LoadProfileDataInChunks(LP_Scheme, _lpCounter, LP_Data.loadData, channelsList, null, _lpCounter.Max_Size);
                }
                // Either Cancel Called
                if (loadProfileController != null && loadProfileController.IsStop && bwAsync != null)
                {
                    e.Cancel = true;
                }
                if (!IsHideReportButton)
                {
                    // get customer code
                    obj_CustomerCode.Customer_Code_String = (string)Application_Controller.Param_Controller.GETString_Any(obj_CustomerCode, Get_Index.Customer_Reference_No, 2);

                    // get active season
                    Instantaneous_Class_obj.Dummy_String = (string)Application_Controller.InstantaneousController.GETString_Any(Instantaneous_Class_obj, Get_Index.Active_Season, 2);
                    byte[] tempBytes = Encoding.ASCII.GetBytes(Instantaneous_Class_obj.Dummy_String);
                    Instantaneous_Class_obj.Active_Season = tempBytes[1];
                }

                // Get Status Word Map
                if (LP_Data != null && LP_Data.loadData != null && LP_Data.loadData.StatusWordAvailable)
                {
                    InitObjectsStatusWord();
                    StatusWord st_Word = null;
                    StatusWord st_WordNew = null;
                    Param_StatusWordMap Param_status_word_map_object = null;
                    StatusWordMapType _statusWordMapType = StatusWordMapType.StatusWordMap_1;

                    if (LP_Scheme == LoadProfileScheme.Load_Profile)
                    {
                        _statusWordMapType = StatusWordMapType.StatusWordMap_1;
                    }
                    else if (LP_Scheme == LoadProfileScheme.Load_Profile_Channel_2)
                    {
                        _statusWordMapType = StatusWordMapType.StatusWordMap_2;
                    }

                    // Update Status Word Map 2
                    Param_status_word_map_object = new Param_StatusWordMap();
                    //_statusWordMapType = StatusWordMapType.StatusWordMap_2;

                    Param_Controller.GET_Status_Word_Map(ref Param_status_word_map_object, _statusWordMapType);
                    st_Word = null;
                    st_WordNew = null;

                    foreach (var stWord in Param_status_word_map_object.StatusWordList)
                    {
                        st_Word = List_AvailableStatausWord.Find((x) => x != null && x.Code == stWord.Code);

                        if (st_Word != null && !string.IsNullOrEmpty(st_Word.Name))
                            st_WordNew = new StatusWord(st_Word);
                        else
                            st_WordNew = stWord;

                        this.StatusWordItems.Add(st_WordNew);
                    }
                }
                GetCompleted = true;

            }
            catch (Exception ex)
            {
                GetCompleted = false;
                // throw ex;
            }
        }

        private void loadProfileBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                if (e.Error != null)
                    throw e.Error;
                displayLoadProfile();
                Cursor.Current = Cursors.Arrow;
                if (grid_LoadProfile.Columns.Count > 0 &&
                    grid_LoadProfile.Columns.Contains(Counter_col))
                    grid_LoadProfile.Sort(grid_LoadProfile.Columns[Counter_col], ListSortDirection.Descending);

                // Add Row Sr Number
                //for (int j = 0; j < grid_LoadProfile.Rows.Count; j++)
                //{
                //    grid_LoadProfile.Rows[j].HeaderCell.Value = (j + 1).ToString();
                //}

                // MessageBox.Show("Load Profile Read complete!");
                Notification Notifier;
                if (GetCompleted)
                    Notifier = new Notification("Process Completed", "Load Profile Read complete");
                else if (!GetCompleted)
                {
                    if (loadProfileController != null && loadProfileController.IsStop)
                        Notifier = new Notification("Information", "You are stop the Read Load Profile");
                }
                else
                    Notifier = new Notification("Error", "Error Reading Load Profile");

            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Reading Load Profile", "" + ex.Message);
            }
            finally
            {
                progressBar1.Visible = false;
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_getloadprofile_Entries_Click(object sender, EventArgs e)
        {
            //v4.8.23

            try
            {
                var right_genRights = application_Controller.CurrentUser.CurrentAccessRights.GeneralRights.Find((x) => x.QuantityName == (Enum.GetName(typeof(GeneralRights), GeneralRights.LoadProfile)));
                //IsWapdaFormat = (right_genRights == null || !right_genRights.Write) ? true : false; //v4.8.31
            }
            catch (Exception) { }

            try
            {
                var _OtherRights = application_Controller.CurrentUser.CurrentAccessRights.OtherRights.Find((x) => x.QuantityName.Contains(OtherRights.IsWebFormat.ToString()));
                //IsWebFormat = (_OtherRights != null && _OtherRights.Read) ? true : false;
            }
            catch (Exception) { }

            //if (IsWapdaFormat && IsWebFormat) IsWebFormat = false;  //v4.8.31   Override Wapda Report on Web report

            combo_FromEntry.Items.Clear();
            combo_ToEntry.Items.Clear();

            if (Application_Controller.IsIOBusy)
            {
                throw new Exception("Data Read Operation in Progress");
            }

            Application_Controller.IsIOBusy = true;
            bgw_GetEntries.RunWorkerAsync();

        }

        private void bgw_GetEntries_DoWork(object sender, DoWorkEventArgs e)
        {

            if (Param_RequestType == LPRequestType.GetEntries)
            {
                var counter = loadProfileController.Get_LoadProfileInfo_Counter(LP_Scheme);
                if (LP_Data != null)
                    LP_Data.LoadProfileCounter = counter;
                LP_Data.MaxEntries = loadProfileController.Get_LP_MaxEntries(LP_Scheme);
            }
            else if (Param_RequestType == LPRequestType.GetChannels)
            {
                ChannelsInfo = loadProfileController.Get_LoadProfileChannels(LP_Scheme);
            }

        }

        private void bgw_GetEntries_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application_Controller.IsIOBusy = false;
            if (Param_RequestType == LPRequestType.GetEntries)
            {
                RefreshEntriesInUse();
            }
            else if (Param_RequestType == LPRequestType.GetChannels)
            {
                if (ChannelsInfo != null)
                {
                    PopulateChannels(ChannelsInfo);
                }
            }
        }

        private void combo_FromEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                entry.FromEntry = Convert.ToUInt32(combo_FromEntry.Text);

                int Prev_ToEntry = (combo_ToEntry.Items.Count > 0) ? Convert.ToInt32(combo_ToEntry.Text) : -1;

                combo_ToEntry.Items.Clear();
                for (int i = (Int32)entry.FromEntry + 1; i < TotalLpCountInMeter + 1; i++)
                    combo_ToEntry.Items.Add(i);
                // combo_ToEntry.Items.Add("Last");
                //   computeAccessSelector(loadProfileInfo.EntriesInUse);
                //v4.8.30
                if (entry.FromEntry < Prev_ToEntry)
                {
                    //combo_ToEntry.SelectedItem = Prev_ToEntry.ToString();
                    combo_ToEntry.SelectedIndex = combo_ToEntry.FindStringExact(Prev_ToEntry.ToString());
                    if (combo_ToEntry.SelectedIndex < 0)
                        combo_ToEntry.SelectedIndex = combo_ToEntry.Items.Count - 1;
                }
                else if (combo_ToEntry.Items.Count > 0)
                {
                    combo_ToEntry.SelectedIndex = combo_ToEntry.Items.Count - 1;
                }
            }

            catch { }
        }

        private void combo_ToEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (combo_ToEntry.Text == "Last")
                //    entry.ToEntry = 0;
                //else
                entry.ToEntry = Convert.ToUInt32(combo_ToEntry.Text);

                //computeAccessSelector(loadProfileInfo.EntriesInUse);
            }
            catch { }
        }

        private Profile_Counter ComputeLoadProfileCounter(uint LP_CurrentMaxCounter)
        {
            Profile_Counter counterInfo = new Profile_Counter();

            if (!rbDateWiseLP.Checked)
            {
                counterInfo.Meter_Counter = (uint)combo_ToEntry.SelectedIndex + 1;// Convert.ToUInt32(combo_ToEntry.Text); //+ (uint)1;
                counterInfo.DB_Counter = (uint)combo_FromEntry.SelectedIndex; //Convert.ToUInt32(combo_FromEntry.Text) - (uint)1;
                if (counterInfo.Meter_Counter > LP_CurrentMaxCounter)
                    counterInfo.Meter_Counter = LP_CurrentMaxCounter;
            }
            else
            {
                counterInfo.LastReadTime = dtpFrom.Value;
                counterInfo.ToTime = dtpTo.Value;
                counterInfo.Period = new TimeSpan(0, 1, 0);
                counterInfo.ChunkSize = 100;//Convert.ToUInt32(((dtpTo.Value - dtpFrom.Value).TotalMinutes)/30);
            }

            counterInfo.Max_Size = LP_Data.MaxEntries;

            //counterInfo.d
            return counterInfo;
        }
        private void computeAccessSelector(uint LP_CurrentMaxCounter, bool EntryDiscripter)
        {
            if (EntryDiscripter)
            {
                EntryDescripter entry = new EntryDescripter();

                uint MaxLP_Entries_Local = LP_Data.MaxEntries;//  MaxLP_Entries;
                #region // Channel_Selector For LP Scheme 2

                if (LP_Scheme == LoadProfileScheme.Daily_Load_Profile)
                {
                    // All Channel Option Not Selected
                    if (PQ_LoadProfile.IsChannelSelectorEnable)
                    {
                        #region // LoadProfileChannel Group Selected

                        if (PQ_LoadProfile.IsChannelGroupSelectedEnable)
                        {
                            int SelectedColumnCount = 0;
                            var Descripter = PQ_LoadProfile.ComputeDescripterByChannelGroups(ref SelectedColumnCount);

                            if (SelectedColumnCount <= 0)
                            {
                                throw new ArgumentException("Invalid Selected Column Count");
                            }

                            // Update Computed Selecter
                            entry = Descripter as EntryDescripter;
                        }

                        #endregion
                        #region // Load Profile Channel Count Selected

                        else
                        {
                            ushort toSelectedValue = 0;
                            if (cmb_HorChCountFilter.SelectedIndex != -1)
                            {
                                toSelectedValue = Convert.ToUInt16(cmb_HorChCountFilter.SelectedItem);
                            }

                            if (!(toSelectedValue > 1 &&
                                  toSelectedValue <= LoadPRofile_2.MAXChannelCount))
                            {
                                throw new ArgumentException("Invalid ToSelectedValue");
                            }

                            entry.ToSelectedValue = toSelectedValue;
                        }

                        #endregion
                    }
                }

                #endregion

                // Update MaxEntries For LP1,LP2
                if (loadProfileController.LPInfo_Data != null &&
                    loadProfileController.LPInfo_Data.MaxEntries > 0)
                {
                    MaxLP_Entries_Local = loadProfileController.LPInfo_Data.MaxEntries;
                    LP_CurrentMaxCounter = loadProfileController.LPInfo_Data.LoadProfileCounter;
                }

                LP_CurrentMaxCounter = Convert.ToUInt32(combo_ToEntry.Items[combo_ToEntry.Items.Count - 1]);
                entry.ToEntry = Convert.ToUInt32(combo_ToEntry.Text) + (uint)1;
                entry.FromEntry = Convert.ToUInt32(combo_FromEntry.Text);

                uint diff = entry.ToEntry - entry.FromEntry;
                uint entryToTemp = entry.ToEntry;

                if (diff > MaxLP_Entries_Local)    // version 3.18
                {
                    entry.FromEntry = 1;
                    entry.ToEntry = 0;
                    return;
                }
                else if (entry.ToEntry > MaxLP_Entries_Local || LP_CurrentMaxCounter > MaxLP_Entries_Local)
                {
                    entry.ToEntry = MaxLP_Entries_Local - (LP_CurrentMaxCounter - entryToTemp) /* + 1 */;// version 3.18
                    entry.FromEntry = entry.ToEntry - diff;
                }
                selector = entry;
            }
            else
            {
                RangeDescripter entry = new RangeDescripter();
                entry.FromEntry = dtpFrom.Value;
                entry.ToEntry = dtpTo.Value;
                entry.EncodingDataType = DataTypes._A19_datetime;

                List<CaptureObject> ObjectsToRead = new List<CaptureObject>();

                //List<LoadProfileChannelInfo> ChannelsInfo = chkLPChannels.SelectedItems.Cast<LoadProfileChannelInfo>().ToList();
                ////List<LoadProfileChannelInfo> ChannelsInfo = CheckedItems.Cast<LoadProfileChannelInfo>().ToList();

                ////ObjectsToRead = loadProfileController.GetCaptureObjectsFromLPChannels(ChannelsInfo);

                //foreach (LoadProfileChannelInfo channel in ChannelsInfo)
                //{

                //}

                foreach (object channel in chkLPChannels.CheckedItems)
                {
                    ObjectsToRead.Add(((LoadProfileChannelInfo)channel).ConvertToCaptureObject());
                }
                entry.Capture_Object_Definition = ObjectsToRead;
                selector = entry;

            }

            // else
            // {
            //     entry.ToEntry= entry.ToEntry-(LP_EntriesInUse-MaxLP_Entries)-1;
            //     entry.FromEntry = entry.ToEntry - diff-1;
            // }
        }


        public void UpdateReadWriteStatus(bool IsReadWriteBusy)
        {
            try
            {
                ///Disable IO Read Write btns 
                if (IsReadWriteBusy && !loadProfileBgW.IsBusy)
                {
                    btn_GetLoadProfile.Enabled = false;
                    btn_getloadprofile_Entries.Enabled = false;
                }
                ///Enable Read Write btns
                else
                {
                    btn_GetLoadProfile.Enabled = true;
                    btn_getloadprofile_Entries.Enabled = true;
                    //progressBar1.Visible = false;
                    //progressBar1.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                btn_GetLoadProfile.Enabled = true;
                //progressBar1.Visible = false;
                //progressBar1.Enabled = false;

            }
        }

        internal void Reset_State()
        {
            try
            {
                combo_FromEntry.Items.Clear();
                combo_ToEntry.Items.Clear();
                DataSet_LoadProfile.DataTable_LoadProfile.Clear();
                DataSet_LoadProfile.DataTable_LoadProfile2.Clear();
                DataSet_LoadProfile.dtLoadProfileGraph.Clear(); //v4.8.21
                List_Records_LoadProfile.Clear();
                //loadData = new LoadProfileData();
                ClearLoadProfileGrid();

                // Added by Sajid

                cmbLoadProfileType.Items.Clear();
                cmbLoadProfileType.Enabled = false;
                rbDateWiseLP.Checked = false;
                rbDateWiseLP.Enabled = false;
                tbLoadProfile.Visible = false;
                btn_GetLoadProfile.Enabled = false;
                tbLoadProfile.TabPages.Clear();

            }
            catch (Exception ex) { }
            finally
            {
                //IsWapdaFormat = true; //v4.8.31 true by default
                //IsWebFormat = false;
                #region Web Format Report Rights
                try
                {
                    var _OtherRights = Application_Controller.CurrentUser.CurrentAccessRights.OtherRights;
                    //var right_IsWebFormat = _OtherRights.Find((x) => x.QuantityName.Contains(OtherRights.IsWebFormat.ToString()));
                    //if (right_IsWebFormat != null && (right_IsWebFormat.Read)) IsWebFormat = true;
                }
                catch (Exception) { }
                #endregion

                #region Wapda Format Rights
                try
                {
                    var _GenRights = Application_Controller.CurrentUser.CurrentAccessRights.GeneralRights;
                    try
                    {
                        //var right_IsWapdaFormat = _GenRights.Find((x) => x.QuantityName.Contains(GeneralRights.Events.ToString()));
                        //if (right_IsWapdaFormat != null && right_IsWapdaFormat.Write) IsWapdaFormat = false; //v4.8.31
                    }
                    catch (Exception) { }
                }
                catch (Exception) { }
                #endregion
                //if (IsWapdaFormat && IsWebFormat) IsWebFormat = false;  //v4.8.31 WapdaFormat important than Web Format
            }
        }



        private void btnGenerateChart_Click(object sender, EventArgs e)
        {
            int from = 0, To = 0;
            //int ChannelNumber = 1;//Temporary comment by sajid//cmbChannelNumber.SelectedIndex + 1;
            int channelsCount = LP_Data.loadData.ChannelsInfo.Count;
            int ChannelNumber = 1;
            if (channelsCount > 4)
                ChannelNumber = cmbChannelNumber.SelectedIndex + 4;
            //string[] qty = new string[4];
            string MSN =
            application_Controller.ConnectionManager.ConnectionInfo.MSN;
            string meter_Model =
            loadProfileController.CurrentConnectionInfo.MeterInfo.MeterModel;
            //fill_DS_for_Report(false); //temp
            //for (int i = 0; i < 4; i++)
            //{
            //    qty[i] = LP_Data.loadData.ChannelsInfo[i+3].Quantity_Name;
            //}

            if (!this.IsDateTimeWise)
            {
                //v4.8.29
                To = Convert.ToInt32(combo_ToEntry.Text);
                from = Convert.ToInt32(combo_FromEntry.Text);

                //if ((Convert.ToInt32(combo_ToEntry.Text) - Convert.ToInt32(combo_FromEntry.Text)) > 48)
                if ((To - from) > 48)
                {
                    MessageBox.Show("Load Profile Chart Can Contain Maximum 48 Entries.");
                    return;
                }
            }

            //else
            //{
            MeterConfig meter_type_info = application_Controller.ConnectionController.SelectedMeter;
            //v4.8.21
            if (LocalCommon.GetReportFormat(application_Controller.CurrentUser.CurrentAccessRights) == ReportFormat.WAPDA_DDS)
            {
                DataSet_LoadProfile.dtLoadProfileGraph.Clear();
                string ChannelName = "", TimeInterval = "";
                for (int i = 0; i < channelsCount; i++)
                {
                    if (ChannelNumber == LP_Data.loadData.ChannelsInfo[i].Channel_id)
                    {
                        ChannelName = LP_Data.loadData.ChannelsInfo[i].Quantity_Name;
                        break;
                    }


                    //if (rbCh1.Checked && loadData.ChannelsInfo[i].Channel_id == 1)
                    //{
                    //    ChannelName = loadData.ChannelsInfo[i].Quantity_Name;
                    //    //TimeInterval = loadData.ChannelsInfo[i].CapturePeriod.TotalMinutes.ToString(); ;
                    //    break;
                    //}
                    //else if (rbCh2.Checked && loadData.ChannelsInfo[i].Channel_id == 2)
                    //{
                    //    ChannelName = loadData.ChannelsInfo[i].Quantity_Name;
                    //    break;
                    //}
                    //else if (rbCh3.Checked && loadData.ChannelsInfo[i].Channel_id == 3)
                    //{
                    //    ChannelName = loadData.ChannelsInfo[i].Quantity_Name;
                    //    break;
                    //}
                    //else if (rbCh4.Checked && loadData.ChannelsInfo[i].Channel_id == 4)
                    //{
                    //    ChannelName = loadData.ChannelsInfo[i].Quantity_Name;
                    //    break;
                    //}
                }
                int RowIndex = 0;
                foreach (DBConnect.Insert_Record_LoadProfile record in List_Records_LoadProfile)
                {
                    if (ChannelNumber == record.channel_id)
                    {
                        if (this.IsDateTimeWise || (record.counter >= from && record.counter <= To))
                        {
                            DataSet_LoadProfile.dtLoadProfileGraph.Rows.Add();
                            RowIndex = DataSet_LoadProfile.dtLoadProfileGraph.Rows.Count - 1;
                            DataSet_LoadProfile.dtLoadProfileGraph[RowIndex].DateTime = record.arrival_time;
                            DataSet_LoadProfile.dtLoadProfileGraph[RowIndex].Value =
                                Convert.ToDouble(LocalCommon.notRoundingOff(record.value.ToString(), LP_DP));
                            if (DataSet_LoadProfile.dtLoadProfileGraph.Rows.Count >= 48)
                                break;
                        }

                    }

                    //if (rbCh1.Checked && record.channel_id == 1)
                    //{
                    //    DataSet_LoadProfile.dtLoadProfileGraph.Rows.Add();
                    //    RowIndex = DataSet_LoadProfile.dtLoadProfileGraph.Rows.Count - 1; 
                    //    DataSet_LoadProfile.dtLoadProfileGraph[RowIndex].DateTime = record.arrival_time;
                    //    DataSet_LoadProfile.dtLoadProfileGraph[RowIndex].Value =
                    //        Convert.ToDouble(SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(record.value.ToString(), LP_DP));
                    //}
                    //else if (rbCh2.Checked && record.channel_id == 2)
                    //{
                    //    DataSet_LoadProfile.dtLoadProfileGraph.Rows.Add();
                    //    RowIndex = DataSet_LoadProfile.dtLoadProfileGraph.Rows.Count - 1;
                    //    DataSet_LoadProfile.dtLoadProfileGraph[RowIndex].DateTime = record.arrival_time;
                    //    DataSet_LoadProfile.dtLoadProfileGraph[RowIndex].Value =
                    //        Convert.ToDouble(SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(record.value.ToString(), LP_DP));
                    //}
                    //else if (rbCh3.Checked && record.channel_id == 3)
                    //{
                    //    DataSet_LoadProfile.dtLoadProfileGraph.Rows.Add();
                    //    RowIndex = DataSet_LoadProfile.dtLoadProfileGraph.Rows.Count - 1;
                    //    DataSet_LoadProfile.dtLoadProfileGraph[RowIndex].DateTime = record.arrival_time;
                    //    DataSet_LoadProfile.dtLoadProfileGraph[RowIndex].Value =
                    //        Convert.ToDouble(SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(record.value.ToString(), LP_DP));
                    //}
                    //else if (rbCh4.Checked && record.channel_id == 4)
                    //{
                    //    DataSet_LoadProfile.dtLoadProfileGraph.Rows.Add();
                    //    RowIndex = DataSet_LoadProfile.dtLoadProfileGraph.Rows.Count - 1;
                    //    DataSet_LoadProfile.dtLoadProfileGraph[RowIndex].DateTime = record.arrival_time;
                    //    DataSet_LoadProfile.dtLoadProfileGraph[RowIndex].Value =
                    //        Convert.ToDouble(SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(record.value.ToString(), LP_DP));
                    //}
                }

                //viewer_LoadProfile = new ReportViewer(
                //    DataSet_LoadProfile.dtLoadProfileGraph, 
                //    MSN, 
                //    meter_Model, 
                //    qty, 
                //    "", 
                //    obj_CustomerCode.Customer_Code_String, 
                //    Application_Controller.Applicationprocess_Controller.UserId, 
                //    Instantaneous_Class_obj.Active_Season.ToString(), 
                //    1, 
                //    combo_FromEntry.Text, 
                //    combo_ToEntry.Text, 
                //    meter_type_info
                //    );

                //MSN, customerCode, pid.ToString(), active_season, meter_type_info
                //MSN, Meter_Model, string[] Quantities, meter_DateTime, customerCode, pid, active_season, type, startEntry, EndEntry, Configs.meter_type_infoRow meter_type_info
                viewer_LoadProfile = new ReportViewer(
                                        DataSet_LoadProfile.dtLoadProfileGraph,
                                        MSN,
                                        ChannelName,
                                        obj_CustomerCode.Customer_Code_String,
                                        Application_Controller.Applicationprocess_Controller.UserId,
                                        Instantaneous_Class_obj.Active_Season.ToString(),
                                        combo_FromEntry.Text,
                                        combo_ToEntry.Text,
                                        meter_type_info, application_Controller.CurrentUser.CurrentAccessRights
                                        );
                viewer_LoadProfile.Show();
            }
            else if (LocalCommon.GetReportFormat(application_Controller.CurrentUser.CurrentAccessRights) == ReportFormat.WEB_GALAXY)
            {
                viewer_LoadProfile = new ReportViewer(DataSet_LoadProfile, MSN, meter_Model, GetLoadProfileChannelsList(LP_Scheme), "", obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(), ChannelNumber, combo_FromEntry.Text, combo_ToEntry.Text, meter_type_info, application_Controller.CurrentUser.CurrentAccessRights);

                //if (rbCh1.Checked)
                //    viewer_LoadProfile = new ReportViewer(DataSet_LoadProfile, MSN, meter_Model, qty, "", obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(), 1, combo_FromEntry.Text, combo_ToEntry.Text, meter_type_info, application_Controller.CurrentUser.CurrentAccessRights);
                //else if (rbCh2.Checked)
                //    viewer_LoadProfile = new ReportViewer(DataSet_LoadProfile, MSN, meter_Model, qty, "", obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(), 2, combo_FromEntry.Text, combo_ToEntry.Text, meter_type_info, application_Controller.CurrentUser.CurrentAccessRights);
                //else if (rbCh3.Checked)
                //    viewer_LoadProfile = new ReportViewer(DataSet_LoadProfile, MSN, meter_Model, qty, "", obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(), 3, combo_FromEntry.Text, combo_ToEntry.Text, meter_type_info, application_Controller.CurrentUser.CurrentAccessRights);
                //else if (rbCh4.Checked)
                //    viewer_LoadProfile = new ReportViewer(DataSet_LoadProfile, MSN, meter_Model, qty, "", obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(), 4, combo_FromEntry.Text, combo_ToEntry.Text, meter_type_info, application_Controller.CurrentUser.CurrentAccessRights);
                viewer_LoadProfile.Show();
            }
            else
            {
            }

            //}
        }

        private void btnGeneratePerDayReport_Click(object sender, EventArgs e)
        {
            fill_DS_for_Report(true);

            //string[] qty = new string[4];
            string MSN = application_Controller.ConnectionManager.ConnectionInfo.MSN;
            string meter_Model = loadProfileController.CurrentConnectionInfo.MeterInfo.MeterModel;

            //for (int i = 0; i < 4; i++)
            //{
            //    qty[i] = LP_Data.loadData.ChannelsInfo[i+3].Quantity_Name;
            //}
            MeterConfig meter_type_info = loadProfileController.CurrentConnectionInfo.MeterInfo;
            viewer_LoadProfile = new ReportViewer(DataSet_LoadProfile, MSN, meter_Model, GetLoadProfileChannelsList(LP_Scheme), "", obj_CustomerCode.Customer_Code_String,
                Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(), meter_type_info,
                application_Controller.CurrentUser.CurrentAccessRights, true); //,
                                                                               //((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));
            viewer_LoadProfile.Show();
        }

        public void displayLoadProfile()
        {
            try
            {
                bool isStatusWord = false;
                char[] tirm_Chars = ", ".ToCharArray();

                ClearLoadProfileGrid();

                // Empty Load Profile Data
                // No Load Profile data to update
                if (LP_Data.loadData == null ||
                    LP_Data.loadData.ChannelsInfo == null ||
                    LP_Data.loadData.ChannelsInfo.Count <= 0 ||
                    LP_Data.loadData.ChannelsInstances == null ||
                    LP_Data.loadData.ChannelsInstances.Count <= 0)
                {
                    //Notification Notifier = new Notification("Error", "Load Profile data not available");
                    return;
                }

                #region // Populate Grid with Data Only If Available

                if (LP_Data.loadData != null &&
                    LP_Data.loadData.ChannelsInfo != null)
                {
                    #region // Declaring arrays to store data of single row containin 4 value

                    int fixColumnCount = 0;// LP_Data.loadData.FixedObjectCount;

                    #endregion

                    // getting serial number of meter
                    // string MSN = application_Controller.ConnectionManager.ConnectionInfo.MSN;

                    #region // Adding static FIX Columns
                    groupBox1.Text = "Load Profile";//"Meter Serial #: "; // +MSN;
                    // This column is added by Sajid
                    if (LP_Data.loadData.ChannelsInstances.Count > 0)
                    {
                        grid_LoadProfile.Columns.Add(Index_col, Index_col); fixColumnCount++;
                    }
                    if (LP_Data.loadData.ClockAvailable)
                    {
                        grid_LoadProfile.Columns.Add(DateTime_col, DateTime_col); fixColumnCount++;
                    }
                    if (LP_Data.loadData.CounterAvailable)
                    {
                        grid_LoadProfile.Columns.Add(Counter_col, Counter_col); grid_LoadProfile.Columns[Counter_col].Visible = false; fixColumnCount++;
                    }
                    if (LP_Data.loadData.IntervalAvailable)
                    {
                        grid_LoadProfile.Columns.Add(Interval_col, Interval_col); fixColumnCount++;
                    }
                    if (LP_Data.loadData.StatusWordAvailable)
                    {
                        isStatusWord = true;
                        grid_LoadProfile.Columns.Add(StatusWord_col, StatusWord_col); fixColumnCount++;
                    }

                    #endregion
                    #region Arrays Declaration

                    int ArrayLength = LP_Data.loadData.ChannelsInfo.Count;// -fixColumnCount;
                    long[] quantityID_loadProfile = new long[ArrayLength];
                    Unit[] unit_loadProfile = new Unit[ArrayLength];
                    int[] scalar_loadProfile = new int[ArrayLength];
                    int[] channel_loadProfile = new int[ArrayLength];
                    double[] value_loadProfile = new double[ArrayLength];
                    DateTime dateTime_loadProfile = new DateTime();
                    int counter_loadProfile = 0;
                    int interval_loadProfile = 0;

                    #endregion
                    #region // Adding Dynamic Column

                    LoadProfileChannelInfo ChannelInfo = null;
                    List<LoadProfileChannelInfo> FixedChannelesInfo = loadProfileController.Get_FixedChannels();
                    LoadProfileItem LPDataItem = null;
                    // foreach (LoadProfileChannelInfo Channel in FixedChannelesInfo)
                    // {
                    //     ChannelInfo = LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Channel.OBIS_Index);
                    //     if (ChannelInfo != null) LP_Data.loadData.ChannelsInfo.Remove(ChannelInfo);
                    // }
                    string columnHeader = "";
                    int ChannelId = 1;
                    //int l = 0; // for index out of bound issue
                    for (int k = 0; k < LP_Data.loadData.ChannelsInfo.Count; k++)
                    {
                        ChannelInfo = LP_Data.loadData.ChannelsInfo[k];
                        if (FixedChannelesInfo.Find(x => x.OBIS_Index == ChannelInfo.OBIS_Index) != null || !ChannelInfo.IsDataPresent) continue;
                        // k + 1;// loadData.ChannelsInfo[k].Channel_id - 3;
                        //int ChannelId = 1;// ChannelInfo.Channel_id - fixColumnCount;

                        if (ChannelInfo.Unit != Unit.UnitLess)
                        {
                            columnHeader = String.Format("Channel Id {0}\r\n{1} {2}", ChannelId++, ChannelInfo.Quantity_Name, "(" + ChannelInfo.Unit + ")");
                            // for database
                            unit_loadProfile[k] = LP_Data.loadData.ChannelsInfo[k].Unit;
                            quantityID_loadProfile[k] = Convert.ToInt64(ChannelInfo.OBIS_Index);
                        }
                        else
                        {
                            columnHeader = String.Format("Channel Id {0}\r\n{1}", ChannelId++, ChannelInfo.Quantity_Name);
                            // database
                            quantityID_loadProfile[k] = Convert.ToInt64(ChannelInfo.OBIS_Index);
                        }
                        if (ChannelInfo.IsDataPresent) //&& ChannelInfo.IsDisplayData)
                            grid_LoadProfile.Columns.Add(columnHeader, columnHeader);
                        // maintaining list
                    }

                    #endregion

                    int gridColmIndex = 0;
                    int gridRowIndex = 0;
                    ChannelInfo = null;
                    LPDataItem = null;

                    // All columns added. Now Add values to respective column
                    // int columnCount = 3;
                    if (List_Records_LoadProfile != null)
                        List_Records_LoadProfile.Clear();
                    this.lblLPReadEntries.Text = LP_Data.loadData.ChannelsInstances.Count.ToString();
                    for (int j = 0; j < LP_Data.loadData.ChannelsInstances.Count; j++)
                    {
                        grid_LoadProfile.Rows.Add();
                        //grid_LoadProfile.Rows[j].HeaderCell.Value = (j + 1).ToString();

                        gridRowIndex = j;
                        gridColmIndex = 0;
                        grid_LoadProfile["index", j].Value = LP_Data.loadData.ChannelsInstances[j].Index;
                        gridColmIndex++;
                        #region // Adding Static FIX Column Value

                        if (LP_Data.loadData.ClockAvailable) // LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Meter_Clock) != null)
                        {
                            grid_LoadProfile["DateTime", j].Value = String.Format("{0:dd/MM/yyyy HH:mm:ss}",
                                                                       LP_Data.loadData.ChannelsInstances[j].DateTimeStamp);
                            gridColmIndex++;
                        }
                        if (LP_Data.loadData.CounterAvailable)// .ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Load_Profile_Counter) != null)
                        {
                            grid_LoadProfile["Counter", j].Value = LP_Data.loadData.ChannelsInstances[j].Counter;
                            gridColmIndex++;
                        }
                        if (LP_Data.loadData.IntervalAvailable) // LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Load_Profile_Capture_Period) != null)
                        {
                            grid_LoadProfile["Interval", j].Value = LP_Data.loadData.ChannelsInstances[j].Interval;
                            gridColmIndex++;
                        }

                        if (LP_Data.loadData.StatusWordAvailable) // LP_Data.loadData.ChannelsInfo.Find(x => (x.OBIS_Index == Get_Index.Status_Word ||
                                                                  //x.OBIS_Index == Get_Index.Status_Word2 ||
                                                                  //x.OBIS_Index == Get_Index.OT_STATUS_WORD_LP2)) != null)
                        {
                            string Status_Word_STR = string.Empty;
                            StringBuilder Status_Word_RAW_STR = new StringBuilder();
                            List<StatusWord> stWordsAll = null;
                            List<StatusWord> stWordsTrigger = null;

                            try
                            {

                                if (isStatusWord && IsStatusWordMap_Available)
                                {
                                    if (Param_Controller != null)
                                    {
                                        stWordsAll = Param_Controller.DecodeStatusWordMap(StatusWordItems, LP_Data.loadData.ChannelsInstances[j].StatusWord);
                                    }
                                }
                                else
                                {
                                    Status_Word_STR = LP_Data.loadData.ChannelsInstances[j].StatusWord;
                                }

                                if (stWordsAll != null && stWordsAll.Count > 0)
                                    stWordsTrigger = stWordsAll.FindAll((x) => x.IsTrigger);
                                else
                                    Status_Word_STR = "Err";

                                if (stWordsTrigger != null &&
                                    stWordsTrigger.Count > 0)
                                {
                                    stWordsTrigger.Sort((x, y) =>
                                    {
                                        string x_Val = string.Format("{0}{1}", x.Priority_Level, x.Code);
                                        string y_Val = string.Format("{0}{1}", y.Priority_Level, y.Code);

                                        return string.CompareOrdinal(x_Val, y_Val);
                                    });

                                    // Select Top MAX_StatusWordCodeCount
                                    stWordsTrigger = stWordsTrigger.GetRange(0, (stWordsTrigger.Count < MAX_StatusWordCodeCount) ? stWordsTrigger.Count : MAX_StatusWordCodeCount);
                                    foreach (var item in stWordsTrigger)
                                    {
                                        Status_Word_RAW_STR.AppendFormat("{0},", item.Display_Code);
                                    }

                                    Status_Word_STR = Status_Word_RAW_STR.ToString();
                                }

                                if (!string.IsNullOrEmpty(Status_Word_STR))
                                    Status_Word_STR = Status_Word_STR.TrimEnd(tirm_Chars);
                                //}
                                //else
                                //    Status_Word_STR = DLMS_Common.ArrayToHexString(LP_Data.loadData.ChannelsInstances[j].StatusWord.ToArray());
                            }
                            catch
                            {
                                Status_Word_STR = "Err";
                            }

                            grid_LoadProfile["Status_Word", j].Value = Status_Word_STR;
                            gridColmIndex++;
                        }

                        #endregion

                        // data for storing to database
                        dateTime_loadProfile = LP_Data.loadData.ChannelsInstances[j].DateTimeStamp;
                        counter_loadProfile = (int)LP_Data.loadData.ChannelsInstances[j].Counter;
                        interval_loadProfile = (int)LP_Data.loadData.ChannelsInstances[j].Interval;

                        LPDataItem = LP_Data.loadData.ChannelsInstances[j];
                        // LP_Data.loadData.ChannelsInfo = LP_Data.loadData.ChannelsInfo.Except<LoadProfileChannelInfo>(FixedChannelesInfo);

                        // for (int k = 0, valIndex = 0;                           // initialize
                        //     (k < LP_Data.loadData.ChannelsInfo.Count) &&
                        //     (valIndex < LPDataItem.LoadProfileInstance.Count);  // Condition 
                        //      k++, valIndex++)                                   // increment
                        for (int k = 0; k < LPDataItem.LoadProfileInstance.Count; k++)
                        {
                            //ChannelInfo = LP_Data.loadData.ChannelsInfo[k];
                            //  if (ChannelInfo == null || FixedChannelesInfo.Find(x => x.OBIS_Index == ChannelInfo.OBIS_Index) != null) continue;
                            // if (ChannelInfo.IsDataPresent && ChannelInfo.IsDisplayData)
                            //if (ChannelInfo.OBIS_Index == Get_Index.Meter_Clock) continue;
                            //else if (ChannelInfo.OBIS_Index == Get_Index.Load_Profile_Counter) continue;
                            //else if (ChannelInfo.OBIS_Index == Get_Index.Load_Profile_Capture_Period) continue;
                            //else if (ChannelInfo.OBIS_Index == Get_Index.Status_Word) continue;
                            //else if (ChannelInfo.OBIS_Index == Get_Index.Status_Word2) continue;
                            //else if (ChannelInfo.OBIS_Index == Get_Index.OT_STATUS_WORD_LP2) continue;
                            grid_LoadProfile[gridColmIndex++, gridRowIndex].Value =
                                        LocalCommon.notRoundingOff(LPDataItem.LoadProfileInstance[k].ToString(), LP_DP);
                            // for database
                            // if (ChannelInfo.IsDataPresent)
                            value_loadProfile[k] = (double)LP_Data.loadData.ChannelsInstances[j].LoadProfileInstance[k];
                            // else
                            //     value_loadProfile[k] = double.NaN;

                            channel_loadProfile[k] = k + 1; // ChannelInfo.Channel_id - fixColumnCount;
                        }

                        // columnCount = 0;
                        #region creating list for load profile

                        for (int value_count = 0, K = 0; value_count < LP_Data.loadData.ChannelsInfo.Count; value_count++)
                        {
                            ChannelInfo = LP_Data.loadData.ChannelsInfo[value_count];

                            // Skip FIX Channels
                            if (FixedChannelesInfo.Exists((x) => x != null &&
                                x.OBIS_Index == ChannelInfo.OBIS_Index))
                                continue;

                            DBConnect.Insert_Record_LoadProfile record_LoadProfile = new DBConnect.Insert_Record_LoadProfile();
                            record_LoadProfile.arrival_time = dateTime_loadProfile;
                            record_LoadProfile.counter = counter_loadProfile;
                            record_LoadProfile.interval = interval_loadProfile;
                            record_LoadProfile.value = value_loadProfile[K++];
                            record_LoadProfile.qty_id = quantityID_loadProfile[value_count];
                            record_LoadProfile.unit = unit_loadProfile[value_count].ToString();
                            record_LoadProfile.scalar = (sbyte)scalar_loadProfile[value_count];
                            record_LoadProfile.msn = "123"; // MSN;
                            record_LoadProfile.channel_id = ChannelInfo.Channel_id;// channel_loadProfile[value_count];

                            // now adding record to list
                            List_Records_LoadProfile.Add(record_LoadProfile);
                        }

                        #endregion
                    }

                    if (DataSet_LoadProfile.DataTable_LoadProfile != null)
                    {
                        DataSet_LoadProfile.DataTable_LoadProfile.Clear();
                    }

                    int dynamic_Channel_Count = 0;

                    if (fixColumnCount > 0)
                        dynamic_Channel_Count = ((LP_Data.loadData.ChannelsInfo.Count + 1) - fixColumnCount);
                    else
                        dynamic_Channel_Count = LP_Data.loadData.ChannelsInfo.Count;

                    // assignment to the data set for reporting
                    //fill_DS_for_Report(dynamic_Channel_Count);
                }

                #endregion
                else if (LP_Data.loadData.ChannelsInstances != null && LP_Data.loadData.ChannelsInstances.Count <= 0)
                {
                    ClearLoadProfileGrid();

                    Notification Notifier = new Notification("Error", "Load Profile data not available");
                    return;
                }
                else
                {
                    ClearLoadProfileGrid();
                }

                #region // if want to store load profile data to databases

                if (check_SaveToDB.Checked == true)
                {
                    // difference by checking last counter
                    // List_Records_LoadProfile = Quantities_With_Difference_of_COunter(List_Records_LoadProfile);
                    if (List_Records_LoadProfile.Count > 0)
                    {
                        if (MyDataBase.SaveToDataBase_LoadProfile(List_Records_LoadProfile, ref unsuccess_count))
                        {
                            Notification Notifier = new Notification("Successfull Saving", "Load Profile Data saved Successfuly in Database", 6000);
                            List_Records_LoadProfile.Clear();
                        }
                        else
                        {
                            if (MyDataBase.ServerConnected == false)
                            {
                                Notification Notifier = new Notification("Error Saving", "Error saving data to Database", 6000);
                            }
                        }
                    }
                    else
                    {
                        Notification notify = new Notification("Alert!", "Load Profile data is upto date", 6000);
                    }
                }

                #endregion
                btn_Rpt_LoadProfile.Enabled = true;

                if (unsuccess_count > 0)
                {
                    Notification n = new Notification("Error", "Unsuccessful database entries= " + unsuccess_count);
                    unsuccess_count = 0;
                }

                if (cmbPerDayDates.Items.Count > 0) cmbPerDayDates.Items.Clear();

                var dateTimeList = LP_Data.loadData.ChannelsInstances.Select(x => x.DateTimeStamp.ToString("dd/MM/yyyy")).Distinct().ToArray();
                cmbPerDayDates.Items.AddRange(dateTimeList);
                if (cmbPerDayDates.Items.Count > 0)
                    cmbPerDayDates.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error",
                                                         String.Format("Error displaying Load Profile data\r\n{0}", ex.Message));
            }
        }
        //public void displayLoadProfile()
        //{
        //    try
        //    {
        //        bool isStatusWord1 = false;
        //        bool isStatusWord2 = false;
        //        char[] tirm_Chars = ", ".ToCharArray();

        //        #region // Populate Grid with Data Only If Available

        //        if (LP_Data.loadData != null &&
        //            LP_Data.loadData.ChannelsInfo != null)
        //        {
        //            ClearLoadProfileGrid();

        //            #region // Declaring arrays to store data of single row containin 4 value



        //            int fixColumnCount = 0;// LP_Data.loadData.FixedObjectCount;

        //            #endregion

        //            // getting serial number of meter
        //            // string MSN = application_Controller.ConnectionManager.ConnectionInfo.MSN;

        //            #region // Adding static FIX Columns

        //            groupBox1.Text = "Load Profile";//"Meter Serial #: "; // +MSN;

        //            if (LP_Data.loadData.ChannelsInstances.Count > 0)
        //            {
        //                grid_LoadProfile.Columns.Add("index", "Index"); fixColumnCount++;
        //            }
        //            if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Meter_Clock) != null)
        //            {
        //                grid_LoadProfile.Columns.Add("DateTime", "Date_time"); fixColumnCount++;
        //            }
        //            if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Load_Profile_Counter) != null)
        //            {
        //                grid_LoadProfile.Columns.Add("Counter", "Counter"); fixColumnCount++;
        //            }
        //            if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Load_Profile_Capture_Period) != null)
        //            {
        //                grid_LoadProfile.Columns.Add("Interval", "Interval"); fixColumnCount++;
        //            }
        //            if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Status_Word) != null)
        //            {
        //                isStatusWord1 = true;
        //                grid_LoadProfile.Columns.Add("Status_Word", "Status_Word"); fixColumnCount++;
        //            }
        //            if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Status_Word2) != null)
        //            {
        //                isStatusWord1 = true;
        //                grid_LoadProfile.Columns.Add("Status_Word", "Status_Word1"); fixColumnCount++;
        //            }
        //            if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.OT_STATUS_WORD_LP2) != null)
        //            {
        //                isStatusWord2 = true;
        //                grid_LoadProfile.Columns.Add("Status_Word", "Status_Word2"); fixColumnCount++;
        //            }

        //            #endregion
        //            #region Arrays Declaration
        //            int ArrayLength = LP_Data.loadData.ChannelsInfo.Count;// -fixColumnCount;
        //            long[] quantityID_loadProfile = new long[ArrayLength];
        //            Unit[] unit_loadProfile = new Unit[ArrayLength];
        //            int[] scalar_loadProfile = new int[ArrayLength];
        //            int[] channel_loadProfile = new int[ArrayLength];
        //            double[] value_loadProfile = new double[ArrayLength];
        //            DateTime dateTime_loadProfile = new DateTime();
        //            int counter_loadProfile = 0;
        //            int interval_loadProfile = 0;
        //            #endregion
        //            #region // Adding Dynamic Column
        //            LoadProfileChannelInfo ChannelInfo = null;
        //            List<LoadProfileChannelInfo> FixedChannelesInfo = loadProfileController.Get_FixedChannels(); ;
        //            LoadProfileItem LPDataItem = null;
        //            //foreach (LoadProfileChannelInfo Channel in FixedChannelesInfo)
        //            //{
        //            //    ChannelInfo = LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Channel.OBIS_Index);
        //            //    if (ChannelInfo != null) LP_Data.loadData.ChannelsInfo.Remove(ChannelInfo);
        //            //}
        //            string columnHeader = "";
        //            int ChannelId = 1;
        //            //int l = 0; // for index out of bound issue
        //            for (int k = 0; k < LP_Data.loadData.ChannelsInfo.Count; k++)
        //            {
        //                ChannelInfo = LP_Data.loadData.ChannelsInfo[k];
        //                if (FixedChannelesInfo.Find(x => x.OBIS_Index == ChannelInfo.OBIS_Index) != null || !ChannelInfo.IsDataPresent) continue;
        //                // k + 1;// loadData.ChannelsInfo[k].Channel_id - 3;
        //                //int ChannelId = 1;// ChannelInfo.Channel_id - fixColumnCount;
        //                if (ChannelInfo != null && ChannelInfo.IsDataPresent)
        //                    ChannelInfo.IsDisplayData = true;

        //                if (ChannelInfo.Unit != Unit.UnitLess)
        //                {
        //                    columnHeader = String.Format("Channel Id {0}\r\n{1} {2}", ChannelId++, ChannelInfo.Quantity_Name, "(" + ChannelInfo.Unit + ")");
        //                    // for database
        //                    unit_loadProfile[k] = LP_Data.loadData.ChannelsInfo[k].Unit;
        //                    quantityID_loadProfile[k] = Convert.ToInt64(ChannelInfo.OBIS_Index);
        //                }
        //                else
        //                {
        //                    columnHeader = String.Format("Channel Id {0}\r\n{1}", ChannelId++, ChannelInfo.Quantity_Name);
        //                    // database
        //                    quantityID_loadProfile[k] = Convert.ToInt64(ChannelInfo.OBIS_Index);
        //                }
        //                if (ChannelInfo.IsDataPresent && ChannelInfo.IsDisplayData)
        //                    grid_LoadProfile.Columns.Add(columnHeader, columnHeader);
        //                // maintaining list
        //            }

        //            #endregion

        //            int gridColmIndex = 0;
        //            int gridRowIndex = 0;
        //            ChannelInfo = null;
        //            LPDataItem = null;

        //            // All columns added. Now Add values to respective column
        //            // int columnCount = 3;
        //            if (List_Records_LoadProfile != null)
        //                List_Records_LoadProfile.Clear();

        //            for (int j = 0; j < LP_Data.loadData.ChannelsInstances.Count; j++)
        //            {
        //                grid_LoadProfile.Rows.Add();
        //                grid_LoadProfile.Rows[j].HeaderCell.Value = (j + 1).ToString();

        //                gridRowIndex = j;
        //                gridColmIndex = 0;

        //                #region // Adding Static FIX Column Value
        //                if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Meter_Clock) != null)
        //                {
        //                    grid_LoadProfile["DateTime", j].Value = String.Format("{0:dd/MM/yyyy HH:mm:ss}",
        //                                                               LP_Data.loadData.ChannelsInstances[j].DateTimeStamp);
        //                    gridColmIndex++;
        //                }
        //                if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Load_Profile_Counter) != null)
        //                {
        //                    grid_LoadProfile["Counter", j].Value = LP_Data.loadData.ChannelsInstances[j].Counter;
        //                    gridColmIndex++;
        //                }
        //                if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Load_Profile_Counter) != null)
        //                {
        //                    grid_LoadProfile["Interval", j].Value = LP_Data.loadData.ChannelsInstances[j].Interval;
        //                    gridColmIndex++;
        //                }

        //                if (LP_Data.loadData.ChannelsInfo.Find(x => (x.OBIS_Index == Get_Index.Status_Word ||
        //                    x.OBIS_Index == Get_Index.Status_Word2 ||
        //                    x.OBIS_Index == Get_Index.OT_STATUS_WORD_LP2)) != null)
        //                {
        //                    string Status_Word_STR = string.Empty;
        //                    StringBuilder Status_Word_RAW_STR = new StringBuilder();
        //                    List<StatusWord> stWordsAll = null;
        //                    List<StatusWord> stWordsTrigger = null;

        //                    try
        //                    {
        //                        if (chk_Status_Word.Checked)
        //                        {
        //                            if (isStatusWord1 && IsStatusWordMap_1Available)
        //                            {
        //                                if (Param_Controller != null)
        //                                {
        //                                    stWordsAll = Param_Controller.DecodeStatusWordMap(StatusWordItems1, LP_Data.loadData.ChannelsInstances[j].StatusWord.ToArray());
        //                                }
        //                            }
        //                            else if (isStatusWord2 && IsStatusWordMap_2Available)
        //                            {
        //                                stWordsAll = Param_Controller.DecodeStatusWordMap(StatusWordItems2, LP_Data.loadData.ChannelsInstances[j].StatusWord.ToArray());
        //                            }
        //                            else
        //                            {
        //                                Status_Word_STR = DLMS_Common.ArrayToHexString(LP_Data.loadData.ChannelsInstances[j].StatusWord.ToArray());
        //                            }

        //                            if (stWordsAll != null && stWordsAll.Count > 0)
        //                                stWordsTrigger = stWordsAll.FindAll((x) => x.IsTrigger);
        //                            else
        //                                Status_Word_STR = "Err";

        //                            if (stWordsTrigger != null &&
        //                                stWordsTrigger.Count > 0)
        //                            {
        //                                stWordsTrigger.Sort((x, y) =>
        //                                {
        //                                    string x_Val = string.Format("{0}{1}", x.Priority_Level, x.Code);
        //                                    string y_Val = string.Format("{0}{1}", y.Priority_Level, y.Code);

        //                                    return string.CompareOrdinal(x_Val, y_Val);
        //                                });

        //                                // Select Top MAX_StatusWordCodeCount
        //                                stWordsTrigger = stWordsTrigger.GetRange(0, (stWordsTrigger.Count < MAX_StatusWordCodeCount) ? stWordsTrigger.Count : MAX_StatusWordCodeCount);
        //                                foreach (var item in stWordsTrigger)
        //                                {
        //                                    Status_Word_RAW_STR.AppendFormat("{0},", item.Display_Code);
        //                                }

        //                                Status_Word_STR = Status_Word_RAW_STR.ToString();
        //                            }

        //                            if (!string.IsNullOrEmpty(Status_Word_STR))
        //                                Status_Word_STR = Status_Word_STR.TrimEnd(tirm_Chars);
        //                        }
        //                        else
        //                            Status_Word_STR = DLMS_Common.ArrayToHexString(LP_Data.loadData.ChannelsInstances[j].StatusWord.ToArray());
        //                    }
        //                    catch(Exception ex)
        //                    {
        //                        Status_Word_STR = "Err";
        //                    }

        //                    grid_LoadProfile["Status_Word", j].Value = Status_Word_STR;
        //                    gridColmIndex++;
        //                }

        //                #endregion

        //                // data for storing to database
        //                dateTime_loadProfile = LP_Data.loadData.ChannelsInstances[j].DateTimeStamp;
        //                counter_loadProfile = (int)LP_Data.loadData.ChannelsInstances[j].Counter;
        //                interval_loadProfile = (int)LP_Data.loadData.ChannelsInstances[j].Interval;


        //                LPDataItem = LP_Data.loadData.ChannelsInstances[j];
        //                //LP_Data.loadData.ChannelsInfo = LP_Data.loadData.ChannelsInfo.Except<LoadProfileChannelInfo>(FixedChannelesInfo);

        //                //for (int k = 0, valIndex = 0;                           // initialize
        //                //    (k < LP_Data.loadData.ChannelsInfo.Count) &&
        //                //    (valIndex < LPDataItem.LoadProfileInstance.Count);  // Condition 
        //                //     k++, valIndex++)                                   // increment
        //                for (int k = 0; k < LPDataItem.LoadProfileInstance.Count; k++)
        //                {
        //                    ChannelInfo = LP_Data.loadData.ChannelsInfo[k];
        //                    if (ChannelInfo.OBIS_Index == Get_Index.Meter_Clock) continue;
        //                    // if (ChannelInfo == null || FixedChannelesInfo.Find(x => x.OBIS_Index == ChannelInfo.OBIS_Index) != null) continue;
        //                    //if (ChannelInfo.IsDataPresent && ChannelInfo.IsDisplayData)
        //                    grid_LoadProfile[gridColmIndex++, gridRowIndex].Value =
        //                                    LocalCommon.notRoundingOff(LPDataItem.LoadProfileInstance[k].ToString(), LP_DP);
        //                    // for database
        //                    //if (ChannelInfo.IsDataPresent)
        //                    value_loadProfile[k] = (double)LP_Data.loadData.ChannelsInstances[j].LoadProfileInstance[k];
        //                    //else
        //                    //    value_loadProfile[k] = double.NaN;

        //                    channel_loadProfile[k] = k + 1;//ChannelInfo.Channel_id - fixColumnCount;
        //                }

        //                // columnCount = 0;
        //                #region creating list for load profile

        //                for (int value_count = 0; value_count < LP_Data.loadData.ChannelsInfo.Count; value_count++)
        //                {
        //                    ChannelInfo = LP_Data.loadData.ChannelsInfo[value_count];

        //                    DBConnect.Insert_Record_LoadProfile record_LoadProfile = new DBConnect.Insert_Record_LoadProfile();
        //                    record_LoadProfile.arrival_time = dateTime_loadProfile;
        //                    record_LoadProfile.counter = counter_loadProfile;
        //                    record_LoadProfile.interval = interval_loadProfile;
        //                    record_LoadProfile.value = value_loadProfile[value_count];
        //                    record_LoadProfile.qty_id = quantityID_loadProfile[value_count];
        //                    record_LoadProfile.unit = unit_loadProfile[value_count].ToString();
        //                    record_LoadProfile.scalar = (sbyte)scalar_loadProfile[value_count];
        //                    record_LoadProfile.msn = "123"; // MSN;
        //                    record_LoadProfile.channel_id = channel_loadProfile[value_count];

        //                    // now adding record to list
        //                    List_Records_LoadProfile.Add(record_LoadProfile);
        //                }

        //                #endregion
        //            }

        //            if (DataSet_LoadProfile.DataTable_LoadProfile != null)
        //            {
        //                DataSet_LoadProfile.DataTable_LoadProfile.Clear();
        //            }
        //            // assignment to the data set for reporting

        //            if (cmbPerDayDates.Items.Count > 0) cmbPerDayDates.Items.Clear();

        //            var dateTimeList = LP_Data.loadData.ChannelsInstances.Select(x => x.DateTimeStamp.ToString("dd/MM/yyyy")).Distinct().ToArray();
        //            cmbPerDayDates.Items.AddRange(dateTimeList);
        //            if (cmbPerDayDates.Items.Count > 0) cmbPerDayDates.SelectedIndex = 0;

        //            //fill_DS_for_Report();
        //        }

        //        #endregion
        //        else if (LP_Data.loadData.ChannelsInstances != null && LP_Data.loadData.ChannelsInstances.Count <= 0)
        //        {
        //            ClearLoadProfileGrid();

        //            Notification Notifier = new Notification("Error", "Load Profile data not available");
        //            return;
        //        }
        //        else
        //        {
        //            ClearLoadProfileGrid();
        //        }

        //        #region // if want to store load profile data to databases

        //        if (check_SaveToDB.Checked == true)
        //        {
        //            // difference by checking last counter
        //            // List_Records_LoadProfile = Quantities_With_Difference_of_COunter(List_Records_LoadProfile);
        //            if (List_Records_LoadProfile.Count > 0)
        //            {

        //                if (MyDataBase.SaveToDataBase_LoadProfile(List_Records_LoadProfile, ref unsuccess_count))
        //                {
        //                    Notification Notifier = new Notification("Successfull Saving", "Load Profile Data saved Successfuly in Database", 6000);
        //                    List_Records_LoadProfile.Clear();
        //                }
        //                else
        //                {
        //                    if (MyDataBase.ServerConnected == false)
        //                    {
        //                        Notification Notifier = new Notification("Error Saving", "Error saving data to Database", 6000);
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                Notification notify = new Notification("Alert!", "Load Profile data is upto date", 6000);
        //            }
        //        }

        //        #endregion
        //        btn_Rpt_LoadProfile.Enabled = true;

        //        if (unsuccess_count > 0)
        //        {
        //            Notification n = new Notification("Error", "Unsuccessful database entries= " + unsuccess_count);
        //            unsuccess_count = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Notification Notifier = new Notification("Error",
        //            String.Format("Error displaying Load Profile data\r\n{0}", ex.Message));
        //    }

        //}

        private void grid_LoadProfile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var tirm_Chars = ";,.\r\n".ToCharArray();

            try
            {
                // Status Word Clicked
                if (grid_LoadProfile.Columns[e.ColumnIndex] != null &&
                    string.Equals(grid_LoadProfile.Columns[e.ColumnIndex].Name, "Status_Word"))
                {

                    string Status_Word_STR = string.Empty;
                    StringBuilder Status_Word_RAW_STR = new StringBuilder();
                    List<StatusWord> stWordsAll = null;
                    List<StatusWord> stWordsTrigger = null;

                    // Status Word Not Present
                    if (!(LP_Data.loadData.StatusWordAvailable))
                        return;

                    int Row_Index_number = 0;
                    int.TryParse(grid_LoadProfile["Index", e.RowIndex].Value + "", out Row_Index_number);

                    // Find lp_Data_Row By Index
                    var lp_Data_Row = LP_Data.loadData.ChannelsInstances.Find((x) => x.Index == Row_Index_number);

                    // Null Status Word
                    if (lp_Data_Row == null ||
                        lp_Data_Row.StatusWord == null)
                    {
                        return;
                    }
                    stWordsAll = Param_Controller.DecodeStatusWordMap(StatusWordItems, lp_Data_Row.StatusWord);


                    if (stWordsAll != null && stWordsAll.Count > 0)
                        stWordsTrigger = stWordsAll.FindAll((x) => x.IsTrigger);
                    else
                        Status_Word_STR = "Err";


                    if (stWordsTrigger != null && stWordsTrigger.Count > 0)
                    {
                        stWordsTrigger.Sort((x, y) => x.Code.CompareTo(y.Code));

                        // Select Top MAX_StatusWordCodeCount
                        // stWordsTrigger = stWordsTrigger.GetRange(0, (stWordsTrigger.Count < MAX_StatusWordCodeCount) ? stWordsTrigger.Count : MAX_StatusWordCodeCount);

                        foreach (var item in stWordsTrigger)
                        {
                            Status_Word_RAW_STR.AppendFormat("{0} {1} \r\n", item.Name, item.Display_Code);
                        }

                        Status_Word_STR = Status_Word_RAW_STR.ToString();
                    }

                    if (!string.IsNullOrEmpty(Status_Word_STR))
                        Status_Word_STR = Status_Word_STR.TrimEnd(tirm_Chars);

                    if (!string.IsNullOrEmpty(Status_Word_STR))
                    {
                        // Set Tool Tip Text
                        grid_LoadProfile[e.ColumnIndex, e.RowIndex].ToolTipText = Status_Word_STR;
                        MessageBox.Show(grid_LoadProfile, Status_Word_STR, "Status Word MAP", MessageBoxButtons.OK);
                    }
                    else
                    {
                        Notification Notifier = new Notification("Display Error", "No Status Word Data Available to dispaly");
                    }
                }

            }
            catch
            {
                Notification Notifier = new Notification("Error", "Error Display Status Word Map");
            }
        }

        //public void displayLoadProfile()
        //{
        //    try
        //    {

        //        #region // Populate Grid with Data Only If Available

        //        if (LP_Data.loadData != null &&
        //            LP_Data.loadData.ChannelsInfo != null)
        //        {
        //            ClearLoadProfileGrid();

        //            #region // Declaring arrays to store data of single row containin 4 value



        //            int fixColumnCount = 0;// LP_Data.loadData.FixedObjectCount;

        //            #endregion

        //            // getting serial number of meter
        //            // string MSN = application_Controller.ConnectionManager.ConnectionInfo.MSN;

        //            #region // Adding static FIX Columns

        //            groupBox1.Text = "Load Profile";//"Meter Serial #: "; // +MSN;

        //            if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Meter_Clock) != null)
        //            {
        //                grid_LoadProfile.Columns.Add("DateTime", "Date_time"); fixColumnCount++;
        //            }
        //            if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Load_Profile_Counter) != null)
        //            {
        //                grid_LoadProfile.Columns.Add("Counter", "Counter"); fixColumnCount++;
        //            }
        //            if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Load_Profile_Capture_Period) != null)
        //            {
        //                grid_LoadProfile.Columns.Add("Interval", "Interval"); fixColumnCount++;
        //            }
        //            if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Status_Word) != null)
        //            {
        //                grid_LoadProfile.Columns.Add("Status_Word", "Status_Word"); fixColumnCount++;
        //            }

        //            #endregion
        //            #region Arrays Declaration
        //            int ArrayLength = LP_Data.loadData.ChannelsInfo.Count;// -fixColumnCount;
        //            long[] quantityID_loadProfile = new long[ArrayLength];
        //            Unit[] unit_loadProfile = new Unit[ArrayLength];
        //            int[] scalar_loadProfile = new int[ArrayLength];
        //            int[] channel_loadProfile = new int[ArrayLength];
        //            double[] value_loadProfile = new double[ArrayLength];
        //            DateTime dateTime_loadProfile = new DateTime();
        //            int counter_loadProfile = 0;
        //            int interval_loadProfile = 0;
        //            #endregion
        //            #region // Adding Dynamic Column
        //            LoadProfileChannelInfo ChannelInfo = null;
        //            List<LoadProfileChannelInfo> FixedChannelesInfo = loadProfileController.Get_FixedChannels(); ;
        //            LoadProfileItem LPDataItem = null;
        //            //foreach (LoadProfileChannelInfo Channel in FixedChannelesInfo)
        //            //{
        //            //    ChannelInfo = LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Channel.OBIS_Index);
        //            //    if (ChannelInfo != null) LP_Data.loadData.ChannelsInfo.Remove(ChannelInfo);
        //            //}
        //            string columnHeader = "";
        //            int ChannelId = 1;
        //            //int l = 0; // for index out of bound issue
        //            for (int k = 0; k < LP_Data.loadData.ChannelsInfo.Count; k++)
        //            {
        //                ChannelInfo = LP_Data.loadData.ChannelsInfo[k];
        //                if (FixedChannelesInfo.Find(x => x.OBIS_Index == ChannelInfo.OBIS_Index) != null || !ChannelInfo.IsDataPresent) continue;
        //                // k + 1;// loadData.ChannelsInfo[k].Channel_id - 3;
        //                //int ChannelId = 1;// ChannelInfo.Channel_id - fixColumnCount;

        //                if (ChannelInfo.Unit != Unit.UnitLess)
        //                {
        //                    columnHeader = String.Format("Channel Id {0}\r\n{1} {2}", ChannelId++, ChannelInfo.Quantity_Name, "(" + ChannelInfo.Unit + ")");
        //                    // for database
        //                    unit_loadProfile[k] = LP_Data.loadData.ChannelsInfo[k].Unit;
        //                    quantityID_loadProfile[k] = Convert.ToInt64(ChannelInfo.OBIS_Index);
        //                }
        //                else
        //                {
        //                    columnHeader = String.Format("Channel Id {0}\r\n{1}", ChannelId++, ChannelInfo.Quantity_Name);
        //                    // database
        //                    quantityID_loadProfile[k] = Convert.ToInt64(ChannelInfo.OBIS_Index);
        //                }
        //                if (ChannelInfo.IsDataPresent && ChannelInfo.IsDisplayData)
        //                    grid_LoadProfile.Columns.Add(columnHeader, columnHeader);
        //                // maintaining list
        //            }

        //            #endregion

        //            int gridColmIndex = 0;
        //            int gridRowIndex = 0;
        //            ChannelInfo = null;
        //            LPDataItem = null;

        //            // All columns added. Now Add values to respective column
        //            // int columnCount = 3;
        //            if (List_Records_LoadProfile != null)
        //                List_Records_LoadProfile.Clear();

        //            for (int j = 0; j < LP_Data.loadData.ChannelsInstances.Count; j++)
        //            {
        //                grid_LoadProfile.Rows.Add();
        //                grid_LoadProfile.Rows[j].HeaderCell.Value = (j + 1).ToString();

        //                gridRowIndex = j;
        //                gridColmIndex = 0;

        //                #region // Adding Static FIX Column Value
        //                if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Meter_Clock) != null)
        //                {
        //                    grid_LoadProfile["DateTime", j].Value = String.Format("{0:dd/MM/yyyy HH:mm:ss}",
        //                                                               LP_Data.loadData.ChannelsInstances[j].DateTimeStamp);
        //                    gridColmIndex++;
        //                }
        //                if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Load_Profile_Counter) != null)
        //                {
        //                    grid_LoadProfile["Counter", j].Value = LP_Data.loadData.ChannelsInstances[j].Counter;
        //                    gridColmIndex++;
        //                }
        //                if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Load_Profile_Counter) != null)
        //                {
        //                    grid_LoadProfile["Interval", j].Value = LP_Data.loadData.ChannelsInstances[j].Interval;
        //                    gridColmIndex++;
        //                }
        //                if (LP_Data.loadData.ChannelsInfo.Find(x => x.OBIS_Index == Get_Index.Status_Word) != null)
        //                {
        //                    grid_LoadProfile["Status_Word", j].Value = DLMS_Common.ArrayToHexString(LP_Data.loadData.ChannelsInstances[j].StatusWord.ToArray());
        //                    gridColmIndex++;
        //                }

        //                #endregion

        //                // data for storing to database
        //                dateTime_loadProfile = LP_Data.loadData.ChannelsInstances[j].DateTimeStamp;
        //                counter_loadProfile = (int)LP_Data.loadData.ChannelsInstances[j].Counter;
        //                interval_loadProfile = (int)LP_Data.loadData.ChannelsInstances[j].Interval;


        //                LPDataItem = LP_Data.loadData.ChannelsInstances[j];
        //                //LP_Data.loadData.ChannelsInfo = LP_Data.loadData.ChannelsInfo.Except<LoadProfileChannelInfo>(FixedChannelesInfo);

        //                //for (int k = 0, valIndex = 0;                           // initialize
        //                //    (k < LP_Data.loadData.ChannelsInfo.Count) &&
        //                //    (valIndex < LPDataItem.LoadProfileInstance.Count);  // Condition 
        //                //     k++, valIndex++)                                   // increment
        //                for (int k = 0; k < LPDataItem.LoadProfileInstance.Count; k++)
        //                {
        //                    //ChannelInfo = LP_Data.loadData.ChannelsInfo[k];
        //                    // if (ChannelInfo == null || FixedChannelesInfo.Find(x => x.OBIS_Index == ChannelInfo.OBIS_Index) != null) continue;
        //                    //if (ChannelInfo.IsDataPresent && ChannelInfo.IsDisplayData)
        //                    grid_LoadProfile[gridColmIndex++, gridRowIndex].Value =
        //                                    Commons.notRoundingOff(LPDataItem.LoadProfileInstance[k].ToString(), LP_DP);
        //                    // for database
        //                    //if (ChannelInfo.IsDataPresent)
        //                    value_loadProfile[k] = (double)LP_Data.loadData.ChannelsInstances[j].LoadProfileInstance[k];
        //                    //else
        //                    //    value_loadProfile[k] = double.NaN;

        //                    channel_loadProfile[k] = k + 1;//ChannelInfo.Channel_id - fixColumnCount;
        //                }

        //                // columnCount = 0;
        //                #region creating list for load profile

        //                for (int value_count = 0; value_count < LP_Data.loadData.ChannelsInfo.Count; value_count++)
        //                {
        //                    ChannelInfo = LP_Data.loadData.ChannelsInfo[value_count];

        //                    DBConnect.Insert_Record_LoadProfile record_LoadProfile = new DBConnect.Insert_Record_LoadProfile();
        //                    record_LoadProfile.arrival_time = dateTime_loadProfile;
        //                    record_LoadProfile.counter = counter_loadProfile;
        //                    record_LoadProfile.interval = interval_loadProfile;
        //                    record_LoadProfile.value = value_loadProfile[value_count];
        //                    record_LoadProfile.qty_id = quantityID_loadProfile[value_count];
        //                    record_LoadProfile.unit = unit_loadProfile[value_count].ToString();
        //                    record_LoadProfile.scalar = (sbyte)scalar_loadProfile[value_count];
        //                    record_LoadProfile.msn = "123"; // MSN;
        //                    record_LoadProfile.channel_id = channel_loadProfile[value_count];

        //                    // now adding record to list
        //                    List_Records_LoadProfile.Add(record_LoadProfile);
        //                }

        //                #endregion
        //            }

        //            if (DataSet_LoadProfile.DataTable_LoadProfile != null)
        //            {
        //                DataSet_LoadProfile.DataTable_LoadProfile.Clear();
        //            }
        //            // assignment to the data set for reporting
        //            fill_DS_for_Report();
        //        }

        //        #endregion
        //        else if (LP_Data.loadData.ChannelsInstances != null && LP_Data.loadData.ChannelsInstances.Count <= 0)
        //        {
        //            ClearLoadProfileGrid();

        //            Notification Notifier = new Notification("Error", "Load Profile data not available");
        //            return;
        //        }
        //        else
        //        {
        //            ClearLoadProfileGrid();
        //        }

        //        #region // if want to store load profile data to databases

        //        if (check_SaveToDB.Checked == true)
        //        {
        //            // difference by checking last counter
        //            // List_Records_LoadProfile = Quantities_With_Difference_of_COunter(List_Records_LoadProfile);
        //            if (List_Records_LoadProfile.Count > 0)
        //            {

        //                if (MyDataBase.SaveToDataBase_LoadProfile(List_Records_LoadProfile, ref unsuccess_count))
        //                {
        //                    Notification Notifier = new Notification("Successfull Saving", "Load Profile Data saved Successfuly in Database", 6000);
        //                    List_Records_LoadProfile.Clear();
        //                }
        //                else
        //                {
        //                    if (MyDataBase.ServerConnected == false)
        //                    {
        //                        Notification Notifier = new Notification("Error Saving", "Error saving data to Database", 6000);
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                Notification notify = new Notification("Alert!", "Load Profile data is upto date", 6000);
        //            }
        //        }

        //        #endregion
        //        btn_Rpt_LoadProfile.Enabled = true;

        //        if (unsuccess_count > 0)
        //        {
        //            Notification n = new Notification("Error", "Unsuccessful database entries= " + unsuccess_count);
        //            unsuccess_count = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Notification Notifier = new Notification("Error",
        //            String.Format("Error displaying Load Profile data\r\n{0}", ex.Message));
        //    }

        //}

        //public void displayLoadProfile()
        //{
        //    #region IsWebFormat

        //    if (IsWebFormat)
        //    {
        //        try
        //        {
        //            //MonthlyBillingData.Sort((x, y) => x.BillingCounter.CompareTo(y.BillingCounter))
        //            //loadData.ChannelsInstances.Sort((x, y) => y.Counter.CompareTo(x.Counter));

        //            if (loadData != null)
        //            {
        //                ClearLoadProfileGrid();
        //                #region //Declaring arrays to store data of single row contain 4 values
        //                long[] quantityID_loadProfile = new long[4];
        //                Unit[] unit_loadProfile = new Unit[4];
        //                int[] scalar_loadProfile = new int[4];
        //                int[] channel_loadProfile = new int[4];
        //                double[] value_loadProfile = new double[4];
        //                DateTime dateTime_loadProfile = new DateTime();
        //                int counter_loadProfile = 0;
        //                int interval_loadProfile = 0;
        //                #endregion
        //                //getting serial number of meter
        //                string MSN = application_Controller.ConnectionManager.ConnectionInfo.MSN;
        //                //Adding static Columns
        //                groupBox1.Text = "Meter Serial #: " + MSN;
        //                grid_LoadProfile.Columns.Add("DateTime", "Date_time");
        //                grid_LoadProfile.Columns.Add("Counter", "Counter");
        //                grid_LoadProfile.Columns.Add("Interval", "Interval");
        //                int LP_Info_Col_Count = grid_LoadProfile.Columns.Count; //by Azeem Inayat
        //                grid_LoadProfile.Columns.Add("kW", "Active Power\n\rkW");
        //                grid_LoadProfile.Columns.Add("kVar", "Reactive Power\n\rkVar");
        //                grid_LoadProfile.Columns.Add("kWh", "Active Energy\n\rkWh");
        //                grid_LoadProfile.Columns.Add("kVarh", "Reactive Energy\n\rkVarh");

        //                CumKVarh_PrevInterval = CumKVarh_CurrentInterval = 0;
        //                int index = 0;
        //                for (int j = 0; j < loadData.ChannelsInstances.Count; j++)
        //                {
        //                    if ((uint)combo_FromEntry.SelectedIndex > 0 && j == 0) //First Record was read extra so skipped to add in grid
        //                    {
        //                        //data for storing to database
        //                        dateTime_loadProfile = loadData.ChannelsInstances[j].DateTimeStamp;
        //                        counter_loadProfile = (int)loadData.ChannelsInstances[j].Counter;
        //                        interval_loadProfile = (int)loadData.ChannelsInstances[j].Interval;

        //                        for (int k = LP_Info_Col_Count; k < loadData.ChannelsInfo.Count + LP_Info_Col_Count; k++)
        //                        {
        //                            //for database
        //                            value_loadProfile[k - LP_Info_Col_Count] = (double)loadData.ChannelsInstances[j].LoadProfileInstance[k - LP_Info_Col_Count];
        //                            channel_loadProfile[k - LP_Info_Col_Count] = loadData.ChannelsInfo[k - LP_Info_Col_Count].Channel_id;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        grid_LoadProfile.Rows.Add();
        //                        grid_LoadProfile.Rows[index].HeaderCell.Value = (index + 1).ToString();
        //                        grid_LoadProfile[0, index].Value = String.Format("{0:dd MMM, yyyy HH:mm}", loadData.ChannelsInstances[j].DateTimeStamp);
        //                        grid_LoadProfile[1, index].Value = loadData.ChannelsInstances[j].Counter;
        //                        grid_LoadProfile[2, index].Value = loadData.ChannelsInstances[j].Interval;

        //                        //data for storing to database
        //                        dateTime_loadProfile = loadData.ChannelsInstances[j].DateTimeStamp;
        //                        counter_loadProfile = (int)loadData.ChannelsInstances[j].Counter;
        //                        interval_loadProfile = (int)loadData.ChannelsInstances[j].Interval;

        //                        CumKVarh_CurrentInterval = float.Parse(float.Parse(loadData.ChannelsInstances[j].LoadProfileInstance[1].ToString()).ToString(LP_DP_s));
        //                        Consumed_kVarh = float.Parse((CumKVarh_CurrentInterval - CumKVarh_PrevInterval).ToString(LP_DP_s));

        //                        string kvar = LP_DP_s;
        //                        for (int k = LP_Info_Col_Count; k < loadData.ChannelsInfo.Count + LP_Info_Col_Count; k++)
        //                        {
        //                            switch (k)
        //                            {
        //                                case 3: //kw
        //                                    grid_LoadProfile[k, grid_LoadProfile.Rows.Count - 1].Value = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(loadData.
        //                                        ChannelsInstances[j].LoadProfileInstance[0].ToString(), LP_DP);
        //                                    break;
        //                                case 4: //kvar
        //                                    if (Consumed_kVarh > 0.0)
        //                                    {
        //                                        kvar = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff((((Consumed_kVarh / (interval_loadProfile * 60)) * 3600).ToString()), LP_DP);
        //                                    }
        //                                    grid_LoadProfile[k, grid_LoadProfile.Rows.Count - 1].Value = kvar;
        //                                    break;
        //                                case 5: //kwh
        //                                    grid_LoadProfile[k, grid_LoadProfile.Rows.Count - 1].Value = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(loadData.
        //                                        ChannelsInstances[j].LoadProfileInstance[2].ToString(), LP_DP);
        //                                    break;
        //                                case 6: //kvarh
        //                                    grid_LoadProfile[k, grid_LoadProfile.Rows.Count - 1].Value = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(loadData.
        //                                        ChannelsInstances[j].LoadProfileInstance[1].ToString(), LP_DP);
        //                                    break;
        //                            }
        //                            //for database
        //                            value_loadProfile[k - LP_Info_Col_Count] = (double)loadData.ChannelsInstances[j].LoadProfileInstance[k - LP_Info_Col_Count];
        //                            channel_loadProfile[k - LP_Info_Col_Count] = loadData.ChannelsInfo[k - LP_Info_Col_Count].Channel_id;
        //                        }
        //                        index++;
        //                    }
        //                    // columnCount = 0;
        //                    #region creating list for load profile
        //                    for (int value_count = 0; value_count < loadData.ChannelsInfo.Count; value_count++)
        //                    {
        //                        DBConnect.Insert_Record_LoadProfile record_LoadProfile = new DBConnect.Insert_Record_LoadProfile();
        //                        record_LoadProfile.arrival_time = dateTime_loadProfile;
        //                        record_LoadProfile.counter = counter_loadProfile;
        //                        record_LoadProfile.interval = interval_loadProfile;
        //                        record_LoadProfile.value = value_loadProfile[value_count];
        //                        record_LoadProfile.qty_id = quantityID_loadProfile[value_count];
        //                        record_LoadProfile.unit = unit_loadProfile[value_count].ToString();
        //                        record_LoadProfile.scalar = (sbyte)scalar_loadProfile[value_count];
        //                        record_LoadProfile.msn = MSN;
        //                        record_LoadProfile.channel_id = channel_loadProfile[value_count];

        //                        // now adding record to list
        //                        List_Records_LoadProfile.Add(record_LoadProfile);

        //                    }
        //                    #endregion

        //                    //Save Last Interval CumKvarh to calculate Consumed kvarh (Energy) which will be used in kvar (power) calculation
        //                    CumKVarh_PrevInterval = float.Parse(loadData.ChannelsInstances[j].LoadProfileInstance[1].ToString(LP_DP_s));
        //                }
        //                //List_Records_LoadProfile.Sort((x, y) => y.counter.CompareTo(x.counter));
        //                //List_Records_LoadProfile.OrderByDescending(o => o.counter).ThenBy(o => o.channel_id);
        //                grid_LoadProfile.Sort(grid_LoadProfile.Columns[1], ListSortDirection.Descending);
        //                grid_LoadProfile.Columns["Counter"].Visible = false;
        //                grid_LoadProfile.Columns["Interval"].Visible = false;

        //                //assignment to the data set for reporting
        //                //fill_DS_for_Report(); //commented in v4.8.25
        //            }
        //            if (loadData.ChannelsInstances.Count <= 0)
        //            {
        //                Notification Notifier = new Notification("Error", "Load Profile data not available");
        //                return;
        //            }
        //            btnGenerateChart.Visible = btnGeneratePerDayReport.Visible =    //v4.8.25
        //           cmbChannelNumber.Visible = cmbPerDayDates.Visible =
        //            btn_Rpt_LoadProfile.Visible = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            Notification Notifier = new Notification("Error",
        //                String.Format("Error displaying Load Profile data\r\n{0}", ex.Message));
        //        }
        //    }
        //    #endregion
        //    #region Default Format
        //    else //If it is not web format report
        //    {
        //        try
        //        {
        //            if (loadData != null)
        //            {
        //                ClearLoadProfileGrid();
        //                #region //Declaring arrays to store data of single row contain 4 values
        //                long[] quantityID_loadProfile = new long[4];
        //                Unit[] unit_loadProfile = new Unit[4];
        //                int[] scalar_loadProfile = new int[4];
        //                int[] channel_loadProfile = new int[4];
        //                double[] value_loadProfile = new double[4];
        //                DateTime dateTime_loadProfile = new DateTime();
        //                int counter_loadProfile = 0;
        //                int interval_loadProfile = 0;
        //                #endregion
        //                //getting serial number of meter
        //                string MSN = application_Controller.ConnectionManager.ConnectionInfo.MSN;

        //                //Adding static Columns
        //                groupBox1.Text = "Meter Serial #: " + MSN;
        //                grid_LoadProfile.Columns.Add("DateTime", "Date_time");
        //                grid_LoadProfile.Columns.Add("Counter", "Counter");
        //                grid_LoadProfile.Columns.Add("Interval", "Interval");

        //                //Adding Columns
        //                string columnHeader = "";
        //                for (int k = 0; k < loadData.ChannelsInfo.Count; k++)
        //                {
        //                    if (loadData.ChannelsInfo[k].Unit != Unit.UnitLess)
        //                    {
        //                        columnHeader = String.Format("Channel Id {0}\r\n{1} {2}", loadData.ChannelsInfo[k].Channel_id,
        //                            loadData.ChannelsInfo[k].Quantity_Name, "(" + loadData.ChannelsInfo[k].Unit + ")");
        //                        //for database
        //                        unit_loadProfile[k] = loadData.ChannelsInfo[k].Unit;
        //                        quantityID_loadProfile[k] = Convert.ToInt64(loadData.ChannelsInfo[k].OBIS_Index);
        //                    }
        //                    else
        //                    {
        //                        columnHeader = String.Format("Channel Id {0}\r\n{1}", loadData.ChannelsInfo[k].Channel_id,
        //                           loadData.ChannelsInfo[k].Quantity_Name);
        //                        //database
        //                        quantityID_loadProfile[k] = Convert.ToInt64(loadData.ChannelsInfo[k].OBIS_Index);
        //                    }
        //                    grid_LoadProfile.Columns.Add(columnHeader, columnHeader);
        //                    //maintaining list
        //                }

        //                //All columns added. Now Add values to respective column
        //                // int columnCount = 3;

        //                //v4.8.25 Start
        //                if (cmbPerDayDates.Items.Count > 0) cmbPerDayDates.Items.Clear();

        //                var dateTimeList = loadData.ChannelsInstances.Select(x => x.DateTimeStamp.ToString("dd/MM/yyyy")).Distinct().ToArray();
        //                cmbPerDayDates.Items.AddRange(dateTimeList);
        //                if (cmbPerDayDates.Items.Count > 0) cmbPerDayDates.SelectedIndex = 0;

        //                //v4.8.25 End

        //                for (int j = 0; j < loadData.ChannelsInstances.Count; j++)
        //                {
        //                    grid_LoadProfile.Rows.Add();
        //                    grid_LoadProfile.Rows[j].HeaderCell.Value = (j + 1).ToString();
        //                    grid_LoadProfile[0, j].Value = String.Format("{0:dd MMM, yyyy HH:mm}", loadData.ChannelsInstances[j].DateTimeStamp);
        //                    grid_LoadProfile[1, j].Value = loadData.ChannelsInstances[j].Counter;
        //                    grid_LoadProfile[2, j].Value = loadData.ChannelsInstances[j].Interval;

        //                    //data for storing to database
        //                    dateTime_loadProfile = loadData.ChannelsInstances[j].DateTimeStamp;
        //                    counter_loadProfile = (int)loadData.ChannelsInstances[j].Counter;
        //                    interval_loadProfile = (int)loadData.ChannelsInstances[j].Interval;

        //                    for (int k = 3; k < loadData.ChannelsInfo.Count + 3; k++)
        //                    {
        //                        grid_LoadProfile[k, grid_LoadProfile.Rows.Count - 1].Value = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(loadData.
        //                            ChannelsInstances[j].LoadProfileInstance[k - 3].ToString(), LP_DP);
        //                        //for database
        //                        value_loadProfile[k - 3] = (double)loadData.ChannelsInstances[j].LoadProfileInstance[k - 3];
        //                        channel_loadProfile[k - 3] = loadData.ChannelsInfo[k - 3].Channel_id;
        //                    }
        //                    // columnCount = 0;
        //                    #region creating list for load profile
        //                    for (int value_count = 0; value_count < loadData.ChannelsInfo.Count; value_count++)
        //                    {
        //                        DBConnect.Insert_Record_LoadProfile record_LoadProfile = new DBConnect.Insert_Record_LoadProfile();
        //                        record_LoadProfile.arrival_time = dateTime_loadProfile;
        //                        record_LoadProfile.counter = counter_loadProfile;
        //                        record_LoadProfile.interval = interval_loadProfile;
        //                        record_LoadProfile.value = value_loadProfile[value_count];
        //                        record_LoadProfile.qty_id = quantityID_loadProfile[value_count];
        //                        record_LoadProfile.unit = unit_loadProfile[value_count].ToString();
        //                        record_LoadProfile.scalar = (sbyte)scalar_loadProfile[value_count];
        //                        record_LoadProfile.msn = MSN;
        //                        record_LoadProfile.channel_id = channel_loadProfile[value_count];

        //                        // now adding record to list
        //                        List_Records_LoadProfile.Add(record_LoadProfile);
        //                    }
        //                    #endregion
        //                }
        //                //assignment to the data set for reporting
        //                //fill_DS_for_Report(); //commented in v4.8.25
        //            }
        //            if (loadData == null || loadData.ChannelsInstances.Count <= 0)
        //            {
        //                Notification Notifier = new Notification("Error", "Load Profile data not available");
        //                return;
        //            }
        //            btnGenerateChart.Visible = btnGeneratePerDayReport.Visible =    //v4.8.25
        //            cmbChannelNumber.Visible = cmbPerDayDates.Visible =
        //            btn_Rpt_LoadProfile.Visible = true;

        //        }
        //        catch (Exception ex)
        //        {
        //            Notification Notifier = new Notification("Error",
        //                String.Format("Error displaying Load Profile data\r\n{0}", ex.Message));
        //        }
        //    }
        //    #endregion

        //}

        private void ClearLoadProfileGrid()
        {
            //grid_LoadProfile.Rows.Clear();
            //grid_LoadProfile.Columns.Clear();
            for (int i = grid_LoadProfile.Rows.Count - 1; i > 0; i--)
            {
                grid_LoadProfile.Rows.RemoveAt(i);
            }
            for (int i = grid_LoadProfile.Columns.Count - 1; i >= 0; i--)
            {
                grid_LoadProfile.Columns.RemoveAt(i);
            }
        }

        float CumKVarh_PrevInterval = 0;
        float CumKVarh_CurrentInterval = 0;
        float Consumed_kVarh = 0;
        float kVar = 0;
        int IntervalTime_Current = 0;
        int IntervalTime_Prev = 0;
        int PrevKvarhIndex = 0;

        public void fill_DS_for_Report(int Channel_Count = 1)
        {
            //IsWapdaFormat = false; //for test
            #region WebFormat
            if (LocalCommon.GetReportFormat(application_Controller.CurrentUser.CurrentAccessRights) == ReportFormat.WEB_GALAXY)
            {
                CumKVarh_PrevInterval = 0;
                CumKVarh_CurrentInterval = 0;
                Consumed_kVarh = 0;
                kVar = 0;
                IntervalTime_Current = 0;
                IntervalTime_Prev = 0;

                string kw_ch1 = "", kvar_ch2 = "", kwh_ch3 = "", kvarh_ch4 = "";
                bool kvarhFound = false;

                //List_Records_LoadProfile.Sort((x, y) => y.counter.CompareTo(x.counter));
                int LoadProfileRecordCount = List_Records_LoadProfile.Count;
                PrevKvarhIndex = LoadProfileRecordCount - 4 - 1 - 2; //4=total 4 channels,  1=Count to index,   2=kvarh index is 2 from last to first

                for (int counter = LoadProfileRecordCount - 1; counter >= 0; counter--)
                {
                    //kw, kvarh, kwh, I
                    if (counter <= 3 && (uint)combo_FromEntry.SelectedIndex > 0)
                    {
                        string msg = "Yes Last Record Skipped";
                        break;
                    }
                    else
                    {
                        kvarhFound = false;
                        kvar_ch2 = LP_DP_s;

                        DataSet_LoadProfile.DataTable_LoadProfile.Rows.Add();
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Date = List_Records_LoadProfile[counter].arrival_time.ToString("dd MMM, yyyy HH:mm");

                        //kw_ch1 = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP);
                        //kvarh_ch4 = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP); //Saving kvarh value from Channel-2
                        //kvar_ch2 = Calculate_kVar_by_KVarh(counter); kvarhFound = true; kvarhIndex = counter;
                        //kwh_ch3 = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP); //Saving kwh value from Channel-3
                        //CumKVarh_PrevInterval = float.Parse(float.Parse(List_Records_LoadProfile[kvarhIndex].value.ToString()).ToString(LP_DP_s));
                        //counter++;

                        kwh_ch3 = LocalCommon.notRoundingOff(List_Records_LoadProfile[--counter].value.ToString(), LP_DP); //Saving kwh value from Channel-3
                        kvarh_ch4 = LocalCommon.notRoundingOff(List_Records_LoadProfile[--counter].value.ToString(), LP_DP); //Saving kvarh value from Channel-2
                        if (PrevKvarhIndex >= 0)
                        {
                            CumKVarh_PrevInterval = float.Parse(float.Parse(List_Records_LoadProfile[PrevKvarhIndex].value.ToString()).ToString(LP_DP_s));
                        }
                        PrevKvarhIndex -= 4;
                        kvar_ch2 = Calculate_kVar_by_KVarh(counter); //kvarhFound = true; kvarhIndex = counter;
                        kw_ch1 = LocalCommon.notRoundingOff(List_Records_LoadProfile[--counter].value.ToString(), LP_DP);

                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_1 = kw_ch1;
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_2 = kvar_ch2;
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_3 = kwh_ch3;
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_4 = kvarh_ch4;
                    }
                }
            }
            #endregion
            else //If it is not web Format
            {
                if (LocalCommon.GetReportFormat(application_Controller.CurrentUser.CurrentAccessRights) == ReportFormat.WAPDA_DDS)
                {
                    //int indx = 0;
                    for (int counter = 0; counter < List_Records_LoadProfile.Count; counter++)
                    {
                        //indx = counter;
                        DataSet_LoadProfile.DataTable_LoadProfile.Rows.Add();
                        int RowIndex = DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1;
                        DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Date = List_Records_LoadProfile[counter].arrival_time.ToString("dd MMM, yyyy");
                        DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Time = List_Records_LoadProfile[counter].arrival_time.ToString("HH:mm");
                        DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Counter = List_Records_LoadProfile[counter].counter.ToString();
                        counter += 3;
                        DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_1 =
                            LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP);

                        DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_2 =
                            LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);

                        DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_3 =
                            LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);

                        DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_4 =
                            LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);
                    }
                }
                else
                {
                    for (int counter = 0; counter < List_Records_LoadProfile.Count; counter++)
                    {
                        DataSet_LoadProfile.DataTable_LoadProfile.Rows.Add();
                        DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Add();
                        //v4.8.22
                        //DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Date = List_Records_LoadProfile[counter].arrival_time.ToString("dd MMM, yyyy HH:mm");
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Date = List_Records_LoadProfile[counter].arrival_time.ToString("dd MMM, yyyy");
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Time = List_Records_LoadProfile[counter].arrival_time.ToString("HH:mm");
                        DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Date = List_Records_LoadProfile[counter].arrival_time;

                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_1 = LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP);
                        DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Channel_1 = Convert.ToDouble(LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP));

                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_2 = LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);
                        DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Channel_2 = Convert.ToDouble(LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP));

                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_3 = LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);
                        DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Channel_3 = Convert.ToDouble(LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP));

                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_4 = LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);
                        DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Channel_4 = Convert.ToDouble(LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP));
                    }
                }
            }
        }

        public void fill_DS_for_Report(bool IsPerDay)
        {
            DataSet_LoadProfile.DataTable_LoadProfile.Rows.Clear();
            //IsWapdaFormat = false; //for test
            #region WebFormat
            if (LocalCommon.GetReportFormat(application_Controller.CurrentUser.CurrentAccessRights) == ReportFormat.WEB_GALAXY)
            {
                CumKVarh_PrevInterval = 0;
                CumKVarh_CurrentInterval = 0;
                Consumed_kVarh = 0;
                kVar = 0;
                IntervalTime_Current = 0;
                IntervalTime_Prev = 0;

                string kw_ch1 = "", kvar_ch2 = "", kwh_ch3 = "", kvarh_ch4 = "";
                bool kvarhFound = false;

                //List_Records_LoadProfile.Sort((x, y) => y.counter.CompareTo(x.counter));
                int LoadProfileRecordCount = List_Records_LoadProfile.Count;
                PrevKvarhIndex = LoadProfileRecordCount - 4 - 1 - 2; //4=total 4 channels,  1=Count to index,   2=kvarh index is 2 from last to first

                for (int counter = LoadProfileRecordCount - 1; counter >= 0; counter--)
                {
                    //kw, kvarh, kwh, I
                    if (counter <= 3 && (uint)combo_FromEntry.SelectedIndex > 0)
                    {
                        string msg = "Yes Last Record Skipped";
                        break;
                    }
                    else
                    {
                        kvarhFound = false;
                        kvar_ch2 = LP_DP_s;

                        DataSet_LoadProfile.DataTable_LoadProfile.Rows.Add();
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Date = List_Records_LoadProfile[counter].arrival_time.ToString("dd MMM, yyyy HH:mm");

                        //kw_ch1 = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP);
                        //kvarh_ch4 = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP); //Saving kvarh value from Channel-2
                        //kvar_ch2 = Calculate_kVar_by_KVarh(counter); kvarhFound = true; kvarhIndex = counter;
                        //kwh_ch3 = SmartEyeControl_7.LocalCommon.Commons.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP); //Saving kwh value from Channel-3
                        //CumKVarh_PrevInterval = float.Parse(float.Parse(List_Records_LoadProfile[kvarhIndex].value.ToString()).ToString(LP_DP_s));
                        //counter++;

                        kwh_ch3 = LocalCommon.notRoundingOff(List_Records_LoadProfile[--counter].value.ToString(), LP_DP); //Saving kwh value from Channel-3
                        kvarh_ch4 = LocalCommon.notRoundingOff(List_Records_LoadProfile[--counter].value.ToString(), LP_DP); //Saving kvarh value from Channel-2
                        if (PrevKvarhIndex >= 0)
                        {
                            CumKVarh_PrevInterval = float.Parse(float.Parse(List_Records_LoadProfile[PrevKvarhIndex].value.ToString()).ToString(LP_DP_s));
                        }
                        PrevKvarhIndex -= 4;
                        kvar_ch2 = Calculate_kVar_by_KVarh(counter); //kvarhFound = true; kvarhIndex = counter;
                        kw_ch1 = LocalCommon.notRoundingOff(List_Records_LoadProfile[--counter].value.ToString(), LP_DP);

                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_1 = kw_ch1;
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_2 = kvar_ch2;
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_3 = kwh_ch3;
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_4 = kvarh_ch4;
                    }
                }
            }
            #endregion
            else //If it is not web Format
            {
                if (LocalCommon.GetReportFormat(application_Controller.CurrentUser.CurrentAccessRights) == ReportFormat.WAPDA_DDS)
                {
                    //string _date = DateTime.Now.ToString();//Temporary by sajid //cmbPerDayDates.SelectedItem.ToString();
                    string _date = cmbPerDayDates.SelectedItem.ToString();

                    if (IsPerDay)
                    {
                        //if (Arival_Time.ToString("dd/MM/yyyy") == _date)
                        //{
                        for (int counter = 0; counter < List_Records_LoadProfile.Count; counter++)
                        {
                            DateTime Arival_Time = List_Records_LoadProfile[counter].arrival_time;

                            if (Arival_Time.ToString("dd/MM/yyyy") == _date)
                            {

                                DataSet_LoadProfile.DataTable_LoadProfile.Rows.Add();

                                int RowIndex = DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1;
                                DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Date = Arival_Time.ToString("dd MMM, yyyy");
                                DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Time = Arival_Time.ToString("HH:mm");
                                DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Counter = List_Records_LoadProfile[counter].counter.ToString();

                                //counter += 3; //commented for Fusion

                                DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_1 =
                                    LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP);

                                DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_2 =
                                    LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);

                                DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_3 =
                                    LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);

                                DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_4 =
                                    LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);
                            }
                        }
                        //}
                    }
                    else
                    {
                        for (int counter = 0; counter < List_Records_LoadProfile.Count; counter++)
                        {
                            DateTime Arival_Time = List_Records_LoadProfile[counter].arrival_time;
                            DataSet_LoadProfile.DataTable_LoadProfile.Rows.Add();
                            int RowIndex = DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1;
                            DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Date = Arival_Time.ToString("dd MMM, yyyy");
                            DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Time = Arival_Time.ToString("HH:mm");
                            DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Counter = List_Records_LoadProfile[counter].counter.ToString();

                            //counter += 3;

                            DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_1 =
                                LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP);

                            DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_2 =
                                LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);

                            DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_3 =
                                LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);

                            DataSet_LoadProfile.DataTable_LoadProfile[RowIndex].Channel_4 =
                                LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);
                        }
                    }
                }
                else
                {
                    for (int counter = 0; counter < List_Records_LoadProfile.Count; counter++)
                    {
                        DataSet_LoadProfile.DataTable_LoadProfile.Rows.Add();
                        DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Add();
                        //v4.8.22
                        //DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Date = List_Records_LoadProfile[counter].arrival_time.ToString("dd MMM, yyyy HH:mm");
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Date = List_Records_LoadProfile[counter].arrival_time.ToString("dd MMM, yyyy");
                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Time = List_Records_LoadProfile[counter].arrival_time.ToString("HH:mm");
                        DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Date = List_Records_LoadProfile[counter].arrival_time;

                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_1 = LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP);
                        DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Channel_1 = Convert.ToDouble(LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP));

                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_2 = LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);
                        DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Channel_2 = Convert.ToDouble(LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP));

                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_3 = LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);
                        DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Channel_3 = Convert.ToDouble(LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP));

                        DataSet_LoadProfile.DataTable_LoadProfile[DataSet_LoadProfile.DataTable_LoadProfile.Rows.Count - 1].Channel_4 = LocalCommon.notRoundingOff(List_Records_LoadProfile[++counter].value.ToString(), LP_DP);
                        try
                        {
                            DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Channel_4 = Convert.ToDouble(LocalCommon.notRoundingOff(List_Records_LoadProfile[counter].value.ToString(), LP_DP));
                        }
                        catch (Exception)
                        {
                            DataSet_LoadProfile.DataTable_LoadProfile2[DataSet_LoadProfile.DataTable_LoadProfile2.Rows.Count - 1].Channel_4 = 0;
                        }
                    }
                }
            }
        }

        private string Calculate_kVar_by_KVarh(int _kvarh_index)
        {
            IntervalTime_Current = List_Records_LoadProfile[_kvarh_index].interval * 60; //To make seconds, multiplied by 60
            CumKVarh_CurrentInterval = float.Parse(float.Parse(List_Records_LoadProfile[_kvarh_index].value.ToString()).ToString(LP_DP_s));
            Consumed_kVarh = float.Parse((CumKVarh_CurrentInterval - CumKVarh_PrevInterval).ToString(LP_DP_s));
            return LocalCommon.notRoundingOff(((Consumed_kVarh / IntervalTime_Current) * 3600).ToString(), LP_DP);
            //return (((Consumed_kVarh / IntervalTime_Current) * 3600).ToString(LP_DP_s));
        }

        private string[] GetLoadProfileChannelsList(LoadProfileScheme lpScheme)
        {
            //TODO: Handle channel list get for RFP135 (+3)
            //string[] qty = new string[4];
            //for (int i = 0; i < qty.Length; i++)
            //{
            //    qty[i] = LP_Data.loadData.ChannelsInfo[i+2].Quantity_Name.Replace("_", " "); // + 3].Quantity_Name;
            //}

            string[] qty = null;
            if (lpScheme == LoadProfileScheme.Load_Profile)
            {
                qty = new string[LP_Data.loadData.ChannelsInfo.Count];

                int i = 0, j = 0;
                if (qty.Length == 4)
                    j = 3;

                for (; i < qty.Length; i++)
                {
                    qty[i] = LP_Data.loadData.ChannelsInfo[j++].Quantity_Name.Replace("_", " "); // + 3].Quantity_Name;
                }
            }
            else if (lpScheme == LoadProfileScheme.Load_Profile_Channel_2)
            {
                qty = new string[LP_Data.loadData.ChannelsInfo.Count];
                for (int i = 0; i < qty.Length; i++)
                {
                    qty[i] = LP_Data.loadData.ChannelsInfo[i + 2].Quantity_Name.Replace("_", " "); // + 3].Quantity_Name;
                }
            }

            return qty;
        }

        private void btn_Rpt_LoadProfile_Click(object sender, EventArgs e)
        {
            fill_DS_for_Report(false);
            //string[] qty = new string[4];
            string MSN = application_Controller.ConnectionManager.ConnectionInfo.MSN;
            string meter_Model = loadProfileController.CurrentConnectionInfo.MeterInfo.MeterModel;

            //for (int i = 0; i < 4; i++)
            //{
            //    qty[i] = LP_Data.loadData.ChannelsInfo[i+3].Quantity_Name;
            //}
            MeterConfig meter_type_info = application_Controller.ConnectionManager.ConnectionInfo.MeterInfo;
            viewer_LoadProfile = new ReportViewer(DataSet_LoadProfile, MSN, meter_Model, GetLoadProfileChannelsList(LP_Scheme), "", obj_CustomerCode.Customer_Code_String,
                Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(), meter_type_info,
                application_Controller.CurrentUser.CurrentAccessRights, false); //,
                                                                                //((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));
            viewer_LoadProfile.Show();
        }

        private void cmbLoadProfileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbLoadProfile.SelectedIndex = rbDateWiseLP.Checked ? 0 : 1;
            chkLPChannels.Items.Clear();
            this.lblLPReadEntries.Text = "0";
            this.IsDateTimeWise = rbDateWiseLP.Checked ? true : false;

            TabSelection();

            btn_Rpt_LoadProfile.Visible = (!IsHideReportButton);

            if (cmbLoadProfileType.SelectedIndex == 0)
            {
                LP_Scheme = LoadProfileScheme.Load_Profile;
                LP_Data = loadData1;


                //gpLPChannelFilter.Visible = false;

                // if (tb_LPFilterView.TabPages.Contains(tbChannelCount)) tb_LPFilterView.TabPages.Remove(tbChannelCount);
                // if (tb_LPFilterView.TabPages.Contains(tbGroupSelection)) tb_LPFilterView.TabPages.Remove(tbGroupSelection);
                chk_channel_filter.Checked = false;
                chk_channel_filter.Visible = false;

            }

            else if (cmbLoadProfileType.SelectedIndex == 1)
            {
                LP_Scheme = LoadProfileScheme.Load_Profile_Channel_2;
                LP_Data = loadData2;

                chk_channel_filter.Checked = false;
                chk_channel_filter.Visible = false;
            }
            else if (cmbLoadProfileType.SelectedIndex == 2)
            {
                LP_Scheme = LoadProfileScheme.Daily_Load_Profile;
                LP_Data = PQ_LoadProfile;

                btn_Rpt_LoadProfile.Visible = false;
                chk_channel_filter.Checked = false;
                chk_channel_filter.Visible = false;
            }
            loadProfileController.LPInfo_Data = LP_Data;
            RefreshEntriesInUse();

            PopulateChannels(LP_Data.loadData.ChannelsInfo);

            if (Application_Controller != null &&
                Application_Controller.IsIOBusy)
            {
                ClearLoadProfileGrid();
                Notification notifier = new Notification("Continued Read Operation",
                                                         "Unable to update Load Profile Data,data operation in continue");
                return;
            }
            else
                displayLoadProfile();
        }

        private void TabSelection()
        {
            if (this.IsDateTimeWise)
            {
                if (this.tbLoadProfile.TabPages.Contains(tpRange) && this.tbLoadProfile.SelectedTab != tpRange)
                    this.tbLoadProfile.SelectedTab = tpRange;
            }
            else
            {
                if (this.tbLoadProfile.TabPages.Contains(tpEntry) && this.tbLoadProfile.SelectedTab != tpEntry)
                    this.tbLoadProfile.SelectedTab = tpEntry;
            }
        }

        private void RefreshEntriesInUse()
        {
            uint fromEntry = 1;
            uint toEntry = 1;

            if (LP_Data.LoadProfileCounter > LP_Data.MaxEntries)
            {
                fromEntry = (LP_Data.LoadProfileCounter + 1) - LP_Data.MaxEntries;
            }
            toEntry = LP_Data.LoadProfileCounter;

            var item_Enumerable = LocalCommon.Series(fromEntry, toEntry);
            uint Item_Count = toEntry - fromEntry;
            // var item_Enumerable =  Enumerable.Range((int)fromEntry, (int)toEntry);

            // Update combo_FromEntry
            if (combo_FromEntry.Items.Count == 0 ||
                combo_FromEntry.Items.Count != Item_Count ||
                (combo_FromEntry.Items.Count > 0 && Convert.ToUInt32(combo_FromEntry.Items[0]) != fromEntry))
            {
                combo_ToEntry.Items.Clear();
                combo_FromEntry.Items.Clear();
                foreach (object lstItem in item_Enumerable)
                {
                    combo_FromEntry.Items.Add(lstItem);
                    combo_ToEntry.Items.Add(lstItem);
                }
            }

            if (combo_FromEntry.Items.Count > 0)
            {
                combo_FromEntry.SelectedIndex = combo_FromEntry.Items.Count - 1;
                combo_ToEntry.SelectedIndex = combo_ToEntry.Items.Count - 1;
            }

        }

        private void PopulateChannels(List<LoadProfileChannelInfo> ChannelsInfo)
        {
            if (ChannelsInfo == null) return;
            chkLPChannels.Items.Clear();
            chkLPChannels.Items.AddRange(ChannelsInfo.ToArray());
        }
        private void rbScheme1_CheckedChanged(object sender, EventArgs e)
        {
            tbLoadProfile.SelectedIndex = rbDateWiseLP.Checked ? 0 : 1;
            this.IsDateTimeWise = rbDateWiseLP.Checked ? true : false;
            chkLPChannels.Items.Clear();
            if (cmbLoadProfileType.SelectedIndex == 0)
            {
                LP_Scheme = LoadProfileScheme.Load_Profile;
                LP_Data = loadData1;
                if (!IsHideReportButton)
                    btn_Rpt_LoadProfile.Visible = true;
                //gpLPChannelFilter.Visible = false;

                // if (tb_LPFilterView.TabPages.Contains(tbChannelCount)) tb_LPFilterView.TabPages.Remove(tbChannelCount);
                // if (tb_LPFilterView.TabPages.Contains(tbGroupSelection)) tb_LPFilterView.TabPages.Remove(tbGroupSelection);
                chk_channel_filter.Checked = false;
                chk_channel_filter.Visible = false;

                if (this.IsDateTimeWise)
                {
                    if (this.tbLoadProfile.TabPages.Contains(tpRange))
                        this.tbLoadProfile.SelectedTab = tpRange;
                }
                else
                {
                    if (this.tbLoadProfile.TabPages.Contains(tpEntry))
                        this.tbLoadProfile.SelectedTab = tpEntry;
                }
            }

            else if (cmbLoadProfileType.SelectedIndex == 1)
            {
                LP_Scheme = LoadProfileScheme.Load_Profile_Channel_2;
                LP_Data = PQ_LoadProfile;

                btn_Rpt_LoadProfile.Visible = false;
                chk_channel_filter.Checked = false;
                chk_channel_filter.Visible = false;
                if (this.IsDateTimeWise)
                {
                    if (this.tbLoadProfile.TabPages.Contains(tpRange))
                        this.tbLoadProfile.SelectedTab = tpRange;
                }
                else
                {
                    if (this.tbLoadProfile.TabPages.Contains(tpEntry))
                        this.tbLoadProfile.SelectedTab = tpEntry;
                }

            }
            else if (cmbLoadProfileType.SelectedIndex == 2)
            {
                LP_Scheme = LoadProfileScheme.Daily_Load_Profile;
                LP_Data = loadData2;

                btn_Rpt_LoadProfile.Visible = false;
                chk_channel_filter.Checked = false;
                chk_channel_filter.Visible = false;

                if (this.IsDateTimeWise)
                {
                    if (this.tbLoadProfile.TabPages.Contains(tpRange))
                        this.tbLoadProfile.SelectedTab = tpRange;
                }
                else
                {
                    if (this.tbLoadProfile.TabPages.Contains(tpEntry))
                        this.tbLoadProfile.SelectedTab = tpEntry;
                }
            }
            loadProfileController.LPInfo_Data = LP_Data;
            RefreshEntriesInUse();

            PopulateChannels(LP_Data.loadData.ChannelsInfo);

            if (Application_Controller != null &&
                Application_Controller.IsIOBusy)
            {
                ClearLoadProfileGrid();
                Notification notifier = new Notification("Continued Read Operation",
                                                         "Unable to update Load Profile Data,data operation in continue");
                return;
            }
            else
                displayLoadProfile();
        }

        private void InitObjectsStatusWord()
        {
            try
            {
                StatusWord statusWord = new StatusWord(0);

                if (loadProfileController.Configurations != null &&
                   loadProfileController.Configurations.Status_Word != null)
                {
                    List_AvailableStatausWord = new List<StatusWord>();

                    foreach (DatabaseConfiguration.DataSet.Configs.Status_WordRow
                             row in loadProfileController.Configurations.Status_Word)
                    {
                        statusWord = new StatusWord(Convert.ToByte(row.Code));

                        if (!row.IsNameNull())
                            statusWord.Name = row.Name;

                        // if (!row.IsDisplay_CodeNull())
                        //     statusWord.Display_Code = Convert.ToUInt32(row.Display_Code);

                        // if (!row.IsP_LevalNull())
                        //     statusWord.Priority_Level = row.P_Leval;

                        List_AvailableStatausWord.Add(statusWord);
                    }
                }


                // Clear Previous Word Item 1
                if (statusWordItems == null)
                    statusWordItems = new List<StatusWord>();
                else
                    statusWordItems.Clear();

            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Param Status Word Map", ex.Message);
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(ApplicationRight Rights)
        {
            try
            {
                this._Rights = Rights;
                this.SuspendLayout();
                if (Rights.LoadProfileRights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights.LoadProfileRights)
                    {
                        _HelperAccessRights((LoadProfileDataRights)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }

                    if (this.cmbLoadProfileType.Items.Count > 0)
                    {
                        this.btn_GetLoadProfile.Enabled = true;
                        this.cmbLoadProfileType.SelectedIndex = 0;
                        if (this.cmbLoadProfileType.Items.Count > 1)
                            this.cmbLoadProfileType.Enabled = true;
                    }

                    if (tbLoadProfile.TabPages.Count > 0)
                        tbLoadProfile.SelectedIndex = 0;
                    return true;
                }
                return false;
            }
            finally
            {
                IsHideReportButton = false;
                if (Rights.GeneralRights.Find(x => x.QuantityName == GeneralRights.IgnoreReports.ToString() && x.Read == true) != null)
                {
                    IsHideReportButton = true;
                    this.btn_Rpt_LoadProfile.Visible = false;
                    this.btnGenerateChart.Visible = false;
                    this.btnGeneratePerDayReport.Visible = false;
                }
                else
                {
                    this.btn_Rpt_LoadProfile.Visible = true;
                    this.btnGenerateChart.Visible = true;
                    this.btnGeneratePerDayReport.Visible = true;
                }
                this.ResumeLayout();
            }
        }

        private void _HelperAccessRights(LoadProfileDataRights qty, bool read, bool write)
        {
            switch (qty)
            {
                case LoadProfileDataRights.ReadLoadProfileData_Scheme1:
                    if (read)
                        this.cmbLoadProfileType.Items.Add("Load Profile 1");
                    break;
                case LoadProfileDataRights.ReadLoadProfileData_Scheme2:
                    if (read)
                        this.cmbLoadProfileType.Items.Add("Load Profile 2");
                    break;
                case LoadProfileDataRights.ReadLoadProfileData_PQ:
                    if (read)
                        this.cmbLoadProfileType.Items.Add("Instant Load Profile");
                    break;
                case LoadProfileDataRights.ReadLoadProfileData_DateTimeWise:
                    if (read)
                    {
                        rbDateWiseLP.Enabled = true;
                        tbLoadProfile.Visible = true;
                        if (!tbLoadProfile.TabPages.Contains(tpRange))
                            tbLoadProfile.TabPages.Add(tpRange);
                    }
                    break;

                case LoadProfileDataRights.ReadLoadProfileData_EntryWise:
                    if (read)
                    {
                        tbLoadProfile.Visible = true;
                        if (!tbLoadProfile.TabPages.Contains(tpEntry))
                            tbLoadProfile.TabPages.Add(tpEntry);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
