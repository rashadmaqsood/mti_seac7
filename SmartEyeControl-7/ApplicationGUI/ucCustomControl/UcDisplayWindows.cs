using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SmartEyeControl_7.Common;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.Param;
using SharedCode.Comm.HelperClasses;
using SharedCode.Controllers;
using SharedCode.Common;
using SEAC.Common;
using SmartEyeControl_7.Reporting;
using OptocomSoftware.Reporting;

namespace ucCustomControl
{
    public partial class ucDisplayWindows : UserControl
    {
        #region Data_Members

        private DisplayWindows obj_displayWindows_normal;
        private DisplayWindows obj_displayWindows_alternate;
        private DisplayWindows obj_displayWindows_test;
        ApplicationController obj_ApplicationController;
        public string msn;
        public string metermodel;
        Param_Customer_Code obj_CustomerCode;
        Instantaneous_Class Instantaneous_Class_obj;

        DisplayWindowsHelper obj_DisplayWindowHelper;
        DisplayWindowItem obj_displayWindowItem;
        WindowNumber obj_WindowNumber;
        private string customercode;
        private ParameterController param_Controller;
        private string activeSeason;

        #endregion

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ApplicationController Obj_ApplicationController
        {
            get { return obj_ApplicationController; }
            set { obj_ApplicationController = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DisplayWindows Obj_displayWindows_test
        {
            get { return obj_displayWindows_test; }
            set { obj_displayWindows_test = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DisplayWindows Obj_displayWindows_normal
        {
            get { return obj_displayWindows_normal; }
            set { obj_displayWindows_normal = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DisplayWindows Obj_displayWindows_alternate
        {
            get { return obj_displayWindows_alternate; }
            set { obj_displayWindows_alternate = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ParameterController Param_Controller
        {
            get { return param_Controller; }
            set { param_Controller = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<AccessRights> AccessRights { get; set; }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    String ErrorMessage = null;
                    ///Validate txt_Window_number
                    ErrorMessage = errorProvider.GetError(txt_Windows_number);
                    if (!String.IsNullOrEmpty(ErrorMessage) ||
                        !String.IsNullOrWhiteSpace(ErrorMessage))
                        return false;
                    ///Validate txt_Windows_ScrollTime
                    ErrorMessage = errorProvider.GetError(txt_Windows_ScrollTime_);
                    if (!String.IsNullOrEmpty(ErrorMessage) ||
                        !String.IsNullOrWhiteSpace(ErrorMessage))
                        return false;
                }
                return true;
            }
        }

        public string Customercode
        {
            get { return customercode; }
            set { customercode = value; }
        }

        public string ActiveSeason
        {
            get { return activeSeason; }
            set { activeSeason = value; }
        }

        public ucDisplayWindows()
        {
            InitializeComponent();
            this.obj_DisplayWindowHelper = new DisplayWindowsHelper();
            obj_DisplayWindowHelper.GetSelectableWindows();
            obj_displayWindows_normal = new DisplayWindows();

            obj_displayWindows_alternate = new DisplayWindows();
            obj_displayWindows_alternate.WindowsMode = DispalyWindowsModes.Alternate; //Azeem
            obj_displayWindows_test = new DisplayWindows();
            obj_displayWindows_test.WindowsMode = DispalyWindowsModes.Test; //v4.8.21
            obj_CustomerCode = new Param_Customer_Code();
            Instantaneous_Class_obj = new Instantaneous_Class();

        }

        public ucDisplayWindows(List<AccessRights> rights, UserTypeID user_type_id)
            : this()
        {
           
            //gp_Avail_Category.Visible =
            //flp_Avail_Win.Visible =
            //flowLayoutPanel_btns.Visible = false;  //Invisible set by Azeem ... v10.0.21
            
            AccessRights = rights;
            //User_Type_Id = user_type_id;
            //ApplyAccessRights(AccessRights, User_Type_Id);

            //radio_windows_Normal.Checked = true; //by Azeem ... v10.0.21
        }

        private void btn_GetWindows_Click(object sender, EventArgs e)
        {
            list_AvailableWindows.Items.Clear();
            foreach (var item in obj_DisplayWindowHelper.SelectedWindows)
            {
                list_AvailableWindows.Items.Add(item);
            }
        }

        private void combo_Windows_NumberFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_Windows_NumberFormat.SelectedIndex == -1) return;
                if (combo_Windows_NumberFormat.SelectedIndex == 0)
                {
                    //OBIS FORMAT SELECT
                    txt_Windows_number.Visible = false;
                    //lbl_Number.Visible = false;
                    btn_EditWindow.Visible = false;

                    obj_WindowNumber.OBIS_Code_Display_Mode = true;

                    obj_WindowNumber.Display_OBIS_Field_C = true;
                    obj_WindowNumber.Display_OBIS_Field_D = true;
                    obj_WindowNumber.Display_OBIS_Field_E = true;

                    check_DisplayOBIS_C.Visible = false;
                    check_DisplayOBIS_D.Visible = false;
                    check_DisplayOBIS_E.Visible = false;

                    check_DisplayOBIS_C.Checked = obj_WindowNumber.Display_OBIS_Field_C;
                    check_DisplayOBIS_D.Checked = obj_WindowNumber.Display_OBIS_Field_D;
                    check_DisplayOBIS_E.Checked = obj_WindowNumber.Display_OBIS_Field_E;

                    //obj_WindowNumber.Display_OBIS_Field_C = true;
                    //obj_WindowNumber.Display_OBIS_Field_D = true;
                    //obj_WindowNumber.Display_OBIS_Field_E = true;

                    //obj_WindowNumber.Display_Dot_1 = false;
                    //obj_WindowNumber.Display_Dot_2 = false;

                    check_DoubleDot.Visible = false;
                    check_SingleDot.Visible = false;

                }
                else if (combo_Windows_NumberFormat.SelectedIndex == 1) //single dot
                {
                    //NUMBER MODE
                    txt_Windows_number.Visible = true;
                    //lbl_Number.Visible = true;
                    btn_EditWindow.Visible = true;

                    obj_WindowNumber.OBIS_Code_Display_Mode = false;

                    check_DisplayOBIS_C.Visible = false;
                    check_DisplayOBIS_D.Visible = false;
                    check_DisplayOBIS_E.Visible = false;

                    check_DoubleDot.Visible = false;
                    check_SingleDot.Visible = false;

                    ///obj_WindowNumber.Display_OBIS_Field_C = false;
                    ///obj_WindowNumber.Display_OBIS_Field_D = false;
                    ///obj_WindowNumber.Display_OBIS_Field_E = false;

                    ///obj_WindowNumber.Display_Dot_1 = true;
                    ///obj_WindowNumber.Display_Dot_2 = true;

                    ///check_DoubleDot.Checked = obj_WindowNumber.Display_Dot_1;
                    ///check_SingleDot.Checked = obj_WindowNumber.Display_Dot_2;

                    obj_WindowNumber.Display_Dot_1 = false;
                    obj_WindowNumber.Display_Dot_2 = false;
                }

                ///Apply Windwos Number Format Setting
                obj_DisplayWindowHelper.WindowDisplayNumberFormat = obj_WindowNumber;

                ///check_DisplayOBIS_C.Checked = obj_DisplayWindowHelper.WindowDisplayNumberFormat.Display_OBIS_Field_C;
                ///check_DisplayOBIS_D.Checked = obj_DisplayWindowHelper.WindowDisplayNumberFormat.Display_OBIS_Field_D;
                ///check_DisplayOBIS_E.Checked = obj_DisplayWindowHelper.WindowDisplayNumberFormat.Display_OBIS_Field_E;
                ///check_DoubleDot.Checked = obj_DisplayWindowHelper.WindowDisplayNumberFormat.Display_Dot_2;
                ///check_SingleDot.Checked = obj_DisplayWindowHelper.WindowDisplayNumberFormat.Display_Dot_1;
                /// ApplyDisplayWindowFormat();

                if (radio_windows_Alternate.Checked)
                {
                    ApplyDisplayWindowFormat(Obj_displayWindows_alternate);
                    updateWindowGrid(obj_displayWindows_alternate);
                }
                else if (radio_windows_Normal.Checked)
                {
                    ApplyDisplayWindowFormat(Obj_displayWindows_normal);
                    updateWindowGrid(obj_displayWindows_normal);
                }
                else
                {
                    ApplyDisplayWindowFormat(Obj_displayWindows_test);
                    updateWindowGrid(obj_displayWindows_test);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Scroll_Time_EventHandlers

        private void txt_Windows_ScrollTime_Leave(object sender, EventArgs e)
        {
            String ErrorMessage = String.Empty;
            try
            {
                TimeSpan ScrollTime = txt_Windows_ScrollTime_.Value.TimeOfDay;
                ///Validate DisplayWindows ScrollTime
                bool isValidated = App_Validation.Validate_Range(LocalCommon.AppValidationInfo.scrollTime_Min,
                    LocalCommon.AppValidationInfo.scrollTime_Max, ScrollTime.TotalSeconds);
                if (!isValidated)
                {
                    ErrorMessage = String.Format("Validation Failed,DisplayWindows Scroll Time range error {0}", ScrollTime);
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, txt_Windows_ScrollTime_, errorProvider);
                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, txt_Windows_ScrollTime_, errorProvider);

                if (radio_windows_Alternate.Checked)
                {
                    ///Convert to TimeSPan(scroll time in SECONDS)
                    obj_displayWindows_alternate.ScrollTime = ScrollTime;
                }
                else if (radio_windows_Normal.Checked)
                {
                    ///Convert to TimeSPan (scroll time in SECONDS)
                    obj_displayWindows_normal.ScrollTime = ScrollTime;
                }
                else if (radio_windows_Test.Checked)
                {
                    ///Convert to TimeSPan (scroll time in SECONDS)
                    obj_displayWindows_test.ScrollTime = ScrollTime;
                }
            }
            catch
            {
                ///txt_Windows_ScrollTime.Text = "--";
                ErrorMessage = String.Format("Validation Failed,DisplayWindows Scroll Time range error");
                App_Validation.Apply_ValidationResult(false, ErrorMessage, txt_Windows_ScrollTime_, errorProvider);
            }
        }

        private void txt_Windows_ScrollTime__ValueChanged(object sender, EventArgs e)
        {
            txt_Windows_ScrollTime_Leave(sender, e);
        }

        #endregion

        private void ApplyDisplayWindowFormat(DisplayWindows obj)
        {
            ///Applying Window Format On All Windows Objects
            /// obj_DisplayWindowHelper.ApplyWindowNumberDisplayFormat();
            ///obj_DisplayWindowHelper.ApplyWindowNumberDisplayFormat(this.Obj_displayWindows_normal);
            ///obj_DisplayWindowHelper.ApplyWindowNumberDisplayFormat(this.Obj_displayWindows_alternate);
            ///obj_DisplayWindowHelper.ApplyWindowNumberDisplayFormat(this.Obj_displayWindows_test);
            obj_DisplayWindowHelper.ApplyWindowNumberDisplayFormat(obj);
        }

        private void UcDisplayWindows_Load(object sender, EventArgs e)
        {
            try
            {
                ///Init ucDisplay Windows Work
                ///this.tabPage1.BackgroundImage = backImage;
                ///this.tabPage2.BackgroundImage = backImage;

                ///Init Local Variables
                if (obj_displayWindows_normal == null)
                    obj_displayWindows_normal = new DisplayWindows();
                if (obj_displayWindows_alternate == null)
                {
                    obj_displayWindows_alternate = new DisplayWindows();
                    obj_displayWindows_alternate.WindowsMode = DispalyWindowsModes.Alternate; //Azeem
                }
                if (obj_displayWindows_test == null)
                {
                    obj_displayWindows_test = new DisplayWindows();
                    obj_displayWindows_test.WindowsMode = DispalyWindowsModes.Test; //v4.8.23
                }
                ///if(Obj_ApplicationController == null)
                ///obj_ApplicationController = new ApplicationController();
                ///Param_Controller = ApplicationController.GetInstance().Param_Controller;
                if (Param_Controller == null)
                    Param_Controller = new ParameterController();
                obj_DisplayWindowHelper = Param_Controller.DisplayWindowsHelper_Obj;
                //obj_DisplayWindowHelper = Param_Controller.DisplayWindowsHelper;
                obj_displayWindowItem = new DisplayWindowItem();
                obj_WindowNumber = 1;
                //Loading Windows Category
                foreach (var item in Enum.GetNames(typeof(DisplayWindowsCategory)))
                {
                    if (!String.Equals(item, "Event", StringComparison.OrdinalIgnoreCase))
                        list_AvailableWindowsCategory.Items.Add(item);
                }
                //Init Display Windows Load
                radio_windows_Normal.Checked = true;
                list_AvailableWindowsCategory.SelectedItem = DisplayWindowsCategory.Instantaneous.ToString();
                //Show_AvailableDisplayWindows(DisplayWindowsCategory.Instantanous);
                #region Set_ErrorMessage

                if (errorProvider != null)
                {
                    errorProvider.SetIconAlignment(txt_Windows_number, ErrorIconAlignment.MiddleRight);
                    errorProvider.SetIconAlignment(txt_Windows_ScrollTime_, ErrorIconAlignment.MiddleRight);
                    errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Display Windows Interface not initialized properly", ex);
            }
        }

        private void list_AvailableWindowsCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String SelectedItem = (String)list_AvailableWindowsCategory.SelectedItem;
                if (String.IsNullOrEmpty(SelectedItem))
                    return;
                DisplayWindowsCategory cat = (DisplayWindowsCategory)Enum.Parse(typeof(DisplayWindowsCategory), SelectedItem);
                Show_AvailableDisplayWindows(cat);
            }
            catch
            { }
        }

        private void Show_AvailableDisplayWindows(DisplayWindowsCategory WinCategory)
        {
            try
            {
                list_AvailableWindows.Items.Clear();
                try
                {
                    try
                    {
                        //Param_Controller.DisplayWindowsHelper.LoadSelectableDisplayWindows(Param_Controller.Configurations);
                        Param_Controller.DisplayWindowsHelper_Obj.LoadSelectableDisplayWindows(Param_Controller.Configurations);

                    }
                    catch (Exception)
                    {

                    }
                    ///Init Display Windows Load
                    ///list_AvailableWindowsCategory.SelectedItem = DisplayWindowsCategory.Instantanous.ToString();
                    ////Show_AvailableDisplayWindows(DisplayWindowsCategory.Instantanous);
                }
                catch (Exception ex)
                {

                }
                foreach (var item in obj_DisplayWindowHelper.SelectedWindows)
                {
                    foreach (var win_Category in item.Category)
                    {
                        if (WinCategory == win_Category)
                        {
                            list_AvailableWindows.Items.Add(item);
                        }
                    }
                }
                if (radio_windows_Alternate.Checked)
                {
                    ApplyDisplayWindowFormat(Obj_displayWindows_alternate);
                    updateWindowGrid(obj_displayWindows_alternate);
                }
                else if (radio_windows_Normal.Checked)
                {
                    ApplyDisplayWindowFormat(Obj_displayWindows_normal);
                    updateWindowGrid(obj_displayWindows_normal);
                }
                else
                {
                    ApplyDisplayWindowFormat(Obj_displayWindows_test);
                    updateWindowGrid(obj_displayWindows_test);
                };
            }
            catch (Exception ex)
            {
            }
        }

        #region CheckedChanged

        private void check_SingleDot_CheckedChanged(object sender, EventArgs e)
        {
            if (check_SingleDot.Checked)
            {
                //obj_WindowNumber.Display_Dot_1 = true;
            }
            else
            {
                obj_WindowNumber.Display_Dot_1 = false;
            }
            obj_DisplayWindowHelper.WindowDisplayNumberFormat = obj_WindowNumber;
        }

        private void check_DoubleDot_CheckedChanged(object sender, EventArgs e)
        {
            if (check_DoubleDot.Checked)
            {
                //obj_WindowNumber.Display_Dot_2 = true;
            }
            else
            {
                obj_WindowNumber.Display_Dot_2 = false;
            }
            obj_DisplayWindowHelper.WindowDisplayNumberFormat = obj_WindowNumber;
        }

        private void check_DisplayOBIS_C_CheckedChanged(object sender, EventArgs e)
        {
            obj_WindowNumber.Display_OBIS_Field_C = check_DisplayOBIS_C.Checked;
            obj_DisplayWindowHelper.WindowDisplayNumberFormat = obj_WindowNumber;
        }

        private void check_DisplayOBIS_D_CheckedChanged(object sender, EventArgs e)
        {
            obj_WindowNumber.Display_OBIS_Field_D = check_DisplayOBIS_D.Checked;
            obj_DisplayWindowHelper.WindowDisplayNumberFormat = obj_WindowNumber;
        }

        private void check_DisplayOBIS_E_CheckedChanged(object sender, EventArgs e)
        {
            obj_WindowNumber.Display_OBIS_Field_E = check_DisplayOBIS_E.Checked;
            obj_DisplayWindowHelper.WindowDisplayNumberFormat = obj_WindowNumber;
        }

        #endregion

        private void gp_GeneralSettings_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (combo_Windows_NumberFormat.SelectedIndex == -1)
                //    MessageBox.Show("Select a value for number format");
                //if (txt_Windows_ScrollTime.Text == "")
                //{
                //    MessageBox.Show("You have not  entered a value for scroll time ");
                //}

                // combo_Windows_NumberFormat_SelectedIndexChanged(this, new EventArgs());
                //  combo_Windows_NumberFormat.SelectedIndex = 1;// hard code NUMBER FORMAT
                txt_Windows_ScrollTime_Leave(this, new EventArgs());

                if (radio_windows_Alternate.Checked)
                {
                    ApplyDisplayWindowFormat(Obj_displayWindows_alternate);
                    updateWindowGrid(obj_displayWindows_alternate);
                }
                else if (radio_windows_Normal.Checked)
                {
                    ApplyDisplayWindowFormat(Obj_displayWindows_normal);
                    updateWindowGrid(obj_displayWindows_normal);
                }
                else
                {
                    ApplyDisplayWindowFormat(Obj_displayWindows_test);
                    updateWindowGrid(obj_displayWindows_test);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void updateWindowGrid(DisplayWindows windowToDisplay)
        {
            Size Grid_Size = grid_SelectedWindows.Size;
            try
            {
                ((System.ComponentModel.ISupportInitialize)(grid_SelectedWindows)).BeginInit();
                grid_SelectedWindows.Size = new Size(0, 0);
                //    obj_DisplayWindowHelper.ApplyWindowNumberDisplayFormat();
                //remove grid before updating
                int maXRows = grid_SelectedWindows.Rows.Count;
                if (windowToDisplay != null)
                {
                    if (windowToDisplay.Windows.Count > 0)
                    {
                        btn_replaceWindow.Enabled = true;
                        btn_RemoveWindow.Enabled = true;

                        for (int i = 0; i < maXRows; i++)
                        {
                            grid_SelectedWindows.Rows.RemoveAt(0);
                        }
                        int currentRow = 0;

                        foreach (var item in windowToDisplay.Windows)
                        {
                            grid_SelectedWindows.Rows.Add();
                            grid_SelectedWindows.Rows[currentRow].HeaderCell.Value = (currentRow + 1).ToString();
                            grid_SelectedWindows[0, currentRow].Value = item.WindowNumberToDisplay.ToString(item.Obis_Index);
                            grid_SelectedWindows[1, currentRow].Value = item.Window_Name;

                            currentRow++;
                        }
                        //Mark Last Cell Selected
                        if (grid_SelectedWindows.Rows[currentRow - 1] != null)
                            grid_SelectedWindows.Rows[currentRow - 1].Selected = true;
                    }
                    else
                    {
                        btn_replaceWindow.Enabled = false;
                        btn_RemoveWindow.Enabled = false;
                        grid_SelectedWindows.Rows.Clear();
                    }
                }
            }
            finally
            {
                grid_SelectedWindows.Size = Grid_Size;
                ((System.ComponentModel.ISupportInitialize)(grid_SelectedWindows)).EndInit();
            }
        }

        private void grid_SelectedWindows_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int i = grid_SelectedWindows.CurrentCell.RowIndex;
                if (radio_windows_Alternate.Checked && obj_displayWindows_alternate.Windows.Count > 0)
                {
                    txt_Windows_number.Text = obj_displayWindows_alternate.Windows[i].WindowNumberToDisplay.DisplayWindowNumber.ToString();

                }
                else if (radio_windows_Normal.Checked && obj_displayWindows_normal.Windows.Count > 0)
                {
                    txt_Windows_number.Text = obj_displayWindows_normal.Windows[i].WindowNumberToDisplay.DisplayWindowNumber.ToString();

                }
                else
                    txt_Windows_number.Text = obj_displayWindows_test.Windows[i].WindowNumberToDisplay.DisplayWindowNumber.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txt_Windows_number_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayWindows SelectedDispWin = null;
                if (radio_windows_Normal.Checked)
                    SelectedDispWin = obj_displayWindows_normal;
                else if (radio_windows_Alternate.Checked)
                    SelectedDispWin = obj_displayWindows_alternate;
                else
                    SelectedDispWin = obj_displayWindows_test;
                ///Apply WindowNumber Change
                obj_WindowNumber.DisplayWindowNumber = Convert.ToUInt16(txt_Windows_number.Text);
                String ErrorMessage = String.Empty;
                ///Validate WindowNumber
                Validate_WinNumber(obj_WindowNumber, SelectedDispWin, txt_Windows_number, ref ErrorMessage);
            }
            catch (Exception)
            {
                Notification notifier = new Notification("Unable to edit Display Window", "Please select display Window from Selected Windows");
            }
        }

        private void txt_Windows_number_Leave(object sender, EventArgs e)
        {
            try
            {
                String ErrorMessage = String.Empty;
                obj_WindowNumber.DisplayWindowNumber = Convert.ToUInt16(txt_Windows_number.Text);
                bool isValidated = App_Validation.Validate_WindowNumber(obj_WindowNumber, ref ErrorMessage);
                // if(obj_WindowNumber
                if (!isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, txt_Windows_number, errorProvider);
                }
            }
            catch
            {
                txt_Windows_number.Text = "--";
            }
        }

        #region Button_Event_Handlers

        private void btn_Windows_AddWindow_Click(object sender, EventArgs e)
        {
            String ErrorMessage = String.Empty;
            try
            {
                if (list_AvailableWindows.SelectedIndex == -1)
                {
                    ///Show Error Message here
                    Notification notifier = new Notification("Please Select Display WindowItems", ErrorMessage);
                    return;
                }
                obj_displayWindowItem = (DisplayWindowItem)list_AvailableWindows.SelectedItem;
                obj_displayWindowItem.WindowNumberToDisplay = obj_WindowNumber;
                DisplayWindows SelectedDispWin = null;
                if (radio_windows_Normal.Checked)
                    SelectedDispWin = obj_displayWindows_normal;
                else if (radio_windows_Alternate.Checked)
                    SelectedDispWin = obj_displayWindows_alternate;
                else
                    SelectedDispWin = obj_displayWindows_test;
                ///ADD Display WINDOWS here
                SelectedDispWin.AddWindow(obj_displayWindowItem);
                #region Validate_DisplayWindowItem

                ///Remove selected item from list to avoid duplicates
                /// list_AvailableWindows.Items.Remove(list_AvailableWindows.SelectedItem);
                bool isValidated = App_Validation.Validate_LookupDuplicate_DisplayWindowItem(SelectedDispWin.Windows,
                    obj_displayWindowItem, ref ErrorMessage);
                if (!isValidated)
                {
                    SelectedDispWin.RemoveWindow(obj_displayWindowItem);
                    ///App_Validation.Apply_ValidationResult(false, ErrorMessage, btn_Windows_AddWindow, errorProvider);
                    Notification notifier = new Notification("Duplicate Display WindowItems", ErrorMessage, Notification.Sounds.Exclamation);
                    return;
                }
                ///else
                ///App_Validation.Apply_ValidationResult(true, String.Empty, btn_Windows_AddWindow, errorProvider);
                ///Validate Duplicate WindowNumber
                isValidated = App_Validation.Validate_LookupDuplicate_WindowNumber(SelectedDispWin.Windows,
                                obj_displayWindowItem.WindowNumberToDisplay, ref ErrorMessage);
                if (!isValidated)
                {
                    SelectedDispWin.RemoveWindow(obj_displayWindowItem);
                    ///App_Validation.Apply_ValidationResult(false, ErrorMessage, btn_Windows_AddWindow, errorProvider);
                    Notification notifier = new Notification("Duplicate Display Window Number", ErrorMessage, Notification.Sounds.Exclamation);
                    return;
                }

                #endregion
                updateWindowGrid(SelectedDispWin);
                ///Step Increment Window Number
                if (obj_WindowNumber.DisplayWindowNumber < WindowNumber.MaxWindowNumber)
                {
                    obj_WindowNumber.DisplayWindowNumber++;
                    txt_Windows_number.Text = obj_WindowNumber.DisplayWindowNumber.ToString("D3");
                }
                if (list_AvailableWindows.SelectedIndex + 1 < list_AvailableWindows.Items.Count)
                {
                    list_AvailableWindows.SelectedIndex++;
                }
                ///list_AvailableWindows.Items.RemoveAt(list_AvailableWindows.SelectedIndex);
                gp_GeneralSettings_Leave(this, new EventArgs());
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Add Display WindowItems", ex.Message);
            }
        }

        private bool check_for_Duplication_of_Display_Window(DisplayWindowItem item)
        {
            bool status = false;

            for (int i = 0; i < grid_SelectedWindows.Rows.Count; i++)
            {
                DataGridViewRow row = grid_SelectedWindows.Rows[i];
                string number = row.Cells[0].Value.ToString();
                string name = row.Cells[1].Value.ToString();
                if (number == item.WindowNumberToDisplay.ToString() || name == item.Window_Name.ToString())
                {
                    status = true;
                    break;
                }
            }
            return status;
        }

        private void btn_EditWindow_Click(object sender, EventArgs e)
        {
            String ErrorMessage = String.Empty;
            try
            {
                int i = grid_SelectedWindows.CurrentCell.RowIndex;
                if (i < 0)
                {
                    Notification notifier = new Notification("Select window to edit", "Please select display Window from Selected Windows");
                    return;
                }
                else
                {
                    DisplayWindows SelectedDispWin = null;
                    if (radio_windows_Normal.Checked)
                        SelectedDispWin = obj_displayWindows_normal;
                    else if (radio_windows_Alternate.Checked)
                        SelectedDispWin = obj_displayWindows_alternate;
                    else
                        SelectedDispWin = obj_displayWindows_test;
                    WindowNumber OldWindowNumber = SelectedDispWin.Windows[i].WindowNumberToDisplay;
                    ///Apply WindowNumber Change
                    obj_WindowNumber.DisplayWindowNumber = Convert.ToUInt16(txt_Windows_number.Text);
                    ///Validate WindowNumber
                    int isValidated = Validate_WinNumber(obj_WindowNumber, SelectedDispWin, txt_Windows_number, ref ErrorMessage);
                    ///Validated
                    if (isValidated > 0)
                    {
                        Notification notifier = new Notification("Duplicate Display Window Number",
                        ErrorMessage, Notification.Sounds.Exclamation);
                        SelectedDispWin.Windows[i].WindowNumberToDisplay = OldWindowNumber;
                    }
                    else
                        SelectedDispWin.Windows[i].WindowNumberToDisplay = obj_WindowNumber;

                    updateWindowGrid(SelectedDispWin);
                }
                gp_GeneralSettings_Leave(this, new EventArgs());
            }
            catch (Exception)
            {
                Notification notifier = new Notification("Unable to edit Display Window", "Please select display Window from Selected Windows");
            }
        }

        private int Validate_WinNumber(WindowNumber obj_WindowNumber, DisplayWindows SelectedDispWin, Control ValidationControl, ref String ErrorMessage)
        {
            int duplicate_Count = 0;
            bool isValidated = false;
            try
            {
                #region ///Validate WindowNumber

                isValidated = App_Validation.Validate_WindowNumber(obj_WindowNumber, ref ErrorMessage);
                if (isValidated)
                {
                    duplicate_Count = App_Validation.Validate_LookupWindowNumber(SelectedDispWin.Windows, obj_WindowNumber);
                    isValidated = (duplicate_Count <= 0);
                    if (!isValidated)
                    {
                        ErrorMessage = String.Format("Window Number Found {0}", obj_WindowNumber);
                        App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, ValidationControl, errorProvider);
                        return duplicate_Count;
                    }
                    else
                    {
                        ErrorMessage = String.Format("Window Number not Found");
                        App_Validation.Apply_ValidationResult(isValidated, String.Empty, ValidationControl, errorProvider);
                        return duplicate_Count;
                    }
                }
                if (!isValidated)
                {
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, ValidationControl, errorProvider);

                }
                else
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, ValidationControl, errorProvider);

                #endregion
                return duplicate_Count;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while Validating Window Number");
            }
        }

        private void btn_RemoveWindow_Click(object sender, EventArgs e)
        {
            try
            {
                int i = -1;
                DisplayWindows SelectedDispWin = null;
                if (radio_windows_Alternate.Checked)
                    SelectedDispWin = obj_displayWindows_alternate;
                else if (radio_windows_Normal.Checked)
                    SelectedDispWin = obj_displayWindows_normal;
                else
                    SelectedDispWin = obj_displayWindows_test;
                if (grid_SelectedWindows.CurrentCell != null)
                    i = grid_SelectedWindows.CurrentCell.RowIndex;
                if (i > -1)
                {
                    obj_displayWindowItem = SelectedDispWin.Windows[i];
                    SelectedDispWin.RemoveWindow(obj_displayWindowItem);
                    updateWindowGrid(SelectedDispWin);
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Unable to Remove Display Window", ex.Message);
            }
        }

        private void btn_replaceWindow_Click(object sender, EventArgs e)
        {
            String ErrorMessage = String.Empty;
            try
            {
                obj_displayWindowItem = (DisplayWindowItem)list_AvailableWindows.SelectedItem;
                obj_displayWindowItem.WindowNumberToDisplay = obj_WindowNumber;
                DisplayWindows SelectedDispWin = null;
                if (radio_windows_Alternate.Checked)
                    SelectedDispWin = obj_displayWindows_alternate;
                else if (radio_windows_Normal.Checked)
                    SelectedDispWin = obj_displayWindows_normal;
                else
                    SelectedDispWin = obj_displayWindows_test;
                ///Apply Display Window Format
                ApplyDisplayWindowFormat(SelectedDispWin);
                updateWindowGrid(SelectedDispWin);
                ///Update DisplayWindowItem 
                int i = grid_SelectedWindows.CurrentCell.RowIndex;
                DisplayWindowItem oldItem = SelectedDispWin.Windows[i];
                SelectedDispWin.ReplaceWindow(oldItem, obj_displayWindowItem);
                #region ///Verify DisplayWindowItem Duplication

                bool isValidated = App_Validation.Validate_LookupDuplicate_DisplayWindowItem(SelectedDispWin.Windows,
                    obj_displayWindowItem, ref ErrorMessage);
                if (!isValidated)
                {
                    SelectedDispWin.ReplaceWindow(obj_displayWindowItem, oldItem);
                    ///App_Validation.Apply_ValidationResult(false, ErrorMessage, btn_Windows_AddWindow, errorProvider);
                    Notification notifier = new Notification("Duplicate Display Window Item", ErrorMessage, Notification.Sounds.Exclamation);
                    return;
                }
                ///else
                ///App_Validation.Apply_ValidationResult(true, String.Empty, btn_Windows_AddWindow, errorProvider);

                #endregion
                updateWindowGrid(SelectedDispWin);
                gp_GeneralSettings_Leave(this, new EventArgs());
            }
            catch (Exception)
            {
                Notification n = new Notification("Error Replace Display Window Item", "Select any Window from available windows");
            }
        }

        private void btn_removeAllWindows_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayWindows windowToDisplay;
                if (radio_windows_Alternate.Checked)
                    windowToDisplay = obj_displayWindows_alternate;
                else if (radio_windows_Normal.Checked)
                    windowToDisplay = obj_displayWindows_normal;
                else
                    windowToDisplay = obj_displayWindows_test;
                ///Remove All Display Windows
                if (windowToDisplay.Windows.Count > 0)
                {
                    //obj_displayWindows_normal.RemoveAll();
                    //grid_SelectedWindows.Rows.Clear();
                    windowToDisplay.RemoveAll();
                    updateWindowGrid(windowToDisplay);
                }
            }
            catch (Exception)
            {
                Notification n = new Notification("Error Replace Display Window Item", "Select any Window from available windows");
            }
        }

        #endregion

        public void showToGUI_DisplayWindows()
        {
            try
            {
                int ScrollTime = 0;
                DisplayWindows SelectedDispWin = null;


                if (radio_windows_Normal.Checked)
                    SelectedDispWin = obj_displayWindows_normal;
                else if (radio_windows_Alternate.Checked)
                    SelectedDispWin = obj_displayWindows_alternate;
                else
                    SelectedDispWin = obj_displayWindows_test;

                //ScrollTime = Convert.ToInt32((SelectedDispWin.ScrollTime.Minutes * 60) +
                //                              SelectedDispWin.ScrollTime.Seconds);
                //txt_Windows_ScrollTime.Text = ScrollTime.ToString();
                DateTime Scroll_Time = DateTime.Now.Date.Add(SelectedDispWin.ScrollTime);
                txt_Windows_ScrollTime_.Value = Scroll_Time;

                WindowNumber n = obj_DisplayWindowHelper.GetWindowsFormat(SelectedDispWin);
                /// if (n.Display_OBIS_Field_C || n.Display_OBIS_Field_D || n.Display_OBIS_Field_E)
                if (n.OBIS_Code_Display_Mode)
                {
                    combo_Windows_NumberFormat.SelectedIndex = 0;
                }
                else
                {
                    combo_Windows_NumberFormat.SelectedIndex = 1;
                }
                ///combo_Windows_NumberFormat_SelectedIndexChanged(this, new EventArgs());
                updateWindowGrid(SelectedDispWin);
                #region Commented_Code_Section

                ///    obj_DisplayWindowHelper.WindowDisplayNumberFormat.OBIS_Code_Display_Mode=obj_displayWindows_normal.;
                ///    if (obj_DisplayWindowHelper.WindowDisplayNumberFormat.OBIS_Code_Display_Mode == true)
                ///        combo_Windows_NumberFormat.SelectedIndex = 0;
                ///    else
                ///        combo_Windows_NumberFormat.SelectedIndex = 1;

                ///if (grid_SelectedWindows.Rows.Count > 0)
                ///{
                ///    if (grid_SelectedWindows[0, 1].Value != null)
                ///    {
                ///        string whichMode = grid_SelectedWindows[0, 1].Value.ToString();
                ///        if (whichMode.Contains('.'))
                ///        {
                ///            //display OBIS MODE
                ///            combo_Windows_NumberFormat.SelectedIndex = 0;
                ///        }
                ///        else
                ///        {
                ///            //display Number Mode
                ///            combo_Windows_NumberFormat.SelectedIndex = 1;
                ///
                ///        }
                ///    }
                ///} 

                #endregion
            }
            catch (Exception ex)
            {
                Notification notif = new Notification("Error ShowToGUI_DisplayWindows,Param Display Windows", ex.Message);
            }
        }

        private void gp_GeneralSettings_Enter(object sender, EventArgs e)
        {
            // combo_Windows_NumberFormat.SelectedIndex = 0;
        }

        #region windows_CheckedChanged

        private void radio_windows_Normal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayWindows SelectedDispWin = null;
                pnl_gen_Scroll_Time.Visible = false; //By Azeem ....v10.0.19
                if (radio_windows_Normal.Checked)
                {
                    pnl_gen_Scroll_Time.Visible = true; //By Azeem ....v10.0.19
                    applyAccessRightsAccordingToDisplayModeSelected(DisplayWindowsParams.DisplayWindowsNormal); //By Azeem .... v10.0.21
                    SelectedDispWin = obj_displayWindows_normal;
                }
                else if (radio_windows_Alternate.Checked)
                {
                    applyAccessRightsAccordingToDisplayModeSelected(DisplayWindowsParams.DisplayWindowsAlternate); //By Azeem .... v10.0.21
                    obj_displayWindows_alternate.WindowsMode = DispalyWindowsModes.Alternate; //Azeem
                    SelectedDispWin = obj_displayWindows_alternate;
                }
                else
                {
                    list_AvailableWindowsCategory.SelectedIndex = 3;
                    //gp_Avail_Category.Visible = false;
                    applyAccessRightsAccordingToDisplayModeSelected(DisplayWindowsParams.DisplayWindowsTest); //By Azeem .... v10.0.21
                    obj_displayWindows_test.WindowsMode = DispalyWindowsModes.Test; //4.8.21
                    SelectedDispWin = obj_displayWindows_test;
                }
                ///update Window Scroll Time
                ///txt_Windows_ScrollTime.Text = ((SelectedDispWin.ScrollTime.Minutes * 60) +
                ///                                SelectedDispWin.ScrollTime.Seconds).ToString();
                DateTime Scroll_Time = DateTime.Now.Date.Add(SelectedDispWin.ScrollTime);
                txt_Windows_ScrollTime_.Value = Scroll_Time;

                WindowNumber w = obj_DisplayWindowHelper.GetWindowsFormat(SelectedDispWin);
                if (w.OBIS_Code_Display_Mode)
                {
                    combo_Windows_NumberFormat.SelectedIndex = 0;
                }
                else
                {
                    combo_Windows_NumberFormat.SelectedIndex = 1;

                }
                updateWindowGrid(SelectedDispWin);
            }
            catch (Exception ex)
            {
                Notification notification = new Notification("Error on Radio_windows_Normal", ex.Message);
            }
        }

        #endregion

        private void txt_Windows_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            { e.Handled = true; }
        }

        private void txt_Windows_ScrollTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            { e.Handled = true; }
        }

        private void btn_GenerateReport_Click(object sender, EventArgs e)
        {
            if (Obj_ApplicationController != null)
            {
                MeterConfig meter_type_info = obj_ApplicationController.ConnectionController.SelectedMeter;
                this.msn = Obj_ApplicationController.ConnectionManager.ConnectionInfo.MSN.ToString();
                this.metermodel = meter_type_info.MeterModel;
                ds_displayWindow dw_ds = new ds_displayWindow();

                if (radio_windows_Normal.Checked)
                {
                    dw_ds.dt_DisplayWindow_nor.Rows.Clear();
                    foreach (var item in Obj_displayWindows_normal.Windows)
                    {
                        if (item.Window_Name.StartsWith("CB"))
                        {
                            item.Window_Name = item.Window_Name.Remove(0, 2);
                        }
                        dw_ds.dt_DisplayWindow_nor.Rows.Add();
                        dw_ds.dt_DisplayWindow_nor[dw_ds.dt_DisplayWindow_nor.Rows.Count - 1].window_name = item.Window_Name.Replace("_", " ");
                        dw_ds.dt_DisplayWindow_nor[dw_ds.dt_DisplayWindow_nor.Rows.Count - 1].window_number = item.WindowNumberToDisplay.ToString(item.Obis_Index);
                    }
                    ReportViewer rpt = new ReportViewer(dw_ds, msn, metermodel, DateTime.Now.ToString(),
                        Obj_displayWindows_normal.ScrollTime.ToString(), "NORMAL MODE", this.Customercode,
                        Obj_ApplicationController.Applicationprocess_Controller.UserId, this.ActiveSeason.ToString(), meter_type_info);
                    rpt.Show();
                }
                else
                {
                    dw_ds.dt_DisplayWindow_nor.Rows.Clear();
                    foreach (var item in Obj_displayWindows_alternate.Windows)
                    {
                        if (item.Window_Name.StartsWith("CB"))
                        {
                            item.Window_Name = item.Window_Name.Remove(0, 2);
                        }
                        dw_ds.dt_DisplayWindow_nor.Rows.Add();
                        dw_ds.dt_DisplayWindow_nor[dw_ds.dt_DisplayWindow_nor.Rows.Count - 1].window_name = item.Window_Name.Replace("_", " ");
                        dw_ds.dt_DisplayWindow_nor[dw_ds.dt_DisplayWindow_nor.Rows.Count - 1].window_number = item.WindowNumberToDisplay.ToString(item.Obis_Index);
                    }
                    ReportViewer rpt = new ReportViewer(dw_ds, msn, metermodel, DateTime.Now.ToString(), Obj_displayWindows_alternate.ScrollTime.ToString(),
                        "ALTERNATE MODE", obj_CustomerCode.Customer_Code_String, Obj_ApplicationController.Applicationprocess_Controller.UserId,
                        this.ActiveSeason.ToString(), meter_type_info);
                    rpt.Show();

                }
            }
        }

        private void ucDisplayWindows_Enter(object sender, EventArgs e)
        {
            if (radio_windows_Normal.Checked)
                updateWindowGrid(obj_displayWindows_normal);
            else if (radio_windows_Alternate.Checked)
                updateWindowGrid(obj_displayWindows_alternate);
            else
                updateWindowGrid(obj_displayWindows_test);
        }

        private void ucDisplayWindows_Leave(object sender, EventArgs e)
        {
            try
            {
                String Error_Message = String.Empty;
                bool isValidated = false;
                isValidated = App_Validation.Validate_DisplayWindows(obj_displayWindows_normal, obj_DisplayWindowHelper.SelectedWindows, ref Error_Message);
                if (!isValidated)
                    throw new Exception(Error_Message);
                isValidated = App_Validation.Validate_DisplayWindows(obj_displayWindows_alternate, obj_DisplayWindowHelper.SelectedWindows, ref Error_Message);
                if (!isValidated)
                    throw new Exception(Error_Message);
                isValidated = App_Validation.Validate_DisplayWindows(obj_displayWindows_test, obj_DisplayWindowHelper.SelectedWindows, ref Error_Message);
                if (!isValidated)
                    throw new Exception(Error_Message);
            }
            catch (Exception ex)
            {
                Notification notification = new Notification("Error Validating Param Display Windows", ex.Message);
            }
        }

        #region AccessControlMethods

        //Method added by Azeen //v10.0.21
        public void applyAccessRightsAccordingToDisplayModeSelected( DisplayWindowsParams ModeSelected)
        {
            if (AccessRights != null)
            {
                if (AccessRights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in AccessRights)
                    {
                        if ((DisplayWindowsParams)Enum.Parse(item.QuantityType, item.QuantityName) == ModeSelected)
                        {
                            _HelperAccessRights((DisplayWindowsParams)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write, obj_ApplicationController.CurrentUser.userTypeID);//User_Type_Id);
                        }
                    }
                } 
            }
        }


        public bool ApplyAccessRights(List<AccessRights> Rights, UserTypeID userTypeId)
        {
            try
            {
                this.AccessRights = Rights;
                //this.User_Type_Id = userTypeId;
                this.SuspendLayout();
                flp_Main_Panel.SuspendLayout();
                this.fLPanel_avail_Category.SuspendLayout();
                this.fLPanel_GenSettings.SuspendLayout();
                this.flp_Avail_Win.SuspendLayout();
                this.flowLayoutPanel_btns.SuspendLayout();
                this.flp_SelectedWin.SuspendLayout();

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((DisplayWindowsParams)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write, userTypeId);
                    }
                    #region Update_GUI
                    
                    if (radio_windows_Normal.Enabled || radio_windows_Normal.Visible)
                    {
                        radio_windows_Normal.Checked = true;
                    }
                    else if (radio_windows_Alternate.Enabled || radio_windows_Alternate.Visible)
                    {
                        radio_windows_Alternate.Checked = true;
                    }
                    else if (radio_windows_Test.Enabled || radio_windows_Test.Visible)
                    {
                        radio_windows_Test.Checked = true;
                    }

                    #endregion
                    return true;
                }
                return false;
            }
            finally
            {
                this.fLPanel_avail_Category.ResumeLayout();
                this.fLPanel_GenSettings.ResumeLayout();
                this.flp_Avail_Win.ResumeLayout();
                this.flowLayoutPanel_btns.ResumeLayout();
                this.flp_SelectedWin.ResumeLayout();
                flp_Main_Panel.ResumeLayout();
                this.ResumeLayout();
            }
        }

        private void _HelperAccessRights(DisplayWindowsParams qty, bool read, bool write, UserTypeID userTypeId)
        {
            switch (qty)
            {
                case DisplayWindowsParams.DisplayWindowsItems:
                    //_HeperDisplayWindows(read, write);  //Commented by Azeem //v10.0.21
                    break;
                case DisplayWindowsParams.DisplayWindowsAlternate:
                    radio_windows_Alternate.Enabled = radio_windows_Alternate.Visible = (write || read);
                    if (radio_windows_Alternate.Checked)
                    {
                        _HeperDisplayWindows(read, write); //Added by Azeem  //v10.0.21 
                    }
                    //radio_windows_Alternate.Visible = read;
                    break;
                case DisplayWindowsParams.DisplayWindowsNormal:
                    radio_windows_Normal.Enabled = radio_windows_Normal.Visible = (write || read);
                    if (radio_windows_Normal.Checked)
                    {
                        _HeperDisplayWindows(read, write); //Added by Azeem  //v10.0.21 
                    }
                    //radio_windows_Normal.Enabled = write;
                    //radio_windows_Normal.Visible = read;
                    break;
                case DisplayWindowsParams.DisplayWindowsTest:
                    radio_windows_Test.Enabled = radio_windows_Test.Visible = (write || read); //Commented by Azeem .... v10.0.21
                    //radio_windows_Test.Enabled = write;
                    //radio_windows_Test.Visible = read;
                    if (radio_windows_Test.Checked)
                    {
                        _HeperDisplayWindows(read, write); //Added by Azeem  //v10.0.21 
                    }

                    //Added by Azeem  //v10.0.21
                    //if (radio_windows_Test.Checked)
                    //{

                    //        if (write)
                    //        {
                    //            if (userTypeId == UserTypeID.Inspector || userTypeId == UserTypeID.Reader)
                    //            {
                    //                combo_Windows_NumberFormat.Enabled = false;
                    //                gp_Avail_Category.Visible = false;
                    //            }
                    //            else
                    //            {
                    //                combo_Windows_NumberFormat.Enabled = true;
                    //                gp_Avail_Category.Visible = true;
                    //            }

                    //            lbl_NumberFormat.Visible = combo_Windows_NumberFormat.Visible = true;
                    //            lbl_NumberFormat.Enabled = true;

                    //            flp_Avail_Win.Visible = true;
                    //            flowLayoutPanel_btns.Visible = true;
                    //            flp_SelectedWin.Visible = true;
                    //        }
                    //        else if (read)
                    //        {
                    //            lbl_NumberFormat.Visible = combo_Windows_NumberFormat.Visible = false;

                    //            gp_Avail_Category.Visible = false;
                    //            flp_Avail_Win.Visible = false;
                    //            flowLayoutPanel_btns.Visible = false;

                    //            flp_SelectedWin.Visible = true;
                    //        }
                    //        else
                    //        {
                    //            lbl_NumberFormat.Visible = combo_Windows_NumberFormat.Visible = false;

                    //            gp_Avail_Category.Visible = false;
                    //            flp_Avail_Win.Visible = false;
                    //            flowLayoutPanel_btns.Visible = false;

                    //            flp_SelectedWin.Visible = false;
                    //        }  
                    //}
                    break;
                case DisplayWindowsParams.NumberFormat:
                    lbl_NumberFormat.Enabled = combo_Windows_NumberFormat.Enabled = write;
                    lbl_NumberFormat.Visible = combo_Windows_NumberFormat.Visible = write;
                    break;
                case DisplayWindowsParams.ScrollTime:
                    txt_Windows_ScrollTime_.Enabled = lbl_ScrollTimeLimit.Enabled = write;
                    pnl_gen_Scroll_Time.Visible = read;
                    break;
                default:
                    break;
            }
        }

        private void _HeperDisplayWindows(bool read, bool write)
        {
            try
            {
                lbl_NumberFormat.Visible = combo_Windows_NumberFormat.Visible =
                gp_Avail_Category.Visible =
                flp_Avail_Win.Visible =
                flowLayoutPanel_btns.Visible = write;
                flp_SelectedWin.Visible = (write||read);

                //if (write)
                //{
                //    lbl_NumberFormat.Visible = combo_Windows_NumberFormat.Visible = true;
                //    gp_Avail_Category.Visible = true;
                //    flp_Avail_Win.Visible = true;
                //    flowLayoutPanel_btns.Visible = true;
                //    flp_SelectedWin.Visible = true;
                //}
                //else if (read)
                //{
                //    lbl_NumberFormat.Visible = combo_Windows_NumberFormat.Visible = false;
                //    gp_Avail_Category.Visible = false;
                //    flp_Avail_Win.Visible = false;
                //    flowLayoutPanel_btns.Visible = false;
                //    flp_SelectedWin.Visible = true;
                //}
                //else
                //{
                //    lbl_NumberFormat.Visible = combo_Windows_NumberFormat.Visible = false;
                //    gp_Avail_Category.Visible = false;
                //    flp_Avail_Win.Visible = false;
                //    flowLayoutPanel_btns.Visible = false;
                //    flp_SelectedWin.Visible = false;
                //}
            }
            finally
            {

            }
        }

        #endregion

        //Flickering Reduction
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var parms = base.CreateParams;
        //        parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
        //        return parms;
        //    }
        //}
    }
}