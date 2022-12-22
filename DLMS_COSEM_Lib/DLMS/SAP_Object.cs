using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DLMS
{
    #region SAP_Object
    
    /// <summary>
    /// An instance of SAP_Object assignment holds information about the addresses (Service_Access_Points, SAPs)
    /// of logical devices within a physical Metering device.
    /// SAP_Object used by the ACSE (Assosication_Control_Of_Service_Element) 
    /// to create associations,for example following method 
    /// <see cref="DLMS.ApplicationProcess_AARQ_Controller.AARQ_Helper(SAP_Object, SAP_Object, String)"/>
    /// will be used to create Association with DLMS_COSEM Compliant Server device(Remote Metering Device).The SAP_Object will be used in 
    /// <see cref="DLMS.Class_17"/> DLMS IC(Interface_Class) to store the Logical devices informaton (SAPs) read from the Metering Device.
    /// </summary>
    /// <example>
    /// //Create Current SAP_Object instance Initialize
    /// SAP_Object current_SAP = new SAP_Object("MTI0R28300000786",0x10);
    /// </example>
    [XmlInclude(typeof(SAP_Object))]
    public class SAP_Object : IComparable<SAP_Object>
    {
        internal string _SAP_Name;               
        internal UInt16 _SAP_Address;

        /// <summary>
        /// The String identifier represent the SAP name
        /// </summary>
        /// <remarks>
        /// The SAP_Name string will uniquely identify the particular SAP in Physical Meter Device
        /// </remarks>
        /// <example>
        /// "MTI0R28300000786"
        /// </example>
        [XmlElement(ElementName = "SAPName", Type = typeof(String))]
        public string SAP_Name
        {
            get { return _SAP_Name; }
            set { _SAP_Name = value; }
        }
        
        /// <summary>
        /// The numeric static identifier represent the SAP Address
        /// </summary>
        /// <remarks>
        ///  The SAP_Address identifier will uniquely identify the particular SAP in Physical Meter Device.
        ///  The following identifier will be used by +COMWrapper <see cref="DLMS.WrapperLayer"/> by
        ///  COSEM_Transport Layer after creation of DLMS_Association with Meter Device.
        /// </remarks>
        [XmlElement(ElementName = "SAPAddress", Type = typeof(ushort))]
        public UInt16 SAP_Address
        {
            get { return _SAP_Address; }
            set { _SAP_Address = value; }
        }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public SAP_Object()
        {
            SAP_Name = "MTI0326G0000002";
            SAP_Address = 01;
        }


        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="Sap_Name"><see cref="SAP_Name"/></param>
        /// <param name="Sap_Address"><see cref="SAP_Address"/></param>
        public SAP_Object(string Sap_Name, UInt16 Sap_Address)
        {
            SAP_Name = Sap_Name;
            SAP_Address = Sap_Address;
        }


        public SAP_Object(UInt16 Sap_Address)
            : this("MTI0326G0000002", Sap_Address)
        {

        }


        public SAP_Object(SAP_Object OtherObj)
        {
            SAP_Name = OtherObj.SAP_Name;
            SAP_Address = OtherObj.SAP_Address;
        }


        /// <summary>
        /// return the current SAP name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string TOReturn = SAP_Name;

            if (!String.IsNullOrEmpty(TOReturn))
            {
                TOReturn = SAP_Address.ToString("X4");
            }

            return TOReturn;
        }

        public override bool Equals(object obj)
        {
            if (obj is SAP_Object)
            {
                SAP_Object other = (SAP_Object)obj;
                
                if (this._SAP_Address == other._SAP_Address)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public int CompareTo(SAP_Object other)
        {
            return this.SAP_Address.CompareTo(other.SAP_Address);
        }
    }

    #endregion
}
