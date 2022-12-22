using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Comm;
using AccurateOptocomSoftware.comm;

namespace comm
{
    public class MeterSerialNumber:IComparable<MeterSerialNumber>
    {
        #region Data_Members

        private byte manufacturerId = 36;
        private byte meterType = 98;
        private uint meterId = 1;

        public static byte HLManufacturerId = 40;
        public static byte HLMeterType = 99;
        public static uint HLMeterSerialNo = 999999;

        private static Dictionary<string, uint> manufacturer_ModelTokens = null;
        public static readonly string MSN_S_Validator = @"^(?<MSNStandard>(?<companyId>\d{2})(?<meterTypeId>\d{2})(?<meterId>\d{6}))$";
        public static readonly string MSN_Validator = @"^(?<MSN>(?<Prefix>[a-zA-Z]*[-,_,,,;,:]*[a-zA-Z0-9]*[-,_,,,;,:]*)(?<meterId>\d{6,30}))";

        #endregion

        #region Properties
        /// <summary>
        /// Manufacturer Identification Number,Valid Upto (00--40)
        /// </summary>
        public byte ManufacturerId
        {
            get { return manufacturerId; }
            set
            {
                if (value >= 00 && value <= HLManufacturerId)
                    this.manufacturerId = value;
                else
                    throw new Exception(String.Format("Invalid Manufacturer Identification Number {0}", value));
            }
        }

        /// <summary>
        /// Meter Type Identifier,Valid Upto (00--99)
        /// </summary>
        public byte MeterType
        {
            get
            { return meterType; }
            set
            {
                if (value <= HLMeterType)
                    meterType = value;
                else
                    throw new Exception(String.Format("Invalid Meter Type Identifier value {0}", value));
            }
        }

        /// <summary>
        /// Meter Identification Number is Six Digit Number Valid Upto (000001-999999)
        /// </summary>
        public uint MeterIdentification
        {
            get
            {
                return meterId;
            }
            set
            {
                if (value <= HLMeterSerialNo)
                    this.meterId = value;
                else
                    throw new Exception(String.Format("Invalid Meter Identification Number {0}", value));
            }
        }

        /// <summary>
        /// Represents four byte unsigned number as MSN
        /// </summary>
        public uint MSN
        {
            get
            {
                uint num = 0;
                unchecked
                {
                    num = (uint)(ManufacturerId * (100000000))
                        + (uint)(MeterType * (1000000)) +
                           MeterIdentification;
                }
                return num;
            }
            set
            {
                uint rawVal = value;
                MeterIdentification = (rawVal % (1000000));
                rawVal = rawVal / (1000000);
                MeterType = (byte)(rawVal % 100);
                rawVal = rawVal / 100;
                ManufacturerId = (byte)(rawVal % 100);
            }
        }

        /// <summary>
        /// Meter Serial Number Prefix Strings,may be loaded lator from configuration files
        /// </summary>
        public static Dictionary<string, uint> Manufacturer_ModelTokens
        {
            get { return MeterSerialNumber.manufacturer_ModelTokens; }
            set { MeterSerialNumber.manufacturer_ModelTokens = value; }
        }

        public bool IsMSN_Valid
        {
            get
            {
                try
                {
                    if (ManufacturerId > 0 && ManufacturerId <= HLManufacturerId  ///Valid ManufacturerId Range Test
                       && MeterType > 0 && MeterType <= HLMeterType         ///Valid MeterType Range Test                                                  
                       && MeterIdentification > 0 && MeterIdentification <= HLMeterSerialNo)
                        return true;
                }
                catch
                {
                }
                return false;
            }
        }


        #endregion

        static MeterSerialNumber()
        { 
            ///Assign Numerical Numbers Upper Limits 
            ///or Load from Configurations

            //HLManufacturerId = 99;        /// Commented by Azeem Inayat
            //HLMeterType = 99;             /// Commented by Azeem Inayat
            //HLMeterSerialNo = 999999;     /// Commented by Azeem Inayat

            ///Populate Meter Models Str
            Manufacturer_ModelTokens = new Dictionary<string, uint>();
            ///Manually Load Strings Here BCD format
            ///lator may load from system configuration file
            Manufacturer_ModelTokens.Add("ACCACT34G"            , 0x2462);  //3698
            Manufacturer_ModelTokens.Add(Commons.ACT34G_Meter   , 0x2462);

            Manufacturer_ModelTokens.Add("MTI_R326_"            , 0x2462);  //3698
            Manufacturer_ModelTokens.Add("R326_"                , 0x2462);
            
            Manufacturer_ModelTokens.Add("MTI_R421_"            , 0x2463);
            Manufacturer_ModelTokens.Add("R421_"                , 0x2463);
            
            Manufacturer_ModelTokens.Add("MTI_R411_"            , 0x2460);
            Manufacturer_ModelTokens.Add("R411_"                , 0x2460);
            
            Manufacturer_ModelTokens.Add("MTI_R283_"            , 0x2461);
            Manufacturer_ModelTokens.Add("R283_"                , 0x2461);
            
            Manufacturer_ModelTokens.Add("MTI_R283@_"           , 0x245F);
            Manufacturer_ModelTokens.Add("R283@_"               , 0x245F);
            
            Manufacturer_ModelTokens.Add("MTI_W275@_"           , 0x244B);
            Manufacturer_ModelTokens.Add("W275@_"               , 0x244B);
        }

        /// <summary>
        /// Convert to MeterSerialNumber object from meter serial no object
        /// </summary>
        /// <param name="MSNStr"></param>
        /// <returns></returns>
        public static MeterSerialNumber ConvertFrom(string MSNStr)
        {
            try
            {
                MeterSerialNumber SrNum = new MeterSerialNumber();
                Regex regEx_MSN_Stan = new Regex(MSN_S_Validator, RegexOptions.Compiled);
                Regex regEx_MSN = new Regex(MSN_Validator, RegexOptions.Compiled);
                if (regEx_MSN_Stan.Match(MSNStr).Success)       ///String Validated As MSN In Standard Format
                {
                    Match results = regEx_MSN_Stan.Match(MSNStr);
                    ///Parse Meter Identification Number
                    String meterId = results.Groups["meterId"].Value;
                    SrNum.MeterIdentification = UInt32.Parse(meterId);
                    ///Parse Meter Model Type
                    String MeterModel = results.Groups["meterTypeId"].Value;
                    SrNum.MeterType = Byte.Parse(MeterModel);
                    ///Parse Manufacturer Id
                    String ManufacturerId = results.Groups["companyId"].Value;
                    SrNum.ManufacturerId = Byte.Parse(ManufacturerId);
                }
                else if (regEx_MSN.Match(MSNStr).Success)  ///String Validated As MSN In extended Format
                {
                    Match results = regEx_MSN.Match(MSNStr);
                    ///Parse Meter Identification Number
                    uint metId = UInt32.MaxValue;
                    String meterId = results.Groups["meterId"].Value;
                    while (metId > HLMeterSerialNo)
                    {
                        bool res = UInt32.TryParse(meterId, out metId);
                        if (!res || metId >= HLMeterSerialNo)
                        {
                            meterId = meterId.Substring(1);
                            res = UInt32.TryParse(meterId, out metId);
                        }
                        if (!res)
                            metId = UInt32.MaxValue;
                    }
                    SrNum.MeterIdentification = metId;
                    ///Parse Meter Model Type
                    String MSN_PreFix = results.Groups["Prefix"].Value;
                    SrNum.ManufacturerId = GetManufacturerId(MSN_PreFix);
                    SrNum.MeterType = GetMeterModelId(MSN_PreFix);
                }
                else
                    throw new Exception("Invalid format of Meter Serial Number");
                return SrNum;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid format of meter serial number");
            }
        }
        /// <summary>
        /// Depricated Code,Get meter Model String From SAP String
        /// </summary>
        /// <returns></returns>
        public string GetMSNPrefix()
        {
            string model = null;
            try
            {
                ushort num = ManufacturerId;
                num <<= 8;
                num |= MeterType;
                
                foreach (var item_Tok in Manufacturer_ModelTokens)
                {
                    if (item_Tok.Value == num)
                        model = item_Tok.Key;
                }
            }
            catch (Exception ex)
            {
                
            }
            if (!String.IsNullOrEmpty(model))
                model = model.TrimEnd("_,:".ToCharArray());
            return model;
        }

        public static byte GetMeterModelId(String MSN_Prefix)
        {
            try
            {
                uint val = Manufacturer_ModelTokens[MSN_Prefix];
                return (byte)(val & 0xFF);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static byte GetManufacturerId(String MSN_Prefix)
        {
            try
            {
                uint val = Manufacturer_ModelTokens[MSN_Prefix];
                return (byte)((val >> 8) & 0xFF);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Convert from Raw bytes received into MeterSerialNumber instance
        /// </summary>
        /// <param name="RawMSN"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static MeterSerialNumber ConvertFrom(byte[] RawMSN,int index,int length)
        {
            try
            {
                uint number = 0;
                for(int index_ = index; 
                    (index_ < RawMSN.Length && 
                    index_ < length); index_++)
                {
                    if (index_ == index)
                        number = RawMSN[index_];
                    else
                    {
                        number = number << 8;
                        number = number | RawMSN[index_];
                    }
                }

                MeterSerialNumber num = new MeterSerialNumber();
                num.MSN = number;
                return num;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid format of meter serial number",ex);
            }
        }

        /// <summary>
        /// Output Meter Serial Number In Standard Numerical Text Formats Manufacturer_ID(two digit 0-40),
        /// Meter_Type(two digit 0-99),(Meter Identification 000001-999999)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return MSN.ToString("D10");
        }

        public string ToString(string formatSpecifier)
        {
            try
            {
                string str = null;
                if (formatSpecifier.Equals("S", StringComparison.OrdinalIgnoreCase))
                    str =  this.ToString();
                else
                {
                    ushort num = ManufacturerId;
                    num <<= 8;
                    num |= MeterType;
                    string model = "Unknown";
                    foreach (var item_Tok in Manufacturer_ModelTokens)
                    {
                        if (item_Tok.Value == num)
                            model = item_Tok.Key;
                    }
                    str = model + MeterIdentification.ToString("D8");
                }
                return str;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region IComparable<MeterSerialNumber> Members

        int IComparable<MeterSerialNumber>.CompareTo(MeterSerialNumber other)
        {
            return MSN.CompareTo(other.MSN);
        }

        #endregion
    }
}
