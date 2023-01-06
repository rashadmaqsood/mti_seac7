using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Communicator
{
    public delegate void OnCrossClick(object sender ,EventArgs e);
    public partial class ucBallonButton : UserControl
    {
        public ucBallonButton()
        {
            InitializeComponent();
        }
        public event OnCrossClick CrossClick;
        
        [DisplayName("Ballon Text")]
        [Category("BallonButton")]
        public string BallonText 
        {
            get { return label1.Text; }
            set
            {
                label1.Text = value;
            }
        }

        [DisplayName("Text Color")]
        [Category("BallonButton")]
        public Color BallonTextColor 
        {
            get { return label1.ForeColor; }
            set
            {
                label1.ForeColor = value;
            }
        }

        [DisplayName("Cross Box Color")]
        [Category("BallonButton")]
        public Color CrossBoxColor 
        {
            get 
            {
                return btnCross.ForeColor;
            }
            set 
            {
                btnCross.ForeColor = value;
                btnCross.FlatAppearance.BorderColor = value;
            }
        }

        [DisplayName("Cross Box Border")]
        [Category("BallonButton")]
        public bool IsCrossBoxBorderHide 
        {
            set 
            {
                if (value)
                {
                    btnCross.FlatAppearance.BorderSize = 0;
                }
                else { btnCross.FlatAppearance.BorderSize = 1; }
            }
        }

        [DisplayName("Bullon Text Font")]
        [Category("BallonButton")]
        public Font BallonTextFont 
        {
            get 
            {
                return label1.Font;
            }
            set 
            {
                label1.Font = value;
            }
        }

        [DisplayName("Bullon Text Location")]
        [Category("BallonButton")]
        public Point BallonTextLocation 
        {
            get 
            {
                return label1.Location;
            }
            set 
            {
                label1.Location = value;
            }
        }

        private void btnCross_Click(object sender, EventArgs e)
        {
            if (CrossClick != null) CrossClick(this, e);
        }

    }
}
