using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using TCP_Communication;
using ucCustomControl;
using comm;
using SmartEyeControl_7.Controllers;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_password))]
    public class Param_password : IParam, ISerializable,ICloneable
    {
        #region Data_Members

        public const int PasswordLength = 19;

        [XmlIgnore]
        internal string _Management_Device = "mtiDLMScosem";
        [XmlIgnore]
        internal string _Electrical_Device = "mtiDLMScosem";

        #endregion

        public string Electrical_Device
        {
            get { return _Electrical_Device; }
            set
            {
                _Electrical_Device = ValidatePassword(value);
            }
        }

        public string Management_Device
        {
            get { return _Management_Device; }
            set
            {
                _Management_Device = ValidatePassword(value);
            }
        }

        private string ValidatePassword(string val)
        {
            String Password = null;
            if (String.IsNullOrEmpty(val) || String.IsNullOrWhiteSpace(val) || val.Length > PasswordLength)
                throw new Exception("Invalid Password Provided");
            
            //Append Trailing Bytes
            //if (val.Length < PasswordLength)
            //{
            //    //Password = val.PadRight(PasswordLength - val.Length, '\r');
            //    Password = val;
            //}
            //else

            Password = val;
            return Password;
        }

        public Param_password()
        { }

        #region Encoders/Decoders

        public Base_Class Encode_Management_Device_Password(GetSAPEntry CommObjectGetter)
        {
            Class_15 Management_Device_Password = (Class_15)CommObjectGetter.Invoke(Get_Index.Current_Association1);
            Management_Device_Password.Password = Management_Device;
            return Management_Device_Password;
        }

        public Base_Class Encode_Electrical_Device_Password(GetSAPEntry CommObjectGetter)
        {
            Class_15 Electrical_Device_Password = (Class_15)CommObjectGetter.Invoke(Get_Index.Current_Association2);
            Electrical_Device_Password.Password = Electrical_Device;
            return Electrical_Device_Password;
        }

        public void Decode_Device_Password(Base_Class arg)
        {
            try
            {
                Class_15 Electrical_Device_Password = (Class_15)arg;
                if (Electrical_Device_Password.INDEX ==
                    Get_Index.Current_Association1)
                {
                    Management_Device = Electrical_Device_Password.Password;
                }
                else if (Electrical_Device_Password.INDEX == Get_Index.Current_Association2)
                {
                    Electrical_Device = Electrical_Device_Password.Password;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                        temp = new String(ASCIIEncoding.ASCII.GetChars(dtArray)); ;
                    return temp;
                }
                if (Class_ID == 3)
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

        #region ISerializable_Members

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public Param_password(SerializationInfo info, StreamingContext context)
        {
            CryptoHelper helper = new CryptoHelper();
            ///Getting Management_Device Type String
            this.Management_Device = helper.GetDecryptedValue(info.GetString("Management_Device"));
            ///Getting Electrical_Device Type String
            this.Electrical_Device = helper.GetDecryptedValue(info.GetString("Electrical_Device"));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            CryptoHelper helper = new CryptoHelper();
            ///Adding Management_Device Type String
            info.AddValue("Management_Device", helper.GetEncryptedValue(this.Management_Device));
            ///Adding Electrical_Device Type String
            info.AddValue("Electrical_Device", helper.GetEncryptedValue(this.Electrical_Device));
        }

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}
