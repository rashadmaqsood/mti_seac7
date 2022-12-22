using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DLMS;
using System.ComponentModel;
using DLMS.Comm;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_WakeUp_Profile))]
    public class Param_WakeUp_Profile : IParam,ICloneable, ICustomStructure
    {
        #region Data_Members

        [XmlElement("WakeUpProfileID", Type = typeof(byte))]
        public byte Wake_Up_Profile_ID;
        [XmlElement("IPProfileID1", Type = typeof(byte))]
        public byte IP_Profile_ID_1;
        [XmlElement("IPProfileID2", Type = typeof(byte))]
        public byte IP_Profile_ID_2;
        [XmlElement("IPProfileID3", Type = typeof(byte))]
        public byte IP_Profile_ID_3;
        [XmlElement("IPProfileID_4", Type = typeof(byte))]
        public byte IP_Profile_ID_4;

        [XmlIgnore()]
        public bool Over_Volt_FLAG_1;
        [XmlIgnore()]
        public bool Over_Volt_FLAG_2;
        [XmlIgnore()]
        public bool Over_Volt_FLAG_3;
        [XmlIgnore()]
        public bool Over_Volt_FLAG_4;

        [XmlIgnore()]
        public bool Under_Volt_FLAG_1;
        [XmlIgnore()]
        public bool Under_Volt_FLAG_2;
        [XmlIgnore()]
        public bool Under_Volt_FLAG_3;
        [XmlIgnore()]
        public bool Under_Volt_FLAG_4;

        [XmlIgnore()]
        public bool Remotely_Control_FLAG_1;
        [XmlIgnore()]
        public bool Remotely_Control_FLAG_2;
        [XmlIgnore()]
        public bool Remotely_Control_FLAG_3;
        [XmlIgnore()]
        public bool Remotely_Control_FLAG_4;

        [XmlIgnore()]
        public bool Local_Control_FLAG_1;
        [XmlIgnore()]
        public bool Local_Control_FLAG_2;
        [XmlIgnore()]
        public bool Local_Control_FLAG_3;
        [XmlIgnore()]
        public bool Local_Control_FLAG_4;

        [XmlElement("FlagWakeUpProfile1", Type = typeof(byte))]
        public byte FLAG_WAKEUP_PROFILE_1
        {
            get
            {
                byte Flags_1 = 0x00;
                //Flags += (Reserved_Flag_7) ? (byte)0x80 : (byte)0x00;
                //Flags += (Reserved_Flag_6) ? (byte)0x40 : (byte)0x00;
                //Flags += (Reserved_Flag_5) ? (byte)0x20 : (byte)0x00;
                //Flags += (Reserved_Flag_4) ? (byte)0x10 : (byte)0x00;
                Flags_1 += (Local_Control_FLAG_1) ? (byte)0x08 : (byte)0x00;
                Flags_1 += (Remotely_Control_FLAG_1) ? (byte)0x04 : (byte)0x00;
                Flags_1 += (Under_Volt_FLAG_1) ? (byte)0x02 : (byte)0x00;
                Flags_1 += (Over_Volt_FLAG_1) ? (byte)0x01 : (byte)0x00;
                return Flags_1;
            }
            set
            {
                ///Decoding Flags Byte
                byte Flags_1 = value;
                //Reserved_Flag_7 = ((Flags & 0x80) > 0) ? true : false;
                //Reserved_Flag_6 = ((Flags & 0x40) > 0) ? true : false;
                //Reserved_Flag_5 = ((Flags & 0x20) > 0) ? true : false;
                //Reserved_Flag_4 = ((Flags & 0x10) > 0) ? true : false;
                Local_Control_FLAG_1 = ((Flags_1 & 0x08) > 0) ? true : false;
                Remotely_Control_FLAG_1 = ((Flags_1 & 0x04) > 0) ? true : false;
                Under_Volt_FLAG_1 = ((Flags_1 & 0x02) > 0) ? true : false;
                Over_Volt_FLAG_1 = ((Flags_1 & 0x01) > 0) ? true : false;
            }
        }
        [XmlElement("FlagWakeUpProfile2", Type = typeof(byte))]
        public byte FLAG_WAKEUP_PROFILE_2
        {
            get
            {
                byte Flags_2 = 0x00;
                //Flags += (Reserved_Flag_7) ? (byte)0x80 : (byte)0x00;
                //Flags += (Reserved_Flag_6) ? (byte)0x40 : (byte)0x00;
                //Flags += (Reserved_Flag_5) ? (byte)0x20 : (byte)0x00;
                //Flags += (Reserved_Flag_4) ? (byte)0x10 : (byte)0x00;
                Flags_2 += (Local_Control_FLAG_2) ? (byte)0x08 : (byte)0x00;
                Flags_2 += (Remotely_Control_FLAG_2) ? (byte)0x04 : (byte)0x00;
                Flags_2 += (Under_Volt_FLAG_2) ? (byte)0x02 : (byte)0x00;
                Flags_2 += (Over_Volt_FLAG_2) ? (byte)0x01 : (byte)0x00;
                return Flags_2;
            }
            set
            {
                ///Decoding Flags Byte
                byte Flags_2 = value;
                //Reserved_Flag_7 = ((Flags & 0x80) > 0) ? true : false;
                //Reserved_Flag_6 = ((Flags & 0x40) > 0) ? true : false;
                //Reserved_Flag_5 = ((Flags & 0x20) > 0) ? true : false;
                //Reserved_Flag_4 = ((Flags & 0x10) > 0) ? true : false;
                Local_Control_FLAG_2 = ((Flags_2 & 0x08) > 0) ? true : false;
                Remotely_Control_FLAG_2 = ((Flags_2 & 0x04) > 0) ? true : false;
                Under_Volt_FLAG_2 = ((Flags_2 & 0x02) > 0) ? true : false;
                Over_Volt_FLAG_2 = ((Flags_2 & 0x01) > 0) ? true : false;
            }
        }
        [XmlElement("FlagWakeUpProfile3", Type = typeof(byte))]
        public byte FLAG_WAKEUP_PROFILE_3
        {
            get
            {
                byte Flags_3 = 0x00;
                //Flags += (Reserved_Flag_7) ? (byte)0x80 : (byte)0x00;
                //Flags += (Reserved_Flag_6) ? (byte)0x40 : (byte)0x00;
                //Flags += (Reserved_Flag_5) ? (byte)0x20 : (byte)0x00;
                //Flags += (Reserved_Flag_4) ? (byte)0x10 : (byte)0x00;
                Flags_3 += (Local_Control_FLAG_3) ? (byte)0x08 : (byte)0x00;
                Flags_3 += (Remotely_Control_FLAG_3) ? (byte)0x04 : (byte)0x00;
                Flags_3 += (Under_Volt_FLAG_3) ? (byte)0x02 : (byte)0x00;
                Flags_3 += (Over_Volt_FLAG_3) ? (byte)0x01 : (byte)0x00;
                return Flags_3;
            }
            set
            {
                ///Decoding Flags Byte
                byte Flags_3 = value;
                //Reserved_Flag_7 = ((Flags & 0x80) > 0) ? true : false;
                //Reserved_Flag_6 = ((Flags & 0x40) > 0) ? true : false;
                //Reserved_Flag_5 = ((Flags & 0x20) > 0) ? true : false;
                //Reserved_Flag_4 = ((Flags & 0x10) > 0) ? true : false;
                Local_Control_FLAG_3 = ((Flags_3 & 0x08) > 0) ? true : false;
                Remotely_Control_FLAG_3 = ((Flags_3 & 0x04) > 0) ? true : false;
                Under_Volt_FLAG_3 = ((Flags_3 & 0x02) > 0) ? true : false;
                Over_Volt_FLAG_3 = ((Flags_3 & 0x01) > 0) ? true : false;
            }
        }
        [XmlElement("FlagWakeUpProfile4", Type = typeof(byte))]
        public byte FLAG_WAKEUP_PROFILE_4
        {
            get
            {
                byte Flags_4 = 0x00;
                //Flags += (Reserved_Flag_7) ? (byte)0x80 : (byte)0x00;
                //Flags += (Reserved_Flag_6) ? (byte)0x40 : (byte)0x00;
                //Flags += (Reserved_Flag_5) ? (byte)0x20 : (byte)0x00;
                //Flags += (Reserved_Flag_4) ? (byte)0x10 : (byte)0x00;
                Flags_4 += (Local_Control_FLAG_4) ? (byte)0x08 : (byte)0x00;
                Flags_4 += (Remotely_Control_FLAG_4) ? (byte)0x04 : (byte)0x00;
                Flags_4 += (Under_Volt_FLAG_4) ? (byte)0x02 : (byte)0x00;
                Flags_4 += (Over_Volt_FLAG_4) ? (byte)0x01 : (byte)0x00;
                return Flags_4;
            }
            set
            {
                ///Decoding Flags Byte
                byte Flags_4 = value;
                //Reserved_Flag_7 = ((Flags & 0x80) > 0) ? true : false;
                //Reserved_Flag_6 = ((Flags & 0x40) > 0) ? true : false;
                //Reserved_Flag_5 = ((Flags & 0x20) > 0) ? true : false;
                //Reserved_Flag_4 = ((Flags & 0x10) > 0) ? true : false;
                Local_Control_FLAG_4 = ((Flags_4 & 0x08) > 0) ? true : false;
                Remotely_Control_FLAG_4 = ((Flags_4 & 0x04) > 0) ? true : false;
                Under_Volt_FLAG_4 = ((Flags_4 & 0x02) > 0) ? true : false;
                Over_Volt_FLAG_4 = ((Flags_4 & 0x01) > 0) ? true : false;
            }
        }

        #endregion

        #region ICustomStructure Members

        public byte[] Encode_Data()
        {
            try
            {
                int temp = 0;
                List<byte> encodeRaw = new List<byte>(25);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A02_structure, 10 });
                ///Wake_Up_Profile_ID <DataType DataTypes._A11_unsigned>

                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.Wake_Up_Profile_ID));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, temp));

                ///IP_Profile_ID <DataType DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.IP_Profile_ID_1));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.IP_Profile_ID_2));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.IP_Profile_ID_3));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.IP_Profile_ID_4));
                ///RAW_Wake_Up_Profiles <DataType DataTypes._A11_unsigned>
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.FLAG_WAKEUP_PROFILE_1));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.FLAG_WAKEUP_PROFILE_2));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.FLAG_WAKEUP_PROFILE_3));
                encodeRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, this.FLAG_WAKEUP_PROFILE_4));


                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_Wake_UP_Profiles", "Encode_Data_Param_Wake_UP_Profiles", ex);
            }
        }

        public void Decode_Data(byte[] Data)
        {
            int array_travers = 0;
            Decode_Data(Data, ref array_travers, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                int temp = 0;
                byte currentByte = Data[array_traverse++];
                if (currentByte != (byte)DataTypes._A02_structure && Data[array_traverse] != 10)
                    throw new DLMSDecodingException("Invalid ICustomStructure Param_Wake_Up_Profiles Structure received", "Decode_Data_Param_Wake_Up_Profiles");
                array_traverse++;
                ///Wake_Up_Profile_ID <DataType DataTypes._A11_unsigned>
                this.Wake_Up_Profile_ID = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                //IP <DataTypes._A06_double_long_unsigned>
                temp = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));

                this.IP_Profile_ID_1 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.IP_Profile_ID_2 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.IP_Profile_ID_3 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.IP_Profile_ID_4 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                ///RAW_Wake_Up_Profiles <DataType DataTypes._A11_unsigned>
                this.FLAG_WAKEUP_PROFILE_1 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.FLAG_WAKEUP_PROFILE_2 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.FLAG_WAKEUP_PROFILE_3 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
                this.FLAG_WAKEUP_PROFILE_4 = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse));
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSDecodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding ICustomStructure Param_Wake_Up_Profiles", "Decode_Data_Param_Wake_Up_Profiles", ex);
            }
        }

        #endregion

        #region ICloneable Members
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Param_WakeUp_ProfileHelper : INotifyPropertyChanged
    {
        public static string PropertyName = "Param_Wakeup_Profile_object";
        public static int Max_WakeUp_Profile = 04;
        private int previous_Total_wakeup_profiles = 0;
        private Param_WakeUp_Profile[] _Param_WakeUp_Profile_object = null;

        public Param_WakeUp_Profile[] Param_WakeUp_Profile_object
        {
            get { return _Param_WakeUp_Profile_object; }
            set { _Param_WakeUp_Profile_object = value; }
        }

        public int Previous_Total_Wakeup_profiles
        {
            get { return previous_Total_wakeup_profiles; }
            set { previous_Total_wakeup_profiles = value; }
        }

        public int Total_Wakeup_Profile
        {
            get
            {
                int count = 0;
                foreach (var ipParam in _Param_WakeUp_Profile_object)
                {
                    if (ipParam != null && count < ipParam.Wake_Up_Profile_ID)
                        count = ipParam.Wake_Up_Profile_ID;
                }
                return count;
            }
        }

        #region Constructor

        public Param_WakeUp_ProfileHelper()
        {
            PropertyChanged += new PropertyChangedEventHandler((object sender, PropertyChangedEventArgs property) => { });
            _Param_WakeUp_Profile_object = new Param_WakeUp_Profile[Max_WakeUp_Profile];
            ///Param_Wakeup Profiles
            for (int index = 0; index < Max_WakeUp_Profile; index++)
                _Param_WakeUp_Profile_object[index] = new Param_WakeUp_Profile()
                {
                    Wake_Up_Profile_ID = 0,
                    IP_Profile_ID_1 = 0,
                    IP_Profile_ID_2 = 0,
                    IP_Profile_ID_3 = 0,
                    IP_Profile_ID_4 = 0
                };
        }

        public Param_WakeUp_ProfileHelper(Param_WakeUp_Profile[] _Param_WakeUp_Profile_objectArg)
        {
            PropertyChanged += new PropertyChangedEventHandler((object sender, PropertyChangedEventArgs property) => { });
            //_Param_WakeUp_Profile_object = new Param_WakeUp_Profile[Max_WakeUp_Profile];
            //for (int index = 0; index < Max_WakeUp_Profile; index++)
            //    _Param_WakeUp_Profile_object[index] = _Param_WakeUp_Profile_objectArg[index];
            _Param_WakeUp_Profile_object = _Param_WakeUp_Profile_objectArg;
        }

        #endregion

        #region AddParam_Wakeup_Profile

        public Param_WakeUp_Profile AddParam_Wakeup_Profile(Param_WakeUp_Profile Arg)
        {
            byte total_wakeUp_Profile_Count = (byte)Total_Wakeup_Profile;
            if (total_wakeUp_Profile_Count < Max_WakeUp_Profile)
            {
                _Param_WakeUp_Profile_object[total_wakeUp_Profile_Count] = Arg;
                Arg.Wake_Up_Profile_ID = (byte)(total_wakeUp_Profile_Count + 1);
            }
            else
                throw new Exception(String.Format("Unable to Add Wakeup Profile {0}", Arg));
            OnPropertyChanged(PropertyName);
            return Arg;
        }

        public Param_WakeUp_Profile AddParam_Wakeup_Profile()
        {
            Param_WakeUp_Profile Param_WakeUp_Profile = new Param_WakeUp_Profile()
            {
                Wake_Up_Profile_ID = 0,
                IP_Profile_ID_1 = 0,
                IP_Profile_ID_2 = 0,
                IP_Profile_ID_3 = 0,
                IP_Profile_ID_4 = 0
            };
            return AddParam_Wakeup_Profile(Param_WakeUp_Profile);
        }

        public Param_WakeUp_Profile GetLastParam_WakeUp_Profile()
        {
            Param_WakeUp_Profile Param_WakeUp_Profile = null;
            byte total_WakeUp_Profile_Count = (byte)Total_Wakeup_Profile;
            if (total_WakeUp_Profile_Count > 0)
                Param_WakeUp_Profile = _Param_WakeUp_Profile_object[total_WakeUp_Profile_Count - 1];
            return Param_WakeUp_Profile;
        }

        #endregion

        #region RemoveParam_IP_Profiles

        public Param_WakeUp_Profile RemoveParam_WakeUp_Profile()
        {
            try
            {
                Param_WakeUp_Profile Arg = null;
                byte total_Wake_Profile_Count = (byte)Total_Wakeup_Profile;
                if (total_Wake_Profile_Count > 0)
                {
                    Arg = _Param_WakeUp_Profile_object[total_Wake_Profile_Count - 1];
                    if (Arg != null)
                        Arg.Wake_Up_Profile_ID = 0;
                }
                else
                    throw new Exception(String.Format("Unable to Remove WakeUp Profile {0}", Arg));
                OnPropertyChanged(PropertyName);
                return Arg;
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException)
                    throw new Exception(String.Format("Unable to Remove IP Profile {0}", ex.Message), ex);
                else
                    throw ex;
            }
        }

        #endregion

        public static Param_WakeUp_Profile[] Param_Wakeup_Profile_object_initialze(int Instances)
        {
            Param_WakeUp_Profile[] Param_Wakeup_Profile_object = new Param_WakeUp_Profile[Instances];
            int count;
            for (count = 0; count < Instances; count++)
            {
                Param_Wakeup_Profile_object[count] = new Param_WakeUp_Profile();
            }
            return Param_Wakeup_Profile_object;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                //Raising the event when FirstName or LastName property value changed
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public interface IParam
    { }
}
