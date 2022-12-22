using System.Drawing;
namespace ucDateTimeChooser
{
    partial class ucCustomWildCards
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
            this.pnlContainer = new System.Windows.Forms.SplitContainer();
            this.lbl_Heading = new System.Windows.Forms.Label();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_Prev = new System.Windows.Forms.Button();
            this.header = new System.Windows.Forms.PictureBox();
            this.gp_Monthly_Flags = new System.Windows.Forms.GroupBox();
            this.chk_2ndLastDay = new System.Windows.Forms.CheckBox();
            this.chk_lastDay = new System.Windows.Forms.CheckBox();
            this.chk_repeat_day = new System.Windows.Forms.CheckBox();
            this.gp_Misc_Flags = new System.Windows.Forms.GroupBox();
            this.chk_repeatDOW = new System.Windows.Forms.CheckBox();
            this.chk_DLS_BEGIN = new System.Windows.Forms.CheckBox();
            this.chk_gmt = new System.Windows.Forms.CheckBox();
            this.chk_repeat_mon = new System.Windows.Forms.CheckBox();
            this.chk_DLS_END = new System.Windows.Forms.CheckBox();
            this.chk_repeat_year = new System.Windows.Forms.CheckBox();
            this.gp_TimeFlags = new System.Windows.Forms.GroupBox();
            this.chk_repeat_sec = new System.Windows.Forms.CheckBox();
            this.chk_repeat_Minute = new System.Windows.Forms.CheckBox();
            this.chk_repeat_Hour = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).BeginInit();
            this.pnlContainer.Panel1.SuspendLayout();
            this.pnlContainer.Panel2.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.header)).BeginInit();
            this.gp_Monthly_Flags.SuspendLayout();
            this.gp_Misc_Flags.SuspendLayout();
            this.gp_TimeFlags.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.pnlContainer.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // pnlContainer.Panel1
            // 
            this.pnlContainer.Panel1.Controls.Add(this.lbl_Heading);
            this.pnlContainer.Panel1.Controls.Add(this.btn_next);
            this.pnlContainer.Panel1.Controls.Add(this.btn_Prev);
            this.pnlContainer.Panel1.Controls.Add(this.header);
            this.pnlContainer.Panel1MinSize = 10;
            // 
            // pnlContainer.Panel2
            // 
            this.pnlContainer.Panel2.Controls.Add(this.gp_Monthly_Flags);
            this.pnlContainer.Panel2.Controls.Add(this.gp_Misc_Flags);
            this.pnlContainer.Panel2.Controls.Add(this.gp_TimeFlags);
            this.pnlContainer.Panel2MinSize = 20;
            this.pnlContainer.Size = new System.Drawing.Size(200, 170);
            this.pnlContainer.SplitterDistance = 37;
            this.pnlContainer.SplitterIncrement = 10;
            this.pnlContainer.SplitterWidth = 1;
            this.pnlContainer.TabIndex = 0;
            // 
            // lbl_Heading
            // 
            this.lbl_Heading.AutoSize = true;
            this.lbl_Heading.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_Heading.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Heading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl_Heading.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbl_Heading.Location = new System.Drawing.Point(36, 11);
            this.lbl_Heading.Name = "lbl_Heading";
            this.lbl_Heading.Size = new System.Drawing.Size(90, 15);
            this.lbl_Heading.TabIndex = 3;
            this.lbl_Heading.Text = "Dialog_Heading";
            this.lbl_Heading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_next
            // 
            this.btn_next.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_next.Font = new System.Drawing.Font("Marlett", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btn_next.Location = new System.Drawing.Point(168, 6);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(31, 24);
            this.btn_next.TabIndex = 2;
            this.btn_next.Text = "4";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_Prev
            // 
            this.btn_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Prev.Font = new System.Drawing.Font("Marlett", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btn_Prev.Location = new System.Drawing.Point(0, 6);
            this.btn_Prev.Name = "btn_Prev";
            this.btn_Prev.Size = new System.Drawing.Size(32, 24);
            this.btn_Prev.TabIndex = 1;
            this.btn_Prev.Text = "3";
            this.btn_Prev.UseVisualStyleBackColor = true;
            this.btn_Prev.Click += new System.EventHandler(this.btn_Prev_Click);
            // 
            // header
            // 
            //this.header.BackgroundImage = global::AccurateOptocomSoftware.Properties.Resources.blue_header;
            this.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(198, 35);
            this.header.TabIndex = 0;
            this.header.TabStop = false;
            // 
            // gp_Monthly_Flags
            // 
            this.gp_Monthly_Flags.Controls.Add(this.chk_2ndLastDay);
            this.gp_Monthly_Flags.Controls.Add(this.chk_lastDay);
            this.gp_Monthly_Flags.Controls.Add(this.chk_repeat_day);
            this.gp_Monthly_Flags.ForeColor = System.Drawing.Color.Maroon;
            this.gp_Monthly_Flags.Location = new System.Drawing.Point(0, 0);
            this.gp_Monthly_Flags.Margin = new System.Windows.Forms.Padding(10);
            this.gp_Monthly_Flags.Name = "gp_Monthly_Flags";
            this.gp_Monthly_Flags.Size = new System.Drawing.Size(198, 130);
            this.gp_Monthly_Flags.TabIndex = 5;
            this.gp_Monthly_Flags.TabStop = false;
            this.gp_Monthly_Flags.Text = "Day Of Month";
            // 
            // chk_2ndLastDay
            // 
            this.chk_2ndLastDay.AutoSize = true;
            this.chk_2ndLastDay.ForeColor = System.Drawing.Color.Navy;
            this.chk_2ndLastDay.Location = new System.Drawing.Point(4, 69);
            this.chk_2ndLastDay.Name = "chk_2ndLastDay";
            this.chk_2ndLastDay.Size = new System.Drawing.Size(95, 19);
            this.chk_2ndLastDay.TabIndex = 8;
            this.chk_2ndLastDay.Text = "2nd Last Day";
            this.chk_2ndLastDay.UseVisualStyleBackColor = true;
            // 
            // chk_lastDay
            // 
            this.chk_lastDay.AutoSize = true;
            this.chk_lastDay.ForeColor = System.Drawing.Color.Navy;
            this.chk_lastDay.Location = new System.Drawing.Point(6, 44);
            this.chk_lastDay.Name = "chk_lastDay";
            this.chk_lastDay.Size = new System.Drawing.Size(71, 19);
            this.chk_lastDay.TabIndex = 7;
            this.chk_lastDay.Text = "Last Day";
            this.chk_lastDay.UseVisualStyleBackColor = true;
            // 
            // chk_repeat_day
            // 
            this.chk_repeat_day.AutoSize = true;
            this.chk_repeat_day.ForeColor = System.Drawing.Color.Navy;
            this.chk_repeat_day.Location = new System.Drawing.Point(6, 19);
            this.chk_repeat_day.Name = "chk_repeat_day";
            this.chk_repeat_day.Size = new System.Drawing.Size(177, 19);
            this.chk_repeat_day.TabIndex = 1;
            this.chk_repeat_day.Text = "Repeat Every Day Of Month";
            this.chk_repeat_day.UseVisualStyleBackColor = true;
            // 
            // gp_Misc_Flags
            // 
            this.gp_Misc_Flags.Controls.Add(this.chk_repeatDOW);
            this.gp_Misc_Flags.Controls.Add(this.chk_DLS_BEGIN);
            this.gp_Misc_Flags.Controls.Add(this.chk_gmt);
            this.gp_Misc_Flags.Controls.Add(this.chk_repeat_mon);
            this.gp_Misc_Flags.Controls.Add(this.chk_DLS_END);
            this.gp_Misc_Flags.Controls.Add(this.chk_repeat_year);
            this.gp_Misc_Flags.ForeColor = System.Drawing.Color.Maroon;
            this.gp_Misc_Flags.Location = new System.Drawing.Point(0, 0);
            this.gp_Misc_Flags.Margin = new System.Windows.Forms.Padding(10);
            this.gp_Misc_Flags.Name = "gp_Misc_Flags";
            this.gp_Misc_Flags.Size = new System.Drawing.Size(198, 130);
            this.gp_Misc_Flags.TabIndex = 6;
            this.gp_Misc_Flags.TabStop = false;
            this.gp_Misc_Flags.Text = "Date Time Flags";
            // 
            // chk_repeatDOW
            // 
            this.chk_repeatDOW.AutoSize = true;
            this.chk_repeatDOW.ForeColor = System.Drawing.Color.Navy;
            this.chk_repeatDOW.Location = new System.Drawing.Point(6, 35);
            this.chk_repeatDOW.Name = "chk_repeatDOW";
            this.chk_repeatDOW.Size = new System.Drawing.Size(172, 19);
            this.chk_repeatDOW.TabIndex = 4;
            this.chk_repeatDOW.Text = "Repeat Every Day Of Week";
            this.chk_repeatDOW.UseVisualStyleBackColor = true;
            // 
            // chk_DLS_BEGIN
            // 
            this.chk_DLS_BEGIN.AutoSize = true;
            this.chk_DLS_BEGIN.ForeColor = System.Drawing.Color.Navy;
            this.chk_DLS_BEGIN.Location = new System.Drawing.Point(6, 71);
            this.chk_DLS_BEGIN.Name = "chk_DLS_BEGIN";
            this.chk_DLS_BEGIN.Size = new System.Drawing.Size(140, 19);
            this.chk_DLS_BEGIN.TabIndex = 3;
            this.chk_DLS_BEGIN.Text = "Daylight Saving Begin";
            this.chk_DLS_BEGIN.UseVisualStyleBackColor = true;
            // 
            // chk_gmt
            // 
            this.chk_gmt.AutoSize = true;
            this.chk_gmt.ForeColor = System.Drawing.Color.Navy;
            this.chk_gmt.Location = new System.Drawing.Point(6, 110);
            this.chk_gmt.Name = "chk_gmt";
            this.chk_gmt.Size = new System.Drawing.Size(115, 19);
            this.chk_gmt.TabIndex = 2;
            this.chk_gmt.Text = "GMT(undefined)";
            this.chk_gmt.UseVisualStyleBackColor = true;
            // 
            // chk_repeat_mon
            // 
            this.chk_repeat_mon.AutoSize = true;
            this.chk_repeat_mon.ForeColor = System.Drawing.Color.Navy;
            this.chk_repeat_mon.Location = new System.Drawing.Point(6, 52);
            this.chk_repeat_mon.Name = "chk_repeat_mon";
            this.chk_repeat_mon.Size = new System.Drawing.Size(138, 19);
            this.chk_repeat_mon.TabIndex = 0;
            this.chk_repeat_mon.Text = "Repeat Every Month";
            this.chk_repeat_mon.UseVisualStyleBackColor = true;
            // 
            // chk_DLS_END
            // 
            this.chk_DLS_END.AutoSize = true;
            this.chk_DLS_END.ForeColor = System.Drawing.Color.Navy;
            this.chk_DLS_END.Location = new System.Drawing.Point(6, 90);
            this.chk_DLS_END.Name = "chk_DLS_END";
            this.chk_DLS_END.Size = new System.Drawing.Size(135, 19);
            this.chk_DLS_END.TabIndex = 2;
            this.chk_DLS_END.Text = "Daylight Saving Ends";
            this.chk_DLS_END.UseVisualStyleBackColor = true;
            // 
            // chk_repeat_year
            // 
            this.chk_repeat_year.AutoSize = true;
            this.chk_repeat_year.ForeColor = System.Drawing.Color.Navy;
            this.chk_repeat_year.Location = new System.Drawing.Point(6, 17);
            this.chk_repeat_year.Name = "chk_repeat_year";
            this.chk_repeat_year.Size = new System.Drawing.Size(126, 19);
            this.chk_repeat_year.TabIndex = 0;
            this.chk_repeat_year.Text = "Repeat Every Year";
            this.chk_repeat_year.UseVisualStyleBackColor = true;
            // 
            // gp_TimeFlags
            // 
            this.gp_TimeFlags.Controls.Add(this.chk_repeat_sec);
            this.gp_TimeFlags.Controls.Add(this.chk_repeat_Minute);
            this.gp_TimeFlags.Controls.Add(this.chk_repeat_Hour);
            this.gp_TimeFlags.ForeColor = System.Drawing.Color.Maroon;
            this.gp_TimeFlags.Location = new System.Drawing.Point(0, 0);
            this.gp_TimeFlags.Margin = new System.Windows.Forms.Padding(10);
            this.gp_TimeFlags.Name = "gp_TimeFlags";
            this.gp_TimeFlags.Size = new System.Drawing.Size(198, 130);
            this.gp_TimeFlags.TabIndex = 4;
            this.gp_TimeFlags.TabStop = false;
            this.gp_TimeFlags.Text = "Time Flags";
            // 
            // chk_repeat_sec
            // 
            this.chk_repeat_sec.AutoSize = true;
            this.chk_repeat_sec.ForeColor = System.Drawing.Color.Navy;
            this.chk_repeat_sec.Location = new System.Drawing.Point(6, 65);
            this.chk_repeat_sec.Name = "chk_repeat_sec";
            this.chk_repeat_sec.Size = new System.Drawing.Size(140, 19);
            this.chk_repeat_sec.TabIndex = 2;
            this.chk_repeat_sec.Text = "Repeat Every Second";
            this.chk_repeat_sec.UseVisualStyleBackColor = true;
            // 
            // chk_repeat_Minute
            // 
            this.chk_repeat_Minute.AutoSize = true;
            this.chk_repeat_Minute.ForeColor = System.Drawing.Color.Navy;
            this.chk_repeat_Minute.Location = new System.Drawing.Point(6, 42);
            this.chk_repeat_Minute.Name = "chk_repeat_Minute";
            this.chk_repeat_Minute.Size = new System.Drawing.Size(141, 19);
            this.chk_repeat_Minute.TabIndex = 1;
            this.chk_repeat_Minute.Text = "Repeat Every Minute";
            this.chk_repeat_Minute.UseVisualStyleBackColor = true;
            // 
            // chk_repeat_Hour
            // 
            this.chk_repeat_Hour.AutoSize = true;
            this.chk_repeat_Hour.ForeColor = System.Drawing.Color.Navy;
            this.chk_repeat_Hour.Location = new System.Drawing.Point(6, 19);
            this.chk_repeat_Hour.Name = "chk_repeat_Hour";
            this.chk_repeat_Hour.Size = new System.Drawing.Size(104, 19);
            this.chk_repeat_Hour.TabIndex = 0;
            this.chk_repeat_Hour.Text = "Repeat Hourly";
            this.chk_repeat_Hour.UseVisualStyleBackColor = true;
            // 
            // ucCustomWildCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlContainer);
            this.DoubleBuffered = true;
            this.Name = "ucCustomWildCards";
            this.Size = new System.Drawing.Size(200, 170);
            this.Load += new System.EventHandler(this.ucCustomWildCards_Load);
            this.pnlContainer.Panel1.ResumeLayout(false);
            this.pnlContainer.Panel1.PerformLayout();
            this.pnlContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.header)).EndInit();
            this.gp_Monthly_Flags.ResumeLayout(false);
            this.gp_Monthly_Flags.PerformLayout();
            this.gp_Misc_Flags.ResumeLayout(false);
            this.gp_Misc_Flags.PerformLayout();
            this.gp_TimeFlags.ResumeLayout(false);
            this.gp_TimeFlags.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer pnlContainer;
        private System.Windows.Forms.PictureBox header;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_Prev;
        private System.Windows.Forms.Label lbl_Heading;
        private System.Windows.Forms.GroupBox gp_TimeFlags;
        private System.Windows.Forms.CheckBox chk_repeat_sec;
        private System.Windows.Forms.CheckBox chk_repeat_Minute;
        private System.Windows.Forms.CheckBox chk_repeat_Hour;
        private System.Windows.Forms.GroupBox gp_Monthly_Flags;
        private System.Windows.Forms.CheckBox chk_DLS_END;
        private System.Windows.Forms.CheckBox chk_repeat_mon;
        private System.Windows.Forms.GroupBox gp_Misc_Flags;
        private System.Windows.Forms.CheckBox chk_gmt;
        private System.Windows.Forms.CheckBox chk_repeat_day;
        private System.Windows.Forms.CheckBox chk_repeat_year;
        private System.Windows.Forms.CheckBox chk_2ndLastDay;
        private System.Windows.Forms.CheckBox chk_lastDay;
        private System.Windows.Forms.CheckBox chk_repeatDOW;
        private System.Windows.Forms.CheckBox chk_DLS_BEGIN;
    }
}
