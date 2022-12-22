using System;
using System.Text;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace comm
{
    [XmlInclude(typeof(Param_Customer_Code))]
    public class Param_Customer_Code : DecodeAnyObject, IParam, ICloneable
    {
        //[XmlElement("CustomerCode", Type = typeof(string))]
        [XmlIgnore()]
        private string customer_Code_String;
        [XmlIgnore()]
        private string customer_Name;
        [XmlIgnore()]
        private string customer_Address;

        public const int CustomerCodeLength = 16;

        [XmlIgnore()]
        public string Customer_Code_String
        {
            get
            {
                if (!String.IsNullOrEmpty(customer_Code_String) &&
                    customer_Code_String.Length != CustomerCodeLength)
                    return customer_Code_String.PadRight(CustomerCodeLength, '\0');
                else
                {
                    return "".PadRight(CustomerCodeLength, '\0');
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    customer_Code_String = value.TrimEnd("\0".ToCharArray());
                else
                    customer_Code_String = value;
            }
        }

        [XmlElement("CustomerCode", Type = typeof(string))]
        [System.ComponentModel.DefaultValue("00000000000")]
        public string _Customer_Code_String
        {
            get { return customer_Code_String; }
            set { customer_Code_String = value; }
        }

        [XmlElement("Customer_Name", Type = typeof(string))]
        [System.ComponentModel.DefaultValue("Accurate")]
        public string Customer_Name
        {
            get { return customer_Name; }
            set { customer_Name = value; }
        }

        [XmlElement("Customer_Address", Type = typeof(string))]
        [System.ComponentModel.DefaultValue("Accurate Pvt. Limited Lahore Pakistan")]
        public string Customer_Address
        {
            get { return customer_Address; }
            set { customer_Address = value; }
        }

        #region Encode/Decode_Customer_Ref_Code

        public Param_Customer_Code()
        {
            Customer_Code_String = "000000000000000";
        }

        public Base_Class Encode_Customer_Reference(GetSAPEntry CommObjectGetter)
        {
            Class_1 Cust_Ref_Code = (Class_1)CommObjectGetter.Invoke(Get_Index.Customer_Reference_No);
            Cust_Ref_Code.EncodingAttribute = 2;
            Cust_Ref_Code.EncodingType = DataTypes._A09_octet_string;
            if (!String.IsNullOrEmpty(Customer_Code_String))
            {
                byte[] custRefCode = ASCIIEncoding.ASCII.GetBytes(Customer_Code_String);
                Cust_Ref_Code.Value_Array = custRefCode;
            }
            else
                Cust_Ref_Code.Value_Array = null;
            return Cust_Ref_Code;
        }

        public Base_Class Encode_Customer_Name(GetSAPEntry CommObjectGetter)
        {
            Class_1 Cust_Ref_Code = (Class_1)CommObjectGetter.Invoke(Get_Index.Customer_Name);
            Cust_Ref_Code.EncodingAttribute = 2;
            Cust_Ref_Code.EncodingType = DataTypes._A09_octet_string;
            if (!String.IsNullOrEmpty(Customer_Name))
            {
                byte[] custName = ASCIIEncoding.ASCII.GetBytes(Customer_Name);
                Cust_Ref_Code.Value_Array = custName;
            }
            else
                Cust_Ref_Code.Value_Array = null;
            return Cust_Ref_Code;
        }

        public Base_Class Encode_Customer_Address(GetSAPEntry CommObjectGetter)
        {
            Class_1 Cust_Ref_Code = (Class_1)CommObjectGetter.Invoke(Get_Index.Customer_Address);
            Cust_Ref_Code.EncodingAttribute = 2;
            Cust_Ref_Code.EncodingType = DataTypes._A09_octet_string;
            if (!String.IsNullOrEmpty(Customer_Address))
            {
                byte[] custAddress = ASCIIEncoding.ASCII.GetBytes(Customer_Address);
                Cust_Ref_Code.Value_Array = custAddress;
            }
            else
                Cust_Ref_Code.Value_Array = null;
            return Cust_Ref_Code;
        }

        public void Decode_Customer_Reference(Base_Class arg)
        {
            try
            {
                Class_1 Cust_Ref_Code = (Class_1)arg;
                ///Verify data Receiced/OBIS Code & Data Access Results/ETC
                byte[] dtArray = (byte[])Cust_Ref_Code.Value_Array;
                String str = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
                Customer_Code_String = str;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Customer_Name(Base_Class arg)
        {
            try
            {
                Class_1 Cust_Ref_Code = (Class_1)arg;
                ///Verify data Receiced/OBIS Code & Data Access Results/ETC
                byte[] dtArray = (byte[])Cust_Ref_Code.Value_Array;
                String str = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
                Customer_Name = str;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Decode_Customer_Address(Base_Class arg)
        {
            try
            {
                Class_1 Cust_Ref_Code = (Class_1)arg;
                ///Verify data Receiced/OBIS Code & Data Access Results/ETC
                byte[] dtArray = (byte[])Cust_Ref_Code.Value_Array;
                String str = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
                Customer_Address = str;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Decode_string(Base_Class arg, byte Class_ID)
        {
            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    byte[] dtArray = (byte[])temp_obj.Value_Array;
                    string temp = "---";
                    if (temp_obj.Value_Array.Length != 0)
                        temp = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
                    return temp;
                }
                else if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    byte[] dtArray = (byte[])temp_obj.Value_Array;
                    string temp = "---";
                    if (temp_obj.Value_Array.Length != 0)
                        temp = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
                    return temp;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Decode_Any

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any(arg, Class_ID);
        }

        public double Decode_Any(Base_Class arg, byte Class_ID, ref StDateTime TimeStamp)
        {
            return DLMS_Common.Decode_Any(arg, Class_ID);
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, DecodeAnyObject DataContainer_Class_obj, string Data_Property)
        {
            return DLMS_Common.TryDecode_Any(arg, Class_ID, DataContainer_Class_obj, Data_Property);
        }

        public bool TryDecode_Any(Base_Class arg, byte Class_ID, DecodeAnyObject DataContainer_Class_obj, string Data_Property, string CaptureTimeStamp_DataProperty)
        {
            throw new NotImplementedException();
        }

        public string Decode_Any_string(Base_Class arg, byte Class_ID)
        {
            string RetVal = "---";

            var tmp = DLMS_Common.Decode_Any_string(arg, Class_ID);

            if (!string.IsNullOrEmpty(tmp))
                RetVal = tmp;

            return RetVal;
        }

        public byte[] Decode_Any_ByteArray(Base_Class arg, byte Class_ID)
        {
            return DLMS_Common.Decode_Any_ByteArray(arg, Class_ID);
        }

        #endregion


    }
}
