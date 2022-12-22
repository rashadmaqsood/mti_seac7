using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLMS.Comm;
using DLMS;
using ucCustomControl;
using SharedCode.Controllers;
using System.Collections;
using SharedCode.Comm.Param;
using SharedCode.Comm.HelperClasses;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucScheduleTableEntry : UserControl
    {
        #region Properties
        public Param_Load_Scheduling ParamLoadScheduling { 
            get
            {
                return paramLoadScheduling;
            }
        }

        #endregion

        #region Class Members
            public List<ScheduleEntry> ListOfEntries;
            public ParameterController paramController;

            private ScheduleEntry _selectedEntry;
            
            //private 
            private int ScheduleEntryNo = 0;
            private Param_Load_Scheduling paramLoadScheduling;
            private const int _MAX_TABLE_ENTRIES = 12;
         #endregion Properties


        #region Constructor
        public ucScheduleTableEntry()
        {
            ListOfEntries = new List<ScheduleEntry>();
            paramLoadScheduling = new Param_Load_Scheduling();

            InitializeComponent();  // Defualt Controller initialization
            InitComponentCustom();  // Custom initialization

          // AddDummyEntries(); // Dummy Schedules
            
        } // ** End Constructor 'ucScheduleTableEntry()' **

        #endregion
        

        #region Public Methods

        /// <summary>
        /// bind List of <see cref="ScheduleEntry "/> with comboBox
        /// </summary>
        public void BindObjects(List<ScheduleEntry> list)
        {
            cmbIndex.DataSource = null;
            cmbIndex.DataSource = list;

            ListOfEntries = list;      // for usage of class level 

            cmbIndex.DisplayMember = "Index";   // 'Index' is property of class 'ScheduleEntry'
            cmbIndex.ValueMember = "Index";

        }


        #endregion Public Methods


        #region Private Methods

        /// <summary>
        /// all custom class level initialization is done in this method
        /// </summary>
        private void InitComponentCustom()
        {
            paramController = new ParameterController();

            #region Tab_1 (Debug) initializations

            toolTipAddNewSchedule.SetToolTip(btnAddNewSchedule, "Add New Schedule Entry");

            // ** Show Date's Attribute && Hide Time's Attribute of >>"Start & End Date"<< **
            ucBeginDate.showYear      = true;      ucEndDate.showYear      = true;
            ucBeginDate.showMonth     = true;      ucEndDate.showMonth     = true;
            ucBeginDate.showDate      = true;      ucEndDate.showDate      = true;
            //ucBeginDate.showDayOfWeek = true;      //ucEndDate.showDayOfWeek = true;

            ucBeginDate.showHour      = false;     ucEndDate.showHour      = false;
            ucBeginDate.showMinute    = false;     ucEndDate.showMinute    = false;
            ucBeginDate.showSeconds   = false;     ucEndDate.showSeconds   = false;
            ucBeginDate.showHundredth = false;     ucEndDate.showHundredth = false;
            //ucBeginDate.Month_NotSpecified = false;      // ucEndDate.Month_NotSpecified = false;
            //ucBeginDate.Date_NotSpecified = false;      //ucEndDate.Date_NotSpecified = false;

            

            // ** Show Time's Attribute && Hide Date's Attribute of >>"Switch Time"<< **
            ucSwitchTime.showHour   = true;
            ucSwitchTime.showMinute = true;
            
            ucSwitchTime.showDate = 
            ucSwitchTime.showMonth =    
            ucSwitchTime.showYear =     
            ucSwitchTime.showDayOfWeek =
            ucSwitchTime.showHundredth = false;

            //ucBeginDate.showDatetime();
            //ucEndDate.showDatetime();
            //ucSwitchTime.showDatetime();

            #endregion Tab_1 End

            #region Tab_2 (Schedule) initialization

            txtBeginDate.Value = DateTime.Today;
            txtEndDate.Value   = DateTime.Today;// DateTime.Today;
             
            DateChanged();
            startTimeChanged(new object(), new EventArgs()); // populate interval combobox at startup
            
            #endregion Tab_2 End
        }

        private void DateChanged()
        {
            paramLoadScheduling.StartDate = txtBeginDate.Value;
            paramLoadScheduling.EndDate = txtEndDate.Value;
        }
        private void AddDummyEntries()
        {
            for(int i=0; i< 5; i++)
            {
                var obj = new ScheduleEntry();
                obj.Index = ((ushort)(ListOfEntries.Count + 1));
                obj.LogicalName = Get_Index.Contactor_Script_Table;
                // obj.LogicalName = StOBISCode.ConvertFrom( ("0.0.1.0.0.266"));// string.Format("0.0.0.0.0.{0}" , i) );
                obj.ScriptSelector = (byte)((i % 2 == 0) ? 2 : 1);

                var time  = new StDateTime();
                time.Hour = time.Minute = time.Second = (byte) (i+1);
                obj.SwitchTime = time;

                //var bDate = new StDateTime();

                //bDate.Year  = Convert.ToUInt16(2001 + i);
                //bDate.Month = (byte)(i + 1);
                //bDate.DayOfMonth = (byte)(i + 1);
                //bDate.DayOfWeek = (byte)(i+ 1);

                //var eDate = new StDateTime(bDate);

                //obj.BeginDate = bDate;
                //obj.EndDate   = eDate;

                this.ListOfEntries.Add( obj);
                
            }
            this.BindObjects(ListOfEntries);
        }


        #region Tab_1 private Methods

        private void btnAddNewSchedule_Click(object sender, EventArgs e)
        {
            AddEntryInSingleList();
        }

        private void AddEntryInSingleList()
        {
            BitArray ExecWeekDays = new BitArray(ScheduleEntry.WeekDaysCount);
            BitArray ExecSpecialDays = new BitArray(0);

            for (int i = 0; i < ExecWeekDays.Count; i++)
                ExecWeekDays[i] = true;

            this.ListOfEntries.Add(new ScheduleEntry()
            {
                Index = ((ushort)(ListOfEntries.Count + 1)),
                LogicalName = Get_Index.Contactor_Script_Table,
                ExecWeekdays = ExecWeekDays,
                ExecSpecialDays = ExecSpecialDays

            });
            this.BindObjects(ListOfEntries);

            cmbIndex.SelectedIndex = cmbIndex.Items.Count - 1;
            //MessageBox.Show(""+_listOfEntries.Count);
        }

        private void cmbIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIndex.SelectedIndex >= 0)
            {
                //MessageBox.Show("Subhan Allah");

                _selectedEntry = ListOfEntries.Find(x => x.Index == Convert.ToUInt16(cmbIndex.SelectedValue.ToString().Trim())); // FirstOrDefault(x => x.Index == Convert.ToUInt16(cmbIndex.SelectedItem.ToString()));
                PopulateScheduleGUI(_selectedEntry);
            }
        }

        /// <summary>
        /// Update All GUI Fields with Data of 'selected Index' ScheduleEntry
        /// </summary>
        /// <param name="selectedEntry"> Data of This Schedule will display in all fields </param>
        private void PopulateScheduleGUI(ScheduleEntry selectedEntry)
        {
            if (selectedEntry != null)
            {
                chkEnable.Checked = selectedEntry.Enable;
                chkSelector.Checked = (selectedEntry.ScriptSelector == 2) ? true : false;

                ucBeginDate.DateTimeStruct = selectedEntry.BeginDate;
                ucEndDate.DateTimeStruct = selectedEntry.EndDate;
                ucSwitchTime.DateTimeStruct = selectedEntry.SwitchTime;

                txtLogicalName.Text = selectedEntry.LogicalName.ToString();
                //MessageBox.Show(selectedEntry.LogicalName.ToString());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (paramController != null)
                {
                    //  MessageBox.Show("in if");
                    paramController.SET_SchedulerTable(Get_Index.SCHEDULE_TABLE, ListOfEntries);
                }
                //  MessageBox.Show("out if");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Tab_1 Single Control Changed functions

        private void LogicalNameChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Subhan Allah");
            if (_selectedEntry != null)
            {

                try
                {
                    _selectedEntry.LogicalName = StOBISCode.ConvertFrom(txtLogicalName.Text);
                }
                catch (Exception) { }

            }
        }

        private void ScriptSelectorChanged(object sender, EventArgs e)
        {
            // MessageBox.Show("Subhan Allah");
            if (_selectedEntry != null)
            {
                _selectedEntry.ScriptSelector = (byte)((chkSelector.Checked == true) ? 2 : 1);
            }
        }

        private void EnableChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Subhan Allah");
            if (_selectedEntry != null)
            {
                _selectedEntry.Enable = chkEnable.Checked;
            }
        }

        private void SwitchTimeChanged(object sender, EventArgs e)
        {
            if (_selectedEntry != null)
            {
                _selectedEntry.SwitchTime = ucSwitchTime.DateTimeStruct;
            }
            //MessageBox.Show("Subhan Allah" + _selectedEntry.SwitchTime.ToString());
        }

        private void BeginDateChanged(object sender, EventArgs e)
        {
            updateEntryDate('B');   // Save Begin Date
        }
        private void EndDateChanged(object sender, EventArgs e)
        {
            updateEntryDate('E');   // Save End Date
        }

        /// <summary>
        /// update Date of Selected Entry
        /// </summary>
        /// <param name="ch"> will update Begin Date, if ch is 'B'. 
        /// Will Update End Date ,if ch is 'E'</param>
        private void updateEntryDate(char ch)
        {
            if (_selectedEntry != null)
            {
                if (ch == 'B')    // update 'Begin Date' 
                {
                    _selectedEntry.BeginDate = ucBeginDate.DateTimeStruct;
                }
                else    // update 'End Date'
                {
                    _selectedEntry.EndDate = ucEndDate.DateTimeStruct;
                }
            }
        }

        #endregion Single Control Changed functions


        #endregion Tab_1 private Methods


        #region Tab_2 private Methods

        ///private void startTimeChanged(object sender, EventArgs e)
        //{
        //    int tillMinutes = 0;
        //    if (dgView.Rows.Count == 0)
        //    {
        //        tillMinutes = 1440; // 60 * 24 = 1440 (minutes in A day)
        //    }
        //    else
        //    {
        //        //int usedMinutes = 0;
        //        //foreach (var obj in paramLoadScheduling.ListLoadScheduling)
        //        //{
        //        //    usedMinutes += obj.Interval;
        //        //}
        //        //tillMinutes = 1440 - usedMinutes; // 60 * 24 = 1440 (minutes in A day)


        //        int hours = txtStartTime.Value.Hour;
        //        int minutes = txtStartTime.Value.Minute;

        //        if (paramLoadScheduling.ListLoadScheduling.Count > 0)
        //        {
        //            TimeSpan firstEntryTime = paramLoadScheduling.ListLoadScheduling.ElementAt(0).StartTime;

        //            hours = (hours == 0) ? 24 : hours; // Bcz 00 hours behaves unexpected...
        //            hours = Math.Abs(firstEntryTime.Hours - hours);
        //            minutes = Math.Abs(firstEntryTime.Minutes - minutes);
        //        }

        //        tillMinutes = (1440 - ((hours * 60) + minutes)); // 60 * 24 = 1440 (minutes in A day)
        //        tillMinutes = (tillMinutes < 0) ? 1440 : tillMinutes;

        //    }

        //    populateCmbIntervalTill( tillMinutes );
        ///}
        

        private void startTimeChanged(object sender, EventArgs e)
        {
            //int tillMinutes = 0;
            //if (dgView.Rows.Count == 0)
            //{
            //    tillMinutes = 1440; // 60 * 24 = 1440 (minutes in A day)
            //}
            //else
            //{
            //    int hours = txtStartTime.Value.Hour;
            //    int minutes = txtStartTime.Value.Minute;

            //    if (paramLoadScheduling.ListLoadScheduling.Count > 0)
            //    {
            //        TimeSpan firstEntryTime = paramLoadScheduling.ListLoadScheduling.ElementAt(0).StartTime;

            //        hours = (hours == 0) ? 24 : hours; // Bcz 00 hours behaves unexpected...
            //        hours = Math.Abs(firstEntryTime.Hours - hours);
            //        minutes = Math.Abs(firstEntryTime.Minutes - minutes);
            //    }

            //    tillMinutes = (1440 - ((hours * 60) + minutes)); // 60 * 24 = 1440 (minutes in A day)
            //    tillMinutes = (tillMinutes < 0) ? 1440 : tillMinutes;

            //}

            //populateCmbIntervalTill( tillMinutes );
        }

        //private void populateCmbIntervalTill(int till_Minutes)
        //{
        //    cmbInterval.Items.Clear();  // clear previous data

        //    for (int i = 1; i <= till_Minutes; i++)
        //    {
        //        cmbInterval.Items.Add(i);
        //    }

        //   // if (till_Minutes >= 1)
        //    //{
        //        cmbInterval.SelectedIndex = 0; 
        //    //}
        //}

        private void btnAddToTable_Click(object sender, EventArgs e)
        {
            LoadScheduling newEntry = new LoadScheduling();
            newEntry.StartTime = txtStartTime.Value.TimeOfDay;
            newEntry.Interval = ( (txtInterval.Value.Hour * 60) + txtInterval.Value.Minute);// Convert.ToInt32(cmbInterval.SelectedItem.ToString().Trim());

            try
            {
                if(newEntry.Interval == 0)
                {
                    throw new Exception("Interval should not be 0 !");
                }

                int overlapIndex = paramLoadScheduling.Add(newEntry);
                if (overlapIndex == -1) // No Overlap Occures (Correctly Added)
                {
                    ShowToGUI(paramLoadScheduling.ListLoadScheduling);
                }
                else  // Overlap Occures With
                {
                    MessageBox.Show(string.Format("Entry Overlapped with Sr # {0}", (++overlapIndex) ));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
            }
        }

        /*IsEntryAddedInTable()
        /// <summary>
        /// returns True, if Entry Added successfully in Table. Otherwise returns False.
        /// </summary>
        /// <returns></returns>
        private bool IsEntryAddedInTable()
        {
            if ( _tableEntries < _MAX_TABLE_ENTRIES)
            {
                if (!IsOverlapTime())
                {
                    AddEntryInTable(txtStartTime.Text, cmbInterval.SelectedItem.ToString());
                    return true;
                }
                //else  
                   // MessageBox.Show("Overlap Time with previous!");
            }
            else
            {
                MessageBox.Show(string.Format("Maximum '{0}' Entries Allowed", _MAX_TABLE_ENTRIES));
            }
            return false;
        }
        */

        /// <summary>
        /// insert a row in Table with given Values
        /// </summary>
        /// <param name="time"> time in string format i.e.,   hh:mm   </param>
        /// <param name="interval"> interval between Start Time and End time </param>
        private void AddEntryInTable(string start_time,string end_time, string interval)
        {
            ScheduleEntryNo++;

            dgView.Rows.Add();
            int RowIndex = dgView.RowCount - 1;
            DataGridViewRow R = dgView.Rows[RowIndex];
            R.Cells["SrNo"].Value = ScheduleEntryNo;
            R.Cells["colStartTime"].Value = start_time;
            R.Cells["colEndTime"].Value = end_time;
            R.Cells["colInterval"].Value = interval;
            ((DataGridViewImageCell)R.Cells["colRemove"]).Value = OptocomSoftware.Properties.Resources.red_cross_icon; // "delete";
            //((DataGridViewImageCell)gvFiles.Rows[row].Cells[1]).Value =
        }

        private void DeleteEntryFromTable(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                //empid = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["EmpId"].Value);
                paramLoadScheduling.Remove(e.RowIndex);
                ShowToGUI(paramLoadScheduling.ListLoadScheduling);
            }
        }

        private void ClearTableEntries(object sender, LinkLabelLinkClickedEventArgs e)
        {
            paramLoadScheduling.Clear();
            ShowToGUI(paramLoadScheduling.ListLoadScheduling);
            //populateCmbIntervalTill(1440);
        }

        internal void ShowGetEntriesInTable(List<ScheduleEntry> list)
        {
            if (list.Count > 0)
            {
                DateTime tempBeginDate = new DateTime((list[0].BeginDate.Year), list[0].BeginDate.Month, list[0].BeginDate.DayOfMonth);
                DateTime tempEndDate   = new DateTime((list[0].EndDate.Year)  , list[0].EndDate.Month  , list[0].EndDate.DayOfMonth  );

                txtBeginDate.Value = tempBeginDate;
                txtEndDate.Value = tempEndDate;

                paramLoadScheduling.ListOfEntries = list;
                ShowToGUI(paramLoadScheduling.ListLoadScheduling); 
            }
        }

        internal void ShowToGUI(Param_Load_Scheduling param_Load_Scheduling)
        {
            this.paramLoadScheduling = param_Load_Scheduling;
            if (paramLoadScheduling != null && paramLoadScheduling.ListLoadScheduling != null)
            {
                if (param_Load_Scheduling.ListOfEntries.Count > 0)
                {
                    ScheduleEntry dummyEntry = param_Load_Scheduling.ListOfEntries.ElementAt(0);// new ScheduleEntry(param_Load_Scheduling.ListOfEntries.ElementAt(0));

                    DateTime tempBeginDate = new DateTime((dummyEntry.BeginDate.Year), dummyEntry.BeginDate.Month, dummyEntry.BeginDate.DayOfMonth);
                    DateTime tempEndDate = new DateTime((dummyEntry.EndDate.Year), dummyEntry.EndDate.Month, dummyEntry.EndDate.DayOfMonth);

                    txtBeginDate.Value = tempBeginDate;
                    txtEndDate.Value = tempEndDate;

                    ShowToGUI(paramLoadScheduling.ListLoadScheduling); 
                }
            }
        }

        private void ShowToGUI(List<LoadScheduling> list)
        {
            if (list != null && list.Count > 0)
            {
                dgView.Rows.Clear();
                dgView.Refresh();
                ScheduleEntryNo = 0;

                //MessageBox.Show("Inserting New Data in Table After removing All");

                foreach (var schedule in list) // (int i=0; i < list.Count; i = i + 2 ) 
                {
                    string start_time = string.Format("{0:00}:{1:00}", schedule.StartTime.Hours, schedule.StartTime.Minutes);
                    string end_time   = string.Format("{0:00}:{1:00}", schedule.EndTime.Hours  , schedule.EndTime.Minutes);
                    string interval = schedule.Interval.ToString();

                    AddEntryInTable(start_time, end_time, interval);
                }
            }
            else
            {
                dgView.Rows.Clear();
            }
        }

        private void txtBeginDate_ValueChanged(object sender, EventArgs e)
        {
            DateChanged();
        }

        

        /*IsOverlapTime()
        private bool IsOverlapTime()
        {
            if (dgView.Rows.Count == 0)
            {
                return false;   // no need for overlap's checks
            }
            else //if (dgView.Rows.Count > 0)
            {
                var lastTimeString = dgView.Rows[(_tableEntries - 1)].Cells[1].Value.ToString().Trim();
                var lastIntervalString = dgView.Rows[(_tableEntries - 1)].Cells[2].Value.ToString().Trim();

                var prevTime = lastTimeString.Split(':');
                int prevHours = Convert.ToInt32(prevTime[0]);
                int prevMints = Convert.ToInt32(prevTime[1]);
                int prevInterval = Convert.ToInt32(lastIntervalString);

                int newHours = (byte)txtStartTime.Value.Hour;
                int newMints = (byte)txtStartTime.Value.Minute;
                int newInterval = Convert.ToInt32(cmbInterval.SelectedItem.ToString().Trim());

                StDateTime prevStartTime = new StDateTime();
                prevStartTime.Hour = (byte)prevHours;
                prevStartTime.Minute = (byte)prevMints;


                StDateTime prevEndTime = new StDateTime(prevStartTime);
                // prevEndTime.AddSeconds(prevInterval * 60); //conversion from minutes to seconds

                #region Now Function use instead (not now)
                int extraHours = prevInterval / 60;
                int extraMins = prevInterval % 60;

                prevEndTime.Hour = (byte)(prevEndTime.Hour + extraHours);
                prevEndTime.Minute = (byte)(prevEndTime.Minute + extraMins);
                #endregion



                //StDateTime newTime = new StDateTime();
                //newTime.Hour = (byte)newHours;
                //newTime.Minute = (byte)newMints;

                if ((newHours >= prevStartTime.Hour) && (newHours <= prevEndTime.Hour))
                {
                    if ((newMints >= prevStartTime.Minute) && (newMints <= prevEndTime.Minute))
                    {
                        MessageBox.Show(string.Format("!!  Time OverLaps!! \n\n you cannot select entry \n\n From Range \t{0}:{1}\n To Range \t{2}:{3}", prevStartTime.Hour, prevStartTime.Minute, prevEndTime.Hour, prevEndTime.Minute));
                        return true; // new entry Overlaps
                    }
                }
                if ((newHours <= prevStartTime.Hour) && (newMints <= prevStartTime.Minute))
                {
                    MessageBox.Show(string.Format("select entry Greater than Time {0}:{1}", prevHours, prevMints));
                    return true; // new entry Overlaps - 
                }

                //MessageBox.Show("Correct Time Entry :-)");
                return false;

                //MessageBox.Show(string.Format("Last Table Entry is \n\n Time: {0}\n Hour:{1} \n Minutes: {2}\n Interval: {3}", lastTimeString, prevHours, prevMints, prevInterval));
            }
        } // end isOverlapTime()
        
        */

        /*AddEntryWithSelector(bool selector)
       private void AddEntryWithSelector(bool selector)
       {
           BitArray ExecWeekDays = new BitArray(ScheduleEntry.WeekDaysCount);
           BitArray ExecSpecialDays = new BitArray(0);

           for (int i = 0; i < ExecWeekDays.Count; i++)
               ExecWeekDays[i] = true;

           StDateTime startDate = new StDateTime();                    StDateTime endDate = new StDateTime();

           startDate.DayOfMonth = (byte)txtBeginDate.Value.Day;        endDate.DayOfMonth = (byte)txtEndDate.Value.Day;
           startDate.Month      = (byte)txtBeginDate.Value.Month;      endDate.Month      = (byte)txtEndDate.Value.Month;

           ScheduleEntry obj = new ScheduleEntry()
           {
               Index = ((ushort)(ListOfEntries.Count + 1)),
               LogicalName = Get_Index.Contactor_Script_Table,
               ExecWeekdays = ExecWeekDays,
               ExecSpecialDays = ExecSpecialDays,
               BeginDate = startDate,
               EndDate = endDate
           };


           var time = new StDateTime();

           if (selector) // selector On
           {
               obj.ScriptSelector = (byte) 2 ;

               int intervalMinutes = Convert.ToInt16(cmbInterval.SelectedItem.ToString());

               #region Now Function use instead
               int extraHours = intervalMinutes / 60;
               int extraMins  = intervalMinutes % 60;

               time.Hour   = (byte)(txtStartTime.Value.Hour   + extraHours);
               time.Minute = (byte)(txtStartTime.Value.Minute + extraMins);
               #endregion

               //time.AddSeconds(intervalMinutes * 60); // convert Minutes to seconds

           }
           else    // selector Off
           {
               obj.ScriptSelector = (byte) 1 ;

               time.Hour   = (byte)(txtStartTime.Value.Hour);
               time.Minute = (byte)(txtStartTime.Value.Minute);
           }

           obj.SwitchTime = time;

           this.ListOfDualEntries.Add( obj );

           //this.BindObjects(ListOfEntries);
       }
       */


        #endregion Tab_2 private Methods

        #endregion Private Methods

    } // End Class ucScheduleTableEntry
}
