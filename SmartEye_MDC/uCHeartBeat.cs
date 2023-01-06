using System;
using System.Threading;
using System.Windows.Forms;
using comm;
using System.Collections.Generic;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.DataContainer;
using SharedCode.Controllers;

namespace Communicator
{
    public partial class uCHeartBeat : Form
    {
        #region Data Members
        int RefreshTime = 0;
        int RefreshTimeLeft = 0;
        ConnectionController obj_ConnController;
        ConnectionsList objConnectionList;

        public event Action ApplicationReadWrite = delegate { };
        const byte _MSN = 0;
        const byte _IP = 1;
        const byte _PORT = 2;
        const byte _ConnectionTime = 3;
        const byte _LastHeartBeatTime = 4;
        const byte _Status = 5;
        private string Selected_IP_Port;

        private int TotalCount = 0;
        private static int Start = 0;
        private static int End = 20;
        #endregion

        #region Properties

        public ConnectionsList ObjConnectionList
        {
            get { return objConnectionList; }
            set { objConnectionList = value; }
        }

        #endregion

        #region Constructor

        public uCHeartBeat()
        {
            InitializeComponent();

        }

        public uCHeartBeat(ConnectionController objConnController)
        {
            InitializeComponent();
            obj_ConnController = objConnController;
        }

        #endregion

        #region Member Functions

        private void uCHeartBeat_Load(object sender, EventArgs e)
        {
            timer_refreshList.Start();
            timer1.Start();
            comboBox1.Text = (timer_refreshList.Interval / 50).ToString();
        }

        private void timer_refreshList_Tick(object sender, EventArgs e)
        {
            RefreshTimeLeft = RefreshTime / 1000;
            timer_refreshList.Enabled = false;
            timer_refreshList.Stop();
            showToGUI_HeartsBeats(Start, End);
            timer_refreshList.Enabled = true;
            timer_refreshList.Start();
            Start = 0;
            End = 20;
        }

        private void showToGUI_HeartsBeats(int R1 = 0, int R2 = 20)
        {
            try
            {
                IOConnection item = null;
                ICollection<MeterSerialNumber> MaxConnectedMeters  ;
                int MaxCount = 0;
                ConnectionsList localList = null;

                clearGrid(grid_MeterHb);
                if (ObjConnectionList != null)
                {
                    //Interlocked.Exchange(ref MaxCount, objConnectionList.Count);
                    localList = objConnectionList;
                    MaxConnectedMeters = ObjConnectionList.Keys;
                    MaxCount = MaxConnectedMeters.Count;
                }
                else
                    return;
                localList = objConnectionList;

                lbl_totalConnections.Text = MaxCount.ToString();//Maximum connected meters

                if (R2 > MaxCount)
                    R2 = MaxCount;
                if (R1 >= R2)
                    R1 = 0;
                //for (int k = R1; k < R2; k++)
                foreach(var meter in MaxConnectedMeters)
                {
                    item = localList[meter];
                    grid_MeterHb.Rows.Add();
                    grid_MeterHb.Rows[grid_MeterHb.Rows.Count - 1].HeaderCell.Value = grid_MeterHb.Rows.Count.ToString();

                    if (item.MSN != null)
                        grid_MeterHb[_MSN, grid_MeterHb.Rows.Count - 1].Value = item.MSN;

                    string IP_PORT = item.IOStream.ToString();
                    int index = IP_PORT.IndexOf(':');
                    grid_MeterHb[_IP, grid_MeterHb.Rows.Count - 1].Value = IP_PORT.Substring(0, index);
                    grid_MeterHb[_PORT, grid_MeterHb.Rows.Count - 1].Value = IP_PORT.Substring(index + 1);
                    grid_MeterHb[_ConnectionTime, grid_MeterHb.Rows.Count - 1].Value = item.ConnectionTime; 

                    if (IP_PORT == Selected_IP_Port)
                    {
                        grid_MeterHb[0, grid_MeterHb.Rows.Count - 1].Selected = true;
                    }

                    if (item.LastHeartBeat != null)
                        grid_MeterHb[_LastHeartBeatTime, grid_MeterHb.Rows.Count - 1].Value = item.LastHeartBeat.DateTimeStamp;
                }
                #region Status
                //for (int i = 0; i < grid_MeterHb.Rows.Count; i++)
                //{
                //    string meterIP = grid_MeterHb[1, i].Value.ToString() + ":" + grid_MeterHb[2, i].Value.ToString();
                //    IOConnection connection = getConnection(meterIP, true);
                //    if (connection == null)
                //        continue;
                //    if (connection.IsConnected)
                //    {
                //        grid_MeterHb[5, i].Value = "Connected";
                //        grid_MeterHb.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                //    }
                //    else
                //    {
                //        if (grid_MeterHb[0, i].Value == null)
                //        {
                //            grid_MeterHb[5, i].Value = "No HeartBeat";
                //            grid_MeterHb.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                //        }
                //        else
                //        {
                //            if (localList[i].HeartBeats.Count > 0)
                //            {
                //                grid_MeterHb[5, i].Value = "HeartBeat";
                //                grid_MeterHb.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                //            }
                //            else
                //            {
                //                grid_MeterHb[5, i].Value = "Idle";
                //                grid_MeterHb.Rows[i].DefaultCellStyle.ForeColor = Color.Brown;
                //            }
                //        }
                //    }

                //}
                #endregion
            }
            catch (Exception)
            {

                //     throw;
            }
        }

        private void clearGrid(DataGridView grid)
        {
            for (int i = grid.Rows.Count - 1; i >= 0; i--)
            {
                grid.Rows.RemoveAt(i);
            }
        }

        private IOConnection getConnection(string MSN)
        {
            return objConnectionList[MSN];
        }

        //private IOConnection getConnection(string IP, bool isTrue)
        //{
        //    foreach (IOConnection item in objConnectionList)
        //    {
        //        string IOStream = item.IOStream.ToString();
        //        if (IOStream == IP)
        //        {
        //            return item;
        //        }

        //    }
        //    return null;
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            RefreshTimeLeft--;
            lbl_RefreshTimeLeft.Text = (timer_refreshList.Interval / 1000 - (-RefreshTimeLeft)).ToString();
            lbl_1.Visible = true;
            lbl_2.Visible = true;
            Application.DoEvents();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showToGUI_HeartsBeats(Start, End);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer_refreshList.Interval = Convert.ToInt32(comboBox1.Text) * 1000;
            RefreshTimeLeft = 0;
            timer1_Tick(this, new EventArgs());
        }

        #endregion

        #region Navigation
        private void btn_Last_Click(object sender, EventArgs e)
        {
            if (ObjConnectionList != null)
            {
                Interlocked.Exchange(ref TotalCount, objConnectionList.Count);
            }
            if (TotalCount >= 20)
            {
                End = TotalCount;
                Start = TotalCount - 20;
            }
            else
            {
                Start = 0;
                End = TotalCount;
            }
            showToGUI_HeartsBeats(Start, End);
            //Load last 20
        }

        private void btn_First_Click(object sender, EventArgs e)
        {
            //Load first 20
            if (ObjConnectionList != null)
            {
                Interlocked.Exchange(ref TotalCount, objConnectionList.Count);
            }
            if (TotalCount >= 20)
            {
                Start = 0;
                End = TotalCount + 20;
            }
            else
            {
                Start = 0;
                End = TotalCount;
            }
            showToGUI_HeartsBeats(Start, End);
        }

        private void btn_Previous_Click(object sender, EventArgs e)
        {
            //Previous

            if (ObjConnectionList != null)
            {
                Interlocked.Exchange(ref TotalCount, objConnectionList.Count);
            }
            if (Start - 20 > 0)
            {
                Start -= 20;
                End -= 20;

            }
            else
            {
                Start = 0;
                if (TotalCount > 20)
                    End = Start + 20;
                else
                    End = TotalCount;
            }
            showToGUI_HeartsBeats(Start, End);
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            //Next
            if (ObjConnectionList != null)
            {
                Interlocked.Exchange(ref TotalCount, objConnectionList.Count);
            }
            if (End.Equals(TotalCount))
                ;
            else
            {
                if (End + 20 <= TotalCount)
                {
                    Start += 20;
                    End += 20;
                }
                else
                {
                    End = TotalCount;
                    Start += 20;
                    if (Start > End)
                        Start = 0;
                }
            }
            showToGUI_HeartsBeats(Start, End);
        }
        #endregion

        private void grid_MeterHb_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var msn = grid_MeterHb[_MSN, e.RowIndex].Value.ToString();
            
            //if (objConnectionList.ContainsKey(MeterSerialNumber.ConvertFrom(msn)))
            //{
            MeterSerialNumber meter = MeterSerialNumber.ConvertFrom(msn);
            
                var connection = ObjConnectionList[meter];

                             
                //LiveConnectionInfo frm = new LiveConnectionInfo(connection);
                //frm.ShowDialog();
            //}
        }

    }
}
