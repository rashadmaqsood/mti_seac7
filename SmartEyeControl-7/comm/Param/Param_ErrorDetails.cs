using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using comm;

namespace AccurateOptocomSoftware.comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_ErrorDetail))]
    public class Param_ErrorDetail:IParam
    {
        #region Data_Members

        private bool _check_BRE = false;
        private bool _check_CMMDI = false;
        private bool _check_RFU1 = false;
        private bool _check_RFU2 = false;
        private string txt_EventDEtail = null;

        #endregion

        #region Properties

        [XmlElement("CheckBRE", typeof(bool))]
        public bool Check_BRE
        {
            get { return _check_BRE; }
            set { _check_BRE = value; }
        }

        [XmlElement("CheckCMMDI", typeof(bool))]
        public bool Check_CMMDI
        {
            get { return _check_CMMDI; }
            set { _check_CMMDI = value; }
        }

        [XmlElement("CheckRFU1", typeof(bool))]
        public bool Check_RFU1
        {
            get { return _check_RFU1; }
            set { _check_RFU1 = value; }
        }

        [XmlElement("CheckRFU2", typeof(bool))]
        public bool Check_RFU2
        {
            get { return _check_RFU2; }
            set { _check_RFU2 = value; }
        }

        [XmlElement("TextEventDEtail", typeof(string))]
        public string Text_EventDEtail
        {
            get { return txt_EventDEtail; }
            set { txt_EventDEtail = value; }
        }
 
        #endregion 

        #region Encoders/Decoder

        public void Decode_OctectString(byte[] octect_String)
        {
            try
            {
                txt_EventDEtail = Encoding.ASCII.GetString(octect_String);
                byte b = octect_String[0];
                _check_BRE = Convert.ToBoolean(b & 0x01);
                _check_CMMDI = Convert.ToBoolean(b & 0x02);

                _check_RFU1 = Convert.ToBoolean(b & 0x04);
                _check_RFU2 = Convert.ToBoolean(b & 0x08);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while decoding Param_ErrorDetails", ex);
            }
        }
 
        #endregion
    }
}
