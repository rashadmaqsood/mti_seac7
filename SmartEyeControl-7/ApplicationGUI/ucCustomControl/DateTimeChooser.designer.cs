using System.Windows.Forms;
namespace datetime
{
    partial class DateTimeChooser
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
            this.combo_Year = new System.Windows.Forms.ComboBox();
            this.label_year = new System.Windows.Forms.Label();
            this.combo_Date = new System.Windows.Forms.ComboBox();
            this.label_Date = new System.Windows.Forms.Label();
            this.combo_Month = new System.Windows.Forms.ComboBox();
            this.label_month = new System.Windows.Forms.Label();
            this.combo_DayofWeek = new System.Windows.Forms.ComboBox();
            this.lbl_DayofWeek = new System.Windows.Forms.Label();
            this.combo_hour = new System.Windows.Forms.ComboBox();
            this.label_hudredth = new System.Windows.Forms.Label();
            this.combo_minute = new System.Windows.Forms.ComboBox();
            this.label_seconds = new System.Windows.Forms.Label();
            this.combo_second = new System.Windows.Forms.ComboBox();
            this.label_minutes = new System.Windows.Forms.Label();
            this.combo_hundredth = new System.Windows.Forms.ComboBox();
            this.label_hours = new System.Windows.Forms.Label();
            this.parent_pnl = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.parent_pnl)).BeginInit();
            this.parent_pnl.Panel1.SuspendLayout();
            this.parent_pnl.Panel2.SuspendLayout();
            this.parent_pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // combo_Year
            // 
            this.combo_Year.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Year.FormattingEnabled = true;
            this.combo_Year.Location = new System.Drawing.Point(5, 16);
            this.combo_Year.Name = "combo_Year";
            this.combo_Year.Size = new System.Drawing.Size(75, 23);
            this.combo_Year.TabIndex = 0;
            this.combo_Year.SelectedIndexChanged += new System.EventHandler(this.combo_Year_SelectedIndexChanged);
            // 
            // label_year
            // 
            this.label_year.ForeColor = System.Drawing.Color.Maroon;
            this.label_year.Location = new System.Drawing.Point(8, 0);
            this.label_year.Name = "label_year";
            this.label_year.Size = new System.Drawing.Size(75, 15);
            this.label_year.TabIndex = 1;
            this.label_year.Text = "Year";
            // 
            // combo_Date
            // 
            this.combo_Date.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Date.FormattingEnabled = true;
            this.combo_Date.Location = new System.Drawing.Point(160, 16);
            this.combo_Date.Name = "combo_Date";
            this.combo_Date.Size = new System.Drawing.Size(75, 23);
            this.combo_Date.TabIndex = 0;
            this.combo_Date.SelectedIndexChanged += new System.EventHandler(this.combo_Date_SelectedIndexChanged);
            // 
            // label_Date
            // 
            this.label_Date.ForeColor = System.Drawing.Color.Maroon;
            this.label_Date.Location = new System.Drawing.Point(157, 0);
            this.label_Date.Name = "label_Date";
            this.label_Date.Size = new System.Drawing.Size(85, 15);
            this.label_Date.TabIndex = 1;
            this.label_Date.Text = "Day of Month";
            // 
            // combo_Month
            // 
            this.combo_Month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Month.FormattingEnabled = true;
            this.combo_Month.Location = new System.Drawing.Point(82, 16);
            this.combo_Month.Name = "combo_Month";
            this.combo_Month.Size = new System.Drawing.Size(75, 23);
            this.combo_Month.TabIndex = 0;
            this.combo_Month.SelectedIndexChanged += new System.EventHandler(this.combo_Month_SelectedIndexChanged);
            // 
            // label_month
            // 
            this.label_month.ForeColor = System.Drawing.Color.Maroon;
            this.label_month.Location = new System.Drawing.Point(84, 0);
            this.label_month.Name = "label_month";
            this.label_month.Size = new System.Drawing.Size(73, 15);
            this.label_month.TabIndex = 1;
            this.label_month.Text = "Month";
            // 
            // combo_DayofWeek
            // 
            this.combo_DayofWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_DayofWeek.FormattingEnabled = true;
            this.combo_DayofWeek.Location = new System.Drawing.Point(238, 16);
            this.combo_DayofWeek.Name = "combo_DayofWeek";
            this.combo_DayofWeek.Size = new System.Drawing.Size(75, 23);
            this.combo_DayofWeek.TabIndex = 0;
            this.combo_DayofWeek.SelectedIndexChanged += new System.EventHandler(this.combo_DayofWeek_SelectedIndexChanged);
            // 
            // lbl_DayofWeek
            // 
            this.lbl_DayofWeek.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_DayofWeek.Location = new System.Drawing.Point(235, 0);
            this.lbl_DayofWeek.Name = "lbl_DayofWeek";
            this.lbl_DayofWeek.Size = new System.Drawing.Size(85, 15);
            this.lbl_DayofWeek.TabIndex = 1;
            this.lbl_DayofWeek.Text = "Day of Week";
            // 
            // combo_hour
            // 
            this.combo_hour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_hour.DropDownWidth = 55;
            this.combo_hour.FormattingEnabled = true;
            this.combo_hour.Location = new System.Drawing.Point(5, 16);
            this.combo_hour.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.combo_hour.Name = "combo_hour";
            this.combo_hour.Size = new System.Drawing.Size(45, 23);
            this.combo_hour.TabIndex = 0;
            this.combo_hour.SelectedIndexChanged += new System.EventHandler(this.combo_hour_SelectedIndexChanged);
            // 
            // label_hudredth
            // 
            this.label_hudredth.ForeColor = System.Drawing.Color.Maroon;
            this.label_hudredth.Location = new System.Drawing.Point(170, 0);
            this.label_hudredth.Name = "label_hudredth";
            this.label_hudredth.Size = new System.Drawing.Size(67, 15);
            this.label_hudredth.TabIndex = 1;
            this.label_hudredth.Text = "hundredth";
            // 
            // combo_minute
            // 
            this.combo_minute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_minute.DropDownWidth = 55;
            this.combo_minute.FormattingEnabled = true;
            this.combo_minute.Location = new System.Drawing.Point(65, 16);
            this.combo_minute.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.combo_minute.Name = "combo_minute";
            this.combo_minute.Size = new System.Drawing.Size(45, 23);
            this.combo_minute.TabIndex = 0;
            this.combo_minute.SelectedIndexChanged += new System.EventHandler(this.combo_minute_SelectedIndexChanged);
            // 
            // label_seconds
            // 
            this.label_seconds.ForeColor = System.Drawing.Color.Maroon;
            this.label_seconds.Location = new System.Drawing.Point(118, 0);
            this.label_seconds.Name = "label_seconds";
            this.label_seconds.Size = new System.Drawing.Size(55, 15);
            this.label_seconds.TabIndex = 1;
            this.label_seconds.Text = "ss";
            // 
            // combo_second
            // 
            this.combo_second.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_second.DropDownWidth = 55;
            this.combo_second.FormattingEnabled = true;
            this.combo_second.Location = new System.Drawing.Point(121, 16);
            this.combo_second.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.combo_second.Name = "combo_second";
            this.combo_second.Size = new System.Drawing.Size(45, 23);
            this.combo_second.TabIndex = 0;
            this.combo_second.SelectedIndexChanged += new System.EventHandler(this.combo_second_SelectedIndexChanged);
            // 
            // label_minutes
            // 
            this.label_minutes.ForeColor = System.Drawing.Color.Maroon;
            this.label_minutes.Location = new System.Drawing.Point(62, 0);
            this.label_minutes.Name = "label_minutes";
            this.label_minutes.Size = new System.Drawing.Size(55, 15);
            this.label_minutes.TabIndex = 1;
            this.label_minutes.Text = "mm";
            // 
            // combo_hundredth
            // 
            this.combo_hundredth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_hundredth.DropDownWidth = 55;
            this.combo_hundredth.FormattingEnabled = true;
            this.combo_hundredth.Location = new System.Drawing.Point(173, 16);
            this.combo_hundredth.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.combo_hundredth.Name = "combo_hundredth";
            this.combo_hundredth.Size = new System.Drawing.Size(45, 23);
            this.combo_hundredth.TabIndex = 0;
            this.combo_hundredth.SelectedIndexChanged += new System.EventHandler(this.combo_hundredth_SelectedIndexChanged);
            // 
            // label_hours
            // 
            this.label_hours.ForeColor = System.Drawing.Color.Maroon;
            this.label_hours.Location = new System.Drawing.Point(8, 0);
            this.label_hours.Name = "label_hours";
            this.label_hours.Size = new System.Drawing.Size(55, 15);
            this.label_hours.TabIndex = 1;
            this.label_hours.Text = "hh";
            // 
            // parent_pnl
            // 
            this.parent_pnl.BackColor = System.Drawing.Color.Transparent;
            this.parent_pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parent_pnl.IsSplitterFixed = true;
            this.parent_pnl.Location = new System.Drawing.Point(0, 0);
            this.parent_pnl.Name = "parent_pnl";
            this.parent_pnl.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // parent_pnl.Panel1
            // 
            this.parent_pnl.Panel1.Controls.Add(this.combo_DayofWeek);
            this.parent_pnl.Panel1.Controls.Add(this.combo_Date);
            this.parent_pnl.Panel1.Controls.Add(this.lbl_DayofWeek);
            this.parent_pnl.Panel1.Controls.Add(this.combo_Year);
            this.parent_pnl.Panel1.Controls.Add(this.label_Date);
            this.parent_pnl.Panel1.Controls.Add(this.label_year);
            this.parent_pnl.Panel1.Controls.Add(this.combo_Month);
            this.parent_pnl.Panel1.Controls.Add(this.label_month);
            // 
            // parent_pnl.Panel2
            // 
            this.parent_pnl.Panel2.Controls.Add(this.label_hudredth);
            this.parent_pnl.Panel2.Controls.Add(this.combo_hundredth);
            this.parent_pnl.Panel2.Controls.Add(this.combo_hour);
            this.parent_pnl.Panel2.Controls.Add(this.label_seconds);
            this.parent_pnl.Panel2.Controls.Add(this.combo_second);
            this.parent_pnl.Panel2.Controls.Add(this.combo_minute);
            this.parent_pnl.Panel2.Controls.Add(this.label_hours);
            this.parent_pnl.Panel2.Controls.Add(this.label_minutes);
            this.parent_pnl.Size = new System.Drawing.Size(317, 85);
            this.parent_pnl.SplitterDistance = 40;
            this.parent_pnl.SplitterWidth = 5;
            this.parent_pnl.TabIndex = 11;
            // 
            // DateTimeChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.parent_pnl);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.Name = "DateTimeChooser";
            this.Size = new System.Drawing.Size(317, 85);
            this.Load += new System.EventHandler(this.DateTimeChooser_Load);
            this.parent_pnl.Panel1.ResumeLayout(false);
            this.parent_pnl.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.parent_pnl)).EndInit();
            this.parent_pnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox combo_hour;
        private System.Windows.Forms.Label label_hudredth;
        private System.Windows.Forms.ComboBox combo_minute;
        private System.Windows.Forms.Label label_seconds;
        private System.Windows.Forms.ComboBox combo_second;
        private System.Windows.Forms.Label label_minutes;
        private System.Windows.Forms.ComboBox combo_hundredth;
        private System.Windows.Forms.Label label_hours;
        private System.Windows.Forms.ComboBox combo_Year;
        private System.Windows.Forms.Label label_Date;
        private System.Windows.Forms.ComboBox combo_Month;
        private System.Windows.Forms.Label label_month;
        private System.Windows.Forms.ComboBox combo_Date;
        private System.Windows.Forms.Label label_year;
        private System.Windows.Forms.Label lbl_DayofWeek;
        private System.Windows.Forms.ComboBox combo_DayofWeek;
        private SplitContainer parent_pnl;


    }
}
