using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace _HDLC
{
    public class CheckSum
    {
        private ushort[] fcs_table = null;
        private int i = 0;
        public const int P = 33800;
       
        public CheckSum()
        { 
            fcs_table = new ushort[256];
            InitCheckSumTable();
        }

        public void InitCheckSumTable()
        {
            ushort v = 0;
            ushort i = 0;
            ushort k = 1;
            for (ushort tableIndex = 0;tableIndex  <  fcs_table.Length; tableIndex++ )
            {
                v = tableIndex;
                for (i = 8; i > 0; i--)
                {

                    if ((v & k) != 0)
                    {
                        v = (ushort)((v >> 1) ^ P);
                    }
                    else
                    {
                        v = (ushort)(v >> 1);
                    }
                }
                fcs_table[tableIndex] = (ushort)(v & ushort.MaxValue);//65535
            }
        }

        public ushort ComputeCheckSum(byte []Data,int length)
        {
            uint fcs = ushort.MaxValue;
            while (length > 0)
            {
                fcs = (fcs >> 8) ^ fcs_table[(fcs ^ Data[(Data.Length - length)]) & (fcs_table.Length-1)];
                length--;

            }
            return (ushort)(fcs ^ ushort.MaxValue); 
        }

        public ushort ComputeCheckSum(byte[] Data)
        {
            return ComputeCheckSum(Data, Data.Length);
        }

        public static ushort Calc_CRC16(byte[] DataArray)
        {
            return Calc_CRC16(DataArray,0, DataArray.Length);
        }

        public static ushort Calc_CRC16(byte[] DataArry,int index ,int Length)
        {
            ushort nCrcChkSum, y;
            byte i;
            int j;

            nCrcChkSum = 0xFFFF;
            for (j = index; j < (index + Length); j++)
            {
                y = (ushort)((ushort)DataArry[j] ^ nCrcChkSum);
                y &= (ushort)0x00FF;		        // calculate CRC on low byte
                for (i = 0; i < 8; i++)     // Loop eight times, once for each bit
                {
                    if ((y & 1) == 1)              // check if this bit is high?
                    {
                        y >>= 1;
                        y ^= (ushort)0x8408;	    // Toggle the feedback bits
                    }
                    else
                    {
                        y >>= 1;
                    }
                }
                y = (ushort)(y ^ (nCrcChkSum >> 8));
                nCrcChkSum = y;			    // Return the 16 bit checksum
            }

            nCrcChkSum = (ushort)~nCrcChkSum;
            return (nCrcChkSum);
        }
     }

    public static class Common
    {
        public static bool ArraysEqual(Array a1, Array a2)
        {
            try
            {
                if (a1 == a2)
                    return true;

                if (a1 == null || a2 == null)
                    return false;

                if (a1.Length != a2.Length)
                    return false;

                IList list1 = a1, list2 = a2;  // error CS0305: Using the generic type 'System.Collections.Generic.IList<T>' requires '1' type arguments
                for (int i = 0; i < a1.Length; i++)
                {
                    if (!Object.Equals(list1[i], list2[i])) //error CS0021: Cannot apply indexing with [] to an expression of type 'IList'(x2)
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool IsEquals(this Array a1, Array a2)
        {
            return Common.ArraysEqual(a1, a2);
        }
    }
}
