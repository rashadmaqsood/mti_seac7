using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DatabaseConfiguration.DataSet;
using DLMS.Comm;

namespace dlmsConfigurationTool
{
    public partial class FindOBISCodeDialog : Form
    {
        #region Data_Members
        
        private Configs configurations;
        private List<StOBISCode> selectedOBISCodes;
        private string label;
        private string Id;
        private string Get_IndexName;
        private string OBISStr;
        private Regex OBISDecimalValidator = null;
        
        #endregion

        #region Properties
        public List<StOBISCode> SelectedOBISCodes
        {
            get { return selectedOBISCodes; }
        }

        public Configs Configurations
        {
            get { return configurations; }
            set { configurations = value; }
        }

        public string Label
        {
            get { return label; }
            set 
            {
                ///Validation
                label = value;
                txtLabel.Text = label;
            }
        }

        public string OBISId
        {
            get { return Id; }
            set 
            {
                ///Validation
                Id = value;
                txtIndexId.Text = Id;
            }
        }

        public string Get_Index_Name
        {
            get { return Get_IndexName; }
            set 
            {
                ///Validation
                Get_IndexName = value;
                txt_GetIndex.Text = Get_IndexName;
            }
        }

        public string OBISCodeStr
        {
            get { return OBISStr; }
            set 
            { 
                ///Validation
                OBISStr = value;
                tb_ObisIndex.Text = OBISStr;
            }
        }
        #endregion
        
        public FindOBISCodeDialog()
        {
            InitializeComponent();
            OBISDecimalValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);
        }

        private void FindOBISCodeDialog_Load(object sender, EventArgs e)
        {
            txtLabel.Text = Label;
            tb_ObisIndex.Text = OBISCodeStr;
            txt_GetIndex.Text = Get_Index_Name;
            txtIndexId.Text = OBISId;
        }

        private void txtIndexId_TextChanged(object sender, EventArgs e)
        {
            try
            {
              // Validate OBIS_Id & then Assign To Variable
              ulong OBISId = 0;
              if (ulong.TryParse(txtIndexId.Text,out OBISId))
              {
                  this.OBISId = txtIndexId.Text;
                  this.txtIndexId.BackColor = Color.White;
              }
              else
              {
                  this.txtIndexId.BackColor = Color.Red;
                  this.OBISId = "";
              }
            }
            catch (Exception ex)
            { 
                
            }
        }

        private void txt_GetIndex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Validate OBIS_Id & then Assign To Variable
                String OBISName = this.txt_GetIndex.Text;
                if (!String.IsNullOrEmpty(OBISName))
                {
                    this.Get_Index_Name = OBISName;
                    this.txt_GetIndex.BackColor = Color.White;
                }
                else
                    Get_Index_Name = "";
            }
            catch (Exception ex)
            {
                this.txt_GetIndex.BackColor = Color.Red;
                this.Get_Index_Name = "";
            }
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Validate OBIS_Id & then Assign To Variable
                String OBISLabel = this.txtLabel.Text;
                if (!String.IsNullOrEmpty(OBISLabel))
                {

                    this.Label = OBISLabel;
                    this.txtLabel.BackColor = Color.White;
                }
                else
                    this.Label = "";
            }
            catch (Exception ex)
            {
                this.txtLabel.BackColor = Color.Red;
                Label = "";
            }
        }

        private void tb_ObisIndex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string OBISString = tb_ObisIndex.Text;
                if (OBISDecimalValidator.IsMatch(OBISString))
                {
                    this.OBISCodeStr = OBISString;
                    tb_ObisIndex.BackColor = Color.White;
                }
                else
                    tb_ObisIndex.BackColor = Color.Red;
                
            }
            catch (Exception ex)
            {
                this.tb_ObisIndex.Text = "";
                
            }
        }



    }
}
