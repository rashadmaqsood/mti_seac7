using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Authentication;
using System.Security.Permissions;
using System.Xml.Serialization;
using comm;

namespace AccurateOptocomSoftware.comm.Param
{
    [Serializable]
    [XmlInclude(typeof(HeaderInfo))]
    public class HeaderInfo : ISerializable, IParam
    {
        #region Data_Members

        private string fileURL = String.Empty;
        private string configName = String.Empty;
        private string description = String.Empty;

        private List<ParamsCategory> paramCategory = null;
        private List<Params> paramList = null;

        #region Signature

        [XmlIgnore()]
        public const string DefaultSignature = "$(01234567890123456789)=*&#-";  ///Max 20 bytes for SHA1 or Max 16 bytes for MD5
        private HashAlgorithmType hashFunction = HashAlgorithmType.None;
        private string signature = DefaultSignature;
        private string signatureComputed = DefaultSignature;

        #endregion

        #endregion

        public HeaderInfo()
        {
            paramList = new List<Params>();
            paramCategory = new List<ParamsCategory>();
        }

        #region Properties

        [XmlElement("ConfigurationName", typeof(string))]
        public string ConfigName
        {
            get { return configName; }
            set { configName = value; }
        }

        [XmlElement("Description", typeof(string))]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [XmlElement("Signature", typeof(string))]
        public string Signature
        {
            get { return signature; }
            set { signature = value; }
        }

        [XmlElement("HashFunction", typeof(HashAlgorithmType))]
        public HashAlgorithmType HashFunction
        {
            get { return hashFunction; }
            set { hashFunction = value; }
        }

        [XmlIgnore()]
        public string SignatureComputed
        {
            get { return signatureComputed; }
            set { signatureComputed = value; }
        }

        [XmlIgnore()]
        public string FileURL
        {
            get { return fileURL; }
            set { fileURL = value; }
        }

        [XmlIgnore()]
        public List<ParamsCategory> ParamCategory
        {
            get { return paramCategory; }
            set { paramCategory = value; }
        }

        [XmlIgnore()]
        public List<Params> ParamList
        {
            get { return paramList; }
            set { paramList = value; }
        }

        #endregion

        #region ISerializable

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected HeaderInfo(SerializationInfo info, StreamingContext context)
        {
            ///Getting fileURL Type string
            ///this.fileURL = info.GetString("FileURL");
            ///Getting configName Type string
            this.configName = info.GetString("ConfigName");
            ///Getting description Type string
            this.description = info.GetString("Description");
            ///Getting HashFunction Type HashAlgorithmType
            this.hashFunction = (HashAlgorithmType)info.GetValue("HashFunction", typeof(HashAlgorithmType));
            /////Getting Signature Type String
            this.signature = info.GetString("Signature");
            ///Getting paramCategory Type Array Of Type ParamsCategory
            ParamsCategory[] TCat = (ParamsCategory[])info.GetValue("ParamsCategory", typeof(ParamsCategory[]));
            this.paramCategory = new List<ParamsCategory>(TCat);
            ///Getting paramList Type Array Of Type Params
            Params[] TParam = (Params[])info.GetValue("ParamsList", typeof(Params[]));
            this.paramList = new List<Params>(TParam);
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Adding fileURL Type string
            ///info.AddValue("FileURL", this.fileURL, typeof(String));
            ///Adding configName Type string
            info.AddValue("ConfigName", this.configName, typeof(String));
            ///Adding description Type string
            info.AddValue("Description", this.description, typeof(String));
            ///Adding HashFunction Type HashAlgorithmType
            info.AddValue("HashFunction", this.hashFunction, typeof(HashAlgorithmType));
            /////Adding Signature Type String
            info.AddValue("Signature", this.signature, typeof(String));
            ///Adding ParamCategory Type Array Of Type ParamsCategory
            info.AddValue("ParamsCategory", this.paramCategory.ToArray(), typeof(ParamsCategory[]));
            ///Adding ParamsList Type Array Of Type Params
            info.AddValue("ParamsList", this.paramList.ToArray(), typeof(Params[]));
        }

        #endregion
    }
}
