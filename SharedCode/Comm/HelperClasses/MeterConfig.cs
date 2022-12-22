using System;
using System.Xml.Serialization;
using DLMS.Comm;

namespace SharedCode.Comm.HelperClasses
{

    [XmlInclude(typeof(Manufacturer))]
    [XmlInclude(typeof(Configuration))]
    [XmlInclude(typeof(DeviceAssociation))]
    [XmlInclude(typeof(Device))]
    public class MeterConfig
    {
        private Device _device;
        private Manufacturer _manufacturer;
        private DeviceAssociation _deviceAssociation;
        private Configuration _configuration;
        private string _version;
        private string _dlmsVersion;
        private string _meterModelSTR;
        private string _meterType=string.Empty;


        [XmlElement(ElementName = "Device", Type = typeof(Device))]
        public Device Device
        {
            get { return _device; }
            set { _device = value; }
        }

        [XmlElement(ElementName = "Manufacturer", Type = typeof(Manufacturer))]
        public Manufacturer Manufacturer
        {
            get { return _manufacturer; }
            set { _manufacturer = value; }
        }

        [XmlElement(ElementName = "Device_Association", Type = typeof(DeviceAssociation))]
        public DeviceAssociation Device_Association
        {
            get { return _deviceAssociation; }
            set { _deviceAssociation = value; }
        }

        [XmlElement(ElementName = "Device_Configuration", Type = typeof(Configuration))]
        public Configuration Device_Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }

        // [XmlElement(ElementName = "id", Type = typeof(int))]
        // public int Id
        // {
        //     get { return _id; }
        //     set { _id = value; }
        // }

        [XmlElement(ElementName = "MeterModelSTR", Type = typeof(String))]
        public string MeterModelSTR
        {
            get { return _meterModelSTR; }
            set { _meterModelSTR = value; }
        }

        [XmlElement(ElementName = "Version", Type = typeof(String))]
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        [XmlElement(ElementName = "DLMSVersion", Type = typeof(String))]
        public string DLMSVersion
        {
            get { return _dlmsVersion; }
            set { _dlmsVersion = value; }
        }


        [XmlIgnore()]
        public string MeterModel
        {
            get
            {
                string meter_Model = string.Empty;

                try
                {
                    if (Device != null)
                    {
                        return meter_Model = Device.Model;
                    }
                }
                catch
                {
                    meter_Model = string.Empty;
                }

                return meter_Model;
            }
        }

        [XmlIgnore()]
        public string MeterType
        {
            get
            {
                return _meterType;
            }
        }
        public MeterConfig()
        {
            _device = null;
            _manufacturer = null;
            _deviceAssociation = null;
            _configuration = null;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="otherObj"></param>
        public MeterConfig(MeterConfig otherObj)
        {
            try
            {
                _device = new Device(otherObj._device);
                _manufacturer = new Manufacturer(otherObj._manufacturer);
                _deviceAssociation = new DeviceAssociation(otherObj._deviceAssociation);
                _configuration = new Configuration(otherObj._configuration);
            }
            catch (Exception ex)
            {
                throw new Exception("Error initializing the Meter Configuration", ex);
            }
        }

        public override string ToString()
        {
            string meterConfigSTR = string.Empty;
            try
            {
                meterConfigSTR = string.Format("{0} {1} {2} {3} {4}", _manufacturer, _device, MeterModelSTR, Version, _deviceAssociation);

            }
            catch
            {
                meterConfigSTR = "MeterConfig Error!";
            }
            return meterConfigSTR;
        }
    }


    [XmlInclude(typeof(Configuration))]
    public class Configuration
    {
        private int _id;
        private string _Name;

        [XmlElement(ElementName = "id", Type = typeof(int))]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [XmlElement(ElementName = "Name", Type = typeof(string))]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [XmlElement(ElementName = "LoadProfileGroupId", Type = typeof(int))]
        public int LoadProfileGroupId
        {
            get;
            set;
        }


        [XmlElement(ElementName = "BillItemsGroupId", Type = typeof(int))]
        public int BillItemsGroupId
        {
            get;
            set;
        }

        [XmlElement(ElementName = "DisplayWindowGroupId", Type = typeof(int))]
        public int DisplayWindowGroupId
        {
            get;
            set;
        }

        [XmlElement(ElementName = "EventGroupId", Type = typeof(int))]
        public int EventGroupId
        {
            get;
            set;
        }


        public Configuration()
        {
            _id = 1;
            _Name = String.Empty;
        }

        public Configuration(int Id, string Name, int LoadProfileGroupId, int BillItemsGroupId, int DisplayWindowGroupId, int EventGroupId)
        {
            _id = Id;
            _Name = Name;

            this.LoadProfileGroupId = LoadProfileGroupId;
            this.BillItemsGroupId = BillItemsGroupId;
            this.DisplayWindowGroupId = DisplayWindowGroupId;
            this.EventGroupId = EventGroupId;
        }

        public Configuration(int Id, string Name)
            : this(Id, Name, 1, 1, 1, 1)
        { }


        public Configuration(Configuration otherObj)
        {
            _id = otherObj.Id;
            _Name = otherObj.Name;

            this.LoadProfileGroupId = otherObj.LoadProfileGroupId;
            this.BillItemsGroupId = otherObj.BillItemsGroupId;
            this.DisplayWindowGroupId = otherObj.DisplayWindowGroupId;
            this.EventGroupId = otherObj.EventGroupId;
        }

        public override string ToString()
        {
            string retVal = string.Empty;
            retVal = string.Format("{0}_{1}", Id, Name);

            return retVal;
        }

    }

    public class Device
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string Model { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsSinglePhase { get; set; }

        public bool SkipSoftwareUserID { get; set; }

        public Device()
        {
            // Set Of Default Value
            Id = 0;
            DeviceName = string.Empty;
            Model = string.Empty;
            ManufacturerId = 0;

            SkipSoftwareUserID = false;
            IsSinglePhase = false;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="OtherObj"></param>
 
        public Device(Device OtherObj)
        {
            // Set Of Default Value
            Id = OtherObj.Id;
            DeviceName = OtherObj.DeviceName;
            Model = OtherObj.Model;
            ManufacturerId = OtherObj.ManufacturerId;

            SkipSoftwareUserID = OtherObj.SkipSoftwareUserID;
            IsSinglePhase = OtherObj.IsSinglePhase;
        }

        public override string ToString()
        {
            return this.DeviceName;
        }
    }

    public class DeviceAssociation
    {
        public int Id { get; set; }
        public string AssociationName { get; set; }
        public HLS_Mechanism AuthenticationType { get; set; }

        public ushort ClientSap { get; set; }
        public ushort MeterSap { get; set; }
        public int DeviceId { get; set; }

        public int ConfigurationId { get; set; }
        public int RightGroupId { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DeviceAssociation()
        {
            Id = 0;
            AssociationName = string.Empty;
            AuthenticationType = HLS_Mechanism.LowestSec;
            ClientSap = 0x10;
            MeterSap = 0x01;
            DeviceId = 0;
            ConfigurationId = 0;
            RightGroupId = 0;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        public DeviceAssociation(DeviceAssociation OtherObj)
        {
            Id = OtherObj.Id;
            AssociationName = OtherObj.AssociationName;
            AuthenticationType = OtherObj.AuthenticationType;
            ClientSap = OtherObj.ClientSap;
            MeterSap = OtherObj.MeterSap;
            DeviceId = OtherObj.DeviceId;
            ConfigurationId = OtherObj.ConfigurationId;
            RightGroupId = OtherObj.RightGroupId;
        }

        public override string ToString()
        {
            return this.AssociationName;
        }
    }

    public class Manufacturer
    {
        public int Id { get; set; }
        public string ManufacturerName { get; set; }
        public string Code { get; set; }

        public Manufacturer()
        {
            Id = 0;
            ManufacturerName = string.Empty;
            Code = String.Empty;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="OtherObj"></param>
        public Manufacturer(Manufacturer OtherObj)
        {
            Id = OtherObj.Id;
            ManufacturerName = OtherObj.ManufacturerName;
            Code = OtherObj.Code;
        }

        public override string ToString()
        {
            return this.ManufacturerName;
        }
    }

    public class AuthenticationType
    {
        public int Id { get; set; }
        public string AuthenticationTypeName { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AuthenticationType()
        {
            Id = Convert.ToByte(HLS_Mechanism.LowSec);
            AuthenticationTypeName = HLS_Mechanism.LowSec.ToString();
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        public AuthenticationType(AuthenticationType OtherObj)
        {
            Id = OtherObj.Id;
            AuthenticationTypeName = OtherObj.AuthenticationTypeName;
        }

        public override string ToString()
        {
            return this.AuthenticationTypeName;
        }
    }

}
