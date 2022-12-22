using System.Windows.Forms;
namespace ucCustomControl
{
    partial class ucStatusWordMap
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbStatusWordList = new System.Windows.Forms.ComboBox();
            this.btnGetStatusWord = new System.Windows.Forms.Button();
            this.lblStatusWordMapCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvSelectedStatusWord = new System.Windows.Forms.DataGridView();
            this.btnRemoveAllStatusWord = new System.Windows.Forms.Button();
            this.btnRemoveStatusWord = new System.Windows.Forms.Button();
            this.btnAddStatusWord = new System.Windows.Forms.Button();
            this.list_AvailableStatausWord = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bgwGetStatusWord = new System.ComponentModel.BackgroundWorker();
            this.mainPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedStatusWord)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.tabControl1);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(931, 449);
            this.mainPanel.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(918, 425);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Controls.Add(this.cmbStatusWordList);
            this.tabPage2.Controls.Add(this.btnGetStatusWord);
            this.tabPage2.Controls.Add(this.lblStatusWordMapCount);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.dgvSelectedStatusWord);
            this.tabPage2.Controls.Add(this.btnRemoveAllStatusWord);
            this.tabPage2.Controls.Add(this.btnRemoveStatusWord);
            this.tabPage2.Controls.Add(this.btnAddStatusWord);
            this.tabPage2.Controls.Add(this.list_AvailableStatausWord);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(910, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Status Word Window";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmbStatusWordList
            // 
            this.cmbStatusWordList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusWordList.FormattingEnabled = true;
            this.cmbStatusWordList.Items.AddRange(new object[] {
            "Status Word Map_1",
            "Status Word Map_2"});
            this.cmbStatusWordList.Location = new System.Drawing.Point(601, 6);
            this.cmbStatusWordList.Name = "cmbStatusWordList";
            this.cmbStatusWordList.Size = new System.Drawing.Size(175, 21);
            this.cmbStatusWordList.TabIndex = 18;
            this.cmbStatusWordList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnGetStatusWord
            // 
            this.btnGetStatusWord.AutoSize = true;
            this.btnGetStatusWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetStatusWord.ForeColor = System.Drawing.Color.Black;
            this.btnGetStatusWord.Location = new System.Drawing.Point(305, 353);
            this.btnGetStatusWord.Name = "btnGetStatusWord";
            this.btnGetStatusWord.Size = new System.Drawing.Size(162, 34);
            this.btnGetStatusWord.TabIndex = 17;
            this.btnGetStatusWord.Tag = "Button";
            this.btnGetStatusWord.Text = "Get Status Word";
            this.btnGetStatusWord.Click += new System.EventHandler(this.btnGetStatusWord_Click);
            // 
            // lblStatusWordMapCount
            // 
            this.lblStatusWordMapCount.AutoSize = true;
            this.lblStatusWordMapCount.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusWordMapCount.ForeColor = System.Drawing.Color.Navy;
            this.lblStatusWordMapCount.Location = new System.Drawing.Point(842, 28);
            this.lblStatusWordMapCount.Name = "lblStatusWordMapCount";
            this.lblStatusWordMapCount.Size = new System.Drawing.Size(14, 15);
            this.lblStatusWordMapCount.TabIndex = 16;
            this.lblStatusWordMapCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(798, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Count:";
            // 
            // dgvSelectedStatusWord
            // 
            this.dgvSelectedStatusWord.AllowUserToAddRows = false;
            this.dgvSelectedStatusWord.AllowUserToDeleteRows = false;
            this.dgvSelectedStatusWord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSelectedStatusWord.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvSelectedStatusWord.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgvSelectedStatusWord.Location = new System.Drawing.Point(476, 47);
            this.dgvSelectedStatusWord.Name = "dgvSelectedStatusWord";
            this.dgvSelectedStatusWord.ReadOnly = true;
            this.dgvSelectedStatusWord.RowHeadersWidth = 50;
            this.dgvSelectedStatusWord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelectedStatusWord.Size = new System.Drawing.Size(384, 340);
            this.dgvSelectedStatusWord.TabIndex = 2;
            // 
            // btnRemoveAllStatusWord
            // 
            this.btnRemoveAllStatusWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAllStatusWord.Location = new System.Drawing.Point(334, 156);
            this.btnRemoveAllStatusWord.Name = "btnRemoveAllStatusWord";
            this.btnRemoveAllStatusWord.Size = new System.Drawing.Size(102, 30);
            this.btnRemoveAllStatusWord.TabIndex = 14;
            this.btnRemoveAllStatusWord.Tag = "Button";
            this.btnRemoveAllStatusWord.Text = "Remove All";
            this.btnRemoveAllStatusWord.Click += new System.EventHandler(this.btnRemoveAllStatusWord_Click);
            // 
            // btnRemoveStatusWord
            // 
            this.btnRemoveStatusWord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRemoveStatusWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveStatusWord.Location = new System.Drawing.Point(334, 120);
            this.btnRemoveStatusWord.Name = "btnRemoveStatusWord";
            this.btnRemoveStatusWord.Size = new System.Drawing.Size(102, 30);
            this.btnRemoveStatusWord.TabIndex = 10;
            this.btnRemoveStatusWord.Tag = "Button";
            this.btnRemoveStatusWord.Text = "REMOVE";
            this.btnRemoveStatusWord.Click += new System.EventHandler(this.btnRemoveStatusWord_Click);
            // 
            // btnAddStatusWord
            // 
            this.btnAddStatusWord.AutoSize = true;
            this.btnAddStatusWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddStatusWord.ForeColor = System.Drawing.Color.Black;
            this.btnAddStatusWord.Location = new System.Drawing.Point(334, 82);
            this.btnAddStatusWord.Name = "btnAddStatusWord";
            this.btnAddStatusWord.Size = new System.Drawing.Size(102, 34);
            this.btnAddStatusWord.TabIndex = 8;
            this.btnAddStatusWord.Tag = "Button";
            this.btnAddStatusWord.Text = "  ADD";
            this.btnAddStatusWord.Click += new System.EventHandler(this.btnAddStatusWord_Click);
            // 
            // list_AvailableStatausWord
            // 
            this.list_AvailableStatausWord.DisplayMember = "Name";
            this.list_AvailableStatausWord.FormattingEnabled = true;
            this.list_AvailableStatausWord.Location = new System.Drawing.Point(49, 47);
            this.list_AvailableStatausWord.Name = "list_AvailableStatausWord";
            this.list_AvailableStatausWord.Size = new System.Drawing.Size(250, 342);
            this.list_AvailableStatausWord.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(473, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Status Word Map Objects";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(89, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Available Status Word Objects";
            // 
            // bgwGetStatusWord
            // 
            this.bgwGetStatusWord.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwGetStatusWord_DoWork);
            this.bgwGetStatusWord.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwGetStatusWord_RunWorkerCompleted);
            // 
            // ucStatusWordMap
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.mainPanel);
            this.Name = "ucStatusWordMap";
            this.Size = new System.Drawing.Size(945, 465);
            this.mainPanel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedStatusWord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        // private System.Windows.Forms.Button btn_Windows_AddWindow;
        private System.Windows.Forms.TabControl tabControl1;
        // private System.Windows.Forms.Button btn_EditWindow;
        //     private System.Windows.Forms.Button btn_RemoveWindow;
        //      private System.Windows.Forms.Button btn_replaceWindow;
        private System.Windows.Forms.TabPage tabPage2;
        private Button btnRemoveStatusWord;
        private  Button btnAddStatusWord;
        private  ListBox list_AvailableStatausWord;
        private System.Windows.Forms.Label label1;
        //private  DataGridView grid_SelectedWindows;
        
        //private System.Windows.Forms.DataGridView grid_SelectedWindows;
       // private Button btn_removeAllWindows;
        private  Button btnRemoveAllStatusWord;
        private Label label2;
        private DataGridView dgvSelectedStatusWord;
        private Label label3;
        private Label lblStatusWordMapCount;
        private Button btnGetStatusWord;
        private System.ComponentModel.BackgroundWorker bgwGetStatusWord;
        private ComboBox cmbStatusWordList;
        

    }
}
