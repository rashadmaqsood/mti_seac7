using DLMS;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SharedCode.Common
{
    public static class Common_PCL
    {
        public static readonly string DateTimeFormat = "dd/MM/yyyy HH:mm:ss"; 
        private static ConcurrentDictionary<string, object> singletonInstaces = null;
        //internal static Color ValidatedColorScheme = Color.Black;
        //internal static Color InValidatedColorScheme = Color.Red;
        //public static Color BgColor1 = Color.LightSkyBlue;
        //public static Color BgColor2 = Color.White;
        public static float BgColorAngle = 270f;

        static Common_PCL()
        {
            //lock (Application.CurrentCulture)
            //{
                if (singletonInstaces == null)
                    singletonInstaces = new ConcurrentDictionary<string, object>();
            //}
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

        //static public bool Init_List(ListBox ls_box, List<string> ls_Items)
        //{
        //    if (ls_Items == null)
        //    {
        //        return false;
        //    }

        //    foreach (string item in ls_Items)
        //        ls_box.Items.Add(item);
        //    return true;
        //}

        //static public void TextCopy2Clipboard(TextBox txtSource)
        //{
        //    if (txtSource.Text.Length == 0)
        //    {
        //        TextBox a = new TextBox();
        //        a.Text = " ";
        //        a.Select(0, 1);
        //        a.Copy();
        //        return;
        //    }

        //    txtSource.Select(0, txtSource.Text.Length);
        //    txtSource.Copy();

        //}

        //static public UInt16 IndexofMainNode(TreeNode TN)
        //{
        //    TreeNode parent = TN.Parent;
        //    UInt16 Index_main_node = (UInt16)TN.Index;

        //    for (; parent != null; )
        //    {
        //        TN = parent;
        //        Index_main_node = (UInt16)TN.Index;
        //        parent = TN.Parent;
        //    }
        //    return Index_main_node;
        //}

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
        //static public UInt16 FindSelectedChildNode(TreeNode TN)
        //{
        //    UInt16 Index = 0xffff;

        //    TreeNodeCollection child_nodes = TN.Nodes;

        //    if (child_nodes.Count == 0)
        //        Index = (UInt16)TN.Index;

        //    return Index;
        //}

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

        //public static bool TextBox_validation(double min, double max, TextBox given_TextBox)
        //{
        //    try
        //    {

        //        double text_box_value = Convert.ToDouble(given_TextBox.Text);
        //        if (text_box_value > max || text_box_value < min || given_TextBox.Text == "")
        //        {
        //            given_TextBox.ForeColor = Color.Red;
        //            return false;
        //        }

        //        else
        //        {
        //            given_TextBox.ForeColor = Color.Black;
        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        given_TextBox.ForeColor = Color.Red;
        //        return false;
        //    }
        //    //       return true;
        //}

        //public static byte DecimalPoint_validation(TextBox given_TextBox, int left, int right, int total)
        //{
        //    string text = String.Empty;
        //    try
        //    {

        //        text = given_TextBox.Text;
        //        //verify that the text entered is Numeric
        //        //*********************************************
        //        //*********************************************

        //        try
        //        {
        //            double val = Convert.ToDouble(text);
        //        }
        //        catch
        //        {
        //            given_TextBox.ForeColor = InValidatedColorScheme;
        //            return 0;
        //        }

        //        //*********************************************
        //        //*********************************************
        //        //*********************************************

        //        int before_decimal;
        //        int after_decimal;
        //        before_decimal = text.IndexOf('.');
        //        if (before_decimal == -1)
        //        {
        //            before_decimal = 0;
        //            after_decimal = 0;
        //            return 0;
        //        }
        //        else
        //        {
        //            after_decimal = text.Length - before_decimal - 1;
        //            if (before_decimal > 16 || after_decimal > 16 || given_TextBox.Text == "" ||
        //                after_decimal > right || before_decimal > left || (after_decimal + before_decimal) > total)
        //            {
        //                given_TextBox.ForeColor = InValidatedColorScheme;
        //                return 0;
        //            }
        //            given_TextBox.ForeColor = ValidatedColorScheme;
        //            return (byte)(before_decimal * 16 + after_decimal);
        //        }
        //    }
        //    catch
        //    {
        //        given_TextBox.ForeColor = InValidatedColorScheme;
        //        return 0;
        //    }

        //}

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

        public static string GetApplicationConfigsDirectory()
        {
            try
            {
                String fileDirectoryPath = (String)SharedCode.Properties.Settings.Default["ApplicationConfigsDirectory"];
                if (!Directory.Exists(fileDirectoryPath))
                {
                    //fileDirectoryPath = Environment.CurrentDirectory + @"\Application_Configs";
                    fileDirectoryPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Application_Configs";
                    if (!Directory.Exists(fileDirectoryPath))
                        Directory.CreateDirectory(fileDirectoryPath);
                    SharedCode.Properties.Settings.Default["ApplicationConfigsDirectory"] = fileDirectoryPath;
                    SharedCode.Properties.Settings.Default.Save();
                }
                return fileDirectoryPath;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //public static String GetAccessRightsFilePath(string associationName)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(associationName))
        //            throw new Exception("Invalid Association Name");
        //        String fileName = String.Format(@"{0}\{1}.xml", GetApplicationConfigsDirectory(), associationName);
        //        return fileName;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

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
        //public static void value_to_string(double value, ref TextBox t)
        //{
        //    if (double.IsNaN(value))
        //        t.Text = "Not Access";
        //    else if (double.IsPositiveInfinity(value))
        //        t.Text = "Not Exist";
        //    else if (double.IsNegativeInfinity(value))
        //        t.Text = "Meter BUG";
        //    else t.Text = value.ToString();
        //}
        //public static void value_to_string(double value, ref Label L)
        //{
        //    if (double.IsNaN(value))
        //        L.Text = "Not Access";
        //    else if (double.IsPositiveInfinity(value))
        //        L.Text = "Not Exist";
        //    else if (double.IsNegativeInfinity(value))
        //        L.Text = "BUG";
        //    else L.Text = value.ToString();
        //    Application.DoEvents();
        //}

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




    }
}
