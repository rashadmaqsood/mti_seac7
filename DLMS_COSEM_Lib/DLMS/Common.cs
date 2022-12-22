﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Collections;
using System.Text;
using DLMS.Comm;
using System.Reflection;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using LogSystem.Shared.Common;
using LogSystem.Shared.Common.Enums;

namespace DLMS
{
    public static class DLMS_Common
    {
        public static readonly SAP_Object ManagementDevice = null;
        public static readonly SAP_Object Public = null;
        public static readonly SAP_Object Management_Client = null;

        static DLMS_Common()
        {
            /// Initialize Variables
            ManagementDevice = new SAP_Object("Management", 0x01);
            Public = new SAP_Object("Public", 0x10);
            Management_Client = new SAP_Object("Management", 0x01);
        }

        //==========================================================================
        //==========================================================================
        internal static List<SAP_Object> InitClientSAP()
        {
            SAP_Object Management_SAP = new SAP_Object("Management", 0x0001);
            SAP_Object Public_SAP = new SAP_Object("Public", 0x0010);

            List<SAP_Object> Client_Logical_Devices = new List<SAP_Object>();
            Client_Logical_Devices.Add(Management_SAP);
            Client_Logical_Devices.Add(Public_SAP);
            return Client_Logical_Devices;
        }

        #region Append Array
        //==========================================================================
        static public byte[] Append_to_End(byte[] First_Array, byte[] Second_Array)
        {
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================
        public static void Reverse(BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
        }

        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        //==========================================================================
        /// <summary>
        /// Appends second_byt at the end of first array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="second_byte"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte[] First_Array, byte second_byte)
        {
            //byte[] Second_Array = new byte[1] { second_byte };
            byte[] Appended = null;

            if (First_Array == null || First_Array.Length <= 0)
            {
                Appended = new byte[1];
                Appended[0] = second_byte;
            }
            else
            {
                Appended = new byte[First_Array.Length + 1];
                System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
                Appended[First_Array.Length] = second_byte;
            }
            //System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

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
            //byte[] First_Array = new byte[1] { First_Byte };
            //byte[] Second_Array = new byte[1] { second_byte };
            //byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            byte[] Appended = new byte[2];
            Appended[0] = First_Byte;
            Appended[1] = second_byte;

            //System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            //System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

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
            //byte[] Second_Array = new byte[1] { second_byte };
            byte[] Appended = new byte[First_Array.Length + 1];
            Appended[0] = second_byte;
            //System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
            System.Buffer.BlockCopy(First_Array, 0, Appended, 1, First_Array.Length);

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
        //========================================================================== 
        #endregion
        //==========================================================================

        /// <summary>
        /// Converts byte array to string in hex format
        /// </summary>
        /// <param name="Hex_Array"></param>
        /// <returns></returns>
        static public string ArrayToHexString(byte[] Hex_Array)
        {
            string Return_String = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            if (Hex_Array != null)
            {
                foreach (byte Value in Hex_Array)
                    stringBuilder.AppendFormat("{0:X2}", Value);

                Return_String = stringBuilder.ToString();
                return Return_String;
            }
            else return string.Empty;
        }

        /// <summary>
        /// Converts byte array to string in hex format
        /// </summary>
        /// <param name="Hex_Array"></param>
        /// <returns></returns>
        static public string ArrayToHexString(byte[] Hex_Array, int OffSet, int Count)
        {
            ArraySegment<byte> Data_Segment = new ArraySegment<byte>(Hex_Array, OffSet, Count);

            string Return_String = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            if (Hex_Array != null)
            {
                foreach (byte Value in Data_Segment.AsEnumerable<byte>())
                    stringBuilder.AppendFormat("{0:X2} ", Value);

                Return_String = stringBuilder.ToString();
                return Return_String;
            }
            else return string.Empty;
        }
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        //==========================================================================
        //==========================================================================
        /// <summary>
        /// Converts string to byte array format
        /// </summary>
        /// <param name="printableString"></param>
        /// <returns></returns>
        static public byte[] PrintableStringToByteArray(string printableString)
        {
            printableString = printableString.Replace("\\n", "\n");
            printableString = printableString.Replace("\\r", "\t");
            printableString = printableString.Replace("\\t", "\t");
            printableString = printableString.Replace("\\v", "\v");


            byte[] Return_String = new byte[printableString.Length];
            int index = 0;
            if (string.IsNullOrEmpty(printableString)) return null;
            foreach (char Value in printableString) Return_String[index++] = Convert.ToByte(Value);
            return Return_String;
        }

        //==========================================================================
        //==========================================================================
        /// <summary>
        /// Converts byte Array to Printable String format
        /// </summary>
        /// <param name="Hex_Array"></param>
        /// <returns></returns>
        public static string ArrayToPrintableString(byte[] Byte_Array)
        {
            string Return_String = string.Empty;

            StringBuilder stringBuilder = new StringBuilder();

            if (Byte_Array != null)
            {
                foreach (byte Value in Byte_Array) stringBuilder.Append(Convert.ToChar(Value));

                Return_String = stringBuilder.ToString();
                return Return_String;
            }
            else return "";
        }

        /// <summary>
        /// Converts byte Array to Printable String format
        /// </summary>
        /// <param name="Hex_Array"></param>
        /// <returns></returns>
        public static string ArrayToPrintableString(IEnumerable<byte> Byte_Array)
        {
            string Return_String = string.Empty;

            StringBuilder stringBuilder = new StringBuilder();

            if (Byte_Array != null)
            {
                foreach (byte Value in Byte_Array) stringBuilder.Append(Convert.ToChar(Value));

                Return_String = stringBuilder.ToString();
                return Return_String;
            }
            else return "";
        }

        //==========================================================================
        //==========================================================================
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
        public static byte[] String_to_Hex_array(string passed)
        {
            /// passed = Remove_White_Spaces(passed);
            /// passed = passed.ToUpper();
            /// char[] temp = passed.ToCharArray();

            List<byte> to_return = new List<byte>(1024);
            byte toAdd = 0;

            char[] data_array = passed.ToCharArray();

            string str_literals = string.Empty;
            for (int index = 0; index + 1 < data_array.Length; index += 2)
            {
                char temp = data_array[index];

                if (temp == ' ' || temp == '\x0d' || temp == '\x0a')
                {
                    index -= 1;
                    continue;
                }
                else
                {
                    string txt = string.Empty + char.ToUpper(data_array[index]) + char.ToUpper(data_array[index + 1]);

                    if (byte.TryParse(txt, System.Globalization.NumberStyles.HexNumber, null, out toAdd))
                        to_return.Add(toAdd);
                }
            }

            return to_return.ToArray();
        }

        //==========================================================================
        //==========================================================================
        #region Length_Verification

        /// <summary>
        /// Verifies length of sub-array
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="starting_index"></param>
        /// <param name="given_length"></param>
        /// <returns></returns>
        static public bool Length_Verified(byte[] arr, UInt16 starting_index, UInt16 given_length)
        {
            if ((arr.Length - starting_index) == given_length) return true;
            return false;
        }

        /// <summary>
        /// Verifies length of sub-array
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="starting_index"></param>
        /// <param name="given_length"></param>
        /// <returns></returns>
        static public bool Length_Verified(byte[] arr, int starting_index, int given_length)
        {
            if ((arr.Length - starting_index) == given_length) return true;
            return false;
        }

        /// <summary>
        /// Verifies length of sub-array
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="starting_index"></param>
        /// <param name="given_length"></param>
        /// <returns></returns>
        static public bool Length_Verified(byte[] arr, int starting_index, int given_length, bool lengthExactMatch = true)
        {
            int RemainingDataLen = arr.Length - starting_index;

            if (lengthExactMatch)
            {
                if (RemainingDataLen == given_length) return true;
                else
                    return false;
            }
            else
            {
                if (RemainingDataLen >= given_length) return true;
                else
                    return false;
            }
        }

        #endregion

        #region Array Manupulation
        //==========================================================================
        //==========================================================================
        /// <summary>
        /// Resizes Destination Array and Copies All Elements of Source Array into it
        /// </summary>
        /// <param name="Source_array"></param>
        /// <param name="Destination_Array"></param>
        static public void Byte_Array_Copier(byte[] Source_array, ref byte[] Destination_Array, UInt16 Source_Index)
        {
            if (Destination_Array == null)
            {
                Destination_Array = new byte[1];
            }
            Array.Resize<byte>(ref Destination_Array, (int)(Source_array.Length - Source_Index));
            Array.Copy(Source_array, Source_Index, Destination_Array, 0, Destination_Array.Length);
        }
        /// <summary>
        /// Resizes Destination Array and Copies All Elements of Source Array into it
        /// </summary>
        /// <param name="Source_array"></param>
        /// <param name="Destination_Array"></param>
        static public void Byte_Array_Copier(byte[] Source_array, ref byte[] Destination_Array, int Source_Index)
        {
            if (Destination_Array == null)
            {
                Destination_Array = new byte[1];
            }
            Array.Resize<byte>(ref Destination_Array, (int)(Source_array.Length - Source_Index));
            Array.Copy(Source_array, Source_Index, Destination_Array, 0, Destination_Array.Length);
        }
        //==========================================================================
        //==========================================================================
        /// <summary>
        /// Resizes Destination Array and Copies a Number of Elements of Source Array into it
        /// </summary>
        /// <param name="Source_array"></param>
        /// <param name="Destination_Array"></param>
        /// <param name="Source_Index"></param>
        static public void Byte_Array_Copier(byte[] Source_array, ref byte[] Destination_Array,
            UInt16 Source_Index, UInt16 Elements)
        {
            if (Destination_Array == null)
            {
                Destination_Array = new byte[1];
            }
            Array.Resize<byte>(ref Destination_Array, (Elements));
            Array.Copy(Source_array, (int)Source_Index, Destination_Array, 0, Elements);
        }

        //==========================================================================
        public static void Data_Array_resizer(ref byte[] Data_Array, int Starting_index)
        {
            if (Starting_index == 0 || Starting_index >= Data_Array.Length)
            {
                return;
            }
            byte[] temp_array = new byte[Data_Array.Length - Starting_index];
            Array.Copy(Data_Array, Starting_index, temp_array, 0, temp_array.Length);
            Data_Array = temp_array;
        }
        //==========================================================================

        /// <summary>
        /// Resizes Destination Array and Copies a Number of Elements of Source Array into it
        /// </summary>
        /// <param name="Source_array"></param>
        /// <param name="Destination_Array"></param>
        /// <param name="Source_Index"></param>
        static public void Byte_Array_Copier(byte[] Source_array, ref byte[] Destination_Array,
            int Source_Index, int Elements)
        {
            if (Destination_Array == null)
            {
                Destination_Array = new byte[1];
            }
            Array.Resize<byte>(ref Destination_Array, (Elements));
            Array.Copy(Source_array, (int)Source_Index, Destination_Array, 0, Elements);
        }

        //==========================================================================
        static public byte Compare_Arrays(byte[] arr1, byte[] arr2)
        {
            if (arr1 == null || arr2 == null || arr1.Length != arr2.Length)
            {
                return 0;
            }
            if (arr1.SequenceEqual<byte>(arr2))
                return 1;
            else
                return 0;
            //for (local_counter = 0; local_counter < arr1.Length; local_counter++)
            //{
            //    if (arr1[local_counter] != arr2[local_counter]) return 0;
            //}

            //return 1;
        }

        static public bool Compare_Array(byte[] arr1, byte[] arr2)
        {
            try
            {
                return arr1.SequenceEqual<byte>(arr2);
            }
            catch (Exception)
            {
                return false;
            }
        }
        //==========================================================================

        #endregion

        //==========================================================================
        //==========================================================================
        static public void AddToSAPTable(ref Base_Class[] Table, Base_Class New_Entry, ref ushort[] Index_Array)
        {
            try
            {
                //Code To Verifty No Duplicate Indices Assigned To OBIS Code
                if (Index_Array != null)
                {

                    if (Index_Array.Contains<ushort>((ushort)New_Entry.INDEX) && New_Entry.INDEX != Get_Index.Dummy)
                    {
                        Base_Class x = Table[(ushort)New_Entry.INDEX];
                        throw new DLMSException(String.Format("OBIS Code {0} Index duplicates with {1} to be added in SAP Table,Cannot create SAP", x.INDEX, New_Entry.INDEX));
                    }

                    foreach (Base_Class x in Table)
                    {
                        if (x != null && New_Entry != null &&
                            x.OBIS_CODE != null && New_Entry.OBIS_CODE != null
                            && New_Entry.OBIS_CODE.Length == 6)
                        {
                            bool matchFlag = true;
                            // razaahsan modification
                            //
                            for (int index = 0; index < 6; index++)
                                if (x.OBIS_CODE[index] != New_Entry.OBIS_CODE[index])
                                {
                                    matchFlag = false;
                                    break;
                                }
                            if (matchFlag)
                                throw new DLMSException(String.Format("OBIS Code {0} duplicates with {1} to be added in SAP Table,Cannot create SAP", x.INDEX, New_Entry.INDEX));
                        }
                        else if (x != null)
                            throw new DLMSException("Invalid OBIS Code to be added,Cannot create SAP Table_AddToSAPTable");
                    }

                    Array.Resize(ref Index_Array, Index_Array.Length + 1);
                    // insert index at last position
                    Index_Array[Index_Array.Length - 1] = (ushort)New_Entry.INDEX;

                }       /// If first obis code is being inserted
                else
                {
                    Index_Array = new ushort[1];
                    Index_Array[0] = (ushort)New_Entry.INDEX;
                }
                Table[(int)New_Entry.INDEX] = New_Entry;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //==========================================================================
        //==========================================================================
        static public UInt16 IndexOf(ref List<byte[]> All_Codes, byte[] CodeToFind)
        {
            UInt16 Index = ushort.MaxValue;
            UInt16 loop_count1 = (UInt16)All_Codes.Capacity;
            byte array_traverse;
            bool found = false;
            foreach (byte[] Obis_Code in All_Codes)
            {
                array_traverse = 0;
                if (Obis_Code == null)
                    continue;

                foreach (byte identifier in CodeToFind)
                {
                    if (Obis_Code[array_traverse] != identifier)
                    {
                        break;
                    }
                    else
                    {
                        if (array_traverse == 5) found = true;
                    }
                    array_traverse++;
                    if (found == true)
                    {
                        Index = (UInt16)All_Codes.IndexOf(Obis_Code);
                        return Index;
                    }
                }
            }

            return Index;
        }

        #region Save_Data_SubFunction
        //==========================================================================
        static public void Save_Data_Subfunction(byte[] Source_Array, ref UInt16 Source_Index, ref ulong Destination)
        {
            // enumerated data type
            ulong temp_ulong = (ulong)(((long)Source_Array[Source_Index + 7]) + ((long)Source_Array[Source_Index + 6] << 8) +
                        ((long)Source_Array[Source_Index + 5] << 16) + ((long)Source_Array[Source_Index + 4] << 24) +
                        ((long)Source_Array[Source_Index + 3] << 32) + ((long)Source_Array[Source_Index + 2] << 40) +
                        ((long)(Source_Array[Source_Index + 1] << 48) + ((long)Source_Array[Source_Index] << 56)));
            Source_Index += 7;
            Destination = temp_ulong;
        }
        //==========================================================================
        //==========================================================================
        static public void Save_Data_Subfunction(byte[] Source_Array, ref int Source_Index, ref UInt16 Destination)
        {
            // enumerated data type
            UInt16 temp_ulong;
            temp_ulong = (UInt16)((Source_Array[Source_Index + 1]) + (Source_Array[Source_Index] << 8));

            Source_Index += 2;
            Destination = temp_ulong;
        }
        //==========================================================================
        //==========================================================================
        static public void Save_Data_Subfunction(byte[] Source_Array, ref int Source_Index, ref Int16 Destination)
        {
            // enumerated data type
            Int16 temp_ulong;
            temp_ulong = (Int16)((Source_Array[Source_Index + 1]) + (Source_Array[Source_Index] << 8));

            Source_Index += 2;
            Destination = temp_ulong;
        }
        //==========================================================================
        //==========================================================================
        static public void Save_Data_Subfunction(byte[] Source_Array, ref int Source_Index, ref byte[] Destination)
        {
            // Manual
            Source_Index++;

            int str_length = BasicEncodeDecode.Decode_Length(Source_Array, ref Source_Index);

            Destination = new byte[str_length];
            // String Traverse
            Array.Copy(Source_Array, Source_Index, Destination, 0, str_length);
            Source_Index += str_length;

            //for (byte str_traverse = 0; str_traverse < str_length; )
            //{
            //    Destination[str_traverse++] = Source_Array[Source_Index++];
            //}
        }
        #endregion

        #region ArraySegment RelatedMethods

        public static ArraySegment<T> GetSegment<T>(this T[] array, int from, int count)
        {
            return new ArraySegment<T>(array, from, count);
        }

        public static ArraySegment<T> GetSegment<T>(this T[] array, int from)
        {
            return GetSegment(array, from, array.Length - from);
        }

        public static ArraySegment<T> GetSegment<T>(this T[] array)
        {
            return new ArraySegment<T>(array);
        }

        public static IEnumerable<T> AsEnumerable<T>(this ArraySegment<T> arraySegment)
        {
            return arraySegment.Array.Skip(arraySegment.Offset).Take(arraySegment.Count);
        }

        public static T[] ToArray<T>(this ArraySegment<T> arraySegment)
        {
            T[] array = new T[arraySegment.Count];
            Array.Copy(arraySegment.Array, arraySegment.Offset, array, 0, arraySegment.Count);
            return array;
        }

        #endregion

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

        public static bool IsIODataTimeOut(this Exception ex)
        {
            string errMessage = string.Empty;
            try
            {
                errMessage = ex.Message;
                if ((ex is IOException ||
                     ex is SocketException) &&
                    !string.IsNullOrEmpty(errMessage) &&
                    errMessage.Contains("IO Operation Connection timed out or remote host has failed to respond"))
                {
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }


        /// <summary>
        /// Reserved For Internal Use
        /// </summary>
        /// <param name="value"></param>
        /// <param name="BitMask"></param>
        /// <param name="val"></param>
        public static void SetBits(ref byte value, byte BitMask, bool val)
        {
            value &= (byte)~BitMask;
            /// Set bit.
            if (val)
            {
                value |= BitMask;
            }
            else /// Clear bit.
            {
                value &= (byte)~BitMask;
            }
        }

        /// <summary>
        /// Is Bit Set
        /// </summary>
        /// <param name="value"></param>
        /// <param name="BitMask"></param>
        /// <returns></returns>
        public static bool GetBits(byte value, int BitMask)
        {
            return (value & BitMask) != 0;
        }

        #region PreciseDelay

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// It has significant Performance Penalty
        /// </summary>
        /// <param name="durationTicks">Milli-Second Thread Duration</param>
        public static void PreciseDelay(long durationTicks)
        {
            // Static method to initialize 
            // and start stopwatch
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < durationTicks)
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(05);
                System.Threading.Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// It has significant Performance Penalty
        /// </summary>
        /// <param name="durationTicks">Milli-Second Thread Duration</param>
        public static void PreciseDelay(double durationTicks)
        {
            // Static method to initialize 
            // and start stopwatch
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < durationTicks)
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(05);
                System.Threading.Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// It has significant Performance Penalty
        /// </summary>
        /// <param name="duration">Thread Duration Delay</param>
        public static void PreciseDelay(TimeSpan duration)
        {
            // Static method to initialize 
            // and start stopwatch
            Stopwatch sw;
            sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < duration.TotalMilliseconds)
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(05);
                System.Threading.Thread.Sleep(50);
            }
        }

        #endregion

        #region Delay

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// </summary>
        /// <param name="durationTicks">Milli-Second Thread Duration</param>
        public static void Delay(long durationTicks)
        {
            // Static method to initialize 
            // and TimeStamp
            TimeSpan currentTimeStamp = DateTime.Now.TimeOfDay;

            do
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(10000);
                System.Threading.Thread.Sleep(50);
            }
            while (DateTime.Now.TimeOfDay.Subtract(currentTimeStamp).
                TotalMilliseconds < durationTicks);
        }

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// </summary>
        /// <param name="durationTicks">Milli-Second Thread Duration</param>
        public static void Delay(double durationTicks)
        {
            // Static method to initialize 
            // and TimeStamp
            TimeSpan currentTimeStamp = DateTime.Now.TimeOfDay;

            do
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(10000);
                System.Threading.Thread.Sleep(50);
            }
            while (DateTime.Now.TimeOfDay.Subtract(currentTimeStamp).
                TotalMilliseconds < durationTicks);
        }

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// </summary>
        /// <param name="duration">Thread Duration Delay</param>
        public static void Delay(TimeSpan duration)
        {
            // Static method to initialize 
            // and TimeStamp
            TimeSpan currentTimeStamp = DateTime.Now.TimeOfDay;

            do
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(10000);
                System.Threading.Thread.Sleep(50);
            }
            while (DateTime.Now.TimeOfDay.Subtract(currentTimeStamp).
                TotalMilliseconds < duration.TotalMilliseconds);
        }

        #endregion

        #region Delay

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// It has significant Performance Penalty
        /// </summary>
        /// <param name="durationTicks">Milli-Second Thread Duration</param>
        public static void DelayUntil(Func<bool> Criteria, long durationTicks)
        {
            // Static method to initialize 
            // and start stopwatch
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < durationTicks
                   && !Criteria.Invoke())
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(05);
                System.Threading.Thread.Sleep(05);
            }
        }




        #endregion

        #region GloDLMSCommmand


        /// <summary>
        /// Get used glo message.
        /// </summary>
        /// <param name="command">Executed command.</param>
        /// <returns>Integer value of glo message.</returns>
        public static byte GetGloDLMSCommmandTAG(DLMSCommand cmd)
        {
            switch (cmd)
            {
                case DLMSCommand.ReadRequest:
                    cmd = DLMSCommand.GloReadRequest;
                    break;
                case DLMSCommand.GetRequest:
                    cmd = DLMSCommand.GloGetRequest;
                    break;
                case DLMSCommand.WriteRequest:
                    cmd = DLMSCommand.GloWriteRequest;
                    break;
                case DLMSCommand.SetRequest:
                    cmd = DLMSCommand.GloSetRequest;
                    break;
                case DLMSCommand.MethodRequest:
                    cmd = DLMSCommand.GloMethodRequest;
                    break;
                case DLMSCommand.ReadResponse:
                    cmd = DLMSCommand.GloReadResponse;
                    break;
                case DLMSCommand.GetResponse:
                    cmd = DLMSCommand.GloGetResponse;
                    break;
                case DLMSCommand.WriteResponse:
                    cmd = DLMSCommand.GloWriteResponse;
                    break;
                case DLMSCommand.SetResponse:
                    cmd = DLMSCommand.GloSetResponse;
                    break;
                case DLMSCommand.MethodResponse:
                    cmd = DLMSCommand.GloMethodResponse;
                    break;
                case DLMSCommand.EventNotificationRequest:
                    cmd = DLMSCommand.GloEventNotificationRequest;
                    break;
                default:
                    throw new DLMSException("Invalid GLO Command.");
            }
            return (byte)cmd;
        }


        /// <summary>
        /// Get used glo message.
        /// </summary>
        /// <param name="command">Executed command.</param>
        /// <returns>Integer value of glo message.</returns>
        public static bool IsGloDLMSCommandTAG(DLMSCommand cmd)
        {
            bool isGloTAG = false;
            switch (cmd)
            {
                case DLMSCommand.GloReadRequest:
                /// case DLMSCommand.ReadRequest:

                case DLMSCommand.GloGetRequest:
                /// case DLMSCommand.GetRequest:

                case DLMSCommand.GloWriteRequest:
                /// case DLMSCommand.WriteRequest:

                case DLMSCommand.GloSetRequest:
                /// cmd = DLMSCommand.SetRequest:

                case DLMSCommand.GloMethodRequest:
                /// case DLMSCommand.MethodRequest:

                case DLMSCommand.GloReadResponse:
                /// case DLMSCommand.ReadResponse:

                case DLMSCommand.GloGetResponse:
                /// case DLMSCommand.GetResponse:

                case DLMSCommand.GloWriteResponse:
                /// case DLMSCommand.WriteResponse:

                case DLMSCommand.GloSetResponse:
                /// case DLMSCommand.SetResponse:

                case DLMSCommand.GloEventNotificationRequest:
                case DLMSCommand.GloMethodResponse:
                    /// case DLMSCommand.MethodResponse:
                    {
                        isGloTAG = true;
                        break;
                    }
                default:
                    ///throw new DLMSException("Invalid GLO Command.");
                    isGloTAG = false;
                    break;
            }
            return isGloTAG;
        }

        #endregion

        #region DecodeAnyObject Members Implementation

        public static double Decode_Any(Base_Class arg, byte Class_ID)
        {
            try
            {
                // ushort Class_ID = arg.Class_ID;
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        temp = double.NaN;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        temp = double.PositiveInfinity;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NegativeInfinity;

                    return temp;
                }
                else if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        temp = double.NaN;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        temp = double.PositiveInfinity;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NegativeInfinity;

                    return temp;
                }
                else if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        temp = double.NaN;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        temp = double.PositiveInfinity;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NegativeInfinity;

                    return temp;
                }
                else
                    throw new InvalidOperationException(String.Format("ClassId {0} not supported", Class_ID));

                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static double Decode_Any(Base_Class arg, byte Class_ID, ref StDateTime TimeStamp)
        {
            TimeStamp = null;

            try
            {
                // ushort Class_ID = arg.Class_ID;
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        temp = double.NaN;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        temp = double.PositiveInfinity;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NegativeInfinity;
                    return temp;
                }
                else if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        temp = double.NaN;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        temp = double.PositiveInfinity;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NegativeInfinity;
                    return temp;

                }
                else if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;

                    double temp = Convert.ToDouble(temp_obj.Value);

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        temp = double.NaN;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        temp = double.PositiveInfinity;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        temp = double.NegativeInfinity;

                    if (temp_obj.GetAttributeDecodingResult(0x05) == DecodingResult.Ready)
                        TimeStamp = temp_obj.Date_Time_Stamp;


                    return temp;
                }
                else
                    throw new InvalidOperationException(String.Format("ClassId {0} not supported", Class_ID));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property)
        {
            bool isSuccess = false;
            ValueType value = double.PositiveInfinity;

            try
            {
                // ushort Class_ID = arg.Class_ID;
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    value = temp_obj.Value;
                    isSuccess = true;

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        isSuccess = false;

                }
                else if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    value = temp_obj.Value;
                    isSuccess = true;

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        isSuccess = false;
                }
                else if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    value = temp_obj.Value;
                    isSuccess = true;

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        isSuccess = false;
                }
                else
                    throw new InvalidOperationException(String.Format("ClassId {0} not supported", Class_ID));

                #region Data Type Validation

                if (!(arg.DecodingType == DataTypes._A03_boolean ||
                     arg.DecodingType == DataTypes._A05_double_long ||
                     arg.DecodingType == DataTypes._A06_double_long_unsigned ||
                     arg.DecodingType == DataTypes._A07_floating_point ||

                     arg.DecodingType == DataTypes._A0D_bcd ||
                     arg.DecodingType == DataTypes._A0F_integer ||
                     arg.DecodingType == DataTypes._A10_long ||
                     arg.DecodingType == DataTypes._A11_unsigned ||
                     arg.DecodingType == DataTypes._A12_long_unsigned ||

                     arg.DecodingType == DataTypes._A14_long_64 ||
                     arg.DecodingType == DataTypes._A15_long_64_unsigned ||
                     arg.DecodingType == DataTypes._A16_enum ||

                     arg.DecodingType == DataTypes._A23_Float32 ||
                     arg.DecodingType == DataTypes._A24_Float64))
                {
                    throw new InvalidCastException(String.Format("DataType {0} not supported", arg.DecodingType.ToString()));
                }

                #endregion

                DataContainer_Class_obj.setDataMemberByName(Data_Property, value);

                return isSuccess;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Exec TryDecode_Any {0},", ex.Message));

                value = double.PositiveInfinity;
                if (DataContainer_Class_obj != null)
                    DataContainer_Class_obj.setDataMemberByName(Data_Property, value);
                // throw;
            }
            return isSuccess;
        }

        public static bool TryDecode_Any(Base_Class arg, byte Class_ID, IDecodeAnyObject DataContainer_Class_obj, string Data_Property, string CaptureTimeStamp_DataProperty)
        {
            bool isSuccess = false;
            ValueType value = double.NaN;
            StDateTime dateTimeRaw = null;
            DateTime dateTime = DateTime.MinValue;

            try
            {
                // ushort Class_ID = arg.Class_ID;
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    value = temp_obj.Value;
                    isSuccess = true;

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        isSuccess = false;

                }
                else if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    value = temp_obj.Value;
                    isSuccess = true;

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        isSuccess = false;
                }
                else if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    value = temp_obj.Value;
                    isSuccess = true;

                    if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.NoAccess)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) == DecodingResult.DataNotPresent)
                        isSuccess = false;
                    else if (temp_obj.GetAttributeDecodingResult(0x02) != DecodingResult.Ready)
                        isSuccess = false;

                    dateTime = DateTime.MinValue;

                    if (temp_obj.GetAttributeDecodingResult(0x04) == DecodingResult.NoAccess)
                        dateTimeRaw = temp_obj.Date_Time_Stamp;

                    // DateTimeRaw Convert_Function
                    if (dateTimeRaw != null && dateTimeRaw.IsDateTimeConvertible)
                    {
                        dateTime = dateTimeRaw.GetDateTime();
                    }
                    else if (dateTimeRaw != null && dateTimeRaw.IsDateConvertible)
                    {
                        dateTime = dateTimeRaw.GetDate();
                    }
                    else if (dateTimeRaw != null && dateTimeRaw.IsTimeConvertible)
                    {
                        dateTime = DateTime.MinValue.Add(dateTimeRaw.GetTime());
                    }
                }
                else
                    throw new InvalidOperationException(String.Format("ClassId {0} not supported", Class_ID));

                #region Data Type Validation

                if (!(arg.DecodingType == DataTypes._A03_boolean ||
                     arg.DecodingType == DataTypes._A05_double_long ||
                     arg.DecodingType == DataTypes._A06_double_long_unsigned ||
                     arg.DecodingType == DataTypes._A07_floating_point ||

                     arg.DecodingType == DataTypes._A0D_bcd ||
                     arg.DecodingType == DataTypes._A0F_integer ||
                     arg.DecodingType == DataTypes._A10_long ||
                     arg.DecodingType == DataTypes._A11_unsigned ||
                     arg.DecodingType == DataTypes._A12_long_unsigned ||

                     arg.DecodingType == DataTypes._A14_long_64 ||
                     arg.DecodingType == DataTypes._A15_long_64_unsigned ||
                     arg.DecodingType == DataTypes._A16_enum ||

                     arg.DecodingType == DataTypes._A23_Float32 ||
                     arg.DecodingType == DataTypes._A24_Float64))
                {
                    throw new InvalidCastException(String.Format("DataType {0} not supported", arg.DecodingType.ToString()));
                }

                #endregion

                DataContainer_Class_obj.setDataMemberByName(Data_Property, value);
                DataContainer_Class_obj.setDataMemberByName(CaptureTimeStamp_DataProperty, dateTime);

                return isSuccess;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                System.Diagnostics.Debug.WriteLineIf(true, string.Format("Error occurred while Exec TryDecode_Any {0},", ex.Message));

                value = double.PositiveInfinity;

                if (DataContainer_Class_obj != null)
                {
                    DataContainer_Class_obj.setDataMemberByName(Data_Property, value);
                    DataContainer_Class_obj.setDataMemberByName(CaptureTimeStamp_DataProperty, DateTime.MinValue);
                }
                // throw;
            }

            return isSuccess;
        }

        public static string Decode_Any_string(Base_Class arg, byte Class_ID)
        {
            string temp = "---"; //string.Empty;

            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    byte[] dtArray = (byte[])temp_obj.Value_Array;

                    if (dtArray != null)
                        temp = (ASCIIEncoding.ASCII.GetString(dtArray));
                    else
                    {
                        temp = temp_obj.Value.ToString();
                    }
                }

                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    byte[] dtArray = (byte[])temp_obj.Value_Array;

                    if (dtArray != null && dtArray.Length > 0)
                        temp = (ASCIIEncoding.ASCII.GetString(dtArray));
                }
            }
            catch (Exception)
            {
                temp = "---"; // string.Empty;
            }

            return temp;
        }

        public static byte[] Decode_Any_ByteArray(Base_Class arg, byte Class_ID)
        {
            byte[] dtArray = null;

            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    dtArray = (byte[])temp_obj.Value_Array;
                }
                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    dtArray = (byte[])temp_obj.Value_Array;
                }
            }
            catch (Exception)
            {
                dtArray = null;
            }

            return dtArray;
        }

        #region DataMemberByName

        public static object getDataMemberByName(this IDecodeAnyObject DataContainerObj, string Data_Property)
        {
            object value = null;
            try
            {
                PropertyInfo prop = null;
                PropertyInfo[] Props = null;

                FieldInfo fprop = null;
                FieldInfo[] fProps = null;

                #region prop,fprop

                fProps = DataContainerObj.GetType().GetFields();

                if (fProps != null)
                    fprop = fProps.FirstOrDefault(p =>
                    {
                        if (p != null && p.IsPublic && string.Equals(p.Name, Data_Property))
                            return true;
                        else
                            return p.GetCustomAttributes(typeof(DataMemberAttribute), false)
                             .OfType<DataMemberAttribute>().Any(x => x != null && string.Equals(x.Name, Data_Property));
                    });

                if (fprop == null)
                {
                    Props = DataContainerObj.GetType().GetProperties();

                    if (Props != null)
                        prop = Props.FirstOrDefault(p =>
                        {
                            if (p != null && p.CanWrite && string.Equals(p.Name, Data_Property))
                                return true;
                            else
                                return p.GetCustomAttributes(typeof(DataMemberAttribute), false)
                                 .OfType<DataMemberAttribute>().Any(x => x != null && string.Equals(x.Name, Data_Property));
                        });
                }

                if (prop == null && fprop == null)
                    throw new ArgumentException(string.Format("{0} Not Exist in Type", Data_Property, DataContainerObj.GetType()));

                #endregion
                #region Data Conversion

                Type TargetType = null;
                if (fprop != null)
                    TargetType = fprop.FieldType;
                else if (prop != null)
                    TargetType = prop.PropertyType;

                // TypeCode typeCode = Type.GetTypeCode(TargetType);
                // Object ConvertedValue = Convert.ChangeType(value, typeCode);

                #endregion
                #region SetValue

                if (fprop != null)
                {
                    value = fprop.GetValue(DataContainerObj);
                }
                else if (prop != null && prop.CanWrite)
                {
                    value = prop.GetValue(DataContainerObj, null);
                }
                else if (!prop.CanRead)
                    throw new ArgumentException(string.Format("{0} is not Readable", Data_Property));

                #endregion

            }
            catch
            {
                throw;
            }
            return value;
        }

        public static void setDataMemberByName(this IDecodeAnyObject DataContainerObj, string Data_Property, object value)
        {
            try
            {
                PropertyInfo prop = null;
                PropertyInfo[] Props = null;

                FieldInfo fprop = null;
                FieldInfo[] fProps = null;

                #region prop,fprop

                fProps = DataContainerObj.GetType().GetFields();

                if (fProps != null)
                    fprop = fProps.FirstOrDefault(p =>
                    {
                        if (p != null && p.IsPublic && string.Equals(p.Name, Data_Property))
                            return true;
                        else
                            return p.GetCustomAttributes(typeof(DataMemberAttribute), false)
                             .OfType<DataMemberAttribute>().Any(x => x != null && string.Equals(x.Name, Data_Property));
                    });

                if (fprop == null)
                {
                    Props = DataContainerObj.GetType().GetProperties();

                    if (Props != null)
                        prop = Props.FirstOrDefault(p =>
                        {
                            if (p != null && p.CanWrite && string.Equals(p.Name, Data_Property))
                                return true;
                            else
                                return p.GetCustomAttributes(typeof(DataMemberAttribute), false)
                                 .OfType<DataMemberAttribute>().Any(x => x != null && string.Equals(x.Name, Data_Property));
                        });
                }

                if (prop == null && fprop == null)
                    throw new ArgumentException(string.Format("{0} Not Exist in Type", Data_Property, DataContainerObj.GetType()));

                #endregion
                #region Data Conversion

                Type TargetType = null;
                if (fprop != null)
                    TargetType = fprop.FieldType;
                else if (prop != null)
                    TargetType = prop.PropertyType;

                TypeCode typeCode = Type.GetTypeCode(TargetType);
                Object ConvertedValue = Convert.ChangeType(value, typeCode);

                #endregion
                #region SetValue

                if (fprop != null)
                {
                    fprop.SetValue(DataContainerObj, ConvertedValue);
                }
                else if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(DataContainerObj, ConvertedValue, null);
                }
                else if (!prop.CanWrite)
                    throw new ArgumentException(string.Format("{0} is not Writable", Data_Property));

                #endregion

            }
            catch
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region Store_Load Data Objects

        public static void InitData_BaseClass(this Base_Class SapEntity)
        {
            SapEntity.ResetAttributeDecodingResults();
            if (SapEntity is Class_1)
            {
                (SapEntity as Class_1).Value_Array = null;
                (SapEntity as Class_1).Value_Obj = null;
                (SapEntity as Class_1).Value = double.NaN;
                (SapEntity as Class_1).BitLength = 0;
            }
            else if (SapEntity is Class_3)
            {
                (SapEntity as Class_3).Value_Array = null;
                (SapEntity as Class_3).Value = double.NaN;
            }
            else if (SapEntity is Class_4)
            {
                (SapEntity as Class_4).Value = double.NaN;
                (SapEntity as Class_4).Date_Time_Stamp = null;
            }
            else if (SapEntity is Class_5)
            {
                (SapEntity as Class_5).LValue = double.NaN;
                (SapEntity as Class_5).CValue = double.NaN;

                (SapEntity as Class_5).CValue_Array = null;
                (SapEntity as Class_5).LValue_Array = null;
            }
        }

        public static object StoreData_BaseClass(this Base_Class Data_Obj, byte AttributeIndex)
        {
            Object ValueToReturn = null;
            try
            {
                if (Data_Obj == null)
                    throw new ArgumentNullException("Base_Class");

                #region Class_1 Data

                if (Data_Obj is Class_1)
                {
                    // Store Value Attribute
                    if (AttributeIndex == 0x02 &&
                        Data_Obj.GetAttributeDecodingResult(AttributeIndex) == DecodingResult.Ready)
                    {
                        #region Integral_Value

                        if ((Data_Obj as Class_1).Value != null &&
                            Convert.ToDouble((Data_Obj as Class_1).Value) != double.NaN)
                        {
                            ValueToReturn = (Data_Obj as Class_1).Value;
                        }

                        #endregion
                        #region Complex_Object

                        else if ((Data_Obj as Class_1).Value_Obj != null)
                        {
                            ValueToReturn = (Data_Obj as Class_1).Value_Obj;
                        }

                        #endregion
                        #region Value_Array

                        else if ((Data_Obj as Class_1).Value_Array != null &&
                                 (Data_Obj as Class_1).Value_Array.Length > 0)
                        {

                            if ((Data_Obj as Class_1).Value_Array is byte[] &&
                                (Data_Obj as Class_1).BitLength > 0)
                            {
                                byte[] flagsRaw = (Data_Obj as Class_1).Value_Array as byte[];
                                BitArray flags = new BitArray(flagsRaw) { Length = (Data_Obj as Class_1).BitLength };

                                ValueToReturn = flags;
                            }
                            else if ((Data_Obj as Class_1).Value_Array is byte[])
                            {
                                byte[] DataRaw = (Data_Obj as Class_1).Value_Array as byte[];

                                ValueToReturn = DataRaw;
                            }
                            else
                            {
                                ValueToReturn = (Data_Obj as Class_1).Value_Array;
                            }
                        }

                        #endregion
                    }
                }

                #endregion
                #region Class_3 Register

                else if (Data_Obj is Class_3)
                {
                    // Store Value Attribute
                    if (AttributeIndex == 0x02 &&
                        Data_Obj.GetAttributeDecodingResult(AttributeIndex) == DecodingResult.Ready)
                    {
                        #region Integral_Value

                        if ((Data_Obj as Class_3).Value != null &&
                            Convert.ToDouble((Data_Obj as Class_3).Value) != double.NaN)
                        {
                            ValueToReturn = (Data_Obj as Class_3).Value;
                        }

                        #endregion
                        #region Value_Array

                        else if ((Data_Obj as Class_3).Value_Array != null &&
                                 (Data_Obj as Class_3).Value_Array.Length > 0)
                        {
                            if ((Data_Obj as Class_3).Value_Array is byte[])
                            {
                                byte[] DataRaw = (Data_Obj as Class_3).Value_Array as byte[];

                                ValueToReturn = DataRaw;
                            }
                            else
                            {
                                ValueToReturn = (Data_Obj as Class_3).Value_Array;
                            }
                        }

                        #endregion
                    }

                    // Store Scaler Unit
                    if (AttributeIndex == 0x03 &&
                        Data_Obj.GetAttributeDecodingResult(AttributeIndex) == DecodingResult.Ready)
                    {
                        String Output_FRMT = "{0} Scl:{1}";
                        ValueToReturn = String.Format(Output_FRMT, (Data_Obj as Class_3).Unit, (Data_Obj as Class_3).scaler);
                    }
                }

                #endregion
                #region Class_4 MDI Register

                else if (Data_Obj is Class_4)
                {
                    // Store Value Attribute
                    if (AttributeIndex == 0x02 &&
                        Data_Obj.GetAttributeDecodingResult(AttributeIndex) == DecodingResult.Ready)
                    {
                        if ((Data_Obj as Class_4).Value != double.NaN)
                        {
                            ValueToReturn = (Data_Obj as Class_4).Value;
                        }
                    }

                    // Store Scaler Unit
                    if (AttributeIndex == 0x03 &&
                        Data_Obj.GetAttributeDecodingResult(AttributeIndex) == DecodingResult.Ready)
                    {
                        String Output_FRMT = "{0} Scl:{1}";
                        ValueToReturn = String.Format(Output_FRMT, (Data_Obj as Class_4).Unit, (Data_Obj as Class_4).scaler);
                    }

                    // Store Status
                    if (AttributeIndex == 0x04 &&
                        Data_Obj.GetAttributeDecodingResult(AttributeIndex) == DecodingResult.Ready)
                    {
                        ValueToReturn = null;  // (Data_Obj as Class_4).Date_Time_Stamp.ToString();
                    }

                    // Store Capture Date Time
                    if (AttributeIndex == 0x05 &&
                        Data_Obj.GetAttributeDecodingResult(AttributeIndex) == DecodingResult.Ready)
                    {
                        ValueToReturn = (Data_Obj as Class_4).Date_Time_Stamp;
                    }
                }
                else
                    throw new InvalidCastException(String.Format("Not Supported {0} {1}", Data_Obj.ObjectType, Data_Obj.GetType()));

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while StoreData_BaseClass", ex);
            }

            return ValueToReturn;
        }

        public static object LoadData_BaseClass(this Base_Class Data_Obj, byte AttributeIndex, DataTypes TargetDataType, Object ValueToSave)
        {
            try
            {
                // Argument validation
                if (Data_Obj == null)
                    throw new ArgumentNullException("Base_Class");

                if (ValueToSave == null)
                    throw new ArgumentNullException("ValueToSave");

                if (TargetDataType == null)
                    throw new ArgumentNullException("TargetDataType");

                if (Data_Obj == null)
                    throw new ArgumentNullException("Base_Class");

                // Initialize Parameters To Encode
                Data_Obj.EncodingType = TargetDataType;
                Data_Obj.EncodingAttribute = AttributeIndex;

                #region Class_1 Data

                if (Data_Obj is Class_1)
                {
                    // Store Value Attribute
                    if (AttributeIndex == 0x02)
                    {
                        if (TargetDataType.IsNumericDataType())
                        {
                            (Data_Obj as Class_1).Value = (ValueType)ValueToSave;
                        }
                        else if (TargetDataType.IsDateAndTimeDataType())
                        {
                            (Data_Obj as Class_1).Value_Obj = ValueToSave;
                        }
                        else if (TargetDataType.IsVariableStringDataType())
                        {
                            (Data_Obj as Class_1).Value_Array = (Array)ValueToSave;
                        }
                        else if (TargetDataType == DataTypes._A01_array)
                        {
                            (Data_Obj as Class_1).Value_Array = (Array)ValueToSave;
                            // FIX Data Type May Be Later Re-define
                            (Data_Obj as Class_1).EncodingSubType = DataTypes._A0F_integer;
                        }
                        else
                            throw new InvalidCastException(string.Format("{0} Target Type Not Support", TargetDataType));
                    }
                }

                #endregion
                #region Class_3 Register

                else if (Data_Obj is Class_3)
                {
                    // Store Value Attribute
                    if (AttributeIndex == 0x02)
                    {
                        if (TargetDataType.IsNumericDataType())
                        {
                            (Data_Obj as Class_3).Value = (ValueType)ValueToSave;
                        }
                        else if (TargetDataType.IsVariableStringDataType())
                        {
                            (Data_Obj as Class_3).Value_Array = (byte[])ValueToSave;
                        }
                        else if (TargetDataType == DataTypes._A01_array)
                        {
                            (Data_Obj as Class_3).Value_Array = (byte[])ValueToSave;
                            // FIX Data Type May Be Later Redefine
                            (Data_Obj as Class_3).EncodingSubType = DataTypes._A0F_integer;
                        }
                        else
                            throw new InvalidCastException(string.Format("{0} Target Type Not Support", TargetDataType));
                    }
                    // Store Scaler Unit
                    if (AttributeIndex == 0x03)
                    {
                        throw new InvalidOperationException("Scaler Unit");
                        // Scaler String Format
                        // String Output_FRMT = "{0} Scl:{1}";
                        // ValueToSave = String.Format(Output_FRMT, (Data_Obj as Class_3).Unit, (Data_Obj as Class_3).scaler);
                    }
                }

                #endregion
                #region Class_4 MDI Register

                else if (Data_Obj is Class_4)
                {
                    // Store Value Attribute
                    if (AttributeIndex == 0x02)
                    {
                        if (TargetDataType.IsNumericDataType())
                        {
                            (Data_Obj as Class_4).Value = Convert.ToDouble(ValueToSave);
                        }
                        else
                            throw new InvalidCastException(string.Format("{0} Target Type Not Support", TargetDataType));
                    }
                    // Store Scaler Unit
                    if (AttributeIndex == 0x03)
                    {
                        throw new InvalidOperationException("Scaler Unit");
                        // Scaler String Format
                        // String Output_FRMT = "{0} Scl:{1}";
                        // ValueToSave = String.Format(Output_FRMT, (Data_Obj as Class_3).Unit, (Data_Obj as Class_3).scaler);
                    }
                    // Store Status
                    if (AttributeIndex == 0x04)
                    {
                        throw new InvalidOperationException("Status");
                        // Scaler String Format
                        // String Output_FRMT = "{0} Scl:{1}";
                        // ValueToSave = String.Format(Output_FRMT, (Data_Obj as Class_3).Unit, (Data_Obj as Class_3).scaler);
                    }

                    // Store Capture Date Time
                    if (AttributeIndex == 0x05)
                    {
                        (Data_Obj as Class_4).Date_Time_Stamp = (StDateTime)(ValueToSave);
                    }
                }
                else
                    throw new InvalidCastException(String.Format("Not Supported {0} {1}", Data_Obj.ObjectType, Data_Obj.GetType()));

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while StoreData_BaseClass", ex);
            }

            return ValueToSave;
        }

        public static bool IsNumericDataType(this DataTypes Recieved_Obj_Type)
        {
            switch (Recieved_Obj_Type)
            {
                case DataTypes._A14_long_64:
                case DataTypes._A15_long_64_unsigned:
                case DataTypes._A05_double_long:
                case DataTypes._A06_double_long_unsigned:
                case DataTypes._A10_long:
                case DataTypes._A12_long_unsigned:
                case DataTypes._A11_unsigned:
                case DataTypes._A0D_bcd:
                case DataTypes._A0F_integer:
                case DataTypes._A16_enum:
                case DataTypes._A03_boolean:
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public static bool IsVariableStringDataType(this DataTypes Recieved_Obj_Type)
        {
            switch (Recieved_Obj_Type)
            {
                case DataTypes._A09_octet_string:
                case DataTypes._A0A_visible_string:
                case DataTypes._A0C_utf8_string:
                case DataTypes._A04_bit_string:
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public static bool IsDateAndTimeDataType(this DataTypes Recieved_Obj_Type)
        {
            switch (Recieved_Obj_Type)
            {
                case DataTypes._A19_datetime:
                case DataTypes._A1A_date:
                case DataTypes._A1B_time:
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        #endregion

        #region Debugger & Logging

        public static void LogMessage(this ILogWriter logger, string Message, uint Level = 0)
        {
            try
            {
                Console.Out.WriteLine(Message);
                //TODO: Writing to Log Problem
                //logger.WriteInformation(Message, LogDestinations.Console | LogDestinations.TextFile | LogDestinations.UDP);
            }
            catch (Exception ex)
            {
                string msgSTR = String.Format(" {0,10}\t{1}", string.Empty, ex.Message);
                if (logger != null && logger.WriteToEventLog)
                    logger.WriteError(msgSTR, LogSystem.Shared.Common.Enums.LogDestinations.EventLog);
            }
        }

        public static void LogErrorMessage(this ILogWriter logger, string Message, uint Level = 0)
        {
            try
            {
                logger.WriteError(Message, LogDestinations.Console | LogDestinations.TextFile | LogDestinations.UDP);
            }
            catch (Exception ex)
            {
                string msgSTR = String.Format(" {0,10}\t{1}", string.Empty, ex.Message);
                if (logger != null && logger.WriteToEventLog)
                    logger.WriteError(msgSTR, LogSystem.Shared.Common.Enums.LogDestinations.EventLog);
            }
        }


        public static void LogMessage(this ILogWriter logger, string Message, string _logCode, string value, uint Level = 0)
        {
            try
            {
                string msgSTR = String.Format(" {0,10} \t{1,-8}{2,-2}{3,-2}", string.Empty, _logCode, value, Message);
                Console.Out.WriteLine(msgSTR);
                //TODO: Writing to Log Problem
                //logger.WriteInformation(msgSTR, LogDestinations.Console | LogDestinations.TextFile | LogDestinations.UDP);
            }
            catch (Exception ex)
            {
                string msgSTR = String.Format(" {0,10}\t{1}", string.Empty, ex.Message);

                if (logger != null &&
                    logger.WriteToEventLog)
                    logger.WriteError(msgSTR, LogSystem.Shared.Common.Enums.LogDestinations.EventLog);
            }
        }

        public static void LogMessage(this ILogWriter logger, string msgArg, Exception Ex, uint Level = 0, string methodName = "")
        {
            try
            {
                StringBuilder strBuilder = new StringBuilder(100);
                StringBuilder errorMsg = new StringBuilder(150);

                if (Ex != null)
                {
                    var msg = "Originator: " + methodName;
                    ExtractEachExceptionAndLog(Ex, Convert.ToInt32(Level), strBuilder);

                    if (!string.IsNullOrEmpty(msgArg))
                        errorMsg.Append(msgArg);
                    // Print Message Exception
                    errorMsg.Append("~*Exception*~");
                    errorMsg.Append(msg);
                    if (Ex.TargetSite != null)
                        errorMsg.Append("Method Name: " + Ex.TargetSite.Name);
                    errorMsg.Append(strBuilder);
                    errorMsg.Append("~*End-Exception*~");

                    if (logger != null && logger.WriteToEventLog)
                        logger.WriteError(errorMsg.ToString(), LogDestinations.EventLog);
                    logger.WriteError(errorMsg.ToString(), LogDestinations.Console | LogDestinations.TextFile | LogDestinations.UDP);
                }
            }
            catch (Exception ex)
            {
                string msgSTR = String.Format(" {0,10}\t{1}", string.Empty, ex.Message);

                if (logger != null &&
                    logger.WriteToEventLog)
                    logger.WriteError(msgSTR, LogSystem.Shared.Common.Enums.LogDestinations.EventLog);
            }
        }

        public static void ExtractEachExceptionAndLog(Exception ex, int level, StringBuilder logger)
        {
            try
            {
                logger.Append(ex.Message);
                for (int i = 1; i < level; i++)
                {
                    if (ex.InnerException != null)
                        ex = ex.InnerException;
                    else
                        break;
                    logger.Append(ex.Message);
                }
            }
            catch
            {
                if (logger != null)
                    logger.Append("Error occurred while Print Exception Logs");
            }
        }

        #endregion

    }

    #region DLMSCommand

    /// <summary>
    /// DLMSCommand Enumeration
    /// </summary>
    public enum DLMSCommand : byte
    {
        /// <summary>
        /// No command to execute.
        /// <value>0</value>
        /// </summary>
        None = 0,

        /// <summary>
        /// Initiate request.
        /// <value>0x01</value>
        /// </summary>
        InitiateRequest = 0x1,

        /// <summary>
        /// Initiate response.
        /// <value>0x08</value>
        /// </summary>
        InitiateResponse = 0x08,

        /// <summary>
        /// Read request.
        /// <value>0x05</value>
        /// </summary>
        ReadRequest = 0x5,

        /// <summary>
        /// Read response.
        /// <value>0x0C</value>
        /// </summary>
        ReadResponse = 0xC,

        /// <summary>
        /// Write request.
        /// <value>0x06</value>
        /// </summary>
        WriteRequest = 0x6,

        /// <summary>
        /// Write response.
        /// <value>0x0D</value>
        /// </summary>
        WriteResponse = 0xD,

        /// <summary>
        /// Get request.
        /// </summary>
        GetRequest = 0xC0,

        /// <summary>
        /// Get response.
        /// <value>0xC4</value>
        /// </summary>
        GetResponse = 0xC4,

        /// <summary>
        /// Set request.
        /// <value>0xC1</value>
        /// </summary>
        SetRequest = 0xC1,

        /// <summary>
        /// Set response.
        /// <value>0xC5</value>
        /// </summary>
        SetResponse = 0xC5,

        /// <summary>
        /// Action request.
        /// <value>0xC3</value>
        /// </summary>
        MethodRequest = 0xC3,

        /// <summary>
        /// Action response.
        /// <value>0xC7</value>
        /// </summary>
        MethodResponse = 0xC7,

        /// <summary>
        /// Command rejected.
        /// <value>0x97</value>
        /// </summary>
        Rejected = 0x97,

        /// <summary>
        /// SNRM request.
        /// <value>0x93</value>
        /// </summary>
        Snrm = 0x93,

        /// <summary>
        /// UA request.
        /// <value>0x73</value>
        /// </summary>
        Ua = 0x73,

        /// <summary>
        /// AARQ request.
        /// <value>0x60</value>
        /// </summary>
        Aarq = 0x60,

        /// <summary>
        /// AARE request.
        /// <value>0x61</value>
        /// </summary>
        Aare = 0x61,

        /// <summary>
        /// Disconnect request for HDLC framing.
        /// <value>0x53</value>
        /// </summary>
        Disc = 0x53,

        /// <summary>
        /// Disconnect request for WRAPPER.
        /// <value>0x62</value>
        /// </summary>
        DisconnectRequest = 0x62,

        /// <summary>
        /// Disconnect response for WRAPPER.
        /// </summary>
        DisconnectResponse = 0x63,

        /// <summary>
        /// Confirmed Service Error.
        /// <value>0x0E</value>
        /// </summary>
        ConfirmedServiceError = 0x0E,

        /// <summary>
        /// Exception Response.
        /// <value>0xD8</value>
        /// </summary>
        ExceptionResponse = 0xD8,

        /// <summary>
        /// General Block Transfer.
        /// <value>0xE0</value>
        /// </summary>
        GeneralBlockTransfer = 0xE0,

        /// <summary>
        /// Access Request.
        /// <value>0xD9</value>
        /// </summary>
        AccessRequest = 0xD9,
        /// <summary>
        /// Access Response.
        /// <value>0xDA</value>
        /// </summary>
        AccessResponse = 0xDA,

        /// <summary>
        /// Data Notification request.
        /// <value>0x0F</value>
        /// </summary>
        DataNotification = 0x0F,

        /// <summary>
        /// Glo get request.
        /// <value>0xC8</value>
        /// </summary>
        GloGetRequest = 0xC8,

        /// <summary>
        /// Glo get response.
        /// <value>0xCC</value>
        /// </summary>
        GloGetResponse = 0xCC,

        /// <summary>
        /// Glo set request.
        /// <value>0xC9</value>
        /// </summary>
        GloSetRequest = 0xC9,

        /// <summary>
        /// Glo set response.
        /// <value>0xCD</value>
        /// </summary>
        GloSetResponse = 0xCD,

        /// <summary>
        /// Glo general ciphering.
        /// <value>0xDB</value>
        /// </summary>
        GloGeneralCiphering = 0xDB,


        /// <summary>
        /// Event notification request.
        /// <value>0xC2</value>
        /// </summary>
        EventNotificationRequest = 0xC2,


        /// <summary>
        /// Glo event notification request.
        /// <value>0xCA</value>
        /// </summary>
        GloEventNotificationRequest = 0xCA,

        /// <summary>
        /// Glo method request.
        /// <value>0xCB</value>
        /// </summary>
        GloMethodRequest = 0xCB,

        /// <summary>
        /// Glo method response.
        /// <value>0xCF</value>
        /// </summary>
        GloMethodResponse = 0xCF,

        /// <summary>
        /// Glo Initiate request.
        /// <value>0x21</value>
        /// </summary>
        GloInitiateRequest = 0x21,

        /// <summary>
        /// Glo read request.
        /// <value>37</value>
        /// </summary>
        GloReadRequest = 37,

        /// <summary>
        /// Glo write request.
        /// <value>38</value>
        /// </summary>
        GloWriteRequest = 38,

        /// <summary>
        /// Glo Initiate response.
        /// <value>0x28</value>
        /// </summary>
        GloInitiateResponse = 0x28,

        /// <summary>
        /// Glo read response.
        /// <value>44</value>
        /// </summary>
        GloReadResponse = 44,

        /// <summary>
        /// Glo write response.
        /// <value>45</value>
        /// </summary>
        GloWriteResponse = 45,

        /// <summary>
        /// Glo Confirmed ServiceError
        /// <value>46</value>
        /// </summary>
        GloConfirmedServiceError = 46
    }

    #endregion



}
