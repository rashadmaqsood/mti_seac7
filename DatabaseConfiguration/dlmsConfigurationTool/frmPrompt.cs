using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dlmsConfigurationTool
{
    public partial class frmPrompt : Form
    {
        #region Fields
        
        private List<string> _checkBoxText;
        
        private int maxColWidth  = 0;
        private int maxRowHeight = 0;

        /// <summary>
        /// CheckBox count to be added.
        /// </summary>
        private int cbCount = 0;

        #region Constants
        
        /// <summary>
        /// My Laptop: Width to Height ratio => Width = 13.6", Height = 7.7" => Width/Height = 1.77
        /// </summary>
        private const double W2Hratio = 1.77;

        #endregion // Constants

        #endregion // Fields

        #region Constructors

        public frmPrompt(List<string> checkBoxText)
        {
            InitializeComponent();

            this._checkBoxText = checkBoxText;
            this.maxColWidth   = (this._checkBoxText.Max(w => w.Length) * (int)_Char.Length) + (int)_ControlWidth.CheckBox;    // get max length string
            this.maxRowHeight  = (int)_ControlHeight.CheckBox;
            this.cbCount       = this._checkBoxText.Count;
            this.Width         = ((this.maxColWidth > this.pnl_btns.Width) ? this.maxColWidth : this.pnl_btns.Width) + (int)_Offset.FormWidth;
            this.Height        = this.pnl_chbxAll.Height + this.pnl_bottom.Height + (int)_Offset.FormHeight;

            this.AddCheckBox();
        }

        #endregion // Constructors

        #region Methods
        /* COMMENTED CODE
        private void AddCheckBox0()
        {
            CheckBox box = null;
            int x = 10, y = 10, x_offSet = 26, OnecharLen = 6;
            int maxColWidth = (this._checkBoxText.Max(w => w.Length) * OnecharLen) + x_offSet;    // get max length string
            int maxRowWidth = 25; // box.height + 8
            this.Height = (this.pnl_chbxAll.Height + this.pnl_btns.Height);

            int totalColWidth = maxColWidth * this._checkBoxText.Count;
            int rowCount = 0, colCount = 0, rem = 0;

            for (int i = 0; i < this._checkBoxText.Count; i++)
            {
                box = new CheckBox();
                box.Name = "chbx_" + this._checkBoxText[i];
                box.Text = this._checkBoxText[i];
                box.AutoSize = true;
                box.Width = maxColWidth;
                box.Location = new Point(x, y); //vertical
                this.Height += maxRowWidth;
                this.pnl_chkList.Controls.Add(box);
                rowCount++;
                y += box.Height + 5;
                rem = totalColWidth % rowCount;
                if (y+(box.Height) > this.pnl_chkList.Height)
                {
                    x += maxColWidth + 5;
                    y = 10;
                    this.Width += maxColWidth + 10;
                }
            }
        }
        */
        private void AddCheckBox()
        {
            try
            {
                this.pnl_chkList.Controls.Clear();

                #region Location Variables

                int x = (int)_Table.Margin_Left;
                int y = (int)_Table.Margin_Top;

                #endregion // Location Variables

                #region Height Decision Variables

                int  colCount           = 1;
                int  rowCount           = 0;
                int  maxRowCount        = 0;
                int  rowPlusRowspace    = this.maxRowHeight + (int)_Table.rowSpace;

                #endregion // Height Decision Variables

                #region Width Decision Variables

                int remItems           = 0;
                int remCols            = 0;
                int lastIncompColItems = 0;
                int lastCol            = 0;
                int pnlExWidth         = 0;
                int remColsWidth       = 0;

                #endregion // Width Decision Variables

                int childWidth         = 0;
                int childHeight        = 0;

                childWidth = (int)_Table.Margin_Left + this.maxColWidth + (int)_Offset.FormWidth;

                CheckBox box = null;

                for (int i = 0; i < cbCount; i++)
                {
                    box = new CheckBox();
                    box.Name = "chbx_" + this._checkBoxText[i];
                    box.Width = this.maxColWidth;
                    box.Text = this._checkBoxText[i];
                    box.Location = new Point(x, y);
                    box.Click += new EventHandler(this.Box_Click);
                    this.pnl_chkList.Controls.Add(box);
                    rowCount++;
                    y += rowPlusRowspace;

                    if (colCount == 1)
                    {
                        childHeight = (this.maxRowHeight * rowCount + (int)_Table.rowSpace * (rowCount - 1)) + (2 * (int)_Table.Margin_Top) + this.pnl_chbxAll.Height + this.pnl_bottom.Height;
                        this.Height = childHeight + (int)_Offset.FormHeight;
                        this.ChangeWidth(this, childWidth, childHeight); 
                        
                        remItems           = cbCount - rowCount;
                        remCols            = remItems / rowCount;
                        lastIncompColItems = remItems % rowCount;
                        lastCol            = (lastIncompColItems == 0) ? 0 : 1;
                        pnlExWidth         = this.Width - childWidth;
                        remColsWidth       = (remCols + lastCol) * (this.maxColWidth + (int)_Table.colSpace);

                        if ((pnlExWidth - remColsWidth) > 2)
                        {
                            x += this.maxColWidth + (int)_Table.colSpace;
                            colCount++;
                            y = (int)_Table.Margin_Top;
                            maxRowCount = rowCount;
                            rowCount = 0;
                        }
                    }
                    else if (rowCount == maxRowCount)
                    {
                        x += this.maxColWidth + (int)_Table.colSpace;
                        colCount++;
                        y = (int)_Table.Margin_Top;
                        rowCount = 0;
                    }
                }

                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    this.pnl_chkList.AutoScroll = true;

                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    this.Width  = Screen.PrimaryScreen.WorkingArea.Width;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeWidth(Control sender, int childWidth, int childHeight)
        {
            try
            {
                sender.Width = (int)Math.Round(sender.Height * W2Hratio);

                if (sender.Width < childWidth)
                {
                    sender.Width = childWidth + (int)_Offset.FormWidth;
                    this.ChangeHeight(sender, childWidth, childHeight);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ChangeHeight(Control sender, int childWidth, int childHeight)
        {
            try
            {
                sender.Height = (int)Math.Round(sender.Width / W2Hratio);

                if (sender.Height < childHeight)
                {
                    sender.Height = childHeight + (int)_Offset.FormHeight;
                    this.ChangeWidth(sender, childWidth, childHeight);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void frmPrompt_Load(object sender, EventArgs e)
        {
            try
            {
                double frmWidth = Math.Round((double)this.Width, 2);
                double frmHeight = Math.Round((double)this.Height, 2);
                double frmW2H = Math.Round((frmWidth / frmHeight), 2);
                string THIS = "this( W: " + frmWidth + ", H: " + frmHeight + ", W2H: " + frmW2H + " )";

                double srnWidth = Math.Round((double)Screen.PrimaryScreen.Bounds.Width, 2);
                double srnHeight = Math.Round((double)Screen.PrimaryScreen.Bounds.Height, 2);
                double srnW2H = Math.Round((srnWidth / srnHeight), 2);
                string SCREEN = "Screen( W: " + srnWidth + ", H: " + srnHeight + ", W2H: " + srnW2H + " )";
                this.Text = "Prompt [ " + THIS + "  ;  " + SCREEN + " ]";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbx_All_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var c in this.pnl_chkList.Controls)
                {
                    if (!(c is CheckBox))
                        return;

                    if (this.chbx_All.Checked)
                    {
                        ((CheckBox)c).CheckState = CheckState.Checked;
                    }
                    else
                    {
                        ((CheckBox)c).CheckState = CheckState.Unchecked;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Box_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is CheckBox)) return;

                bool isAllChecked = true;
                bool isAllUnchecked = true;

                foreach (var c in this.pnl_chkList.Controls)
                {
                    if (!(c is CheckBox)) continue;

                    if (!((CheckBox)c).Checked) isAllChecked   = false;
                    else                        isAllUnchecked = false;
                }

                if (((CheckBox)sender).Checked)
                {
                    if (isAllChecked)
                        this.chbx_All.CheckState = CheckState.Checked;
                }
                else
                {
                    if (isAllUnchecked)
                        this.chbx_All.CheckState = CheckState.Unchecked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                // Formclosing event would be called on the next line
                this.Close();  // Just close but still live.
                //this.Dispose();//Releases all resources. it meanse the frmPrompt dies. No control available in it.              

                foreach (var c in this.pnl_chkList.Controls)
                {
                    if (!(c is CheckBox)) continue;

                    if (((CheckBox)c).Checked)
                    {
                        this._checkBoxText.Add(((CheckBox)c).Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                // Formclosing event would be called on the next line
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmPrompt_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._checkBoxText.Clear();
        }

        #endregion // Methods

        #region Enums

        enum _ControlWidth
        {
            CheckBox    = 26,
            RadioButton = 25,
            VScroll     = 18
        }

        enum _ControlHeight
        {
            CheckBox    = 25,
            RadioButton = 24,
            HScroll     = 18
        }

        enum _Char
        {
            Length = 6
        }

        enum _Table
        {
            colSpace    = 4,
            rowSpace    = 4,
            Margin_Left = 8,
            Margin_Top  = 8
        }

        enum _Offset
        {
            FormTop    = 30,
            FormBottom = 8,
            FormHeight = FormTop + FormBottom,

            FormLeft   = 8,
            FormRight  = 8,
            FormWidth  = FormLeft + FormRight
        }

        #endregion // Enums
    }
}
