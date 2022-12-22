using DatabaseConfiguration.DataSet;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharedCode.Comm.HelperClasses
{
    public class DisplayWindowsHelper
    {
        List<DisplayWindowItem> SelectedWindowItems = null;
        private WindowNumber windowDisplayNumberFormat;
        List<StOBISCode> _All_OBISCodeLst = null;
        public Func<Get_Index, StOBISCode> OBISLabelLookup;

        public List<StOBISCode> All_OBISCodeLst
        {
            get { return _All_OBISCodeLst; }
            set { _All_OBISCodeLst = value; }
        }

        public List<DisplayWindowItem> SelectedWindows
        {
            get { return SelectedWindowItems; }
            set { SelectedWindowItems = value; }
        }

        public List<StOBISCode> DisplayWindowsAll_OBISCodeLst
        {
            get
            {
                List<StOBISCode> All_ActualOBISCodeLstLocal = new List<StOBISCode>();
                StOBISCode OBISCode;

                foreach (var winItems in SelectedWindowItems)
                {
                    try
                    {
                        if (winItems != null)
                        {
                            OBISCode = GetOBISCode(winItems.Obis_Index);
                            All_ActualOBISCodeLstLocal.Add(OBISCode);
                        }
                    }
                    catch
                    {
                    }
                }

                return All_OBISCodeLst;
            }
        }

        public DisplayWindowsHelper()
        {
            SelectedWindowItems = GetSelectableWindows();
            WindowDisplayNumberFormat = 0;
        }
        
        public WindowNumber WindowDisplayNumberFormat
        {
            get { return windowDisplayNumberFormat; }
            set 
            {
                windowDisplayNumberFormat = value; 
            }
        }

        public List<DisplayWindowItem> GetSelectableWindows()
        {
            try
            {
                List<DisplayWindowItem> Windows = new List<DisplayWindowItem>();
                DisplayWindowItem win = null;

                #region Misc
                ///Current Local Date
                win = new DisplayWindowItem();
                win.Window_Name = "Current Date";
                win.Obis_Index = Get_Index.Local_Date;
                win.AttributeSelected = 2;
                win.Category.Add(DisplayWindowsCategory.Misc);
                Windows.Add(win);

                ///Current Local Time
                win = new DisplayWindowItem();
                win.Window_Name = "Current Time";
                win.Obis_Index = Get_Index.Local_Time;
                win.AttributeSelected = 2;
                win.Category.Add(DisplayWindowsCategory.Misc);
                Windows.Add(win); 
                #endregion

                //#region Cumulative_Tariff1_KwhExport
                
                /////Cumulative_Tariff1_KwhImport
                //win = new DisplayWindowItem();
                //win.Window_Name = "Active Energy Import Tariff 1";
                //win.Obis_Index = Get_Index.Cumulative_Tariff1_KwhImport;
                //win.AttributeSelected = 0;
                //win.Category.Add(DisplayWindowsCategory.Billing);
                //Windows.Add(win);


                /////Cumulative_Tariff2_KwhImport
                //win = new DisplayWindowItem();
                //win.Window_Name = "Active Energy Import Tariff 2";
                //win.Obis_Index = Get_Index.Cumulative_Tariff2_KwhImport;
                //win.AttributeSelected = 0;
                //win.Category.Add(DisplayWindowsCategory.Billing);
                //Windows.Add(win);


                /////Cumulative_Tariff3_KwhImport
                //win = new DisplayWindowItem();
                //win.Window_Name = "Active Energy Import Tariff 3";
                //win.Obis_Index = Get_Index.Cumulative_Tariff3_KwhImport;
                //win.AttributeSelected = 0;
                //win.Category.Add(DisplayWindowsCategory.Billing);
                //Windows.Add(win);

                /////Cumulative_Tariff4_KwhImport
                //win = new DisplayWindowItem();
                //win.Window_Name = "Active Energy Import Tariff 4";
                //win.Obis_Index = Get_Index.Cumulative_Tariff4_KwhImport;
                //win.AttributeSelected = 0;
                //win.Category.Add(DisplayWindowsCategory.Billing);
                //Windows.Add(win);
                
                /////Cumulative_TariffTL_KwhImport
                //win = new DisplayWindowItem();
                //win.Window_Name = "Active Energy Import Total Tariff";
                //win.Obis_Index = Get_Index.Cumulative_TariffTL_KwhImport;
                //win.AttributeSelected = 0;
                //win.Category.Add(DisplayWindowsCategory.Billing);
                //Windows.Add(win);

                //#endregion

                //#region Voltage Windows

                /////Voltage_Ph_A
                //win = new DisplayWindowItem();
                //win.Window_Name = "Voltage Phase A";
                //win.Obis_Index = Get_Index.Voltage_Ph_A;
                //win.AttributeSelected = 0;
                //win.Category.Add(DisplayWindowsCategory.Instantanous);
                //Windows.Add(win);


                /////Voltage_Ph_B
                //win = new DisplayWindowItem();
                //win.Window_Name = "Voltage Phase B";
                //win.Obis_Index = Get_Index.Voltage_Ph_B;
                //win.AttributeSelected = 0;
                //win.Category.Add(DisplayWindowsCategory.Instantanous);
                //Windows.Add(win);


                /////Voltage_Ph_C
                //win = new DisplayWindowItem();
                //win.Window_Name = "Voltage Phase C";
                //win.Obis_Index = Get_Index.Voltage_Ph_C;
                //win.AttributeSelected = 0;
                //win.Category.Add(DisplayWindowsCategory.Instantanous);
                //Windows.Add(win);

                /////Voltage_Ph_Any
                //win = new DisplayWindowItem();
                //win.Window_Name = "Voltage Phase Any";
                //win.Obis_Index = Get_Index.Voltage_Any;
                //win.AttributeSelected = 0;
                //win.Category.Add(DisplayWindowsCategory.Instantanous);
                //Windows.Add(win);
                
                //#endregion

                return Windows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Display Window Number Format On All Windows
        
        public void ApplyWindowNumberDisplayFormat(WindowNumber FormatToDisplay, DisplayWindows DisplayWindowsObj)
        {
            try
            {
                if (DisplayWindowsObj != null &&
                    DisplayWindowsObj.Windows != null &&
                    DisplayWindowsObj.Windows.Count > 0)
                {
                    foreach (var item in DisplayWindowsObj.Windows)
                    {   
                        WindowNumber newNumber = item.WindowNumberToDisplay.DisplayWindowNumber;
                        
                        if (FormatToDisplay.OBIS_Code_Display_Mode)
                        {
                            newNumber.OBIS_Code_Display_Mode = true;
                            item.WindowNumberToDisplay = newNumber;
                            newNumber.Display_OBIS_Field_C = FormatToDisplay.Display_OBIS_Field_C;
                            newNumber.Display_OBIS_Field_D = FormatToDisplay.Display_OBIS_Field_D;
                            newNumber.Display_OBIS_Field_E = FormatToDisplay.Display_OBIS_Field_E;
                        }
                        else
                        {
                            newNumber.OBIS_Code_Display_Mode = false;
                            newNumber.Display_Dot_1 = FormatToDisplay.Display_Dot_1;
                            newNumber.Display_Dot_2 = FormatToDisplay.Display_Dot_2;
                        }                             
                        item.WindowNumberToDisplay = newNumber;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ApplyWindowNumberDisplayFormat(DisplayWindows DisplayWindowsObj)
        {
            ApplyWindowNumberDisplayFormat(WindowDisplayNumberFormat, DisplayWindowsObj);
        }

        public void ApplyWindowNumberDisplayFormat()
        {
            try
            {
                DisplayWindows windowObjTemp = new DisplayWindows();
                windowObjTemp.Windows = this.SelectedWindows;
                ApplyWindowNumberDisplayFormat(this.WindowDisplayNumberFormat,windowObjTemp);
            }
            catch (Exception ex_)
            {
                throw ex_;
            }
        }

        public WindowNumber GetWindowsFormat(DisplayWindows DisplayWindowsObj)
        {
            try
            {   
                WindowNumber WinFormat = 1;
                if (DisplayWindowsObj.Windows != null && DisplayWindowsObj.Windows.Count > 0)
                {
                    WindowNumber Receive = DisplayWindowsObj.Windows[0].WindowNumberToDisplay;
                    WinFormat.OBIS_Code_Display_Mode = Receive.OBIS_Code_Display_Mode;
                    WinFormat.Display_OBIS_Field_C = Receive.Display_OBIS_Field_C;
                    WinFormat.Display_OBIS_Field_D = Receive.Display_OBIS_Field_D;
                    WinFormat.Display_OBIS_Field_E = Receive.Display_OBIS_Field_E;
                }
                else
                {
                   WinFormat = 1;
                   return WinFormat;
                }
                return WinFormat;
                
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public void GetWindowsFormat(DisplayWindows DisplayWindowsObj,int dummy)
        {
            this.WindowDisplayNumberFormat =  GetWindowsFormat(DisplayWindowsObj);
        }

        #endregion

        #region Save_Load_DisplayWindows
        
        public DisplayWindows LoadDisplayWindows(String fileUrl)
        {
            try
            {
                FileInfo WindowsFile = new FileInfo(fileUrl);
                DisplayWindows win = null;
                if (WindowsFile.Exists)
                {
                    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(DisplayWindows));
                    using (FileStream new_File = new FileStream(fileUrl, FileMode.Open))
                    {
                        win =(DisplayWindows)x.Deserialize(new_File);
                    }
                }
                else
                    throw new Exception("Unable to find file on specified path");
                return win;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading Display Windows", ex);
            }
        }

        public void SaveDisplayWindows(DisplayWindows DisplayWindowObj, String fileUrl)
        {
            try
            {
                FileInfo WindowsFile = new FileInfo(fileUrl);
                DirectoryInfo Directoryinfo = WindowsFile.Directory;
                if (!Directoryinfo.Exists)
                    Directoryinfo.Create();
                if (WindowsFile.Exists || true)
                {
                    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(DisplayWindows));
                    using (FileStream new_File = new FileStream(fileUrl, FileMode.Create))
                    {
                        x.Serialize(new_File, DisplayWindowObj);
                    }
                }
                else
                    throw new Exception("Unable to find file on specified path");

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while saving DisplayWindows", ex);
            }
        }

        public List<DisplayWindowItem> LoadSelectableDisplayWindows(string fileUrl)
        {
            try
            {
                FileInfo WindowsFile = new FileInfo(fileUrl);
                List<DisplayWindowItem> win = null;
                if (WindowsFile.Exists)
                {
                    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(List<DisplayWindowItem>));
                    using (FileStream new_File = new FileStream(fileUrl, FileMode.Open))
                    {
                        win = (List<DisplayWindowItem>)x.Deserialize(new_File);
                    }
                }
                else
                    throw new Exception("Unable to find file on specified path");
                this.SelectedWindows = win;
                return win;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading All Selectable Display Windows", ex);
            }
        }

        public List<DisplayWindowItem> LoadSelectableDisplayWindows(Configs AppConfigurater)
        {
            try
            {
                ConfigsHelper Helper = new ConfigsHelper(AppConfigurater);
                List<DisplayWindowItem> AllSelectable =  Helper.GetAllSelectableWindows();
                this.SelectedWindows = AllSelectable;
                return AllSelectable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while loading All Selectable Display Windows", ex); 
            }
        }

        public void SaveSelectableWindows(List<DisplayWindowItem> SelectableDispWindows, string fileUrl)
        {
            try
            {
                FileInfo WindowsFile = new FileInfo(fileUrl);
                DirectoryInfo Directoryinfo = WindowsFile.Directory;
                if (!Directoryinfo.Exists)
                    Directoryinfo.Create();
                if (WindowsFile.Exists || true)
                {
                    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(List<DisplayWindowItem>));
                    using (FileStream new_File = new FileStream(fileUrl, FileMode.Create))
                    {
                        x.Serialize(new_File, SelectableDispWindows);
                    }
                }
                else
                    throw new Exception("Unable to find file on specified path");

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while saving All Selectable Display Windows", ex);
            }   
        }
        
        #endregion
        
        #region Encode_Display_Windows

        public St_DisplayWindows Encode_DisplayWindows(DisplayWindows WindowObj)
        {
            try
            {
                St_DisplayWindows windowObjToSET = new St_DisplayWindows();
                windowObjToSET.Scroll_Time = Convert.ToByte(WindowObj.ScrollTime.TotalSeconds);
                windowObjToSET.DisplayWindows = new St_WindowItem[WindowObj.Windows.Count];
                int index = 0;
                foreach (var item in WindowObj.Windows)
                {
                    windowObjToSET.DisplayWindows[index++] = Encode_DispalyWindowItem(item);
                }
                return windowObjToSET;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public St_WindowItem Encode_DispalyWindowItem(DisplayWindowItem WindowItem)
        {
            try
            {
                St_WindowItem item = new St_WindowItem();
                ///Assign Windows Items
                item.WinNumberToDisplay = WindowItem.WindowNumberToDisplay.Value;
                item.AttributeNo = WindowItem.AttributeSelected;
                StOBISCode OBIS_T = WindowItem.Obis_Index;
                item.OBISCode = OBIS_T.OBISCode;
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public StOBISCode GetOBISCode(Get_Index OBIS_Index, bool logErr = true)
        {
            StOBISCode obisCode = Get_Index.Dummy;

            try
            {
                if (OBISLabelLookup == null)
                    throw new NotImplementedException("OBISLabelLookup");

                obisCode = OBISLabelLookup.Invoke(OBIS_Index);
            }
            catch (Exception ex)
            {
                obisCode = OBIS_Index;
#if DEBUG
                if (logErr)
                    System.Diagnostics.Debug.Write(string.Format("{0} OBIS Code Not Found {1}", OBIS_Index.ToString(), ex.Message));
#endif
                // throw;
            }

            return obisCode;
        }

        public static void InitialzeDisplayWindowsHelper(ApplicationProcess_Controller AP_ControllerLocal,
                                                         Configs Configurations, ref DisplayWindowsHelper displayWindowHelper)
        {
            try
            {
                if (displayWindowHelper == null)
                {
                    displayWindowHelper = new DisplayWindowsHelper();
                }

                #region Initiailzation

                // Load SelectedWindows
                if (AP_ControllerLocal != null && Configurations != null &&
                    (displayWindowHelper.SelectedWindows == null ||
                     displayWindowHelper.SelectedWindows.Count <= 0))
                {
                    displayWindowHelper.LoadSelectableDisplayWindows(Configurations);
                }

                Func<Get_Index, StOBISCode> DefaultOBISLabelLookup = new Func<Get_Index, StOBISCode>((x) => { StOBISCode OBISVal = x; return OBISVal; });

                if (AP_ControllerLocal != null &&
                    AP_ControllerLocal.ApplicationProcessSAPTable != null)
                {
                    DefaultOBISLabelLookup = new Func<Get_Index, StOBISCode>(AP_ControllerLocal.GetOBISCode);
                }

                // Initialize All OBISCodes here
                List<StOBISCode> All_OBISCodes = new List<StOBISCode>();

                if (displayWindowHelper.All_OBISCodeLst == null ||
                    displayWindowHelper.All_OBISCodeLst.Count <= 0)
                {
                    // if (AP_Controller != null &&
                    //     AP_Controller.ApplicationProcessSAPTable != null)
                    // {
                    //     All_OBISCodes = AP_Controller.ApplicationProcessSAPTable.
                    //                                   OBISLabelLookup.Values.ToList<StOBISCode>();
                    // }

                    if (All_OBISCodes == null || All_OBISCodes.Count <= 0)
                    {
                        ulong[] OBISCodesValues = (ulong[])Enum.GetValues(typeof(Get_Index));
                        StOBISCode OBISCodes = Get_Index.Dummy;

                        foreach (var obisVal in OBISCodesValues)
                        {
                            OBISCodes = Get_Index.Dummy;
                            OBISCodes.OBIS_Value = obisVal;

                            All_OBISCodes.Add(OBISCodes);
                        }
                    }
                }

                #endregion

                displayWindowHelper.OBISLabelLookup = DefaultOBISLabelLookup;
                if (All_OBISCodes != null && All_OBISCodes.Count > 0)
                    displayWindowHelper.All_OBISCodeLst = All_OBISCodes;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error occurred while Init DisplayWindowHelper," + ex.Message);
            }
        }

        #region Decode_Display_Windows

        public DisplayWindows Decode_DisplayWindows(St_DisplayWindows WindowObj)
        {
            try
            {
                List<StOBISCode> All_OBISCodesLocal = null;
                List<StOBISCode> DispWinAll_OBISCodes = null;

                All_OBISCodesLocal = All_OBISCodeLst;
                DispWinAll_OBISCodes = DisplayWindowsAll_OBISCodeLst;

                if ((DispWinAll_OBISCodes == null || DispWinAll_OBISCodes.Count <= 0) &&
                   (All_OBISCodesLocal == null || All_OBISCodesLocal.Count <= 0))
                {
                    throw new InvalidOperationException("All OBIS Codes List Not Initiailzed");
                }
                StOBISCode OBISCode = Get_Index.Dummy;

                DisplayWindows DispWindows = new DisplayWindows();
                DispWindows.ScrollTime = new TimeSpan(0, 0, WindowObj.Scroll_Time);

                foreach (var item in WindowObj.DisplayWindows)
                {
                    OBISCode = StOBISCode.FindByOBISCode(DispWinAll_OBISCodes, item.OBISCode);
                    if (OBISCode == Get_Index.Dummy)
                    {
                        OBISCode = StOBISCode.FindByOBISCode(All_OBISCodesLocal, item.OBISCode);
                    }
                    if (OBISCode == Get_Index.Dummy)
                    {
                        // Custom OBIS Code Not Found
                        OBISCode = StOBISCode.ConvertFrom(item.OBISCode);
                        OBISCode.ClassId = 1;
                    }

                    DisplayWindowItem WinItem = Decode_DisplayWindowItem(OBISCode, item);
                    DispWindows.Windows.Add(WinItem);
                }
                return DispWindows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DisplayWindowItem Decode_DisplayWindowItem(StOBISCode OBISCodeReceived, St_WindowItem WindowItemObj)
        {
            try
            {
                // Assign Windows Items

                if (OBISCodeReceived.OBISIndex == Get_Index.Dummy)
                {
                    throw new Exception(String.Format("Error occurred while Decode Display Window,{0} OBIS not exists",
                        Commons.ArrayToHexString(WindowItemObj.OBISCode)));
                }

                DisplayWindowItem Item = SelectedWindows.Find((x) =>
                {
                    bool isMatch = false;
                    StOBISCode localOBISToMatch = Get_Index.Dummy;
                    try
                    {
                        localOBISToMatch = GetOBISCode(x.Obis_Index, false);
                        isMatch = localOBISToMatch == OBISCodeReceived;
                    }
                    catch
                    {
                        isMatch = false;
                    }
                    return isMatch;
                });

                if (Item == null)
                {
                    Item = new DisplayWindowItem();
                    Item.Obis_Index = OBISCodeReceived.OBISIndex;
                    Item.AttributeSelected = WindowItemObj.AttributeNo;
                    Item.WindowNumberToDisplay = WindowItemObj.WinNumberToDisplay;

                    var TWinName = OBISCodeReceived.OBISIndex.ToString();
                    ulong OBISVAL = 0;

                    if (string.IsNullOrEmpty(TWinName) || ulong.TryParse(TWinName, out OBISVAL))
                    {
                        TWinName = "unknown";
                    }
                    else
                    {
                        TWinName = "**" + TWinName;
                    }

                    Item.Window_Name = TWinName;
                    if (Item.Category == null)
                        Item.Category = new List<DisplayWindowsCategory>();
                    Item.Category.Add(DisplayWindowsCategory.Misc);
                    // throw new Exception("Unable to find Window for Quantity " + Index);
                }
                Item = new DisplayWindowItem(Item);

                WindowNumber WinNum = WindowItemObj.WinNumberToDisplay;
                Item.WindowNumberToDisplay = WinNum;
                Item.AttributeSelected = WindowItemObj.AttributeNo;
                return Item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
