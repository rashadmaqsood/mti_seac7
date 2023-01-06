using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dlmsConfigurationTool
{
    public class MyColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground
        {
            get
            {
                return Color.LightSlateGray;
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return Color.LightSlateGray;
            }
        }

        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return Color.LightSlateGray;
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return Color.LightSlateGray;
            }
        }

        public override Color MenuStripGradientBegin
        {
            get
            {
                return Color.White;
            }
        }

        public override Color MenuStripGradientEnd
        {
            get
            {
                return Color.White;
            }
        }
        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(60, 70, 80); }
        }
        
        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return Color.LightSlateGray;
            }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return Color.LightSlateGray;
            }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return Color.LightSlateGray;
            }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return Color.LightSlateGray;
            }
        }
        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(60, 70, 80); }
        }
    }
}
