using SharedCode.Comm.DataContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCode.Comm.HelperClasses
{
    public class St_FirmwareInfo
    {
        #region Constants

        public const int ModelID_Length = 1;
        public const int Model_Length = 5;
        public const int CompanyCode_Length = 3;
        public const int SerialNo_Length = 8;
        public const int Date_Length = 9;
        public const int Time_Length = 6;
        public const int ReleaseID_Length = 6;
        public const int Version_Length = 6;
        public const int Info_1_Length = 2;
        public const int RFU_Length = 2;
        public const int Features_Length = 12;
        public const int DLMSVersion_Length = 4;

        public const string DateTime_Format = @"dd/MM/yy HH:mm";
        public const string Date_Format = @"dd/MM/yy";
        public const string Time_Format = @"HH:mm";

        #endregion

        public byte Model_ID;

        public string Model;
        public byte CompanyCode;

        public DateTime ReleaseDateTime;

        public string Release_ID;
        public string Version;

        /// <summary>
        /// Meter Serial Number
        /// </summary>
        public byte Info_1;
        public byte Info_2;
        public uint SrNum;

        public string RFU;
        public string Feature;
        public string DLMS_Version;

        public MeterSerialNumber MSN_Info
        {
            get
            {
                MeterSerialNumber msn = null;
                msn = new MeterSerialNumber();

                try
                {
                    msn.ManufacturerId = Info_1;
                    msn.MeterType = Info_2;
                    msn.MeterIdentification = Convert.ToUInt32(SrNum % 1000000);
                }
                catch
                {
                }

                return msn;
            }
            set
            {
                try
                {
                    Info_1 = value.ManufacturerId;
                    Info_2 = value.MeterType;
                    SrNum = value.MeterIdentification;
                }
                catch
                {
                }
            }
        }

        public St_FirmwareInfo(int dummy = 0)
        {
            Model_ID = 4;

            Model = "T421_";
            CompanyCode = 000;

            SrNum = 00000000;

            ReleaseDateTime = DateTime.MinValue; /// 13/10/14_12:10_

            Release_ID = "SC0718";
            Version = string.Empty;

            Info_1 = 36;
            Info_2 = 99;

            RFU = String.Empty;

            Feature = "DTGC4";
            DLMS_Version = string.Empty;
        }

        public void Decode_St_FirmwareInfo(byte[] byte_Array, int indexer, int length)
        {
            char[] TrimLiterals = " _-".ToCharArray();
            try
            {
                List<byte> byte_ArrayTmp = new List<byte>(15);
                String TmpSTR = string.Empty;

                for (int i = 0; i < byte_Array.Length; i++)
                {
                    byte_Array[i] = (byte)(byte_Array[i] ^ (0x55));
                }
                Model_ID = (byte_Array[indexer++]);

                // Model STR
                for (int i = 0; i < Model_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                Model = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());

                // Company Code STR
                byte_ArrayTmp.Clear();
                for (int i = 0; i < CompanyCode_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                var CompanyCodeSTR = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());
                byte.TryParse(CompanyCodeSTR, out CompanyCode);

                // Serial_No
                byte_ArrayTmp.Clear();
                for (int i = 0; i < SerialNo_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                TmpSTR = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());
                uint.TryParse(TmpSTR, out SrNum);

                // Release Date
                byte_ArrayTmp.Clear();
                for (int i = 0; i < Date_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                var DateSTR = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());

                // Release Time
                byte_ArrayTmp.Clear();
                for (int i = 0; i < Time_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                var TimeSTR = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());

                // Release_ID
                byte_ArrayTmp.Clear();
                for (int i = 0; i < ReleaseID_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                Release_ID = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());

                // Version
                byte_ArrayTmp.Clear();
                for (int i = 0; i < Version_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                Version = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());

                // Info_1
                byte_ArrayTmp.Clear();
                for (int i = 0; i < Info_1_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                TmpSTR = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());
                byte.TryParse(TmpSTR, out Info_1);

                // Info_2
                byte_ArrayTmp.Clear();
                for (int i = 0; i < Info_1_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                TmpSTR = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());
                byte.TryParse(TmpSTR, out Info_2);

                // RFU
                byte_ArrayTmp.Clear();
                for (int i = 0; i < RFU_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                RFU = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());

                // Features
                byte_ArrayTmp.Clear();
                for (int i = 0; i < Features_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                Feature = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());

                // DLMS_Version
                byte_ArrayTmp.Clear();
                for (int i = 0; i < DLMSVersion_Length; i++)
                {
                    byte_ArrayTmp.Add(byte_Array[indexer++]);
                }
                DLMS_Version = Encoding.ASCII.GetString(byte_ArrayTmp.ToArray());

                // Parse Release DateTime
                try
                {
                    TmpSTR = String.Format("{0} {1}", DateSTR.TrimEnd(TrimLiterals),
                                                      TimeSTR.TrimEnd(TrimLiterals));

                    ReleaseDateTime = DateTime.ParseExact(TmpSTR, @"dd/MM/yy HH:mm", null,
                                                          System.Globalization.DateTimeStyles.None);
                }
                catch
                {
                    ReleaseDateTime = DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while decoding St_FirmwareInfo", ex);
            }
            finally
            {
                if (!String.IsNullOrEmpty(Model))
                    Model = Model.TrimEnd(TrimLiterals);

                if (!String.IsNullOrEmpty(Release_ID))
                    Release_ID = Release_ID.TrimEnd(TrimLiterals);

                if (!String.IsNullOrEmpty(Version))
                    Version = Version.TrimEnd(TrimLiterals);

                if (!String.IsNullOrEmpty(Feature))
                    Feature = Feature.TrimEnd(TrimLiterals);

                if (!String.IsNullOrEmpty(DLMS_Version))
                    DLMS_Version = DLMS_Version.TrimEnd(TrimLiterals);
            }
        }

    }
}
