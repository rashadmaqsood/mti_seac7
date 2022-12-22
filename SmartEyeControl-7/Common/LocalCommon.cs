using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DLMS;
using System.Threading;
using System.Linq;
using SharedCode.Common;
using System.Reflection;
using System.Data;
using ClosedXML.Excel;
using SharedCode.Comm.HelperClasses;
using OptocomSoftware.Properties;

namespace SEAC.Common
{
    public static class LocalCommon
    {
        public static readonly string DateTimeFormat = "dd/MM/yyyy HH:mm:ss"; 
        private static ConcurrentDictionary<string, object> singletonInstaces = null;
        internal static Color ValidatedColorScheme = Color.Black;
        internal static Color InValidatedColorScheme = Color.Red;
        public static Color BgColor1 = Color.CadetBlue;
        public static Color BgColor2 = Color.LightBlue;
        public static float BgColorAngle = 270f;

        public static FileStream fs = null;
        public static StreamWriter wr = null;
        public static BufferedStream _ConsoleStream = null;
        public static readonly int ExceptionMaxLevel = 50;
        public static readonly string LogSourceName = "MySource";

        static LocalCommon()
        {
            lock (Application.CurrentCulture)
            {
                if (singletonInstaces == null)
                    singletonInstaces = new ConcurrentDictionary<string, object>();
            }
        }

        #region Default_Application_Directory

        public static void SaveApplicationException(Exception ex, int curLevel = 5)
        {
            try
            {
                string fileUrl = String.Format(@"{0}\Exceptions\Exception_{1}.txt", GetApplicationErrorDirectory(), DateTime.Now.ToFileTime());
                SaveException(ex, fileUrl);
            }
            catch (Exception) { }
        }

        public static string GetApplicationConfigsDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];
                if (!Directory.Exists(fileDirectoryPath))
                {
                    fileDirectoryPath = Directory.GetCurrentDirectory() + @"\Application_Configs"; //Environment.CurrentDirectory + @"\Application_Configs";
                    if (!Directory.Exists(fileDirectoryPath))
                        Directory.CreateDirectory(fileDirectoryPath);
                    Settings.Default["ApplicationConfigsDirectory"] = fileDirectoryPath;
                    Settings.Default.Save();
                }
                return fileDirectoryPath;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static string GetApplicationLogsDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];

                fileDirectoryPath = Directory.GetCurrentDirectory() + @"\Application_IOLogs";// Environment.CurrentDirectory + @"\Application_IOLogs";
                if (!Directory.Exists(fileDirectoryPath))
                    Directory.CreateDirectory(fileDirectoryPath);

                return fileDirectoryPath;
            }
            catch (Exception ex)
            {
                return Environment.CurrentDirectory;
            }
        }

        public static string GetApplicationErrorDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];

                fileDirectoryPath = Directory.GetCurrentDirectory() + @"\Application_Errors";// Environment.CurrentDirectory + @"\Application_Errors";
                if (!Directory.Exists(fileDirectoryPath))
                    Directory.CreateDirectory(fileDirectoryPath);

                return fileDirectoryPath;
            }
            catch (Exception ex)
            {
                return Environment.CurrentDirectory;
            }
        }

        public static AppDomain GetCurrentAppDomain()
        {
            AppDomain domain = null;
            try
            {
                ///Load Current Application Domain
                AppDomain root = AppDomain.CurrentDomain;
                AppDomainSetup setup = new AppDomainSetup();
                domain = AppDomain.CreateDomain(GetApplicationConfigsDirectory(), null, setup);
            }
            catch
            {
                domain = AppDomain.CurrentDomain;
            }
            return domain;
        }
        public static String GetAccessRightsFilePath(string associationName)
        {
            try
            {
                if (String.IsNullOrEmpty(associationName))
                    throw new Exception("Invalid Association Name");
                String fileName = String.Format(@"{0}\{1}.xml", GetApplicationConfigsDirectory(), associationName);
                return fileName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Byte Conversions

        public static ushort ByteToShort(byte lowByte, byte highByte)
        {
            ushort low = (ushort)lowByte;
            ushort high = (ushort)(((ushort)highByte) << 8);
            return (ushort)(low | high);
        }

        #endregion

        /// <summary>
        /// Converts byte array to string in hex format
        /// </summary>
        /// <param name="Hex_Array"></param>
        /// <returns></returns>
        static public string ArrayToHexString(byte[] Hex_Array, bool appendSplitLine = false, int ColumnSize = 16)
        {
            string Return_String = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            int columnSizeCounter = 1;

            if (Hex_Array != null)
            {
                foreach (byte Value in Hex_Array)
                {
                    stringBuilder.AppendFormat("{0:X2} ", Value);

                    if (appendSplitLine && (columnSizeCounter % ColumnSize) == 0)
                        stringBuilder.Append("\r\n");
                    else if (appendSplitLine && (columnSizeCounter % 8) == 0)
                        stringBuilder.Append("\t");

                    columnSizeCounter++;
                }

                Return_String = stringBuilder.ToString();
                return Return_String;
            }
            else return string.Empty;
        }

        public static ReportFormat GetReportFormat (ApplicationRight _currentAccessRights)
        {
                try
                {
                    var _OtherRights = _currentAccessRights.OtherRights.Find((x) => x.QuantityName.Contains(OtherRights.IsWebFormat.ToString()));

                    if (!_OtherRights.Read)
                    {
                        return ReportFormat.WAPDA_DDS;
                    }
                    else if (_OtherRights.Read && _OtherRights.Write)
                    {
                        return ReportFormat.ADVANCE_MTI;
                    }
                    else
                    {
                        return ReportFormat.WEB_GALAXY;
                    }
                    //IsWebFormat = (_OtherRights != null && _OtherRights.Read) ? true : false;
                }
                catch (Exception ex)
                {
                return ReportFormat.WAPDA_DDS;
            }
        }

        public static string Byte_Arrayto_PlainString(byte[] hexValues)
        {
            //string hexValues = "48 65 6C 6C 6F 20 57 6F 72 6C 64 21";
            //StringBuilder temp = new StringBuilder();
            //string[] hexValuesSplit = hexValues.Split(' ');
            //byte[] stringval = new byte[hexValuesSplit.Length];
            //int i = 0;
            //foreach (String hex in hexValuesSplit)
            //{
            //    if (hex.Length == 2)
            //    {
            //        // Convert the number expressed in base-16 to an integer. 
            //        int value = Convert.ToInt32(hex, 16);
            //        // Get the character corresponding to the integral value. 
            //        string stringValue = Char.ConvertFromUtf32(value);
            //        char charValue = (char)value;
            //        stringval[i++] = (byte)value;
            //        temp.Append(charValue);
            //    }
            //    //Console.WriteLine("hexadecimal value = {0}, int value = {1}, char value = {2} or {3}",
            //      //                  hex, value, stringValue, charValue);
            //}
            string st = System.Text.Encoding.UTF8.GetString(hexValues).Replace("\n", "\r\n");
            return st;// temp.ToString();// temp.ToString();
            //StringBuilder temp = new StringBuilder();
            //for (int i = 0; i < hexString.Length; i += 2)
            //{
            //    string hs = hexString.Substring(i, 2);
            //    UInt32 x = Convert.ToUInt32(hs, 16);
            //    temp.Append(BitConverter.ToChar(BitConverter.GetBytes(x), 0));
            //}
            //return temp.ToString();
        }
        public static byte[] GetRandomOctectString(int DataStringLength = 08)
        {
            Random random = new Random((int)DateTime.Now.Ticks);


            byte[] Challenge_STR = new byte[DataStringLength];

            for (int index = 0; index < Challenge_STR.Length; index++)
            {
                Challenge_STR[index] = Convert.ToByte(random.Next(0, 255));
            }


            return Challenge_STR;
        }

        public static IEnumerable<uint> Series(uint min, uint max)
        {
            uint k = min;
            while (k <= max)
            {
                yield return k;
                k = k + 1;
            }
        }

        static public bool Init_List(ListBox ls_box, List<string> ls_Items)
        {
            if (ls_Items == null)
            {
                return false;
            }

            foreach (string item in ls_Items)
                ls_box.Items.Add(item);
            return true;
        }

        static public void TextCopy2Clipboard(TextBox txtSource)
        {
            if (txtSource.Text.Length == 0)
            {
                TextBox a = new TextBox();
                a.Text = " ";
                a.Select(0, 1);
                a.Copy();
                return;
            }

            txtSource.Select(0, txtSource.Text.Length);
            txtSource.Copy();

        }

        static public UInt16 IndexofMainNode(TreeNode TN)
        {
            TreeNode parent = TN.Parent;
            UInt16 Index_main_node = (UInt16)TN.Index;

            for (; parent != null; )
            {
                TN = parent;
                Index_main_node = (UInt16)TN.Index;
                parent = TN.Parent;
            }
            return Index_main_node;
        }

        public static byte[] GetHash64(string str)
        {
            var PhraseAsByte = ASCIIEncoding.UTF8.GetBytes(str);
            Byte[] EncryptedBytes;
            byte[] EncryptedBytes16 = new byte[24];
            SHA512Managed hash = null;

            for (int i = 0; i < EncryptedBytes16.Length; i++)
            {
                EncryptedBytes16[i] = 0;
            }

            try
            {
                hash = new SHA512Managed();
                EncryptedBytes = hash.ComputeHash(PhraseAsByte);

                for (int i = 0; i < 4; i++)
                    for (int j = (i * 16); j < (i * 16) + 16; j++)
                        EncryptedBytes16[j - (i * 16)] ^= EncryptedBytes[j];
            }
            finally
            {
                hash.Clear();

            }
            return EncryptedBytes16;
        }

        /// <summary>
        /// Returns index of selected sub-node
        /// </summary>
        /// <param name="TV"></param>
        /// <returns></returns>
        static public UInt16 FindSelectedChildNode(TreeNode TN)
        {
            UInt16 Index = 0xffff;

            TreeNodeCollection child_nodes = TN.Nodes;

            if (child_nodes.Count == 0)
                Index = (UInt16)TN.Index;

            return Index;
        }

        public static string FindMeterModelByMeterSAP(string  deviceAddressFromMeter)
        {
            try
            {
                ///SAP Name Details
                ///MTI0R32600000786
                ///MTI0S32800000786
                if (String.IsNullOrEmpty(deviceAddressFromMeter))
                    throw new Exception(String.Format("Invalid Server SAP Name {0}", deviceAddressFromMeter));
                if (deviceAddressFromMeter.Contains("R421"))
                    return "R421";
                if (deviceAddressFromMeter.Contains("R326"))
                    return "R326";
                if (deviceAddressFromMeter.Contains("T421"))
                    return "T421";
                //if (deviceAddressFromMeter.Contains("R421") || deviceAddressFromMeter.Contains("R411"))
                //    return ServerSAP.SAP_Name.Substring(4, 4);
                else //Modified by Azeem for Accurate
                {
                    if (deviceAddressFromMeter.Contains("MTI"))
                        return deviceAddressFromMeter.Substring(deviceAddressFromMeter.IndexOf("MTI")+4, 4);
                    else if (deviceAddressFromMeter.Contains("ACC"))
                        return deviceAddressFromMeter.Substring(deviceAddressFromMeter.IndexOf("ACC") + 4, 6);
                    else
                        return deviceAddressFromMeter.Substring(5, 4);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string FindMeterModelByMeterSAP(SAP_Object ServerSAP)
        {
            try
            {
                ///SAP Name Details
                ///MTI0R32600000786
                ///MTI0S32800000786
                if (String.IsNullOrEmpty(ServerSAP.SAP_Name) || ServerSAP.SAP_Name.Length != 16)
                    throw new Exception(String.Format("Invalid Server SAP Name {0}", ServerSAP.SAP_Name));
                if (ServerSAP.SAP_Name.Contains("R421"))
                    return "R421";
                if (ServerSAP.SAP_Name.Contains("R326"))
                    return "R326";
                if (ServerSAP.SAP_Name.Contains("T421"))
                    return "T421";
                if (ServerSAP.SAP_Name.Contains("R421") || ServerSAP.SAP_Name.Contains("R411"))
                    return ServerSAP.SAP_Name.Substring(4, 4);
                else //Modified by Azeem for Accurate
                {
                    if (ServerSAP.SAP_Name.Contains("MTI"))
                        return ServerSAP.SAP_Name.Substring(3, 4);
                    else if (ServerSAP.SAP_Name.Contains("ACC"))
                        return ServerSAP.SAP_Name.Substring(3, 6);
                    else
                        return ServerSAP.SAP_Name.Substring(3, 4);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool TextBox_validation(double min, double max, TextBox given_TextBox)
        {
            try
            {

                double text_box_value = Convert.ToDouble(given_TextBox.Text);
                if (text_box_value > max || text_box_value < min || given_TextBox.Text == "")
                {
                    given_TextBox.ForeColor = Color.Red;
                    return false;
                }

                else
                {
                    given_TextBox.ForeColor = Color.Black;
                    return true;
                }
            }
            catch
            {
                given_TextBox.ForeColor = Color.Red;
                return false;
            }
            //       return true;
        }

        public static bool isValidStringNumbers( int max_digits, TextBox given_TextBox)
        {
            try
            {
                string text_box_value = given_TextBox.Text;

                if(text_box_value.Length == max_digits)
                {
                    bool isValid = long.TryParse(text_box_value, out long n);
                
                    if(isValid)
                    {
                        given_TextBox.ForeColor = Color.Black;
                        return true;
                    }
                }
                //else// (text_box_value.Length < max_digits || given_TextBox.Text == "")
                //{
                    given_TextBox.ForeColor = Color.Red;
                    return false;
                //}
            }
            catch
            {
                given_TextBox.ForeColor = Color.Red;
                return false;
            }
            //       return true;
        }

        public static bool isValidDisplayableString(TextBox given_TextBox)
        {
            try
            {
                string text_box_value = given_TextBox.Text;

                for(int i=0; i < text_box_value.Length; i++)
                {
                    if( char.IsControl( text_box_value[i]) )
                    {
                        given_TextBox.ForeColor = Color.Red;
                        return false;
                    }
                }
                
                given_TextBox.ForeColor = Color.Black;
                return true;
                    
            }
            catch
            {
                given_TextBox.ForeColor = Color.Red;
                return false;
            }
            //       return true;
        }

        /// <summary>
        /// Check that, Every Character is in HEX i.e., [ 0-9 or A-F ]
        /// </summary>
        /// <param name="given_TextBox"> Text Field to check HEX </param>
        /// <returns>Return True,  if every Character is between [ 0-9 or A-F ]. Else, Return False </returns>
        public static bool isValidHexString(TextBox given_TextBox)
        {
            try
            {
                string text_box_value = given_TextBox.Text;

                bool isOkay = System.Text.RegularExpressions.Regex.IsMatch(text_box_value, @"\A\b[0-9a-fA-F]+\b\Z");
                
                if ( !isOkay )
                {
                    given_TextBox.ForeColor = Color.Red;
                    return false;
                }
                
                given_TextBox.ForeColor = Color.Black;
                return true;
                    
            }
            catch
            {
                given_TextBox.ForeColor = Color.Red;
                return false;
            }
        }


        public static bool ValidateIPv4(TextBox given_TextBox)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(given_TextBox.Text))
                {
                    given_TextBox.ForeColor = Color.Red;
                    return false;
                }

                string[] splitValues = given_TextBox.Text.Split('.');
                if (splitValues.Length != 4)
                {
                    given_TextBox.ForeColor = Color.Red;
                    return false;
                }

                byte tempForParsing;

                bool result = splitValues.All(r => byte.TryParse(r, out tempForParsing));
                if(result)
                {
                    given_TextBox.ForeColor = Color.Black;
                    return true;
                }
                else
                {
                    given_TextBox.ForeColor = Color.Red;
                    return false;
                }
            }
            catch
            {
                given_TextBox.ForeColor = Color.Red;
                return false;
            }
        }

        public static byte DecimalPoint_validation(TextBox given_TextBox, int left, int right, int total)
        {
            string text = String.Empty;
            try
            {

                text = given_TextBox.Text;
                //verify that the text entered is Numeric
                //*********************************************
                //*********************************************

                try
                {
                    double val = Convert.ToDouble(text);
                }
                catch
                {
                    given_TextBox.ForeColor = InValidatedColorScheme;
                    return 0;
                }

                //*********************************************
                //*********************************************
                //*********************************************

                int before_decimal;
                int after_decimal;
                before_decimal = text.IndexOf('.');
                if (before_decimal == -1)
                {
                    before_decimal = 0;
                    after_decimal = 0;
                    return 0;
                }
                else
                {
                    after_decimal = text.Length - before_decimal - 1;
                    if (before_decimal > 16 || after_decimal > 16 || given_TextBox.Text == "" ||
                        after_decimal > right || before_decimal > left || (after_decimal + before_decimal) > total)
                    {
                        given_TextBox.ForeColor = InValidatedColorScheme;
                        return 0;
                    }
                    given_TextBox.ForeColor = ValidatedColorScheme;
                    return (byte)(before_decimal * 16 + after_decimal);
                }
            }
            catch
            {
                given_TextBox.ForeColor = InValidatedColorScheme;
                return 0;
            }

        }

        public static string DecimalPoint_toGUI(byte value)
        {
            string temp;
            int upper;
            int lower;
            upper = value / 16;
            lower = value % 16;
            temp = "";
            temp = temp.PadLeft(upper, '0') + "." + temp.PadRight(lower, '0');
            //temp =;
            return temp;
        }

        /// <summary>
        /// Removes blank spaces and new line chars from the string
        /// </summary>
        /// <param name="passed"></param>
        /// <returns></returns>
        private static string Remove_White_Spaces(string passed)
        {

            passed = passed.Replace(" ", "");
            passed = passed.Replace("\x0d", "");
            passed = passed.Replace("\x0a", "");

            return passed;
        }

        //==========================================================================
        //==========================================================================
        /// <summary>
        /// Converts byte array to string in hex format
        /// </summary>
        /// <param name="Hex_Array"></param>
        /// <returns></returns>
        static public string ArrayToHexString(byte[] Hex_Array)
        {
            string Return_String = "";
            foreach (byte Value in Hex_Array) Return_String += (Value.ToString("X2") + " ");
            return Return_String;
        }
        static public string ArrayToHexString(byte[] Hex_Array, bool h)
        {
            string Return_String = "";
            foreach (byte Value in Hex_Array) Return_String += (Value.ToString("X2"));
            return Return_String;
        }

        //==========================================================================
        //==========================================================================
        public static byte[] String_to_Hex_array(string passed)
        {
            passed = Remove_White_Spaces(passed);

            passed = passed.ToUpper();

            char[] temp = passed.ToCharArray();

            byte[] to_return = new byte[(temp.Length) / 2];

            for (byte i = 0; i < to_return.Length; i++)
            {
                if (temp[2 * i] >= '0' && temp[2 * i] <= '9') to_return[i] |= (byte)((temp[2 * i] - 0x30) << 4);
                else if (temp[2 * i] >= 'A' && temp[2 * i] <= 'F') to_return[i] |= (byte)((temp[2 * i] - 0x37) << 4);

                if (temp[(2 * i) + 1] >= '0' && temp[(2 * i) + 1] <= '9') to_return[i] |= (byte)(temp[(2 * i) + 1] - 0x30);
                else if (temp[(2 * i) + 1] >= 'A' && temp[(2 * i) + 1] <= 'F') to_return[i] |= (byte)(temp[(2 * i) + 1] - 0x37);
            }


            return to_return;
        }
        //==========================================================================
        #region Array Functions


        static public byte[] Append_to_End(byte[] First_Array, byte[] Second_Array)
        {
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Appends second_byt at the end of first array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="second_byte"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte[] First_Array, byte second_byte)
        {
            byte[] Second_Array = new byte[1] { second_byte };
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Appends second_long at the end of First_Array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="second_long"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte[] First_Array, ulong second_long)
        {
            byte[] Second_Array = new byte[4] 
            { (byte)((ulong)second_long >> 24 & 0xff), (byte)((ulong)second_long >> 16 & 0xff),  
             (byte)((ulong)second_long>>8 & 0xff), (byte)((ulong)second_long & 0xff) };
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Takes two bytes and appends them in a byte array
        /// </summary>
        /// <param name="First_Byte"></param>
        /// <param name="second_byte"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte First_Byte, byte second_byte)
        {
            byte[] First_Array = new byte[1] { First_Byte };
            byte[] Second_Array = new byte[1] { second_byte };
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Appends second_short at the end of First_Array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="second_short"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte[] First_Array, UInt16 second_short)
        {
            byte[] Second_Array = new byte[2] { (byte)((short)second_short >> 8 & 0xff), (byte)((short)second_short & 0xff) };

            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }


        //==========================================================================
        /// <summary>
        /// Appends second array at the start of first array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="Second_Array"></param>
        /// <returns></returns>
        static public byte[] Append_to_Start(byte[] First_Array, byte[] Second_Array)
        {
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        static public byte[] Append_to_Start(byte[] First_Array, byte second_byte)
        {
            byte[] Second_Array = new byte[1] { second_byte };
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        static public byte[] Append_to_Start(byte[] First_Array, UInt16 second_short)
        {
            byte[] Second_Array = new byte[2] { (byte)((short)second_short >> 8 & 0xff), (byte)((short)second_short & 0xff) };

            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

            return Appended;
        }

        public static void Append_to_Start(ref byte[] First_Array, ref int offSet_Dest, ref int size_Dest, byte[] SecondArray, int offSet_Src, int size_Src)
        {
            try
            {
                if (First_Array.Length >= (offSet_Dest + size_Dest + size_Src))
                {
                    ///Append room on Start
                    if (offSet_Dest >= size_Src)
                    {
                        Buffer.BlockCopy(SecondArray, offSet_Src, First_Array, (offSet_Dest - size_Src), size_Src);
                        offSet_Dest = (offSet_Dest - size_Src);
                    }
                    else
                    {
                        ///Shift Elements
                        for (int index = offSet_Dest + size_Dest; index >= 0 && index >= offSet_Dest; index--)
                            First_Array[index + size_Src] = First_Array[index];
                        ///Copy Second Array
                        Buffer.BlockCopy(SecondArray, offSet_Src, First_Array, offSet_Dest, size_Src);
                    }
                    size_Dest = size_Dest + size_Src;
                }
                else
                {
                    byte[] tLargeBuf = new byte[size_Dest + size_Src];
                    Buffer.BlockCopy(tLargeBuf, 0, SecondArray, 0, size_Src);
                    Buffer.BlockCopy(tLargeBuf, size_Src, First_Array, offSet_Dest, size_Dest);
                    First_Array = tLargeBuf;
                    offSet_Dest = 0;
                    size_Dest = tLargeBuf.Length;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while coping data on destination", ex);
            }
        }

        #endregion

        public static App_Validation_Info AppValidationInfo
        {
            get
            {
                App_Validation_Info validationInfo = null;
                try
                {

                    validationInfo = (App_Validation_Info)Get_CurrentInstance("App_Validation_Info_");
                    if (validationInfo == null)
                    {
                        ///Should be init on Application_Startup
                        ///default Constructor Object
                        validationInfo = new App_Validation_Info();
                        Set_CurrentInstance("App_Validation_Info_", validationInfo);
                    }
                    return validationInfo;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while taking Object From Dictionary", ex);
                }
            }
            internal set
            {
                App_Validation_Info validationInfo = null;
                if (value == null)
                    validationInfo = new App_Validation_Info();
                else
                    validationInfo = value;
                Set_CurrentInstance("App_Validation_Info_", validationInfo);
            }
        }

        #region Get_CurrentInstance

        public static object Get_CurrentInstance(string keyBase)
        {
            try
            {
                string key = Get_CurrentKey(keyBase);
                object obj_val = null;
                int maxTryCount = 10;
                if (singletonInstaces.ContainsKey(key))
                    while (maxTryCount > 0)
                    {
                        if (singletonInstaces.TryGetValue(key, out obj_val))
                            break;
                        maxTryCount--;
                    }
                if (maxTryCount <= 0)
                    throw new Exception("maxTryCount <= 0");
                return obj_val;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while taking object from Singleton Cache", ex);
            }
        }

        public static object Set_CurrentInstance(string keyBase, Object currentInstance)
        {
            try
            {
                string key = Get_CurrentKey(keyBase);
                object obj_val = null;
                obj_val = singletonInstaces.AddOrUpdate(key, currentInstance, (_key, oldValue) => oldValue = currentInstance);
                return obj_val;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while taking object from Singleton Cache", ex);
            }
        }

        public static string Get_CurrentKey(string keyBase)
        {
            try
            {
                Process currentProcess = Process.GetCurrentProcess();
                String current_key = String.Format("{0}_{1:D5}", keyBase, currentProcess.Id);
                return current_key;
            }
            catch
            {
            }
            return keyBase;
        }

        #endregion

        public static ushort Calculate_PPM(double Xtal_feq, ref bool isNegative)
        {
            App_Validation_Info _ValidationInfo = AppValidationInfo;
            ushort ppm_calc = 0;
            try
            {
                if (!(Xtal_feq < _ValidationInfo.Xtal_freq_max && Xtal_feq > _ValidationInfo.Xtal_freq_min))
                    throw new ArgumentOutOfRangeException("Xtal_feq");
                ///formula to convert XTAL into PPM
                double ppm = ((Xtal_feq - App_Validation_Info.Xtal_freq_Const) / App_Validation_Info.Xtal_freq_Const) * 1000000 * 10;
                isNegative = (ppm < 0);
                ppm_calc = Convert.ToUInt16(Math.Abs(ppm));
            }
            catch (Exception ex)
            {
                throw new OverflowException("PPM_Max", ex);
            }
            return ppm_calc;
        }

        public static double Calculate_XTALFrequency(ushort ppm_, bool isNegative)
        {
            App_Validation_Info _ValidationInfo = AppValidationInfo;
            double Xtal_freq = 0;
            double ppm_Calc = 0;
            try
            {
                if (isNegative)
                    ppm_Calc = ppm_ * -1;

                Xtal_freq = (((ppm_Calc * App_Validation_Info.Xtal_freq_Const) / 1000000 / 10) + App_Validation_Info.Xtal_freq_Const);

                if (!(Xtal_freq < _ValidationInfo.Xtal_freq_max && Xtal_freq > _ValidationInfo.Xtal_freq_min))
                    throw new ArgumentOutOfRangeException("Xtal_feq");
            }
            catch (Exception ex)
            {
                throw new OverflowException("ppm_", ex);
            }
            return Xtal_freq;
        }

        //Added By Azeem
        public static string value_to_string(double value, string Format)
        {
            if (double.IsNaN(value))
                return "Not Access";
            else if (double.IsPositiveInfinity(value))
                return "Not Exist";
            else if (double.IsNegativeInfinity(value))
                return "Meter BUG";
            else return value.ToString(Format); //Format Should be f0, f1, f2, f3 and so on
        }


        public static string value_to_string(double value, bool Format)
        {
            if (double.IsNaN(value))
                return "Not Access";
            else if (double.IsPositiveInfinity(value))
                return "Not Exist";
            else if (double.IsNegativeInfinity(value))
                return "Meter BUG";
            else return value.ToString("f3");
        }

        public static string value_to_string(double value)
        {
            if (double.IsNaN(value))
                return "Not Access";
            else if (double.IsPositiveInfinity(value))
                return "Not Exist";
            else if (double.IsNegativeInfinity(value))
                return "Meter BUG";
            else return value.ToString("");
        }
        public static void value_to_string(double value, ref TextBox t)
        {
            if (double.IsNaN(value))
                t.Text = "Not Access";
            else if (double.IsPositiveInfinity(value))
                t.Text = "Not Exist";
            else if (double.IsNegativeInfinity(value))
                t.Text = "Meter BUG";
            else t.Text = value.ToString();
        }
        public static void value_to_string(double value, ref Label L)
        {
            if (double.IsNaN(value))
                L.Text = "Not Access";
            else if (double.IsPositiveInfinity(value))
                L.Text = "Not Exist";
            else if (double.IsNegativeInfinity(value))
                L.Text = "BUG";
            else L.Text = value.ToString();
            Application.DoEvents();
        }

        public static uint IPAddressToLong(System.Net.IPAddress address)
        {
            byte[] byteIP = address.GetAddressBytes();

            uint ip = (uint)byteIP[0] << 24;
            ip += (uint)byteIP[1] << 16;
            ip += (uint)byteIP[2] << 8;
            ip += (uint)byteIP[3];
            return ip;
        }

        public static string LongToIPAddressString(uint IPAddress)
        {
            byte[] ip = new byte[4];
            ip[3] = (byte)(IPAddress & 0x000000ff);
            ip[2] = (byte)((IPAddress & 0x0000ff00) / 256);
            ip[1] = (byte)((IPAddress & 0x00ff0000) / 256 / 256);
            ip[0] = (byte)((IPAddress & 0xff000000) / 256 / 256 / 256);
            string IP = ip[0].ToString() + ".";

            IP += ip[1].ToString() + ".";
            IP += ip[2].ToString() + ".";
            IP += ip[3];
            return IP;
        }

        public static string notRoundingOff(string value, int decimalPlaces)
        {
            string preDecimal = "";
            string postDecimal = "";
            if (decimalPlaces == 0) //Added For report: Eliminating decimal points
            {
                if (value.Contains("."))
                {
                    value = value.Substring(0, value.IndexOf("."));
                }
                else
                { 
                }

                //int index = value.IndexOf(".") - 1;
                //if (index <= 0)
                //{
                //    value = value.Substring(0, 1);
                //}
                //else
                //{
                //    value = value.Substring(0, value.IndexOf(".") - 1);
                //}
            }
            else
            {
                //decimalPlaces++;    //v4.8.25 commented
                if (value.Contains("."))
                {
                    decimalPlaces++;    //v4.8.25
                    preDecimal = value.Substring(0, value.IndexOf("."));
                    int index = value.IndexOf(".");
                    postDecimal = value.Substring((index + 1));
                    if (postDecimal.Length < decimalPlaces)
                    {
                        int zeroesToAppend = decimalPlaces - postDecimal.Length;
                        for (int i = 0; i < zeroesToAppend - 1; i++)
                        {
                            postDecimal += "0";
                        }
                        // value = value.Substring(0, value.IndexOf(".") + decimalPlaces);
                        value = preDecimal + "." + postDecimal;
                    }
                    else
                    {
                        value = value.Substring(0, value.IndexOf(".") + decimalPlaces);
                    }
                }
                else
                {
                    string s = ".";
                    for (int i = 0; i < decimalPlaces; i++)
                    {
                        s += "0";
                    }
                    value += s;
                }
            } 

            return value;
        }

        public static string ConvertToValidString(byte[] array)
        {
            try
            {
                string _toReturn = "";

                byte current_Char = 0;
                if (array != null)
                {
                    current_Char = array[0];
                    //Process First Char
                    //if (current_Char >= 0 && current_Char <= 9)
                    //    _toReturn += current_Char.ToString();
                    //else
                    //    _toReturn += Char.ConvertFromUtf32(current_Char + 48);

                    for (int i = 0; i < array.Length; i++)
                    {
                        current_Char = array[i];
                        if (current_Char < (byte)10 && current_Char >= (byte)0)
                        {
                            _toReturn += current_Char.ToString();
                        }
                        else
                            //_toReturn += Char.ConvertFromUtf32(current_Char + 48);
                            _toReturn += Char.ConvertFromUtf32(current_Char);
                    }

                }
                if (!String.IsNullOrEmpty(_toReturn))
                    _toReturn = _toReturn.TrimEnd(("? " + Convert.ToChar(0x0F)).ToCharArray());
                return _toReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static byte[] ConvertFromValidNumberString(String phoneTxt)
        {
            try
            {
                char AppendChar = '?';//0x3F //Char.ConvertFromUtf32(63).ToCharArray()[0];
                bool isAlphaNumeric = false;
                phoneTxt = phoneTxt.TrimEnd("+-".ToCharArray());
                foreach (var char_Val in phoneTxt)
                {
                    if (!char.IsDigit(char_Val))
                    {
                        isAlphaNumeric = true;
                        break;
                    }
                }
                if (isAlphaNumeric)
                    AppendChar = Convert.ToChar(0x0F);
                else
                    AppendChar = '?';
                phoneTxt = phoneTxt.PadRight(16, AppendChar);

                //byte[] byte_array = new byte[16] { 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63 };
                byte[] _tArray = Encoding.ASCII.GetBytes(phoneTxt);
                if (!isAlphaNumeric)
                {
                    ///byte_array = DLMS_Common.Append_to_End(_tArray, byte_array);
                    for (int i = 0; i < _tArray.Length; i++)
                    {
                        _tArray[i] = (byte)(_tArray[i] - 48);
                    }
                }
                Array.Resize(ref _tArray, 16);
                return _tArray;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IList ConvertArray(object[] firstArray, Type targetType)
        {
            Array array = Array.CreateInstance(targetType, firstArray.Length);
            for (int i = 0; i < firstArray.Length; i++)
            {
                array.SetValue(Convert.ChangeType(firstArray[i], targetType), i);
            }
            return array;
        }

        public static bool IsThreadRunning(Thread ThRunner)
        {
            bool IsRunning = false;
            try
            {
                if (ThRunner != null)
                {
                    #region Test Allocater Thread Status

                    if (ThRunner.ThreadState == System.Threading.ThreadState.Unstarted ||
                                        ThRunner.ThreadState == System.Threading.ThreadState.StopRequested ||
                                       ThRunner.ThreadState == System.Threading.ThreadState.Stopped ||
                                       ThRunner.ThreadState == System.Threading.ThreadState.SuspendRequested ||
                                        ThRunner.ThreadState == System.Threading.ThreadState.Suspended ||
                                        ThRunner.ThreadState == System.Threading.ThreadState.AbortRequested ||
                                        ThRunner.ThreadState == System.Threading.ThreadState.Aborted)
                        IsRunning = false;
                    else if (ThRunner.IsAlive)
                        IsRunning = true;

                    #endregion
                }
            }
            catch
            { }
            return IsRunning;
        }

        #region Process_Info

        public static int Processor_CoreCount
        {
            get
            {
                int coreCount = 0;
                try
                {

                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
                    {
                        coreCount += int.Parse(item["NumberOfCores"].ToString());
                    }
                }
                catch
                {
                    coreCount = -1;
                }
                return coreCount;
            }
        }

        public static int ProcessorCount
        {
            get
            {
                int processorCount = 0;
                try
                {
                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                    {
                        processorCount += int.Parse(item["NumberOfProcessors"].ToString());
                    }

                }
                catch
                {
                    processorCount = -1;
                }
                return processorCount;
            }
        }

        public static int CPU_LogicalCoreCount
        {
            get
            {
                int coreCount = 0;
                try
                {

                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                    {
                        coreCount += int.Parse(item["NumberOfLogicalProcessors"].ToString());
                    }
                }
                catch
                {
                    coreCount = -1;
                }
                return coreCount;
            }
        }

        public static float TotalPhysicalMemory_GigaByte
        {
            get
            {
                float GigaByteMemory = 0.0f;
                long memoryBytes = 0;
                try
                {
                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                    {
                        memoryBytes += long.Parse(item["TotalPhysicalMemory"].ToString());
                    }
                    GigaByteMemory = memoryBytes / Commons.GigaByteDivider;
                }
                catch
                {
                    memoryBytes = 0;
                }
                return GigaByteMemory;
            }
        }

        public static int Processor_LogicalCoreCount
        {
            get
            {
                try
                {
                    return Environment.ProcessorCount;
                }
                catch
                { }
                return -1;
            }
        }

        public static float WorkingSetGBytes
        {
            get
            {
                float WorkingSetInMBytes = -1.0f;
                try
                {
                    WorkingSetInMBytes = Environment.WorkingSet / (Commons.GigaByteDivider);
                }
                catch
                {
                    WorkingSetInMBytes = -1.0f;
                }
                return WorkingSetInMBytes;
            }
        }

        public static long ActiveThreadCount
        {
            get
            {
                try
                {
                    return ((IEnumerable)System.Diagnostics.Process.GetCurrentProcess().Threads)
           .OfType<System.Diagnostics.ProcessThread>()
           .Where(t => t.ThreadState == System.Diagnostics.ThreadState.Running)
           .Count();
                }
                catch { }
                return -1;
            }
        }

        public static long ThreadCount
        {
            get
            {
                try
                {
                    return System.Diagnostics.Process.GetCurrentProcess().Threads.Count;
                }
                catch { }
                return 0;
            }
        }

        public static float WorkingSet64GBytes
        {
            get
            {
                float WorkingSetInMBytes = -1.0f;
                try
                {
                    WorkingSetInMBytes = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (Commons.GigaByteDivider);
                }
                catch
                {
                    WorkingSetInMBytes = -1.0f;
                }
                return WorkingSetInMBytes;
            }
        }

        public static float PrivateByte64GBytes
        {
            get
            {
                float PrivateBytetInMBytes = -1.0f;
                try
                {
                    PrivateBytetInMBytes = System.Diagnostics.Process.GetCurrentProcess().PrivateMemorySize64 / (Commons.GigaByteDivider);
                }
                catch
                {
                    PrivateBytetInMBytes = -1.0f;
                }
                return PrivateBytetInMBytes;
            }
        }

        public static float VirtualByte64GBytes
        {
            get
            {
                float VirtualSetInMBytes = -1.0f;
                try
                {
                    VirtualSetInMBytes = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64 / (Commons.GigaByteDivider);
                }
                catch
                {
                    VirtualSetInMBytes = -1.0f;
                }
                return VirtualSetInMBytes;
            }
        }

        #endregion

        public static string[] Directory_GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            string[] searchPatterns = searchPattern.Split('|');
            List<string> files = new List<string>();
            foreach (string sp in searchPatterns)
                files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
            files.Sort();
            return files.ToArray();
        }
        public static void SaveException(Exception ex, string fileURL, int curLevel = 14)
        {
            Exception _ex = ex;
            try
            {
                FileInfo FileINfo = new FileInfo(fileURL);
                string fileName = FileINfo.Name;
                string dirName = FileINfo.DirectoryName;

                ///File Lenght Exceeds 20K 
                if (FileINfo.Exists && FileINfo.Length > 20480)
                    BackUpFileSameLoc(fileURL);

                using (StreamWriter wr = ExceptionsInit(dirName, fileName))
                {
                    StringBuilder builder = BuildExceptionLog(ex, curLevel);
                    wr.WriteLine(builder.ToString());
                }
            }
            catch (Exception) { }
        }
        public static void BackUpFileSameLoc(string fileUrl)
        {
            try
            {
                FileInfo FileINfo = new FileInfo(fileUrl);
                if (FileINfo.Exists)
                {
                    String NewFileName = FileINfo.Name.Replace(FileINfo.Extension, "");
                    NewFileName = String.Format("{0}_{1}_{2}", NewFileName, DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                    ///Process New File Name
                    NewFileName = NewFileName.Replace(" ", "");
                    NewFileName = NewFileName.Replace(".", "");
                    NewFileName = NewFileName.Replace("/", "");
                    NewFileName = NewFileName.Replace(":", "");
                    NewFileName = String.Format(@"{0}\{1}{2}", FileINfo.Directory, NewFileName, FileINfo.Extension);
                    File.Move(fileUrl, NewFileName);
                    File.Delete(fileUrl);
                }
            }
            catch
            {
            }
        }
        public static StreamWriter ExceptionsInit(string relativeDirPath, string fileName)
        {
            try
            {
                FileStream fs = null;
                StreamWriter wr = null;

                string dir = relativeDirPath;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string path = dir + String.Format("\\{0}", fileName);
                if (!File.Exists(path))
                    fs = new FileStream(path, FileMode.CreateNew);
                else
                    fs = File.Open(path, FileMode.Append);
                wr = new StreamWriter(fs);
                return wr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static StringBuilder BuildExceptionLog(Exception ex, int curLevel = 14)
        {
            StringBuilder builder = new StringBuilder(512);

            builder.AppendFormat("Unhandled Error{0} {1},details\r\n", DateTime.Now, ex.Message);
            Exception _ex = ex;

            while (_ex != null && curLevel < ExceptionMaxLevel)
            {
                _ex = _ex.InnerException;
                if (_ex == null)
                    break;
                builder.AppendFormat("{0}\r\n", _ex.Message);
                curLevel++;
            }
            if (ex != null)
                builder.AppendLine(ex.StackTrace);
            return builder;
        }

        public class ProcessInfo
        {

            #region Limit Variable

            /// <summary>
            /// Modify Variable To Change Max Limit Variable 
            /// </summary>
            internal static readonly int MaxWorkerThreadPerCore = 250;
            internal static readonly int MaxIOCompThreadPerCore = 1000;
            internal static readonly float MinPrivateByteMemoryRatio = 0.75f;  ///Orignal 0.75f
            internal static readonly float MaxPrivateByteMemoryRatio = 0.50f;  ///Orignal 0.50f
            internal static readonly float MinMemoryLimit = 4.0f;
            internal static readonly float LowMinMomoryLimit = 0.45f;          ///Orignal 0.35
            internal static readonly float MaxToMinThreadRatio = 0.10f;
            public static readonly float MinThreadCountStepDown = 0.25f;
            public static readonly int MaxSampleCount = 10;


            #endregion

            #region Data_Members

            public static readonly int Processor_Count = 1;
            public static readonly int Processor_Core_Count = 1;

            public static readonly float Total_AvailPhysicalMemory = 1.0f;

            public static readonly int MaxWorkerThreadCount = 25;
            public static readonly int DefaultMinWorkerThreadCount = 25;
            public static readonly int FinalMinWorkerThreadCount = 25;
            public static readonly int MaxIOCompelitionThreadCount = 1000;
            public static readonly int DefaultMinIOCompelitionTheadCount = 1000;
            public static readonly int FinalMinIOCompelitionTheadCount = 1000;

            public static readonly float MaxMemoryUsageLimitGB = 1.5f;
            public static readonly float MinMemoryUsageLimitGB = 0.7f;

            internal static readonly object ProcessInfo_MemLock = null;
            #endregion

            #region Instance Data_Members

            private int _CurMinWorkerThreadCount = 25;
            private int _CurMinIOComplitionThreadCount = 1000;
            private int _CurMaxWorkerThreadCount = 25;
            private int _CurMaxIOComplitionThreadCount = 1000;
            private Queue<ProcessInfoSample> ProcessSampleCollected = null;

            #endregion

            #region Constructur

            /// <summary>
            /// Static Constructur To Init Class Variables
            /// </summary>
            static ProcessInfo()
            {
                try
                {
                    ///Init Physical Processor Count
                    Processor_Count = ProcessorCount;
                    ///Init Processor_Core_Count
                    Processor_Core_Count = Processor_CoreCount;
                    ///Init System Available Physical Memory Limit
                    Total_AvailPhysicalMemory = TotalPhysicalMemory_GigaByte;
                    if (Processor_Core_Count > 0)
                    {
                        ///Init Max Worker Thread Counts
                        MaxWorkerThreadCount = Processor_Core_Count * MaxWorkerThreadPerCore;
                        FinalMinWorkerThreadCount = Convert.ToInt32(Math.Round(MaxWorkerThreadCount * MaxToMinThreadRatio));
                        ///Init MaxIOComplitionThreadCount
                        MaxIOCompelitionThreadCount = Processor_Core_Count * MaxIOCompThreadPerCore;
                        FinalMinIOCompelitionTheadCount = Convert.ToInt32(Math.Round(MaxIOCompelitionThreadCount * MaxToMinThreadRatio));
                    }
                    if (Total_AvailPhysicalMemory > 0)
                    {
                        if (Total_AvailPhysicalMemory <= MinMemoryLimit)
                        {
                            MaxMemoryUsageLimitGB = Total_AvailPhysicalMemory * MinPrivateByteMemoryRatio;
                        }
                        else
                        {
                            MaxMemoryUsageLimitGB = Total_AvailPhysicalMemory * MaxPrivateByteMemoryRatio;
                        }
                        MinMemoryUsageLimitGB = Total_AvailPhysicalMemory * LowMinMomoryLimit;
                    }
                    ProcessInfo_MemLock = new object();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occured while initialize ProcessInfo_Common", ex);
                }
            }

            public ProcessInfo()
            {
                try
                {
                    ///Init Instance Variable
                    ProcessSampleCollected = new Queue<ProcessInfoSample>();
                    _CurMaxWorkerThreadCount = MaxWorkerThreadCount;
                    _CurMaxIOComplitionThreadCount = MaxIOCompelitionThreadCount;
                    _CurMinWorkerThreadCount = FinalMinWorkerThreadCount;
                    _CurMinIOComplitionThreadCount = FinalMinIOCompelitionTheadCount;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occured while initialize ProcessInfo_Common", ex);
                }
            }

            #endregion

            #region Property

            public static float WorkingSetGBytes
            {
                get
                {
                    return LocalCommon.WorkingSetGBytes;
                }
            }

            public static long ActiveThreadCount
            {
                get
                {
                    return LocalCommon.ActiveThreadCount;
                }
            }

            public static long ThreadCount
            {
                get
                {
                    return LocalCommon.ThreadCount;
                }
            }

            public static float WorkingSet64GBytes
            {
                get
                {
                    return LocalCommon.WorkingSet64GBytes;
                }
            }

            public static float PrivateByte64GBytes
            {
                get
                {
                    return LocalCommon.PrivateByte64GBytes;
                }
            }

            public static float VirtualByte64GBytes
            {
                get
                {
                    return LocalCommon.VirtualByte64GBytes;
                }
            }

            #endregion

            #region Property_Instance

            public int CurrentMinWorkerThreadCount
            {
                get { return _CurMinWorkerThreadCount; }
                set
                {
                    if (value >= DefaultMinWorkerThreadCount &&
                        value <= FinalMinWorkerThreadCount &&
                        value <= CurrentMaxWorkerThreadCount)
                        _CurMinWorkerThreadCount = value;
                }
            }

            public int CurrentMinIOCompelitionThreadCount
            {
                get { return _CurMinIOComplitionThreadCount; }
                set
                {
                    if (value >= DefaultMinIOCompelitionTheadCount &&
                        value <= FinalMinIOCompelitionTheadCount &&
                        value <= CurrentMaxIOCompelitionThreadCount)
                        _CurMinIOComplitionThreadCount = value;
                }
            }

            public int CurrentMaxWorkerThreadCount
            {
                get { return _CurMaxWorkerThreadCount; }
                set
                {
                    if ((value >= CurrentMinWorkerThreadCount &&
                        value <= MaxWorkerThreadCount) ||
                        value == DefaultMinWorkerThreadCount)
                        _CurMaxWorkerThreadCount = value;
                }
            }

            public int CurrentMaxIOCompelitionThreadCount
            {
                get { return _CurMaxIOComplitionThreadCount; }
                set
                {
                    if ((value >= CurrentMinIOCompelitionThreadCount &&
                        value <= MaxIOCompelitionThreadCount) ||
                        value == DefaultMinIOCompelitionTheadCount)
                        _CurMaxIOComplitionThreadCount = value;
                }
            }

            public bool IsMaxPrivateMemoryUsageLimitExceed
            {
                get
                {
                    try
                    {
                        foreach (var procSample in ProcessSampleCollected)
                        {
                            if (procSample.PrivateByte64GBytes >= MaxMemoryUsageLimitGB)
                                continue;
                            else
                                return false;
                        }
                        if (ProcessSampleCollected.Count < MaxSampleCount)
                            return false;
                        else
                            return true;
                    }
                    catch { }
                    return false;
                }
            }

            public bool IsMaxPrivateMemoryUsageLimitBelowMaxUsage
            {
                get
                {
                    try
                    {
                        foreach (var procSample in ProcessSampleCollected)
                        {
                            if (procSample.PrivateByte64GBytes <= MaxMemoryUsageLimitGB)
                                continue;
                            else
                                return false;
                        }
                        if (ProcessSampleCollected.Count < MaxSampleCount)
                            return false;
                        else
                            return true;
                    }
                    catch { }
                    return false;
                }
            }

            public float IdleThreadRatio
            {
                get
                {
                    long InactiveThreadCount = 0;
                    long ThreadCount = 0;
                    int sampleCount = 0;
                    float avgRatio = 0.0f;
                    try
                    {

                        foreach (var procSample in ProcessSampleCollected)
                        {
                            ThreadCount += procSample.ThreadCount;
                            InactiveThreadCount += (procSample.ThreadCount - procSample.AcitveThreadCount);
                            avgRatio += (InactiveThreadCount / ThreadCount);
                            sampleCount++;
                        }
                        avgRatio /= sampleCount;
                    }
                    catch
                    {
                        avgRatio = 0.0f;
                    }
                    return avgRatio;
                }
            }

            public bool IsThreadCountersUpdated
            {
                get
                {
                    try
                    {
                        int maxWKThreadsCount = 0, maxIOThreadsCount = 0;
                        int minWKThreadsCount = 0, minIOThreadsCount = 0;
                        try
                        {
                            ThreadPool.GetMaxThreads(out maxWKThreadsCount, out maxIOThreadsCount);
                            ThreadPool.GetMinThreads(out minWKThreadsCount, out minIOThreadsCount);
                        }
                        catch { }
                        if (maxWKThreadsCount != CurrentMaxWorkerThreadCount ||
                            maxIOThreadsCount != CurrentMaxIOCompelitionThreadCount ||
                            minWKThreadsCount != CurrentMinWorkerThreadCount ||
                            minIOThreadsCount != CurrentMinIOCompelitionThreadCount)
                        {
                            return false;
                        }
                        else
                            return true;
                    }
                    catch
                    { }
                    return false;
                }
            }

            #endregion

            #region Member Methods

            public int StepDownCurrentCount(int CurrentCounter)
            {
                int latestCounter = CurrentCounter - Convert.ToInt32(Math.Round(CurrentCounter * MinThreadCountStepDown));
                return latestCounter;
            }

            public bool StepDownThreadCounters()
            {
                try
                {
                    ///Init Current Thread Counters
                    int curMaxWThCounterBef = CurrentMaxWorkerThreadCount;
                    int curMaxIOThCounterBef = CurrentMaxIOCompelitionThreadCount;
                    int curMinWThCounterBef = CurrentMinWorkerThreadCount;
                    int curMinIOThCounterBef = CurrentMinIOCompelitionThreadCount;
                    ///Step Down Thread Counters
                    int curMaxWThCounterAft = StepDownCurrentCount(CurrentMaxWorkerThreadCount);
                    int curMaxIOThCounterAft = StepDownCurrentCount(CurrentMaxIOCompelitionThreadCount);
                    int curMinWThCounterAft = StepDownCurrentCount(CurrentMinWorkerThreadCount);
                    int curMinIOThCounterAft = StepDownCurrentCount(CurrentMinIOCompelitionThreadCount);
                    ///Update Counter Variable
                    CurrentMaxWorkerThreadCount = curMaxWThCounterAft;
                    CurrentMaxIOCompelitionThreadCount = curMaxIOThCounterAft;
                    CurrentMinWorkerThreadCount = curMinWThCounterAft;
                    curMinIOThCounterAft = CurrentMinIOCompelitionThreadCount;
                    ///Validates All Values Updated
                    if (CurrentMaxWorkerThreadCount != curMaxWThCounterAft ||
                        CurrentMaxIOCompelitionThreadCount != curMaxIOThCounterAft ||
                        CurrentMinWorkerThreadCount != curMinWThCounterAft ||
                        curMinIOThCounterAft != CurrentMinIOCompelitionThreadCount)
                    {
                        ///Restore All Previouse Values
                        CurrentMaxWorkerThreadCount = curMaxWThCounterBef;
                        CurrentMaxIOCompelitionThreadCount = curMaxIOThCounterBef;
                        CurrentMinWorkerThreadCount = curMinWThCounterBef;
                        CurrentMinIOCompelitionThreadCount = curMinIOThCounterBef;
                        return false;
                    }
                    else
                        return true;
                }
                catch
                {
                }
                return false;
            }

            public bool LowerThreadCounters()
            {
                try
                {
                    _CurMaxWorkerThreadCount = DefaultMinWorkerThreadCount;
                    _CurMaxIOComplitionThreadCount = DefaultMinIOCompelitionTheadCount;
                    _CurMinWorkerThreadCount = DefaultMinWorkerThreadCount;
                    _CurMinIOComplitionThreadCount = DefaultMinIOCompelitionTheadCount;
                    return true;
                }
                catch
                {
                }
                return false;
            }

            public void ResetCounters()
            {
                _CurMaxWorkerThreadCount = MaxWorkerThreadCount;
                _CurMaxIOComplitionThreadCount = MaxIOCompelitionThreadCount;
                _CurMinWorkerThreadCount = FinalMinWorkerThreadCount;
                _CurMinIOComplitionThreadCount = FinalMinIOCompelitionTheadCount;
            }

            public void ResetProcessSample()
            {
                ///Reset All Samples Stored
                ProcessSampleCollected.Clear();
            }

            public void TakeProcessCounterSample()
            {
                try
                {
                    ProcessInfoSample ProcessInfoSample =
                            new ProcessInfoSample(ActiveThreadCount, ThreadCount, PrivateByte64GBytes, VirtualByte64GBytes);
                    if (ProcessSampleCollected.Count > MaxSampleCount)
                        ProcessSampleCollected.Dequeue();
                    ///Insert New ProcessSample
                    ProcessSampleCollected.Enqueue(ProcessInfoSample);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occured to take process info sample", ex);
                }
            }

            public bool TryUpdateThreadCounts()
            {
                bool IsUpdated = false;
                try
                {
                    int curMaxWorkTh = CurrentMaxWorkerThreadCount, curMaxIOTh = CurrentMaxIOCompelitionThreadCount;
                    int curMinWorkTh = CurrentMinWorkerThreadCount, curMinIOTh = CurrentMinIOCompelitionThreadCount;

                    lock (this)
                    {
                        System.Threading.ThreadPool.GetMinThreads(out curMinWorkTh, out curMinIOTh);
                        System.Threading.ThreadPool.GetMaxThreads(out curMaxWorkTh, out curMaxIOTh);

                        ///Reset Connection Thread Counts Logic
                        if (curMaxWorkTh <= CurrentMinWorkerThreadCount || curMaxIOTh <= CurrentMinIOCompelitionThreadCount)
                        {
                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(CurrentMaxWorkerThreadCount, CurrentMaxIOCompelitionThreadCount);
                            if (!IsUpdated)
                                return IsUpdated;
                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(CurrentMinWorkerThreadCount, CurrentMinIOCompelitionThreadCount);
                        }
                        else
                        {
                            ///Settings.Default.WorkerThreadPoolSize, Settings.Default.IOThreadPoolSize);
                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(CurrentMinWorkerThreadCount, CurrentMinIOCompelitionThreadCount);
                            if (!IsUpdated)
                                return IsUpdated;
                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(CurrentMaxWorkerThreadCount, CurrentMaxIOCompelitionThreadCount);
                        }
                    }
                    return IsUpdated;
                }
                catch { }
                finally
                {
                    #region Debugging && Logging Message
#if Enable_DEBUG_ECHO
                    if (IsUpdated)
                    {
                        Commons.WriteAlert(String.Format("ThreadPool MaxWorkerThreadCount:{0} MaxIOThreadCount:{1} ", CurrentMaxWorkerThreadCount,
                            CurrentMaxIOCompelitionThreadCount));
                        Commons.WriteAlert(String.Format("ThreadPool MinWorkerThreadCount:{0} MinIOThreadCount:{1} ", CurrentMinWorkerThreadCount,
                            CurrentMinIOCompelitionThreadCount));
                    }
#endif
                    #endregion
                }
                return IsUpdated;
            }

            public static bool UpdateThreadCounts()
            {
                bool IsUpdated = false;
                try
                {
                    int curMaxWorkTh = MaxWorkerThreadCount, curMaxIOTh = MaxIOCompelitionThreadCount;
                    int curMinWorkTh = FinalMinWorkerThreadCount, curMinIOTh = FinalMinIOCompelitionTheadCount;

                    lock (ProcessInfo_MemLock)
                    {
                        System.Threading.ThreadPool.GetMinThreads(out curMinWorkTh, out curMinIOTh);
                        System.Threading.ThreadPool.GetMaxThreads(out curMaxWorkTh, out curMaxIOTh);

                        ///Reset Connection Thread Counts Logic
                        if (curMaxWorkTh <= FinalMinWorkerThreadCount || curMaxIOTh <= FinalMinIOCompelitionTheadCount)
                        {
                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(MaxWorkerThreadCount, MaxIOCompelitionThreadCount);
                            if (!IsUpdated)
                                return IsUpdated;
                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(FinalMinWorkerThreadCount, FinalMinIOCompelitionTheadCount);
                        }
                        else
                        {
                            ///Settings.Default.WorkerThreadPoolSize, Settings.Default.IOThreadPoolSize);
                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(MaxWorkerThreadCount, MaxIOCompelitionThreadCount);
                            if (!IsUpdated)
                                return IsUpdated;
                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(MaxWorkerThreadCount, MaxIOCompelitionThreadCount);
                        }
                    }
                    return IsUpdated;
                }
                catch { }
                finally
                {
                    #region Debugging && Logging Message
#if Enable_DEBUG_ECHO
                    if (IsUpdated)
                    {
                        //Commons.WriteAlert(String.Format("ThreadPool MaxWorkerThreadCount:{0} MaxIOThreadCount:{1} ", CurrentMaxWorkerThreadCount,
                        //    CurrentMaxIOCompelitionThreadCount));
                        //Commons.WriteAlert(String.Format("ThreadPool MinWorkerThreadCount:{0} MinIOThreadCount:{1} ", CurrentMinWorkerThreadCount,
                        //    CurrentMinIOCompelitionThreadCount));
                    }
#endif
                    #endregion
                }
                return IsUpdated;
            }

            #endregion

        }

        public struct ProcessInfoSample
        {
            public long AcitveThreadCount;
            public long ThreadCount;
            public float PrivateByte64GBytes;
            public float VirtualByte64GBytes;

            public ProcessInfoSample(long AcitveThreadCount = 0,
            long ThreadCount = 0,
            float PrivateByte64GBytes = 0.0f,
            float VirtualByte64GBytes = 0.0f)
            {
                ///Init ProcessInfoSample
                this.AcitveThreadCount = AcitveThreadCount;
                this.ThreadCount = ThreadCount;
                this.PrivateByte64GBytes = PrivateByte64GBytes;
                this.VirtualByte64GBytes = VirtualByte64GBytes;
            }
        }

        /*
        #region Code from Common.cs
        //Added by Azeem Inayat
        //v10.0.14 for AOS
        public static string ACT34G_Meter = "ACT34G";

        //End

        public static readonly TimeSpan ReadLOCKLow_TimeOut = TimeSpan.FromSeconds(05);
        public static readonly TimeSpan ReadLOCK_TimeOut = TimeSpan.FromSeconds(25);
        public static readonly TimeSpan WriteLOCK_TimeOut = TimeSpan.FromSeconds(45);
        public static readonly TimeSpan WriteLOCKLow_TimeOut = TimeSpan.FromSeconds(05);

        public static readonly float KiloByteDivider = 1024.0f;
        public static readonly float MegaByteDivider = 1048576.0f;
        public static readonly float GigaByteDivider = 1073741824.0f;

        public static bool EnableEcho = false;
        public static Random NextRandomNum = null;

        #region Out_Put_Class

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static FileStream fs = null;
        public static StreamWriter wr = null;
        public static BufferedStream _ConsoleStream = null;
        public static readonly int ExceptionMaxLevel = 50;
        public static readonly string LogSourceName = "MySource";
        /// <summary>
        /// Static Constructur
        /// </summary>
        static Commons()
        {
            Stream stdOut = Console.OpenStandardOutput();
            _ConsoleStream = new BufferedStream(stdOut, 2048);
            string LoadedAssemblyInfo = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            LogSourceName = "MySource" + LoadedAssemblyInfo;
            NextRandomNum = new Random();
            ///Init Variables
            //ManagementDevice = new SAPConfig();
            //ManagementDevice.SAP = new SAP_Object("Management", 0x01);
            //ManagementDevice.RequestAccessRightsRead = false;
            //ManagementDevice.FaceName = "Management";
            //ManagementDevice.DefaultPassword = "mtiDLMScosem";

            //Public = new SAP_Object("Public", 0x10);
            //Management_Client = new SAP_Object("Management", 0x01);

        }

        #region Event_Logger

        public static EventLog GetEventLogger()
        {
            try
            {
                if (!EventLog.SourceExists(LogSourceName))
                {
                    //An event log source should not be created and immediately used. 
                    //There is a latency time to enable the source, it should be created 
                    //prior to executing the application that uses the source. 
                    //Execute this sample a second time to use the new source.
                    EventLog.CreateEventSource(LogSourceName, "Application");
                    // The source is created.  Exit the application to allow it to be registered. 
                }
                EventLog _logger = new EventLog("MTI_MDC_EventLog", ".", LogSourceName);
                return _logger;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occured while Getting Event Logger", ex);
            }
        }

        public static void LogApplicationException(Exception ex, int curLevel = 10)
        {
            try
            {
                StringBuilder builder = BuildExceptionLog(ex, curLevel);
                EventLog systemEventLogger = GetEventLogger();
                systemEventLogger.WriteEntry(builder.ToString(), EventLogEntryType.FailureAudit);
            }
            catch (Exception) { }
        }

        #endregion

        #region Save Error Exceptions

        public static StreamWriter ExceptionsInit(string relativeDirPath, string fileName)
        {
            try
            {
                FileStream fs = null;
                StreamWriter wr = null;

                string dir = relativeDirPath;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string path = dir + String.Format("\\{0}", fileName);
                if (!File.Exists(path))
                    fs = new FileStream(path, FileMode.CreateNew);
                else
                    fs = File.Open(path, FileMode.Append);
                wr = new StreamWriter(fs);
                return wr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveException(Exception ex, string fileURL, int curLevel = 14)
        {
            Exception _ex = ex;
            try
            {
                FileInfo FileINfo = new FileInfo(fileURL);
                string fileName = FileINfo.Name;
                string dirName = FileINfo.DirectoryName;

                ///File Lenght Exceeds 20K 
                if (FileINfo.Exists && FileINfo.Length > 20480)
                    BackUpFileSameLoc(fileURL);

                using (StreamWriter wr = ExceptionsInit(dirName, fileName))
                {
                    StringBuilder builder = BuildExceptionLog(ex, curLevel);
                    wr.WriteLine(builder.ToString());
                }
            }
            catch (Exception) { }
        }

        public static StringBuilder BuildExceptionLog(Exception ex, int curLevel = 14)
        {
            StringBuilder builder = new StringBuilder(512);

            builder.AppendFormat("Unhandled Error{0} {1},details\r\n", DateTime.Now, ex.Message);
            Exception _ex = ex;

            while (_ex != null && curLevel < ExceptionMaxLevel)
            {
                _ex = _ex.InnerException;
                if (_ex == null)
                    break;
                builder.AppendFormat("{0}\r\n", _ex.Message);
                curLevel++;
            }
            if (ex != null)
                builder.AppendLine(ex.StackTrace);
            return builder;
        }

        public static void SaveApplicationException(Exception ex, int curLevel = 5)
        {
            try
            {
                string fileUrl = String.Format(@"{0}\Exceptions\Exception_{1}.txt", Commons.GetApplicationErrorDirectory(), DateTime.Now.ToFileTime());
                SaveException(ex, fileUrl);
            }
            catch (Exception) { }
        }


        public static void BackUpFileSameLoc(string fileUrl)
        {
            try
            {
                FileInfo FileINfo = new FileInfo(fileUrl);
                if (FileINfo.Exists)
                {
                    String NewFileName = FileINfo.Name.Replace(FileINfo.Extension, "");
                    NewFileName = String.Format("{0}_{1}_{2}", NewFileName, DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                    ///Process New File Name
                    NewFileName = NewFileName.Replace(" ", "");
                    NewFileName = NewFileName.Replace(".", "");
                    NewFileName = NewFileName.Replace("/", "");
                    NewFileName = NewFileName.Replace(":", "");
                    NewFileName = String.Format(@"{0}\{1}{2}", FileINfo.Directory, NewFileName, FileINfo.Extension);
                    File.Move(fileUrl, NewFileName);
                    File.Delete(fileUrl);
                }
            }
            catch
            {
            }
        }

        public static void SaveApplicationLogMessage(StringBuilder BuilderMsg, string fileURL)
        {

            try
            {
                FileInfo FileINfo = new FileInfo(fileURL);
                ///File Lenght Exceeds 10K 
                if (FileINfo.Exists && FileINfo.Length > 10240)
                    BackUpFileSameLoc(fileURL);

                string fileName = FileINfo.Name;
                string dirName = FileINfo.DirectoryName;

                using (StreamWriter wr = ExceptionsInit(dirName, fileName))
                {
                    wr.WriteLine(BuilderMsg);
                }
            }
            catch (Exception) { }
        }

        #endregion

        public static void WriteLine(string text)
        {
            Write(text + "\r\n-------\t\t\t--------\t\t\t--------\r\n");
        }

        public static void Write(string text)
        {
            try
            {
                Console.Out.Write(text);
                //byte[] textAsBytes = Encoding.UTF8.GetBytes(text);
                //_ConsoleStream.Write(textAsBytes, 0, textAsBytes.Length);
            }
            catch (Exception)
            { }
        }

        public static void WriteError(string text)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine("\t" + text);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception)
            { }
        }

        public static void WriteSuccess(string text)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Out.WriteLine("\t" + text);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception)
            { }
        }
        public static void WriteAlert(string text)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Out.WriteLine("\t" + text);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception)
            { }
        }

        #endregion

        public static string DecimalPoint_toGUI(byte value)
        {
            string temp;
            int upper;
            int lower;
            upper = value / 16;
            lower = value % 16;
            temp = "";
            temp = temp.PadLeft(upper, '0') + "." + temp.PadRight(lower, '0');
            //temp =;
            return temp;
        }
        /// <summary>
        /// Removes blank spaces and new line chars from the string
        /// </summary>
        /// <param name="passed"></param>
        /// <returns></returns>
        private static string Remove_White_Spaces(string passed)
        {

            passed = passed.Replace(" ", "");
            passed = passed.Replace("\x0d", "");
            passed = passed.Replace("\x0a", "");

            return passed;
        }

        //==========================================================================
        //==========================================================================
        /// <summary>
        /// Converts byte array to string in hex format
        /// </summary>
        /// <param name="Hex_Array"></param>
        /// <returns></returns>
        static public string ArrayToHexString(byte[] Hex_Array)
        {
            string Return_String = "";
            foreach (byte Value in Hex_Array) Return_String += (Value.ToString("X2") + " ");
            return Return_String;
        }

        //==========================================================================
        //==========================================================================
        public static byte[] String_to_Hex_array(string passed)
        {
            passed = Remove_White_Spaces(passed);

            passed = passed.ToUpper();

            char[] temp = passed.ToCharArray();

            byte[] to_return = new byte[(temp.Length) / 2];

            for (byte i = 0; i < to_return.Length; i++)
            {
                if (temp[2 * i] >= '0' && temp[2 * i] <= '9') to_return[i] |= (byte)((temp[2 * i] - 0x30) << 4);
                else if (temp[2 * i] >= 'A' && temp[2 * i] <= 'F') to_return[i] |= (byte)((temp[2 * i] - 0x37) << 4);

                if (temp[(2 * i) + 1] >= '0' && temp[(2 * i) + 1] <= '9') to_return[i] |= (byte)(temp[(2 * i) + 1] - 0x30);
                else if (temp[(2 * i) + 1] >= 'A' && temp[(2 * i) + 1] <= 'F') to_return[i] |= (byte)(temp[(2 * i) + 1] - 0x37);
            }


            return to_return;
        }
        //==========================================================================
        #region Array Functions


        static public byte[] Append_to_End(byte[] First_Array, byte[] Second_Array)
        {
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Appends second_byt at the end of first array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="second_byte"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte[] First_Array, byte second_byte)
        {
            byte[] Second_Array = new byte[1] { second_byte };
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Appends second_long at the end of First_Array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="second_long"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte[] First_Array, ulong second_long)
        {
            byte[] Second_Array = new byte[4]
            { (byte)((ulong)second_long >> 24 & 0xff), (byte)((ulong)second_long >> 16 & 0xff),
             (byte)((ulong)second_long>>8 & 0xff), (byte)((ulong)second_long & 0xff) };
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Takes two bytes and appends them in a byte array
        /// </summary>
        /// <param name="First_Byte"></param>
        /// <param name="second_byte"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte First_Byte, byte second_byte)
        {
            byte[] First_Array = new byte[1] { First_Byte };
            byte[] Second_Array = new byte[1] { second_byte };
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Appends second_short at the end of First_Array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="second_short"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte[] First_Array, UInt16 second_short)
        {
            byte[] Second_Array = new byte[2] { (byte)((short)second_short >> 8 & 0xff), (byte)((short)second_short & 0xff) };

            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }


        //==========================================================================
        /// <summary>
        /// Appends second array at the start of first array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="Second_Array"></param>
        /// <returns></returns>
        static public byte[] Append_to_Start(byte[] First_Array, byte[] Second_Array)
        {
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        static public byte[] Append_to_Start(byte[] First_Array, byte second_byte)
        {
            byte[] Second_Array = new byte[1] { second_byte };
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        static public byte[] Append_to_Start(byte[] First_Array, UInt16 second_short)
        {
            byte[] Second_Array = new byte[2] { (byte)((short)second_short >> 8 & 0xff), (byte)((short)second_short & 0xff) };

            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

            return Appended;
        }
        #endregion

        #region Default_Application_Directory

        public static string GetApplicationConfigsDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];
                if (!Directory.Exists(fileDirectoryPath))
                {
                    fileDirectoryPath = Environment.CurrentDirectory + @"\Application_Configs";
                    if (!Directory.Exists(fileDirectoryPath))
                        Directory.CreateDirectory(fileDirectoryPath);
                    Settings.Default["ApplicationConfigsDirectory"] = fileDirectoryPath;
                    Settings.Default.Save();
                }
                return fileDirectoryPath;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static string GetApplicationLogsDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];

                fileDirectoryPath = Environment.CurrentDirectory + @"\Application_IOLogs";
                if (!Directory.Exists(fileDirectoryPath))
                    Directory.CreateDirectory(fileDirectoryPath);

                return fileDirectoryPath;
            }
            catch (Exception ex)
            {
                return Environment.CurrentDirectory;
            }
        }

        public static string GetApplicationErrorDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];

                fileDirectoryPath = Environment.CurrentDirectory + @"\Application_Errors";
                if (!Directory.Exists(fileDirectoryPath))
                    Directory.CreateDirectory(fileDirectoryPath);

                return fileDirectoryPath;
            }
            catch (Exception ex)
            {
                return Environment.CurrentDirectory;
            }
        }

        public static AppDomain GetCurrentAppDomain()
        {
            AppDomain domain = null;
            try
            {
                ///Load Current Application Domain
                AppDomain root = AppDomain.CurrentDomain;
                AppDomainSetup setup = new AppDomainSetup();
                domain = AppDomain.CreateDomain(GetApplicationConfigsDirectory(), null, setup);
            }
            catch
            {
                domain = AppDomain.CurrentDomain;
            }
            return domain;
        }


        #endregion

        public static String GetAccessRightsFilePath(string associationName)
        {
            try
            {
                if (String.IsNullOrEmpty(associationName))
                    throw new Exception("Invalid Association Name");
                String fileName = String.Format(@"{0}\{1}.xml", GetApplicationConfigsDirectory(), associationName);
                return fileName;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static string[] Directory_GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            string[] searchPatterns = searchPattern.Split('|');
            List<string> files = new List<string>();
            foreach (string sp in searchPatterns)
                files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
            files.Sort();
            return files.ToArray();
        }

        public static string value_to_string(double value, bool Format)
        {
            if (double.IsNaN(value))
                return "Not Access";
            else if (double.IsPositiveInfinity(value))
                return "Not Exist";
            else if (double.IsNegativeInfinity(value))
                return "Meter BUG";
            else return value.ToString("f3");
        }
        public static string value_to_string(double value)
        {
            if (double.IsNaN(value))
                return "Not Access";
            else if (double.IsPositiveInfinity(value))
                return "Not Exist";
            else if (double.IsNegativeInfinity(value))
                return "Meter BUG";
            else return value.ToString("");
        }

        public static bool IsThreadRunning(Thread ThRunner)
        {
            bool IsRunning = false;
            try
            {
                if (ThRunner != null)
                {
                    #region Test Allocater Thread Status

                    if (ThRunner.ThreadState == System.Threading.ThreadState.Unstarted ||
                                        ThRunner.ThreadState == System.Threading.ThreadState.StopRequested ||
                                       ThRunner.ThreadState == System.Threading.ThreadState.Stopped ||
                                       ThRunner.ThreadState == System.Threading.ThreadState.SuspendRequested ||
                                        ThRunner.ThreadState == System.Threading.ThreadState.Suspended ||
                                        ThRunner.ThreadState == System.Threading.ThreadState.AbortRequested ||
                                        ThRunner.ThreadState == System.Threading.ThreadState.Aborted)
                        IsRunning = false;
                    else if (ThRunner.IsAlive)
                        IsRunning = true;

                    #endregion
                }
            }
            catch
            { }
            return IsRunning;
        }

        #region Process_Info

        public static int Processor_CoreCount
        {
            get
            {
                int coreCount = 0;
                try
                {

                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
                    {
                        coreCount += int.Parse(item["NumberOfCores"].ToString());
                    }
                }
                catch
                {
                    coreCount = -1;
                }
                return coreCount;
            }
        }

        public static int ProcessorCount
        {
            get
            {
                int processorCount = 0;
                try
                {
                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                    {
                        processorCount += int.Parse(item["NumberOfProcessors"].ToString());
                    }

                }
                catch
                {
                    processorCount = -1;
                }
                return processorCount;
            }
        }

        public static int CPU_LogicalCoreCount
        {
            get
            {
                int coreCount = 0;
                try
                {

                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                    {
                        coreCount += int.Parse(item["NumberOfLogicalProcessors"].ToString());
                    }
                }
                catch
                {
                    coreCount = -1;
                }
                return coreCount;
            }
        }

        public static float TotalPhysicalMemory_GigaByte
        {
            get
            {
                float GigaByteMemory = 0.0f;
                long memoryBytes = 0;
                try
                {
                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                    {
                        memoryBytes += long.Parse(item["TotalPhysicalMemory"].ToString());
                    }
                    GigaByteMemory = memoryBytes / GigaByteDivider;
                }
                catch
                {
                    memoryBytes = 0;
                }
                return GigaByteMemory;
            }
        }

        public static int Processor_LogicalCoreCount
        {
            get
            {
                try
                {
                    return Environment.ProcessorCount;
                }
                catch
                { }
                return -1;
            }
        }

        public static float WorkingSetGBytes
        {
            get
            {
                float WorkingSetInMBytes = -1.0f;
                try
                {
                    WorkingSetInMBytes = Environment.WorkingSet / (GigaByteDivider);
                }
                catch
                {
                    WorkingSetInMBytes = -1.0f;
                }
                return WorkingSetInMBytes;
            }
        }

        public static long ActiveThreadCount
        {
            get
            {
                try
                {
                    return ((IEnumerable)System.Diagnostics.Process.GetCurrentProcess().Threads)
           .OfType<System.Diagnostics.ProcessThread>()
           .Where(t => t.ThreadState == System.Diagnostics.ThreadState.Running)
           .Count();
                }
                catch { }
                return -1;
            }
        }

        public static long ThreadCount
        {
            get
            {
                try
                {
                    return System.Diagnostics.Process.GetCurrentProcess().Threads.Count;
                }
                catch { }
                return 0;
            }
        }

        public static float WorkingSet64GBytes
        {
            get
            {
                float WorkingSetInMBytes = -1.0f;
                try
                {
                    WorkingSetInMBytes = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (GigaByteDivider);
                }
                catch
                {
                    WorkingSetInMBytes = -1.0f;
                }
                return WorkingSetInMBytes;
            }
        }

        public static float PrivateByte64GBytes
        {
            get
            {
                float PrivateBytetInMBytes = -1.0f;
                try
                {
                    PrivateBytetInMBytes = System.Diagnostics.Process.GetCurrentProcess().PrivateMemorySize64 / (GigaByteDivider);
                }
                catch
                {
                    PrivateBytetInMBytes = -1.0f;
                }
                return PrivateBytetInMBytes;
            }
        }

        public static float VirtualByte64GBytes
        {
            get
            {
                float VirtualSetInMBytes = -1.0f;
                try
                {
                    VirtualSetInMBytes = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64 / (GigaByteDivider);
                }
                catch
                {
                    VirtualSetInMBytes = -1.0f;
                }
                return VirtualSetInMBytes;
            }
        }

        #endregion

        public class ProcessInfo
        {

            #region Limit Variable

            /// <summary>
            /// Modify Variable To Change Max Limit Variable 
            /// </summary>
            internal static readonly int MaxWorkerThreadPerCore = 250;
            internal static readonly int MaxIOCompThreadPerCore = 1000;
            internal static readonly float MinPrivateByteMemoryRatio = 0.75f;  ///Orignal 0.75f
            internal static readonly float MaxPrivateByteMemoryRatio = 0.50f;  ///Orignal 0.50f
            internal static readonly float MinMemoryLimit = 4.0f;
            internal static readonly float LowMinMomoryLimit = 0.45f;          ///Orignal 0.35
            internal static readonly float MaxToMinThreadRatio = 0.10f;
            public static readonly float MinThreadCountStepDown = 0.25f;
            public static readonly int MaxSampleCount = 10;


            #endregion

            #region Data_Members

            public static readonly int Processor_Count = 1;
            public static readonly int Processor_Core_Count = 1;

            public static readonly float Total_AvailPhysicalMemory = 1.0f;

            public static readonly int MaxWorkerThreadCount = 25;
            public static readonly int DefaultMinWorkerThreadCount = 25;
            public static readonly int FinalMinWorkerThreadCount = 25;
            public static readonly int MaxIOCompelitionThreadCount = 1000;
            public static readonly int DefaultMinIOCompelitionTheadCount = 1000;
            public static readonly int FinalMinIOCompelitionTheadCount = 1000;

            public static readonly float MaxMemoryUsageLimitGB = 1.5f;
            public static readonly float MinMemoryUsageLimitGB = 0.7f;

            internal static readonly object ProcessInfo_MemLock = null;
            #endregion

            #region Instance Data_Members

            private int _CurMinWorkerThreadCount = 25;
            private int _CurMinIOComplitionThreadCount = 1000;
            private int _CurMaxWorkerThreadCount = 25;
            private int _CurMaxIOComplitionThreadCount = 1000;
            private Queue<ProcessInfoSample> ProcessSampleCollected = null;

            #endregion

            #region Constructur

            /// <summary>
            /// Static Constructur To Init Class Variables
            /// </summary>
            static ProcessInfo()
            {
                try
                {
                    ///Init Physical Processor Count
                    Processor_Count = ProcessorCount;
                    ///Init Processor_Core_Count
                    Processor_Core_Count = Processor_CoreCount;
                    ///Init System Available Physical Memory Limit
                    Total_AvailPhysicalMemory = TotalPhysicalMemory_GigaByte;
                    if (Processor_Core_Count > 0)
                    {
                        ///Init Max Worker Thread Counts
                        MaxWorkerThreadCount = Processor_Core_Count * MaxWorkerThreadPerCore;
                        FinalMinWorkerThreadCount = Convert.ToInt32(Math.Round(MaxWorkerThreadCount * MaxToMinThreadRatio));
                        ///Init MaxIOComplitionThreadCount
                        MaxIOCompelitionThreadCount = Processor_Core_Count * MaxIOCompThreadPerCore;
                        FinalMinIOCompelitionTheadCount = Convert.ToInt32(Math.Round(MaxIOCompelitionThreadCount * MaxToMinThreadRatio));
                    }
                    if (Total_AvailPhysicalMemory > 0)
                    {
                        if (Total_AvailPhysicalMemory <= MinMemoryLimit)
                        {
                            MaxMemoryUsageLimitGB = Total_AvailPhysicalMemory * MinPrivateByteMemoryRatio;
                        }
                        else
                        {
                            MaxMemoryUsageLimitGB = Total_AvailPhysicalMemory * MaxPrivateByteMemoryRatio;
                        }
                        MinMemoryUsageLimitGB = Total_AvailPhysicalMemory * LowMinMomoryLimit;
                    }
                    ProcessInfo_MemLock = new object();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occured while initialize ProcessInfo_Common", ex);
                }
            }

            public ProcessInfo()
            {
                try
                {
                    ///Init Instance Variable
                    ProcessSampleCollected = new Queue<ProcessInfoSample>();
                    _CurMaxWorkerThreadCount = MaxWorkerThreadCount;
                    _CurMaxIOComplitionThreadCount = MaxIOCompelitionThreadCount;
                    _CurMinWorkerThreadCount = FinalMinWorkerThreadCount;
                    _CurMinIOComplitionThreadCount = FinalMinIOCompelitionTheadCount;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occured while initialize ProcessInfo_Common", ex);
                }
            }

            #endregion

            #region Property

            public static float WorkingSetGBytes
            {
                get
                {
                    return Commons.WorkingSetGBytes;
                }
            }

            public static long ActiveThreadCount
            {
                get
                {
                    return Commons.ActiveThreadCount;
                }
            }

            public static long ThreadCount
            {
                get
                {
                    return Commons.ThreadCount;
                }
            }

            public static float WorkingSet64GBytes
            {
                get
                {
                    return Commons.WorkingSet64GBytes;
                }
            }

            public static float PrivateByte64GBytes
            {
                get
                {
                    return Commons.PrivateByte64GBytes;
                }
            }

            public static float VirtualByte64GBytes
            {
                get
                {
                    return Commons.VirtualByte64GBytes;
                }
            }

            #endregion

            #region Property_Instance

            public int CurrentMinWorkerThreadCount
            {
                get { return _CurMinWorkerThreadCount; }
                set
                {
                    if (value >= DefaultMinWorkerThreadCount &&
                        value <= FinalMinWorkerThreadCount &&
                        value <= CurrentMaxWorkerThreadCount)
                        _CurMinWorkerThreadCount = value;
                }
            }

            public int CurrentMinIOCompelitionThreadCount
            {
                get { return _CurMinIOComplitionThreadCount; }
                set
                {
                    if (value >= DefaultMinIOCompelitionTheadCount &&
                        value <= FinalMinIOCompelitionTheadCount &&
                        value <= CurrentMaxIOCompelitionThreadCount)
                        _CurMinIOComplitionThreadCount = value;
                }
            }

            public int CurrentMaxWorkerThreadCount
            {
                get { return _CurMaxWorkerThreadCount; }
                set
                {
                    if ((value >= CurrentMinWorkerThreadCount &&
                        value <= MaxWorkerThreadCount) ||
                        value == DefaultMinWorkerThreadCount)
                        _CurMaxWorkerThreadCount = value;
                }
            }

            public int CurrentMaxIOCompelitionThreadCount
            {
                get { return _CurMaxIOComplitionThreadCount; }
                set
                {
                    if ((value >= CurrentMinIOCompelitionThreadCount &&
                        value <= MaxIOCompelitionThreadCount) ||
                        value == DefaultMinIOCompelitionTheadCount)
                        _CurMaxIOComplitionThreadCount = value;
                }
            }

            public bool IsMaxPrivateMemoryUsageLimitExceed
            {
                get
                {
                    try
                    {
                        foreach (var procSample in ProcessSampleCollected)
                        {
                            if (procSample.PrivateByte64GBytes >= MaxMemoryUsageLimitGB)
                                continue;
                            else
                                return false;
                        }
                        if (ProcessSampleCollected.Count < MaxSampleCount)
                            return false;
                        else
                            return true;
                    }
                    catch { }
                    return false;
                }
            }

            public bool IsMaxPrivateMemoryUsageLimitBelowMaxUsage
            {
                get
                {
                    try
                    {
                        foreach (var procSample in ProcessSampleCollected)
                        {
                            if (procSample.PrivateByte64GBytes <= MaxMemoryUsageLimitGB)
                                continue;
                            else
                                return false;
                        }
                        if (ProcessSampleCollected.Count < MaxSampleCount)
                            return false;
                        else
                            return true;
                    }
                    catch { }
                    return false;
                }
            }

            public float IdleThreadRatio
            {
                get
                {
                    long InactiveThreadCount = 0;
                    long ThreadCount = 0;
                    int sampleCount = 0;
                    float avgRatio = 0.0f;
                    try
                    {

                        foreach (var procSample in ProcessSampleCollected)
                        {
                            ThreadCount += procSample.ThreadCount;
                            InactiveThreadCount += (procSample.ThreadCount - procSample.AcitveThreadCount);
                            avgRatio += (InactiveThreadCount / ThreadCount);
                            sampleCount++;
                        }
                        avgRatio /= sampleCount;
                    }
                    catch
                    {
                        avgRatio = 0.0f;
                    }
                    return avgRatio;
                }
            }

            public bool IsThreadCountersUpdated
            {
                get
                {
                    try
                    {
                        int maxWKThreadsCount = 0, maxIOThreadsCount = 0;
                        int minWKThreadsCount = 0, minIOThreadsCount = 0;
                        try
                        {
                            ThreadPool.GetMaxThreads(out maxWKThreadsCount, out maxIOThreadsCount);
                            ThreadPool.GetMinThreads(out minWKThreadsCount, out minIOThreadsCount);
                        }
                        catch { }
                        if (maxWKThreadsCount != CurrentMaxWorkerThreadCount ||
                            maxIOThreadsCount != CurrentMaxIOCompelitionThreadCount ||
                            minWKThreadsCount != CurrentMinWorkerThreadCount ||
                            minIOThreadsCount != CurrentMinIOCompelitionThreadCount)
                        {
                            return false;
                        }
                        else
                            return true;
                    }
                    catch
                    { }
                    return false;
                }
            }

            #endregion

            #region Member Methods

            public int StepDownCurrentCount(int CurrentCounter)
            {
                int latestCounter = CurrentCounter - Convert.ToInt32(Math.Round(CurrentCounter * MinThreadCountStepDown));
                return latestCounter;
            }

            public bool StepDownThreadCounters()
            {
                try
                {
                    ///Init Current Thread Counters
                    int curMaxWThCounterBef = CurrentMaxWorkerThreadCount;
                    int curMaxIOThCounterBef = CurrentMaxIOCompelitionThreadCount;
                    int curMinWThCounterBef = CurrentMinWorkerThreadCount;
                    int curMinIOThCounterBef = CurrentMinIOCompelitionThreadCount;
                    ///Step Down Thread Counters
                    int curMaxWThCounterAft = StepDownCurrentCount(CurrentMaxWorkerThreadCount);
                    int curMaxIOThCounterAft = StepDownCurrentCount(CurrentMaxIOCompelitionThreadCount);
                    int curMinWThCounterAft = StepDownCurrentCount(CurrentMinWorkerThreadCount);
                    int curMinIOThCounterAft = StepDownCurrentCount(CurrentMinIOCompelitionThreadCount);
                    ///Update Counter Variable
                    CurrentMaxWorkerThreadCount = curMaxWThCounterAft;
                    CurrentMaxIOCompelitionThreadCount = curMaxIOThCounterAft;
                    CurrentMinWorkerThreadCount = curMinWThCounterAft;
                    curMinIOThCounterAft = CurrentMinIOCompelitionThreadCount;
                    ///Validates All Values Updated
                    if (CurrentMaxWorkerThreadCount != curMaxWThCounterAft ||
                        CurrentMaxIOCompelitionThreadCount != curMaxIOThCounterAft ||
                        CurrentMinWorkerThreadCount != curMinWThCounterAft ||
                        curMinIOThCounterAft != CurrentMinIOCompelitionThreadCount)
                    {
                        ///Restore All Previouse Values
                        CurrentMaxWorkerThreadCount = curMaxWThCounterBef;
                        CurrentMaxIOCompelitionThreadCount = curMaxIOThCounterBef;
                        CurrentMinWorkerThreadCount = curMinWThCounterBef;
                        CurrentMinIOCompelitionThreadCount = curMinIOThCounterBef;
                        return false;
                    }
                    else
                        return true;
                }
                catch
                {
                }
                return false;
            }

            public bool LowerThreadCounters()
            {
                try
                {
                    _CurMaxWorkerThreadCount = DefaultMinWorkerThreadCount;
                    _CurMaxIOComplitionThreadCount = DefaultMinIOCompelitionTheadCount;
                    _CurMinWorkerThreadCount = DefaultMinWorkerThreadCount;
                    _CurMinIOComplitionThreadCount = DefaultMinIOCompelitionTheadCount;
                    return true;
                }
                catch
                {
                }
                return false;
            }

            public void ResetCounters()
            {
                _CurMaxWorkerThreadCount = MaxWorkerThreadCount;
                _CurMaxIOComplitionThreadCount = MaxIOCompelitionThreadCount;
                _CurMinWorkerThreadCount = FinalMinWorkerThreadCount;
                _CurMinIOComplitionThreadCount = FinalMinIOCompelitionTheadCount;
            }

            public void ResetProcessSample()
            {
                ///Reset All Samples Stored
                ProcessSampleCollected.Clear();
            }

            public void TakeProcessCounterSample()
            {
                try
                {
                    ProcessInfoSample ProcessInfoSample =
                            new ProcessInfoSample(ActiveThreadCount, ThreadCount, PrivateByte64GBytes, VirtualByte64GBytes);
                    if (ProcessSampleCollected.Count > MaxSampleCount)
                        ProcessSampleCollected.Dequeue();
                    ///Insert New ProcessSample
                    ProcessSampleCollected.Enqueue(ProcessInfoSample);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occured to take process info sample", ex);
                }
            }

            public bool TryUpdateThreadCounts()
            {
                bool IsUpdated = false;
                try
                {
                    int curMaxWorkTh = CurrentMaxWorkerThreadCount, curMaxIOTh = CurrentMaxIOCompelitionThreadCount;
                    int curMinWorkTh = CurrentMinWorkerThreadCount, curMinIOTh = CurrentMinIOCompelitionThreadCount;

                    lock (this)
                    {
                        System.Threading.ThreadPool.GetMinThreads(out curMinWorkTh, out curMinIOTh);
                        System.Threading.ThreadPool.GetMaxThreads(out curMaxWorkTh, out curMaxIOTh);

                        ///Reset Connection Thread Counts Logic
                        if (curMaxWorkTh <= CurrentMinWorkerThreadCount || curMaxIOTh <= CurrentMinIOCompelitionThreadCount)
                        {
                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(CurrentMaxWorkerThreadCount, CurrentMaxIOCompelitionThreadCount);
                            if (!IsUpdated)
                                return IsUpdated;
                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(CurrentMinWorkerThreadCount, CurrentMinIOCompelitionThreadCount);
                        }
                        else
                        {
                            ///Settings.Default.WorkerThreadPoolSize, Settings.Default.IOThreadPoolSize);
                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(CurrentMinWorkerThreadCount, CurrentMinIOCompelitionThreadCount);
                            if (!IsUpdated)
                                return IsUpdated;
                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(CurrentMaxWorkerThreadCount, CurrentMaxIOCompelitionThreadCount);
                        }
                    }
                    return IsUpdated;
                }
                catch { }
                finally
                {
                    #region Debugging && Logging Message
#if Enable_DEBUG_ECHO
                    if (IsUpdated)
                    {
                        Commons.WriteAlert(String.Format("ThreadPool MaxWorkerThreadCount:{0} MaxIOThreadCount:{1} ", CurrentMaxWorkerThreadCount,
                            CurrentMaxIOCompelitionThreadCount));
                        Commons.WriteAlert(String.Format("ThreadPool MinWorkerThreadCount:{0} MinIOThreadCount:{1} ", CurrentMinWorkerThreadCount,
                            CurrentMinIOCompelitionThreadCount));
                    }
#endif
                    #endregion
                }
                return IsUpdated;
            }

            public static bool UpdateThreadCounts()
            {
                bool IsUpdated = false;
                try
                {
                    int curMaxWorkTh = MaxWorkerThreadCount, curMaxIOTh = MaxIOCompelitionThreadCount;
                    int curMinWorkTh = FinalMinWorkerThreadCount, curMinIOTh = FinalMinIOCompelitionTheadCount;

                    lock (ProcessInfo_MemLock)
                    {
                        System.Threading.ThreadPool.GetMinThreads(out curMinWorkTh, out curMinIOTh);
                        System.Threading.ThreadPool.GetMaxThreads(out curMaxWorkTh, out curMaxIOTh);

                        ///Reset Connection Thread Counts Logic
                        if (curMaxWorkTh <= FinalMinWorkerThreadCount || curMaxIOTh <= FinalMinIOCompelitionTheadCount)
                        {
                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(MaxWorkerThreadCount, MaxIOCompelitionThreadCount);
                            if (!IsUpdated)
                                return IsUpdated;
                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(FinalMinWorkerThreadCount, FinalMinIOCompelitionTheadCount);
                        }
                        else
                        {
                            ///Settings.Default.WorkerThreadPoolSize, Settings.Default.IOThreadPoolSize);
                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(MaxWorkerThreadCount, MaxIOCompelitionThreadCount);
                            if (!IsUpdated)
                                return IsUpdated;
                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(MaxWorkerThreadCount, MaxIOCompelitionThreadCount);
                        }
                    }
                    return IsUpdated;
                }
                catch { }
                finally
                {
                    #region Debugging && Logging Message
#if Enable_DEBUG_ECHO
                    if (IsUpdated)
                    {
                        //Commons.WriteAlert(String.Format("ThreadPool MaxWorkerThreadCount:{0} MaxIOThreadCount:{1} ", CurrentMaxWorkerThreadCount,
                        //    CurrentMaxIOCompelitionThreadCount));
                        //Commons.WriteAlert(String.Format("ThreadPool MinWorkerThreadCount:{0} MinIOThreadCount:{1} ", CurrentMinWorkerThreadCount,
                        //    CurrentMinIOCompelitionThreadCount));
                    }
#endif
                    #endregion
                }
                return IsUpdated;
            }

            #endregion

        }

        public struct ProcessInfoSample
        {
            public long AcitveThreadCount;
            public long ThreadCount;
            public float PrivateByte64GBytes;
            public float VirtualByte64GBytes;

            public ProcessInfoSample(long AcitveThreadCount = 0,
            long ThreadCount = 0,
            float PrivateByte64GBytes = 0.0f,
            float VirtualByte64GBytes = 0.0f)
            {
                ///Init ProcessInfoSample
                this.AcitveThreadCount = AcitveThreadCount;
                this.ThreadCount = ThreadCount;
                this.PrivateByte64GBytes = PrivateByte64GBytes;
                this.VirtualByte64GBytes = VirtualByte64GBytes;
            }
        }
        #endregion
        */

        #region Save to Excel
        public static void SaveGridToExcel(DataGridView dgv, string fileName)
        {
            DataTable dt = new DataTable();
            string temp = "";
            //Adding the Columns
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dt.Columns.Add(column.Name, column.ValueType == null ? temp.GetType() : column.ValueType);
            }

            //Adding the Rows
            string dataPropertyName = string.Empty;
            foreach (DataGridViewRow row in dgv.Rows)
            {

                dt.Rows.Add();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    dataPropertyName = dgv.Columns[cell.ColumnIndex].DataPropertyName;
                    if (string.IsNullOrEmpty(dataPropertyName))
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
                    else
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = BindProperty(row.DataBoundItem, dgv.Columns[cell.ColumnIndex].DataPropertyName);// cell.Value;//(cell.Value==null?"": cell.Value.ToString());
                }
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, DateTime.Now.ToString("yyyy_MM_dd_HHmmss"));
                wb.SaveAs(fileName);
            }
            MessageBox.Show("File Saved Successfully!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static string BindProperty(object property, string propertyName)
        {
            string returnVal = string.Empty;
            if (propertyName.Contains("."))
            {
                PropertyInfo[] arrayProperties = property.GetType().GetProperties();
                string leftPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                foreach (PropertyInfo pInfo in arrayProperties)
                {
                    if (pInfo.Name == leftPropertyName)
                    {
                        returnVal = BindProperty(pInfo.GetValue(property, null), propertyName.Substring(propertyName.IndexOf(".") + 1));
                        break;
                    }
                }

            }
            else
            {
                PropertyInfo propertyInfo;
                Type propertyType;

                propertyType = property.GetType();
                propertyInfo = propertyType.GetProperty(propertyName);
                returnVal = (propertyInfo.GetValue(property, null)).ToString();
            }
            return returnVal;
        }
        #endregion
    }
}
