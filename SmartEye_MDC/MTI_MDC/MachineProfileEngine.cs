// Copyright (C) 2014 MicroTech Industries
// All rights reserved.
// CODED by Muhammad Abubakar.
// Last Modified: 18/03/2014

namespace System
{
    using Management;

    [Serializable]
    public sealed class MachineProfileEngine
    {
        #region Public Methods

        public string GetProfile()
        {
            this.m_id = string.Format("MTI-Salt-AOFS*(&973^%92OIjh&E975aarasaa143*$43JH(*&IASYREY34FI^&524{0}{1}{2}{3}",
                                    GetCPUID(), GetHDDID(), GetChasisID(), GetBIOSID()).Replace(" ", "");
            return this.m_id;
        }

        #endregion

        #region Private Methods

        string GetIdentifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (var obj in moc)
            {
                ManagementObject mo = (ManagementObject)obj;
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch (Exception) { }
                }
            }
            return result;
        }

        string GetIdentifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (var obh in moc)
            {
                ManagementObject mo = (ManagementObject)obh;
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch (Exception) { }
                    }
                }
            }
            return result;
        }

        private string GetCPUID()
        {
            string strID = "";

            try
            {
                strID = this.GetIdentifier("Win32_Processor", "UniqueId");
                if (strID == "")
                {
                    strID = this.GetIdentifier("Win32_Processor", "ProcessorId");
                    if (strID == "")
                    {
                        strID = this.GetIdentifier("Win32_Processor", "Name");
                        if (strID == "")
                        {
                            strID = this.GetIdentifier("Win32_Processor", "Manufacturer");
                        }
                        strID += this.GetIdentifier("Win32_Processor", "MaxClockSpeed");
                    }
                }
            }
            catch (Exception) { }

            return strID;
        }

        private string GetHDDID()
        {
            return string.Format("{0}{1}{2}{3}",
                this.GetIdentifier("Win32_DiskDrive", "Model"),
                this.GetIdentifier("Win32_DiskDrive", "Manufacturer"),
                this.GetIdentifier("Win32_DiskDrive", "Signature"),
                this.GetIdentifier("Win32_DiskDrive", "TotalHeads"));
        }

        private string GetChasisID()
        {
            return string.Format("{0}{1}{2}{3}",
                this.GetIdentifier("Win32_BaseBoard", "Model"),
                this.GetIdentifier("Win32_BaseBoard", "Manufacturer"),
                this.GetIdentifier("Win32_BaseBoard", "Name"),
                this.GetIdentifier("Win32_BaseBoard", "SerialNumber"));
        }

        private string GetBIOSID()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}",
                this.GetIdentifier("Win32_BIOS", "Manufacturer"),
                this.GetIdentifier("Win32_BIOS", "SMBIOSBIOSVersion"),
                this.GetIdentifier("Win32_BIOS", "IdentificationCode"),
                this.GetIdentifier("Win32_BIOS", "SerialNumber"),
                this.GetIdentifier("Win32_BIOS", "ReleaseDate"),
                this.GetIdentifier("Win32_BIOS", "Version"));
        }

        private string GetMACID()
        {
            return this.GetIdentifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }

        #endregion

        #region Properties

        public static MachineProfileEngine Current
        {
            get { return s_engine; }
        }

        public string HID
        {
            get
            {
                return string.IsNullOrEmpty(m_id)
                    ? this.GetProfile()
                    : this.m_id;
            }
            set { m_id = value; }
        }

        #endregion

        #region Fields

        static readonly MachineProfileEngine s_engine = new MachineProfileEngine();

        string m_id = "";
        [NonSerialized]
        byte[] key = { 8, 8, 1, 3, 8, 1, 4, 3 };
        [NonSerialized]
        byte[] iv = { 1, 4, 3, 1, 3, 9, 8, 8 };

        #endregion // Fields
    }
}
