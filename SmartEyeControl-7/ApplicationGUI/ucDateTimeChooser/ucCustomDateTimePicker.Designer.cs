using DLMS;
namespace ucDateTimeChooser
{
    partial class ucCustomDateTimePicker
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.txtDateTime = new System.Windows.Forms.TextBox();
            this.DatetimePicker = new ucDateTimeChooser.DateTimePicker();
            this.btn_dropDown = new System.Windows.Forms.Button();
            this.Tooltip = new System.Windows.Forms.ToolTip(this.components);
            this._ErrorMessage = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._ErrorMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.txtDateTime);
            this.splitContainer.Panel1.Controls.Add(this.DatetimePicker);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.btn_dropDown);
            this.splitContainer.Size = new System.Drawing.Size(211, 20);
            this.splitContainer.SplitterDistance = 185;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 0;
            // 
            // txtDateTime
            // 
            this.txtDateTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtDateTime.Location = new System.Drawing.Point(0, 0);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.Size = new System.Drawing.Size(166, 23);
            this.txtDateTime.TabIndex = 0;
            this.txtDateTime.Click += new System.EventHandler(this.txtDateTime_Click);
            this.txtDateTime.TextChanged += new System.EventHandler(this.txtDateTime_TextChanged);
            this.txtDateTime.DoubleClick += new System.EventHandler(this.txtDateTime_DoubleClick);
            // 
            // DatetimePicker
            // 
            this.DatetimePicker.CalendarFont = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.DatetimePicker.CalendarForeColor = System.Drawing.Color.Navy;
            this.DatetimePicker.CustomFormat = "ddd, dd MMM yyyy HH:mm:ss";
            this.DatetimePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DatetimePicker.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.DatetimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DatetimePicker.FormatEx = DLMS.dtpCustomExtensions.dtpLongDateTimeWildCard;
            this.DatetimePicker.LinkedTo = null;
            this.DatetimePicker.Location = new System.Drawing.Point(0, 0);
            this.DatetimePicker.Name = "DatetimePicker";
            this.DatetimePicker.ReadOnly = false;
            this.DatetimePicker.ShowButtons = true;
            this.DatetimePicker.Size = new System.Drawing.Size(185, 23);
            this.DatetimePicker.TabIndex = 1;
            this.DatetimePicker.Value = new System.DateTime(2014, 5, 22, 0, 0, 0, 0);
            // 
            // btn_dropDown
            // 
            this.btn_dropDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_dropDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_dropDown.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btn_dropDown.Location = new System.Drawing.Point(0, 0);
            this.btn_dropDown.Margin = new System.Windows.Forms.Padding(0);
            this.btn_dropDown.Name = "btn_dropDown";
            this.btn_dropDown.Size = new System.Drawing.Size(25, 20);
            this.btn_dropDown.TabIndex = 2;
            this.btn_dropDown.Text = "6";
            this.btn_dropDown.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Tooltip.SetToolTip(this.btn_dropDown, ";");
            this.btn_dropDown.UseVisualStyleBackColor = true;
            this.btn_dropDown.Click += new System.EventHandler(this.btn_dropDown_Click);
            // 
            // _ErrorMessage
            // 
            this._ErrorMessage.ContainerControl = this;
            // 
            // ucCustomDateTimePicker
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Name = "ucCustomDateTimePicker";
            this.Size = new System.Drawing.Size(211, 20);
            this.Enter += new System.EventHandler(this.ucCustomDateTimePicker_Enter);
            this.Resize += new System.EventHandler(this.ucCustomDateTimePicker_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._ErrorMessage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        internal DateTimePicker DatetimePicker;
        private System.Windows.Forms.Button btn_dropDown;
        private System.Windows.Forms.TextBox txtDateTime;
        private System.Windows.Forms.ToolTip Tooltip;
        private System.Windows.Forms.ErrorProvider _ErrorMessage;

    }
}
