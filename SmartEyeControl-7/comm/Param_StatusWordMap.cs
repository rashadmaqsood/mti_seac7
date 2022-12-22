using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using DLMS.Comm;
using System.Xml.Serialization;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_StatusWordMap))]
    public class Param_StatusWordMap : ICustomStructure, IParam, ICloneable
    {
        [XmlElement("StatusWordList", Type = typeof(List<StatusWord>))]
        public List<StatusWord> StatusWordList { get; set; }

        public Param_StatusWordMap()
        {
            StatusWordList = new List<StatusWord>();
        }

        public byte[] Encode_Data()
        {
            try
            {
                List<byte> encodeRaw = new List<byte>(25);
                encodeRaw.AddRange(new byte[] { (byte)DataTypes._A09_octet_string, (byte)StatusWordList.Count() });
                foreach (StatusWord item in StatusWordList)
                {
                    encodeRaw.Add((byte)item.Code);
                }
                return encodeRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException || ex is DLMSException)
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException("Error occurred while encoding ICustomStructure Param_StatusWord", "Encode_Data_Param_StatusWord", ex);
            }
        }

        public void Decode_Data(byte[] Data)
        {
            int arrayTraverser = 0;
            Decode_Data(Data, ref arrayTraverser, Data.Length);
        }

        public void Decode_Data(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                //byte currentByte = Data[array_traverse++];
                //if (currentByte != (byte)DataTypes._A09_octet_string)
                //    throw new DLMSDecodingException("Invalid ICustomStructure Param_StatusWord Octet String Should be received", "Decode_Data_Param_StatusWord");
                if (StatusWordList == null) StatusWordList = new List<StatusWord>();
                for (int i = 0; i < length; i++)
                {
                    StatusWord StatusWordItem = new StatusWord(Data[array_traverse++]);
                    StatusWordList.Add(StatusWordItem);
                }
            }
            catch (Exception)
            {
                throw new DLMSDecodingException("Error While Decoding Param_StatusWord", "Decode_Data_Param_StatusWord");
            }

        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    public enum StatusWordMapType
    {
        StatusWordMap_1,
        StatusWordMap_2
    }

    [Serializable]
    [XmlInclude(typeof(StatusWord))]
    public class StatusWord : ICloneable
    {
        #region Data_Members
        [XmlElement("Name", Type = typeof(string))]
        private string name;
        [XmlElement("Code", Type = typeof(byte))]
        private byte code;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public byte Code
        {
            get { return code; }
            set { code = value; }
        }

        #endregion

        public StatusWord(string name, byte code)
        {
            this.Name = name;
            this.Code = code;
        }

        public StatusWord(byte code)
        {
            this.Code = code;
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
