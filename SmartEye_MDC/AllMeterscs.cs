using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using comm;
using System.Threading;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using SharedCode.Comm.DataContainer;
using Communicator.MTI_MDC;

namespace Communicator
{
    public partial class AllMeterscs : Form
    {
        #region Data Members
        //refreshing in 10sec 10000 milisec
        private int refreshingTimeLeft = 0;
        System.Windows.Forms.Timer refereshing_timer;
        ContextMenuStrip strip;
        #endregion

        #region Properties
        //=====================================================================
        public ConnectionsList MeterConnectionList { get; set; }
        //=====================================================================
        public AutoCompleteStringCollection autoCompleteSource;
        //=====================================================================
        public List<string> FilterMsns { get; set; }
        //=====================================================================
        public ConnectionManager ConManager { get; set; }
        #endregion

        #region Constructor

        public AllMeterscs(ConnectionsList conList)
        {
            try
            {
                InitializeComponent();
                MeterConnectionList = conList;
                dtg.AutoGenerateColumns = false;
                autoCompleteSource = new AutoCompleteStringCollection();
                refereshing_timer = new System.Windows.Forms.Timer();
                refereshing_timer.Tick += new EventHandler(refereshing_timer_Tick);
                FilterMsns = new List<string>();
                
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }

        #endregion

        #region Events

        //===============================================================================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                lblTotalConnection.Text = GetMaxConnectionCount().ToString();
                refereshing_timer.Stop();
                if (chkSingleSearch.Checked)
                {
                    InitAutoCompleteSource();
                    txtFilter.Text = "";
                }
                else if (chkMultiSelect.Checked)
                {

                    var tbl = GetConnectionTable();
                    if (FilterMsns.Count > 0)
                        tbl.DefaultView.RowFilter = string.Format("msn IN {0}", CreateMsnQuery(FilterMsns));
                    dtg.DataSource = tbl;

                }
                else
                {

                    if (cmbSearchOn.Text == "Meter Serial Number")
                    {
                        var tbl = GetConnectionTable();
                        if (txtFilter.Text.Length == 10)
                            tbl.DefaultView.RowFilter = string.Format("msn LIKE '%{0}%'", txtFilter.Text);
                        dtg.DataSource = tbl;
                    }
                    else
                    {
                        var tbl = GetConnectionTable();
                        if (txtFilter.Text != string.Empty)
                            tbl.DefaultView.RowFilter = string.Format("type ='{0}'", txtFilter.Text);
                        dtg.DataSource = tbl;
                    }
                    InitAutoCompleteSource();

                }
                refreshingTimeLeft = int.Parse(cmbAutoRefresh.Text);
                refereshing_timer.Start();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }

        }
        //===============================================================================
        private void AllMeterscs_Load(object sender, EventArgs e)
        {
            try
            {
                InitAutoCompleteSource();
                chkSingleSearch.Checked = true;
                cmbSearchOn.SelectedIndex = 0;
                lblTotalConnection.Text = GetMaxConnectionCount().ToString();
                cmbAutoRefresh.Text = Properties.Settings.Default.RefreshingTimeForAllMeterWindow;
                refreshingTimeLeft = int.Parse(cmbAutoRefresh.Text);
                refereshing_timer.Interval = 1000;
                refereshing_timer.Start();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkSingleSearch.Checked)
                {
                    if (txtFilter.Text != string.Empty && txtFilter.Text.Length > 3 && dtg.DataSource != null)
                    {

                        if (cmbSearchOn.Text == "Meter Serial Number")
                        {
                            var tbl = (dtg.DataSource as DataTable);
                            tbl.DefaultView.RowFilter = string.Format("msn LIKE '%{0}%'", txtFilter.Text);
                            dtg.DataSource = tbl;
                        }
                        else
                        {
                            var tbl = (dtg.DataSource as DataTable);
                            tbl.DefaultView.RowFilter = string.Format("type ='{0}'", txtFilter.Text);
                            dtg.DataSource = tbl;
                        }
                    }
                    else
                    {
                        //dtg.DataSource = GetConnectionTable();
                    }
                }
                else
                {

                    if (txtFilter.Text.Length != 10) return;
                    var conn = MeterConnectionList.FirstOrDefault(t => t.Key.MSN.ToString() == txtFilter.Text);
                    if (conn.Value != null && conn.Value.MSN == txtFilter.Text)
                    {
                        var tbl = MakeNamesTable();
                        AddNewRowToTable(ref tbl, conn.Value);
                        dtg.DataSource = tbl;
                    }


                }
                Application.DoEvents(); 
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        private void dtg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {

                    var msn = dtg["clmMsn1", e.RowIndex].Value.ToString();
                    if (MeterConnectionList.Count > 0)
                    {
                        //var connection = MeterConnectionList[e.RowIndex];
                        var connection = MeterConnectionList.FirstOrDefault(x => x.Value.MSN == msn);
                        if (connection.Value != null && connection.Value.MSN == msn)
                        {
                            if (connection.Value.Tag == null)
                            {
                                connection.Value.Tag = new LiveConnectionInfo(connection.Value, ConManager);
                                ((LiveConnectionInfo)connection.Value.Tag).Show();
                            }
                            else
                            {
                                if (((LiveConnectionInfo)connection.Value.Tag).IsDisposed)
                                    connection.Value.Tag = new LiveConnectionInfo(connection.Value, ConManager);
                                ((LiveConnectionInfo)connection.Value.Tag).Show();

                            }
                            ((LiveConnectionInfo)connection.Value.Tag).BringToFront();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSearchOn.SelectedItem.ToString() == "Meter Serial Number")
                {
                    txtFilter.Items.Clear();
                    //dtg.DataSource = null;
                    txtFilter.Text = "";
                }
                else
                {
                    txtFilter.Items.Clear();
                    txtFilter.Items.AddRange(Enum.GetNames(typeof(PhysicalConnectionType)));
                    //dtg.DataSource = null;
                    txtFilter.Text = "";
                }
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        private void chkSingleSearch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkMultiSelect.Checked) chkMultiSelect.Checked = !chkSingleSearch.Checked;
                if (chkSingleSearch.Checked)
                {
                    dtg.DataSource = null;
                    cmbSearchOn.SelectedIndex = 0;
                    cmbSearchOn.Enabled = false;
                    txtFilter.Text = "";

                }
                else
                {
                    cmbSearchOn.Enabled = true;
                    dtg.DataSource = GetConnectionTable();
                    InitAutoCompleteSource();
                    txtFilter.Text = "";
                }
                lblTotalConnection.Text = GetMaxConnectionCount().ToString();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkMultiSelect.Checked) 
                {
                    FilterMsns.Clear();
                    flpFilter.Controls.Clear();
                }
                else if (chkSingleSearch.Checked) txtFilter.Text = "";
                dtg.DataSource = null;
                lblTotalConnection.Text = GetMaxConnectionCount().ToString();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        void refereshing_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();
                refreshingTimeLeft--;
                if (refreshingTimeLeft == 0)
                {
                    refereshing_timer.Stop();
                    if (chkSingleSearch.Checked)
                    {
                        InitAutoCompleteSource();
                        txtFilter.Text = "";
                    }
                    else if (chkMultiSelect.Checked)
                    {

                        var tbl = GetConnectionTable();
                        if (FilterMsns.Count > 0)
                            tbl.DefaultView.RowFilter = string.Format("msn IN {0}", CreateMsnQuery(FilterMsns));
                        dtg.DataSource = tbl;

                    }
                    else
                    {

                        if (cmbSearchOn.Text == "Meter Serial Number")
                        {
                            var tbl = GetConnectionTable();
                            if (txtFilter.Text.Length == 10)
                                tbl.DefaultView.RowFilter = string.Format("msn LIKE '%{0}%'", txtFilter.Text);
                            dtg.DataSource = tbl;
                        }
                        else
                        {
                            var tbl = GetConnectionTable();
                            if (txtFilter.Text != string.Empty)
                                tbl.DefaultView.RowFilter = string.Format("type ='{0}'", txtFilter.Text);
                            dtg.DataSource = tbl;
                        }
                        InitAutoCompleteSource();

                    }
                    refreshingTimeLeft = int.Parse(cmbAutoRefresh.Text);
                    refereshing_timer.Start();
                }
                lblRefreshTimeLeft.Text = refreshingTimeLeft.ToString();
                lblTotalConnection.Text = GetMaxConnectionCount().ToString();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        private void cmbAutoRefresh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                refereshing_timer.Stop();
                refreshingTimeLeft = int.Parse(cmbAutoRefresh.Text);
                lblRefreshTimeLeft.Text = refreshingTimeLeft.ToString();
                refereshing_timer.Start();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        private void AllMeterscs_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                refereshing_timer.Stop();
                refereshing_timer.Dispose();
                dtg.Dispose();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        private void dtg_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 0) return;

                strip = new ContextMenuStrip();
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = "Add To Filter";
                item.Click += new EventHandler(item_Click);
                item.Tag = dtg[0, e.RowIndex].Value;
                strip.Tag = dtg[0, e.RowIndex].Value;
                strip.Items.Add(item);

                e.ContextMenuStrip = strip;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }

        }
        //===============================================================================
        void item_Click(object sender, EventArgs e)
        {

            try
            {
                var msn = (sender as ToolStripMenuItem).Tag.ToString();
                AddToFilter(msn);
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }

        }
        //===============================================================================
        void bbtn_CrossClick(object sender, EventArgs e)
        {
            try
            {

                FilterMsns.Remove((sender as ucBallonButton).BallonText);
                flpFilter.Controls.Remove(sender as ucBallonButton);

            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        private void chkMultiSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSingleSearch.Checked) chkSingleSearch.Checked = !chkMultiSelect.Checked;
                flpFilter.Visible = chkMultiSelect.Checked;
                if (!chkMultiSelect.Checked) btnUpdate.PerformClick();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================
        private void btnAddToFilter_Click(object sender, EventArgs e)
        {
            try
            {
                AddToFilter(txtFilter.Text);                
                txtFilter.Text = "";
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }
        //===============================================================================

        #endregion

        #region Helper Methods

        //===============================================================================
        private DataTable GetConnectionTable(IOConnection Conn = null)
        {
            var tbl = MakeNamesTable();

            try
            {
                var MaxCount = GetMaxConnectionCount();
                tbl.Rows.Clear();
                var totalKeys = MeterConnectionList.Keys;

                //for (int i = 0; i < MaxCount; i++)
                foreach(var key in totalKeys)
                {

                    var item = MeterConnectionList[key];
                    if(item!=null)
                    AddNewRowToTable(ref tbl, item);
                    //DataRow row = tbl.NewRow();

                    //if (item.MSN != null)
                    //    row["msn"] = item.MSN;

                    //string IP_PORT = item.IOStream.ToString();
                    //int index = IP_PORT.IndexOf(':');
                    //row["ip"] = IP_PORT.Substring(0, index);
                    //row["port"] = IP_PORT.Substring(index + 1);
                    //row["connectionTime"] = item.ConnectionTime;
                    //if (item.LastHeartBeat != null)
                    //    row["last_HB_time"] = item.LastHeartBeat.DateTimeStamp;
                    //row["status"] = item.MeterLiveLog;
                    //row["type"] = item.ConnectionInfo.ConnectionType.ToString();

                    //tbl.Rows.Add(row);
                }
            }
            catch (Exception)
            {

            }

            return tbl;
        }
        //===============================================================================
        private void AddNewRowToTable(ref DataTable tbl, IOConnection item)
        {
            try
            {
                DataRow row = tbl.NewRow();

                if (item.MSN != null)
                    row["msn"] = item.MSN;

                string IP_PORT = item.IOStream.ToString();
                int index = IP_PORT.IndexOf(':');
                row["ip"] = IP_PORT.Substring(0, index);
                row["port"] = IP_PORT.Substring(index + 1);
                row["connectionTime"] = item.ConnectionTime;
                if (item.LastHeartBeat != null)
                    row["last_HB_time"] = item.LastHeartBeat.DateTimeStamp;
                row["status"] = item.MeterLiveLog;
                row["type"] = item.ConnectionInfo.ConnectionType.ToString();

                tbl.Rows.Add(row);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //===============================================================================
        private DataTable MakeNamesTable()
        {
            try
            {
                // Create a new DataTable titled 'Names.'
                DataTable namesTable = new DataTable("tblConnections");

                // Add three column objects to the table.
                DataColumn msn = new DataColumn();
                msn.DataType = System.Type.GetType("System.String");
                msn.ColumnName = "msn";
                namesTable.Columns.Add(msn);

                DataColumn clmIp = new DataColumn();
                clmIp.DataType = System.Type.GetType("System.String");
                clmIp.ColumnName = "ip";
                namesTable.Columns.Add(clmIp);

                DataColumn clmPort = new DataColumn();
                clmPort.DataType = System.Type.GetType("System.String");
                clmPort.ColumnName = "port";
                namesTable.Columns.Add(clmPort);

                var conTime = new DataColumn();
                conTime.DataType = System.Type.GetType("System.String");
                conTime.ColumnName = "connectionTime";
                namesTable.Columns.Add(conTime);

                var HBTime = new DataColumn();
                HBTime.DataType = System.Type.GetType("System.String");
                HBTime.ColumnName = "last_HB_time";
                namesTable.Columns.Add(HBTime);

                var status = new DataColumn();
                status.DataType = System.Type.GetType("System.String");
                status.ColumnName = "status";
                namesTable.Columns.Add(status);

                var type = new DataColumn();
                type.DataType = System.Type.GetType("System.String");
                type.ColumnName = "type";
                namesTable.Columns.Add(type);


                // Return the new DataTable. 
                return namesTable;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //===============================================================================
        //private List<string> GetAllConnectedMSN()
        //{
        //    try
        //    {
        //        List<string> msns = new List<string>();
        //        int Count = 0;
        //        Interlocked.Exchange(ref Count, MeterConnectionList.Count);
        //        for (int i = 0; i < Count; i++)
        //        {
        //            msns.Add(MeterConnectionList[i].MSN);
        //        }
                
        //        return msns;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private List<string> GetAllConnectedMSN()
        {
            try
            {
                var totalMeters = GetMaxConnectedMeters();
                List<string> msns = new List<string>();
                foreach (var meter in totalMeters)
                {
                    msns.Add(meter.MSN.ToString());
                }

                return msns;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //===============================================================================
        private void InitAutoCompleteSource()
        {
            try
            {
                autoCompleteSource.AddRange(GetAllConnectedMSN().ToArray());
                txtFilter.AutoCompleteCustomSource = autoCompleteSource;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //===============================================================================
        private int GetMaxConnectionCount()
        {
            try
            {
                int MaxCount = 0;
                Interlocked.Exchange(ref MaxCount, MeterConnectionList.Count);
                return MaxCount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MeterSerialNumber> GetMaxConnectedMeters()
        {
            try
            {
                return MeterConnectionList.Keys.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //===============================================================================
        private string CreateMsnQuery(List<string> msns)
        {
            try
            {
                StringBuilder msn = new StringBuilder();
                msn.Append("(");
                foreach (var item in msns)
                {
                    msn.Append(string.Format(",'{0}'", item));
                }
                msn = msn.Replace("(,", "(");
                msn.Append(")");
                return msn.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //===============================================================================
        private void AddToFilter(string msn)
        {
            try
            {
                if (msn.Length == 10)
                {
                    var bbtn = new ucBallonButton();
                    bbtn.CrossBoxColor = Color.Red;
                    bbtn.CrossClick += new OnCrossClick(bbtn_CrossClick);
                    bbtn.BallonText = msn;

                    if (!FilterMsns.Contains(msn) && msn.StartsWith("36"))
                    {
                        flpFilter.Controls.Add(bbtn);
                        FilterMsns.Add(msn);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //===============================================================================
        #endregion

        #region Destructor

        ~AllMeterscs()
        {
            try
            {
                this.Dispose(false);
            }
            catch (Exception)
            {

            }
        }

        #endregion


    }
}
