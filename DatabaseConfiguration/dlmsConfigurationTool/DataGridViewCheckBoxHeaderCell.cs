using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace dlmsConfigurationTool
{
    public class DataGridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        internal CheckBox cb = new CheckBox() {Margin = new Padding(05,05,05,05)};

        public event EventHandler CheckBoxValueChanged;//event fired when CheckBoxValue property is changed.

        public CheckState CheckBoxValue
        {
            get
            {
                return cb.CheckState;
            }
            set
            {
                if (value != cb.CheckState)
                {
                    cb.CheckState = value;
                    if (CheckBoxValueChanged != null)
                    {
                        CheckBoxValueChanged(this, EventArgs.Empty);
                    }
                }
            }
        }
        protected override void OnClick(DataGridViewCellEventArgs e)
        {
            base.OnClick(e);

        }
        
        public DataGridViewCheckBoxHeaderCell()
        {

            this.cb.MouseClick += new MouseEventHandler(cb_MouseClick);
            this.cb.CheckedChanged += new EventHandler(cb_CheckedChanged);
            this.cb.Text = "";
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxValueChanged != null)
            {
                CheckBoxValueChanged(sender, e);
            }
        }

        void cb_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnClick(new DataGridViewCellEventArgs(this.ColumnIndex, this.RowIndex));
        }

        void tb_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnClick(new DataGridViewCellEventArgs(this.ColumnIndex, this.RowIndex));
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            //Here Change the location, height, width property of CheckBox..

            this.cb.Location = new Point(cellBounds.Left, cellBounds.Top);
            this.cb.Height = cellBounds.Height;
            this.cb.Width = cellBounds.Width;

        }

        protected override void OnDataGridViewChanged()
        {
            //when the column is just attached to the DataGridView, we add the TextBox and CheckBox to the dataGridView.
            if (this.DataGridView != null)
            {
                this.DataGridView.Controls.Add(cb);
            }
        }

    }
}
