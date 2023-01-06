using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DLMS;
using DLMS.Comm;

namespace dlmsConfigurationTool
{
    public partial class OBISCodeDialog : Form
    {
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
            get
            {
                if (rdbOBISCode.Checked)
                    return OBISCodeMode.ObisCode;
                else if (rdbOBISLabel.Checked)
                    return OBISCodeMode.ObisLabel;
                else
                    return OBISCodeMode.ObisIndex;
            }
            set
            {
                if (value == OBISCodeMode.ObisCode)
                    rdbOBISCode.Checked = true;
                else if (value == OBISCodeMode.ObisLabel)
                    rdbOBISLabel.Checked = true;
                else if (value == OBISCodeMode.ObisIndex)
                    rdbIndex.Checked = true;
            }
        }

        #endregion

        public OBISCodeDialog()
        {
            InitializeComponent();
            OBISDecimalValidator = new Regex(StOBISCode.OBISPatternValidator, RegexOptions.Compiled);
        }

        private void OBISCodeDialog_Load(object sender, EventArgs e)
        {
            // OBIS_Labels
            if (OBIS_LabelNames == null || OBIS_LabelNames.Count <= 0)
            {
                OBIS_LabelNames = new List<string>();

                var labels = Enum.GetNames(typeof(Get_Index));
                OBIS_LabelNames.AddRange(labels);
            }

            Update_GUI();
        }

        public void Update_GUI(string LabelSTR = "", string OBISCodeSTR = "")
        {
            // Populate_Interface
            if (!string.IsNullOrEmpty(LabelSTR))
                txtLabel.Text = LabelSTR;
            else
                txtLabel.Text = this.Label;

            if (string.IsNullOrEmpty(OBISCodeSTR))
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

            tb_ObisIndex.Text = OBISCodeSTR;
        }

        private void tb_ObisIndex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string RawOBISSTR = tb_ObisIndex.Text;
                tb_ObisIndex.BackColor = Color.Red;

                // Empty OBIS_STR Value
                if (string.IsNullOrEmpty(RawOBISSTR))
                {
                    tb_ObisIndex.BackColor = Color.Red;
                    return;
                }

                if (ObisCodeMode == OBISCodeMode.ObisCode)
                {
                    this.OBISCodeStr = string.Empty;

                    string OBISString = RawOBISSTR;
                    if (OBISDecimalValidator.IsMatch(OBISString))
                    {
                        this.OBISCodeStr = OBISString;
                        tb_ObisIndex.BackColor = Color.White;
                    }
                }
                else if (ObisCodeMode == OBISCodeMode.ObisIndex)
                {
                    this.OBISId = string.Empty;
                    string OBISString = RawOBISSTR;
                    ulong OBISIndexValue = 0;

                    if (ulong.TryParse(OBISString, out OBISIndexValue))
                    {
                        this.OBISId = OBISString;

                        StOBISCode ObisValidate = Get_Index.Dummy;
                        ObisValidate.OBIS_Value = OBISIndexValue;

                        if (ObisValidate.ClassId > 0 && (ObisValidate.OBIS_Value & 0x0000FFFFFFFFFFFF) > 0)
                            tb_ObisIndex.BackColor = Color.White;
                    }
                }
                else if (ObisCodeMode == OBISCodeMode.ObisLabel)
                {
                    // Validation
                    this.Get_Index_Name = string.Empty;

                    if (OBIS_LabelNames != null && OBIS_LabelNames.Count > 0)
                    {
                        if (OBIS_LabelNames.Contains(RawOBISSTR))
                        {
                            this.Get_Index_Name = RawOBISSTR;
                            tb_ObisIndex.BackColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.tb_ObisIndex.Text = "";
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
                {
                    OBISLabel = "";
                }
            }
            catch (Exception ex)
            {
                this.txtLabel.BackColor = Color.Red;
                Label = "";
            }
        }

        private void rdbOBISCode_CheckedChanged(object sender, EventArgs e)
        {
            tb_ObisIndex_TextChanged(sender, e);
        }

        public enum OBISCodeMode : byte
        {
            ObisCode = 1,
            ObisIndex = 2,
            ObisLabel = 3
        }
    }
}
