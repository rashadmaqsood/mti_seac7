using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SharedCode.Comm.HelperClasses;
using OptocomSoftware.Reporting;

namespace SmartEyeControl_7.Reporting
{
    public partial class ReportSelection : Form
    {

        #region Variables
        bool IsWapdaFormat = true;
        bool IsWebFormat = false;
        List<string> elecQuantites_array = new List<string>();
        List<string> mdi_array = new List<string>();
        List<string> misc_array = new List<string>();
        ds_Ins ins_DS = new ds_Ins();
        string MSN;
        string meterDateTime;
        string meterModel;
        string customer_code;
        string pID;
        string activeSeason;
        List<string> selectedQuantities;
        List<int> forcedlyUnchecked = new List<int>();

        MeterConfig meter_type_info;
        ApplicationRight currentAccessRights;

        #endregion
        #region properties
        public List<string> ElecQuantites_array
        {
            get { return elecQuantites_array; }
            set { elecQuantites_array = value; }
        }
        public List<string> Mdi_array
        {
            get { return mdi_array; }
            set { mdi_array = value; }
        }
        public List<string> Misc_array
        {
            get { return misc_array; }
            set { misc_array = value; }
        }
        #endregion

        public ReportSelection(ds_Ins dataSet_INS, string ManufacturingID, string MeterDT, string MeterModel, List<string> QuantitiesToSelect, string customerCode, ushort pid, string active_season, MeterConfig _meter_type_info, ApplicationRight _currentAccessRights)
        {
            InitializeComponent();

            meter_type_info = _meter_type_info;
            currentAccessRights = _currentAccessRights;
            try
            {
                ins_DS = dataSet_INS;
                MSN = ManufacturingID;
                meterDateTime = MeterDT;
                meterModel = MeterModel;
                selectedQuantities = QuantitiesToSelect;
                this.activeSeason = active_season;
                this.pID = pid.ToString();
                this.customer_code = customerCode;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public ReportSelection(
                                ds_Ins dataSet_INS, 
                                string ManufacturingID, 
                                string MeterDT, 
                                string MeterModel, 
                                List<string> QuantitiesToSelect, 
                                string customerCode, 
                                ushort pid, 
                                string active_season,
                                MeterConfig meterConfig)
        {
            InitializeComponent();

            try
            {
                ins_DS = dataSet_INS;
                MSN = ManufacturingID;
                meterDateTime = MeterDT;
                meterModel = MeterModel;
                selectedQuantities = QuantitiesToSelect;
                this.activeSeason = active_season;
                this.pID = pid.ToString();
                this.customer_code = customerCode;
                meter_type_info = meterConfig;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ReportSelection_Load(object sender, EventArgs e)
        {
            try
            {
                if(ins_DS.dataTable_P.Count>0)
                if (ins_DS.dataTable_P[ins_DS.dataTable_P.Count - 1].Quantity == "Power Quadrant")
                    ElectricalQuantities.Items.Add("Power Quadrant");
                int[] index_Unchecked = new int[9];
                Point p1 = new Point(37, 12);
                Point p2 = new Point(252, 12);
                Point p3 = new Point(462, 12);

                for (int i = 0; i < index_Unchecked.Length; i++)
                {
                    index_Unchecked[i] = 1;
                }
                grpBx_Electrical_Quantities.Visible = false;
                grpBx_MDI.Visible = false;
                grpBx_MISC.Visible = false;
                for (int i = 0; i < ElectricalQuantities.Items.Count; i++)
                    ElectricalQuantities.SetItemCheckState(i, CheckState.Unchecked);
                int location_Flag = 0;
                int flag = 0;

                #region quantitySelectionReport
                int powerQuadrantFlag = 0;
                foreach (var qty in selectedQuantities)
                {

                    {
                        if (qty == "MDI")
                        {
                            location_Flag++;
                            grpBx_MDI.Visible = true;
                            if (location_Flag == 1)
                            {
                                grpBx_MDI.Location = p1;
                                this.Size = new Size(275, 436);
                                btn_selectQuantityforReport.Location = new Point(37 + 136, 370);
                            }
                            else if (location_Flag == 2)
                            {
                                grpBx_MDI.Location = p2;
                                this.Size = new Size(480, 436);
                                btn_selectQuantityforReport.Location = new Point(252 + 136, 370);
                            }
                            else if (location_Flag == 3)
                            {
                                grpBx_MDI.Location = p3;
                                this.Size = new Size(700, 436);
                                btn_selectQuantityforReport.Location = new Point(462 + 136, 370);
                            }
                        }
                        else if (qty == "MISC")
                        {
                            location_Flag++;
                            grpBx_MISC.Visible = true;
                            if (location_Flag == 1)
                            {
                                grpBx_MISC.Location = p1;
                                this.Size = new Size(275, 436);
                                btn_selectQuantityforReport.Location = new Point(37 + 136, 370);
                            }
                            else if (location_Flag == 2)
                            {
                                grpBx_MISC.Location = p2;

                                this.Size = new Size(480, 436);
                                btn_selectQuantityforReport.Location = new Point(252 + 136, 370);
                            }
                            else if (location_Flag == 3)
                            {
                                grpBx_MISC.Location = p3;
                                this.Size = new Size(700, 436);
                                btn_selectQuantityforReport.Location = new Point(462 + 136, 370);
                            }
                        }
                        else
                        {
                            if (flag++ == 0)
                                location_Flag++;
                            grpBx_Electrical_Quantities.Location = p1;
                            this.Size = new Size(275, 436);
                            btn_selectQuantityforReport.Location = new Point(37 + 136, 370);
                            grpBx_Electrical_Quantities.Visible = true;

                            if (qty == "Voltage")
                            {
                                ElectricalQuantities.SetItemCheckState(0, CheckState.Checked);
                                index_Unchecked[0] = 0;
                            }
                            if (qty == "Current")
                            {
                                ElectricalQuantities.SetItemCheckState(1, CheckState.Checked);
                                index_Unchecked[1] = 0;
                            }
                            if (qty == "Active Power")
                            {
                                ElectricalQuantities.SetItemCheckState(2, CheckState.Checked);
                                ElectricalQuantities.SetItemCheckState(3, CheckState.Checked);
                                index_Unchecked[2] = 0;
                                index_Unchecked[3] = 0;
                                powerQuadrantFlag++;
                            }
                            if (qty == "Reactive Power")
                            {
                                ElectricalQuantities.SetItemCheckState(4, CheckState.Checked);
                                ElectricalQuantities.SetItemCheckState(5, CheckState.Checked);
                                index_Unchecked[4] = 0;
                                index_Unchecked[5] = 0;
                                powerQuadrantFlag++;
                            }

                            if (qty == "Apparent Power")
                            {
                                ElectricalQuantities.SetItemCheckState(6, CheckState.Unchecked);
                                index_Unchecked[6] = 0;
                            }
                            if (qty == "Power Factor")
                            {
                                ElectricalQuantities.SetItemCheckState(7, CheckState.Unchecked);
                                index_Unchecked[7] = 0;
                            }
                        }
                    }
                    //if (powerQuadrantFlag >= 2)
                    //{
                    //    ElectricalQuantities.SetItemCheckState(ins_DS.dataTable_P.Count - 1, CheckState.Checked);
                    //    index_Unchecked[ins_DS.dataTable_P.Count - 1] = 0;
                    //}
                }
                #endregion

                //for electric quantities
                for (int i = 0; i < ElectricalQuantities.Items.Count - 3; i++)
                {
                    if (index_Unchecked[i] == 0)
                        ElectricalQuantities.SetItemCheckState(i, CheckState.Checked);
                    else
                        forcedlyUnchecked.Add(i);
                }
                //for MDI
                for (int i = 1; i <= MDI.Items.Count - 1; i++)
                {
                    if (i != 3 && i != 5)
                        MDI.SetItemCheckState(i - 1, CheckState.Checked);
                }

                //for MISC
                for (int i = 1; i <= MISC.Items.Count; i++)
                {
                    //if (i == 1 || i == 2 || i == 4 || i == 13)
                    //{
                        MISC.SetItemCheckState(i - 1, CheckState.Checked);
                    //}
                    //else
                    //{
                    //    MISC.SetItemCheckState(i - 1, CheckState.Unchecked);
                    //}


                    //if (i != 2 && i != 5 && i != 6 && i != 7 && i != 8 && i != 11 && i != 13)
                    //    MISC.SetItemCheckState(i - 1, CheckState.Checked);
                }

                //Added by Azeem //To Hide Selection Form
                btn_selectQuantityforReport_Click(btn_selectQuantityforReport, new EventArgs());
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void btn_selectQuantityforReport_Click(object sender, EventArgs e)
        {

            
            if (grpBx_Electrical_Quantities.Visible == false)
                for (int i = 0; i < ElectricalQuantities.Items.Count; i++)
                {
                    ElectricalQuantities.SetItemCheckState(i, CheckState.Unchecked);
                }
            if (grpBx_MDI.Visible == false)
                for (int i = 0; i < MDI.Items.Count; i++)
                {
                    MDI.SetItemCheckState(i, CheckState.Unchecked);
                }
            if (grpBx_MISC.Visible == false)
                for (int i = 0; i < MISC.Items.Count; i++)
                {
                    MISC.SetItemCheckState(i, CheckState.Unchecked);
                }
            if (ElectricalQuantities.CheckedItems.Count == 0 && MISC.CheckedItems.Count == 0 && MDI.CheckedItems.Count == 0)
            {
                Notification Notify = new Notification("Selection", "Please select at least one item!");
                return;
            }
            

            List<string> elecQuantites_array1 = new List<string>();
            List<string> mdi_array1 = new List<string>();
            List<string> misc_array1 = new List<string>();

            for (int i = 0; i < ElectricalQuantities.CheckedItems.Count; i++)
            {
                elecQuantites_array1.Add(ElectricalQuantities.CheckedItems[i].ToString());
            }

            for (int i = 0; i < MDI.CheckedItems.Count; i++)
            {
                mdi_array1.Add(MDI.CheckedItems[i].ToString());
            }

            for (int i = 0; i < MISC.CheckedItems.Count; i++)
            {
                misc_array1.Add(MISC.CheckedItems[i].ToString());
            }

            this.ElecQuantites_array = elecQuantites_array1;
            this.Mdi_array = mdi_array1;
            this.Misc_array = misc_array1;
            this.DialogResult = DialogResult.OK;

            if (grpBx_Electrical_Quantities.Visible == false)
                this.elecQuantites_array.Clear();
            if (grpBx_MDI.Visible == false)
                this.mdi_array.Clear();
            if (grpBx_MISC.Visible == false)
                this.misc_array.Clear();
            this.Hide();

            ReportViewer RVObj = new ReportViewer(ins_DS, MSN, meterDateTime, meterModel, this.ElecQuantites_array, this.Mdi_array, this.Misc_array, customer_code, pID,
                activeSeason, meter_type_info, currentAccessRights); //,
               //((IsWapdaFormat) ? ReportFormat.WAPDA_DDS : ((IsWebFormat) ? ReportFormat.WEB_GALAXY : ReportFormat.ADVANCE_MTI)));
            RVObj.ShowDialog();

        }
        //check all
        private void chkBx_elecQuantities_CheckedChanged(object sender, EventArgs e)
        {
            //electrical quantities
            if (chkBx_elecQuantities.Checked == true)
            {
                //for electric quantities
                for (int i = 0; i < ElectricalQuantities.Items.Count; i++)
                {
                    ElectricalQuantities.SetItemCheckState(i, CheckState.Checked);
                }
            }
            else
            {
                //for electric quantities
                for (int i = 0; i < ElectricalQuantities.Items.Count; i++)
                {
                    ElectricalQuantities.SetItemCheckState(i, CheckState.Unchecked);
                }
            }

        }

        private void chkBx_MDI_CheckedChanged(object sender, EventArgs e)
        {
            //MDI
            if (chkBx_MDI.Checked == true)
            {
                //for electric quantities
                for (int i = 0; i < MDI.Items.Count; i++)
                {
                    MDI.SetItemCheckState(i, CheckState.Checked);
                }
            }
            else
            {
                //for electric quantities
                for (int i = 0; i < MDI.Items.Count; i++)
                {
                    MDI.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void chkBx_MISC_CheckedChanged(object sender, EventArgs e)
        {

            //MISC
            if (chkBx_MISC.Checked == true)
            {
                //for electric quantities
                for (int i = 0; i < MISC.Items.Count; i++)
                {
                    MISC.SetItemCheckState(i, CheckState.Checked);
                }
            }
            else
            {
                //for electric quantities
                for (int i = 0; i < MISC.Items.Count; i++)
                {
                    MISC.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void MISC_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (e.Index == 2 || e.Index == 11)
            //    e.NewValue = CheckState.Checked;

        }

        private void MDI_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (e.Index == 1)
            //    e.NewValue = CheckState.Checked;
        }

        private void ElectricalQuantities_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            foreach (var item in forcedlyUnchecked)
                if (e.Index == item)
                    e.NewValue = CheckState.Unchecked;
        }


    }
}
