using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS.Comm
{
    /// <summary>
    /// Capture object is An attribute of a "Data", "Register", "Extended register", "Demand register", "Clock" or "Profile
    /// generic" object captured into the buffer of a "Profile generic object".
    /// </summary>
    public class CaptureObject : ICloneable
    {
        private ushort classId;
        private byte[] OBIS_Code;
        private byte attributeIndex;
        private ulong dataIndex;

        public CaptureObject()
        {
            this.StOBISCode = Get_Index.Dummy;
        }


        /// <summary>
        /// represent the owner class 
        /// </summary>
        public ushort ClassId
        {
            get { return classId; }
            set { classId = value; }
        }
        
        /// <summary>
        /// OBIS code to uniquely identify the data quantity
        /// </summary>
        public byte[] OBISCode
        {
            get { return OBIS_Code; }
            set { OBIS_Code = value; }
        }

        public StOBISCode StOBISCode
        {
            get
            {
                StOBISCode TORet = Get_Index.Dummy;

                if (OBISCode != null &&
                    OBISCode.Length == 06 &&
                    ClassId > 0)
                {
                    TORet = StOBISCode.ConvertFrom(OBISCode);
                    TORet.ClassId = ClassId;
                }

                return TORet;
            }
            set
            {
                if (value.IsValidate)
                {
                    this.ClassId = value.ClassId;
                    this.OBISCode = value.OBISCode;
                }
                else
                {
                    // Set Get_Index.Dummy
                    ClassId = 0;
                    OBISCode = new byte[06];
                }
            }
        }


        /// <summary>
        /// index of an attribute to fetch the value in a particular object 
        /// </summary>
        public byte AttributeIndex
        {
            get { return attributeIndex; }
            set { attributeIndex = value; }
        }


        /// <summary>
        /// index to read the holding data in a particular object
        /// </summary>
        public ulong DataIndex
        {
            get { return dataIndex; }
            set { dataIndex = value; }
        }

        public string DatabaseFieldName { get; set; }
        public short Multiplier { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
