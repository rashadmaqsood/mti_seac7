namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucMajorAlarm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grid_Events = new System.Windows.Forms.DataGridView();
            this.Event_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Event_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Caution_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Is_Enable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsExclude_LogBook = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Read_caution = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Display_Caution = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isFlash = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Flash_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsMajorAlarm = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsTriggered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResetAlarmStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Events)).BeginInit();
            this.SuspendLayout();
            // 
            // grid_Events
            // 
            this.grid_Events.AllowUserToAddRows = false;
            this.grid_Events.AllowUserToDeleteRows = false;
            this.grid_Events.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            this.grid_Events.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grid_Events.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_Events.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.grid_Events.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_Events.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grid_Events.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Event_Name,
            this.Event_Code,
            this.Caution_Number,
            this.Is_Enable,
            this.IsExclude_LogBook,
            this.Read_caution,
            this.Display_Caution,
            this.isFlash,
            this.Flash_Time,
            this.IsMajorAlarm,
            this.IsTriggered,
            this.ResetAlarmStatus});
            this.grid_Events.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_Events.GridColor = System.Drawing.Color.SteelBlue;
            this.grid_Events.Location = new System.Drawing.Point(0, 0);
            this.grid_Events.Name = "grid_Events";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_Events.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grid_Events.RowHeadersWidth = 10;
            this.grid_Events.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grid_Events.Size = new System.Drawing.Size(1192, 526);
            this.grid_Events.TabIndex = 4;
            this.grid_Events.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_Events_CellValueChanged);
            // 
            // Event_Name
            // 
            this.Event_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Event_Name.FillWeight = 62.49745F;
            this.Event_Name.HeaderText = "Event Name";
            this.Event_Name.MaxInputLength = 1;
            this.Event_Name.Name = "Event_Name";
            this.Event_Name.ReadOnly = true;
            this.Event_Name.Width = 175;
            // 
            // Event_Code
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Event_Code.DefaultCellStyle = dataGridViewCellStyle3;
            this.Event_Code.FillWeight = 2.055837F;
            this.Event_Code.HeaderText = "Event Code";
            this.Event_Code.Name = "Event_Code";
            this.Event_Code.ReadOnly = true;
            // 
            // Caution_Number
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Caution_Number.DefaultCellStyle = dataGridViewCellStyle4;
            this.Caution_Number.FillWeight = 2.055837F;
            this.Caution_Number.HeaderText = "Caution Number";
            this.Caution_Number.Name = "Caution_Number";
            // 
            // Is_Enable
            // 
            this.Is_Enable.FillWeight = 2.055837F;
            this.Is_Enable.HeaderText = "Is Disable";
            this.Is_Enable.Name = "Is_Enable";
            // 
            // IsExclude_LogBook
            // 
            this.IsExclude_LogBook.FillWeight = 2.055837F;
            this.IsExclude_LogBook.HeaderText = "Exclude Log";
            this.IsExclude_LogBook.Name = "IsExclude_LogBook";
            this.IsExclude_LogBook.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsExclude_LogBook.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Read_caution
            // 
            this.Read_caution.FillWeight = 2.055837F;
            this.Read_caution.HeaderText = "Read Caution";
            this.Read_caution.Name = "Read_caution";
            this.Read_caution.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Display_Caution
            // 
            this.Display_Caution.FillWeight = 2.055837F;
            this.Display_Caution.HeaderText = "Display Caution";
            this.Display_Caution.Name = "Display_Caution";
            this.Display_Caution.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // isFlash
            // 
            this.isFlash.FillWeight = 2.055837F;
            this.isFlash.HeaderText = "isFlash";
            this.isFlash.Name = "isFlash";
            this.isFlash.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Flash_Time
            // 
            this.Flash_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Flash_Time.DefaultCellStyle = dataGridViewCellStyle5;
            this.Flash_Time.FillWeight = 2.055837F;
            this.Flash_Time.HeaderText = "Flash Time";
            this.Flash_Time.Name = "Flash_Time";
            this.Flash_Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IsMajorAlarm
            // 
            this.IsMajorAlarm.FillWeight = 2.055837F;
            this.IsMajorAlarm.HeaderText = "Is Major Alarm";
            this.IsMajorAlarm.Name = "IsMajorAlarm";
            this.IsMajorAlarm.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // IsTriggered
            // 
            this.IsTriggered.FillWeight = 2.055837F;
            this.IsTriggered.HeaderText = "Alarm Status";
            this.IsTriggered.Name = "IsTriggered";
            this.IsTriggered.ReadOnly = true;
            this.IsTriggered.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IsTriggered.Visible = false;
            // 
            // ResetAlarmStatus
            // 
            this.ResetAlarmStatus.FillWeight = 2.055837F;
            this.ResetAlarmStatus.HeaderText = "Reset Alarm Status";
            this.ResetAlarmStatus.Name = "ResetAlarmStatus";
            this.ResetAlarmStatus.Visible = false;
            // 
            // ucMajorAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.grid_Events);
            this.Name = "ucMajorAlarm";
            this.Size = new System.Drawing.Size(1192, 526);
            this.Load += new System.EventHandler(this.ucMajorAlarm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_Events)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid_Events;
        private System.Windows.Forms.DataGridViewTextBoxColumn Event_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Event_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Caution_Number;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Is_Enable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsExclude_LogBook;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Read_caution;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Display_Caution;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isFlash;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flash_Time;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsMajorAlarm;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsTriggered;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ResetAlarmStatus;
    }
}
