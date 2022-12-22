using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(DisplayWindows))]
    [XmlInclude(typeof(DisplayWindowItem))]
    public class DisplayWindows : IParam, ISerializable, ICloneable
    {
        #region Data_Members

        private TimeSpan scrollTime;
        private List<DisplayWindowItem> windows;
        private DisplayWindowItem[] _windowsDesInternal;
        private DispalyWindowsModes windowMode;
        public static int MaxWidowCount = 50;

        #endregion

        #region Properties
        
        [XmlIgnore()]
        public TimeSpan ScrollTime
        {
            get { return scrollTime; }
            set
            {
                if (value.TotalSeconds > 0 && value.TotalSeconds <= 255)
                    scrollTime = value;
                else
                    throw new Exception("Invalid Windows Scrolling Time");
            }
        }

        [XmlElement("ScrollTimeRaw", typeof(long))]
        public long ScrollTimeRaw
        {
            get
            {
                return ScrollTime.Ticks;
            }
            set
            {
                this.scrollTime = new TimeSpan(value);
            }
        }

        [XmlElement("DisplayWindowItems", typeof(List<DisplayWindowItem>))]
        public List<DisplayWindowItem> Windows
        {
            get { return windows; }
            set { windows = value; }
        }

        [XmlElement("DisplayWindowsMode", typeof(DispalyWindowsModes))]
        public DispalyWindowsModes WindowsMode
        {
            get { return windowMode; }
            set { windowMode = value; }
        }

        [XmlIgnore()]
        public bool IsValid
        {
            get
            {
                if (!(ScrollTime.TotalSeconds > 0 && ScrollTime.TotalSeconds <= 255))   ///Verify Scroll Time
                    return false;
                if (Windows != null && Windows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        #endregion

        public DisplayWindows()
        {
            Windows = new List<DisplayWindowItem>();
            scrollTime = new TimeSpan(0, 0, 15);
            windowMode = DispalyWindowsModes.Normal;
        }

        public void AddWindow(DisplayWindowItem windowItem)
        {
            if (windows.Count < MaxWidowCount)
                windows.Add(new DisplayWindowItem(windowItem));
            else
                throw new Exception("Unable to Add Window,maximum already added");
        }

        public void AddWindowRange(DisplayWindowItem[] windowItems)
        {
            if (windowItems != null)
            {
                if (windows.Count + windowItems.Length <= MaxWidowCount)
                {
                    foreach (var winItemToAdd in windowItems)
                    {
                        windows.Add(new DisplayWindowItem(winItemToAdd));
                    }
                }
                else
                    throw new Exception("Unable to Add Windows,maximum already added");
            }
            else
                throw new Exception("Unable to Add Windows,null reference");
        }

        public int GetWindowCount()
        {
            return windows.Count;
        }

        public void RemoveWindow(DisplayWindowItem windowItem)
        {
            for (int index = 0; index < Windows.Count; index++)
            {
                DisplayWindowItem item = Windows[index];
                if (item.Obis_Index == windowItem.Obis_Index || 
                    item.Window_Name.Equals(windowItem.Window_Name))
                {
                    Windows.Remove(item);
                    break;
                }
            }
        }

        public DisplayWindowItem FindWindow(DisplayWindowItem windowItem)
        {
            return Windows.Find((x) => x.Obis_Index == windowItem.Obis_Index ||
                                 x.Window_Name.Equals(windowItem.Window_Name));
        }

        public void RemoveAll()
        {
            if (windows == null)
                windows = new List<DisplayWindowItem>();
            else
                windows.Clear();
        }

        public void ReplaceWindow(DisplayWindowItem windowItemOld, DisplayWindowItem windowItemNew)
        {
            try
            {
                int windowIndex = Windows.FindIndex((x) => x.Obis_Index == windowItemOld.Obis_Index || 
                                                     x.Window_Name.Equals(windowItemOld.Window_Name));
                if (windowIndex == -1)
                    throw new Exception("No window exists to replace");
                Windows[windowIndex] = new DisplayWindowItem(windowItemNew);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ISerializable

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public DisplayWindows(SerializationInfo info, StreamingContext context)
        {
            ///Deserialize DisplayWindows Object
            ///Getting ScrollTime Type Long
            this.ScrollTimeRaw = Convert.ToInt64(info.GetValue("ScrollTimeRaw", typeof(long)));
            ///Getting DisplayWindowsMode Type DispalyWindowsModes
            this.WindowsMode = (DispalyWindowsModes)info.GetValue("DisplayWindowsMode",typeof(DispalyWindowsModes));
            ///Getting DisplayWindowItems Type List<DisplayWindowItem>
            ///_windowsDesInternal = (DisplayWindowItem[])info.GetValue("DisplayWindowItems", typeof(DisplayWindowItem[]));
            ///this.windows = new List<DisplayWindowItem>(_windowsDesInternal);
            this.windows = new List<DisplayWindowItem>();
            ///windows.AddRange(localWin);
            ///Getting DisplayWindowItemsCount Type int
            int maxWinCount = info.GetInt32("DisplayWindowItemsCount");
            DisplayWindowItem WinItem = null;
            for (int Count = 1; Count <= maxWinCount; Count++)
            {
                WinItem = null;
                ///Adding DisplayWindowItem Type DisplayWindowItem
                WinItem = (DisplayWindowItem)info.GetValue(String.Format("DisplayWindowItem_{0}", Count)
                ,typeof(DisplayWindowItem));
                this.windows.Add(WinItem);
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)this).GetObjectData(info, context);
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Serialize DisplayWindows Object
            ///Adding ScrollTime Type Long
            info.AddValue("ScrollTimeRaw", this.ScrollTimeRaw);
            ///Adding DisplayWindowsMode Type DispalyWindowsModes
            info.AddValue("DisplayWindowsMode", this.WindowsMode);
            ///Adding DisplayWindowItemsCount Type int
            info.AddValue("DisplayWindowItemsCount",
                this.Windows.Count,
                typeof(int));
            for (int Count = 1; Count <= this.Windows.Count; Count++)
            {
                ///Adding Type DisplayWindowItem
                info.AddValue(String.Format("DisplayWindowItem_{0}",Count),
                    this.Windows[Count-1],
                    typeof(DisplayWindowItem));
            }
        }

        #endregion

        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(256))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while Clone Object", ex);
            }
        }

    }

    [Serializable]
    [XmlInclude(typeof(WindowNumber))]
    public class DisplayWindowItem : ICloneable,ISerializable
    {
        #region Data_Members

        private string window_Name;
        private List<DisplayWindowsCategory> category;
        private Get_Index obis_Index;
        private byte attributeSelected;
        private WindowNumber windowNumberToDisplay;

        #endregion

        #region Constructur

        public DisplayWindowItem()
        {
            window_Name = "";
            category = new List<DisplayWindowsCategory>();
            obis_Index = Get_Index.Dummy;
            attributeSelected = 0;
            windowNumberToDisplay = 1;
        }

        public DisplayWindowItem(DisplayWindowItem WindowItem)
        {
            this.Window_Name = WindowItem.Window_Name;
            this.Category = WindowItem.Category;
            this.Obis_Index = WindowItem.Obis_Index;
            this.AttributeSelected = WindowItem.AttributeSelected;
            this.WindowNumberToDisplay = WindowItem.WindowNumberToDisplay;
        }

        public DisplayWindowItem(string WinName, DisplayWindowsCategory Category, 
            Get_Index OBIS_Index, byte AttributeNo, WindowNumber WinNumberToShow)
            : this()
        {
            this.Window_Name = WinName;
            this.Obis_Index = OBIS_Index;
            this.AttributeSelected = AttributeNo;
            this.WindowNumberToDisplay = WinNumberToShow;
            this.Category.Add(Category);
        }

        #endregion

        #region Properties
        
        [XmlElement("OBISIndex", typeof(Get_Index))]
        public Get_Index Obis_Index
        {
            get { return obis_Index; }
            set { obis_Index = value; }
        }

        [XmlElement("SelectedAttributeNo", typeof(byte))]
        public byte AttributeSelected
        {
            get { return attributeSelected; }
            set { attributeSelected = value; }
        }

        [XmlElement("WindowNumberToDispay", typeof(WindowNumber))]
        public WindowNumber WindowNumberToDisplay
        {
            get { return windowNumberToDisplay; }
            set { windowNumberToDisplay = value; }
        }

        [XmlElement("WindowName", typeof(String))]
        public string Window_Name
        {
            get { return window_Name; }
            set { window_Name = value; }
        }

        [XmlElement("Category", typeof(List<DisplayWindowsCategory>))]
        public List<DisplayWindowsCategory> Category
        {
            get { return category; }
            set { category = value; }
        }
        
        #endregion

        public override string ToString()
        {
            return String.Format("{0}", Window_Name);
        }

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return new DisplayWindowItem(this);
        }

        public object Clone()
        {
            return ((ICloneable)this).Clone();
        }

        #endregion

        #region ISerializable

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public DisplayWindowItem(SerializationInfo info, StreamingContext context)
        {
            ///Deserialize DisplayWindowItem Object
            ///Getting window_Name Type String
            this.window_Name = (String)(info.GetValue("WindowName", typeof(String)));
            ///Getting OBISIndex Type Get_Index
            this.obis_Index =(Get_Index)(info.GetValue("OBISIndex", typeof(Get_Index)));
            ///Getting SelectedAttributeNo Type typeof(byte)
            this.attributeSelected = (byte)(info.GetValue("SelectedAttributeNo", typeof(byte)));
            ///Getting WindowNumberToDispay Type typeof(WindowNumber)
            this.windowNumberToDisplay.Value = Convert.ToUInt16(info.GetValue("WindowNumberToDispay", typeof(ushort)));
            ///Getting DisplayWindowItems Type List<DisplayWindowItem>
            DisplayWindowsCategory[] localWinCat = (DisplayWindowsCategory[])info.GetValue("Category", typeof(DisplayWindowsCategory[]));
            this.category = new List<DisplayWindowsCategory>();
            category.AddRange(localWinCat);
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)this).GetObjectData(info, context);
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Serialize DisplayWindowItem Object
            ///Add window_Name Type String
            info.AddValue("WindowName", this.window_Name);
            ///Add OBISIndex Type Get_Index
            info.AddValue("OBISIndex", this.obis_Index, typeof(Get_Index));
            ///Add SelectedAttributeNo Type typeof(byte)
            info.AddValue("SelectedAttributeNo", this.attributeSelected, typeof(byte));
            ///Add WindowNumberToDispay Type typeof(WindowNumber)
            info.AddValue("WindowNumberToDispay", this.windowNumberToDisplay.Value, typeof(ushort));
            ///Add DisplayWindowItems Type List<DisplayWindowItem>
            info.AddValue("Category", this.category.ToArray(), typeof(DisplayWindowsCategory[]));
        }

        #endregion

    }

    [Serializable]
    [XmlInclude(typeof(WindowNumber))]
    public struct WindowNumber
    {
        #region DataMembers

        private ushort value;
        public static readonly ushort WindowNumberNullable = 0xFFF;
        public static readonly ushort MaxWindowNumber = 999;
        public static readonly string EmptyOBIS_Field = "XYZ";

        #endregion

        #region Internal_WindowNumber_Flags

        private bool Flag_4
        {
            get
            {
                return ((this.value & 0x8000) != 0) ? true : false;
            }
            set
            {
                if (value)
                    this.value = (ushort)(this.value | 0x8000);
                else
                    this.value = (ushort)(this.value & 0x7FFF);
            }
        }

        private bool Flag_3
        {
            get
            {
                return ((this.value & 0x4000) != 0) ? true : false;
            }
            set
            {
                if (value)
                    this.value = (ushort)(this.value | 0x4000);
                else
                    this.value = (ushort)(this.value & 0xBFFF);
            }

        }

        private bool Flag_2
        {
            get
            {
                return ((this.value & 0x2000) != 0) ? true : false;
            }
            set
            {
                if (value)
                    this.value = (ushort)(this.value | 0x2000);
                else
                    this.value = (ushort)(this.value & 0xDFFF);
            }

        }

        private bool Flag_1
        {
            get
            {
                return ((this.value & 0x1000) != 0) ? true : false;
            }
            set
            {
                if (value)
                    this.value = (ushort)(this.value | 0x1000);
                else
                    this.value = (ushort)(this.value & 0xEFFF);
            }
        }

        #endregion

        #region Properties

        [XmlElement("DisplayWindowNumberRaw", typeof(ushort))]
        public ushort Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        [XmlIgnore()]
        public ushort DisplayWindowNumber
        {
            get
            {
                return (ushort)(value & 0x0FFF);
            }
            set
            {
                if (value >= 1 && value <= 999)
                {
                    this.value = (ushort)(this.value & 0xF000);
                    this.value |= value;
                }
                else
                    throw new Exception(String.Format("Invalid Window Number {0}", value));
            }
        }

        [XmlIgnore()]
        public bool IsValid
        {
            get
            {
                if (Flag_4)
                {
                    if ((Flag_3 || Flag_2 || Flag_1))
                        return true;
                    else
                        return false;
                }
                else
                {
                    ///Display Window # 1-999 & Flag_1 Is Zero
                    if (DisplayWindowNumber > 0 && DisplayWindowNumber <= MaxWindowNumber && !Flag_1)
                        return true;
                    else
                        return false;
                }
            }
        }

        [XmlIgnore()]
        public bool OBIS_Code_Display_Mode
        {
            get
            {
                return Flag_4;
            }
            set
            {
                Flag_4 = value;
                if (!Flag_4)
                {
                    Display_OBIS_Field_C = false;
                    Display_OBIS_Field_D = false;
                    Display_OBIS_Field_E = false;
                }
            }
        }

        [XmlIgnore()]
        public bool Display_OBIS_Field_E
        {
            get
            {
                return Flag_1;
            }
            set
            {
                if (OBIS_Code_Display_Mode)
                    Flag_1 = value;
            }
        }

        [XmlIgnore()]
        public bool Display_OBIS_Field_D
        {
            get
            {
                return Flag_2;
            }
            set
            {
                if (OBIS_Code_Display_Mode)
                    Flag_2 = value;
            }
        }

        [XmlIgnore()]
        public bool Display_OBIS_Field_C
        {
            get
            {
                return Flag_3;
            }
            set
            {
                if (OBIS_Code_Display_Mode)
                    Flag_3 = value;
            }
        }

        [XmlIgnore()]
        public bool Display_Dot_1
        {
            get
            {
                return Flag_2;
            }
            set
            {
                if (!OBIS_Code_Display_Mode)
                    Flag_2 = value;
            }

        }

        [XmlIgnore()]
        public bool Display_Dot_2
        {
            get
            {
                return Flag_3;
            }
            set
            {
                if (!OBIS_Code_Display_Mode)
                    Flag_3 = value;
            }
        }

        #endregion

        public static implicit operator WindowNumber(ushort value)
        {
            return new WindowNumber() { Value = value };
        }

        public override string ToString()
        {
            ///Print Window Number In Specified Format
            if (IsValid)
            {
                string RetValue = "";
                if (this.OBIS_Code_Display_Mode)
                {
                    if (Display_OBIS_Field_C)
                        RetValue += "C";
                    else
                        RetValue += "Y";
                    if (Display_OBIS_Field_D)
                        RetValue += "D";
                    else
                        RetValue += "Y";
                    if (Display_OBIS_Field_E)
                        RetValue += "E";
                    else
                        RetValue += "Y";
                }
                else
                {
                    RetValue = this.DisplayWindowNumber.ToString("D3");
                    if (Display_Dot_2)
                        RetValue = RetValue.Insert(1, ".");
                    if (Display_Dot_1 && Display_Dot_2)
                        RetValue = RetValue.Insert(3, ".");
                    else if (Display_Dot_1 && !Display_Dot_2)
                        RetValue = RetValue.Insert(2, ".");
                }
                return RetValue;

            }
            else
                return "Invalid Format";
        }

        public string ToString(Get_Index OBIS_Index)
        {
            try
            {
                ///Print Window Number In Specified Format
                if (IsValid)
                {
                    String RetValue = "";

                    if (this.OBIS_Code_Display_Mode)
                    {
                        StOBISCode OBIS_Code = OBIS_Index;
                        if (Display_OBIS_Field_C)
                            //RetValue +=   OBIS_Code.OBISCode_Feild_C.ToString("D3") + "."; //ahmed
                            RetValue += OBIS_Code.OBISCode_Feild_C.ToString() + ".";
                        else
                            RetValue += EmptyOBIS_Field + ".";
                        if (Display_OBIS_Field_D)
                            RetValue += OBIS_Code.OBISCode_Feild_D.ToString() + ".";
                        else
                            RetValue += EmptyOBIS_Field + ".";
                        if (Display_OBIS_Field_E)
                            RetValue += OBIS_Code.OBISCode_Feild_E.ToString();
                        else
                            RetValue += EmptyOBIS_Field;
                    }
                    else
                    {
                        RetValue = this.DisplayWindowNumber.ToString("D3");
                        if (Display_Dot_2)
                            RetValue = RetValue.Insert(1, ".");
                        if (Display_Dot_1 && Display_Dot_2)
                            RetValue = RetValue.Insert(3, ".");
                        else if (Display_Dot_1 && !Display_Dot_2)
                            RetValue = RetValue.Insert(2, ".");
                    }
                    return RetValue;

                }
                else
                    return "Invalid Format";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    public enum DispalyWindowsModes : byte
    {
        Normal = 1,
        Alternate,
        Test
    }

    public enum DisplayWindowsCategory : byte
    {
        Misc = 0,
        Instantaneous,
        Billing,
        Event,
        Last_Month
    }
}


//'281476587389183
//'281476588111359
//'281476593811711
//'282574488404223
//'282574488930047
//'282574488930303
//'845524693745919
//'845526055649535
//'845526055649791
//'845526055650047
//'845526055650303
//'845526055650559
//'845526055715071
//'845526055715327
//'845526055715583
//'845526055715839
//'845526055716095
//'845526055780607
//'845526055780863
//'845526055781119
//'845526055781375
//'845526055781631
//'845526055911679
//'845526055911935
//'845526055912191
//'845526055912447
//'845526055912703
//'845526056042751
//'845526056043007
//'845526056043263
//'845526056043519
//'845526056043775
//'845526056108287
//'845526056108543
//'845526056108799
//'845526056109055
//'845526056109311
//'845526056173823
//'845526056174079
//'845526056174335
//'845526056174591
//'845526056174847
//'845526056239359
//'845526056239615
//'845526056239871
//'845526056240127
//'845526056240383
//'845526056304895
//'845526056305151
//'845526056305407
//'845526056305663
//'845526056305919
//'845526056501503
//'845526056501759
//'845526056502015
//'845526056502271
//'845526056502527
//'845526056567039
//'845526056567295
//'845526056567551
//'845526056567807
//'845526056568063
//'1127001033343231
//'1127001033343487
//'1127001033343743
//'1127001033343999
//'1127001033344255
//'1127001033408767
//'1127001033409023
//'1127001033409279
//'1127001033409535
//'1127001033409791
//'1127001033736447
//'1127001033736703
//'1127001033736959
//'1127001033737215
//'1127001033737471
//'1127001033801983
//'1127001033802239
//'1127001033802495
//'1127001033802751
//'1127001033803007
//'282574488928767
//'845526100738303
//'282574488929023
//'281476587783679
//'845524962312447
//'844426541138943
//'845525297856767
//'845525633401087
//'282576103735551
//'845524677099775
//'281476587391743
//'845524979089663
//'845525314633983
//'845525650178303
//'845524459061759
//'845524459062015
//'845524459062271
//'845524459062527
//'845524459061503
//'845524475838975
//'845524475839231
//'845524475839487
//'845524475839743
//'845524475838719
//'845524693942783
//'845524693943039
//'845524693943295
//'845524693943551
//'845524693942527
//'845524526170623
//'845524526170879
//'845524526171135
//'845524526171391
//'845524526170367
//'845524542947839
//'845524928758015
//'845524542948095
//'845525264302335
//'845524542948351
//'845525599846655
//'845524542948607
//'845524593213695
//'845524542947583
//'845524794540287
//'845524559725055
//'845524811317503
//'845524559725311
//'845525130084607
//'845524559725567
//'845525146861823
//'845524559725823
//'845525465628927
//'845524559724799
//'845525482406143
//'845524458995967
//'845524475773183
//'845524828094719
//'845524576502271
//'845524844871935
//'845524576502527
//'845525163639039
//'845524576502783
//'845525180416255
//'845524576503039
//'845525499183359
//'845524576502015
//'845525515960575
//'845524492550399
//'845524509327615
//'845526058795519
//'845526058795775
//'845526058796031
//'845524660322559
//'845524995866879
//'845526024847871
//'845525331411199
//'845526024848127
//'845525666955519
//'845526024848383
//'845526024848639
//'845525046198527
//'845526024847615
//'845525381742847
//'845525717287167
//'845524710654207
//'845524593279487
//'1126999670522367
//'845524593279743
//'1126999670522623
//'845524593279999
//'1126999670522879
//'845524593280255
//'1126999670523135
//'845524593279231
//'1126999670522111
//'845526100673023
//'1126999469195775
//'845526100673279
//'1126999469196031
//'845526100673535
//'1126999469196287
//'845526100673791
//'1126999469196543
//'845526100672767
//'1126999469195519
//'845524659929599
//'845524659929855
//'845524659930111
//'845524659930367
//'845524659929343
//'1126999670260223
//'1126999670260479
//'1126999670260735
//'1126999670260991
//'1126999670259967
//'1126999468933631
//'1126999468933887
//'1126999468934143
//'1126999468934399
//'1126999468933375

