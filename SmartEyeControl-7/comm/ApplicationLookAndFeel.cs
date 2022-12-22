using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SmartEyeControl_7
{

    public class ApplicationLookAndFeel
    {
        static void ApplyTheme(TextBox c)
        {
            c.Font = new Font("Calibri", 10.0f); c.BackColor = Color.Blue; c.ForeColor = Color.White;
        }
        static void ApplyTheme(Label c)
        {
            c.Font = new Font("Calibri", 10.0f,FontStyle.Bold); c.BackColor = Color.Black; c.ForeColor = Color.White;
        }
        static void ApplyTheme(Form c)
        {
            c.Font = new Font("Arial", 10.0f); c.BackColor = Color.Black; c.ForeColor = Color.White;
        }

        static void ApplyTheme(UserControl c)
        {
            c.Font = new Font("Arial", 10.0f); c.BackColor = Color.Black; c.ForeColor = Color.White;
        }
        static void ApplyButtonTheme(Button c)
        {
            c.FlatStyle = FlatStyle.Flat;
            c.ImageAlign = ContentAlignment.MiddleLeft;
            c.Font = new Font("Microsoft Sans Serif", 8.0f);
        }


        public static Control UseTheme(Control ParentCntl)
        {

            if (ParentCntl.GetType() == typeof(Button))
                ApplyButtonTheme((Button)ParentCntl);

            foreach (Control ChildCntl in ParentCntl.Controls)
            {

                Control ResultCntl = UseTheme(ChildCntl);

                if (ResultCntl != null)
                    return ResultCntl;

            }

            return null;

        }

        //public static void ApplyButtonStyle(UserControl controls)
        //{
        //    Control contr = null;
        //    foreach (Control c in controls.Controls)
        //    {
                
                

        //        if (c.GetType() == typeof(Button)) ApplyButtonTheme((Button)c);
        //        else if (c.GetType() == typeof(TabControl))
        //        {
        //            foreach (TabPage tp in ((TabControl)c).TabPages)
        //            {
        //                foreach (var vv in tp.Controls)
        //                {
        //                    if (vv.GetType() == typeof(Button)) ApplyButtonTheme((Button)vv);
        //                }
        //            }
        //        }
        //    }
        //}

        private static void FormatControl(Control c)
        {
            switch (c.GetType().ToString())
            {
                case "System.Windows.Forms.TextBox":
                    ApplyTheme((TextBox)c);
                    break;
                case "System.Windows.Forms.Label":
                    ApplyTheme((Label)c);
                    break;
            }
        }
    }
}
