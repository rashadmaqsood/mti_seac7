using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using SharedCode.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{

    public partial class ucMajorAlarm : UserControl
    {
        //=================================================================================(Constants)

        #region Definded_Constants

        const string r_EventName = "Event_Name";
        const string r_EventCode = "Event_Code";
        const string r_Caution = "Caution_Number";
        const string r_DisplayCaution = "Display_Caution";
        const string r_FlashTime = "Flash_Time";
        const string r_IsMajorAlarm = "IsMajorAlarm";
        const string r_AlarmStatus = "IsTriggered";
        const string r_ResetAlarmStatus = "ResetAlarmStatus";
        const string r_IsFlash = "isFlash";
        const string r_ReadCaution = "Read_caution";
        const string r_IsEnable = "Is_Enable";
        const string r_EventDetails = "EventDetails";
        const string r_EventCounter = "EventCounter";
        const string r_ExcludeLog = "IsExclude_LogBook";

        #endregion

        //=================================================================================(Data Members)

        #region DataMembers
       
        
        private bool ShowtoGUI_Flag;
        private List<EventInfo> ListEventInfo;
        private List<EventLogInfo> ListEventLogInfo;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<Param_EventsCaution> ListEventCautions{ get;  internal set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public EventController Event_Controller { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Param_MajorAlarmProfile Param_MajorAlarmProfile_obj { get; internal set; }

        #endregion

        //=================================================================================(Load Events)

        #region Constructor & load Events
        public ucMajorAlarm()
        {
            InitializeComponent();
            Param_MajorAlarmProfile_obj = new Param_MajorAlarmProfile();
            ListEventInfo = new List<EventInfo>();
            ListEventLogInfo = new List<EventLogInfo>();
            ListEventCautions = new List<Param_EventsCaution>();
        }
        
        private void ucMajorAlarm_Load(object sender, EventArgs e)
        {

            //grid_Events.Columns[r_IsMajorAlarm].Visible = false;
            //grid_Events.Columns[r_ResetAlarmStatus].Visible = false;
        }

        private void grid_Events_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string column = "";
            int row = 0;
            int EventCode = 0;
            int flashTime = 0;
            byte newValue = 0;
            try
            {
                if (ShowtoGUI_Flag)
                {
                    if (true && grid_Events.CurrentCell != null)//if (eventListInitialized)
                    {
                        column = grid_Events.CurrentCell.OwningColumn.Name;
                        row = grid_Events.CurrentCell.RowIndex;

                        //************************************************
                        //verify that entered value is numeric
                        //************************************************
                        try
                        {
                            newValue = Convert.ToByte(grid_Events[column, row].Value);
                        }
                        catch
                        {
                            //grid_Events[column, row].Value = 0;
                            return;
                        }
                        //************************************************
                        //************************************************

                        if (!r_EventName.Equals(column) && !r_EventCode.Equals(column) && !r_IsMajorAlarm.Equals(column) &&
                            !r_DisplayCaution.Equals(column)) //Events Name and Major Alarm checkbox bypassed
                        {
                            newValue = Convert.ToByte(grid_Events[column, row].Value);
                        }
                        if (r_IsMajorAlarm.Equals(column)) //IS Major Alarm
                        {
                            Save_MajorAlarmProfile();
                        }
                        else if (r_ResetAlarmStatus.Equals(column)) //IS Alarm Reset Clicked
                        {
                            Save_MajorAlarmProfile();
                        }
                        grid_Events.CurrentCell.ErrorText = "";
                        if (r_Caution.Equals(column)) //caution number
                        {
                            if (newValue < 0 || newValue > 199)
                            {
                                //newValue = 1;
                                grid_Events.CurrentCell.ErrorText = "value must be between 1 and 199";
                            }
                            else
                            {
                                EventCode = Convert.ToInt32(grid_Events[1, row].Value);
                                for (int k = 0; k < ListEventCautions.Count; k++)
                                {
                                    if (ListEventCautions[k].Event_Code == EventCode)
                                    {
                                        ListEventCautions[k].CautionNumber = newValue;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                            if (r_FlashTime.Equals(column)) //flash time
                            {
                                if (newValue < 0 || newValue > 5)
                                {
                                    newValue = 1;
                                    grid_Events.CurrentCell.ErrorText = "value must be between 1 and 5";
                                }
                                else
                                {
                                    EventCode = Convert.ToInt32(grid_Events[1, row].Value);
                                    for (int k = 0; k < ListEventCautions.Count; k++)
                                    {
                                        if (ListEventCautions[k].Event_Code == EventCode)
                                        {
                                            ListEventCautions[k].FlashTime = newValue;
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (r_ReadCaution.Equals(column))
                            {
                                bool isChecked = Convert.ToBoolean(grid_Events[column, row].Value);
                                EventCode = Convert.ToInt32(grid_Events[r_EventCode, row].Value);
                                for (int k = 0; k < ListEventCautions.Count; k++)
                                {
                                    if (ListEventCautions[k].Event_Code == EventCode)
                                    {
                                        ListEventCautions[k].IsReadCaution = isChecked;
                                        break;
                                    }
                                }
                            }
                            else if (r_IsFlash.Equals(column)) //isFlash
                            {
                                bool isChecked = Convert.ToBoolean(grid_Events[column, row].Value);
                                EventCode = Convert.ToInt32(grid_Events[r_EventCode, row].Value);
                                for (int k = 0; k < ListEventCautions.Count; k++)
                                {
                                    if (ListEventCautions[k].Event_Code == EventCode)
                                    {
                                        ListEventCautions[k].IsFlashCaution = isChecked;
                                        break;
                                    }
                                }
                            }
                            else if (r_DisplayCaution.Equals(column)) //Display Caution
                            {
                                bool isChecked = Convert.ToBoolean(grid_Events[column, row].Value);
                                EventCode = Convert.ToInt32(grid_Events[r_EventCode, row].Value);
                                for (int k = 0; k < ListEventCautions.Count; k++)
                                {
                                    if (ListEventCautions[k].Event_Code == EventCode)
                                    {
                                        ListEventCautions[k].IsDisplayCaution = isChecked;
                                        break;
                                    }
                                }
                            }
                            else if (r_IsEnable.Equals(column)) //Is Disable log Caution
                            {
                                bool isChecked = Convert.ToBoolean(grid_Events[column, row].Value);
                                EventCode = Convert.ToInt32(grid_Events[r_EventCode, row].Value);
                                for (int k = 0; k < ListEventCautions.Count; k++)
                                {
                                    if (ListEventCautions[k].Event_Code == EventCode)
                                    {
                                        ListEventCautions[k].IsDisableLog = isChecked;
                                        break;
                                    }
                                }
                            }
                            else if (r_ExcludeLog.Equals(column)) //Is Disable log Caution
                            {
                                bool isChecked = Convert.ToBoolean(grid_Events[column, row].Value);
                                EventCode = Convert.ToInt32(grid_Events[r_EventCode, row].Value);
                                for (int k = 0; k < ListEventCautions.Count; k++)
                                {
                                    if (ListEventCautions[k].Event_Code == EventCode)
                                    {
                                        ListEventCautions[k].IsExcludeFromLogBook = isChecked;
                                        break;
                                    }
                                }
                            }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        //=================================================================================(Helper Methods)

        #region Helper_Support_Methods

        public void RefreshEventsConfiguration(Param_MajorAlarmProfile majorAlarmProfile = null,
            List<Param_EventsCaution> eventsCautions = null)
        {
            try
            {
                ListEventInfo = Event_Controller.EventInfoList;
                ListEventLogInfo = Event_Controller.EventLogInfoList;
                ListEventLogInfo.Sort((x, y) => x.EventLogIndex.CompareTo(y.EventLogIndex));
                //AddEventItemsToList();
                LoadParams_Events(majorAlarmProfile, eventsCautions);
                ShowtoGUI_Flag = true;
                ShowtoGUI_EventCautions();
                ShowtoGUI_MajorAlarmProfile();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load events configurations", ex);
            }
        }

        private void LoadParams_Events(Param_MajorAlarmProfile majorAlarmProfile = null,
            List<Param_EventsCaution> eventsCautions = null)
        {
            ///ListEventCautions.Clear();
            List<Param_EventsCaution> _ListEventCautions = new List<Param_EventsCaution>();
            for (int i = 0; i < ListEventInfo.Count; i++)
            {
                Param_EventsCaution Param_EventsCaution_obj = new Param_EventsCaution();
                if (ListEventInfo[i].EventId != null)
                {
                    Param_EventsCaution_obj.EventId = ListEventInfo[i].EventId;
                    Param_EventsCaution_obj.Event_Name = ListEventInfo[i].EventName;
                    Param_EventsCaution_obj.Event_Code = ListEventInfo[i].EventCode;
                    Param_EventsCaution_obj.CautionNumber = (byte)ListEventInfo[i].EventCode;
                    _ListEventCautions.Add(Param_EventsCaution_obj);
                }
                ///Param_EventsCaution_obj.Event_Name = ListEventInfo[i].EventName;
                ///Param_EventsCaution_obj.Event_Code = ListEventInfo[i].EventCode;
                ///Param_EventsCaution_obj.CausionNumber = (byte)ListEventInfo[i].EventCode;
                _ListEventCautions.Sort(Param_EventsCaution.Param_EventsCautionSort_Helper);
                if (_ListEventCautions != ListEventCautions)
                    ListEventCautions = _ListEventCautions;
            }
            ///Init Major Alarm Object Using Configurations
            Param_MajorAlarmProfile_obj = new Param_MajorAlarmProfile(ListEventInfo, _ListEventCautions.Count);
            #region ///Populate Param_EventsCaution Loaded

            if (eventsCautions != null && eventsCautions.Count > 0)
            {
                eventsCautions.Sort(Param_EventsCaution.Param_EventsCautionSort_Helper);
                Param_EventsCaution evCautionToModify = null;
                foreach (Param_EventsCaution evCaution in eventsCautions)
                {
                    if (eventsCautions == null)///Skip For Null
                        continue;
                    evCautionToModify = ListEventCautions.Find((x) => x != null && x.EventId == evCaution.EventId);
                    if (evCautionToModify == null)///Skip For Null
                        continue;
                    ///Copy Loaded Param Details
                    evCautionToModify.CautionNumber = evCaution.CautionNumber;
                    evCautionToModify.FlashTime = evCaution.FlashTime;
                    evCautionToModify.Flag = evCaution.Flag;
                }
            }

            #endregion
            #region Populate Param_MajorAlarmProfile Loaded

            if (majorAlarmProfile != null && majorAlarmProfile.AlarmItems.Count > 0)
            {
                majorAlarmProfile.AlarmItems.Sort(Param_MajorAlarmProfile.MajorAlarmSort_Helper);
                MajorAlarm majorAlarmToModify = null;
                foreach (var majorAlarm in majorAlarmProfile.AlarmItems)
                {
                    if (majorAlarm == null)
                        continue;
                    try
                    {
                        majorAlarmToModify = Param_MajorAlarmProfile_obj.AlarmItems.Find((x) => x.Info.EventId == majorAlarm.Info.EventId);
                    }
                    catch
                    {
                        majorAlarmToModify = null;
                    }
                    ///Copy Loaded Param Details
                    if (majorAlarmToModify != null)
                    {
                        majorAlarmToModify.IsMajorAlarm = majorAlarm.IsMajorAlarm;
                        majorAlarmToModify.IsTriggered = majorAlarm.IsTriggered;
                    }
                }
            }

            #endregion
        }

        private void ClearGrid(DataGridView gridName)
        {
            while (gridName.Rows.Count > 0)
            {
                gridName.Rows.RemoveAt(0);
            }
        }

        private void AddEventItemsToList()
        {
            //if (this.IsHandleCreated || true)
            //{
            //    list_Event_SelectableEvents.Items.Clear();
            //    foreach (comm.EventInfo Eventitem in ListEventLogInfo)
            //    {
            //        if (Eventitem.EventCode == 0) continue;
            //        list_Event_SelectableEvents.Items.Add(Eventitem);
            //    }
            //}
        }

        private void Save_MajorAlarmProfile()
        {
            try
            {
                foreach (DataGridViewRow item in grid_Events.Rows)
                {
                    string eventName = Convert.ToString(item.Cells[r_EventName].Value);
                    MajorAlarm Alarm = Param_MajorAlarmProfile_obj.AlarmItems.Find((x) => x.Info != null && x.Info.EventName.Equals(eventName));
                    Param_MajorAlarmProfile_obj.AlarmItems.Remove(Alarm);
                    if (Alarm != null)
                    {
                        Alarm.IsMajorAlarm = Convert.ToBoolean(item.Cells[r_IsMajorAlarm].Value);
                        //if (Alarm.Info.EventId == MeterEvent.TimeBaseEvent_1 ||
                        //   Alarm.Info.EventId == MeterEvent.TimeBaseEvent_2)
                        //{
                        //    Alarm.IsMajorAlarm = true;
                        //}
                        Alarm.IsReset = Convert.ToBoolean(item.Cells[r_ResetAlarmStatus].Value);
                        Param_MajorAlarmProfile_obj.AlarmItems.Add(Alarm);
                    }
                    //for (int k = 0; k < ListEventCautions.Count; k++)
                    //{
                    //    if (ListEventCautions[k].Event_Code == item)
                    //    {
                    //        grid_Events[r_IsMajorAlarm, k].Value = true;
                    //        break;
                    //    }
                    //}
                }
            }
            catch (Exception)
            {
                return;
            }
        }

      
        #endregion

        //=================================================================================(GUI Helpers)

        #region showMethods_Interface

        public void ShowtoGUI_EventCautions()
        {
            try
            {
                if (ShowtoGUI_Flag)
                {
                    ClearGrid(grid_Events);
                    for (int i = 0; i < ListEventCautions.Count; i++)
                    {
                        //if (ListEventCautions[i].EventId == MeterEvent.TimeBaseEvent_1 || ListEventCautions[i].EventId == MeterEvent.TimeBaseEvent_2)
                        //{
                        //    grid_Events.Rows.Add();
                        //    grid_Events[r_EventName, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].Event_Name;
                        //    grid_Events[r_EventCode, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].Event_Code;
                        //    grid_Events[r_Caution, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].CausionNumber;
                        //    grid_Events[r_DisplayCaution, grid_Events.Rows.Count - 1].Value = false;
                        //    grid_Events[r_FlashTime, grid_Events.Rows.Count - 1].Value = "x";
                        //    grid_Events[r_IsMajorAlarm, grid_Events.Rows.Count - 1].Value = true;

                        //    ///Mark As ReadOnly Column
                        //    grid_Events[r_Caution, grid_Events.Rows.Count - 1].ReadOnly = true;
                        //    grid_Events[r_DisplayCaution, grid_Events.Rows.Count - 1].ReadOnly = true;
                        //    grid_Events[r_FlashTime, grid_Events.Rows.Count - 1].ReadOnly = true;
                        //    //  grid_Events[r_IsMajorAlarm, grid_Events.Rows.Count - 1].ReadOnly = true;

                        //}
                        // else 
                        if (ListEventCautions[i].EventId != null)
                        {
                            grid_Events.Rows.Add();
                            grid_Events[r_EventName, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].Event_Name;
                            grid_Events[r_EventCode, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].Event_Code;
                            grid_Events[r_Caution, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].CautionNumber;
                            grid_Events[r_FlashTime, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].FlashTime;
                            // grid_Events[r_ReadCaution, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].to_Read;
                            ///Display Caution
                            grid_Events[r_DisplayCaution, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].IsDisplayCaution;
                            //Display Is Disable flag
                            grid_Events[r_IsEnable, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].IsDisableLog;
                            //display flash 
                            grid_Events[r_IsFlash, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].IsFlashCaution;
                            ///Read Caution
                            grid_Events[r_ReadCaution, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].IsReadCaution;
                            //Exclude From Log Book Flag
                            grid_Events[r_ExcludeLog, grid_Events.Rows.Count - 1].Value = ListEventCautions[i].IsExcludeFromLogBook;
                        }
                    }
                    grid_Events.Sort(Event_Name, ListSortDirection.Ascending);
                    Event_Name.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    // grid_Events.Sort(grid_Events.Columns[r_EventName], ListSortDirection.Ascending);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ShowtoGUI_MajorAlarmProfile()
        {
            ShowtoGUI_Flag = false;
            for (int i = 0; i < grid_Events.Rows.Count - 1; i++)
            {
                grid_Events[r_IsMajorAlarm, i].Value = false;
            }

            //timebase events are Major Alarms by default
            //for (int i = 0; i < grid_Events.Rows.Count - 1; i++)
            //{
            //    if (Convert.ToInt32(grid_Events[r_EventCode, i].Value) == 211 || Convert.ToInt32(grid_Events[r_EventCode, i].Value) == 212)
            //    {
            //        grid_Events[r_IsMajorAlarm, i].Value = true;
            //    }
            //    Application.DoEvents();
            //}

            try
            {
                foreach (DataGridViewRow item in grid_Events.Rows)
                {
                    string eventName = Convert.ToString(item.Cells[r_EventName].Value);
                    if (item.Cells[r_AlarmStatus] != null)
                        item.Cells[r_AlarmStatus].Style.BackColor = item.Cells[r_EventName].Style.BackColor;
                    MajorAlarm Alarm = Param_MajorAlarmProfile_obj.AlarmItems.Find((x) => x.Info != null && x.Info.EventName.Equals(eventName));
                    if (Alarm != null)
                    {
                        //if (Alarm.Info.EventCode != 211 && Alarm.Info.EventCode != 212)
                        //{
                        //    item.Cells[r_IsMajorAlarm].Value = Alarm.IsMajorAlarm;
                        //}
                        item.Cells[r_IsMajorAlarm].Value = Alarm.IsMajorAlarm;
                        item.Cells[r_AlarmStatus].Value = (Alarm.IsTriggered) ? "TRG" : "RST";
                        item.Cells[r_AlarmStatus].Style.BackColor = (Alarm.IsTriggered) ?
                            Color.Red : item.Cells[r_EventName].Style.BackColor;

                        if (Alarm.IsTriggered)
                        {
                            item.Cells[r_ResetAlarmStatus].Value = Alarm.IsReset;
                            item.Cells[r_ResetAlarmStatus].ReadOnly = false;
                        }
                        else
                        {
                            item.Cells[r_ResetAlarmStatus].Value = false;
                            item.Cells[r_ResetAlarmStatus].ReadOnly = true;
                        }
                    }
                    //for (int k = 0; k < ListEventCautions.Count; k++)
                    //{
                    //    if (ListEventCautions[k].Event_Code == item)
                    //    {
                    //        grid_Events[r_IsMajorAlarm, k].Value = true;
                    //        break;
                    //    }
                    //}
                }
                ShowtoGUI_Flag = true;
            }
            catch (Exception)
            {
                ShowtoGUI_Flag = true;
                return;
            }
            finally
            {
                ShowtoGUI_Flag = true;
            }
        }

        #endregion

        //=================================================================================(Common Methods)

        #region Common Methods

        public void InitMajorAlarm() 
        {
            RefreshEventsConfiguration(Param_MajorAlarmProfile_obj,ListEventCautions);
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

    }
}
