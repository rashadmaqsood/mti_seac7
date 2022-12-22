using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using DLMS;
using SmartEyeControl_7;
using DatabaseConfiguration.DataSet;
using SharedCode.Comm.DataContainer;
using SharedCode.Controllers;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;

namespace ucCustomControl
{
    public partial class ucStatusWordMap : UserControl
    {
        ApplicationController obj_ApplicationController;
        Configs ConfigurationsLocal = null;
        List<StatusWord> statusWordItems1;
        List<StatusWord> statusWordItems2;
        DLMS_Application_Process Application_Process;
        ParameterController Param_Controller;
        Param_StatusWordMap Param_status_word_map_object;
        StatusWordMapType _statusWordMapType;

        public ApplicationController Obj_ApplicationController
        {
            get { return obj_ApplicationController; }
            set 
            {
                obj_ApplicationController = value;
                if ( obj_ApplicationController != null )
                    this.InitObjects();
            }
        }

        public StatusWordMapType StatusWordMap_Type
        {
            get { return _statusWordMapType; }
            set { _statusWordMapType = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<StatusWord> StatusWordItems1
        {
            get { return statusWordItems1; }
            set { statusWordItems1 = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<StatusWord> StatusWordItems2
        {
            get { return statusWordItems2; }
            set { statusWordItems2 = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<AccessRights> AccessRights
        {
            get;
            set;
        }

        public ucStatusWordMap()
        {
            InitializeComponent();
            ApplicationLookAndFeel.UseTheme(this);
            statusWordItems1 = new List<StatusWord>();
            statusWordItems2 = new List<StatusWord>();

            this.cmbStatusWordList.SelectedIndex = 0;

        }

        private void InitObjects()
        {
            try
            {
                if (obj_ApplicationController != null)
                {
                    ConfigurationsLocal = obj_ApplicationController.Configurations;
                    Application_Process = obj_ApplicationController.Applicationprocess_Controller.ApplicationProcess;
                    Param_Controller = obj_ApplicationController.Param_Controller;
                }

                foreach (Configs.Status_WordRow row in ConfigurationsLocal.Status_Word)
                {
                    list_AvailableStatausWord.Items.Add(row);
                }
            }
            catch (Exception ex)
            {

                Notification notifier = new Notification("Param Status Word Map", ex.Message);
            }
        }

        private void btnAddStatusWord_Click(object sender, EventArgs e)
        {
            if (list_AvailableStatausWord.SelectedIndex != -1)
            {

                Configs.Status_WordRow selectedItem = (Configs.Status_WordRow)list_AvailableStatausWord.SelectedItem;
                StatusWord statusWord = new StatusWord(selectedItem.Name, (byte)selectedItem.Code);
                updateStatusWordGrid(statusWord);

            }


        }
        public void updateStatusWordGrid(StatusWord statusWord)
        {
            if (_statusWordMapType == StatusWordMapType.StatusWordMap_1)
            {
                StatusWord word = (from t in this.statusWordItems1 where t.Code == statusWord.Code select t).FirstOrDefault();
                if (word == null)
                {
                    this.statusWordItems1.Add(statusWord);
                    this.RefreshGridView();
                }
                else
                {
                    Notification notifier = new Notification("Param Status Word Map", "Item already selected");
                }
            }
            else if (_statusWordMapType == StatusWordMapType.StatusWordMap_2)
            {
                StatusWord word = (from t in this.statusWordItems2 where t.Code == statusWord.Code select t).FirstOrDefault();
                if (word == null)
                {
                    this.statusWordItems2.Add(statusWord);
                    this.RefreshGridView();
                }
                else
                {
                    Notification notifier = new Notification("Param Status Word Map", "Item already selected");
                }
            }
        }

        private void btnRemoveStatusWord_Click(object sender, EventArgs e)
        {
            try
            {
                int i = -1;
                if (this.dgvSelectedStatusWord.CurrentRow != null)
                    i = dgvSelectedStatusWord.CurrentRow.Index;
                if (i > -1)
                {
                    if (_statusWordMapType == StatusWordMapType.StatusWordMap_1)
                    {
                        this.statusWordItems1.RemoveAt(i);
                        this.RefreshGridView();
                    }
                    else if (_statusWordMapType == StatusWordMapType.StatusWordMap_2)
                    {
                        this.statusWordItems2.RemoveAt(i);
                        this.RefreshGridView();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshGridView()
        {
            this.dgvSelectedStatusWord.DataSource = null;
            if (_statusWordMapType == StatusWordMapType.StatusWordMap_1)
            {
                this.dgvSelectedStatusWord.DataSource = statusWordItems1;
                this.lblStatusWordMapCount.Text = statusWordItems1.Count.ToString();
            }
            else if (_statusWordMapType == StatusWordMapType.StatusWordMap_2)
            {
                this.dgvSelectedStatusWord.DataSource = statusWordItems2;
                this.lblStatusWordMapCount.Text = statusWordItems2.Count.ToString();
            }
           
        }

        private void btnRemoveAllStatusWord_Click(object sender, EventArgs e)
        {
            if (_statusWordMapType == StatusWordMapType.StatusWordMap_1)
            {
                this.statusWordItems1.Clear();
            }
            else if (_statusWordMapType == StatusWordMapType.StatusWordMap_2)
            {
                this.statusWordItems2.Clear();
            }
            this.RefreshGridView();
        }

        public void showToGUI_StatusWord(StatusWordMapType type)
        {
            if (type == StatusWordMapType.StatusWordMap_1)
            {
                foreach (StatusWord item in this.statusWordItems1)
                {
                    item.Name = ((Configs.Status_WordRow[])ConfigurationsLocal.Status_Word.Select(string.Format("Code={0}", item.Code)))[0].Name;
                }
            }
            else if (type == StatusWordMapType.StatusWordMap_2)
            {
                foreach (StatusWord item in this.statusWordItems2)
                {
                    item.Name = ((Configs.Status_WordRow[])ConfigurationsLocal.Status_Word.Select(string.Format("Code={0}", item.Code)))[0].Name;
                }
            }
            this.RefreshGridView();
        }

        private void bgwGetStatusWord_DoWork(object sender, DoWorkEventArgs e)
        {
            Param_status_word_map_object = new Param_StatusWordMap();
            Param_Controller.GET_Status_Word(ref Param_status_word_map_object, _statusWordMapType);
            if (_statusWordMapType == StatusWordMapType.StatusWordMap_1)
                this.StatusWordItems1 = Param_status_word_map_object.StatusWordList;
            else if (_statusWordMapType == StatusWordMapType.StatusWordMap_2)
                this.StatusWordItems2 = Param_status_word_map_object.StatusWordList;
        }

        private void bgwGetStatusWord_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Notification notifier = new Notification("Get Status Word", "Complete Process");
            this.btnGetStatusWord.Enabled = true;
            showToGUI_StatusWord(StatusWordMapType.StatusWordMap_1);
            showToGUI_StatusWord(StatusWordMapType.StatusWordMap_2);
        }

        private void btnGetStatusWord_Click(object sender, EventArgs e)
        {
            if (Application_Process != null && !(Application_Process.Is_Association_Developed))
            {
                Notification notifier = new Notification("Association Error", "Create Application Association");
            }
            else
            {
                this.btnGetStatusWord.Enabled = false;
                bgwGetStatusWord.RunWorkerAsync();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbStatusWordList.SelectedIndex == 0)
            {
                _statusWordMapType = StatusWordMapType.StatusWordMap_1;
                RefreshGridView();
            }
            else if (this.cmbStatusWordList.SelectedIndex == 1)
            {
                _statusWordMapType = StatusWordMapType.StatusWordMap_2;
                RefreshGridView();
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            try
            {
                AccessRights = Rights;
                this.SuspendLayout();
                //    //this.fLP_Main.SuspendLayout();

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    //        #region Initialize Combo_control_TBE

                    //        cmbStatusWordList.Items.Clear();

                    //if (IsControlRDEnable(eStatusWordWindow.ComboOptionStatusWordMap1) || IsControlWTEnable(eStatusWordWindow.ComboOptionStatusWordMap1))
                    //    combo_control_TBE.Items.Add(DisableStr);

                    //        if (IsControlRDEnable(MeterScheduling.Disable) || IsControlWTEnable(MeterScheduling.Disable))
                    //            combo_control_TBE.Items.Add(DisableStr);
                    //        if (IsControlRDEnable(MeterScheduling.DateTime) || IsControlWTEnable(MeterScheduling.DateTime))
                    //            combo_control_TBE.Items.Add(DateTimeStr);
                    //        if (IsControlRDEnable(MeterScheduling.TimeInterval) || IsControlWTEnable(MeterScheduling.TimeInterval))
                    //            combo_control_TBE.Items.Add(TimeIntervalStr);
                    //        if (IsControlRDEnable(MeterScheduling.TimeInterval_Sink) || IsControlWTEnable(MeterScheduling.TimeInterval_Sink))
                    //            combo_control_TBE.Items.Add(TimeIntervalSinkStr);
                    //        if (IsControlRDEnable(MeterScheduling.TimeInterval_Fixed) || IsControlWTEnable(MeterScheduling.TimeInterval_Fixed))
                    //            combo_control_TBE.Items.Add(TimeIntervalFixedStr);

                    //        #endregion
                    //        ShowtoGUI_TBE();
                    return true;
                }
                return false;
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        #endregion

        #region Support_Fuction

        private bool IsControlWTEnable(MeterScheduling type)
        {
            bool isEnable = false;
            try
            {
                AccessRights right = AccessRights.Find((x) => (MeterScheduling)Enum.Parse(x.QuantityType, x.QuantityName) == type);
                if (right != null)
                    isEnable = right.Write;
            }
            catch { }
            return isEnable;
        }

        public bool IsControlRDEnable(MeterScheduling type)
        {
            bool isEnable = false;
            try
            {
                AccessRights right = AccessRights.Find((x) => (MeterScheduling)Enum.Parse(x.QuantityType, x.QuantityName) == type);
                if (right != null)
                    isEnable = right.Read;
            }
            catch { }
            return isEnable;
        }

        #endregion
    }


}
