using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using DLMS;
using SmartEyeControl_7.Common;
using DLMS.Comm;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_Number_Profile))]
    public class Param_Number_Profile : ICustomStructure, IParam
    {
        #region Data_Members

        [XmlElement("Unique_ID", Type = typeof(byte))]
        public byte Unique_ID;
        [XmlElement("Number", Type = typeof(byte[]))]
        public byte[] Number;
        [XmlElement("DataCallNumber", Type = typeof(byte[]))]
        public byte[] Datacall_Number;
        [XmlElement("RawFlags", Type = typeof(byte))]
        public byte RawFLAGs
        {
            get
            {
                byte Flags = 0x00;
                Flags += (Accept_Data_Call_FLAG_7) ? (byte)0x80 : (byte)0x00;
                Flags += (Allow_2way_SMS_communication_FLAG_6) ? (byte)0x40 : (byte)0x00;
                Flags += (Wakup_On_Voice_Call_FLAG_5) ? (byte)0x20 : (byte)0x00;
                Flags += (Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4) ? (byte)0x10 : (byte)0x00;
                Flags += (Wakeup_On_SMS_FLAG_3) ? (byte)0x08 : (byte)0x00;
                Flags += (Reject_With_Attend_FLAG_2) ? (byte)0x04 : (byte)0x00;
                Flags += (Reject_Call_FLAG_1) ? (byte)0x02 : (byte)0x00;
                Flags += (Verify_Password_FLAG_0) ? (byte)0x01 : (byte)0x00;
                return Flags;
            }
            set
            {
                byte Flags = value;
                ///Decoding Flags Byte
                Accept_Data_Call_FLAG_7 = ((Flags & 0x80) > 0) ? true : false;
                Allow_2way_SMS_communication_FLAG_6 = ((Flags & 0x40) > 0) ? true : false;
                Wakup_On_Voice_Call_FLAG_5 = ((Flags & 0x20) > 0) ? true : false;
                Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4 = ((Flags & 0x10) > 0) ? true : false;
                Wakeup_On_SMS_FLAG_3 = ((Flags & 0x08) > 0) ? true : false;
                Reject_With_Attend_FLAG_2 = ((Flags & 0x04) > 0) ? true : false;
                Reject_Call_FLAG_1 = ((Flags & 0x02) > 0) ? true : false;
                Verify_Password_FLAG_0 = ((Flags & 0x01) > 0) ? true : false;
            }
        }
        [XmlIgnore()]
        public bool Verify_Password_FLAG_0;
        [XmlIgnore()]
        public bool Reject_Call_FLAG_1;
        [XmlIgnore()]
        public bool Reject_With_Attend_FLAG_2;
        [XmlIgnore()]
        public bool Wakeup_On_SMS_FLAG_3;
        [XmlIgnore()]
        public bool Accept_Paramaeters_In_Wake_Up_SMS_FLAG_4;
        [XmlIgnore()]
        public bool Wakup_On_Voice_Call_FLAG_5;
        [XmlIgnore()]
        public bool Allow_2way_SMS_communication_FLAG_6;
        [XmlIgnore()]
        public bool Accept_Data_Call_FLAG_7;
        [XmlElement("WakeUpOnSMS", Type = typeof(byte))]
        public byte Wake_Up_On_SMS;
        [XmlElement("WakeUpOnVoiceCall", Type = typeof(byte))]
        public byte Wake_Up_On_Voice_Call;
        [XmlElement("Flag2", Type = typeof(byte))]
        public byte FLAG2;

        #endregion

        #region OBIS_data_from_GUI
        public OBIS_data_from_GUI[] encode_ALL()
        {

            OBIS_data_from_GUI[] structToReturn = new OBIS_data_from_GUI[2];

            // structToReturn[0] = encode_Management_Device();
            //   structToReturn[1] = encode_Electrical_Device();

            return structToReturn;
        }

        public OBIS_data_from_GUI encode_Unique_ID()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Current_Association1;
            obj_OBIS_data_from_GUI.attribute = 7;
            obj_OBIS_data_from_GUI.Type = DataTypes._A11_unsigned;
            obj_OBIS_data_from_GUI.Data_Array = Unique_ID;

            return obj_OBIS_data_from_GUI;
        }
        public OBIS_data_from_GUI encode_Number()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Current_Association2;
            obj_OBIS_data_from_GUI.attribute = 7;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            //  obj_OBIS_data_from_GUI.Data_Array = Electrical_Device;

            return obj_OBIS_data_from_GUI;
        }
        public OBIS_data_from_GUI encode_Datacall_Number()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Current_Association1;
            obj_OBIS_data_from_GUI.attribute = 7;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            //      obj_OBIS_data_from_GUI.Data_Array = Management_Device;

            return obj_OBIS_data_from_GUI;
        }
        public OBIS_data_from_GUI encode_Wake_Up_On_SMS()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Current_Association2;
            obj_OBIS_data_from_GUI.attribute = 7;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            //  obj_OBIS_data_from_GUI.Data_Array = Electrical_Device;

            return obj_OBIS_data_from_GUI;
        }
        public OBIS_data_from_GUI encode_Wake_Up_On_Voice_Call()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Current_Association1;
            obj_OBIS_data_from_GUI.attribute = 7;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            //     obj_OBIS_data_from_GUI.Data_Array = Management_Device;

            return obj_OBIS_data_from_GUI;
        }
        public OBIS_data_from_GUI encode_FLAG()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Current_Association2;
            obj_OBIS_data_from_GUI.attribute = 7;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            //  obj_OBIS_data_from_GUI.Data_Array = Electrical_Device;

            return obj_OBIS_data_from_GUI;
        }

        #endregion

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                return 0;
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
                throw;
            }
        }

        #region ICustomStructure Members

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>(50);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 8 });
                ///Unique_ID <DataType DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Unique_ID));
                ////Wake_Up_On_SMS <DataType DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Wake_Up_On_SMS));
                ////Wake_Up_On_Voice_Call <DataType DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Wake_Up_On_Voice_Call));

                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, 0)); //DUMMY!!!!    
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.RawFLAGs));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.FLAG2));
                ///Datacall_Number <DataTypes._A09_octet_string>
                encodeRaw.AddRange(BasicEncodeDecode.Encode_OctetString(this.Number, DataTypes._A09_octet_string));
                encodeRaw.AddRange(BasicEncodeDecode.Encode_OctetString(this.Datacall_Number, DataTypes._A09_octet_string));//Number <DataTypes._A09_octet_string>

                ///Encoding Flags Byte

                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_Number_Profile", "Encode_Param_Number_Profile", ex);
            }
        }

        public void Decode_Data(byte[] Data)
        {
            int array_traverse = 0;
            Decode_Data(Data, ref array_traverse, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 8)
                    throw new DLMSDecodingException("Invalid ICustomStructure ICustomStructure Param_Number_Profile Structure received", "Decode_Data_Param_IP_Profiles");
                array_traverse++;
                ///Unique_ID <DataTypes._A11_unsigned>
                this.Unique_ID = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                //Number <DataTypes._A09_octet_string>
                this.Wake_Up_On_SMS = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ////Wake_Up_On_Voice_Call <DataType DataTypes._A11_unsigned>
                this.Wake_Up_On_Voice_Call = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                byte Dummy = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.RawFLAGs = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.FLAG2 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.Number = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                //DataCall <DataTypes._A09_octet_string>
                this.Datacall_Number = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                ///Flag <DataTypes._A11_unsigned>
                ////Wake_Up_On_SMS <DataType DataTypes._A11_unsigned>

            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while encoding ICustomStructure Param_Number_Profile", "Decode_Data_Param_Number_Profile", ex);
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(256))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while Clone Object", ex);
            }
        }

        #endregion
    }

    public class Param_Number_ProfileHelper : INotifyPropertyChanged
    {
        public static string PropertyName = "Param_Number_Profile";
        public static string DefaultMobileNumber = "+920000000000";
        public static int Max_Number_Profile = 04;
        private int previous_Total_number_profiles = 0;
        private Param_Number_Profile[] _Param_Number_Profile_object = null;

        public Param_Number_Profile[] Param_Number_Profiles_object
        {
            get { return _Param_Number_Profile_object; }
            set { _Param_Number_Profile_object = value; }
        }

        public int Previous_Total_number_profiles
        {
            get { return previous_Total_number_profiles; }
            set { previous_Total_number_profiles = value; }
        }

        public int Total_Number_Profile
        {
            get
            {
                int count = 0;
                foreach (var ipParam in _Param_Number_Profile_object)
                {
                    if (ipParam != null && count < ipParam.Unique_ID && ipParam.Unique_ID <= Max_Number_Profile)
                        count = ipParam.Unique_ID;
                }
                return count;
            }
        }

        #region Constructor

        public Param_Number_ProfileHelper()
        {
            PropertyChanged += new PropertyChangedEventHandler((object sender, PropertyChangedEventArgs property) => { });
            _Param_Number_Profile_object = new Param_Number_Profile[Max_Number_Profile];
            ///Param_Number Profile
            for (int index = 0; index < Max_Number_Profile; index++)
                _Param_Number_Profile_object[index] = new Param_Number_Profile()
                {
                    Unique_ID = 0,
                    Wake_Up_On_Voice_Call = 0,
                    Wake_Up_On_SMS = 0,
                    Number = new byte[0],
                    Datacall_Number = new byte[0]
                };
        }

        public Param_Number_ProfileHelper(Param_Number_Profile[] _Param_Number_Profile_objectArg)
        {
            PropertyChanged += new PropertyChangedEventHandler((object sender, PropertyChangedEventArgs property) => { });
            //_Param_Number_Profile_object = new Param_Number_Profile[Max_Number_Profile];
            //for (int index = 0; index < Max_Number_Profile; index++)
            //    _Param_Number_Profile_object[index] = _Param_Number_Profile_objectArg[index];
            _Param_Number_Profile_object = _Param_Number_Profile_objectArg;
        }

        #endregion

        #region AddParam_Number_Profile

        public Param_Number_Profile AddParam_Number_Profile(Param_Number_Profile Arg)
        {
            byte total_Number_Profile_Count = (byte)Total_Number_Profile;
            if (total_Number_Profile_Count < Max_Number_Profile)
            {
                _Param_Number_Profile_object[total_Number_Profile_Count] = Arg;
                Arg.Unique_ID = (byte)(total_Number_Profile_Count + 1);
                //Init Number Profile
                Arg.Number = App_Common.ConvertFromValidNumberString(DefaultMobileNumber);
                //Init DataCall_Number
                Arg.Datacall_Number = App_Common.ConvertFromValidNumberString(DefaultMobileNumber);
                //Init Wakeup Profile
                Arg.Wake_Up_On_SMS = Arg.Wake_Up_On_Voice_Call = 1;
            }
            else
                throw new Exception(String.Format("Unable to Add Number Profile {0}", Arg));
            OnPropertyChanged(PropertyName);
            return Arg;
        }

        public Param_Number_Profile AddParam_Number_Profile()
        {
            Param_Number_Profile Param_Number_Profile = new Param_Number_Profile()
            {
                Unique_ID = 0,
                Wake_Up_On_Voice_Call = 0,
                Wake_Up_On_SMS = 0,
                Number = new byte[16] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 },
                Datacall_Number = new byte[16] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 }
            };
            return AddParam_Number_Profile(Param_Number_Profile);
        }

        public Param_Number_Profile GetLastParam_Number_Profile()
        {
            Param_Number_Profile Param_Number_Profile = null;
            byte total_Number_Profile_Count = (byte)Total_Number_Profile;
            if (total_Number_Profile_Count > 0)
                Param_Number_Profile = _Param_Number_Profile_object[total_Number_Profile_Count - 1];
            return Param_Number_Profile;
        }

        #endregion

        #region RemoveParam_Number_Profile

        public Param_Number_Profile RemoveParam_Number_Profile()
        {
            try
            {
                Param_Number_Profile Arg = null;
                byte total_Number_Profile_Count = (byte)Total_Number_Profile;
                if (total_Number_Profile_Count > 0)
                {
                    Arg = _Param_Number_Profile_object[total_Number_Profile_Count - 1];
                    if (Arg != null)
                        Arg.Unique_ID = 0;
                }
                else
                    throw new Exception(String.Format("Unable to Remove Number Profile {0}", Arg));
                OnPropertyChanged(PropertyName);
                return Arg;
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException)
                    throw new Exception(String.Format("Unable to Remove Number Profile {0}", ex.Message), ex);
                else
                    throw ex;
            }
        }

        #endregion

        public static Param_Number_Profile[] Param_Number_Profile_object_initialze(int Instances)
        {
            Param_Number_Profile[] Param_Number_Profile_object = new Param_Number_Profile[Instances];
            int count;
            for (count = 0; count < Instances; count++)
            {
                Param_Number_Profile_object[count] = new Param_Number_Profile();
                Param_Number_Profile_object[count].Number = new byte[16] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
                Param_Number_Profile_object[count].Datacall_Number = new byte[16] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };

            }
            if (Param_Number_Profile_object.Length > 4)
            {
                Param_Number_Profile_object[4].Number = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                Param_Number_Profile_object[4].Datacall_Number = new byte[16] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
            }
            return Param_Number_Profile_object;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                ///Raising the event when FirstName or LastName 
                ///property value changed
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
