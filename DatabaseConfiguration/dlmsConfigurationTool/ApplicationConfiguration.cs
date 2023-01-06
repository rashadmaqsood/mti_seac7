using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Comm;
using System.Configuration;
using DatabaseConfiguration.DataSet;
using DLMS;
using DatabaseConfiguration.DataBase;
using DLMS.Comm;
using System.Drawing;
using System.Drawing.Drawing2D;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.Data.SqlClient;
using DatabaseConfiguration.CommonModels;
using System.Data.OleDb;
using System.Threading;
using DatabaseConfiguration;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace dlmsConfigurationTool
{
    public partial class ApplicationConfiguration : UserControl
    {
        #region Insert/Update All Quantities

        #region Data_Members

        private string label;
        private string Id;
        private string Get_IndexName;
        private string OBISStr;

        private Regex OBISDecimalValidator = null;

        public List<String> OBIS_LabelNames = null;

        #endregion

        #region Properties

        public string Label
        {
            get { return label; }
            set
            {
                // Validation
                label = value;
            }
        }

        public string OBISId
        {
            get { return Id; }
            set
            {
                // Validation
                Id = value;
            }
        }

        public string Get_Index_Name
        {
            get { return Get_IndexName; }
            set
            {
                // Validation
                Get_IndexName = value;
            }
        }

        public string OBISCodeStr
        {
            get { return OBISStr; }
            set
            {
                // Validation
                OBISStr = value;
            }
        }

        public OBISCodeMode ObisCodeMode
        {
            get;
            //{
            //    //if (rdbOBISCode.Checked)
            //    //    return OBISCodeMode.ObisCode;
            //    //else
            //    //    return OBISCodeMode.ObisIndex;
            //}
            set;
            //{
            //    //if (value == OBISCodeMode.ObisCode)
            //    //    rdbOBISCode.Checked = true;
            //    //else if (value == OBISCodeMode.ObisIndex)
            //    //    rdbIndex.Checked = true;
            //}
        }

        #endregion 

        public void Update_GUI(string LabelSTR = "", string OBISCodeSTR = "")
        {
            if (!string.IsNullOrEmpty(OBISCodeSTR))
            {
                if (ObisCodeMode == OBISCodeMode.ObisCode)
                {
                    OBISCodeSTR = this.OBISCodeStr;
                }
                else if (ObisCodeMode == OBISCodeMode.ObisIndex)
                {
                    OBISCodeSTR = this.OBISId;
                }
                else if (ObisCodeMode == OBISCodeMode.ObisLabel)
                {
                    OBISCodeSTR = this.Get_Index_Name;
                }
            }

            // Empty OBIS_STR Value
            if (string.IsNullOrEmpty(OBISCodeSTR))
            {
                MessageBox.Show("Invalid OBIS Code STR", "Invalid OBIS Code STR Format");
                return;
            }
        }

        private void tb_ObisIndex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //tb_ObisIndexAllQuan.TextChanged -= tb_ObisIndex_TextChanged;
                string RawOBISstr = tb_ObisIndexAllQuan.Text;
                //tb_ObisIndexAllQuan.BackColor = Color.Red;

                // Empty OBIS_STR Value
                if (string.IsNullOrEmpty(RawOBISstr))
                {
                    tb_ObisIndexAllQuan.BackColor = Color.Red;
                    return;
                }

                if (ObisCodeMode == OBISCodeMode.ObisCode)
                {
                    this.OBISCodeStr = string.Empty;

                    string OBISString        = RawOBISstr;
                    ulong indexValue         = Convert.ToUInt64(RawOBISstr);
                    StOBISCode OBISCode      = (Get_Index)indexValue;
                    tb_ObisIndexAllQuan.Text = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);

                    if (OBISDecimalValidator.IsMatch(tb_ObisIndexAllQuan.Text))
                    {
                        this.OBISCodeStr              = OBISString;
                        tb_ObisIndexAllQuan.BackColor = Color.White;
                    }
                }
                else if (ObisCodeMode == OBISCodeMode.ObisIndex)
                {
                    //this.OBISId = string.Empty;
                    string OBISString    = RawOBISstr;
                    ulong OBISIndexValue = 0;

                    if (ulong.TryParse(this.OBISCodeStr, out OBISIndexValue))
                    {
                        this.OBISId             = OBISString;

                        StOBISCode ObisValidate = Get_Index.Dummy;
                        ObisValidate.OBIS_Value = OBISIndexValue;
                        this.tb_ObisIndexAllQuan.Text = OBISIndexValue.ToString();
                        if (ObisValidate.ClassId > 0 && (ObisValidate.OBIS_Value & 0x0000FFFFFFFFFFFF) > 0)
                            tb_ObisIndexAllQuan.BackColor = Color.White;
                    }
                }
                else if (ObisCodeMode == OBISCodeMode.ObisLabel)
                {
                    // Validation
                    this.Get_Index_Name = string.Empty;

                    if (OBIS_LabelNames != null && OBIS_LabelNames.Count > 0)
                    {
                        if (OBIS_LabelNames.Contains(RawOBISstr))
                        {
                            this.Get_Index_Name = RawOBISstr;
                            tb_ObisIndexAllQuan.BackColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.tb_ObisIndexAllQuan.Text = "";
            }
            finally
            {
                //tb_ObisIndexAllQuan.TextChanged += tb_ObisIndex_TextChanged;
            }
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Validate OBIS_Id & then Assign To Variable
                String OBISLabel = this.tb_Label.Text;
                if (!String.IsNullOrEmpty(OBISLabel))
                {
                    this.Label = OBISLabel;
                    this.tb_Label.BackColor = Color.White;
                }
                else
                {
                    OBISLabel = "";
                }
            }
            catch (Exception ex)
            {
                this.tb_Label.BackColor = Color.Red;
                Label = "";
            }
        }

        public Configs.AllQuantitiesRow AllQuantitiesRow_Dummy
        {
            get
            {
                Configs.AllQuantitiesRow DummyRow = (Configs.AllQuantitiesRow)configs.AllQuantities.NewRow();

                DummyRow.OBIS_Index    = 0;
                DummyRow.Label         = "Dummy";
                DummyRow.Dp_Name       = "";
                DummyRow.Unit          = "";
                DummyRow.Priority      = 0;
                DummyRow.Quantity_Code = "0:0.0.0.0.0";
                DummyRow.Quantity_Name = "Dummy";

                return DummyRow;
            }
        }

        private void rdbOBISCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                Configs.AllQuantitiesRow CurrentRow = (SelectedAllQuantityRows != null && SelectedAllQuantityRows.Count > 0) ? SelectedAllQuantityRows[0] : this.AllQuantitiesRow_Dummy;
                
                if (this.rdbOBISCode.Checked)
                {
                    StOBISCode OBISCode = (Get_Index)CurrentRow.OBIS_Index;
                    this.ObisCodeMode = OBISCodeMode.ObisCode;
                    this.lbl_obis_code.Text = this.rdbOBISCode.Text;
                    this.tb_ObisIndexAllQuan.Text = (this.cb_Insert.Checked) ? "0:0.0.0.0.0.0" : OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                }
                else if (this.rdbIndex.Checked)
                {
                    this.ObisCodeMode = OBISCodeMode.ObisIndex;
                    this.lbl_obis_code.Text = this.rdbIndex.Text;
                    this.tb_ObisIndexAllQuan.Text = (this.cb_Insert.Checked) ? "0" : CurrentRow.OBIS_Index.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public enum OBISCodeMode : byte
        {
            ObisCode  = 1,
            ObisIndex = 2,
            ObisLabel = 3
        }

        #endregion  //Insert/Update All Quantities

        public static int MAX_RECONNECT_TRY = 03;
        private string _dsn = string.Empty;

        List<CaptureObjectInfo> _allBillingItemsList;
        List<CaptureObjectInfo> _allLoadProfileItemsList;

        List<Configs.AllQuantitiesRow> _listBillingItemsAllQuantities;
        List<Configs.AllQuantitiesRow> _listLoadProfileAllQuantities;

        List<CatagoryInfo> _catagoryList;

        ConfigDBController _dbController;
        IDBAccessLayer _dbAccessLayer;
        //DbConnection _connection;
        DataBaseTypes active_database = DataBaseTypes.MDC_DATABASE;
        string _connectionString = string.Empty;

        ConfigUpdater configUpdater;
        Dictionary<string, Button> btnTabs_Dictionary;
        Dictionary<string, BindingSource> bSrc_Dictionary;
        Dictionary<string, DataTable> configsTables_Dictionary;

        public ApplicationConfiguration(ModelConfigurator frm)
        {
            InitializeComponent();
            menuStrip.Renderer = new ToolStripProfessionalRenderer(new MyColorTable());
            MainForm = frm;
            this._catagoryList = new List<CatagoryInfo>(){new CatagoryInfo{CatagoryName = "Load Profile",OBIS_Code = 1971426009612543},
                                                          new CatagoryInfo{CatagoryName = "Billing Items",OBIS_Code = 1970326489858303}};

            OBISDecimalValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);

            // OBIS_Labels
            if (OBIS_LabelNames == null || OBIS_LabelNames.Count <= 0)
            {
                OBIS_LabelNames = new List<string>();

                var labels = Enum.GetNames(typeof(Get_Index));
                OBIS_LabelNames.AddRange(labels);
            }


            /*
            try
            {
                int active_database = Properties.Settings.Default.Active_ConnectionString;
                if (active_database == (int)DataBaseTypes.MDC_DATABASE)
                {
                    //this._dbAccessLayer = new MDC_DBAccessLayer(Properties.Settings.Default.MDC_ConnectionString);
                    this._connection = new MySqlConnection
                    {
                        ConnectionString = Properties.Settings.Default.MDC_ConnectionString
                    };
                }
                else if (active_database == (int)DataBaseTypes.SCT_DATABASE)
                {
                    //this._dbAccessLayer = new SCT_DBAccessLayer(Properties.Settings.Default.SCT_ConnectionString);
                    this._connection = new SqlConnection
                    {
                        ConnectionString = Properties.Settings.Default.SCT_ConnectionString
                    };
                }
                else if (active_database == (int)DataBaseTypes.SEAC_DATABASE)
                {
                    //this._dbAccessLayer = new SEAC_DBAccessLayer(Properties.Settings.Default.SEAC_ConnectionString);
                    this._connection = new SQLiteConnection
                    {
                        ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = Properties.Settings.Default.SEAC_ConnectionString, ForeignKeys = true }.ConnectionString
                    };
                }
                else
                {
                    this._dbAccessLayer = null;
                    this._connection = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
            finally
            {
                if (this._connection != null)
                    _configDB = new ConfigDBController(this._connection);
            }
            */
        }

        private void Close_DbConnection()
        {
            try
            {
                //this.Enabled = false;
                this.Log_Message("Disconnecting . . .", 0, 1);

                if (this._dbController == null) return;

                this._dbController.CloseConnection();
                this.btn_TestConnection.Text = "Connect";
                this.Log_Message("Disconnected successfully . . .", 1, 1);
            }
            catch (Exception ex)
            {
                string exception = $"Can't be disconnected. {Environment.NewLine}{ex.GetBaseException().Message}";
                this.Log_Message(exception, 0, 1);
                MessageBox.Show(exception);
            }
            finally
            {
                //this.Enabled = true;
            }
        }

        private void Open_DbConnection()
        {
            try
            {
                //this.Enabled = false;
                this.Log_Message("Connecting to the database . . .", 0, 1);

                if (!String.IsNullOrEmpty(this._connectionString))
                {
                    this._dbController = new ConfigDBController(this._connectionString, this.active_database);
                }

                this._dbController.OpenConnection();
                this.configs.Clear();
                this.cb_Insert.Checked = true;

                this.btn_TestConnection.Text = "Disconnect";
                this.Log_Message("Connected successfully . . .", 1, 1);
            }
            catch (Exception ex)
            {
                string exception = $"Connection Failed. {Environment.NewLine}{ex.GetBaseException().Message}";
                this.Log_Message(exception, 0, 1);
                MessageBox.Show(exception);
            }
            finally
            {
                //this.Enabled = true;
            }
        }

        private void Load_connectionString()
        {
            try
            {
                this.Enabled = false;
                this.Log_Message("Loading connectionString from app.config file . . .", 0, 1);

                active_database = (DataBaseTypes)Properties.Settings.Default.Active_ConnectionString;
                if (active_database == DataBaseTypes.MDC_DATABASE)
                {
                    this._connectionString = Properties.Settings.Default.MDC_ConnectionString;
                }
                else if (active_database == DataBaseTypes.SCT_DATABASE)
                {
                    this._connectionString = Properties.Settings.Default.SCT_ConnectionString;
                }
                else if (active_database == DataBaseTypes.SEAC_DATABASE)
                {
                    this._connectionString = Properties.Settings.Default.SEAC_ConnectionString;
                }
                else
                {
                    this._connectionString = string.Empty;
                }
                this.Log_Message("Loaded successfully . . .", 1, 1);
            }
            catch (Exception ex)
            {
                string exception = $"Loading connectionString from app.config file Faild. {Environment.NewLine}{ex.GetBaseException().ToString()}";
                this.Log_Message(exception, 0, 1);
                throw new Exception(exception);

            }
            finally
            {
                this.Enabled = true;
            }
        }

        /*
        private void Open_DbConnection1()
        {
            try
            {
                this.Enabled = false;
                this.Log_Message("Connecting to the database . . .", 0, 1);

                int active_database = Properties.Settings.Default.Active_ConnectionString;
                if (active_database == (int)DataBaseTypes.MDC_DATABASE)
                {
                    //this._dbAccessLayer = new MDC_DBAccessLayer(Properties.Settings.Default.MDC_ConnectionString);
                    this._connection = new MySqlConnection
                    {
                        ConnectionString = Properties.Settings.Default.MDC_ConnectionString
                    };
                }
                else if (active_database == (int)DataBaseTypes.SCT_DATABASE)
                {
                    //this._dbAccessLayer = new SCT_DBAccessLayer(Properties.Settings.Default.SCT_ConnectionString);
                    this._connection = new SqlConnection
                    {
                        ConnectionString = Properties.Settings.Default.SCT_ConnectionString
                    };
                }
                else if (active_database == (int)DataBaseTypes.SEAC_DATABASE)
                {
                    //this._dbAccessLayer = new SEAC_DBAccessLayer(Properties.Settings.Default.SEAC_ConnectionString);
                    this._connection = new SQLiteConnection
                    {
                        ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = Properties.Settings.Default.SEAC_ConnectionString, ForeignKeys = true }.ConnectionString
                    };
                }
                else
                {
                    this._dbAccessLayer = null;
                    this._connection = null;
                }

                this._connection.Open();
                this.configs.Clear();
                this.cb_Insert.Checked = true;

                if (this._connection != null)
                {
                    _dbController = new ConfigDBController(this._connection);
                }

                this.btn_TestConnection.Text = "Disconnect";
                this.Log_Message("Connected successfully . . .", 1, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed \r\n \r\n Detail: \r\n" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }
        */

        public ModelConfigurator MainForm { get; set; }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.btn_TestConnection.Text != "Disconnect")
                {
                    MessageBox.Show("Please make connection with your database.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btn_TestConnection.Focus();
                    this.toolTip.Show("Please click here to connect.", this.btn_TestConnection);
                    return;
                }

                #region Old Logic
                // FileInfo configFile = new FileInfo(this.DefaultURLPath);

                // if (configFile.Exists)
                // {
                //     DialogResult res = MessageBox.Show(this, "Are you sure want to over-write existing config file",
                //                                        "Save File", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                //     if (res != DialogResult.OK)
                //         return;
                //     configFile.Delete();
                // }
                // using (FileStream xmlConfig = new FileStream(this.DefaultURLPath, FileMode.Create))
                // {
                //     configs.WriteXml(xmlConfig, XmlWriteMode.IgnoreSchema);
                // }

                // for (int i = 0; i < dgv_ObisDetails.Rows.Count; i++)
                // {
                //     Configs.OBIS_LabelsRow = dgv_ObisDetails.Rows[i].Cells[2];
                // }

                //this._dbAccessLayer.Update_All_Configuration(configs);

                //this._configDB.Update_All_Configuration(configs); 
                #endregion
                this.Enabled = false;
                //_dbController.Update_All_Configuration(configs);
                this.SaveConfigsToDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Load_DBtoConfigs();
                this.Enabled = true;
            }
        }

        private void SaveConfigsToDB()
        {
            try
            {
                int totalSteps = 2 + _dbController.Update_All_Configs.Count;
                string[] tableName = new string[_dbController.Update_All_Configs.Keys.Count];
                _dbController.Update_All_Configs.Keys.CopyTo(tableName, 0);

                this.Log_Message("Getting changes in configs . . . ", 0, totalSteps);
                Configs dSet = new Configs();
                configs.EnforceConstraints = false;
                dSet = (Configs)configs.GetChanges();

                for (int i = 0; i < _dbController.Update_All_Configs.Count; i++)
                {
                    this.Log_Message("Updating configs." + tableName[i] + " . . . ", i + 1, totalSteps);
                    _dbController.Update_All_Configs[tableName[i]](dSet);
                }

                this.Log_Message("Saving changes into db . . . ", totalSteps - 1, totalSteps);
                _dbController._entityModel.SaveChanges();
                this.Log_Message("Saved changes into db successfully.", totalSteps, totalSteps);

                MessageBox.Show("Configuration Saved Successfully.");
            }
            catch (Exception ex)
            {
                string msg = $"Saving Failed:{Environment.NewLine}{ex.GetBaseException().Message}";
                this.Log_Message(msg, 0, 1);
                throw new Exception(msg);
            }
        }

        #region Load/Save Configurations

        private void newAllQuantitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // NewEditOBISCodesQuantities();
            RefreshLabelNewEditOBISCodesQuantities();
        }

        private void RefreshLabelAllQuantitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshLabelNewEditOBISCodesQuantities();
        }

        private void NewAddOBISCodesQuantities()
        {
            try
            {
                List<StOBISCode> OBISCodes = new List<StOBISCode>();
                Get_Index[] tArray = (Get_Index[])Enum.GetValues(typeof(Get_Index));
                foreach (var item in tArray)
                {
                    StOBISCode ObisCode = item;
                    OBISCodes.Add(ObisCode);
                }

                foreach (Configs.AllQuantitiesRow preVOBIS in configs.AllQuantities)
                {
                    try
                    {
                        preVOBIS.Delete();
                        configs.AllQuantities.RemoveAllQuantitiesRow(preVOBIS);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                for (int index = 0; index < OBISCodes.Count; index++)
                {
                    try
                    {
                        Configs.AllQuantitiesRow NEw = (Configs.AllQuantitiesRow)configs.AllQuantities.NewRow();
                        NEw.OBIS_Index = OBISCodes[index].OBIS_Value;
                        NEw.Label = OBISCodes[index].OBISIndex.ToString();

                        //________________________________
                        NEw.Label = NEw.Label.ToUpper();
                        //________________________________
                        configs.AllQuantities.AddAllQuantitiesRow(NEw);
                    }
                    catch
                    {
                    }
                }

                dgv_all_quantities.Refresh();
                if (OBISCodes.Count != configs.AllQuantities.Rows.Count)
                    throw new Exception("Unable to add display windows");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }


        private void RefreshLabelNewEditOBISCodesQuantities()
        {
            Configs.AllQuantitiesRow Prev_Row = null;
            Configs.AllQuantitiesRow NEw = null;
            StOBISCode ObisCode = Get_Index.Dummy;

            try
            {
                List<StOBISCode> OBISCodes = new List<StOBISCode>();
                Get_Index[] tArray = (Get_Index[])Enum.GetValues(typeof(Get_Index));
                foreach (var item in tArray)
                {
                    ObisCode = item;
                    OBISCodes.Add(ObisCode);
                }


                for (int index = 0; index < OBISCodes.Count; index++)
                {
                    ObisCode = Get_Index.Dummy;
                    try
                    {
                        ObisCode = OBISCodes[index];
                        Prev_Row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == ObisCode.OBIS_Value);

                        if (Prev_Row == null)
                        {
                            NEw = (Configs.AllQuantitiesRow)configs.AllQuantities.NewRow();
                            StOBISCode OBIS = OBISCodes[index];
                            NEw.OBIS_Index = OBISCodes[index].OBIS_Value;
                            NEw.Label = OBISCodes[index].OBISIndex.ToString();

                            // New OBIS Quantity
                            //________________________________
                            configs.AllQuantities.AddAllQuantitiesRow(NEw);
                        }
                        else if (Prev_Row != null &&
                            (string.IsNullOrEmpty(Prev_Row.Label) ||
                             string.Equals(Prev_Row.Label, "auto added", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(Prev_Row.Label, "unknown", StringComparison.OrdinalIgnoreCase)))
                        {
                            Prev_Row.Label = OBISCodes[index].OBISIndex.ToString();
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch
                    {
                    }
                }

                dgv_all_quantities.Refresh();
                if (OBISCodes.Count < configs.AllQuantities.Rows.Count)
                    throw new Exception("Unable to add few OBIS Quantities");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.btn_TestConnection.Text != "Disconnect")
            {
                MessageBox.Show("Please make connection with your database.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btn_TestConnection.Focus();
                this.toolTip.Show("Please click here to connect.", this.btn_TestConnection);
                return;
            }

            this.LoadDbAndShow();
        }

        private void LoadDbAndShow()
        {
            try
            {
                this.Enabled = false;
                this.rbInsertUpdate.Checked = true;

                this.Load_DBtoConfigs();
                this.LoadOthers();
                this.btn_TabPage_ClickAction(this.btnTabs_Dictionary[lbl_Header.Text]);
            }
            catch (Exception ex)
            {
                string _ExceptionMessage = string.Empty;
                _ExceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Error Loading Database: \r\n" + ex.Message + "\r\n\r\nInnerException: " + _ExceptionMessage);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        void LoadOthers()
        {
            try
            {
                this.LoadBillingObjects();
                this.LoadLoadProfileObjects();

                this.cmb_Devices.DataSource = configs.Device;
                this.cmb_Devices.DisplayMember = "Device_Name";
                this.cmb_Devices.ValueMember = "id";

                if (this.cmb_Devices.Items.Count > 0)
                    this.cmb_Devices.SelectedIndex = 0;

                this.cmb_Catagory.DataSource = null;
                this.cmb_Catagory.DataSource = this._catagoryList;
                this.cmb_Catagory.DisplayMember = "CatagoryName";
                this.cmb_Catagory.ValueMember = "OBIS_Code";
                this.cmb_Catagory.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadLoadProfileObjects()
        {
            this._allLoadProfileItemsList = new List<CaptureObjectInfo>();
            this._listLoadProfileAllQuantities = new List<Configs.AllQuantitiesRow>();
            DataTable dt = configs.LoadProfileChannels.DefaultView.ToTable(true, "QuantityIndex", "AttributeNo");
            if (dt != null && dt.Rows.Count > 0)
            {
                this.Log_Message("Loading LP Objects . . .", 0, dt.Rows.Count);
                for (int i = 0; i < dt.Rows.Count; i++)
                {                    
                    CaptureObjectInfo coInfo = new CaptureObjectInfo();
                    coInfo.AttributeNo = Convert.ToInt32(dt.Rows[i]["AttributeNo"].ToString());
                    coInfo.OBIS_Index = Convert.ToInt64(dt.Rows[i]["QuantityIndex"].ToString());
                    this._allLoadProfileItemsList.Add(coInfo);
                    Configs.AllQuantitiesRow row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == coInfo.OBIS_Index);
                    if (row != null)
                        this._listLoadProfileAllQuantities.Add(row);

                    this.Log_Message("Loading LP Objects . . .", i + 1, dt.Rows.Count);
                }

                this.Log_Message("Loaded LP Objects successfully.", dt.Rows.Count, dt.Rows.Count);

            }
        }
        private void LoadBillingObjects()
        {
            using (StreamReader sr = File.OpenText("BillingCaptureObjects.txt"))
            {
                string s = String.Empty;
                this._listBillingItemsAllQuantities = new List<Configs.AllQuantitiesRow>();
                this._allBillingItemsList = new List<CaptureObjectInfo>();
                                
                int currentLine = 0;
                int totalLines = File.ReadAllLines("BillingCaptureObjects.txt").Length;
                this.Log_Message("Loading bill objects. . .", 0, totalLines);
                while ((s = sr.ReadLine()) != null)
                {
                    string[] words = s.Split(' ');
                    if (words.Length == 2)
                    {
                        long obis_code = Convert.ToInt64(words[0].Trim());
                        int attNo = Convert.ToInt32(words[1].Trim());
                        CaptureObjectInfo coInfo = new CaptureObjectInfo();
                        coInfo.AttributeNo = attNo;
                        coInfo.OBIS_Index = obis_code;
                        this._allBillingItemsList.Add(coInfo);
                        Configs.AllQuantitiesRow row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == obis_code);
                        if (row != null)
                            _listBillingItemsAllQuantities.Add(row);
                    }

                    this.Log_Message("Loading bill objects. . .", ++currentLine, totalLines);
                }

                this.Log_Message("Loaded bill objects successfully.", totalLines, totalLines);
            }
        }
        void GetGroupIds(long obisCode)
        {
            try
            {
                if (configs.CaptureObjects != null && configs.CaptureObjects.Rows.Count > 0)
                {
                    DataTable tblFiltered = configs.CaptureObjects.Select(string.Format("Target_OBIS_Index = {0} ", obisCode)).CopyToDataTable();
                    if (tblFiltered != null && tblFiltered.Rows.Count > 0)
                    {
                        DataTable dt = tblFiltered.DefaultView.ToTable(true, "GroupId");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string item = null;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                try
                                {
                                    item = dt.Rows[i].Field<long>("GroupId").ToString();
                                }
                                catch
                                {
                                    item = null;
                                }
                                if (!string.IsNullOrEmpty(item))
                                    this.cmb_BillingGroups.Items.Add(item);
                            }

                        }

                        if (this.cmb_BillingGroups.Items.Count > 0)
                            this.cmb_BillingGroups.SelectedIndex = 0;
                    }
                }
            }
            catch
            {
            }
        }

        private void Load_DBtoConfigs()
        {
            try
            {
                #region OldCode COMMENTED

                //Configs newConfig = new Configs();
                /// newConfig.ReadXml(xmlConfig, XmlReadMode.Auto);
                /// newConfig.LoadConfigurations();
                //if (this._dbAccessLayer != null)

                #endregion

                if (this._connectionString != null && this._dbController != null)
                {
                    #region OldCode COMMENTED

                    //this._dbController.Load_Manufac_Data(configs);

                    //this._dbAccessLayer.Load_All_Configurations(ref newConfig);
                    //this._configDB.Load_All_Configurations(ref newConfig);
                    //newConfig.Clear();

                    #endregion
                    //_dbController.Load_All_Configurations(configs);
                    configs.Clear();
                    int tableCount = _dbController.Load_All_Configs.Count;
                    int i = 0;
                    try
                    {
                        while (i < tableCount)
                        {
                            this.Log_Message(this._dbController.Load_All_Configs[i].Method.Name, i, tableCount);
                            _dbController.Load_All_Configs[i](configs);

                            i++;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Log_Message("Loading failed", 0, tableCount);

                        string _ExceptionMessage = string.Empty;
                        _ExceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                        MessageBox.Show("TableName: " + this._dbController.Load_All_Configs[i].Method.Name.Substring(5) + ": \r\n" + _ExceptionMessage);

                        throw ex;
                    }
                    finally
                    {
                        configs.AcceptChanges();
                    }
                    this.Log_Message("Loaded successfully", tableCount, tableCount);

                    #region OldCode COMMENTED

                    /*
                    //configs = newConfig;

                    /// Set Data Source
                    this.allQuantitiesBindingSource.DataSource = configs;

                    this.configurationBindingSource.DataSource = configs;
                    this.OBISDetailsBindingSource.DataSource = configs;
                    this.ObisRightsGroupbindingSource.DataSource = configs;
                    this.LPGP_bindingSource.DataSource = configs;
                    this.eventInfoGP_bindingSource.DataSource = configs;
                    this.billItemsGP_bindingSource.DataSource = configs;
                    this.displayWindowsGP_BindingSource.DataSource = configs;

                    this.displayWindowsbindingSource.DataSource = configs;
                    this.loadProfileChannelsBindingSource.DataSource = configs;
                    this.billingItemsBindingSource.DataSource = configs;
                    this.billTariffQuantityBindingSource.DataSource = configs;
                    this.eventInfoBindingSource.DataSource = configs;
                    this.eventLogsBindingSource.DataSource = configs;
                    this.ManufacturerbindingSource.DataSource = configs;
                    this.AuthenticationbindingSource.DataSource = configs;
                    this.DevicebindingSource.DataSource = configs;
                    this.DeviceAssociationbindingSource.DataSource = configs;
                    this.captureObjectsBindingSource.DataSource = configs;
                    
                    
                    // Refresh All Data Grids
                    // meterInfoDataGridView.Refresh();
                    // meterUserDataGridView.Refresh();

                    // sAP_dgv_Configuration.Refresh();
                    // meterdgv_Configuration.Refresh();
                    dgv_Configuration.Refresh();
                    ManufacturerDataGridView.Refresh();
                    AuthenticationDataGridView.Refresh();
                    dgv_DeviceAssociation.Refresh();
                    dgv_Device.Refresh();
                    dgv_LpChannels.Refresh();
                    dgv_BillingItemGroup.Refresh();
                    dgv_EventsGroup.Refresh();
                    dgv_ObisDetails.Refresh();
                    dgv_ObisRightsGroup.Refresh();

                    dgv_LpChannels.Refresh();
                    dgv_BillingItems.Refresh();
                    dgv_BillTariffQuantity.Refresh();
                    dgv_EventInfo.Refresh();
                    dgv_CaptureObjects.Refresh();
                    allQuantitiesDataGridView.Refresh();
                    */
                    #endregion
                }
                else
                {
                    MessageBox.Show("Database Settings in config file are not correct ");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region All Quantities Labels

        private void dgv_AllQuantities_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            //try
            //{
            //    DataGridViewRow CurrentRow = (DataGridViewRow)allQuantitiesDataGridView.Rows[e.RowIndex];
            //    Configs.AllQuantitiesRow DataRow = null;
            //    DataRow = (Configs.AllQuantitiesRow)((DataRowView)CurrentRow.DataBoundItem).Row;
            //    StOBISCode OBISCode = (Get_Index)DataRow.OBIS_Index;
            //    if (allQuantitiesDataGridView["OBIS_Code", e.RowIndex].ColumnIndex == e.ColumnIndex)
            //    {
            //        e.Value = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
            //    }
            //    else if (allQuantitiesDataGridView["OBIS_Name", e.RowIndex].ColumnIndex == e.ColumnIndex)
            //    {
            //        e.Value = OBISCode.OBISIndex.ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (e != null)
            //        e.Value = "!";
            //}

        }

        private void dgv_AllQuantities_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow CurrentRow = null;
            //DataGridViewCell CurrentCell = null;
            //DataGridViewCell OBISCodeCell = null;
            //Configs.AllQuantitiesRow DataRow = null;
            //try
            //{
            //    CurrentRow = (DataGridViewRow)allQuantitiesDataGridView.Rows[e.RowIndex];
            //    CurrentCell = (DataGridViewCell)allQuantitiesDataGridView[e.ColumnIndex, e.RowIndex];
            //    OBISCodeCell = (DataGridViewCell)allQuantitiesDataGridView["OBIS_Code", e.RowIndex];

            //    if (CurrentCell == OBISCodeCell)
            //    {
            //        DataRow = (Configs.AllQuantitiesRow)((DataRowView)CurrentRow.DataBoundItem).Row;
            //        StOBISCode OBISCode = (Get_Index)DataRow.OBIS_Index;
            //        string OBIS = (String)OBISCodeCell.EditedFormattedValue;
            //        StOBISCode OBISCodeVal = StOBISCode.ConvertFrom(OBIS);
            //        Configs dt = ((Configs)allQuantitiesBindingSource.DataSource);
            //        if (DataRow != null && dt.AllQuantities.FindByOBIS_Index(OBISCodeVal.OBIS_Value) == null)
            //            DataRow.OBIS_Index = OBISCodeVal.OBIS_Value;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    if (CurrentCell != null)
            //        CurrentCell.Value = "!";
            //}
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_all_quantities.ClearSelection();

                FindOBISCodeDialog OBISFinder = new FindOBISCodeDialog();
                Configs.AllQuantitiesRow CurrentRow = (SelectedAllQuantityRows != null &&
                    SelectedAllQuantityRows.Count > 0) ? SelectedAllQuantityRows[0] : configs.AllQuantities[0];
                StOBISCode OBISCode = (Get_Index)CurrentRow.OBIS_Index;
                ///Populate OBIS_Finder
                OBISFinder.Get_Index_Name = OBISCode.OBISIndex.ToString();
                OBISFinder.Label = CurrentRow.Label;
                OBISFinder.OBISCodeStr = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                OBISFinder.OBISId = OBISCode.OBIS_Value.ToString();
                OBISFinder.ShowDialog(this);
                if (OBISFinder.DialogResult == DialogResult.Cancel)
                    return;
                //foreach (DataGridViewRow OBISFinderRow in allQuantitiesDataGridView.Rows)
                //{
                //    OBISFinderRow.Selected = false;
                //}
                #region ///Search By OBIS Index
                ulong OBISIndexNum = 0;
                if (ulong.TryParse(OBISFinder.OBISId, out OBISIndexNum))
                {
                    foreach (DataGridViewRow OBISFinderRow in dgv_all_quantities.Rows)
                    {
                        Configs.AllQuantitiesRow OBISIndexRow =
                            (Configs.AllQuantitiesRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                        if (OBISIndexRow.OBIS_Index == OBISIndexNum)
                        {
                            OBISFinderRow.Selected = true;
                            dgv_all_quantities.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
                            break;
                        }
                    }
                }
                #endregion
                #region ///Search By OBIS_Name
                if (!String.IsNullOrEmpty(OBISFinder.Get_Index_Name))
                {
                    String OBISName = OBISFinder.Get_Index_Name;
                    EnumConverter Convertor = new EnumConverter(typeof(Get_Index));
                    String[] OBisName = Enum.GetNames(typeof(Get_Index));
                    String[] OBISNames = Array.FindAll<string>(OBisName, (x) => x.ToLower().Contains(OBISName.ToLower()));
                    foreach (var _OBISName in OBISNames)
                    {
                        Get_Index OBIS_Index = (Get_Index)Convertor.ConvertFromString(_OBISName);
                        StOBISCode OBISCodeT = OBIS_Index;
                        foreach (DataGridViewRow OBISFinderRow in dgv_all_quantities.Rows)
                        {
                            Configs.AllQuantitiesRow OBISIndexRow =
                                (Configs.AllQuantitiesRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                            if (OBISIndexRow.OBIS_Index == OBISCodeT.OBIS_Value)
                            {
                                OBISFinderRow.Selected = true;
                                dgv_all_quantities.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
                                break;
                            }
                        }
                    }

                }
                #endregion
                #region ///Search By OBIS Label
                if (!String.IsNullOrEmpty(OBISFinder.Label))
                {
                    String OBISName = OBISFinder.Label;
                    foreach (DataGridViewRow OBISFinderRow in dgv_all_quantities.Rows)
                    {
                        Configs.AllQuantitiesRow OBISIndexRow = null;
                        if (OBISFinderRow != null && OBISFinderRow.DataBoundItem != null)
                        {
                            OBISIndexRow = (Configs.AllQuantitiesRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                            if (OBISIndexRow.Label != null &&
                                OBISIndexRow.Label.ToLower().Contains(OBISFinder.Label.ToLower()))
                            {
                                OBISFinderRow.Selected = true;
                                dgv_all_quantities.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
                                continue;
                            }
                        }
                    }
                }
                #endregion
                #region ///Search By OBIS Code
                Regex OBISValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);
                if (OBISValidator.IsMatch(OBISFinder.OBISCodeStr))
                {
                    ///Evaluate the OBIS Code String For Values
                    String OBISString = OBISFinder.OBISCodeStr;
                    Match OBISStrs = OBISValidator.Match(OBISString);

                    foreach (DataGridViewRow OBISFinderRow in dgv_all_quantities.Rows)
                    {
                        Configs.AllQuantitiesRow OBISIndexRow = null;
                        if (OBISFinderRow != null && OBISFinderRow.DataBoundItem != null)
                        {
                            OBISIndexRow = (Configs.AllQuantitiesRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                            ///Compute Each OBIS Feilds Separatly
                            ulong OBISVal = (ulong)OBISIndexRow.OBIS_Index;
                            StOBISCode OBIS = (Get_Index)OBISVal;
                            String OBISStr = OBIS.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                            Match OBISToMatchStrs = OBISValidator.Match(OBISStr);
                            bool IsMatched = true;
                            ///Compare OBIS Codes
                            if (OBISStrs.Groups["ClassId"].Success)
                            {
                                string ClassId = OBISStrs.Groups["ClassId"].Value;
                                string ClassIdTOMatch = OBISToMatchStrs.Groups["ClassId"].Value;
                                if (!ClassId.Equals(ClassIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldA"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldA"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldA"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldB"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldB"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldB"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldC"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldC"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldC"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldD"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldD"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldD"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldE"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldE"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldE"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldF"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldF"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldF"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (IsMatched)
                            {
                                OBISFinderRow.Selected = true;
                                dgv_all_quantities.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error Searching OBIS Text", "Error Search");
            }
        }

        private void insertNewQuantityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OBISCodeDialog OBISDialog = new OBISCodeDialog();
                Configs.AllQuantitiesRow CurrentRow = (SelectedAllQuantityRows != null && SelectedAllQuantityRows.Count > 0) ?
                    SelectedAllQuantityRows[0] : configs.AllQuantities[0]; StOBISCode OBISCode = (Get_Index)CurrentRow.OBIS_Index;
                // Populate OBIS_Code Dialog
                OBISDialog.Get_Index_Name = OBISCode.OBISIndex.ToString();
                OBISDialog.Label = CurrentRow.Label;
                OBISDialog.OBISCodeStr = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                OBISDialog.OBISId = OBISCode.OBIS_Value.ToString();
                OBISDialog.ObisCodeMode = OBISCodeDialog.OBISCodeMode.ObisCode;
                OBISDialog.ShowDialog(this);
                if (OBISDialog.DialogResult == DialogResult.Cancel)
                    return;
                string Label = OBISDialog.Label;
                if (OBISDialog.ObisCodeMode == OBISCodeDialog.OBISCodeMode.ObisIndex)
                {
                    #region // Insert By OBISIndex
                    ulong OBISIndexNum = 0;
                    if (ulong.TryParse(OBISDialog.OBISId, out OBISIndexNum))
                    {
                        StOBISCode ObisValidate = Get_Index.Dummy;
                        ObisValidate.OBIS_Value = OBISIndexNum;
                        // Validate OBISIndex
                        if (ObisValidate.ClassId > 0 && (ObisValidate.OBIS_Value & 0x0000FFFFFFFFFFFF) > 0)
                        {
                            var Prev_Row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == Convert.ToDecimal(OBISIndexNum));
                            if (Prev_Row != null)
                            {
                                MessageBox.Show("Failure", string.Format("{0} Obis code Already Exists", ObisValidate.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                NewRow.OBIS_Index = Convert.ToDecimal(OBISIndexNum);
                                NewRow.Label = Label;
                                configs.AllQuantities.AddAllQuantitiesRow(NewRow);
                                MessageBox.Show("Success", string.Format("{0} Obis code Successfuly Added",
                                    ObisValidate.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                            }
                        }
                    }
                    #endregion
                }
                else if (OBISDialog.ObisCodeMode == OBISCodeDialog.OBISCodeMode.ObisLabel)
                {
                    #region // SearchByOBIS_Name
                    if (!String.IsNullOrEmpty(OBISDialog.Get_Index_Name))
                    {
                        String OBISName = OBISDialog.Get_Index_Name;
                        EnumConverter Convertor = new EnumConverter(typeof(Get_Index));
                        String[] OBisName = Enum.GetNames(typeof(Get_Index));
                        String[] OBISNames = Array.FindAll<string>(OBisName, (x) => x.ToLower().Contains(OBISName.ToLower()));
                        if (OBISNames != null && OBISNames.Length > 0)
                        {
                            Get_Index OBIS_Index = (Get_Index)Convertor.ConvertFromString(OBISNames[0]);
                            StOBISCode OBISCodeT = OBIS_Index;
                            var Prev_Row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == Convert.ToDecimal(OBISCodeT.OBIS_Value));
                            if (Prev_Row != null)
                            {
                                MessageBox.Show("Failure", string.Format("{0} Obis code Already Exists",
                                    OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                NewRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                                NewRow.Label = Label;
                                configs.AllQuantities.AddAllQuantitiesRow(NewRow);
                                MessageBox.Show("Success", string.Format("{0} Obis code Successfuly Added",
                                    OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                            }
                        }
                    }
                    #endregion
                }
                else if (OBISDialog.ObisCodeMode == OBISCodeDialog.OBISCodeMode.ObisCode)
                {
                    #region // Search By OBIS Code
                    Regex OBISValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);
                    if (OBISValidator.IsMatch(OBISDialog.OBISCodeStr))
                    {
                        // Evaluate the OBISCode String For Value  
                        String OBISString = OBISDialog.OBISCodeStr;
                        Match OBISStrs = OBISValidator.Match(OBISString);
                        // Validate OBIS Code STR      
                        if (OBISStrs.Groups["ClassId"].Success &&
                            OBISStrs.Groups["FieldA"].Success &&
                            OBISStrs.Groups["FieldB"].Success &&
                            OBISStrs.Groups["FieldC"].Success &&
                            OBISStrs.Groups["FieldD"].Success &&
                            OBISStrs.Groups["FieldE"].Success &&
                            OBISStrs.Groups["FieldF"].Success)
                        {
                            StOBISCode OBISCodeT = Get_Index.Dummy;
                            OBISCodeT = StOBISCode.ConvertFrom(OBISString);
                            var Prev_Row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == Convert.ToDecimal(OBISCodeT.OBIS_Value));
                            if (Prev_Row != null)
                            {
                                MessageBox.Show("Failure", string.Format("{0} Obis code Already Exists",
                                    OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                NewRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                                NewRow.Label = Label;
                                configs.AllQuantities.AddAllQuantitiesRow(NewRow);
                                MessageBox.Show("Success", string.Format("{0} Obis code Successfuly Added",
                                    OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failure", string.Format("{0} Obis Code not complete specified", OBISDialog.OBISCodeStr));
                            return;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, "Error Insert New OBIS Entry " + ex.Message, ""); 
                MessageBox.Show("Failure", "Error Insert New OBIS Entry " + ex.Message);
            }
        }

        void SearchQuantityFromAllQuantities()
        {
            try
            {
                dgv_all_quantities.ClearSelection();

                // Populate OBIS_Code Dialog
                this.Get_Index_Name = this.tb_Label.Text;//OBISCode.OBISIndex.ToString();
                this.Label = this.tb_Label.Text;
                this.OBISCodeStr = this.tb_ObisIndexAllQuan.Text;//OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                this.OBISId = this.tb_ObisIndexAllQuan.Text;

                this.Update_GUI(this.tb_Label.Text, this.tb_ObisIndexAllQuan.Text);


                #region ///Search By OBIS Index
                if (this.ObisCodeMode == OBISCodeMode.ObisIndex)
                {
                    ulong OBISIndexNum = 0;
                    if (ulong.TryParse(this.OBISId, out OBISIndexNum))
                    {
                        foreach (DataGridViewRow OBISFinderRow in dgv_all_quantities.Rows)
                        {
                            Configs.AllQuantitiesRow OBISIndexRow =
                                (Configs.AllQuantitiesRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                            if (OBISIndexRow.OBIS_Index == OBISIndexNum)
                            {
                                OBISFinderRow.Selected = true;
                                dgv_all_quantities.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
                                break;
                            }
                        }
                    }
                }
                #endregion

                #region ///Search By OBIS Code

                else if (this.ObisCodeMode == OBISCodeMode.ObisCode)
                {
                    Regex OBISValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);
                    if (OBISValidator.IsMatch(this.OBISCodeStr))
                    {
                        ///Evaluate the OBIS Code String For Values
                        String OBISString = this.OBISCodeStr;
                        Match OBISStrs = OBISValidator.Match(OBISString);

                        foreach (DataGridViewRow OBISFinderRow in dgv_all_quantities.Rows)
                        {
                            Configs.AllQuantitiesRow OBISIndexRow = null;
                            if (OBISFinderRow != null && OBISFinderRow.DataBoundItem != null)
                            {
                                OBISIndexRow = (Configs.AllQuantitiesRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                                ///Compute Each OBIS Feilds Separatly
                                ulong OBISVal = (ulong)OBISIndexRow.OBIS_Index;
                                StOBISCode OBIS = (Get_Index)OBISVal;
                                String OBISStr = OBIS.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                                Match OBISToMatchStrs = OBISValidator.Match(OBISStr);
                                bool IsMatched = true;
                                ///Compare OBIS Codes
                                if (OBISStrs.Groups["ClassId"].Success)
                                {
                                    string ClassId = OBISStrs.Groups["ClassId"].Value;
                                    string ClassIdTOMatch = OBISToMatchStrs.Groups["ClassId"].Value;
                                    if (!ClassId.Equals(ClassIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldA"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldA"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldA"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldB"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldB"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldB"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldC"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldC"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldC"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldD"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldD"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldD"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldE"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldE"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldE"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldF"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldF"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldF"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (IsMatched)
                                {
                                    OBISFinderRow.Selected = true;
                                    dgv_all_quantities.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
                                }
                            }
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error Searching OBIS Text", "Error Search");
            }
        }
        void UpdateQuantityInAllQuantities()
        {
            try
            {
                Configs.AllQuantitiesRow CurrentRow = null;

                if (SelectedAllQuantityRows != null &&
                    SelectedAllQuantityRows.Count > 0)
                    CurrentRow = SelectedAllQuantityRows[0];

                if (CurrentRow == null)
                {
                    MessageBox.Show("Please Select AllQuantity Rows to proceed", "Failure");
                    return;
                }

                // Populate OBIS_Code Dialog
                this.Get_Index_Name = this.tb_Label.Text;//OBISCode.OBISIndex.ToString();
                this.Label = this.tb_Label.Text;
                this.OBISCodeStr = this.tb_ObisIndexAllQuan.Text;//OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                this.OBISId = this.tb_ObisIndexAllQuan.Text;

                this.Update_GUI(this.tb_Label.Text, this.tb_ObisIndexAllQuan.Text);

                string Label = this.Label;

                if (this.ObisCodeMode == OBISCodeMode.ObisIndex)
                {
                    #region // Insert By OBISIndex

                    ulong OBISIndexNum = 0;
                    if (ulong.TryParse(this.OBISId, out OBISIndexNum))
                    {
                        StOBISCode ObisValidate = Get_Index.Dummy;
                        ObisValidate.OBIS_Value = OBISIndexNum;

                        // Validate OBISIndex
                        if (ObisValidate.ClassId > 0 && (ObisValidate.OBIS_Value & 0x0000FFFFFFFFFFFF) > 0)
                        {
                            CurrentRow.OBIS_Index = Convert.ToDecimal(OBISIndexNum);
                            CurrentRow.Label = Label;

                            StOBISCode OBISCode = (Get_Index)CurrentRow.OBIS_Index;
                            CurrentRow.Quantity_Code = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                            CurrentRow.Quantity_Name = Label;

                            MessageBox.Show(string.Format("{0} Obis code Edited  Successfuly",
                                                                     ObisValidate.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)), "Success"); 
                        }
                    }

                    #endregion
                }
                else if (this.ObisCodeMode == OBISCodeMode.ObisLabel)
                {
                    #region // SearchByOBIS_Name

                    if (!String.IsNullOrEmpty(this.Get_Index_Name))
                    {
                        String OBISName = this.Get_Index_Name;
                        EnumConverter Convertor = new EnumConverter(typeof(Get_Index));
                        String[] OBisName = Enum.GetNames(typeof(Get_Index));
                        String[] OBISNames = Array.FindAll<string>(OBisName, (x) => x.ToLower().Contains(OBISName.ToLower()));

                        if (OBISNames != null && OBISNames.Length > 0)
                        {
                            Get_Index OBIS_Index = (Get_Index)Convertor.ConvertFromString(OBISNames[0]);
                            StOBISCode OBISCodeT = OBIS_Index;
                            CurrentRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                            CurrentRow.Label = Label;

                            MessageBox.Show(string.Format("{0} Obis code Edited  Successfuly",
                                                                     OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)), "Success");

                        }
                    }

                    #endregion
                }
                else if (this.ObisCodeMode == OBISCodeMode.ObisCode)
                {
                    #region // Search By OBIS Code

                    Regex OBISValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);
                    if (OBISValidator.IsMatch(this.OBISCodeStr))
                    {
                        // Evaluate the OBISCode String For Value
                        String OBISString = this.OBISCodeStr;
                        Match OBISStrs = OBISValidator.Match(OBISString);

                        // Validate OBIS Code STR
                        if (OBISStrs.Groups["ClassId"].Success &&
                            OBISStrs.Groups["FieldA"].Success &&
                            OBISStrs.Groups["FieldB"].Success &&
                            OBISStrs.Groups["FieldC"].Success &&
                            OBISStrs.Groups["FieldD"].Success &&
                            OBISStrs.Groups["FieldE"].Success &&
                            OBISStrs.Groups["FieldF"].Success)
                        {
                            StOBISCode OBISCodeT = Get_Index.Dummy;
                            OBISCodeT = StOBISCode.ConvertFrom(OBISString);

                            CurrentRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                            CurrentRow.Label = Label;
                            CurrentRow.Quantity_Code = OBISString;
                            CurrentRow.Quantity_Name = label;
                            MessageBox.Show(string.Format("{0} Obis code Edited  Successfuly",
                                                                     OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)), "Success");

                        }
                        else
                        {
                            MessageBox.Show(string.Format("{0} Obis Code not complete specified", this.OBISCodeStr), "Failure");
                            return;
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Insert New OBIS Entry " + ex.Message, "Failure");
            }
        }
        void AddNewQuantityInAllQuantities()
        {
            try
            {
                // Populate OBIS_Code Dialog
                this.Get_Index_Name = this.tb_Label.Text;//OBISCode.OBISIndex.ToString();
                this.Label = this.tb_Label.Text;
                this.OBISCodeStr = this.tb_ObisIndexAllQuan.Text;//OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                this.OBISId = this.tb_ObisIndexAllQuan.Text;

                this.Update_GUI(this.tb_Label.Text, this.tb_ObisIndexAllQuan.Text);

                string Label = this.Label;

                if (this.ObisCodeMode == OBISCodeMode.ObisIndex)
                {
                    #region // Insert By OBISIndex

                    ulong OBISIndexNum = 0;
                    if (ulong.TryParse(this.OBISId, out OBISIndexNum))
                    {
                        StOBISCode ObisValidate = Get_Index.Dummy;
                        ObisValidate.OBIS_Value = OBISIndexNum;

                        // Validate OBISIndex
                        if (ObisValidate.ClassId > 0 && (ObisValidate.OBIS_Value & 0x0000FFFFFFFFFFFF) > 0)
                        {
                            var Prev_Row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == Convert.ToDecimal(OBISIndexNum));
                            if (Prev_Row != null)
                            {
                                MessageBox.Show(string.Format("{0} Obis code Already Exists.", ObisValidate.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)), "Failure");
                                return;
                            }
                            else
                            {
                                var NewRow = configs.AllQuantities.NewAllQuantitiesRow();

                                NewRow.OBIS_Index = Convert.ToDecimal(OBISIndexNum);
                                NewRow.Label = Label;
                                StOBISCode OBISCode = (Get_Index)NewRow.OBIS_Index;
                                NewRow.Quantity_Code = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                                NewRow.Quantity_Name = Label;
                                NewRow.Dp_Name = "";
                                NewRow.Unit = "";
                                NewRow.Priority = 0;

                                configs.AllQuantities.AddAllQuantitiesRow(NewRow);

                                MessageBox.Show(string.Format("{0} Obis code Successfuly Added",
                                                                         ObisValidate.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode),"Success"));
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("{0} is invalid obis code.", ObisValidate.ToString()), "Failure");
                        }
                    }

                    #endregion
                }
                else if (this.ObisCodeMode == OBISCodeMode.ObisLabel)
                {
                    #region // Insert By OBIS_Name

                    if (!String.IsNullOrEmpty(this.Get_Index_Name))
                    {
                        String OBISName = this.Get_Index_Name;
                        EnumConverter Convertor = new EnumConverter(typeof(Get_Index));
                        String[] OBisName = Enum.GetNames(typeof(Get_Index));
                        String[] OBISNames = Array.FindAll<string>(OBisName, (x) => x.ToLower().Contains(OBISName.ToLower()));

                        if (OBISNames != null && OBISNames.Length > 0)
                        {
                            Get_Index OBIS_Index = (Get_Index)Convertor.ConvertFromString(OBISNames[0]);
                            StOBISCode OBISCodeT = OBIS_Index;

                            var Prev_Row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == Convert.ToDecimal(OBISCodeT.OBIS_Value));
                            if (Prev_Row != null)
                            {
                                MessageBox.Show("Failure", string.Format("{0} Obis code Already Exists",
                                                                                          OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                NewRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                                NewRow.Label = Label;
                                NewRow.Dp_Name = "";
                                NewRow.Unit = "";
                                NewRow.Priority = 0;

                                configs.AllQuantities.AddAllQuantitiesRow(NewRow);

                                MessageBox.Show("Success", string.Format("{0} Obis code Successfuly Added",
                                                                         OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                            }
                        }
                    }

                    #endregion
                }
                else if (this.ObisCodeMode == OBISCodeMode.ObisCode)
                {
                    #region // Insert By OBIS Code

                    Regex OBISValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);
                    if (OBISValidator.IsMatch(this.OBISCodeStr))
                    {
                        // Evaluate the OBISCode String For Value
                        String OBISString = this.OBISCodeStr;
                        Match OBISStrs = OBISValidator.Match(OBISString);

                        // Validate OBIS Code STR
                        if (OBISStrs.Groups["ClassId"].Success &&
                            OBISStrs.Groups["FieldA"].Success &&
                            OBISStrs.Groups["FieldB"].Success &&
                            OBISStrs.Groups["FieldC"].Success &&
                            OBISStrs.Groups["FieldD"].Success &&
                            OBISStrs.Groups["FieldE"].Success &&
                            OBISStrs.Groups["FieldF"].Success)
                        {
                            StOBISCode OBISCodeT = Get_Index.Dummy;
                            OBISCodeT = StOBISCode.ConvertFrom(OBISString);
                            
                            if (OBISCodeT.ClassId < 1 || (OBISCodeT.OBIS_Value & 0x0000FFFFFFFFFFFF) < 1)
                                throw new Exception(string.Format("{0} is invalid obis code.", OBISCodeT.ToString()));

                            var Prev_Row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == Convert.ToDecimal(OBISCodeT.OBIS_Value));
                            if (Prev_Row != null)
                            {
                                MessageBox.Show(string.Format("{0} Obis code Already Exists",
                                                                                          OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)), "Failure");
                                return;
                            }
                            else
                            {
                                var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                NewRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                                NewRow.Label = Label;
                                NewRow.Quantity_Code = OBISString;
                                NewRow.Quantity_Name = Label;

                                NewRow.Dp_Name = "";
                                NewRow.Unit = "";
                                NewRow.Priority = 0;

                                configs.AllQuantities.AddAllQuantitiesRow(NewRow);

                                MessageBox.Show(string.Format("{0} Obis code Successfuly Added",
                                                                         OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)), "Success");
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("{0} Obis Code not complete specified", this.OBISCodeStr), "Failure");
                            return;
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Insert New OBIS Entry \r\n" + ex.Message, "Failure");
            }
        }

        private void editQuantityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OBISCodeDialog OBISDialog = new OBISCodeDialog();
                Configs.AllQuantitiesRow CurrentRow = null;

                if (SelectedAllQuantityRows != null &&
                    SelectedAllQuantityRows.Count > 0)
                    CurrentRow = SelectedAllQuantityRows[0];

                if (CurrentRow == null)
                {
                    MessageBox.Show("Failure", "Please Select AllQuantity Rows to proceed");
                    return;
                }

                StOBISCode OBISCode = (Get_Index)CurrentRow.OBIS_Index;
                // Populate OBIS_Code Dialog
                OBISDialog.Get_Index_Name = OBISCode.OBISIndex.ToString();
                OBISDialog.Label = CurrentRow.Label;
                OBISDialog.OBISCodeStr = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                OBISDialog.OBISId = OBISCode.OBIS_Value.ToString();
                OBISDialog.ObisCodeMode = OBISCodeDialog.OBISCodeMode.ObisCode;

                OBISDialog.ShowDialog(this);

                if (OBISDialog.DialogResult == DialogResult.Cancel)
                    return;

                string Label = OBISDialog.Label;

                if (OBISDialog.ObisCodeMode == OBISCodeDialog.OBISCodeMode.ObisIndex)
                {
                    #region // Insert By OBISIndex

                    ulong OBISIndexNum = 0;
                    if (ulong.TryParse(OBISDialog.OBISId, out OBISIndexNum))
                    {
                        StOBISCode ObisValidate = Get_Index.Dummy;
                        ObisValidate.OBIS_Value = OBISIndexNum;

                        // Validate OBISIndex
                        if (ObisValidate.ClassId > 0 && (ObisValidate.OBIS_Value & 0x0000FFFFFFFFFFFF) > 0)
                        {
                            var Prev_Row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == Convert.ToDecimal(OBISIndexNum));
                            if (Prev_Row != null)
                            {
                                MessageBox.Show("Failure", string.Format("{0} Obis Code Already Exists", ObisValidate.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                CurrentRow.OBIS_Index = Convert.ToDecimal(OBISIndexNum);
                                CurrentRow.Label = Label;

                                MessageBox.Show("Success", string.Format("{0} Obis code Edited  Successfuly",
                                                                         ObisValidate.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                            }
                        }
                    }

                    #endregion
                }
                else if (OBISDialog.ObisCodeMode == OBISCodeDialog.OBISCodeMode.ObisLabel)
                {
                    #region // SearchByOBIS_Name

                    if (!String.IsNullOrEmpty(OBISDialog.Get_Index_Name))
                    {
                        String OBISName = OBISDialog.Get_Index_Name;
                        EnumConverter Convertor = new EnumConverter(typeof(Get_Index));
                        String[] OBisName = Enum.GetNames(typeof(Get_Index));
                        String[] OBISNames = Array.FindAll<string>(OBisName, (x) => x.ToLower().Contains(OBISName.ToLower()));

                        if (OBISNames != null && OBISNames.Length > 0)
                        {
                            Get_Index OBIS_Index = (Get_Index)Convertor.ConvertFromString(OBISNames[0]);
                            StOBISCode OBISCodeT = OBIS_Index;

                            var Prev_Row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == Convert.ToDecimal(OBISCodeT.OBIS_Value));
                            if (Prev_Row != null)
                            {
                                MessageBox.Show("Failure", string.Format("{0} Obis code Already Exists",
                                                                                          OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                // var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                CurrentRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                                CurrentRow.Label = Label;
                                // configs.AllQuantities.AddAllQuantitiesRow(NewRow);
                                MessageBox.Show("Success", string.Format("{0} Obis code Edited Successfuly",
                                                                         OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                            }
                        }
                    }

                    #endregion
                }
                else if (OBISDialog.ObisCodeMode == OBISCodeDialog.OBISCodeMode.ObisCode)
                {
                    #region // Search By OBIS Code

                    Regex OBISValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);
                    if (OBISValidator.IsMatch(OBISDialog.OBISCodeStr))
                    {
                        // Evaluate the OBISCode String For Value
                        String OBISString = OBISDialog.OBISCodeStr;
                        Match OBISStrs = OBISValidator.Match(OBISString);

                        // Validate OBIS Code STR
                        if (OBISStrs.Groups["ClassId"].Success &&
                            OBISStrs.Groups["FieldA"].Success &&
                            OBISStrs.Groups["FieldB"].Success &&
                            OBISStrs.Groups["FieldC"].Success &&
                            OBISStrs.Groups["FieldD"].Success &&
                            OBISStrs.Groups["FieldE"].Success &&
                            OBISStrs.Groups["FieldF"].Success)
                        {
                            StOBISCode OBISCodeT = Get_Index.Dummy;
                            OBISCodeT = StOBISCode.ConvertFrom(OBISString);

                            var Prev_Row = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == Convert.ToDecimal(OBISCodeT.OBIS_Value));
                            if (Prev_Row != null)
                            {
                                MessageBox.Show("Failure", string.Format("{0} Obis Code Already Exists",
                                                                                          OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                // var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                CurrentRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                                CurrentRow.Label = Label;
                                // conifgs.AllQuantities.AddAllQuantitiesRow(NewRow);

                                MessageBox.Show("Success", string.Format("{0} Obis code Successfuly Added",
                                                                         OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failure", string.Format("{0} Obis Code not complete specified", OBISDialog.OBISCodeStr));
                            return;
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, "Error Insert New OBIS Entry " + ex.Message, "");
                MessageBox.Show("Failure", "Error Insert New OBIS Entry " + ex.Message);
            }
        }

        #endregion

        #region Display Windows Menu Items Handlers

        /// <summary>
        /// Add Selected Display Windows Into Display Windows Data Grid View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDisplayWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Configs.ConfigurationRow SelectedRow = (SelectedConfigurationRows != null &&
                                                        SelectedConfigurationRows.Count > 0) ?
                                                        SelectedConfigurationRows[0] : null;
                List<Configs.AllQuantitiesRow> SelectedWindows = SelectedAllQuantityRows;
                if (SelectedRow == null)
                {
                    MessageBox.Show("Please Select Configuration To Add Windows");
                    return;
                }
                if (SelectedWindows == null || SelectedWindows.Count == 0)
                {
                    MessageBox.Show("Please Select Quantities To be Added in Display Windows");
                    return;
                }

                SelectedWindows.Sort((x, y) => x.OBIS_Index.CompareTo(y.OBIS_Index));
                // Add Quantities In Display Windows Table
                foreach (var item in SelectedWindows)
                {
                    Configs.DisplayWindowsRow NewRow = (Configs.DisplayWindowsRow)configs.DisplayWindows.NewRow();
                    NewRow.DisplayWindowsGroupId = SelectedRow.DisplayWindowGroupId;

                    string WinName = string.Empty;
                    WinName = item.Label;

                    if (String.IsNullOrEmpty(WinName))
                        WinName = configs.AllQuantities.FindByDefault_OBIS_Code(item.OBIS_Index).Label;

                    NewRow.Label = WinName;
                    configs.DisplayWindows.AddDisplayWindowsRow(NewRow);
                }
                // MessageBox.Show(this, "Selected Windows Added Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Successful", "Selected Windows Added Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void removeWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Configs.ConfigurationRow SelectedRow = (SelectedConfigurationRows != null
        //            && SelectedConfigurationRows.Count > 0) ? SelectedConfigurationRows[0] : null;
        //        List<Configs.AllQuantitiesRow> SelectedWindows = SelectedAllQuantityRows;
        //        List<Configs.DisplayWindowsRow> SelectedRows = SelectedDisplayWindowRows;

        //        /// Remove Rows Selected from Display Windows
        //        if (SelectedRows.Count > 0)
        //        {
        //            SelectedWindows.Clear();
        //            foreach (var item in SelectedRows)
        //            {
        //                SelectedWindows.Add(configs.AllQuantities.FindByOBIS_Index(item.QuantityIndex));
        //            }
        //        }

        //        if (SelectedRow == null)
        //        {
        //            MessageBox.Show("Please Select Configuration To Remove Windows");
        //            return;
        //        }
        //        if (SelectedWindows == null || SelectedWindows.Count == 0)
        //        {
        //            MessageBox.Show("Please Select Quantities To be removed in Display Windows");
        //            return;
        //        }

        //        DialogResult result = MessageBox.Show(this, "Are you sure,want to remove selected display Windows",
        //                                                    "Confirm Delete",
        //                                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //        if (result != DialogResult.OK)
        //            return;

        //        /// Remove Quantities From Display Windows Table
        //        List<Configs.AllQuantitiesRow> WindowsRemoved = new List<Configs.AllQuantitiesRow>();
        //        foreach (var item in SelectedWindows)
        //        {
        //            try
        //            {
        //                DataRow[] win = configs.DisplayWindows.Select(String.Format(
        //                    "[DisplayWindowsGroupId] = {0} and [QuantityIndex] = {1}", SelectedRow.id, item.OBIS_Index));

        //                if (win != null && win.Length > 0)
        //                {
        //                    foreach (var winItem in win)
        //                    {
        //                        winItem.Delete();
        //                        configs.DisplayWindows.RemoveDisplayWindowsRow((Configs.DisplayWindowsRow)winItem);
        //                    }
        //                    WindowsRemoved.Add(item);
        //                }
        //            }
        //            catch (Exception)
        //            {
        //            }
        //        }

        //        String removedWindowList = "";
        //        if (WindowsRemoved.Count > 0)
        //        {
        //            foreach (var item in WindowsRemoved)
        //            {
        //                removedWindowList += String.Format("{0},", item.Label);
        //            }
        //            removedWindowList = removedWindowList.Trim(",".ToCharArray());
        //        }

        //       // Notification notifier = null;
        //        if (WindowsRemoved.Count <= 0)
        //            // MessageBox.Show(this, "Selected Windows not removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            MessageBox.Show("Error", "Selected Windows not removed");
        //        else
        //            //MessageBox.Show(this, String.Format("Selected Windows removed Successfully\r\n{0}", removedWindowList),
        //            //                      "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            MessageBox.Show("Successful", String.Format("Selected Windows removed Successfuly\r\n {0}", removedWindowList));

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void assignCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void assignSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void reAssignLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Configs.DisplayWindowsRow SelectedRow = (SelectedDisplayWindowRows != null &&
        //                                                 SelectedDisplayWindowRows.Count > 0) ?
        //                                                 SelectedDisplayWindowRows[0] : null;

        //        List<Configs.DisplayWindowsRow> SelectedWindows = new List<Configs.DisplayWindowsRow>(50);

        //        if (SelectedRow == null)
        //        {
        //            foreach (Configs.DisplayWindowsRow item in configs.DisplayWindows.Rows)
        //            {
        //                SelectedWindows.Add(item);
        //            }
        //            // MessageBox.Show("Please Select Configuration To Add Windows");
        //            // return;
        //        }
        //        else
        //        {
        //            SelectedWindows.Add(SelectedRow);
        //        }

        //        if (SelectedWindows == null || SelectedWindows.Count == 0)
        //        {
        //            MessageBox.Show("Please Select Display Windows Rows");
        //            return;
        //        }

        //        // Add Quantities In DisplayWindows Table
        //        foreach (var item in SelectedWindows)
        //        {
        //            string WinName = string.Empty;

        //            if (String.IsNullOrEmpty(WinName))
        //                WinName = configs.AllQuantities.FindByDefault_OBIS_Code(item.QuantityIndex).Label;

        //            if (String.IsNullOrEmpty(WinName))
        //                WinName = "unknown";

        //            item.Label = WinName;
        //            // configs.DisplayWindows.AddDisplayWindowsRow(NewRow);
        //        }
        //        // MessageBox.Show(this, "Selected Windows Labels Altered Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        MessageBox.Show("Successful",
        //                                                 "Selected Windows Labels Altered Successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        #endregion

        #region Display Windows Handlers

        /// <summary>
        /// To View Row Tool TIP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayWindowsDataGridView_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {

        }

        /// <summary>
        /// To Display Window Label (Unbounded) Cell_Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void displayWindowsDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {

        }


        #endregion

        #region Load Profile Handlers

        private void dgv_LpChannels_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (dgv_load_profile_channels["QuantityName", e.RowIndex].ColumnIndex == e.ColumnIndex)
                {
                    DataGridViewRow CurrentRow = (DataGridViewRow)dgv_load_profile_channels.Rows[e.RowIndex];
                    Configs.LoadProfileChannelsRow DataRow = null;
                    if (CurrentRow.DataBoundItem != null)
                    {
                        DataRow = (Configs.LoadProfileChannelsRow)((DataRowView)CurrentRow.DataBoundItem).Row;
                        e.Value = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == DataRow.QuantityIndex).Label;
                    }
                    ///DisplayWindowLabel(e.RowIndex);
                }
            }
            catch (Exception)
            {
                e.Value = "!";
            }
        }

        private void dgv_LpChannels_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            try
            {
                Configs.LoadProfileChannelsRow SelectedLPChannel = (Configs.LoadProfileChannelsRow)configs.LoadProfileChannels[e.RowIndex];
                Configs.AllQuantitiesRow ChannelWindowLabel = null;
                if (SelectedLPChannel != null)
                    ChannelWindowLabel = (Configs.AllQuantitiesRow)configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == SelectedLPChannel.QuantityIndex);
                String ToolTip = String.Format("{0} ,{1} ,{2} ,{3} ,{4}", SelectedLPChannel.id, ChannelWindowLabel.Label,
                    SelectedLPChannel.AttributeNo, SelectedLPChannel.FormatSpecifier, SelectedLPChannel.SequenceId);
                e.ToolTipText = ToolTip;
            }
            catch (Exception ex)
            {
                e.ToolTipText = "!";
            }
        }
        #endregion

        #region Member Methods

        ///// <summary>
        ///// Display Windows Labels That Programmed
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void DisplayWindowLabel(int RowIndex)
        //{
        //    Configs.DisplayWindowsRow SelectedDisplayWindowRow = null;
        //    try
        //    {
        //        SelectedDisplayWindowRow = (Configs.DisplayWindowsRow)configs.DisplayWindows[RowIndex];
        //        Configs.AllQuantitiesRow DisplayWindowLabel = null;
        //        if (SelectedDisplayWindowRow != null)
        //            DisplayWindowLabel = (Configs.AllQuantitiesRow)configs.AllQuantities.FindByOBIS_Index(SelectedDisplayWindowRow.QuantityIndex);
        //        this.displayWindowsDataGridView["WindowLabel", RowIndex].Value = DisplayWindowLabel.Label;

        //    }
        //    catch (Exception ex)
        //    {
        //        if (RowIndex != -1)
        //        {
        //            if (displayWindowsDataGridView.RowCount > RowIndex)
        //            {
        //                this.displayWindowsDataGridView["WindowLabel", RowIndex].Value = "!";
        //            }
        //        }
        //    }
        //}

        //private string DisplayWindowLabel(Configs.DisplayWindowsRow SelectedDisplayWindowRow)
        //{
        //    try
        //    {
        //        if (SelectedDisplayWindowRow == null)
        //            return null;
        //        Configs.AllQuantitiesRow DisplayWindowLabel = null;
        //        if (SelectedDisplayWindowRow != null)
        //            DisplayWindowLabel = (Configs.AllQuantitiesRow)configs.AllQuantities.FindByOBIS_Index(SelectedDisplayWindowRow.QuantityIndex);
        //        return DisplayWindowLabel.Label;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public List<Configs.Meter_InfoRow> SelectedMeterInfoRows
        //{
        //    get
        //    {
        //        List<Configs.Meter_InfoRow> SelectedRows = new List<Configs.Meter_InfoRow>();
        //        try
        //        {
        //            Configs.Meter_InfoRow CurrentRow;
        //            if (meterInfoDataGridView.SelectedRows != null && meterInfoDataGridView.SelectedRows.Count > 0)
        //            {
        //                foreach (DataGridViewRow item in meterInfoDataGridView.SelectedRows)
        //                {
        //                    CurrentRow = (Configs.Meter_InfoRow)((DataRowView)item.DataBoundItem).Row;
        //                    SelectedRows.Add(CurrentRow);
        //                }
        //            }

        //            return SelectedRows;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public List<Configs.Meter_ConfigurationRow> SelectedMeterConfigurationRows
        //{
        //    get
        //    {
        //        List<Configs.Meter_ConfigurationRow> SelectedRows = new List<Configs.Meter_ConfigurationRow>();
        //        try
        //        {
        //            Configs.Meter_ConfigurationRow CurrentRow;

        //            if (meterdgv_Configuration.SelectedRows != null && meterdgv_Configuration.SelectedRows.Count > 0)
        //            {
        //                foreach (DataGridViewRow item in meterdgv_Configuration.SelectedRows)
        //                {
        //                    CurrentRow = (Configs.Meter_ConfigurationRow)((DataRowView)item.DataBoundItem).Row;
        //                    SelectedRows.Add(CurrentRow);
        //                }
        //            }

        //            return SelectedRows;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

        public List<Configs.ConfigurationRow> SelectedConfigurationRows
        {
            get
            {
                List<Configs.ConfigurationRow> SelectedRows = new List<Configs.ConfigurationRow>();
                try
                {
                    Configs.ConfigurationRow CurrentRow;

                    /// int SelectedIndex = 0;
                    if (dgv_configuration_new.SelectedRows != null && dgv_configuration_new.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in dgv_configuration_new.SelectedRows)
                        {
                            CurrentRow = (Configs.ConfigurationRow)((DataRowView)item.DataBoundItem).Row;
                            SelectedRows.Add(CurrentRow);
                        }
                    }

                    return SelectedRows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<Configs.EventLogsRow> SelectedEventLogsRows
        {
            get
            {
                List<Configs.EventLogsRow> SelectedRows = new List<Configs.EventLogsRow>();
                try
                {
                    Configs.EventLogsRow CurrentRow;

                    /// int SelectedIndex = 0;
                    if (dgv_event_logs.SelectedRows != null && dgv_event_logs.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in dgv_event_logs.SelectedRows)
                        {
                            CurrentRow = (Configs.EventLogsRow)((DataRowView)item.DataBoundItem).Row;
                            SelectedRows.Add(CurrentRow);
                        }
                    }

                    return SelectedRows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<Configs.AllQuantitiesRow> SelectedAllQuantityRows
        {
            get
            {
                List<Configs.AllQuantitiesRow> SelectedRows = new List<Configs.AllQuantitiesRow>();
                try
                {
                    Configs.AllQuantitiesRow CurrentRow;
                    if (dgv_all_quantities.SelectedRows != null && dgv_all_quantities.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in dgv_all_quantities.SelectedRows)
                        {
                            if (item.DataBoundItem != null)
                            {
                                CurrentRow = (Configs.AllQuantitiesRow)((DataRowView)item.DataBoundItem).Row;
                                SelectedRows.Add(CurrentRow);
                            }
                        }
                    }

                    return SelectedRows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<Configs.OBIS_DetailsRow> SelectedOBISDetailRows
        {
            get
            {
                List<Configs.OBIS_DetailsRow> SelectedRows = new List<Configs.OBIS_DetailsRow>();
                try
                {
                    Configs.OBIS_DetailsRow CurrentRow;
                    if (dgv_obis_details.SelectedRows != null && dgv_obis_details.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in dgv_obis_details.SelectedRows)
                        {
                            if (item.DataBoundItem != null)
                            {
                                CurrentRow = (Configs.OBIS_DetailsRow)((DataRowView)item.DataBoundItem).Row;
                                SelectedRows.Add(CurrentRow);
                            }
                        }
                    }

                    return SelectedRows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        //public List<Configs.DisplayWindowsRow> SelectedDisplayWindowRows
        //{
        //    get
        //    {
        //        List<Configs.DisplayWindowsRow> SelectedRows = new List<Configs.DisplayWindowsRow>();
        //        try
        //        {
        //            Configs.DisplayWindowsRow CurrentRow;
        //            int SelectedIndex = 0;
        //            if (displayWindowsDataGridView.SelectedRows != null && displayWindowsDataGridView.SelectedRows.Count > 0)
        //            {
        //                foreach (DataGridViewRow item in displayWindowsDataGridView.SelectedRows)
        //                {
        //                    SelectedIndex = item.Index;
        //                    CurrentRow = (Configs.DisplayWindowsRow)configs.DisplayWindows[SelectedIndex];
        //                    SelectedRows.Add(CurrentRow);
        //                }
        //            }

        //            return SelectedRows;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

        public List<Configs.BillingItemsRow> SelectedBillItemRows
        {
            get
            {
                List<Configs.BillingItemsRow> SelectedRows = new List<Configs.BillingItemsRow>();
                try
                {
                    Configs.BillingItemsRow CurrentRow;
                    if (dgv_billing_items.SelectedRows != null && dgv_billing_items.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in dgv_billing_items.SelectedRows)
                        {
                            CurrentRow = (Configs.BillingItemsRow)((DataRowView)item.DataBoundItem).Row;
                            SelectedRows.Add(CurrentRow);
                        }
                    }

                    return SelectedRows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        public List<DataRow> SelctedRows(DataGridView SourceDataGridView)
        {
            List<DataRow> SelectedRows = new List<DataRow>();
            try
            {
                DataRow CurrentRow;
                if (SourceDataGridView.SelectedRows != null && SourceDataGridView.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow item in SourceDataGridView.SelectedRows)
                    {
                        CurrentRow = ((DataRowView)item.DataBoundItem).Row;
                        SelectedRows.Add(CurrentRow);
                    }
                }
                return SelectedRows;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Configs.LoadProfileChannelsRow> SelectedLoadProfileChannels
        {
            get
            {
                List<Configs.LoadProfileChannelsRow> SelectedRows = new List<Configs.LoadProfileChannelsRow>();
                try
                {
                    Configs.LoadProfileChannelsRow CurrentRow;
                    int SelectedIndex = 0;
                    if (dgv_load_profile_channels.SelectedRows != null && dgv_load_profile_channels.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in dgv_load_profile_channels.SelectedRows)
                        {
                            SelectedIndex = item.Index;
                            CurrentRow = (Configs.LoadProfileChannelsRow)configs.LoadProfileChannels[SelectedIndex];
                            SelectedRows.Add(CurrentRow);
                        }
                    }

                    return SelectedRows;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<Configs.BillingItemsRow> SelectedBillItemsRows
        {
            get
            {
                List<Configs.BillingItemsRow> SelectedRows = new List<Configs.BillingItemsRow>();
                try
                {
                    Configs.BillingItemsRow CurrentRow;

                    int SelectedIndex = 0;
                    if (dgv_billing_items.SelectedRows != null &&
                        dgv_billing_items.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in dgv_billing_items.SelectedRows)
                        {
                            SelectedIndex = item.Index;
                            CurrentRow = (Configs.BillingItemsRow)configs.BillingItems[SelectedIndex];
                            SelectedRows.Add(CurrentRow);
                        }
                    }

                    return SelectedRows;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region Load Profile Menu Event Handlers

        private void addLoadProfileChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Configs.ConfigurationRow> _SelectedConfigRows = SelectedConfigurationRows;

                Configs.ConfigurationRow SelectedRow = (_SelectedConfigRows != null && _SelectedConfigRows.Count > 0) ?
                                                        _SelectedConfigRows[0] : null;

                List<Configs.AllQuantitiesRow> SelectedLPChannels = SelectedAllQuantityRows;
                if (SelectedRow == null)
                {
                    MessageBox.Show("Please Select Configuration To Add Load Profile Channels");
                    return;
                }
                if (SelectedLPChannels == null || SelectedLPChannels.Count == 0)
                {
                    MessageBox.Show("Please Select Quantities To be Added in Load Profile Channels");
                    return;
                }
                SelectedLPChannels.Sort((x, y) => x.OBIS_Index.CompareTo(y.OBIS_Index));

                // Add Quantities In Display Windows Table
                foreach (var item in SelectedLPChannels)
                {
                    Configs.LoadProfileChannelsRow NewRow = (Configs.LoadProfileChannelsRow)configs.LoadProfileChannels.NewRow();
                    NewRow.LoadProfileGroupId = SelectedRow.lp_channels_group_id;

                    string Quantity_Name = string.Empty;
                    if (!item.IsLabelNull())
                        Quantity_Name = item.Label;

                    if (String.IsNullOrEmpty(Quantity_Name))
                        Quantity_Name = configs.AllQuantities.FindByDefault_OBIS_Code(item.OBIS_Index).Label;
                    NewRow.Label = Quantity_Name;

                    NewRow.QuantityIndex = item.OBIS_Index;
                    configs.LoadProfileChannels.AddLoadProfileChannelsRow(NewRow);
                }

                dgv_load_profile_channels.Refresh();
                MessageBox.Show("Successful",
                                                         "Selected Load Profile Channels Added Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void removeLoadProfileChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Configs.ConfigurationRow SelectedRow = (SelectedConfigurationRows != null
                    && SelectedConfigurationRows.Count > 0) ? SelectedConfigurationRows[0] : null;

                List<Configs.AllQuantitiesRow> SelectedLPChannels = SelectedAllQuantityRows;
                List<Configs.LoadProfileChannelsRow> SelectedLPRows = SelectedLoadProfileChannels;

                ///Remove Rows Selected from Load Profile Channels
                if (SelectedLPRows.Count > 0)
                {
                    SelectedLPChannels.Clear();
                    foreach (var item in SelectedLPRows)
                    {
                        SelectedLPChannels.Add(configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == item.QuantityIndex));
                    }
                }

                if (SelectedRow == null)
                {
                    MessageBox.Show("Please Select Configuration To Remove Load Profile Channels");
                    return;
                }
                if (SelectedLPChannels == null || SelectedLPChannels.Count == 0)
                {
                    MessageBox.Show("Please Select Quantities To be removed in Load Profile Channels");
                    return;
                }

                DialogResult result = MessageBox.Show(this, "Are you sure,want to remove selected load profile channels", "Confirm Delete",
                                                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result != DialogResult.OK)
                    return;
                // Remove Quantities From Display Windows Table
                List<Configs.AllQuantitiesRow> LP_ChannelsRemove = new List<Configs.AllQuantitiesRow>();
                foreach (var item in SelectedLPChannels)
                {
                    try
                    {
                        DataRow[] win = configs.LoadProfileChannels.Select(String.Format(
                            "[LoadProfileGroupId] = {0} and [QuantityIndex] = {1}", SelectedRow.LoadProfile_GroupRow.id, item.OBIS_Index));
                        if (win != null && win.Length > 0)
                        {
                            foreach (var winItem in win)
                            {
                                winItem.Delete();
                                /// configs.LoadProfileChannels.RemoveLoadProfileChannelsRow((Configs.LoadProfileChannelsRow)winItem);
                            }
                            LP_ChannelsRemove.Add(item);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                String removedLPChList = "";
                if (LP_ChannelsRemove.Count > 0)
                {
                    foreach (var item in LP_ChannelsRemove)
                    {
                        removedLPChList += String.Format("{0},", item.Label);
                    }
                    removedLPChList = removedLPChList.Trim(",".ToCharArray());
                }

                //Notification notifier = null;
                if (LP_ChannelsRemove.Count <= 0)
                    MessageBox.Show("Error",
                                                "Selected Load Profile Channels not removed");
                else
                    MessageBox.Show("Successful",
                                                String.Format("Selected Load Profile Channels removed Successfully \r\n{0}", removedLPChList));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void ReAssignLabelLoadProfileChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Configs.LoadProfileChannelsRow SelectedRow = (SelectedLoadProfileChannels != null &&
                                                              SelectedLoadProfileChannels.Count > 0) ?
                                                              SelectedLoadProfileChannels[0] : null;

                List<Configs.LoadProfileChannelsRow> SelectedLoadProfieChannel = new List<Configs.LoadProfileChannelsRow>(50);

                if (SelectedRow == null)
                {
                    foreach (Configs.LoadProfileChannelsRow item in configs.LoadProfileChannels.Rows)
                    {
                        SelectedLoadProfieChannel.Add(item);
                    }
                    // MessageBox.Show("Please Select Configuration To Add Windows");
                    // return;
                }
                else
                {
                    SelectedLoadProfieChannel.Add(SelectedRow);
                }

                if (SelectedLoadProfieChannel == null || SelectedLoadProfieChannel.Count == 0)
                {
                    MessageBox.Show("Please Select Load Profile Channel Rows");
                    return;
                }

                // Add Quantities In DisplayWindows Table
                foreach (var item in SelectedLoadProfieChannel)
                {
                    string LPChannelName = string.Empty;

                    if (String.IsNullOrEmpty(LPChannelName))
                        LPChannelName = configs.AllQuantities.FindByDefault_OBIS_Code(item.QuantityIndex).Label;

                    if (String.IsNullOrEmpty(LPChannelName))
                        LPChannelName = "unknown";

                    item.Label = LPChannelName;
                    // configs.DisplayWindows.AddDisplayWindowsRow(NewRow);
                }

                this.dgv_load_profile_channels.Refresh();

                MessageBox.Show("Successful",
                                                         "Selected Load Profile Channel Labels Altered Successfully");
                // MessageBox.Show(this, "Selected Load Profile Channel Labels Altered Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Monthly Billing Event Handlers

        /// <summary>
        /// To Change the Filter On Billing Item Quantity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_BillingItems_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region make filter based on BillingItemId

                DataGridViewRow currentRow = dgv_billing_items.Rows[e.RowIndex];
                Configs.BillingItemsRow SelectedBillingItem = (Configs.BillingItemsRow)((DataRowView)currentRow.DataBoundItem).Row;

                if (this.bSrc_bill_tariff_quantity != null && currentRow != null)
                {
                    this.bSrc_bill_tariff_quantity.Filter = String.Format("[BillItemId] = '{0}'", SelectedBillingItem.id);
                }
                else if (this.bSrc_bill_tariff_quantity != null)
                {
                    this.bSrc_bill_tariff_quantity.Filter = null;
                }

                #endregion
            }
            catch
            {
                if (this.bSrc_bill_tariff_quantity != null)
                {
                    this.bSrc_bill_tariff_quantity.Filter = null;
                }
            }
        }

        private void dgv_BillTariffQuantity_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            try
            {
                Configs.BillTariffQuantityRow SelectedBillingQuantityIteml = (Configs.BillTariffQuantityRow)configs.BillTariffQuantity[e.RowIndex];
                Configs.BillingItemsRow SelectedBillItem = null;
                Configs.AllQuantitiesRow ChannelWindowLabel = null;
                if (SelectedBillingQuantityIteml != null)
                {
                    ChannelWindowLabel = (Configs.AllQuantitiesRow)configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == SelectedBillingQuantityIteml.OBIS_Index);
                    SelectedBillItem = (Configs.BillingItemsRow)configs.BillingItems.FindByid(SelectedBillingQuantityIteml.BillItemId);
                }
                String ToolTip = String.Format("{0} ,{1} ,{2}", SelectedBillItem.Label, SelectedBillingQuantityIteml.SequenceId, ChannelWindowLabel.Label);
                e.ToolTipText = ToolTip;
            }
            catch (Exception ex)
            {
                e.ToolTipText = "!";
            }
        }

        private void dgv_BillTariffQuantity_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            //try
            //{
            //    if (dgv_BillTariffQuantity["ItemName", e.RowIndex].ColumnIndex == e.ColumnIndex)
            //    {
            //        DataGridViewRow CurrentRow = (DataGridViewRow)dgv_BillTariffQuantity.Rows[e.RowIndex];
            //        Configs.BillTariffQuantityRow DataRow = null;
            //        if (CurrentRow.DataBoundItem != null)
            //        {
            //            DataRow = (Configs.BillTariffQuantityRow)((DataRowView)CurrentRow.DataBoundItem).Row;
            //            e.Value = configs.AllQuantities.FindByOBIS_Index(DataRow.OBIS_Index).Label;
            //        }
            //        ///DisplayWindowLabel(e.RowIndex);
            //    }
            //}
            //catch (Exception)
            //{
            //    e.Value = "!";
            //}
        }


        private void addBillingItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Configs.ConfigurationRow SelectedRow = (SelectedConfigurationRows != null && SelectedConfigurationRows.Count > 0) ?
                                                        SelectedConfigurationRows[0] : null;

                List<Configs.AllQuantitiesRow> SelectedBillingItems = SelectedAllQuantityRows;
                if (SelectedRow == null)
                {
                    MessageBox.Show("Please Select Configuration To Add Billing Item");
                    return;
                }
                if (SelectedBillingItems == null || SelectedBillingItems.Count == 0)
                {
                    MessageBox.Show("Please Select Quantities To be Added in Billing Item");
                    return;
                }
                SelectedBillingItems.Sort((x, y) => x.OBIS_Index.CompareTo(y.OBIS_Index));

                // make New Billing Item
                Configs.BillingItemsRow NewBillItem = (Configs.BillingItemsRow)configs.BillingItems.NewRow();
                NewBillItem.BillItemGroupId = SelectedRow.BillItemsGroupId;
                NewBillItem.Label = (SelectedBillingItems != null && SelectedBillingItems.Count > 0) ? SelectedBillingItems[0].Label : "New Item";
                configs.BillingItems.AddBillingItemsRow(NewBillItem);

                int sequenceId = 1;
                // Add Quantities In Display Windows Table
                foreach (var item in SelectedBillingItems)
                {
                    Configs.BillTariffQuantityRow NewRow = (Configs.BillTariffQuantityRow)configs.BillTariffQuantity.NewRow();
                    NewRow.BillItemId = NewBillItem.id;
                    NewRow.OBIS_Index = item.OBIS_Index;
                    NewRow.SequenceId = sequenceId++;
                    configs.BillTariffQuantity.AddBillTariffQuantityRow(NewRow);
                }

                // Refresh Billing Grid
                foreach (var item in dgv_billing_items.Rows)
                {
                    ((DataGridViewRow)item).Selected = false;
                }
                if (dgv_billing_items.Rows.Count > 0)
                {
                    dgv_billing_items.Rows[dgv_billing_items.Rows.Count - 1].Selected = true;
                    dgv_billing_items.Refresh();
                    dgv_bill_tariff_quantity.Refresh();
                }
                MessageBox.Show(this, "Selected Billing Items Added Successfully", "Successful",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void removeBillingItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Configs.ConfigurationRow SelectedRow = (SelectedConfigurationRows != null
                    && SelectedConfigurationRows.Count > 0) ? SelectedConfigurationRows[0] : null;

                List<Configs.BillingItemsRow> SelectedBillItemRows = SelectedBillItemsRows;

                if (SelectedRow == null)
                {
                    MessageBox.Show("Please Select Configuration to remove Bill Items");
                    return;
                }
                if (SelectedBillItemRows == null || SelectedBillItemRows.Count == 0)
                {
                    MessageBox.Show("Please Select BillingItems to be removed");
                    return;
                }

                DialogResult result = MessageBox.Show(this, "Are you sure,want to remove Billing Items",
                                                      "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result != DialogResult.OK)
                    return;
                int removedRow_Count = 0;

                // Remove Quantities 
                foreach (var item in SelectedBillItemRows)
                {
                    try
                    {
                        DataRow[] win = configs.BillingItems.Select(String.Format(
                            "[id] = {0} and [BillItemGroupId] = {1}", item.id, SelectedRow.BillingItem_GroupRow.id));

                        if (win != null && win.Length > 0)
                        {
                            foreach (var winItem in win)
                            {
                                winItem.Delete();
                                // configs.BillingItems.RemoveBillingItemsRow((Configs.BillingItemsRow)winItem);
                                removedRow_Count++;
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                if (removedRow_Count <= 0)
                    MessageBox.Show(this, "Selected Billing Items not removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this, String.Format("Selected Billing Items removed Successfully"),
                                          "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Event Info Event Handlers

        private void dgv_EventLog_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            //try
            //{
            //    try
            //    {
            //        DataGridViewRow CurrentRow = (DataGridViewRow)dgv_EventLog.Rows[e.RowIndex];
            //        Configs.EventLogsRow DataRow = null;
            //        if (CurrentRow.DataBoundItem != null)
            //        {
            //            DataRow = (Configs.EventLogsRow)((DataRowView)CurrentRow.DataBoundItem).Row;
            //        }

            //        if (dgv_EventLog["EventName", e.RowIndex].ColumnIndex == e.ColumnIndex)
            //        {
            //            Configs.EventInfoRow EventInfo = configs.EventInfo.Single<Configs.EventInfoRow>((x) => x.id == DataRow.id &&
            //                x.EventGroupId == DataRow.EventInfoRow.EventGroupId);
            //            e.Value = EventInfo.Label;
            //            ///DisplayWindowLabel(e.RowIndex);
            //        }
            //        else if (dgv_EventLog["EventLogIndexName", e.RowIndex].ColumnIndex == e.ColumnIndex)
            //        {
            //            Configs.AllQuantitiesRow OBIS_Row = null;
            //            OBIS_Row = (Configs.AllQuantitiesRow)configs.AllQuantities.FindByOBIS_Index(DataRow.EventLogIndex);
            //            e.Value = OBIS_Row.Label;
            //        }
            //        else if (dgv_EventLog["EventCounterName", e.RowIndex].ColumnIndex == e.ColumnIndex)
            //        {
            //            Configs.AllQuantitiesRow OBIS_Row = null;
            //            OBIS_Row = (Configs.AllQuantitiesRow)configs.AllQuantities.FindByOBIS_Index(DataRow.EventCounterIndex);
            //            e.Value = OBIS_Row.Label;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        e.Value = "!";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    e.Value = "!";
            //}
        }

        #endregion

        #region Configurations_EventHandlers

        private void dgv_Configuration_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.dgv_configuration_new.Rows[e.RowIndex].Selected = true;

                Configs.ConfigurationRow SelectedConfigRow = (SelectedConfigurationRows != null &&
                    SelectedConfigurationRows.Count > 0) ? SelectedConfigurationRows[0] : null;
                if (SelectedConfigRow == null)
                    return;
                else
                {
                    configs.Configuration.CurrentConfiguration = SelectedConfigRow;
                }
                #region make filter based on BillingItemId

                /// Apply Current Configuration Filter On Billing Data Table
                if (this.bSrc_billing_items != null && SelectedConfigRow != null)
                {
                    this.bSrc_billing_items.Filter = String.Format("[BillItemGroupId] = '{0}'", SelectedConfigRow.BillItemsGroupId);
                }
                else if (this.bSrc_billing_items != null)
                {
                    this.bSrc_billing_items.Filter = null;
                }

                #endregion
                #region make filter based on LoadProfileChannels

                /// Apply Current Configuration Filter On Billing Data Table
                if (this.bSrc_load_profile_channels != null && SelectedConfigRow != null)
                {
                    this.bSrc_load_profile_channels.Filter = String.Format("[LoadProfileGroupId] = '{0}'",
                                                                                 SelectedConfigRow.lp_channels_group_id);
                }
                else if (this.bSrc_load_profile_channels != null)
                {
                    this.bSrc_load_profile_channels.Filter = null;
                }

                #endregion
                #region make filter based on Display Windows

                /// Apply Current Configuration Filter On Billing Data Table
                if (this.bSrc_display_windows != null && SelectedConfigRow != null)
                {
                    this.bSrc_display_windows.Filter = String.Format("[DisplayWindowsGroupId] = '{0}'",
                                                                            SelectedConfigRow.DisplayWindowGroupId);
                }
                else if (this.bSrc_display_windows != null)
                {
                    this.bSrc_display_windows.Filter = null;
                }

                #endregion
                #region make filter based on Event Info

                /// Apply Current Configuration Filter On Billing Data Table
                if (this.bSrc_event_info != null && SelectedConfigRow != null)
                {
                    this.bSrc_event_info.Filter = String.Format("[EventGroupId] = '{0}'", SelectedConfigRow.EventGroupId);
                }
                else if (this.bSrc_event_info != null)
                {
                    this.bSrc_event_info.Filter = null;
                }

                #endregion
                #region make filter based on Event Log Info

                /// Apply Current Configuration Filter On Billing Data Table
                if (this.bSrc_event_logs != null && SelectedConfigRow != null)
                {
                    this.bSrc_event_logs.Filter = String.Format("[EventGroupId] = '{0}'", SelectedConfigRow.EventGroupId);
                }
                else if (this.bSrc_event_logs != null)
                {
                    this.bSrc_event_logs.Filter = null;
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (this.bSrc_billing_items != null)
                {
                    this.bSrc_billing_items.Filter = null;
                }
                if (this.bSrc_load_profile_channels != null)
                {
                    this.bSrc_load_profile_channels.Filter = null;
                }
                if (this.bSrc_display_windows != null)
                {
                    this.bSrc_display_windows.Filter = null;
                }
                if (this.bSrc_event_info != null)
                {
                    this.bSrc_event_info.Filter = null;
                }
                if (this.bSrc_event_logs != null)
                {
                    this.bSrc_event_logs.Filter = null;
                }
            }
            finally
            {
                this.btn_TabPage_ClickAction(this.btnTabs_Dictionary[lbl_Header.Text]);
            }
        }

        private void copyConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Configs.ConfigurationRow> SelectedConfigs = (SelectedConfigurationRows != null && SelectedConfigurationRows.Count > 0) ?
                                                                  SelectedConfigurationRows : null;

                if (SelectedConfigs == null || SelectedConfigs.Count < 1)
                {
                    MessageBox.Show("Please Select Source Configurations to be copied");
                    return;
                }

                // Comment by sajid: TODO Later

                //ConfigsHelper Configurator = new ConfigsHelper(configs);

                //Configs.ConfigurationRow Config_OutRow = null;
                //Configurator.CopyMeterConfiguration(SelectedConfigs[0], out Config_OutRow, 1);
            }
            catch
            {
                MessageBox.Show(this, "Error Copying Selected Configurations", "Error", MessageBoxButtons.OK);
            }
        }


        private void deleteConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Configs.ConfigurationRow> SelectedConfigs = SelectedConfigurationRows; /// (SelectedConfigurationRows != null &&
                                                                                            /// SelectedConfigurationRows.Count > 0) ? SelectedConfigurationRows : null;

                if (SelectedConfigs == null || SelectedConfigs.Count <= 0)
                {
                    MessageBox.Show("Please Select Configurations to be deleted");
                    return;
                }

                //foreach (var config in SelectedConfigs)
                //{
                //    var Meter_ConfigRows = config.GetMeter_ConfigurationRows();
                //    config.Delete();
                //    /// configs.Configuration.RemoveConfigurationRow(config);
                //    if (Meter_ConfigRows != null && Meter_ConfigRows.Length > 0)
                //        foreach (var meter_config in Meter_ConfigRows)
                //        {
                //            /// Set ConfigId Null In Meter_ConfigRow
                //            meter_config.SetConfig_idNull();
                //        }
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error deleting Selected Configurations", "Error", MessageBoxButtons.OK);
            }
        }

        private void deleteCompleteConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                List<Configs.ConfigurationRow> SelectedConfigs = SelectedConfigurationRows;
                /// (SelectedConfigurationRows != null &&
                /// SelectedConfigurationRows.Count > 0) ? SelectedConfigurationRows : null;

                if (SelectedConfigs == null || SelectedConfigs.Count <= 0)
                {
                    MessageBox.Show("Please Select Configurations to be deleted");
                    return;
                }

                #region /// Warning Configuration Delete Process

                DialogResult result = MessageBox.Show("Are you sure want to delete complete Configuration Set?",
                                                               "Proceed Delete Process",
                                                                MessageBoxButtons.OKCancel);

                if (result != System.Windows.Forms.DialogResult.OK)
                    return;

                #endregion
                #region Remove All Selected Configurations

                foreach (var config_row in SelectedConfigs)
                {
                    /// Configuration Group Rows
                    var Event_Group = config_row.Events_GroupRow;
                    ///configs.Eve.Events_Group.Select(string.Format("id = {0}", config_row.id));
                    var LoadProfile_Group = config_row.LoadProfile_GroupRow;
                    ///configs.LoadProfile_Group.Select(string.Format("id = {0}", config_row.id));
                    var BillItem_Group = config_row.BillingItem_GroupRow;
                    /// configs.BillingItem_Group.Select(string.Format("id = {0}", config_row.id));
                    var displayWin_Group = config_row.DisplayWindows_GroupRow;
                    /// configs.DisplayWindows_Group.Select(string.Format("id = {0}", config_row.id));


                    //var Meter_ConfigRows =  config_row.GetMeter_ConfigurationRows();  

                    //config_row.Delete();
                    ///// configs.Configuration.RemoveConfigurationRow(config_row);

                    //if (Meter_ConfigRows != null && Meter_ConfigRows.Length > 0)
                    //    foreach (var meter_config in Meter_ConfigRows)
                    //    {
                    //        /// Set ConfigId Null In Meter_ConfigRow
                    //        meter_config.SetConfig_idNull();
                    //    }

                    Configs.ConfigurationRow[] All_Configs = null;

                    /// Delete All Group Data
                    /// Event Group
                    if (Event_Group != null)
                    {
                        All_Configs = Event_Group.GetConfigurationRows();
                        foreach (var local_Config in All_Configs)
                        {
                            if (local_Config != null)
                                local_Config.SetEventGroupIdNull();
                        }
                        Event_Group.Delete();
                        /// configs.Events_Group.RemoveEvents_GroupRow(Event_Group);
                    }

                    /// Billing Group
                    if (BillItem_Group != null)
                    {
                        All_Configs = BillItem_Group.GetConfigurationRows();
                        foreach (var local_Config in All_Configs)
                        {
                            if (local_Config != null)
                                local_Config.SetBillItemsGroupIdNull();
                        }
                        BillItem_Group.Delete();
                        /// configs.BillingItem_Group.RemoveBillingItem_GroupRow(BillItem_Group);
                    }

                    /// LoadProfile Group
                    if (LoadProfile_Group != null)
                    {
                        All_Configs = LoadProfile_Group.GetConfigurationRows();
                        foreach (var local_Config in All_Configs)
                        {
                            if (local_Config != null)
                                local_Config.Setlp_channels_group_idNull();
                        }
                        LoadProfile_Group.Delete();
                        /// configs.LoadProfile_Group.RemoveLoadProfile_GroupRow(LoadProfile_Group);
                    }

                    /// DisplayWindows Group
                    if (displayWin_Group != null)
                    {
                        All_Configs = displayWin_Group.GetConfigurationRows();
                        foreach (var local_Config in All_Configs)
                        {
                            if (local_Config != null)
                                local_Config.SetDisplayWindowGroupIdNull();
                        }
                        displayWin_Group.Delete();
                        /// configs.DisplayWindows_Group.RemoveDisplayWindows_GroupRow(displayWin_Group);
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error deleting Selected Configurations", "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region Meter_Configurations_EventHandlers

        // private void meter_configurationDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        // {
        //     try
        //     {
        //         #region make filter based on meter configuration id

        //         var _SelectedMeterConfigurationRows = SelectedMeterConfigurationRows;

        //         Configs.Meter_ConfigurationRow SelectedConfigRow = (_SelectedMeterConfigurationRows != null &&
        //             _SelectedMeterConfigurationRows.Count > 0) ? _SelectedMeterConfigurationRows[0] : null;

        //         if (SelectedConfigRow == null)
        //             return;
        //         else
        //         {

        //             /// Update Current Configuration Rows
        //             configs.Meter_Configuration.CurrentConfiguration = SelectedConfigRow;
        //             configs.Configuration.CurrentConfiguration = SelectedConfigRow.ConfigurationRow;
        //             configs.Meter_Info.CurrentMeterInfo = SelectedConfigRow.Meter_InfoRow;

        //         }

        //         /// Apply Current MeterConfiguration Filter On Configuration Data Table
        //         if (this.configurationBindingSource != null && SelectedConfigRow != null)
        //         {
        //             this.configurationBindingSource.Filter = String.Format("[Id] = '{0}'", SelectedConfigRow.Config_id);
        //             foreach (DataGridViewRow data_Row in this.dgv_Configuration.Rows)
        //             {
        //                 data_Row.Selected = false;
        //             }
        //             /// Change Selected Configuration Rows
        //             this.dgv_Configuration.Rows.OfType<DataGridViewRow>().Where(x => x.DataBoundItem != null &&
        //                                          ((Configs.ConfigurationRow)((System.Data.DataRowView)x.DataBoundItem).Row).id ==
        //                                          SelectedConfigRow.Config_id).ToArray<DataGridViewRow>()[0].Selected = true;

        //         }
        //         else if (this.configurationBindingSource != null)
        //         {
        //             this.configurationBindingSource.Filter = null;
        //         }

        //         #endregion
        //         #region make filter based on MeterInfo

        //         /// Apply Current Configuration Filter On Meter Info Data Table
        //         if (this.meterInfoBindingSource != null && SelectedConfigRow != null)
        //         {
        //             this.meterInfoBindingSource.Filter = null;
        //             /// this.meterInfoBindingSource.Filter = String.Format("[Id] = '{0}'", SelectedConfigRow.Meter_Info_id);
        //             foreach (DataGridViewRow data_Row in this.meterInfoDataGridView.Rows)
        //             {
        //                 data_Row.Selected = false;
        //             }
        //             /// Change Selected Configuration Rows
        //             this.meterInfoDataGridView.Rows.OfType<DataGridViewRow>().Where(x => x.DataBoundItem != null &&
        //                                             ((Configs.Meter_InfoRow)((System.Data.DataRowView)x.DataBoundItem).Row).id ==
        //                                              SelectedConfigRow.Meter_Info_id).ToArray<DataGridViewRow>()[0].Selected = true;
        //         }
        //         else if (this.meterInfoBindingSource != null)
        //         {
        //             this.meterInfoBindingSource.Filter = null;
        //         }

        //         /// Apply Current Configuration Filter On SAP' Info
        //         if (this.sAP_ConfigBindingSource != null && SelectedConfigRow != null)
        //         {
        //             this.sAP_ConfigBindingSource.Filter = String.Format("[Meter_Config_Id] = '{0}'", SelectedConfigRow.Meter_Info_id);
        //         }
        //         else if (this.sAP_ConfigBindingSource != null)
        //         {
        //             this.sAP_ConfigBindingSource.Filter = null;
        //         }

        //         #endregion

        //         /// Initialize Unbound Column
        //     }
        //     catch
        //     {
        //         if (this.meterInfoBindingSource != null)
        //         {
        //             this.meterInfoBindingSource.Filter = null;
        //         }
        //         if (this.configurationBindingSource != null)
        //         {
        //             this.configurationBindingSource.Filter = null;
        //         }
        //         if (this.sAP_ConfigBindingSource != null)
        //         {
        //             this.sAP_ConfigBindingSource.Filter = null;
        //         }
        //     }
        // }

        //// private void meter_configurationDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        //// {
        ////     try
        ////     {
        ////         DataGridViewRow CurrentRow = (DataGridViewRow)meterdgv_Configuration.Rows[e.RowIndex];
        ////         Configs.Meter_ConfigurationRow DataRow = null;
        ////         DataRow = (Configs.Meter_ConfigurationRow)((DataRowView)CurrentRow.DataBoundItem).Row;

        ////         /// StOBISCode OBISCode = (Get_Index)DataRow.OBIS_Index;
        ////         if (meterdgv_Configuration["cnf_MeterModel", e.RowIndex].ColumnIndex == e.ColumnIndex)
        ////         {
        ////             e.Value = (DataRow.IsMeter_Info_idNull()) ? "!" : DataRow.Meter_InfoRow.Model_Name;
        ////             /// OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
        ////         }
        ////         else if (meterdgv_Configuration["Config_Name", e.RowIndex].ColumnIndex == e.ColumnIndex)
        ////         {

        ////             e.Value = (DataRow.IsConfig_idNull()) ? "!" : DataRow.c.Name;

        ////         }
        ////     }
        ////     catch
        ////     {
        ////         if (e != null)
        ////             e.Value = "!";
        ////     }
        //// }

        //// private void meter_configurationDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //// {
        ////     DataGridViewRow CurrentRow = null;
        ////     DataGridViewCell CurrentCell = null;

        ////     DataGridViewCell ConfigNameCell = null;
        ////     DataGridViewCell MeterModelNameCell = null;

        ////     Configs.Meter_ConfigurationRow DataRow = null;
        ////     try
        ////     {
        ////         CurrentRow = (DataGridViewRow)meterdgv_Configuration.Rows[e.RowIndex];
        ////         CurrentCell = (DataGridViewCell)meterdgv_Configuration[e.ColumnIndex, e.RowIndex];

        ////         MeterModelNameCell = (DataGridViewCell)meterdgv_Configuration["cnf_MeterModel", e.RowIndex];
        ////         ConfigNameCell = (DataGridViewCell)meterdgv_Configuration["Config_Name", e.RowIndex];

        ////         #region ConfigNameCell

        ////         if (CurrentCell == ConfigNameCell)
        ////         {
        ////             DataRow = (Configs.Meter_ConfigurationRow)((DataRowView)CurrentRow.DataBoundItem).Row;
        ////             ConfigNameCell.Value = (DataRow.IsConfig_idNull()) ? "!" : DataRow.ConfigurationRow.Name;
        ////         }

        ////         #endregion
        ////         #region MeterModelNameCell

        ////         else if (CurrentCell == MeterModelNameCell)
        ////         {
        ////             DataRow = (Configs.Meter_ConfigurationRow)((DataRowView)CurrentRow.DataBoundItem).Row;
        ////             MeterModelNameCell.Value = (DataRow.IsMeter_Info_idNull()) ? "!" : DataRow.Meter_InfoRow.Model_Name;
        ////         }

        ////         #endregion

        ////     }
        ////     catch (Exception ex)
        ////     {
        ////         if (CurrentCell != null)
        ////             CurrentCell.Value = "!";
        ////     }
        //// }

        // private void copy_meter_ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        // {
        //     try
        //     {
        //         var _SelectedMeterConfigurationRows = SelectedMeterConfigurationRows;

        //         List<Configs.Meter_ConfigurationRow> SelectedConfigs =
        //         (_SelectedMeterConfigurationRows != null && _SelectedMeterConfigurationRows.Count > 0) ? _SelectedMeterConfigurationRows : null;

        //         if (SelectedConfigs == null)
        //         {
        //             MessageBox.Show("Please Select Source and destination Meter_Configurations to be copied");
        //             return;
        //         }

        //         ConfigsHelper Configurator = new ConfigsHelper(configs);

        //         Configs.Meter_ConfigurationRow SelectedConfigs_1 = null;
        //         Configurator.CopyMeter_Configuration(SelectedConfigs[0], out SelectedConfigs_1);

        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show(this, "Error Copying Selected Configurations", "Error", MessageBoxButtons.OK);
        //     }
        // }

        // private void delete_meter_ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        // {
        //     try
        //     {
        //         List<Configs.Meter_ConfigurationRow> SelectedConfigs = SelectedMeterConfigurationRows;

        //         if (SelectedConfigs == null || SelectedConfigs.Count <= 0)
        //         {
        //             MessageBox.Show("Please Select Meter Configurations to be deleted");
        //             return;
        //         }

        //         #region /// Warning Configuration Delete Process

        //         DialogResult result = MessageBox.Show("Are you sure want to delete complete Meter_Configuration Set?",
        //                                               "Proceed Delete Process",
        //                                               MessageBoxButtons.OKCancel);

        //         if (result != System.Windows.Forms.DialogResult.OK)
        //             return;

        //         #endregion
        //         #region Remove All Selected Meter_Configurations

        //         foreach (var config_row in SelectedConfigs)
        //         {
        //             /// Configuration Group Rows
        //             var SapConfigRows = config_row.GetSap_ConfigRows();

        //             if (SapConfigRows != null && SapConfigRows.Length > 0)
        //                 foreach (var SapConfig in SapConfigRows)
        //                 {
        //                     SapConfig.Delete();
        //                     /// configs.Sap_Config.RemoveSap_ConfigRow(SapConfig);
        //                 }

        //             config_row.Delete();
        //             /// configs.Meter_Configuration.RemoveMeter_ConfigurationRow(config_row);
        //         }

        //         #endregion
        //     }
        //     catch
        //     {
        //         MessageBox.Show(this, "Error deleting Selected Configurations", "Error", MessageBoxButtons.OK);
        //     }
        // }

        #endregion

        #region MeterInfo Id

        //private void meterTypeInfoDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        #region make filter based on Meter Info

        //        Configs.Meter_InfoRow SelectedMeterInfo = (Configs.Meter_InfoRow)configs.Meter_Info[e.RowIndex];
        //        Configs.Meter_ConfigurationRow[] meter_Config = (Configs.Meter_ConfigurationRow[])configs.Meter_Configuration.Select(String.Format("[Meter_Info_Id] = '{0}'", SelectedMeterInfo.id), "id");

        //        /// Apply Current Configuration Filter On SAP' Info
        //        if (this.sAP_ConfigBindingSource != null && meter_Config != null)
        //        {
        //            this.sAP_ConfigBindingSource.Filter = String.Format("[Meter_Config_Id] = '{0}'", meter_Config[0].id);
        //        }
        //        else if (this.sAP_ConfigBindingSource.Filter != null && meter_Config == null)
        //        {
        //            this.billingItemsBindingSource.Filter = null;
        //        }

        //        #endregion
        //    }
        //    catch
        //    {
        //        if (this.sAP_ConfigBindingSource.Filter != null)
        //        {
        //            this.sAP_ConfigBindingSource.Filter = null;
        //        }
        //    }
        //}

        private void copyMeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //List<Configs.Meter_InfoRow> SelectedMetersInfoRows = (SelectedMeterInfoRows != null && SelectedMeterInfoRows.Count > 0) ? SelectedMeterInfoRows : null;
                //if (SelectedMetersInfoRows == null || SelectedMetersInfoRows.Count != 2)
                //{
                //    MessageBox.Show("Please Select Source and destination meters Configurations to be copied");
                //    return;
                //}
                //throw new NotImplementedException("Not Implemented.");

                /// ConfigsHelper Configurator = new ConfigsHelper(configs);
                /// Configurator.CopyMeter(SelectedMetersInfoRows[0].dlms_version,
                ///         SelectedMetersInfoRows[1].dlms_version,);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Copying Selected Meters", ex);
            }
        }

        private void deleteMeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //List<Configs.Meter_InfoRow> SelectedMetersInfoRows = (SelectedMeterInfoRows != null && SelectedMeterInfoRows.Count > 0) ? SelectedMeterInfoRows : null;
                //if (SelectedMetersInfoRows == null || SelectedMetersInfoRows.Count <= 0)
                //{
                //    MessageBox.Show("Please Select meter to delete");
                //    return;
                //}
                //foreach (var MeterInfoRow in SelectedMeterInfoRows)
                //{
                //    MeterInfoRow.Delete();
                //    /// configs.Meter_Info.RemoveMeter_InfoRow(MeterInfoRow);
                //}

            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Selected Meter", ex);
            }
        }

        #endregion

        #region DataGridView Filtering Event Handlers

        private void dgv_Manufacturer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region make filter based on ManufacturerId
                //if (e.RowIndex < 0) return;
                //DataGridViewRow currentRow = ManufacturerDataGridView.Rows[e.RowIndex];
                //Configs.ManufacturerRow SelectedManufacturerItem = (Configs.ManufacturerRow)((DataRowView)currentRow.DataBoundItem).Row;

                //if (this.DevicebindingSource != null && currentRow != null)
                //{
                //    this.DevicebindingSource.Filter = String.Format("[Manufacturer_Id] = '{0}'", SelectedManufacturerItem.id);
                //}
                //else if (this.DevicebindingSource != null)
                //{
                //    this.DevicebindingSource.Filter = null;
                //}

                #endregion
            }
            catch
            {
                if (this.bSrc_device != null)
                {
                    this.bSrc_device.Filter = null;
                }
            }
        }

        private void dgv_Device_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region make filter based on DeviceId
                if (e.RowIndex < 0) return;

                DataGridViewRow currentRow = dgv_device.Rows[e.RowIndex];

                string selectedDeviceId = currentRow.Cells[0].Value.ToString();
                string selectedDeviceName = currentRow.Cells[1].Value.ToString();

                Configs.DeviceRow SelectedDeviceItem = (Configs.DeviceRow)((DataRowView)currentRow.DataBoundItem).Row;

                lbl_DeviceId.Text = SelectedDeviceItem.id.ToString();
                lbl_DeviceName.Text = SelectedDeviceItem.Device_Name;

                if (this.bSrc_device_association != null && currentRow != null)
                {
                    this.bSrc_device_association.Filter = String.Format("[Device_Id] = '{0}'", SelectedDeviceItem.id);
                }
                else if (this.bSrc_device_association != null)
                {
                    this.bSrc_device_association.Filter = null;
                }

                if (this.bSrc_obis_details != null && currentRow != null)
                {
                    this.bSrc_obis_details.Filter = String.Format("[Device_Id] = '{0}'", SelectedDeviceItem.id);
                }
                else if (this.bSrc_obis_details != null)
                {
                    this.bSrc_obis_details.Filter = null;
                }

                if (this.bSrc_capture_objects != null && currentRow != null)
                {
                    this.bSrc_capture_objects.Filter = String.Format("[DeviceId] = '{0}'", SelectedDeviceItem.id);
                }
                else if (this.bSrc_capture_objects != null)
                {
                    this.bSrc_capture_objects.Filter = null;
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (this.bSrc_device_association != null)
                {
                    this.bSrc_device_association.Filter = null;
                }
                if (this.bSrc_obis_details != null)
                {
                    this.bSrc_obis_details.Filter = null;
                }
                if (this.bSrc_capture_objects != null)
                {
                    this.bSrc_capture_objects.Filter = null;
                }
            }
            finally
            {
                this.btn_TabPage_ClickAction(this.btnTabs_Dictionary[lbl_Header.Text]);
            }
        }

        private void dgv_DeviceAssociation_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region make filter based on DeviceId

                if (e.RowIndex < 0) return;
                DataGridViewRow currentRow = dgv_device_association.Rows[e.RowIndex];
                Configs.Device_AssociationRow SelectedDeviceAssociation = (Configs.Device_AssociationRow)((DataRowView)currentRow.DataBoundItem).Row;

                if (this.bSrc_configuration_new != null && currentRow != null)
                {
                    this.bSrc_configuration_new.Filter = String.Format("[Configuration_Id] = '{0}'", SelectedDeviceAssociation.Configuration_Id);
                }
                else if (this.bSrc_configuration_new != null)
                {
                    this.bSrc_configuration_new.Filter = null;
                }

                #endregion
            }
            catch
            {
                if (this.bSrc_configuration_new != null)
                {
                    this.bSrc_configuration_new.Filter = null;
                }
            }
        }

        private void dgvOBISLabels_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            // try
            // {
            //     #region make filter based on LabelId
            //     if (e.RowIndex < 0) return;
            //     DataGridViewRow currentRow = dgvOBISLabels.Rows[e.RowIndex];
            //     Configs.OBIS_LabelsRow SelectedOBISLabelsItem = (Configs.OBIS_LabelsRow)((DataRowView)currentRow.DataBoundItem).Row;

            //     if (this.OBISDetailsBindingSource != null && currentRow != null)
            //     {
            //         this.OBISDetailsBindingSource.Filter = String.Format("[Obis_Label_Id] = '{0}'", SelectedOBISLabelsItem.id);
            //     }
            //     else if (this.OBISDetailsBindingSource != null)
            //     {
            //         this.OBISDetailsBindingSource.Filter = null;
            //     }

            //     #endregion
            // }
            // catch
            // {
            //     if (this.OBISDetailsBindingSource != null)
            //     {
            //         this.OBISDetailsBindingSource.Filter = null;
            //     }
            // }
        }

        #endregion // DataGridView Filtering Event Handlers

        private void dgv_ObisDetails_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            //try
            //{
            //    if (e.RowIndex < 0) return;
            //    DataGridViewRow CurrentRow = (DataGridViewRow)dgv_ObisDetails.Rows[e.RowIndex];
            //    Configs.OBIS_DetailsRow DataRow = null;
            //    DataRow = (Configs.OBIS_DetailsRow)((DataRowView)CurrentRow.DataBoundItem).Row;
            //    StOBISCode OBISCode = (Get_Index)DataRow.Obis_Code;
            //    if (dgv_ObisDetails["OBIS_Quantity", e.RowIndex].ColumnIndex == e.ColumnIndex)
            //    {
            //        e.Value = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (e != null)
            //        e.Value = "!";
            //}
        }
        private void dgv_ObisRightsGroup_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region make filter based on DeviceId
                if (e.RowIndex < 0) return;
                DataGridViewRow currentRow = dgv_obis_rights_group.Rows[e.RowIndex];
                Configs.Obis_Rights_GroupRow SelectedObisRightGroupItem = (Configs.Obis_Rights_GroupRow)((DataRowView)currentRow.DataBoundItem).Row;

                if (this.bSrc_obis_details != null && currentRow != null)
                {
                    this.bSrc_obis_details.Filter = String.Format("[Rights_Group_Id] = '{0}'", SelectedObisRightGroupItem.id);
                }
                else if (this.bSrc_obis_details != null)
                {
                    this.bSrc_obis_details.Filter = null;
                }

                #endregion
            }
            catch
            {
                if (this.bSrc_obis_details != null)
                {
                    this.bSrc_obis_details.Filter = null;
                }
            }
        }
        private void searchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_obis_details.ClearSelection();

                FindOBISCodeDialog OBISFinder = new FindOBISCodeDialog();
                Configs.OBIS_DetailsRow CurrentRow = (SelectedOBISDetailRows != null &&
                    SelectedOBISDetailRows.Count > 0) ? SelectedOBISDetailRows[0] : configs.OBIS_Details[0];
                StOBISCode OBISCode = (Get_Index)CurrentRow.Obis_Code;
                // Populate OBIS_Finder
                // OBISFinder.Get_Index_Name = OBISCode.OBISIndex.ToString();
                // OBISFinder.Label = CurrentRow.Label;
                OBISFinder.OBISCodeStr = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                OBISFinder.OBISId = OBISCode.OBIS_Value.ToString();
                OBISFinder.ShowDialog(this);
                if (OBISFinder.DialogResult == DialogResult.Cancel)
                    return;
                //foreach (DataGridViewRow OBISFinderRow in allQuantitiesDataGridView.Rows)
                //{
                //    OBISFinderRow.Selected = false;
                //}
                #region ///Search By OBIS Index
                ulong OBISIndexNum = 0;
                if (ulong.TryParse(OBISFinder.OBISId, out OBISIndexNum))
                {
                    foreach (DataGridViewRow OBISFinderRow in dgv_obis_details.Rows)
                    {
                        Configs.OBIS_DetailsRow OBISIndexRow =
                            (Configs.OBIS_DetailsRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                        if (OBISIndexRow.Obis_Code == OBISIndexNum)
                        {
                            OBISFinderRow.Selected = true;
                            dgv_obis_details.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
                            return;
                        }
                    }
                }
                #endregion
                #region // Search By OBIS Code
                Regex OBISValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);
                if (OBISValidator.IsMatch(OBISFinder.OBISCodeStr))
                {
                    // Evaluate the OBIS Code String For Values
                    String OBISString = OBISFinder.OBISCodeStr;
                    Match OBISStrs = OBISValidator.Match(OBISString);

                    foreach (DataGridViewRow OBISFinderRow in dgv_obis_details.Rows)
                    {
                        Configs.OBIS_DetailsRow OBISIndexRow = null;
                        if (OBISFinderRow != null && OBISFinderRow.DataBoundItem != null)
                        {
                            OBISIndexRow = (Configs.OBIS_DetailsRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;

                            // Compute Each OBIS Feilds Separatly
                            ulong OBISVal = (ulong)OBISIndexRow.Obis_Code;
                            StOBISCode OBIS = (Get_Index)OBISVal;
                            String OBISStr = OBIS.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                            Match OBISToMatchStrs = OBISValidator.Match(OBISStr);
                            bool IsMatched = true;

                            // Compare OBIS Codes
                            if (OBISStrs.Groups["ClassId"].Success)
                            {
                                string ClassId = OBISStrs.Groups["ClassId"].Value;
                                string ClassIdTOMatch = OBISToMatchStrs.Groups["ClassId"].Value;
                                if (!ClassId.Equals(ClassIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldA"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldA"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldA"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldB"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldB"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldB"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldC"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldC"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldC"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldD"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldD"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldD"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldE"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldE"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldE"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (OBISStrs.Groups["FieldF"].Success)
                            {
                                string Feild = OBISStrs.Groups["FieldF"].Value;
                                string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldF"].Value;
                                if (!Feild.Equals(FeildIdTOMatch))
                                {
                                    IsMatched = false;
                                    continue;
                                }
                                else
                                    IsMatched = true;
                            }
                            if (IsMatched)
                            {
                                OBISFinderRow.Selected = true;
                                dgv_obis_details.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
                            }
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error Searching OBIS Text", "Error Search");
            }
        }
        void SearchFromOBISDetails()
        {
            try
            {
                dgv_obis_details.ClearSelection();

                this.OBISCodeStr = this.tb_ObisIndexInDetails.Text;//OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                this.OBISId = this.tb_ObisIndexInDetails.Text;

                this.Update_GUI("", this.tb_ObisIndexInDetails.Text);

                #region ///Search By OBIS Index
                if (this.ObisCodeMode == OBISCodeMode.ObisIndex)
                {
                    ulong OBISIndexNum = 0;
                    if (ulong.TryParse(this.OBISId, out OBISIndexNum))
                    {
                        foreach (DataGridViewRow OBISFinderRow in dgv_obis_details.Rows)
                        {
                            Configs.OBIS_DetailsRow OBISIndexRow =
                                (Configs.OBIS_DetailsRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                            if (OBISIndexRow.Obis_Code == OBISIndexNum)
                            {
                                OBISFinderRow.Selected = true;
                                dgv_obis_details.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
                                return;
                            }
                        }
                    }
                }
                #endregion

                #region // Search By OBIS Code
                else if (this.ObisCodeMode == OBISCodeMode.ObisCode)
                {
                    Regex OBISValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);
                    if (OBISValidator.IsMatch(this.OBISCodeStr))
                    {
                        // Evaluate the OBIS Code String For Values
                        String OBISString = this.OBISCodeStr;
                        Match OBISStrs = OBISValidator.Match(OBISString);

                        foreach (DataGridViewRow OBISFinderRow in dgv_obis_details.Rows)
                        {
                            Configs.OBIS_DetailsRow OBISIndexRow = null;
                            if (OBISFinderRow != null && OBISFinderRow.DataBoundItem != null)
                            {
                                OBISIndexRow = (Configs.OBIS_DetailsRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;

                                // Compute Each OBIS Feilds Separatly
                                ulong OBISVal = (ulong)OBISIndexRow.Obis_Code;
                                StOBISCode OBIS = (Get_Index)OBISVal;
                                String OBISStr = OBIS.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                                Match OBISToMatchStrs = OBISValidator.Match(OBISStr);
                                bool IsMatched = true;

                                // Compare OBIS Codes
                                if (OBISStrs.Groups["ClassId"].Success)
                                {
                                    string ClassId = OBISStrs.Groups["ClassId"].Value;
                                    string ClassIdTOMatch = OBISToMatchStrs.Groups["ClassId"].Value;
                                    if (!ClassId.Equals(ClassIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldA"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldA"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldA"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldB"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldB"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldB"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldC"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldC"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldC"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldD"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldD"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldD"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldE"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldE"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldE"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (OBISStrs.Groups["FieldF"].Success)
                                {
                                    string Feild = OBISStrs.Groups["FieldF"].Value;
                                    string FeildIdTOMatch = OBISToMatchStrs.Groups["FieldF"].Value;
                                    if (!Feild.Equals(FeildIdTOMatch))
                                    {
                                        IsMatched = false;
                                        continue;
                                    }
                                    else
                                        IsMatched = true;
                                }
                                if (IsMatched)
                                {
                                    OBISFinderRow.Selected = true;
                                    dgv_obis_details.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
                                }
                            }
                        }


                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error Searching OBIS Text", "Error Search");
            }
        }
        private void dgv_EventLog_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // try
            // {
            //     Configs.EventLogsRow SelectedEvents = (SelectedEventLogsRows != null && SelectedEventLogsRows.Count > 0) ? SelectedEventLogsRows[0] : null;
            //     if (SelectedEventLogsRows == null)
            //         return;

            //     #region make filter based on Event Log Info

            //     /// Apply Current Configuration Filter On Billing Data Table
            //     if (this.eventLogsBindingSource != null && SelectedEvents != null)
            //     {
            //         this.OBISLabelsBindingSource.Filter = String.Format("[Label_Name] like '%Event%'");
            //         ((DataGridViewComboBoxColumn)dgv_EventLog.Columns["EvenlLogIndex"]).DataSource = this.OBISLabelsBindingSource;
            //     }
            //     else if (this.eventLogsBindingSource != null)
            //     {
            //         this.eventLogsBindingSource.Filter = null;
            //     }

            //     #endregion
            // }
            // catch (Exception ex)
            // {
            //     if (this.eventLogsBindingSource != null)
            //     {
            //         this.eventLogsBindingSource.Filter = null;
            //     }
            // }
            // finally
            // {
            //     this.OBISLabelsBindingSource.Filter = string.Empty;
            // }
        }
        private void dgv_ObisDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow CurrentRow = (DataGridViewRow)dgv_obis_details.Rows[e.RowIndex];
            if (CurrentRow.Cells[2].Value != null)
                this.tb_ObisCode.Text = CurrentRow.Cells[2].Value.ToString();
            if (CurrentRow.Cells[1].Value != null)
                this.tb_ObisIndex.Text = CurrentRow.Cells[1].Value.ToString();
        }
        private void btn_Convert_Click(object sender, EventArgs e)
        {
            string OBISCode        = this.tb_ObisCode.Text;
            this.tb_ObisIndex.Text = StOBISCode.ConvertFrom(OBISCode).OBIS_Value.ToString();
        }
        private void copyBillingItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CopyBillingItems copyBillingItems = new CopyBillingItems(configs);
                copyBillingItems.ShowDialog(this);
                if (copyBillingItems.DialogResult == DialogResult.Cancel)
                    return;

                Configs.BillingItem_GroupRow billingGroupRow = null;
                List<Configs.BillingItemsRow> billingItems = configs.BillingItems.GetBillingItemsByGroup(copyBillingItems.CopyGroup);
                //int id = ((Configs.BillingItemsRow)configs.BillingItems.Rows[configs.BillingItems.Rows.Count - 1]).id + 1;
                if (copyBillingItems.IsExistingGroup)
                {

                    List<Configs.BillingItemsRow> existingBillingItems = configs.BillingItems.GetBillingItemsByGroup(copyBillingItems.ExistingGroup);
                    this.configs.BillTariffQuantity.RemoveBillingItemsByGroup(existingBillingItems);
                    configs.BillingItems.RemoveBillingItemsByGroup(existingBillingItems);
                    billingGroupRow = copyBillingItems.ExistingGroup;
                }
                else
                {
                    if (!string.IsNullOrEmpty(copyBillingItems.GroupName))
                    {
                        Configs.BillingItem_GroupRow itemGroupRow = (Configs.BillingItem_GroupRow)configs.BillingItem_Group.Rows[configs.BillingItem_Group.Rows.Count - 1];
                        var desRow = configs.BillingItem_Group.NewRow();
                        Configs.BillingItem_GroupRow newRow = (Configs.BillingItem_GroupRow)desRow;

                        newRow.id = itemGroupRow.id + 1;
                        newRow.BillingItem_Group_Name = copyBillingItems.GroupName;

                        configs.BillingItem_Group.AddBillingItem_GroupRow(newRow);
                        billingGroupRow = newRow;

                    }
                }
                configs.SaveConfigurationData(Properties.Settings.Default.MDC_ConnectionString);
                int autoValue = configs.BillingItems.GetMaxId();
                autoValue = autoValue + 1;
                foreach (Configs.BillingItemsRow billingRow in billingItems)
                {
                    configs.BillingItems.CopyBillingItemsByGroup(billingRow, billingGroupRow, autoValue);

                    List<Configs.BillTariffQuantityRow> billTariffQuantityRows = configs.BillTariffQuantity.GetBillingTariffQuanitityByBillingItem(billingRow);

                    foreach (Configs.BillTariffQuantityRow billTariffRow in billTariffQuantityRows)
                    {
                        var desRow = configs.BillTariffQuantity.NewRow();
                        desRow.ItemArray = billTariffRow.ItemArray.Clone() as object[];
                        Configs.BillTariffQuantityRow newRow = (Configs.BillTariffQuantityRow)desRow;

                        newRow.BillItemId = autoValue;
                        configs.BillTariffQuantity.AddBillTariffQuantityRow(newRow);
                    }

                    autoValue++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void copyEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CopyEvents copyEvents = new CopyEvents(configs);
                copyEvents.ShowDialog(this);
                if (copyEvents.DialogResult == DialogResult.Cancel)
                    return;

                Configs.Events_GroupRow eventGroupRow = null;
                List<Configs.EventInfoRow> events = configs.EventInfo.GetEventsByGroup(copyEvents.CopyGroup);
                if (copyEvents.IsExistingGroup)
                {

                    List<Configs.EventInfoRow> existingevents = configs.EventInfo.GetEventsByGroup(copyEvents.ExistingGroup);
                    this.configs.EventLogs.RemoveEventLogsByGroup(existingevents);
                    configs.EventInfo.RemoveEventsByGroup(existingevents);
                    eventGroupRow = copyEvents.ExistingGroup;

                }
                else
                {
                    if (!string.IsNullOrEmpty(copyEvents.GroupName))
                    {
                        int newGroupId = configs.Events_Group.GetMaxId();
                        var desRow = configs.Events_Group.NewRow();
                        Configs.Events_GroupRow newRow = (Configs.Events_GroupRow)desRow;

                        newRow.id = newGroupId + 1;
                        newRow.Events_group_Name = copyEvents.GroupName;

                        configs.Events_Group.AddEvents_GroupRow(newRow);
                        eventGroupRow = newRow;

                    }
                }
                configs.SaveConfigurationData(Properties.Settings.Default.MDC_ConnectionString);

                int autoValue = configs.EventInfo.GetMaxId();
                autoValue = autoValue + 1;

                foreach (Configs.EventInfoRow eventRow in events)
                {
                    var eventInfoRow = configs.EventInfo.NewRow();
                    eventInfoRow.ItemArray = eventRow.ItemArray.Clone() as object[];
                    Configs.EventInfoRow eventInfoNewRow = (Configs.EventInfoRow)eventInfoRow;

                    eventInfoNewRow.id = autoValue;
                    eventInfoNewRow.Events_GroupRow = eventGroupRow;
                    configs.EventInfo.AddEventInfoRow(eventInfoNewRow);

                    //configs.EventInfo.CopyEventsByGroup(eventRow, eventGroupRow, id);

                    List<Configs.EventLogsRow> eventLogRows = configs.EventLogs.GetEventLogsByEventInfo(eventRow);

                    foreach (Configs.EventLogsRow eventLogRow in eventLogRows)
                    {
                        var desRow = configs.EventLogs.NewRow();
                        desRow.ItemArray = eventLogRow.ItemArray.Clone() as object[];
                        Configs.EventLogsRow newRow = (Configs.EventLogsRow)desRow;

                        newRow.id = autoValue;
                        newRow.Events_GroupRow = eventGroupRow;
                        configs.EventLogs.AddEventLogsRow(newRow);
                    }

                    autoValue++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DisableGUI();
                List<Configs.AllQuantitiesRow> rows = new List<Configs.AllQuantitiesRow>();
                foreach (object itemChecked in this.cbl_capture_objects.CheckedItems)
                {
                    Configs.AllQuantitiesRow selectedRow = (Configs.AllQuantitiesRow)itemChecked;
                    rows.Add(selectedRow);
                }

                int deviceId = 0;
                if (this.cmb_Devices.SelectedItem != null)
                    deviceId = Convert.ToInt32(((DataRowView)this.cmb_Devices.SelectedItem)["id"].ToString());

                decimal targetOBISCode = Convert.ToDecimal(((CatagoryInfo)this.cmb_Catagory.SelectedItem).OBIS_Code);

                if (cb_newGroup.Checked && !string.IsNullOrEmpty(this.txtNewGroupId.Text))
                {
                    long groupId = Convert.ToInt64(this.txtNewGroupId.Text);
                    this.InsertCaptureObjects(rows, deviceId, targetOBISCode, groupId);

                    CatagoryInfo catagory = (CatagoryInfo)this.cmb_Catagory.SelectedItem;
                    this.cmb_BillingGroups.Items.Clear();
                    this.GetGroupIds(catagory.OBIS_Code);

                    MessageBox.Show("Capture Objects are save successfully in Local Dataset");
                }
                else if (!cb_newGroup.Checked)
                {
                    this.DeleteAndSaveCaptureObjects(rows, deviceId, targetOBISCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                EnableGUI();
            }

        }
        private void DeleteAndSaveCaptureObjects(List<Configs.AllQuantitiesRow> rows, int deviceId, decimal targetOBISCode)
        {
            if (this.cmb_BillingGroups.SelectedIndex != -1)
            {
                long groupId = Convert.ToInt64((string)this.cmb_BillingGroups.SelectedItem);
                ulong targetCode = Convert.ToUInt64(((CatagoryInfo)this.cmb_Catagory.SelectedItem).OBIS_Code);
                this.DeleteCaptureObjects(deviceId, groupId, targetCode);
                //configs.SaveConfigurationData(Connection);
                this.InsertCaptureObjects(rows, deviceId, targetOBISCode, groupId);
                MessageBox.Show("Capture Objects are save successfully in Local Dataset");
            }
            else
            {
                MessageBox.Show("There is no Group Selected");
            }
        }
        private void DeleteCaptureObjects(int deviceId, long groupId, ulong targetCode)
        {
            List<Configs.CaptureObjectsRow> captureObjectList = configs.CaptureObjects.GetCaptureObjectByProfile(deviceId, targetCode, groupId);

            foreach (Configs.CaptureObjectsRow row in captureObjectList)
            {
                row.Delete();

            }
        }
        void DisableGUI()
        {
            this.cmb_BillingGroups.Enabled = false;
            this.cmb_Catagory.Enabled = false;
            this.cmb_Devices.Enabled = false;
            this.cb_newGroup.Enabled = false;
            this.cbl_capture_objects.Enabled = false;
            this.btnSaveCaptureObjects.Enabled = false;
            this.txtNewGroupId.Enabled = false;
            this.btnCheckAll.Enabled = false;
            this.btnUncheckAll.Enabled = false;
            this.btnDeleteGroupData.Enabled = false;

        }
        void EnableGUI()
        {
            this.cmb_BillingGroups.Enabled = true;
            this.cmb_Catagory.Enabled = true;
            this.cmb_Devices.Enabled = true;
            this.cb_newGroup.Enabled = true;
            this.cbl_capture_objects.Enabled = true;
            this.btnSaveCaptureObjects.Enabled = true;
            this.txtNewGroupId.Enabled = true;
            this.btnCheckAll.Enabled = true;
            this.btnUncheckAll.Enabled = true;
            this.btnDeleteGroupData.Enabled = true;
        }
        private void InsertCaptureObjects(List<Configs.AllQuantitiesRow> rows, int deviceId, decimal targetOBISCode, long groupId)
        {
            int squenceId = 1;
            if (this.cmb_Catagory.SelectedIndex == 0)
            {
                foreach (Configs.AllQuantitiesRow row in rows)
                {
                    CaptureObjectInfo coInfo = this._allLoadProfileItemsList.Find(x => x.OBIS_Index == row.OBIS_Index);
                    this.AddCaptureObjectInTable(deviceId, targetOBISCode, groupId, squenceId, row, coInfo);
                    squenceId++;
                }
            }
            else if (this.cmb_Catagory.SelectedIndex == 1)
            {
                foreach (Configs.AllQuantitiesRow row in rows)
                {
                    CaptureObjectInfo coInfo = this._allBillingItemsList.Find(x => x.OBIS_Index == row.OBIS_Index);
                    this.AddCaptureObjectInTable(deviceId, targetOBISCode, groupId, squenceId, row, coInfo);
                    squenceId++;
                }
            }
        }
        private void AddCaptureObjectInTable(int deviceId, decimal targetOBISCode, long groupId, int squenceId, Configs.AllQuantitiesRow row, CaptureObjectInfo coInfo)
        {
            Configs.CaptureObjectsRow coRow = configs.CaptureObjects.NewCaptureObjectsRow();
            coRow.OBIS_Index = row.OBIS_Index;
            coRow.SequenceId = squenceId;
            coRow.DeviceId = deviceId;
            coRow.DataIndex = 0;
            coRow.AttributeNo = (byte)coInfo.AttributeNo;
            coRow.GroupId = groupId;
            coRow.Target_OBIS_Index = targetOBISCode;
            configs.CaptureObjects.AddCaptureObjectsRow(coRow);
        }
        private void cmbCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            CatagoryInfo catagory = (CatagoryInfo)this.cmb_Catagory.SelectedItem;
            if (this.cmb_Catagory.SelectedIndex == 1)
            {
                ((ListBox)this.cbl_capture_objects).DataSource = null;
                ((ListBox)this.cbl_capture_objects).DataSource = _listBillingItemsAllQuantities;
            }
            else if (this.cmb_Catagory.SelectedIndex == 0)
            {
                ((ListBox)this.cbl_capture_objects).DataSource = null;
                ((ListBox)this.cbl_capture_objects).DataSource = _listLoadProfileAllQuantities;
            }
            ((ListBox)this.cbl_capture_objects).DisplayMember = "Label";
            ((ListBox)this.cbl_capture_objects).ValueMember = "OBIS_Index";

            this.cmb_BillingGroups.Items.Clear();
            if (catagory != null)
                this.GetGroupIds(catagory.OBIS_Code);

        }
        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateGUI();
        }
        private void UpdateGUI()
        {
            try
            {
                this.UnCheckAll();

                int deviceId = 0;
                if (this.cmb_Devices.SelectedItem != null)
                    deviceId = Convert.ToInt32(((DataRowView)this.cmb_Devices.SelectedItem)["id"].ToString());
                long groupId = 0;
                if (!string.IsNullOrEmpty(this.cmb_BillingGroups.Text))
                    groupId = Convert.ToInt64(this.cmb_BillingGroups.Text);
                CatagoryInfo catInfo = null;
                if (this.cmb_Catagory.SelectedItem != null)
                    catInfo = (CatagoryInfo)this.cmb_Catagory.SelectedItem;
                if (deviceId > 0 && catInfo != null && groupId > 0)
                {
                    this.CheckSavedItems(deviceId, groupId, catInfo);
                }
            }
            catch
            {
            }

        }
        private void CheckSavedItems(int deviceId, long groupId, CatagoryInfo catInfo)
        {
            DataTable tblFiltered = null;
            DataRow[] tblData = configs.CaptureObjects.Select(string.Format("Target_OBIS_Index = {0} and DeviceId = {1} and GroupId = {2}",
                                                                  catInfo.OBIS_Code, deviceId, groupId));
            if (tblData != null && tblData.Length > 0)
            {
                tblFiltered = tblData.CopyToDataTable();
                for (int i = 0; i < tblFiltered.Rows.Count; i++)
                {
                    for (int j = 0; j < this.cbl_capture_objects.Items.Count; j++)
                    {
                        if (tblFiltered.Rows[i]["OBIS_Index"].ToString().Equals(((Configs.AllQuantitiesRow)this.cbl_capture_objects.Items[j]).OBIS_Index.ToString()))
                        {
                            this.cbl_capture_objects.SetItemChecked(j, true);
                            break;
                        }
                    }
                }
            }
        }
        private void UnCheckAll()
        {
            foreach (int i in cbl_capture_objects.CheckedIndices)
            {
                cbl_capture_objects.SetItemCheckState(i, CheckState.Unchecked);
            }
        }
        private void cmbBillingGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateGUI();
            this.cb_newGroup.Checked = false;
        }
        private void cb_newGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_newGroup.Checked)
            {
                this.lblNewGroupId.Visible = true;
                this.txtNewGroupId.Visible = true;
            }
            else
            {
                this.lblNewGroupId.Visible = false;
                this.txtNewGroupId.Visible = false;
                this.txtNewGroupId.Text = "";
            }
        }
        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_capture_objects.Items.Count; i++)
            {
                cbl_capture_objects.SetItemCheckState(i, CheckState.Checked);
            }
        }
        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            this.UnCheckAll();
        }
        private void btnDeleteGroupData_Click(object sender, EventArgs e)
        {
            if (this.cmb_BillingGroups.SelectedIndex != -1)
            {
                int deviceId = 0;
                if (this.cmb_Devices.SelectedItem != null)
                    deviceId = Convert.ToInt32(((DataRowView)this.cmb_Devices.SelectedItem)["id"].ToString());

                long groupId = Convert.ToInt64((string)this.cmb_BillingGroups.SelectedItem);
                ulong targetCode = Convert.ToUInt64(((CatagoryInfo)this.cmb_Catagory.SelectedItem).OBIS_Code);
                this.DeleteCaptureObjects(deviceId, groupId, targetCode);

                CatagoryInfo catagory = (CatagoryInfo)this.cmb_Catagory.SelectedItem;
                this.cmb_BillingGroups.Items.Clear();
                this.GetGroupIds(catagory.OBIS_Code);

                //configs.SaveConfigurationData(Connection);
                MessageBox.Show("Capture Objects are delete successfully in Local Dataset");
            }
            else
            {
                MessageBox.Show("There is no Group Selected");
            }
        }

        private void btnInsertUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rbSearch.Checked)
                {
                    MessageBox.Show("You are in Search mode.");
                    return;
                }

                if (this.rdbOBISCode.Checked)
                {
                    this.ObisCodeMode = OBISCodeMode.ObisCode;
                }
                else if (this.rdbIndex.Checked)
                {
                    this.ObisCodeMode = OBISCodeMode.ObisIndex;
                }

                if (this.btnInsertUpdate.Text.Equals("Insert"))
                {
                    this.AddNewQuantityInAllQuantities();
                }
                else if (this.btnInsertUpdate.Text.Equals("Update"))
                {
                    this.UpdateQuantityInAllQuantities();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.btn_TabPage_ClickAction(this.btnTabs_Dictionary[lbl_Header.Text]);
            }

        }
        
        private void rbInsertUpdate_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (configs.AllQuantities.Count < 1 && this.rbSearch.Checked)
                {
                    this.rbInsertUpdate.Checked = true;
                    MessageBox.Show("Nothing to search.");
                    return;
                }

                Configs.AllQuantitiesRow CurrentRow = (SelectedAllQuantityRows != null && SelectedAllQuantityRows.Count > 0) ? SelectedAllQuantityRows[0] : this.AllQuantitiesRow_Dummy;
                StOBISCode OBISCode                 = (Get_Index)CurrentRow.OBIS_Index;

                if (this.rbInsertUpdate.Checked)
                {
                    this.bSrc_all_quantities.RemoveFilter();
                    this.tb_Label.Text            = (this.cb_Insert.Checked) ? "Dummy" : CurrentRow.Label;
                    this.tb_ObisIndexAllQuan.Text = (this.cb_Insert.Checked) ? "0:0.0.0.0.0.0" : OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                }
                else if (this.rbSearch.Checked)
                {
                    this.cb_Insert.Checked        = true;
                    this.btnInsertUpdate.Text     = "Insert";
                    this.tb_ObisIndexAllQuan.Text = (this.rdbOBISCode.Checked) ? OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode) : CurrentRow.OBIS_Index.ToString();
                    this.tb_Label.Text            = CurrentRow.Label;
                    this.Search_all_quantities_by_Label();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void btn_CancelSearch_Click(object sender, EventArgs e)
        {
            this.CancelInDetails();
        }

        void CancelInDetails()
        {
            this.tb_ObisIndexInDetails.Text = "0";
            this.rbtn_ObisCodeDetail.Checked = true;
            this.tb_ObisIndexInDetails.BackColor = Color.White;
            this.bSrc_obis_details.Filter = null;
        }

        private void btnSearchInDetails_Click(object sender, EventArgs e)
        {
            if (this.rbtn_ObisCodeDetail.Checked)
            {
                this.ObisCodeMode = OBISCodeMode.ObisCode;
            }
            else if (this.rbtn_ObisIndexDetail.Checked)
            {
                this.ObisCodeMode = OBISCodeMode.ObisIndex;
            }

            this.SearchFromOBISDetails();
            this.CancelInDetails();
        }

        void ConvertTextOnRadioButtonChangeInOBISDetails()
        {
            try
            {
                //tb_ObisIndexAllQuan.TextChanged -= tb_ObisIndex_TextChanged;
                string RawOBISSTR = tb_ObisIndexInDetails.Text;
                tb_ObisIndexInDetails.BackColor = Color.Red;

                // Empty OBIS_STR Value
                if (string.IsNullOrEmpty(RawOBISSTR))
                {
                    tb_ObisIndexInDetails.BackColor = Color.Red;
                    return;
                }

                if (ObisCodeMode == OBISCodeMode.ObisCode)
                {
                    this.OBISCodeStr = string.Empty;

                    string OBISString = RawOBISSTR;
                    ulong indexValue = Convert.ToUInt64(RawOBISSTR);
                    StOBISCode OBISCode = (Get_Index)indexValue;
                    tb_ObisIndexInDetails.Text = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                    if (OBISDecimalValidator.IsMatch(tb_ObisIndexInDetails.Text))
                    {
                        this.OBISCodeStr = OBISString;
                        tb_ObisIndexInDetails.BackColor = Color.White;
                    }
                }
                else if (ObisCodeMode == OBISCodeMode.ObisIndex)
                {
                    //this.OBISId = string.Empty;
                    string OBISString = RawOBISSTR;
                    ulong OBISIndexValue = 0;

                    if (ulong.TryParse(this.OBISCodeStr, out OBISIndexValue))
                    {
                        this.OBISId = OBISString;

                        StOBISCode ObisValidate = Get_Index.Dummy;
                        ObisValidate.OBIS_Value = OBISIndexValue;
                        this.tb_ObisIndexInDetails.Text = OBISIndexValue.ToString();
                        if (ObisValidate.ClassId > 0 && (ObisValidate.OBIS_Value & 0x0000FFFFFFFFFFFF) > 0)
                            tb_ObisIndexInDetails.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                this.tb_ObisIndexInDetails.Text = "";
            }
            finally
            {
                //tb_ObisIndexAllQuan.TextChanged += tb_ObisIndex_TextChanged;
            }
        }

        private void Update_all_quantities_LabelField()
        {
            try
            {
                Configs.AllQuantitiesRow CurrentRow = (SelectedAllQuantityRows != null && SelectedAllQuantityRows.Count > 0) ? SelectedAllQuantityRows[0] : configs.AllQuantities[0];

                if (CurrentRow != null)
                {
                    StOBISCode OBISCode = (Get_Index)CurrentRow.OBIS_Index;
                    this.tb_Label.Text  = CurrentRow.Label;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Search_all_quantities_by_OBIS()
        {
            try
            {
                if (!rbSearch.Checked) return;

                if (this.rdbIndex.Checked)
                {

                    this.bSrc_all_quantities.Filter = string.Format("CONVERT([OBIS_Index], 'System.String') like '%{0}%'", tb_ObisIndexAllQuan.Text.Trim());
                }
                else if (this.rdbOBISCode.Checked)
                {

                    this.bSrc_all_quantities.Filter = string.Format("Quantity_Code like '%{0}%'", tb_ObisIndexAllQuan.Text.Trim());
                }
                this.dgv_all_quantities.Refresh();
                //this.Update_all_quantities_LabelField();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void tb_ObisIndexAllQuan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.Search_all_quantities_by_OBIS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Search_all_quantities_by_Label()
        {
            try
            {
                if (!rbSearch.Checked) return;

                this.bSrc_all_quantities.Filter = string.Format("Label like '%{0}%'", tb_Label.Text.Trim());
                this.dgv_all_quantities.Refresh();
                //this.Update_all_quantities_ObisField();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Update_all_quantities_ObisField()
        {
            try
            {
                Configs.AllQuantitiesRow CurrentRow = (SelectedAllQuantityRows != null && SelectedAllQuantityRows.Count > 0) ? SelectedAllQuantityRows[0] : configs.AllQuantities[0];

                if (CurrentRow != null)
                {
                    StOBISCode OBISCode = (Get_Index)CurrentRow.OBIS_Index;
                    this.tb_ObisIndexAllQuan.Text = (this.rdbIndex.Checked) ? CurrentRow.OBIS_Index.ToString() : OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void tb_Label_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.Search_all_quantities_by_Label();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tb_ObisIndexInDetails_TextChanged(object sender, EventArgs e)
        {
            if (this.rbtn_ObisCodeDetail.Checked)
            {
                this.ObisCodeMode = OBISCodeMode.ObisCode;
            }
            else if (this.rbtn_ObisIndexDetail.Checked)
            {
                this.ObisCodeMode = OBISCodeMode.ObisIndex;
            }

            if (this.ObisCodeMode == OBISCodeMode.ObisIndex)
            {

                this.bSrc_obis_details.Filter = string.Format("CONVERT([Obis_Code], 'System.String') like '%{0}%'", this.tb_ObisIndexInDetails.Text.Trim());
                //dt.DefaultView.RowFilter = string.Format("CONVERT(OBIS_Index, 'System.String') like '%{0}%'", tb_ObisIndexAllQuan.Text.Trim());
                dgv_obis_details.Refresh();
            }
            else if (this.ObisCodeMode == OBISCodeMode.ObisCode)
            {

                this.bSrc_obis_details.Filter = string.Format("OBIS_Quantity like '%{0}%'", this.tb_ObisIndexInDetails.Text.Trim());
                //dt.DefaultView.RowFilter = string.Format("CONVERT(OBIS_Index, 'System.String') like '%{0}%'", tb_ObisIndexAllQuan.Text.Trim());
                dgv_obis_details.Refresh();
            }
        }

        private void Log_Message(string message, int ExecutedSteps = -1, int TotalSteps = -1)
        {
            try
            {
                int ExecSteps = 0;
                int TLSteps   = 0;
                if (ExecutedSteps > - 1) ExecSteps = ExecutedSteps;
                if (TotalSteps > -1)     TLSteps   = TotalSteps;
                int pVal = (100 * ExecSteps) / TotalSteps;
                pBar.Value = pVal;
                pBar.Refresh();

                this.lbl_Progress.Text = " [ " + pVal + "% ]";

                this.tb_Progress.AppendText(DateTime.Now + "    " + message + "\r\n");
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        private void Export_TableRowsGroup_ToExcel<T>(DataTable _DataTable, string colName, List<T> GroupIds)
        {
            // Creating a Excel object.
            Microsoft.Office.Interop.Excel._Application excel   = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook   = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.Worksheets[1];

            try
            {
                int GroupCount = GroupIds.Count;
                this.Log_Message("Exporting all . . .", 0, GroupCount);

                worksheet.Cells.NumberFormat = "@";// Set data type of entire sheet is "Text"

                worksheet.Name = _DataTable.TableName;

                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                // Loop to write header text in excel sheet.
                for (int i = 0; i < _DataTable.Columns.Count; i++)
                {
                    worksheet.Cells[cellRowIndex, cellColumnIndex] = _DataTable.Columns[i].Caption;
                    worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Bold = true;
                    cellColumnIndex++;
                }

                cellRowIndex = 2;
                cellColumnIndex = 1;


                // Loop to add extra sheet if datagridviews are more than one
                for (int index = 0; index < GroupCount; index++)
                {
                    this.Log_Message("Exporting . . . " + GroupIds[index], index + 1, GroupCount);

                    //Filter the table
                    DataTable customTable = _DataTable.AsEnumerable().Where(x => x.Field<T>(colName).Equals( GroupIds[index] )).CopyToDataTable();
                    //DataTable customTable = dgv_CaptureObjects.Rows.Where(x => x.Field<T>(colName).Equals(GroupIds[index])).CopyToDataTable();

                    //Loop to read value from each row.
                    for (int i = 0; i < customTable.Rows.Count; i++)
                    {
                        //Loop to read value from each column.
                        for (int j = 0; j < customTable.Columns.Count; j++)
                        {
                            if (customTable.Rows[i].ItemArray[j] != null)
                                worksheet.Cells[cellRowIndex, cellColumnIndex] = customTable.Rows[i].ItemArray[j].ToString();

                            cellColumnIndex++;
                        }
                        cellColumnIndex = 1;
                        cellRowIndex++;
                    }

                    // Set column width to its content
                    worksheet.Columns.AutoFit();
                }

                this.Log_Message("Exported all Groups successfully", GroupCount, GroupCount);

                //Getting the location and file name of the excel to save from user.
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Log_Message("Exporting data tables failed.", 0, 100);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }

        }

        private void ExportTableToExcel(List<string> tableNames)
        {
            // Creating a Excel object.
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.Worksheets[1];

            try
            {
                int tableCount = tableNames.Count;
                this.Log_Message("Exporting all . . .", 0, tableCount);

                // Loop to add extra sheet if datagridviews are more than one
                for (int index = 0; index < tableCount; index++)
                {
                    this.Log_Message("Exporting . . . " + this.configsTables_Dictionary[tableNames[index]], index + 1, tableCount);

                    worksheet.Cells.NumberFormat = "@";// Set data type of entire sheet is "Text"

                    ///worksheet.Name = configs.Tables[index].TableName;
                    worksheet.Name = tableNames[index];


                    int cellRowIndex = 1;
                    int cellColumnIndex = 1;

                    // Loop to write header text in excel sheet.
                    for (int i = 0; i < this.configsTables_Dictionary[tableNames[index]].Columns.Count; i++)
                    {
                        worksheet.Cells[cellRowIndex, cellColumnIndex] = this.configsTables_Dictionary[tableNames[index]].Columns[i].Caption;
                        worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Bold = true;
                        cellColumnIndex++;
                    }

                    cellRowIndex = 2;
                    cellColumnIndex = 1;

                    //Loop to read value from each row.
                    for (int i = 0; i < this.configsTables_Dictionary[tableNames[index]].Rows.Count; i++)
                    {
                        //Loop to read value from each column.
                        for (int j = 0; j < this.configsTables_Dictionary[tableNames[index]].Columns.Count; j++)
                        {
                            if (this.configsTables_Dictionary[tableNames[index]].Rows[i].ItemArray[j] != null)
                                worksheet.Cells[cellRowIndex, cellColumnIndex] = this.configsTables_Dictionary[tableNames[index]].Rows[i].ItemArray[j].ToString();

                            cellColumnIndex++;
                        }
                        cellColumnIndex = 1;
                        cellRowIndex++;
                    }

                    // Set column width to its content
                    worksheet.Columns.AutoFit();

                    // Add new excel sheet, if needed
                    if (index < tableNames.Count - 1)
                    {
                        worksheet = workbook.Worksheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
                    }
                }

                this.Log_Message("Exported all tables successfully", tableCount, tableCount);

                //Getting the location and file name of the excel to save from user.
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Log_Message("Exporting data tables failed.", 0, 100);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }

        }

        private void ExportTableToExcell(List<string> tableNames)
        {
            // Creating a Excel object.
            Microsoft.Office.Interop.Excel._Application excel   = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook   = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.Worksheets[1];

            try
            {
                int tableCount = tableNames.Count;
                this.Log_Message("Exporting all . . .", 0, tableCount);

                // Loop to add extra sheet if datagridviews are more than one
                for (int index = 0; index < tableCount; index++)
                {
                    this.Log_Message("Exporting . . . " + configs.Tables[tableNames[index]].TableName, index + 1, tableCount);
                    this.Log_Message("Exporting . . . " + this.configsTables_Dictionary[tableNames[index]], index + 1, tableCount);

                    worksheet.Cells.NumberFormat = "@";// Set data type of entire sheet is "Text"

                    ///worksheet.Name = configs.Tables[index].TableName;
                    worksheet.Name = tableNames[index];
                    

                    int cellRowIndex = 1;
                    int cellColumnIndex = 1;

                    // Loop to write header text in excel sheet.
                    for (int i = 0; i < configs.Tables[tableNames[index]].Columns.Count; i++)
                    {
                        worksheet.Cells[cellRowIndex, cellColumnIndex] = configs.Tables[tableNames[index]].Columns[i].Caption;
                        worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Bold = true;
                        cellColumnIndex++;
                    }

                    cellRowIndex = 2;
                    cellColumnIndex = 1;

                    //Loop to read value from each row.
                    for (int i = 0; i < configs.Tables[tableNames[index]].Rows.Count; i++)
                    {
                        //Loop to read value from each column.
                        for (int j = 0; j < configs.Tables[tableNames[index]].Columns.Count; j++)
                        {
                            if (configs.Tables[tableNames[index]].Rows[i].ItemArray[j] != null)
                                worksheet.Cells[cellRowIndex, cellColumnIndex] = configs.Tables[tableNames[index]].Rows[i].ItemArray[j].ToString();

                            cellColumnIndex++;
                        }
                        cellColumnIndex = 1;
                        cellRowIndex++;
                    }

                    // Set column width to its content
                    worksheet.Columns.AutoFit();

                    // Add new excel sheet, if needed
                    if (index < tableNames.Count - 1)
                    {
                        worksheet = workbook.Worksheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
                    }
                }

                this.Log_Message("Exported all tables successfully", tableCount, tableCount);

                //Getting the location and file name of the excel to save from user.
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Log_Message("Exporting data tables failed.", 0, 100);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }

        }

        #region COMMENTED ImportExportToExcel() Methods 
        /* 
        private void ExportToExcel(DataTable dt)
        {
            // Creating a Excel object.
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.Worksheets[1];

            try
            {
                worksheet.Cells.NumberFormat = "@";// Set data type of entire sheet is "Text"

                worksheet.Name = dt.TableName;

                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                // Loop to write header text in excel sheet.
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[cellRowIndex, cellColumnIndex] = dt.Columns[i].Caption;
                    worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Bold = true;
                    cellColumnIndex++;
                }

                cellRowIndex = 2;
                cellColumnIndex = 1;

                //Loop to read value from each row.
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Loop to read value from each column.
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i].ItemArray[j] != null)
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dt.Rows[i].ItemArray[j].ToString();

                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                // Set column width to its content
                worksheet.Columns.AutoFit();

                //Getting the location and file name of the excel to save from user.
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }

        }

        private void ExportToExcel(DataGridView[] dgv)
        {
            // Creating a Excel object.
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.Worksheets[1];

            try
            {
                // Loop to add extra sheet if datagridviews are more than one
                for (int index = 0; index < dgv.Length; index++)
                {
                    worksheet.Cells.NumberFormat = "@";// Set data type of entire sheet is "Text"

                    worksheet.Name = dgv[index].Name;

                    int cellRowIndex = 1;
                    int cellColumnIndex = 1;

                    // Loop to write header text in excel sheet.
                    for (int i = 0; i < dgv[index].Columns.Count; i++)
                    {
                        worksheet.Cells[cellRowIndex, cellColumnIndex] = dgv[index].Columns[i].HeaderText;
                        worksheet.Cells[cellRowIndex, cellColumnIndex].Font.Bold = true;
                        cellColumnIndex++;
                    }

                    cellRowIndex = 2;
                    cellColumnIndex = 1;

                    //Loop to read value from each row.
                    for (int i = 0; i < dgv[index].Rows.Count; i++)
                    {
                        //Loop to read value from each column.
                        for (int j = 0; j < dgv[index].Columns.Count; j++)
                        {
                            if (dgv[index].Rows[i].Cells[j].Value != null)
                                worksheet.Cells[cellRowIndex, cellColumnIndex] = dgv[index].Rows[i].Cells[j].Value.ToString();

                            cellColumnIndex++;
                        }
                        cellColumnIndex = 1;
                        cellRowIndex++;
                    }

                    // Set column width to its content
                    worksheet.Columns.AutoFit();

                    if (index < dgv.Length - 1)
                    {
                        worksheet = workbook.Worksheets.Add();
                    }
                }

                //Getting the location and file name of the excel to save from user.
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }

        }        

        private void ImportFromExcel(DataTable configTable, string sheetName)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*", ValidateNames = true })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                                                
                        String name = sheetName;
                        String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                        ofd.FileName +
                                        ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                        OleDbConnection con = new OleDbConnection(constr);
                        OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                        con.Open();

                        OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                        DataTable data = new DataTable();
                        sda.Fill(data);

                        if (Is_Validated_DataTable(data, configTable))
                        {
                            configUpdater.Update_configs(configs, data);
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception raised while importing data form excel.\r\n" + ex.Message);
            }

        }

        private void ImportFromExcell()
        {
            configUpdater = new ConfigUpdater(configs, _dbController);
            OleDbConnection con = null;
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*", ValidateNames = true })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                            ofd.FileName +
                                           ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                        List<string> SheetNamesList = this.GetExcelSheetNames(constring);
                        if (SheetNamesList.Count < 1) throw new Exception("No sheet has been found.");

                        frmPrompt Prompt = new frmPrompt(SheetNamesList);
                        Prompt.ShowDialog();

                        if (SheetNamesList.Count < 1) return;

                        for (int i = 0; i < SheetNamesList.Count; i++)
                        {
                            using (con = new OleDbConnection(constring))
                            {
                                this.Update_prgBar("Importing excel sheet: " + SheetNamesList[i], i + 1, SheetNamesList.Count);

                                if (!configs.Tables.Contains(SheetNamesList[i]))
                                    throw new Exception("Excel Sheet: " + SheetNamesList[i] + " doesn't match with any table of database.");

                                var dataTable = new DataTable();
                                string query = string.Format("SELECT * FROM [{0}]", SheetNamesList[i] + "$");   // append char $ to fill dataTable (to access excel sheet).
                                con.Open();
                                OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                                adapter.Fill(dataTable);

                                if (Is_Validated_DataTable(dataTable, configs.Tables[SheetNamesList[i]], SheetNamesList[i]))
                                {
                                    configUpdater.Update_configs_table[SheetNamesList[i]](dataTable);
                                }

                                con.Close();
                            }
                        }

                        this.Update_prgBar("Imported all excel sheets successfully. ", SheetNamesList.Count, SheetNamesList.Count);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception raised while importing data form excel.\r\n" + ex.Message);
                this.Update_prgBar("Importing excel sheets failed.", 0, 100);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

        }
        */
        #endregion

        private void ImportFromExcel()
        {
            configUpdater = new ConfigUpdater(configs, _dbController);
            OleDbConnection con = null;
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*", ValidateNames = true })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        String constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                            ofd.FileName +
                                           ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                        
                        List<string> SheetNamesList = this.GetExcelSheetNames(constring);
                        if (SheetNamesList.Count < 1) throw new Exception("No sheet has been found.");

                        frmPrompt Prompt = new frmPrompt(SheetNamesList);
                        Prompt.ShowDialog();

                        if (SheetNamesList.Count < 1) return;

                        for (int i = 0; i < SheetNamesList.Count; i++)
                        {
                            using (con = new OleDbConnection(constring))
                            {
                                this.Log_Message("Importing excel sheet: " + SheetNamesList[i], i + 1, SheetNamesList.Count);

                                if (!this.configsTables_Dictionary.ContainsKey(SheetNamesList[i]))
                                    throw new Exception("Excel Sheet: " + SheetNamesList[i] + " doesn't match with any table of database.");

                                var dataTable = new DataTable();
                                string query = string.Format("SELECT * FROM [{0}]", SheetNamesList[i] + "$");   // append char $ to fill dataTable (to access excel sheet).
                                con.Open();
                                OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                                adapter.Fill(dataTable);

                                if (Is_Validated_DataTable(dataTable, this.configsTables_Dictionary[SheetNamesList[i]], SheetNamesList[i]))
                                {
                                    configUpdater.Update_configs_table[SheetNamesList[i]](dataTable);
                                }

                                con.Close();
                            }
                        }

                        this.Log_Message("Imported all excel sheets successfully. ", SheetNamesList.Count, SheetNamesList.Count);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception raised while importing data form excel.\r\n" + ex.Message);
                this.Log_Message("Importing excel sheets failed.", 0, 100);
            }
            finally
            {
                if(con != null)
                    con.Close();
            }

        }

        private List<string> GetExcelSheetNames(string connectionString)
        {
            OleDbConnection con = null;
            DataTable dt = null;
            con = new OleDbConnection(connectionString);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            List<string> excelSheetNames = new List<string>();
            
            string sheetName = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                if (row["TABLE_NAME"].ToString().Contains("FilterDatabase"))
                    continue;

                if (row["TABLE_NAME"].ToString().Contains("$"))
                {
                    sheetName = row["TABLE_NAME"].ToString().Replace("$", string.Empty);
                }
                else
                {
                    sheetName = row["TABLE_NAME"].ToString();
                }

                if (sheetName != null)
                    excelSheetNames.Add(sheetName);
            }

            return excelSheetNames;
        }

        private bool Is_Validated_DataTable(DataTable excel_dt, DataTable config_dt, string sheetName)
        {
            bool IsValidated = false;
            
            try
            {
                if (config_dt.Columns.Count != excel_dt.Columns.Count)
                    throw new Exception($"Excel file [ {sheetName} ] => columns count doesn't match with the database.");

                for(int i = 0; i < config_dt.Columns.Count; i++)
                {
                    if (config_dt.Columns[i].Caption != excel_dt.Columns[i].Caption)
                        throw new Exception($"Excel file [ {sheetName} ] => Column[ {i+1} ] name mis-matched {Environment.NewLine} Received : {excel_dt.Columns[i].Caption} \t Existing : {config_dt.Columns[i].Caption}");
                }
                IsValidated = true;

                return IsValidated;
            }
            catch (Exception ex)
            {
                IsValidated = false;
                throw ex;
            }

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.btn_TestConnection.Text != "Disconnect")
                {
                    MessageBox.Show("Please make connection with your database.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btn_TestConnection.Focus();
                    this.toolTip.Show("Please click here to connect.", this.btn_TestConnection);
                    return;
                }

                this.LoadDbAndShow();
                this.Enabled = false;

                this.ImportFromExcel();
                this.btn_TabPage_ClickAction(this.btnTabs_Dictionary[lbl_Header.Text]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                
                List<string> tableNames = new List<string>();

                foreach (var item in this.btnTabs_Dictionary)
                {
                    tableNames.Add(item.Key);
                }
                /*
                for (int i = 0; i < configs.Tables.Count; i++)
                {
                    tableNames.Add(configs.Tables[i].TableName);
                }
                */
                frmPrompt Prompt = new frmPrompt(tableNames);
                Prompt.ShowDialog();
                if (tableNames.Count < 1) return;

                this.ExportTableToExcel(tableNames);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btn_TestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.btn_TestConnection.Text == "Disconnect")
                {
                    this.Close_DbConnection();
                    return;
                }

                active_database = (DataBaseTypes)Enum.Parse(typeof(DataBaseTypes), cb_dbType.SelectedItem.ToString());
                string Host     = this.cb_Host.Text;
                string Username = this.tb_username.Text;
                string Password = this.tb_Password.Text;
                string Schema   = (this.cb_Schema.Text == "") ? string.Empty : this.cb_Schema.Text.ToString();

                Properties.Settings.Default["Active_ConnectionString"] = (int)active_database;

                MySqlConnectionStringBuilder mySqlConStrBuilder = null;
                SqlConnectionStringBuilder sqlConStrBuilder = null;
                SQLiteConnectionStringBuilder sqliteConStrBuilder = null;

                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = "dlmsConfigurationTool.exe.config";
                Configuration _Config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                _Config.GetSection("connectionStrings");

                if (active_database == DataBaseTypes.MDC_DATABASE)
                {
                    //string act = _Config.SectionGroups["dlmsConfigurationTool.Properties.Settings"].SectionGroups["Active_ConnectionString"];
                    string MDC_ConnString       = Properties.Settings.Default.MDC_ConnectionString;
                    mySqlConStrBuilder          = new MySqlConnectionStringBuilder(MDC_ConnString);
                    mySqlConStrBuilder.Server   = Host;
                    mySqlConStrBuilder.UserID   = Username;
                    mySqlConStrBuilder.Password = Password;
                    mySqlConStrBuilder.Database = Schema;

                    _Config.ConnectionStrings.ConnectionStrings["dlmsConfigurationTool.Properties.Settings.MDC_ConnectionString"].ConnectionString 
                        = mySqlConStrBuilder.ConnectionString;

                    using (MySqlConnection conn = new MySqlConnection(mySqlConStrBuilder.ConnectionString))
                    {
                        Properties.Settings.Default["MDC_ConnectionString"] = conn.ConnectionString;
                        this._connectionString                              = conn.ConnectionString;
                    }
                }

                else if (active_database == DataBaseTypes.SCT_DATABASE)
                {
                    string SCT_ConnString           = Properties.Settings.Default.SCT_ConnectionString;
                    sqlConStrBuilder                = new SqlConnectionStringBuilder(SCT_ConnString);
                    sqlConStrBuilder.DataSource     = Host;
                    sqlConStrBuilder.UserID         = Username;
                    sqlConStrBuilder.Password       = Password;
                    sqlConStrBuilder.InitialCatalog = Schema;

                    _Config.ConnectionStrings.ConnectionStrings["dlmsConfigurationTool.Properties.Settings.SCT_ConnectionString"].ConnectionString 
                        = sqlConStrBuilder.ConnectionString;

                    using (SqlConnection conn = new SqlConnection(sqlConStrBuilder.ConnectionString))
                    {
                        Properties.Settings.Default["SCT_ConnectionString"] = conn.ConnectionString;
                        this._connectionString                              = conn.ConnectionString;
                    }
                }

                else if (active_database == DataBaseTypes.SEAC_DATABASE)
                {
                    _Config.ConnectionStrings.ConnectionStrings["dlmsConfigurationTool.Properties.Settings.SEAC_ConnectionString"].ConnectionString 
                        = Schema;   // Schema contains local db file path i.e used as connection string

                    //using (SQLiteConnection conn = new SQLiteConnection(sqliteConStrBuilder.ConnectionString))
                    {
                        Properties.Settings.Default["SEAC_ConnectionString"] = Schema;
                        this._connectionString                               = Schema;
                    }
                }

                _Config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("ConnectionStrings");
                this.UpdateAppSettings((int)active_database);
                this.Open_DbConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed. \r\n \r\n Detail: \r\n" + ex.Message);
            }
        }

        private void UpdateAppSettings(int newValue)
        {
            System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
            xml.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            System.Xml.XmlNode node;
            node = xml.SelectSingleNode("configuration/applicationSettings/dlmsConfigurationTool.Properties.Settings/setting[@name='Active_ConnectionString']");
            node.ChildNodes[0].InnerText = newValue.ToString();
            xml.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }

        private void HideTabControlHeaders(TabControl tbControl)
        {
            tbControl.ItemSize   = new Size(0, 1);
            tbControl.SizeMode   = TabSizeMode.Fixed;

            foreach (TabPage tab in tbControl.TabPages)
            {
                tab.Text = "";
            }
        }

        private void FindDGVsAndEnableDoubleBuffered(Control container)
        {
            foreach (Control item in container.Controls)
            {
                if (item is DataGridView)
                {
                    DataGridView dgv = (DataGridView)item;
                    dgv.DoubleBuffered(true);
                }
                if (item.HasChildren)
                {
                    this.FindDGVsAndEnableDoubleBuffered(item);
                }
            }
        }

        private void ApplicationConfiguration_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.FindDGVsAndEnableDoubleBuffered(this);

            this.HideTabControlHeaders(tb_DataViewer);

            this.cb_dbType.DataSource    = Enum.GetNames(typeof(DataBaseTypes));

            int ActiveConnStr_Index      = Properties.Settings.Default.Active_ConnectionString;
            this.cb_dbType.SelectedIndex = ActiveConnStr_Index - 1;     // comboBox has zero-based index

            this.Load_connectionString();
            this.Open_DbConnection();

            this.Init_btnTabsDictionary();
            this.Init_configsTablesDictionary();
            this.Init_BindingSourceDictionary();

            this.btn_all_quantities.PerformClick();
        }

        private void Init_btnTabsDictionary()
        {
            this.btnTabs_Dictionary = new Dictionary<string, Button>();

            this.btnTabs_Dictionary.Add("all_quantities"         , this.btn_all_quantities);
            this.btnTabs_Dictionary.Add("authentication_type"    , this.btn_authentication_type);
            this.btnTabs_Dictionary.Add("bill_tariff_quantity"   , this.btn_bill_tariff_quantity);
            this.btnTabs_Dictionary.Add("billing_items"          , this.btn_billing_items);
            this.btnTabs_Dictionary.Add("billingitem_group"      , this.btn_billingitem_group);
            this.btnTabs_Dictionary.Add("capture_objects"        , this.btn_capture_objects);
            this.btnTabs_Dictionary.Add("configuration_new"      , this.btn_configuration_new);
            this.btnTabs_Dictionary.Add("device"                 , this.btn_device);
            this.btnTabs_Dictionary.Add("device_association"     , this.btn_device_association);
            this.btnTabs_Dictionary.Add("display_windows"        , this.btn_display_windows);
            this.btnTabs_Dictionary.Add("displaywindows_group"   , this.btn_displaywindows_group);
            this.btnTabs_Dictionary.Add("event_info"             , this.btn_event_info);
            this.btnTabs_Dictionary.Add("event_logs"             , this.btn_event_logs);
            this.btnTabs_Dictionary.Add("events_group"           , this.btn_events_group);
            this.btnTabs_Dictionary.Add("load_profile_channels"  , this.btn_load_profile_channels);
            this.btnTabs_Dictionary.Add("loadprofile_group"      , this.btn_loadprofile_group);
            this.btnTabs_Dictionary.Add("manufacturer"           , this.btn_manufacturer);
            this.btnTabs_Dictionary.Add("obis_details"           , this.btn_obis_details);
            this.btnTabs_Dictionary.Add("obis_rights"            , this.btn_obis_rights);
            this.btnTabs_Dictionary.Add("obis_rights_group"      , this.btn_obis_rights_group);
            this.btnTabs_Dictionary.Add("rights"                 , this.btn_rights);
            this.btnTabs_Dictionary.Add("status_word"            , this.btn_status_word);
            this.btnTabs_Dictionary.Add("users"                  , this.btn_users);
        }

        private void Init_configsTablesDictionary()
        {
            this.configsTables_Dictionary = new Dictionary<string, DataTable>();

            this.configsTables_Dictionary.Add("all_quantities"        , this.configs.AllQuantities);
            this.configsTables_Dictionary.Add("authentication_type"   , this.configs.Authentication_Type);
            this.configsTables_Dictionary.Add("bill_tariff_quantity"  , this.configs.BillTariffQuantity);
            this.configsTables_Dictionary.Add("billing_items"         , this.configs.BillingItems);
            this.configsTables_Dictionary.Add("billingitem_group"     , this.configs.BillingItem_Group);
            this.configsTables_Dictionary.Add("capture_objects"       , this.configs.CaptureObjects);
            this.configsTables_Dictionary.Add("configuration_new"     , this.configs.Configuration);
            this.configsTables_Dictionary.Add("device"                , this.configs.Device);
            this.configsTables_Dictionary.Add("device_association"    , this.configs.Device_Association);
            this.configsTables_Dictionary.Add("display_windows"       , this.configs.DisplayWindows);
            this.configsTables_Dictionary.Add("displaywindows_group"  , this.configs.DisplayWindows_Group);
            this.configsTables_Dictionary.Add("event_info"            , this.configs.EventInfo);
            this.configsTables_Dictionary.Add("event_logs"            , this.configs.EventLogs);
            this.configsTables_Dictionary.Add("events_group"          , this.configs.Events_Group);
            this.configsTables_Dictionary.Add("load_profile_channels" , this.configs.LoadProfileChannels);
            this.configsTables_Dictionary.Add("loadprofile_group"     , this.configs.LoadProfile_Group);
            this.configsTables_Dictionary.Add("manufacturer"          , this.configs.Manufacturer);
            this.configsTables_Dictionary.Add("obis_details"          , this.configs.OBIS_Details);
            this.configsTables_Dictionary.Add("obis_rights"           , this.configs.OBIS_Rights);
            this.configsTables_Dictionary.Add("obis_rights_group"     , this.configs.Obis_Rights_Group);
            this.configsTables_Dictionary.Add("rights"                , this.configs.Rights);
            this.configsTables_Dictionary.Add("status_word"           , this.configs.Status_Word);
            this.configsTables_Dictionary.Add("users"                 , this.configs.users);
        }

        private void Init_BindingSourceDictionary()
        {
            this.bSrc_Dictionary = new Dictionary<string, BindingSource>();
            
            this.bSrc_Dictionary.Add("bSrc_all_quantities"         , this.bSrc_all_quantities);
            this.bSrc_Dictionary.Add("bSrc_authentication_type"    , this.bSrc_authentication_type);
            this.bSrc_Dictionary.Add("bSrc_bill_tariff_quantity"   , this.bSrc_bill_tariff_quantity);
            this.bSrc_Dictionary.Add("bSrc_billing_items"          , this.bSrc_billing_items);
            this.bSrc_Dictionary.Add("bSrc_billingitem_group"      , this.bSrc_billingitem_group);
            this.bSrc_Dictionary.Add("bSrc_capture_objects"        , this.bSrc_capture_objects);
            this.bSrc_Dictionary.Add("bSrc_configuration_new"      , this.bSrc_configuration_new);
            this.bSrc_Dictionary.Add("bSrc_device"                 , this.bSrc_device);
            this.bSrc_Dictionary.Add("bSrc_device_association"     , this.bSrc_device_association);
            this.bSrc_Dictionary.Add("bSrc_display_windows"        , this.bSrc_display_windows);
            this.bSrc_Dictionary.Add("bSrc_displaywindows_group"   , this.bSrc_displaywindows_group);
            this.bSrc_Dictionary.Add("bSrc_event_info"             , this.bSrc_event_info);
            this.bSrc_Dictionary.Add("bSrc_event_logs"             , this.bSrc_event_logs);
            this.bSrc_Dictionary.Add("bSrc_events_group"           , this.bSrc_events_group);
            this.bSrc_Dictionary.Add("bSrc_load_profile_channels"  , this.bSrc_load_profile_channels);
            this.bSrc_Dictionary.Add("bSrc_loadprofile_group"      , this.bSrc_loadprofile_group);
            this.bSrc_Dictionary.Add("bSrc_manufacturer"           , this.bSrc_manufacturer);
            this.bSrc_Dictionary.Add("bSrc_obis_details"           , this.bSrc_obis_details);
            this.bSrc_Dictionary.Add("bSrc_obis_rights"            , this.bSrc_obis_rights);
            this.bSrc_Dictionary.Add("bSrc_obis_rights_group"      , this.bSrc_obis_rights_group);
            this.bSrc_Dictionary.Add("bSrc_rights"                 , this.bSrc_rights);
            this.bSrc_Dictionary.Add("bSrc_status_word"            , this.bSrc_status_word);
            this.bSrc_Dictionary.Add("bSrc_users"                  , this.bSrc_users);
        }

        private void Load_dbSchemas()
        {
            try
            {
                this.cb_Schema.Items.Clear();

                DataBaseTypes Selected_dbType = (DataBaseTypes)Enum.Parse(typeof(DataBaseTypes), cb_dbType.SelectedItem.ToString());
                string Host = this.cb_Host.Text;
                string Username = this.tb_username.Text;
                string Password = this.tb_Password.Text;

                Properties.Settings.Default["Active_ConnectionString"] = (int)Selected_dbType;

                if (Selected_dbType == DataBaseTypes.MDC_DATABASE)
                {
                    MySqlConnectionStringBuilder mySqlConStrBuilder = new MySqlConnectionStringBuilder();

                    mySqlConStrBuilder.Server   = Host;
                    mySqlConStrBuilder.UserID   = Username;
                    mySqlConStrBuilder.Password = Password;

                    using (MySqlConnection conn = new MySqlConnection(mySqlConStrBuilder.ConnectionString))
                    {
                        this.cb_Schema.Items.AddRange(this.GetMySqlDatabaseList(conn.ConnectionString).ToArray());
                    }
                }

                else if (Selected_dbType == DataBaseTypes.SCT_DATABASE)
                {
                    SqlConnectionStringBuilder sqlConStrBuilder = new SqlConnectionStringBuilder();

                    //sqlConStrBuilder.Authentication         = SqlAuthenticationMethod.ActiveDirectoryPassword;
                    //sqlConStrBuilder.TrustServerCertificate = true;
                    //sqlConStrBuilder.Encrypt = true;
                    sqlConStrBuilder.DataSource         = Host;
                    sqlConStrBuilder.UserID             = Username;
                    sqlConStrBuilder.Password           = Password;
                    //sqlConStrBuilder.IntegratedSecurity = ;

                    using (SqlConnection conn = new SqlConnection(sqlConStrBuilder.ConnectionString))
                    {
                        this.cb_Schema.Items.AddRange(this.GetSqlDatabaseList(conn.ConnectionString).ToArray());
                    }
                }

                else if (Selected_dbType == DataBaseTypes.SEAC_DATABASE)
                {
                    var FD = new OpenFileDialog();
                    FD.InitialDirectory = Application.StartupPath;

                    if (FD.ShowDialog() == DialogResult.OK)
                    {
                        this.cb_Schema.Items.Add(FD.FileName);
                    }

                    /*
                    SQLiteConnectionStringBuilder sqliteConStrBuilder = new SQLiteConnectionStringBuilder();

                    using (SQLiteConnection conn = new SQLiteConnection(sqliteConStrBuilder.ConnectionString))
                    {
                        this.cb_Schema.Items.AddRange(this.GetSQLiteDatabaseList(conn.ConnectionString).ToArray());
                    }
                    */
                }

                this.cb_Schema.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                this.cb_Schema.Items.Clear();
                throw ex;
            }
        }

        private List<string> GetMySqlDatabaseList(string conString)
        {
            List<string> list = new List<string>();

            try
            {
                this.Enabled = false;
                this.Log_Message("Loading database list from the selected MySQL Server  . . .", 0, 1);

                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("select SCHEMA_NAME from information_schema.SCHEMATA", con))
                    {
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(dr[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while loading database list from the selected MySQL Server {Environment.NewLine}{ex.Message}");
            }
            finally
            {
                this.Enabled = true;
            }

            this.Log_Message("Loaded successfully.", 1, 1);
            return list;
        }

        private List<string> GetSqlDatabaseList(string conString)
        {
            List<string> list = new List<string>();

            try
            {
                this.Enabled = false;
                this.Log_Message("Loading database list from the selected SQL Server  . . .", 0, 1);

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                    {
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(dr[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while loading database list from the selected SQL Server {Environment.NewLine}{ex.Message}");
            }
            finally
            {
                this.Enabled = true;
            }

            this.Log_Message("Loaded successfully.", 1, 1);
            return list;
        }

        private List<string> GetSQLiteDatabaseList(string conString)
        {
            List<string> list = new List<string>();

            try
            {
                this.Enabled = false;
                this.Log_Message("Loading database list from the selected SQL Server  . . .", 0, 1);

                using (SQLiteConnection con = new SQLiteConnection(conString))
                {
                    con.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master", con))
                    {
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(dr[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while loading database list from the selected SQLite Server {Environment.NewLine}{ex.Message}");
            }
            finally
            {
                this.Enabled = true;
            }

            this.Log_Message("Loaded successfully.", 1, 1);
            return list;
        }

        private List<string> GetSQLServerList()
        {
            List<string> list = new List<string>();

            try
            {
                this.Enabled = false;
                this.Log_Message("Loading SQL Servers available on LAN . . .", 0, 1);

                DataTable table = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();
                string server_name = string.Empty;
                foreach (DataRow server in table.Rows)
                {
                    if (string.IsNullOrEmpty(server[table.Columns["InstanceName"]].ToString()))
                    {
                        server_name = $"{server[table.Columns["ServerName"]].ToString()}";
                    }
                    else
                    {
                        server_name = $"{server[table.Columns["ServerName"]].ToString()}\\{server[table.Columns["InstanceName"]].ToString()}";
                    }

                    list.Add(server_name);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while loading SQL Servers {Environment.NewLine}{ex.Message}");
            }
            finally
            {
                this.Enabled = true;
            }

            this.Log_Message("Loaded successfully.", 1, 1);
            return list;
        }

        private void cb_dbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.cb_Host.Items.Clear();
                this.tb_username.Text = "";
                this.tb_Password.Text = "";
                this.cb_Schema.Items.Clear();

                DataBaseTypes Selected_dbType = (DataBaseTypes)Enum.Parse(typeof(DataBaseTypes), cb_dbType.SelectedItem.ToString());
                if (Selected_dbType == DataBaseTypes.MDC_DATABASE)
                {

                    string MDC_ConnString = Properties.Settings.Default.MDC_ConnectionString;
                    MySqlConnectionStringBuilder mySqlConStrBuilder = new MySqlConnectionStringBuilder(MDC_ConnString);

                    this.cb_Host.FlatStyle     = FlatStyle.Standard;
                    this.cb_Host.DropDownStyle = ComboBoxStyle.Simple;
                    this.cb_Host.Text          = mySqlConStrBuilder.Server;

                    this.tb_username.Text = mySqlConStrBuilder.UserID;
                    this.tb_Password.Text = mySqlConStrBuilder.Password;

                    this.cb_Schema.FlatStyle     = FlatStyle.Flat;
                    this.cb_Schema.DropDownStyle = ComboBoxStyle.DropDownList;
                    this.cb_Schema.Items.Add(mySqlConStrBuilder.Database);
                    this.cb_Schema.SelectedIndex = 0;

                    this.cb_Host.Enabled     = true;
                    this.tb_username.Enabled = true;
                    this.tb_Password.Enabled = true;
                    this.cb_Schema.Enabled   = true;
                }
                else if (Selected_dbType == DataBaseTypes.SCT_DATABASE)
                {
                    string SCT_ConnString = Properties.Settings.Default.SCT_ConnectionString;
                    SqlConnectionStringBuilder sqlConStrBuilder = new SqlConnectionStringBuilder(SCT_ConnString);

                    this.cb_Host.Items.AddRange(this.GetSQLServerList().ToArray());
                    this.cb_Host.FlatStyle     = FlatStyle.Flat;
                    this.cb_Host.DropDownStyle = ComboBoxStyle.DropDownList;
                    int index                  = this.cb_Host.Items.IndexOf(sqlConStrBuilder.DataSource);
                    this.cb_Host.SelectedIndex = (index > -1) ? index : 0; // index = -1 means, server_name received from config file, is yet not available on LAN
                    
                    this.tb_username.Text = sqlConStrBuilder.UserID;
                    this.tb_Password.Text = sqlConStrBuilder.Password;

                    this.cb_Schema.FlatStyle     = FlatStyle.Flat;
                    this.cb_Schema.DropDownStyle = ComboBoxStyle.DropDownList;
                    this.cb_Schema.Items.Add(sqlConStrBuilder.InitialCatalog);
                    this.cb_Schema.SelectedIndex = 0;

                    this.cb_Host.Enabled     = true;
                    this.tb_username.Enabled = true;
                    this.tb_Password.Enabled = true;
                    this.cb_Schema.Enabled   = true;
                }
                else if (Selected_dbType == DataBaseTypes.SEAC_DATABASE)
                {
                    string SEAC_ConnString = Properties.Settings.Default.SEAC_ConnectionString;

                    this.cb_Host.FlatStyle     = FlatStyle.Standard;
                    this.cb_Host.DropDownStyle = ComboBoxStyle.Simple;
                    
                    this.cb_Schema.FlatStyle     = FlatStyle.Standard;
                    this.cb_Schema.DropDownStyle = ComboBoxStyle.Simple;
                    this.cb_Schema.Items.Add(SEAC_ConnString);
                    this.cb_Schema.SelectedIndex = 0;

                    this.cb_Host.Enabled     = false;
                    this.tb_username.Enabled = false;
                    this.tb_Password.Enabled = false;
                    this.cb_Schema.Enabled   = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox cBox = e.Control as ComboBox;

            if (cBox == null || cBox.DataSource != this.bSrc_all_quantities)
                return;

            cBox.DropDownStyle = ComboBoxStyle.DropDown;
            cBox.SelectedIndex = -1;
            cBox.AutoCompleteMode = AutoCompleteMode.None;
            
            // Remove an existing event-handler, if present, to avoid adding multiple handlers when the editing control is reused
            cBox.TextUpdate -= new EventHandler(cBox_TextUpdate);

            // Add the event handler
            cBox.TextUpdate += new EventHandler(cBox_TextUpdate);
        }

        private void cBox_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                ComboBox cBox = (ComboBox)sender;
                string textToSearch = cBox.Text;

                if (this.configs.AllQuantities.Count > 0)
                {
                    Configs FilteredConfigs = new Configs();
                    FilteredConfigs.Clear();

                    _dbController.Filter_All_Quan(textToSearch, FilteredConfigs);
                    cBox.DataSource = FilteredConfigs.AllQuantities;
                    cBox.DroppedDown = true;

                    cBox.Text = textToSearch;

                    // Set cursor position at the end of text
                    cBox.SelectionStart = cBox.Text.Length;
                    cBox.SelectionLength = 0;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_ExportGrpId_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.bSrc_capture_objects.Count < 1) return;

                this.Enabled = false;
                /*
                var DeviceIds = configs.CaptureObjects.AsEnumerable().Select(s => s.DeviceId).Distinct().ToList();
                DeviceIds.Sort();
                List<string> DeviceIds_string = DeviceIds.ConvertAll<string>(x => x.ToString());
                */
                DataGridViewRow currentRow = dgv_device.CurrentRow;
                Configs.DeviceRow SelectedDeviceItem = (Configs.DeviceRow)((DataRowView)currentRow.DataBoundItem).Row;
                var SelectedDeviceId = SelectedDeviceItem.id;
                
                // Get GroupIds of selected DeviceId
                var GroupIds = configs.CaptureObjects.AsEnumerable().Where(x=>x.DeviceId==SelectedDeviceId).Select(x => x.GroupId).Distinct().ToList();
                GroupIds.Sort();

                List<string> GroupIds_string = GroupIds.ConvertAll<string>(x => x.ToString());
                
                frmPrompt Prompt = new frmPrompt(GroupIds_string);
                Prompt.ShowDialog();
                if (GroupIds_string.Count < 1) return;
                GroupIds = GroupIds_string.ConvertAll(x => long.Parse(x));

                // Filter CaptureObjects table w.r.t SelectedDeviceId
                DataTable SelectedCaptureObjects = configs.CaptureObjects.AsEnumerable().Where(x => x.DeviceId.Equals(SelectedDeviceId)).CopyToDataTable();
                SelectedCaptureObjects.TableName = "capture_objects";
                this.Export_TableRowsGroup_ToExcel(SelectedCaptureObjects, configs.CaptureObjects.GroupIdColumn.ColumnName, GroupIds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void dgv_CaptureObjects_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (configs.CaptureObjects.Count > 0)
                {
                    this.btn_ExportGrpId.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cb_Schema_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.Close_DbConnection();
                string[] dbName              = this.cb_Schema.Text.Split('\\', '/');
                this.tb_db_name.Text         = dbName[dbName.Length -1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lnklbl_Schema_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Load_dbSchemas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;

                if (e.ColumnIndex < 0) return;
                DataGridViewCell currentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (!(currentCell is DataGridViewComboBoxCell))
                    throw new Exception($"{dgv.Name}: [ ColumnIndex: {e.ColumnIndex} ]: {Environment.NewLine}Exception: {e.Exception.Message}{Environment.NewLine}Context: {e.Context}");
                
                DataGridViewComboBoxCell cBoxCell = (DataGridViewComboBoxCell)currentCell;

                if (cBoxCell.DataSource != this.bSrc_all_quantities)
                    throw new Exception($"{dgv.Name}: [ ColumnIndex: {e.ColumnIndex} ]: {Environment.NewLine}Exception: {e.Exception.Message}{Environment.NewLine}Context: {e.Context}");

                var existingRow = configs.AllQuantities.FirstOrDefault(x => x.OBIS_Index == (decimal)currentCell.Value);

                if (existingRow == null)
                {
                    currentCell.ToolTipText = currentCell.Value + " not exist in AllQuantities table";
                    currentCell.ErrorText   = currentCell.Value + " not exist in AllQuantities table";
                }
            }
            catch (Exception ex)
            {
                this.Log_Message(ex.GetBaseException().Message);
            }
        }

        private void dgv_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                DataGridView dgv = (DataGridView)sender;
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSteelBlue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                DataGridView dgv = (DataGridView)sender;
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_HideMenu_Click(object sender, EventArgs e)
        {
            this.pnl_Menu.Visible     = false;
            this.btn_ShowMenu.Visible = true;
        }

        private void btn_ShowMenu_Click(object sender, EventArgs e)
        {
            this.pnl_Menu.Visible     = true;
            this.btn_ShowMenu.Visible = false;
        }

        private void btn_HideMenu_MouseHover(object sender, EventArgs e)
        {
            this.toolTip.SetToolTip(this.btn_HideMenu, "Hide");
        }

        private void btn_ShowMenu_MouseHover(object sender, EventArgs e)
        {
            this.toolTip.SetToolTip(this.btn_ShowMenu, "Show Menu");
        }

        private void SetPanelBackColor(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Panel)
                    this.SetPanelBackColor((Panel)control);

                control.BackColor = Color.FromArgb(60, 70, 80); 
            }
        }

        private void btn_TabPage_ClickAction(Button btn)
        {
            try
            {
                this.lbl_Header.Text = btn.Text.Trim();

                string tabPageName = btn.Name.Trim().Replace("btn", "tp");
                this.tb_DataViewer.SelectTab(tabPageName);

                this.SetPanelBackColor(this.pnl_Menu);
                btn.BackColor = Color.LightSlateGray;

                string tableName = btn.Name.Trim().Replace("btn_", "");
                if (this.configsTables_Dictionary.ContainsKey(tableName))
                {
                    this.pb_Header.Visible = true;
                    this.lbl_totalItems.Text = this.configsTables_Dictionary[tableName].Rows.Count.ToString();
                    this.lbl_tableName.Text = btn.Text.Trim();
                }
                else
                {
                    this.pb_Header.Visible = false;
                }

                string bSrcName = btn.Name.Trim().Replace("btn", "bSrc");
                if (this.bSrc_Dictionary.ContainsKey(bSrcName))
                {
                    this.lbl_selectedItems.Text = this.bSrc_Dictionary[bSrcName].Count.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btn_TabPage_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;

                this.btn_TabPage_ClickAction(btn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pnl_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, panel.DisplayRectangle, Color.WhiteSmoke, ButtonBorderStyle.Solid);
        }

        private void tb_SearchField_TextChanged(object sender, EventArgs e)
        {
            if (this.rbtn_ObisCodeDetail.Checked)
            {
                this.ObisCodeMode = OBISCodeMode.ObisCode;
            }
            else if (this.rbtn_ObisIndexDetail.Checked)
            {
                this.ObisCodeMode = OBISCodeMode.ObisIndex;
            }

            if (this.ObisCodeMode == OBISCodeMode.ObisIndex)
            {

                this.bSrc_obis_details.Filter = string.Format("CONVERT([Obis_Code], 'System.String') like '%{0}%'", this.tb_SearchField.Text.Trim());
                //dt.DefaultView.RowFilter = string.Format("CONVERT(OBIS_Index, 'System.String') like '%{0}%'", txtOBISCodeAllQuan.Text.Trim());
                dgv_obis_details.Refresh();
            }
            else if (this.ObisCodeMode == OBISCodeMode.ObisCode)
            {

                this.bSrc_obis_details.Filter = string.Format("OBIS_Quantity like '%{0}%'", this.tb_SearchField.Text.Trim());
                //dt.DefaultView.RowFilter = string.Format("CONVERT(OBIS_Index, 'System.String') like '%{0}%'", txtOBISCodeAllQuan.Text.Trim());
                dgv_obis_details.Refresh();
            }
        }

        private void CheckBox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                CheckBox cb = sender as CheckBox;

                Point pt       = new Point(e.ClipRectangle.Left + 2, e.ClipRectangle.Top + 4);
                Rectangle rect = new Rectangle(pt, new Size(12, 12));

                if (cb.Checked)
                {
                    using (Font newFont = new Font("Wingdings", 10f))
                    {
                        e.Graphics.DrawString("ü", newFont, Brushes.WhiteSmoke, rect);
                    }
                    
                }

                e.Graphics.DrawRectangle(Pens.WhiteSmoke, rect);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_all_quantities_Fields()
        {
            try
            {
                if (this.rbSearch.Checked || configs.AllQuantities.Count < 1) return;

                Configs.AllQuantitiesRow CurrentRow = (SelectedAllQuantityRows != null && SelectedAllQuantityRows.Count > 0) ? SelectedAllQuantityRows[0] : configs.AllQuantities[0];

                if (CurrentRow != null)
                {
                    StOBISCode OBISCode           = (Get_Index)CurrentRow.OBIS_Index;
                    this.tb_Label.Text            = CurrentRow.Label;
                    this.tb_ObisIndexAllQuan.Text = (this.rdbIndex.Checked) ? CurrentRow.OBIS_Index.ToString() : OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                    this.cb_Insert.CheckState     = CheckState.Unchecked;
                    this.btnInsertUpdate.Text     = "Update";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void dgv_all_quantities_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                this.Update_all_quantities_Fields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cb_Insert_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.configs.AllQuantities.Count < 1)
                {
                    this.cb_Insert.CheckState = CheckState.Checked;
                    this.btnInsertUpdate.Text = "Insert";
                    return;
                }

                if (this.rbSearch.Checked)
                {
                    if (!this.cb_Insert.Checked) this.cb_Insert.CheckState = CheckState.Checked;
                    this.btnInsertUpdate.Text = "Insert";
                    return;
                }

                if (cb_Insert.Checked)
                {
                    this.tb_Label.Text              = "Dummy";
                    this.tb_ObisIndexAllQuan.Text   = (this.rdbOBISCode.Checked) ? "0:0.0.0.0.0.0" : "0";
                    this.bSrc_all_quantities.Filter = null;
                    this.btnInsertUpdate.Text       = "Insert";
                }
                else
                {
                    this.Update_all_quantities_Fields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.btn_TabPage_ClickAction(this.btnTabs_Dictionary[lbl_Header.Text]);
        }

        private void tb_DataViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.rbInsertUpdate.Checked = true;
        }
    }
}
