using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using DLMS;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DLMS.Comm;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(LoadProfileChannelInfo))]
    public class LoadProfileChannelInfo : IComparable<LoadProfileChannelInfo>, IParam, ICloneable
    {
        #region Data_Members

        private Get_Index OBIS;
        private string format;
        private float multiplier;
        private string quantity_name;
        private Unit unit;
        private int channel_id;
        private bool dataPresent;
        private TimeSpan capturePeriod;
        private byte selectedAttribute;
        private bool isDisplayData;

        #endregion

        #region Properties

        [XmlElement("OBISCode",typeof(Get_Index))]
        public Get_Index OBIS_Index
        {
            get { return OBIS; }
            set { OBIS = value; }
        }
        
        [XmlElement("Format", typeof(string))]
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        [XmlElement("Multiplier", typeof(float))]
        public float Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        [XmlElement("QuantityName", typeof(string))]
        public string Quantity_Name
        {
            get { return quantity_name; }
            set { quantity_name = value; }
        }

        [XmlElement("Unit", typeof(Unit))]
        public Unit Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        [XmlElement("IsDisplayData", typeof(bool))]
        public bool IsDisplayData
        {
            get { return isDisplayData; }
            set { isDisplayData = value; }
        }

        [XmlElement("Channel_Id", typeof(int))]
        public int Channel_id
        {
            get { return channel_id; }
            set { channel_id = value; }
        }

        [XmlElement("IsDataPresent", typeof(bool))]
        public bool IsDataPresent
        {
            get { return dataPresent; }
            set { dataPresent = value; }
        }

        [XmlIgnore()]
        public TimeSpan CapturePeriod
        {
            get { return capturePeriod; }
            set { capturePeriod = value; }
        }

        [XmlElement("CapturePeriod", typeof(long))]
        public long CapturePeriodTicks
        {
            get { return capturePeriod.Ticks; }
            set 
            { 
                capturePeriod = new TimeSpan(value); 
            }
        }

        [XmlElement("SelectedAttribute", typeof(byte))]
        public byte SelectedAttribute
        {
            get { return selectedAttribute; }
            set { selectedAttribute = value; }
        } 
        #endregion

        #region Constructur

        public LoadProfileChannelInfo()
        {
            Channel_id = 0;
            this.CapturePeriod = new TimeSpan();
            IsDataPresent = false;
            SelectedAttribute = 0;
            Unit = Unit.UnitLess;
            OBIS_Index = Get_Index.Dummy;
            this.Quantity_Name = OBIS_Index.ToString();
            this.Format = "f3";
            this.Multiplier = 1.0f;
        } 
        #endregion

        #region IComparable<LoadProfileChannelInfo> Members

        public int CompareTo(LoadProfileChannelInfo other)
        {
            if (this.OBIS_Index == other.OBIS_Index && this.Channel_id == other.Channel_id &&
                this.SelectedAttribute == other.SelectedAttribute &&
                this.Quantity_Name.Equals(other.Quantity_Name) && this.Format.Equals(other.Format) &&
                this.Multiplier == other.Multiplier && this.Unit == other.Unit)
            {
                return 0;
            }
            else
               return this.Channel_id.CompareTo(other.Channel_id);
        }
        #endregion

        public CaptureObject ConvertToCaptureObject()
        {
            StOBISCode OBIS = OBIS_Index;
            CaptureObject objCapture = new CaptureObject()
            {
                AttributeIndex = selectedAttribute,
                OBISCode = OBIS.OBISCode,
                ClassId = OBIS.ClassId
            };
            return objCapture;
        }

        public override string ToString()
        {
            String str = Quantity_Name;
            if (String.IsNullOrEmpty(str))
                str = OBIS_Index.ToString();
            return str;
        }

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
    
    }
}
