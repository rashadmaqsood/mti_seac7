using DatabaseConfiguration.DataBase;
using DatabaseConfiguration.DataSet;
using DLMS;
using DLMS.Comm;
using OptocomSoftware.Properties;
using SharedCode.Comm.HelperClasses;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SmartEyeControl_7.ApplicationGUI.GUI
{
    public partial class ApplicationConfiguration : Form
    {
        private string defaultURLPath = "";

        SQLiteConnection Connection = null;

        public string DefaultURLPath
        {
            get { return defaultURLPath; }
            set { defaultURLPath = value; }
        }

        public ApplicationConfiguration()
        {
            InitializeComponent();
            //myconnection = new OleDbConnection(Settings.Default.SEAC7_DBConnectionString);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo configFile = new FileInfo(this.DefaultURLPath);

                if (configFile.Exists)
                {
                    DialogResult res = MessageBox.Show(this, "Are you sure want to over-write existing config file",
                                                       "Save File", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (res != DialogResult.OK)
                        return;
                    configFile.Delete();
                }
                using (FileStream xmlConfig = new FileStream(this.DefaultURLPath, FileMode.Create))
                {
                    configs.WriteXml(xmlConfig, XmlWriteMode.IgnoreSchema);
                }

                //for (int i = 0; i < dgvOBISDetails.Rows.Count; i++)
                //{
                //    Configs.OBIS_LabelsRow = dgvOBISDetails.Rows[i].Cells[2];
                //}


                configs.SaveConfigurationData(Settings.Default.SEAC7_DBConnectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                allQuantitiesDataGridView.Refresh();
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
                        Prev_Row = configs.AllQuantities.FindByOBIS_Index(ObisCode.OBIS_Value);

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

                allQuantitiesDataGridView.Refresh();
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
            Load_DataBases();
        }


        private void Load_DataBases()
        {
            try
            {
                Connection = null;
                //SEAC_DBAccessLayer DBAccessLayer = null;
                ConfigDBController DBAccessLayer = null;
                try
                {
                    //DBAccessLayer = new SEAC_DBAccessLayer(AccurateOptocomSoftware.Properties.Settings.Default.SEAC7_DBConnectionString);
                    DBAccessLayer = new ConfigDBController(Settings.Default.SEAC7_DBConnectionString, DatabaseConfiguration.DataBaseTypes.SEAC_DATABASE);
                    //string connectionString = DBAccessLayer.ConnectionString;
                    //DbConnectionStringBuilder ConnectionStringBuilder = new DbConnectionStringBuilder(true);
                    //ConnectionStringBuilder.ConnectionString = connectionString;

                    ///// Try to Open Connection
                    //Connection = new OleDbConnection(AccurateOptocomSoftware.Properties.Settings.Default.SEAC7_DBConnectionString);
                    //Connection.Open();

                    //DBAccessLayer.DataBaseConnection = Connection;
                    //DBAccessLayer.ConnectionString = ConnectionStringBuilder.ConnectionString;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                Configs newConfig = new Configs();
                /// newConfig.ReadXml(xmlConfig, XmlReadMode.Auto);
                /// newConfig.LoadConfigurations();
                DBAccessLayer.Load_All_Configurations(newConfig);
                configs = newConfig;
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
                this.statusWordBindingSource.DataSource = configs;

                /// Refresh All Data Grids
                //meterInfoDataGridView.Refresh();
                //meterUserDataGridView.Refresh();

                //sAP_ConfigDataGridView.Refresh();
                //meterConfigDataGridView.Refresh();
                configdataGridView.Refresh();
                ManufacturerDataGridView.Refresh();
                AuthenticationDataGridView.Refresh();
                DeviceAssociationsDataGridView.Refresh();
                DevicesDataGridView.Refresh();
                LPChannelsGroupDataGridView.Refresh();
                DisplayWindowItemsGroupDataGridView.Refresh();
                BillingItemsGroupDataGridView.Refresh();
                EventInfoGroupDataGridView.Refresh();
                dgvOBISDetails.Refresh();
                ObisRightsGroupDataGridView.Refresh();

                displayWindowsDataGridView.Refresh();
                loadProfileChannelsDataGridView.Refresh();
                billingItemsDataGridView.Refresh();
                billTariffQuantityDataGridView.Refresh();
                eventInfoDataGridView.Refresh();
                dgvStatusWord.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error Loading Database");
            }
        }

        #endregion

        #region All Quantities Labels

        private void allQuantitiesDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                DataGridViewRow CurrentRow = (DataGridViewRow)allQuantitiesDataGridView.Rows[e.RowIndex];
                Configs.AllQuantitiesRow DataRow = null;
                DataRow = (Configs.AllQuantitiesRow)((DataRowView)CurrentRow.DataBoundItem).Row;
                StOBISCode OBISCode = (Get_Index)DataRow.OBIS_Index;
                if (allQuantitiesDataGridView["OBIS_Code", e.RowIndex].ColumnIndex == e.ColumnIndex)
                {
                    e.Value = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                }
                else if (allQuantitiesDataGridView["OBIS_Name", e.RowIndex].ColumnIndex == e.ColumnIndex)
                {
                    e.Value = OBISCode.OBISIndex.ToString();
                }
            }
            catch (Exception ex)
            {
                if (e != null)
                    e.Value = "!";
            }

        }

        private void allQuantitiesDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow CurrentRow = null;
            DataGridViewCell CurrentCell = null;
            DataGridViewCell OBISCodeCell = null;
            Configs.AllQuantitiesRow DataRow = null;
            try
            {
                CurrentRow = (DataGridViewRow)allQuantitiesDataGridView.Rows[e.RowIndex];
                CurrentCell = (DataGridViewCell)allQuantitiesDataGridView[e.ColumnIndex, e.RowIndex];
                OBISCodeCell = (DataGridViewCell)allQuantitiesDataGridView["OBIS_Code", e.RowIndex];

                if (CurrentCell == OBISCodeCell)
                {
                    DataRow = (Configs.AllQuantitiesRow)((DataRowView)CurrentRow.DataBoundItem).Row;
                    StOBISCode OBISCode = (Get_Index)DataRow.OBIS_Index;
                    string OBIS = (String)OBISCodeCell.EditedFormattedValue;
                    StOBISCode OBISCodeVal = StOBISCode.ConvertFrom(OBIS);
                    Configs dt = ((Configs)allQuantitiesBindingSource.DataSource);
                    if (DataRow != null && dt.AllQuantities.FindByOBIS_Index(OBISCodeVal.OBIS_Value) == null)
                        DataRow.OBIS_Index = OBISCodeVal.OBIS_Value;
                }

            }
            catch (Exception ex)
            {
                if (CurrentCell != null)
                    CurrentCell.Value = "!";
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                allQuantitiesDataGridView.ClearSelection();

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
                    foreach (DataGridViewRow OBISFinderRow in allQuantitiesDataGridView.Rows)
                    {
                        Configs.AllQuantitiesRow OBISIndexRow =
                            (Configs.AllQuantitiesRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                        if (OBISIndexRow.OBIS_Index == OBISIndexNum)
                        {
                            OBISFinderRow.Selected = true;
                            allQuantitiesDataGridView.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
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
                        foreach (DataGridViewRow OBISFinderRow in allQuantitiesDataGridView.Rows)
                        {
                            Configs.AllQuantitiesRow OBISIndexRow =
                                (Configs.AllQuantitiesRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                            if (OBISIndexRow.OBIS_Index == OBISCodeT.OBIS_Value)
                            {
                                OBISFinderRow.Selected = true;
                                allQuantitiesDataGridView.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
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
                    foreach (DataGridViewRow OBISFinderRow in allQuantitiesDataGridView.Rows)
                    {
                        Configs.AllQuantitiesRow OBISIndexRow = null;
                        if (OBISFinderRow != null && OBISFinderRow.DataBoundItem != null)
                        {
                            OBISIndexRow = (Configs.AllQuantitiesRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                            if (OBISIndexRow.Label != null &&
                                OBISIndexRow.Label.ToLower().Contains(OBISFinder.Label.ToLower()))
                            {
                                OBISFinderRow.Selected = true;
                                allQuantitiesDataGridView.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
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

                    foreach (DataGridViewRow OBISFinderRow in allQuantitiesDataGridView.Rows)
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
                                allQuantitiesDataGridView.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
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
                Configs.AllQuantitiesRow CurrentRow = (SelectedAllQuantityRows != null &&
                    SelectedAllQuantityRows.Count > 0) ? SelectedAllQuantityRows[0] : configs.AllQuantities[0];

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
                            var Prev_Row = configs.AllQuantities.FindByOBIS_Index(Convert.ToDecimal(OBISIndexNum));
                            if (Prev_Row != null)
                            {
                                Notification notifier = new Notification("Failure", string.Format("{0} Obis code Already Exists", ObisValidate.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                var NewRow = configs.AllQuantities.NewAllQuantitiesRow();

                                NewRow.OBIS_Index = Convert.ToDecimal(OBISIndexNum);
                                NewRow.Label = Label;

                                configs.AllQuantities.AddAllQuantitiesRow(NewRow);

                                Notification notifier = new Notification("Success", string.Format("{0} Obis code Successfuly Added",
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

                            var Prev_Row = configs.AllQuantities.FindByOBIS_Index(Convert.ToDecimal(OBISCodeT.OBIS_Value));
                            if (Prev_Row != null)
                            {
                                Notification notifier = new Notification("Failure", string.Format("{0} Obis code Already Exists",
                                                                                          OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                NewRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                                NewRow.Label = Label;

                                configs.AllQuantities.AddAllQuantitiesRow(NewRow);

                                Notification notifier = new Notification("Success", string.Format("{0} Obis code Successfuly Added",
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

                            var Prev_Row = configs.AllQuantities.FindByOBIS_Index(Convert.ToDecimal(OBISCodeT.OBIS_Value));
                            if (Prev_Row != null)
                            {
                                Notification notifier = new Notification("Failure", string.Format("{0} Obis code Already Exists",
                                                                                          OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                NewRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                                NewRow.Label = Label;

                                configs.AllQuantities.AddAllQuantitiesRow(NewRow);

                                Notification notifier = new Notification("Success", string.Format("{0} Obis code Successfuly Added",
                                                                         OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                            }
                        }
                        else
                        {
                            Notification notifier = new Notification("Failure", string.Format("{0} Obis Code not complete specified", OBISDialog.OBISCodeStr));
                            return;
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, "Error Insert New OBIS Entry " + ex.Message, "");
                Notification notifier = new Notification("Failure", "Error Insert New OBIS Entry " + ex.Message, 2500);
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
                    Notification notifier = new Notification("Failure", "Please Select AllQuantity Rows to proceed");
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
                            var Prev_Row = configs.AllQuantities.FindByOBIS_Index(Convert.ToDecimal(OBISIndexNum));
                            if (Prev_Row != null)
                            {
                                Notification notifier = new Notification("Failure", string.Format("{0} Obis Code Already Exists", ObisValidate.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                CurrentRow.OBIS_Index = Convert.ToDecimal(OBISIndexNum);
                                CurrentRow.Label = Label;

                                Notification notifier = new Notification("Success", string.Format("{0} Obis code Edited  Successfuly",
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

                            var Prev_Row = configs.AllQuantities.FindByOBIS_Index(Convert.ToDecimal(OBISCodeT.OBIS_Value));
                            if (Prev_Row != null)
                            {
                                Notification notifier = new Notification("Failure", string.Format("{0} Obis code Already Exists",
                                                                                          OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                // var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                CurrentRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                                CurrentRow.Label = Label;
                                // configs.AllQuantities.AddAllQuantitiesRow(NewRow);
                                Notification notifier = new Notification("Success", string.Format("{0} Obis code Edited Successfuly",
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

                            var Prev_Row = configs.AllQuantities.FindByOBIS_Index(Convert.ToDecimal(OBISCodeT.OBIS_Value));
                            if (Prev_Row != null)
                            {
                                Notification notifier = new Notification("Failure", string.Format("{0} Obis Code Already Exists",
                                                                                          OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                                return;
                            }
                            else
                            {
                                // var NewRow = configs.AllQuantities.NewAllQuantitiesRow();
                                CurrentRow.OBIS_Index = Convert.ToDecimal(OBISCodeT.OBIS_Value);
                                CurrentRow.Label = Label;
                                // conifgs.AllQuantities.AddAllQuantitiesRow(NewRow);

                                Notification notifier = new Notification("Success", string.Format("{0} Obis code Successfuly Added",
                                                                         OBISCodeT.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)));
                            }
                        }
                        else
                        {
                            Notification notifier = new Notification("Failure", string.Format("{0} Obis Code not complete specified", OBISDialog.OBISCodeStr));
                            return;
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, "Error Insert New OBIS Entry " + ex.Message, "");
                Notification notifier = new Notification("Failure", "Error Insert New OBIS Entry " + ex.Message, 2500);
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
                this.displayWindowsDataGridView.Refresh();
                // MessageBox.Show(this, "Selected Windows Added Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Notification notifier = new Notification("Successful", "Selected Windows Added Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void removeWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Configs.ConfigurationRow SelectedRow = (SelectedConfigurationRows != null
                    && SelectedConfigurationRows.Count > 0) ? SelectedConfigurationRows[0] : null;
                List<Configs.AllQuantitiesRow> SelectedWindows = SelectedAllQuantityRows;
                List<Configs.DisplayWindowsRow> SelectedRows = SelectedDisplayWindowRows;

                /// Remove Rows Selected from Display Windows
                if (SelectedRows.Count > 0)
                {
                    SelectedWindows.Clear();
                    foreach (var item in SelectedRows)
                    {
                        SelectedWindows.Add(configs.AllQuantities.FindByOBIS_Index(item.QuantityIndex));
                    }
                }

                if (SelectedRow == null)
                {
                    MessageBox.Show("Please Select Configuration To Remove Windows");
                    return;
                }
                if (SelectedWindows == null || SelectedWindows.Count == 0)
                {
                    MessageBox.Show("Please Select Quantities To be removed in Display Windows");
                    return;
                }

                DialogResult result = MessageBox.Show(this, "Are you sure,want to remove selected display Windows",
                                                            "Confirm Delete",
                                                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result != DialogResult.OK)
                    return;

                /// Remove Quantities From Display Windows Table
                List<Configs.AllQuantitiesRow> WindowsRemoved = new List<Configs.AllQuantitiesRow>();
                foreach (var item in SelectedWindows)
                {
                    try
                    {
                        DataRow[] win = configs.DisplayWindows.Select(String.Format(
                            "[DisplayWindowsGroupId] = {0} and [QuantityIndex] = {1}", SelectedRow.id, item.OBIS_Index));

                        if (win != null && win.Length > 0)
                        {
                            foreach (var winItem in win)
                            {
                                winItem.Delete();
                                configs.DisplayWindows.RemoveDisplayWindowsRow((Configs.DisplayWindowsRow)winItem);
                            }
                            WindowsRemoved.Add(item);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                String removedWindowList = "";
                if (WindowsRemoved.Count > 0)
                {
                    foreach (var item in WindowsRemoved)
                    {
                        removedWindowList += String.Format("{0},", item.Label);
                    }
                    removedWindowList = removedWindowList.Trim(",".ToCharArray());
                }

                Notification notifier = null;
                if (WindowsRemoved.Count <= 0)
                    // MessageBox.Show(this, "Selected Windows not removed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    notifier = new Notification("Error", "Selected Windows not removed");
                else
                    //MessageBox.Show(this, String.Format("Selected Windows removed Successfully\r\n{0}", removedWindowList),
                    //                      "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    notifier = new Notification("Successful", String.Format("Selected Windows removed Successfuly\r\n {0}", removedWindowList));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

        private void reAssignLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Configs.DisplayWindowsRow SelectedRow = (SelectedDisplayWindowRows != null &&
                                                         SelectedDisplayWindowRows.Count > 0) ?
                                                         SelectedDisplayWindowRows[0] : null;

                List<Configs.DisplayWindowsRow> SelectedWindows = new List<Configs.DisplayWindowsRow>(50);

                if (SelectedRow == null)
                {
                    foreach (Configs.DisplayWindowsRow item in configs.DisplayWindows.Rows)
                    {
                        SelectedWindows.Add(item);
                    }
                    // MessageBox.Show("Please Select Configuration To Add Windows");
                    // return;
                }
                else
                {
                    SelectedWindows.Add(SelectedRow);
                }

                if (SelectedWindows == null || SelectedWindows.Count == 0)
                {
                    MessageBox.Show("Please Select Display Windows Rows");
                    return;
                }

                // Add Quantities In DisplayWindows Table
                foreach (var item in SelectedWindows)
                {
                    string WinName = string.Empty;

                    if (String.IsNullOrEmpty(WinName))
                        WinName = configs.AllQuantities.FindByDefault_OBIS_Code(item.QuantityIndex).Label;

                    if (String.IsNullOrEmpty(WinName))
                        WinName = "unknown";

                    item.Label = WinName;
                    // configs.DisplayWindows.AddDisplayWindowsRow(NewRow);
                }

                this.displayWindowsDataGridView.Refresh();
                // MessageBox.Show(this, "Selected Windows Labels Altered Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Notification notifier = new Notification("Successful",
                                                         "Selected Windows Labels Altered Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

        private void loadProfileChannelsDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (loadProfileChannelsDataGridView["QuantityName", e.RowIndex].ColumnIndex == e.ColumnIndex)
                {
                    DataGridViewRow CurrentRow = (DataGridViewRow)loadProfileChannelsDataGridView.Rows[e.RowIndex];
                    Configs.LoadProfileChannelsRow DataRow = null;
                    if (CurrentRow.DataBoundItem != null)
                    {
                        DataRow = (Configs.LoadProfileChannelsRow)((DataRowView)CurrentRow.DataBoundItem).Row;
                        e.Value = configs.AllQuantities.FindByOBIS_Index(DataRow.QuantityIndex).Label;
                    }
                    ///DisplayWindowLabel(e.RowIndex);
                }
            }
            catch (Exception)
            {
                e.Value = "!";
            }
        }

        private void loadProfileChannelsDataGridView_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            try
            {
                Configs.LoadProfileChannelsRow SelectedLPChannel = (Configs.LoadProfileChannelsRow)configs.LoadProfileChannels[e.RowIndex];
                Configs.AllQuantitiesRow ChannelWindowLabel = null;
                if (SelectedLPChannel != null)
                    ChannelWindowLabel = (Configs.AllQuantitiesRow)configs.AllQuantities.FindByOBIS_Index(SelectedLPChannel.QuantityIndex);
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

        /// <summary>
        /// Display Windows Labels That Programmed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayWindowLabel(int RowIndex)
        {
            Configs.DisplayWindowsRow SelectedDisplayWindowRow = null;
            try
            {
                SelectedDisplayWindowRow = (Configs.DisplayWindowsRow)configs.DisplayWindows[RowIndex];
                Configs.AllQuantitiesRow DisplayWindowLabel = null;
                if (SelectedDisplayWindowRow != null)
                    DisplayWindowLabel = (Configs.AllQuantitiesRow)configs.AllQuantities.FindByOBIS_Index(SelectedDisplayWindowRow.QuantityIndex);
                this.displayWindowsDataGridView["WindowLabel", RowIndex].Value = DisplayWindowLabel.Label;

            }
            catch (Exception ex)
            {
                if (RowIndex != -1)
                {
                    if (displayWindowsDataGridView.RowCount > RowIndex)
                    {
                        this.displayWindowsDataGridView["WindowLabel", RowIndex].Value = "!";
                    }
                }
            }
        }

        private string DisplayWindowLabel(Configs.DisplayWindowsRow SelectedDisplayWindowRow)
        {
            try
            {
                if (SelectedDisplayWindowRow == null)
                    return null;
                Configs.AllQuantitiesRow DisplayWindowLabel = null;
                if (SelectedDisplayWindowRow != null)
                    DisplayWindowLabel = (Configs.AllQuantitiesRow)configs.AllQuantities.FindByOBIS_Index(SelectedDisplayWindowRow.QuantityIndex);
                return DisplayWindowLabel.Label;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

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

        //            if (meterConfigDataGridView.SelectedRows != null && meterConfigDataGridView.SelectedRows.Count > 0)
        //            {
        //                foreach (DataGridViewRow item in meterConfigDataGridView.SelectedRows)
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
                    if (configdataGridView.SelectedRows != null && configdataGridView.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in configdataGridView.SelectedRows)
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
                    if (eventLogsDataGridView.SelectedRows != null && eventLogsDataGridView.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in eventLogsDataGridView.SelectedRows)
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
                    if (allQuantitiesDataGridView.SelectedRows != null && allQuantitiesDataGridView.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in allQuantitiesDataGridView.SelectedRows)
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
                    if (dgvOBISDetails.SelectedRows != null && dgvOBISDetails.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in dgvOBISDetails.SelectedRows)
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

        public List<Configs.DisplayWindowsRow> SelectedDisplayWindowRows
        {
            get
            {
                List<Configs.DisplayWindowsRow> SelectedRows = new List<Configs.DisplayWindowsRow>();
                try
                {
                    Configs.DisplayWindowsRow CurrentRow = null;
                    int SelectedIndex = 0;
                    if (displayWindowsDataGridView.SelectedRows != null && displayWindowsDataGridView.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in displayWindowsDataGridView.SelectedRows)
                        {
                            SelectedIndex = item.Index;
                            CurrentRow = (Configs.DisplayWindowsRow)configs.DisplayWindows[SelectedIndex];
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

        public List<Configs.BillingItemsRow> SelectedBillItemRows
        {
            get
            {
                List<Configs.BillingItemsRow> SelectedRows = new List<Configs.BillingItemsRow>();
                try
                {
                    Configs.BillingItemsRow CurrentRow;
                    if (billingItemsDataGridView.SelectedRows != null && billingItemsDataGridView.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in billingItemsDataGridView.SelectedRows)
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
                    Configs.LoadProfileChannelsRow CurrentRow = null;
                    int SelectedIndex = 0;
                    if (loadProfileChannelsDataGridView.SelectedRows != null && loadProfileChannelsDataGridView.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in loadProfileChannelsDataGridView.SelectedRows)
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
                    Configs.BillingItemsRow CurrentRow = null;

                    int SelectedIndex = 0;
                    if (billingItemsDataGridView.SelectedRows != null &&
                        billingItemsDataGridView.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow item in billingItemsDataGridView.SelectedRows)
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

                loadProfileChannelsDataGridView.Refresh();
                Notification notifier = new Notification("Successful",
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
                        SelectedLPChannels.Add(configs.AllQuantities.FindByOBIS_Index(item.QuantityIndex));
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

                Notification notifier = null;
                if (LP_ChannelsRemove.Count <= 0)
                    notifier = new Notification("Error",
                                                "Selected Load Profile Channels not removed");
                else
                    notifier = new Notification("Successful",
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

                this.loadProfileChannelsDataGridView.Refresh();

                Notification notifier = new Notification("Successful",
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
        private void billingItemsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region make filter based on BillingItemId

                DataGridViewRow currentRow = billingItemsDataGridView.Rows[e.RowIndex];
                Configs.BillingItemsRow SelectedBillingItem = (Configs.BillingItemsRow)((DataRowView)currentRow.DataBoundItem).Row;

                if (this.billTariffQuantityBindingSource != null && currentRow != null)
                {
                    this.billTariffQuantityBindingSource.Filter = String.Format("[BillItemId] = '{0}'", SelectedBillingItem.id);
                }
                else if (this.billTariffQuantityBindingSource != null)
                {
                    this.billTariffQuantityBindingSource.Filter = null;
                }

                #endregion
            }
            catch
            {
                if (this.billTariffQuantityBindingSource != null)
                {
                    this.billTariffQuantityBindingSource.Filter = null;
                }
            }
        }

        private void billTariffQuantityDataGridView_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            try
            {
                Configs.BillTariffQuantityRow SelectedBillingQuantityIteml = (Configs.BillTariffQuantityRow)configs.BillTariffQuantity[e.RowIndex];
                Configs.BillingItemsRow SelectedBillItem = null;
                Configs.AllQuantitiesRow ChannelWindowLabel = null;
                if (SelectedBillingQuantityIteml != null)
                {
                    ChannelWindowLabel = (Configs.AllQuantitiesRow)configs.AllQuantities.FindByOBIS_Index(SelectedBillingQuantityIteml.OBIS_Index);
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

        private void billTariffQuantityDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            //try
            //{
            //    if (billTariffQuantityDataGridView["ItemName", e.RowIndex].ColumnIndex == e.ColumnIndex)
            //    {
            //        DataGridViewRow CurrentRow = (DataGridViewRow)billTariffQuantityDataGridView.Rows[e.RowIndex];
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

                /// make New Billing Item
                Configs.BillingItemsRow NewBillItem = (Configs.BillingItemsRow)configs.BillingItems.NewRow();
                NewBillItem.BillItemGroupId = SelectedRow.BillItemsGroupId;
                NewBillItem.Label = (SelectedBillingItems != null && SelectedBillingItems.Count > 0) ? SelectedBillingItems[0].Label : "New Item";
                configs.BillingItems.AddBillingItemsRow(NewBillItem);

                int sequenceId = 1;
                /// Add Quantities In Display Windows Table
                foreach (var item in SelectedBillingItems)
                {
                    Configs.BillTariffQuantityRow NewRow = (Configs.BillTariffQuantityRow)configs.BillTariffQuantity.NewRow();
                    NewRow.BillItemId = NewBillItem.id;
                    NewRow.OBIS_Index = item.OBIS_Index;
                    NewRow.SequenceId = sequenceId++;
                    configs.BillTariffQuantity.AddBillTariffQuantityRow(NewRow);
                }

                /// Refresh Billing Grid
                foreach (var item in billingItemsDataGridView.Rows)
                {
                    ((DataGridViewRow)item).Selected = false;
                }
                if (billingItemsDataGridView.Rows.Count > 0)
                {
                    billingItemsDataGridView.Rows[billingItemsDataGridView.Rows.Count - 1].Selected = true;
                    billingItemsDataGridView.Refresh();
                    billTariffQuantityDataGridView.Refresh();
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

                /// Remove Quantities 
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
                                /// configs.BillingItems.RemoveBillingItemsRow((Configs.BillingItemsRow)winItem);
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

        private void eventLogsDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            //try
            //{
            //    try
            //    {
            //        DataGridViewRow CurrentRow = (DataGridViewRow)eventLogsDataGridView.Rows[e.RowIndex];
            //        Configs.EventLogsRow DataRow = null;
            //        if (CurrentRow.DataBoundItem != null)
            //        {
            //            DataRow = (Configs.EventLogsRow)((DataRowView)CurrentRow.DataBoundItem).Row;
            //        }

            //        if (eventLogsDataGridView["EventName", e.RowIndex].ColumnIndex == e.ColumnIndex)
            //        {
            //            Configs.EventInfoRow EventInfo = configs.EventInfo.Single<Configs.EventInfoRow>((x) => x.id == DataRow.id &&
            //                x.EventGroupId == DataRow.EventInfoRow.EventGroupId);
            //            e.Value = EventInfo.Label;
            //            ///DisplayWindowLabel(e.RowIndex);
            //        }
            //        else if (eventLogsDataGridView["EventLogIndexName", e.RowIndex].ColumnIndex == e.ColumnIndex)
            //        {
            //            Configs.AllQuantitiesRow OBIS_Row = null;
            //            OBIS_Row = (Configs.AllQuantitiesRow)configs.AllQuantities.FindByOBIS_Index(DataRow.EventLogIndex);
            //            e.Value = OBIS_Row.Label;
            //        }
            //        else if (eventLogsDataGridView["EventCounterName", e.RowIndex].ColumnIndex == e.ColumnIndex)
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

        private void configurationDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region make filter based on BillingItemId

                Configs.ConfigurationRow SelectedConfigRow = (SelectedConfigurationRows != null &&
                    SelectedConfigurationRows.Count > 0) ? SelectedConfigurationRows[0] : null;
                if (SelectedConfigRow == null)
                    return;
                else
                {
                    configs.Configuration.CurrentConfiguration = SelectedConfigRow;
                }
                /// Apply Current Configuration Filter On Billing Data Table
                if (this.billingItemsBindingSource != null && SelectedConfigRow != null)
                {
                    this.billingItemsBindingSource.Filter = String.Format("[BillItemGroupId] = '{0}'", SelectedConfigRow.BillItemsGroupId);
                }
                else if (this.billingItemsBindingSource != null)
                {
                    this.billingItemsBindingSource.Filter = null;
                }

                #endregion
                #region make filter based on LoadProfileChannels

                /// Apply Current Configuration Filter On Billing Data Table
                if (this.loadProfileChannelsBindingSource != null && SelectedConfigRow != null)
                {
                    this.loadProfileChannelsBindingSource.Filter = String.Format("[LoadProfileGroupId] = '{0}'",
                                                                                 SelectedConfigRow.lp_channels_group_id);
                }
                else if (this.loadProfileChannelsBindingSource != null)
                {
                    this.loadProfileChannelsBindingSource.Filter = null;
                }

                #endregion
                #region make filter based on Display Windows

                /// Apply Current Configuration Filter On Billing Data Table
                if (this.displayWindowsbindingSource != null && SelectedConfigRow != null)
                {
                    this.displayWindowsbindingSource.Filter = String.Format("[DisplayWindowsGroupId] = '{0}'",
                                                                            SelectedConfigRow.DisplayWindowGroupId);
                }
                else if (this.displayWindowsbindingSource != null)
                {
                    this.displayWindowsbindingSource.Filter = null;
                }

                #endregion
                #region make filter based on Event Info

                /// Apply Current Configuration Filter On Billing Data Table
                if (this.eventInfoBindingSource != null && SelectedConfigRow != null)
                {
                    this.eventInfoBindingSource.Filter = String.Format("[EventGroupId] = '{0}'", SelectedConfigRow.EventGroupId);
                }
                else if (this.eventInfoBindingSource != null)
                {
                    this.eventInfoBindingSource.Filter = null;
                }

                #endregion
                #region make filter based on Event Log Info

                /// Apply Current Configuration Filter On Billing Data Table
                if (this.eventLogsBindingSource != null && SelectedConfigRow != null)
                {
                    this.eventLogsBindingSource.Filter = String.Format("[EventGroupId] = '{0}'", SelectedConfigRow.EventGroupId);
                }
                else if (this.eventLogsBindingSource != null)
                {
                    this.eventLogsBindingSource.Filter = null;
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (this.billingItemsBindingSource != null)
                {
                    this.billingItemsBindingSource.Filter = null;
                }
                if (this.loadProfileChannelsBindingSource != null)
                {
                    this.loadProfileChannelsBindingSource.Filter = null;
                }
                if (this.displayWindowsbindingSource != null)
                {
                    this.displayWindowsbindingSource.Filter = null;
                }
                if (this.eventInfoBindingSource != null)
                {
                    this.eventInfoBindingSource.Filter = null;
                }
                if (this.eventLogsBindingSource != null)
                {
                    this.eventLogsBindingSource.Filter = null;
                }
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

                ConfigsHelper Configurator = new ConfigsHelper(configs);

                Configs.ConfigurationRow Config_OutRow = null;
                Configurator.CopyMeterConfiguration(SelectedConfigs[0], out Config_OutRow, 1);
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
        //             foreach (DataGridViewRow data_Row in this.configdataGridView.Rows)
        //             {
        //                 data_Row.Selected = false;
        //             }
        //             /// Change Selected Configuration Rows
        //             this.configdataGridView.Rows.OfType<DataGridViewRow>().Where(x => x.DataBoundItem != null &&
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
        ////         DataGridViewRow CurrentRow = (DataGridViewRow)meterConfigDataGridView.Rows[e.RowIndex];
        ////         Configs.Meter_ConfigurationRow DataRow = null;
        ////         DataRow = (Configs.Meter_ConfigurationRow)((DataRowView)CurrentRow.DataBoundItem).Row;

        ////         /// StOBISCode OBISCode = (Get_Index)DataRow.OBIS_Index;
        ////         if (meterConfigDataGridView["cnf_MeterModel", e.RowIndex].ColumnIndex == e.ColumnIndex)
        ////         {
        ////             e.Value = (DataRow.IsMeter_Info_idNull()) ? "!" : DataRow.Meter_InfoRow.Model_Name;
        ////             /// OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
        ////         }
        ////         else if (meterConfigDataGridView["Config_Name", e.RowIndex].ColumnIndex == e.ColumnIndex)
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
        ////         CurrentRow = (DataGridViewRow)meterConfigDataGridView.Rows[e.RowIndex];
        ////         CurrentCell = (DataGridViewCell)meterConfigDataGridView[e.ColumnIndex, e.RowIndex];

        ////         MeterModelNameCell = (DataGridViewCell)meterConfigDataGridView["cnf_MeterModel", e.RowIndex];
        ////         ConfigNameCell = (DataGridViewCell)meterConfigDataGridView["Config_Name", e.RowIndex];

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

        private void ManufacturerDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
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
                if (this.DevicebindingSource != null)
                {
                    this.DevicebindingSource.Filter = null;
                }
            }
        }

        private void DevicesDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region make filter based on DeviceId
                if (e.RowIndex < 0) return;
                DataGridViewRow currentRow = DevicesDataGridView.Rows[e.RowIndex];
                Configs.DeviceRow SelectedDeviceItem = (Configs.DeviceRow)((DataRowView)currentRow.DataBoundItem).Row;

                if (this.DeviceAssociationbindingSource != null && currentRow != null)
                {
                    this.DeviceAssociationbindingSource.Filter = String.Format("[Device_Id] = '{0}'", SelectedDeviceItem.id);
                }
                else if (this.DeviceAssociationbindingSource != null)
                {
                    this.DeviceAssociationbindingSource.Filter = null;
                }

                if (this.OBISDetailsBindingSource != null && currentRow != null)
                {
                    this.OBISDetailsBindingSource.Filter = String.Format("[Device_Id] = '{0}'", SelectedDeviceItem.id);
                }
                else if (this.OBISDetailsBindingSource != null)
                {
                    this.OBISDetailsBindingSource.Filter = null;
                }

                #endregion
            }
            catch
            {
                if (this.DeviceAssociationbindingSource != null)
                {
                    this.DeviceAssociationbindingSource.Filter = null;
                }
                if (this.OBISDetailsBindingSource != null)
                {
                    this.OBISDetailsBindingSource.Filter = null;
                }
            }
        }

        private void DeviceAssociationsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region make filter based on DeviceId
                
                if (e.RowIndex < 0) return;
                DataGridViewRow currentRow = DeviceAssociationsDataGridView.Rows[e.RowIndex];
                Configs.Device_AssociationRow SelectedDeviceAssociation = (Configs.Device_AssociationRow)((DataRowView)currentRow.DataBoundItem).Row;

                if (this.configurationBindingSource != null && currentRow != null)
                {
                    this.configurationBindingSource.Filter = String.Format("[Configuration_Id] = '{0}'", SelectedDeviceAssociation.Configuration_Id);
                }
                else if (this.configurationBindingSource != null)
                {
                    this.configurationBindingSource.Filter = null;
                }

                #endregion
            }
            catch
            {
                if (this.configurationBindingSource != null)
                {
                    this.configurationBindingSource.Filter = null;
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

        // private void dgvOBISLabels_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        // {
        //     DataGridViewRow CurrentRow = null;
        //     DataGridViewCell CurrentCell = null;
        //     DataGridViewCell OBISCodeCell = null;
        //     Configs.OBIS_LabelsRow DataRow = null;
        //     try
        //     {
        //         CurrentRow = (DataGridViewRow)dgvOBISLabels.Rows[e.RowIndex];
        //         CurrentCell = (DataGridViewCell)allQuantitiesDataGridView[e.ColumnIndex, e.RowIndex];
        //         OBISCodeCell = (DataGridViewCell)allQuantitiesDataGridView["OBIS_Value", e.RowIndex];
           
        //         if (CurrentCell == OBISCodeCell)
        //         {
        //             DataRow = (Configs.OBIS_LabelsRow)((DataRowView)CurrentRow.DataBoundItem).Row;
        //             StOBISCode OBISCode = (Get_Index)DataRow.Default_OBIS_Code;
        //             string OBIS = (String)OBISCodeCell.EditedFormattedValue;
        //             StOBISCode OBISCodeVal = StOBISCode.ConvertFrom(OBIS);
        //             Configs dt = ((Configs)OBISLabelsBindingSource.DataSource);
        //             if (DataRow != null && dt.OBIS_Labels.FindByDefault_OBIS_Code(OBISCodeVal.OBIS_Value) == null)
        //                 DataRow.Default_OBIS_Code = OBISCodeVal.OBIS_Value;
        //         }
           
        //     }
        //     catch (Exception ex)
        //     {
        //         if (CurrentCell != null)
        //             CurrentCell.Value = "!";
        //     }
        // }

        // private void dgvOBISLabels_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        // {
        //     try
        //     {
        //         DataGridViewRow CurrentRow = (DataGridViewRow)dgvOBISLabels.Rows[e.RowIndex];
        //         Configs.OBIS_LabelsRow DataRow = null;
        //         DataRow = (Configs.OBIS_LabelsRow)((DataRowView)CurrentRow.DataBoundItem).Row;
        //         StOBISCode OBISCode = (Get_Index)DataRow.Default_OBIS_Code;
        //         if (dgvOBISLabels["OBIS_Value", e.RowIndex].ColumnIndex == e.ColumnIndex)
        //         {
        //             e.Value = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         if (e != null)
        //             e.Value = "!";
        //     }
        // }

        private void dgvOBISDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOBISDetails_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                DataGridViewRow CurrentRow = (DataGridViewRow)dgvOBISDetails.Rows[e.RowIndex];
                Configs.OBIS_DetailsRow DataRow = null;
                DataRow = (Configs.OBIS_DetailsRow)((DataRowView)CurrentRow.DataBoundItem).Row;
                StOBISCode OBISCode = (Get_Index)DataRow.Obis_Code;
                if (dgvOBISDetails["OBIS_Quantity", e.RowIndex].ColumnIndex == e.ColumnIndex)
                {
                    e.Value = OBISCode.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                }
            }
            catch (Exception ex)
            {
                if (e != null)
                    e.Value = "!";
            }
        }

        private void ObisRightsGroupDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region make filter based on DeviceId
                if (e.RowIndex < 0) return;
                DataGridViewRow currentRow = ObisRightsGroupDataGridView.Rows[e.RowIndex];
                Configs.Obis_Rights_GroupRow SelectedObisRightGroupItem = (Configs.Obis_Rights_GroupRow)((DataRowView)currentRow.DataBoundItem).Row;

                if (this.OBISDetailsBindingSource != null && currentRow != null)
                {
                    this.OBISDetailsBindingSource.Filter = String.Format("[Rights_Group_Id] = '{0}'", SelectedObisRightGroupItem.id);
                }
                else if (this.OBISDetailsBindingSource != null)
                {
                    this.OBISDetailsBindingSource.Filter = null;
                }

                #endregion
            }
            catch
            {
                if (this.OBISDetailsBindingSource != null)
                {
                    this.OBISDetailsBindingSource.Filter = null;
                }
            }
        }

        private void searchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                dgvOBISDetails.ClearSelection();

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
                    foreach (DataGridViewRow OBISFinderRow in dgvOBISDetails.Rows)
                    {
                        Configs.OBIS_DetailsRow OBISIndexRow =
                            (Configs.OBIS_DetailsRow)((DataRowView)OBISFinderRow.DataBoundItem).Row;
                        if (OBISIndexRow.Obis_Code == OBISIndexNum)
                        {
                            OBISFinderRow.Selected = true;
                            dgvOBISDetails.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
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

                    foreach (DataGridViewRow OBISFinderRow in dgvOBISDetails.Rows)
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
                                dgvOBISDetails.FirstDisplayedScrollingRowIndex = OBISFinderRow.Index;
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

        private void eventLogsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
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
            //         ((DataGridViewComboBoxColumn)eventLogsDataGridView.Columns["EvenlLogIndex"]).DataSource = this.OBISLabelsBindingSource;
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

        private void dgvOBISDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow CurrentRow = (DataGridViewRow)dgvOBISDetails.Rows[e.RowIndex];
            this.txtOBISQuantity.Text = CurrentRow.Cells[2].Value.ToString();
            this.txtOBISCode.Text = CurrentRow.Cells[1].Value.ToString();
        }

        private void txtOBISQuantity_Leave(object sender, EventArgs e)
        {
            string OBISCode = this.txtOBISQuantity.Text;
            this.txtOBISCode.Text = StOBISCode.ConvertFrom(OBISCode).OBIS_Value.ToString();
        }

    }
}
