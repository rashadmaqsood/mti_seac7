using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Communicator
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
            
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.lbl_productName.Text = AssemblyProduct;
            this.lbl_version.Text = String.Format("Version:{0}", AssemblyVersion);
            this.lbl_copyRights.Text = AssemblyCopyright;
            this.lbl_CompanyName.Text = AssemblyCompany;
            this.lbl_buildDate.Text = GetBuildDateTime(Assembly.GetEntryAssembly()).ToString("yyyy-MM-dd HH:mm:ss");
            //this.textBoxDescription.Text = AssemblyDescription;
            String description = AssemblyDescription;
           // textBoxDescription.Text = description;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        static DateTime GetBuildDateTime(Assembly assembly)
        {
            if (File.Exists(assembly.Location))
            {
                var buffer = new byte[Math.Max(Marshal.SizeOf(typeof(_IMAGE_FILE_HEADER)), 4)];
                using (var fileStream = new FileStream(assembly.Location, FileMode.Open, FileAccess.Read))
                {
                    fileStream.Position = 0x3C;
                    fileStream.Read(buffer, 0, 4);
                    fileStream.Position = BitConverter.ToUInt32(buffer, 0); // COFF header offset
                    fileStream.Read(buffer, 0, 4); // "PE\0\0"
                    fileStream.Read(buffer, 0, buffer.Length);
                }
                var pinnedBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                try
                {
                    var coffHeader = (_IMAGE_FILE_HEADER)Marshal.PtrToStructure(pinnedBuffer.AddrOfPinnedObject(), typeof(_IMAGE_FILE_HEADER));

                    return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1) + new TimeSpan(coffHeader.TimeDateStamp * TimeSpan.TicksPerSecond));
                }
                finally
                {
                    pinnedBuffer.Free();
                }
            }
            return new DateTime();
        }
        struct _IMAGE_FILE_HEADER
        {
            public ushort Machine;
            public ushort NumberOfSections;
            public uint TimeDateStamp;
            public uint PointerToSymbolTable;
            public uint NumberOfSymbols;
            public ushort SizeOfOptionalHeader;
            public ushort Characteristics;
        };
        private void AboutBox1_Shown(object sender, EventArgs e)
        {
            try
            {
              //  textBoxDescription.Text = "";
                //textBoxDescription.Enabled = false;
              //  StreamReader f = new StreamReader(new FileStream(Environment.CurrentDirectory + "\\Version Log.txt", FileMode.Open));
              //  textBoxDescription.Text = f.ReadToEnd();
              //  f.Close();
            }
            catch
            { }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        #region ShortCutKey
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData == (Keys.Control | Keys.M)))
            {
                const int Margin = 20;
                Label CopyRightsLabel = lbl_copyRights;
                if (Cursor.Position.X >= this.Location.X &&
                    Cursor.Position.X <= this.Location.X + Margin &&
                    Cursor.Position.Y >= this.Location.Y + this.Size.Height - Margin &&
                    Cursor.Position.Y <= this.Location.Y + this.Size.Height)
                {
                    string ExpDate = " __ Ex:: "+ProductValidationEngine.Current.ExpiryDate.ToString("yyyyMMdd");
                    string LabelText = CopyRightsLabel.Text;
                    if (LabelText.EndsWith(ExpDate)) LabelText = LabelText.Replace(ExpDate, string.Empty);
                    else LabelText += ExpDate;
                    CopyRightsLabel.Text = LabelText;
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
