namespace SmartEyeControl_7.Reporting
{
    partial class ReportSelection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ElectricalQuantities = new ComponentFactory.Krypton.Toolkit.KryptonCheckedListBox();
            this.MDI = new ComponentFactory.Krypton.Toolkit.KryptonCheckedListBox();
            this.MISC = new ComponentFactory.Krypton.Toolkit.KryptonCheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkBx_elecQuantities = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkBx_MDI = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkBx_MISC = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.grpBx_Electrical_Quantities = new System.Windows.Forms.GroupBox();
            this.grpBx_MDI = new System.Windows.Forms.GroupBox();
            this.grpBx_MISC = new System.Windows.Forms.GroupBox();
            this.btn_selectQuantityforReport = new System.Windows.Forms.Button();
            this.grpBx_Electrical_Quantities.SuspendLayout();
            this.grpBx_MDI.SuspendLayout();
            this.grpBx_MISC.SuspendLayout();
            this.SuspendLayout();
            // 
            // ElectricalQuantities
            // 
            this.ElectricalQuantities.CheckOnClick = true;
            this.ElectricalQuantities.Items.AddRange(new object[] {
            "Voltage",
            "Current",
            "Active Power Positive",
            "Active Power Negative",
            "Reactive Power Positive",
            "Reactive Power Negative",
            "Apparent Power",
            "Power Factor"});
            this.ElectricalQuantities.Location = new System.Drawing.Point(18, 72);
            this.ElectricalQuantities.Name = "ElectricalQuantities";
            this.ElectricalQuantities.Size = new System.Drawing.Size(164, 270);
            this.ElectricalQuantities.TabIndex = 1;
            this.ElectricalQuantities.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ElectricalQuantities_ItemCheck);
            // 
            // MDI
            // 
            this.MDI.CheckOnClick = true;
            this.MDI.Items.AddRange(new object[] {
            "Current Month MDI KW",
            "Current Month MDI Kvar"});
            this.MDI.Location = new System.Drawing.Point(14, 72);
            this.MDI.Name = "MDI";
            this.MDI.Size = new System.Drawing.Size(164, 270);
            this.MDI.TabIndex = 1;
            this.MDI.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.MDI_ItemCheck);
            // 
            // MISC
            // 
            this.MISC.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImageColumn;
            this.MISC.CheckOnClick = true;
            this.MISC.Items.AddRange(new object[] {
            "Meter DateTime",
            "Active Tariff",
            "Active Firmware Version",
            "Battery Volts",
            "Billing Period Counter VZ",
            "CT Ratio Numerator",
            "CT Ratio Denominator",
            "Contactor Relay Status",
            "Event Counter",
            "Load Profile Counter",
            "PT Ratio Numerator",
            "PT Ratio Denominator",
            "Power Failure Number",
            "Supply Frequency",
            "Tamper Power",
            "RSSI Signal Strength (dBm)"});
            this.MISC.Location = new System.Drawing.Point(18, 72);
            this.MISC.Name = "MISC";
            this.MISC.Size = new System.Drawing.Size(164, 270);
            this.MISC.TabIndex = 1;
            this.MISC.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.MISC_ItemCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Electrical Quantities";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(75, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "MDI";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(70, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "MISC";
            // 
            // chkBx_elecQuantities
            // 
            this.chkBx_elecQuantities.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkBx_elecQuantities.Location = new System.Drawing.Point(22, 49);
            this.chkBx_elecQuantities.Name = "chkBx_elecQuantities";
            this.chkBx_elecQuantities.Size = new System.Drawing.Size(74, 20);
            this.chkBx_elecQuantities.TabIndex = 3;
            this.chkBx_elecQuantities.Text = "Check All";
            this.chkBx_elecQuantities.Values.Text = "Check All";
            this.chkBx_elecQuantities.CheckedChanged += new System.EventHandler(this.chkBx_elecQuantities_CheckedChanged);
            // 
            // chkBx_MDI
            // 
            this.chkBx_MDI.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkBx_MDI.Location = new System.Drawing.Point(17, 50);
            this.chkBx_MDI.Name = "chkBx_MDI";
            this.chkBx_MDI.Size = new System.Drawing.Size(74, 20);
            this.chkBx_MDI.TabIndex = 3;
            this.chkBx_MDI.Text = "Check All";
            this.chkBx_MDI.Values.Text = "Check All";
            this.chkBx_MDI.CheckedChanged += new System.EventHandler(this.chkBx_MDI_CheckedChanged);
            // 
            // chkBx_MISC
            // 
            this.chkBx_MISC.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkBx_MISC.Location = new System.Drawing.Point(21, 50);
            this.chkBx_MISC.Name = "chkBx_MISC";
            this.chkBx_MISC.Size = new System.Drawing.Size(74, 20);
            this.chkBx_MISC.TabIndex = 3;
            this.chkBx_MISC.Text = "Check All";
            this.chkBx_MISC.Values.Text = "Check All";
            this.chkBx_MISC.CheckedChanged += new System.EventHandler(this.chkBx_MISC_CheckedChanged);
            // 
            // grpBx_Electrical_Quantities
            // 
            this.grpBx_Electrical_Quantities.Controls.Add(this.chkBx_elecQuantities);
            this.grpBx_Electrical_Quantities.Controls.Add(this.label1);
            this.grpBx_Electrical_Quantities.Controls.Add(this.ElectricalQuantities);
            this.grpBx_Electrical_Quantities.Location = new System.Drawing.Point(37, 12);
            this.grpBx_Electrical_Quantities.Name = "grpBx_Electrical_Quantities";
            this.grpBx_Electrical_Quantities.Size = new System.Drawing.Size(199, 352);
            this.grpBx_Electrical_Quantities.TabIndex = 4;
            this.grpBx_Electrical_Quantities.TabStop = false;
            // 
            // grpBx_MDI
            // 
            this.grpBx_MDI.Controls.Add(this.chkBx_MDI);
            this.grpBx_MDI.Controls.Add(this.label2);
            this.grpBx_MDI.Controls.Add(this.MDI);
            this.grpBx_MDI.Location = new System.Drawing.Point(252, 12);
            this.grpBx_MDI.Name = "grpBx_MDI";
            this.grpBx_MDI.Size = new System.Drawing.Size(193, 352);
            this.grpBx_MDI.TabIndex = 5;
            this.grpBx_MDI.TabStop = false;
            // 
            // grpBx_MISC
            // 
            this.grpBx_MISC.Controls.Add(this.chkBx_MISC);
            this.grpBx_MISC.Controls.Add(this.label3);
            this.grpBx_MISC.Controls.Add(this.MISC);
            this.grpBx_MISC.Location = new System.Drawing.Point(462, 12);
            this.grpBx_MISC.Name = "grpBx_MISC";
            this.grpBx_MISC.Size = new System.Drawing.Size(198, 352);
            this.grpBx_MISC.TabIndex = 6;
            this.grpBx_MISC.TabStop = false;
            // 
            // btn_selectQuantityforReport
            // 
            this.btn_selectQuantityforReport.Location = new System.Drawing.Point(598, 370);
            this.btn_selectQuantityforReport.Name = "btn_selectQuantityforReport";
            this.btn_selectQuantityforReport.Size = new System.Drawing.Size(62, 25);
            this.btn_selectQuantityforReport.TabIndex = 7;
            this.btn_selectQuantityforReport.Text = "OK";
            this.btn_selectQuantityforReport.UseVisualStyleBackColor = true;
            this.btn_selectQuantityforReport.Click += new System.EventHandler(this.btn_selectQuantityforReport_Click);
            // 
            // ReportSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(692, 402);
            this.Controls.Add(this.btn_selectQuantityforReport);
            this.Controls.Add(this.grpBx_MISC);
            this.Controls.Add(this.grpBx_MDI);
            this.Controls.Add(this.grpBx_Electrical_Quantities);
            this.Name = "ReportSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quantity Selection";
            this.Load += new System.EventHandler(this.ReportSelection_Load);
            this.grpBx_Electrical_Quantities.ResumeLayout(false);
            this.grpBx_Electrical_Quantities.PerformLayout();
            this.grpBx_MDI.ResumeLayout(false);
            this.grpBx_MDI.PerformLayout();
            this.grpBx_MISC.ResumeLayout(false);
            this.grpBx_MISC.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonCheckedListBox ElectricalQuantities;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckedListBox MDI;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckedListBox MISC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkBx_elecQuantities;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkBx_MDI;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkBx_MISC;
        private System.Windows.Forms.GroupBox grpBx_Electrical_Quantities;
        private System.Windows.Forms.GroupBox grpBx_MDI;
        private System.Windows.Forms.GroupBox grpBx_MISC;
        private System.Windows.Forms.Button btn_selectQuantityforReport;

    }
}