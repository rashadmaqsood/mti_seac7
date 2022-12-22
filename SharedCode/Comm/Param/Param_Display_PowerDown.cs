using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DLMS;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_Display_PowerDown))]
    public class Param_Display_PowerDown :  ICloneable, ISerializable,IParam
    {
        #region Data_Member
        
        private bool is_alwaysOn;
        private bool isImmidateOff;
        private bool isDisplayOnByButton;
        private bool isDisplayRepeat;
        private bool onTimeCycleScroll;
        private byte _onTime;
        private byte _OffTime;
        private byte _offDelay; 
        
        #endregion

        #region Properties

        [XmlIgnore()]
        public bool IsAlwaysOn
        {
            get { return is_alwaysOn; }
            set { is_alwaysOn = value; }
        }
        [XmlIgnore()]
        public bool IsImmidateOff
        {
            get { return isImmidateOff; }
            set { isImmidateOff = value; }
        }
        [XmlIgnore()]
        public bool IsDisplayOnByButton
        {
            get { return isDisplayOnByButton; }
            set { isDisplayOnByButton = value; }
        }
        [XmlIgnore()]
        public bool IsOnTimeCycleScroll
        {
            get { return onTimeCycleScroll; }
            set { onTimeCycleScroll = value; }
        }
        [XmlIgnore()]
        public bool IsDisplayRepeat
        {
            get { return isDisplayRepeat; }
            set { isDisplayRepeat = value; }
        }
        [XmlIgnore()]
        private bool isOnMinute_Sec;
        [XmlIgnore()]
        public bool IsOnMinute_Sec
        {
            get { return isOnMinute_Sec; }
            set { isOnMinute_Sec = value; }
        }
        [XmlIgnore()]
        private bool isOffMinute_Sec;
        [XmlIgnore()]
        public bool IsOffMinute_Sec
        {
            get { return isOffMinute_Sec; }
            set { isOffMinute_Sec = value; }
        }
        [XmlIgnore()]
        public byte Flags
        {
            get
            {
                byte Flags = 0;

                if (IsAlwaysOn) Flags |= 0x01;
                if (IsImmidateOff) Flags |= 0x02;
                if (IsOnTimeCycleScroll) Flags |= 0x04;
                if (IsDisplayRepeat) Flags |= 0x08;
                if (!IsDisplayOnByButton) Flags |= 0x20;
                if (IsOnMinute_Sec) Flags |= 0x40;
                if (IsOffMinute_Sec) Flags |= 0x80;


                return Flags;
            }
            set
            {
                byte Flags = 0;

                Flags = value;

                IsAlwaysOn = (Flags & 0x01) != 0;
                IsImmidateOff = (Flags & 0x02) != 0;
                IsOnTimeCycleScroll = (Flags & 0x04) != 0;
                IsDisplayRepeat = (Flags & 0x08) != 0;
                IsDisplayOnByButton = !((Flags & 0x20) != 0); // Inverse logic
                IsOnMinute_Sec = (Flags & 0x40) != 0;
                IsOffMinute_Sec = (Flags & 0x80) != 0;
            }
        }

        [XmlIgnore()]
        [DefaultValueAttribute(45)]
        public byte OnTime
        {
            get { return _onTime; }
            set { _onTime = value; }
        }

        [XmlIgnore()]
        [DefaultValueAttribute(120)]
        public byte OffTime
        {
            get { return _OffTime; }
            set { _OffTime = value; }
        }

        [XmlIgnore()]
        [DefaultValueAttribute(05)]
        public byte OffDelay
        {
            get { return _offDelay; }
            set { _offDelay = value; }
        }
 
        #endregion

        public Param_Display_PowerDown()
        {

        }                

        #region Encoder_Decoder

        public Base_Class Encode_Param_Display_PowerDown(GetSAPEntry CommObjectGetter)
        {
            Class_1 com_Obj_Disp_PDown = null;
            try
            {
                com_Obj_Disp_PDown = (Class_1)CommObjectGetter.Invoke(Get_Index.Display_At_Power_Down_Mode);
                com_Obj_Disp_PDown.EncodingAttribute = 2;
                com_Obj_Disp_PDown.EncodingType = DLMS.Comm.DataTypes._A09_octet_string;
                com_Obj_Disp_PDown.Value_Array = Encode_Buffer();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding Param_Display_PowerDown", "Param_Display_PowerDown", ex);
            }

            return com_Obj_Disp_PDown;
        }

        public void Decode_Param_Display_PowerDown(Base_Class arg)
        {
            Class_1 com_Obj_Disp_PDown = null;
            try
            {
                com_Obj_Disp_PDown = (Class_1)arg;
                if (com_Obj_Disp_PDown.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    byte[] p_buffer = (byte[])com_Obj_Disp_PDown.Value_Array;
                    //init p_buffer
                    Decode_Buffer(p_buffer);
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while Decoding Param_Display_PowerDown", "Param_Display_PowerDown", ex);
            }
        } 
        #endregion

        public void Decode_Buffer(byte[] p_Buffer)
        {
            //init p_buffer
            Flags = p_Buffer[0];
            OffDelay = p_Buffer[1];
            OnTime = p_Buffer[2];
            OffTime = p_Buffer[3];
        }

        public byte[] Encode_Buffer()
        {
            byte[] p_buffer = new byte[16];
            //init p_buffer
            p_buffer[0] = Flags;
            p_buffer[1] = OffDelay;
            p_buffer[2] = OnTime;
            p_buffer[3] = OffTime;
            return p_buffer;
        }

        #region ISerializable_Members

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public Param_Display_PowerDown(SerializationInfo info, StreamingContext context)
        {
            byte[] P_Buffer = (byte[])info.GetValue("Raw_Encoded_Buffer", typeof(byte[]));
            Decode_Buffer(P_Buffer);
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //Init P_Buffer
            byte[] P_Buffer = Encode_Buffer();
            ///Adding P_Buffer
            info.AddValue("Raw_Encoded_Buffer", P_Buffer);
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
