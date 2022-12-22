using System;
using System.Collections.Generic;
using System.IO;
using Comm;
using DLMS;
using AccurateOptocomSoftware.comm;
using DLMS.Comm;
using Comm.DataContainers;
using DatabaseConfiguration.DataSet;
using comm.DataContainer;

namespace comm
{
    public class DisplayWindowsHelper
    {
        List<DisplayWindowItem> SelectedWindowItems = null;
        private WindowNumber windowDisplayNumberFormat;
        
        public List<DisplayWindowItem> SelectedWindows
        {
            get { return SelectedWindowItems; }
            set { SelectedWindowItems = value; }
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

        #region Decode_Display_Windows

        public DisplayWindows Decode_DisplayWindows(St_DisplayWindows WindowObj)
        {
            try
            {
                DisplayWindows DispWindows = new DisplayWindows();
                DispWindows.ScrollTime = new TimeSpan(0, 0, WindowObj.Scroll_Time);
                foreach (var item in WindowObj.DisplayWindows)
                {
                    DisplayWindowItem WinItem = Decode_DisplayWindowItem(item);
                    WinItem = (DisplayWindowItem)WinItem.Clone();
                    DispWindows.Windows.Add(WinItem);
                }
                return DispWindows;
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DisplayWindowItem Decode_DisplayWindowItem(St_WindowItem WindowItemObj)
        {
            try
            {
                ///Assign Windows Items
                StOBISCode Index = StOBISCode.FindByOBISCode(WindowItemObj.OBISCode);
                if (Index.OBISIndex == Get_Index.Dummy)
                { 
                    throw new Exception(String.Format("Error occurred while Decode Display Window,{0} OBIS not exists",
                        SmartEyeControl_7.Common.App_Common.ArrayToHexString(WindowItemObj.OBISCode)));
                }
                DisplayWindowItem Item = SelectedWindows.Find((x) => x.Obis_Index == Index.OBISIndex);
                if (Item == null)
                    throw new Exception("Unable to find Window for Quantity " + Index);
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
