using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using comm;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using DLMS;
using System.Security.Permissions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_Generel_Process))]
    public class Param_Generel_Process : ICloneable, ISerializable,IParam
    {
         #region Data_Member
        
        private bool _sVS;
       
        
        #endregion

        #region Properties

        [XmlIgnore()]
        public bool IsSVS
        {
            get { return _sVS; }
            set { _sVS = value; }
        }
        
        [XmlIgnore()]
        public byte Flags
        {
            get
            {
                byte Flags = 0;
                if (IsSVS) Flags |= 0x01;
                return Flags;
            }
            set
            {
                byte Flags = 0;
                Flags = value;
                IsSVS = (Flags & 0x01) != 0;
                
            }
        }

     
 
        #endregion

        public Param_Generel_Process() { }

        #region Encoder_Decoder

        public Base_Class Encode_Param_Generel_Process(GetSAPEntry CommObjectGetter)
        {
            Class_1 com_Obj_Gpp = null;
            try
            {
                com_Obj_Gpp = (Class_1)CommObjectGetter.Invoke(Get_Index.General_Process_Param);
                com_Obj_Gpp.EncodingAttribute = 2;
                com_Obj_Gpp.EncodingType = DLMS.Comm.DataTypes._A09_octet_string;
                com_Obj_Gpp.Value_Array = Encode_Buffer();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DLMSEncodingException) || ex.GetType() == typeof(DLMSException))
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding Param_Generel_Process", "Param_Generel_Process", ex);
            }

            return com_Obj_Gpp;
        }

        public void Decode_Param_Generel_Process(Base_Class arg)
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
                    throw new DLMSEncodingException("Error occurred while Decoding Param_Generel_Process", "Param_Generel_Process", ex);
            }
        } 
        #endregion

        public void Decode_Buffer(byte[] p_Buffer)
        {
            //init p_buffer
            Flags = p_Buffer[0];
            
        }

        public byte[] Encode_Buffer()
        {
            byte[] p_buffer = new byte[16];
            //init p_buffer
            p_buffer[0] = Flags;
           
            return p_buffer;
        }

        #region ISerializable_Members

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public Param_Generel_Process(SerializationInfo info, StreamingContext context)
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
