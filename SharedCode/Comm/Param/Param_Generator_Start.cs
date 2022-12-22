using DLMS;
using DLMS.Comm;
using SharedCode.Comm.HelperClasses;
using SharedCode.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{

    [Serializable]
    [XmlInclude(typeof(Param_Generator_Start))]
    public class Param_Generator_Start : ISerializable, IParam, ICloneable
    {
        private TimeSpan _generatorStart_MonitoringTime;
        private byte _generatorStart_Tariff;
       
        #region Properties

        [XmlIgnore()]
        public TimeSpan GeneratorStart_MonitoringTime
        {
            get { return _generatorStart_MonitoringTime; }
            set { _generatorStart_MonitoringTime = value; }
        }

        [XmlIgnore()]
        public byte GeneratorStart_Tariff
        {
            get { return _generatorStart_Tariff; }
            set { _generatorStart_Tariff = value; }
        }

        #endregion

        #region Constuctor

        public Param_Generator_Start()
        {
        }

        #endregion

        #region Member Methods


        public Base_Class Encode_Generator_Start_MonitorTime(GetSAPEntry CommObjectGetter)
        {
            Class_3 MonitoringTime_Start_Generatorl_MT =
                (Class_3)Param_Monitoring_Time.EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_GENERATOR_START, this.GeneratorStart_MonitoringTime);

            return MonitoringTime_Start_Generatorl_MT;
        }
        public Base_Class Encode_Generator_Start_Tariff(GetSAPEntry CommObjectGetter)
        {
            Class_1 Start_Generator_Tariff = (Class_1)CommObjectGetter.Invoke(Get_Index.TARIFF_ON_GENERATOR);
            Start_Generator_Tariff.EncodingAttribute = 0x02;
            Start_Generator_Tariff.EncodingType = DataTypes._A11_unsigned;
            Start_Generator_Tariff.Value = this.GeneratorStart_Tariff;
            return Start_Generator_Tariff;
        }


        #endregion

        #region ISerializable interface Members


        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public Param_Generator_Start(SerializationInfo info, StreamingContext context)
        {
            this._generatorStart_MonitoringTime = (TimeSpan)info.GetValue("MonitorTimeGeneratorStart", typeof(TimeSpan));
            this._generatorStart_Tariff = (byte)info.GetValue("TariffOnGenerator", typeof(byte));
        }


        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                ///Adding StartDate Type DateTime
                info.AddValue("MonitorTimeGeneratorStart", (this.GeneratorStart_MonitoringTime != null) ? this.GeneratorStart_MonitoringTime : new TimeSpan(0,0,0), typeof(TimeSpan));
                ///Adding EndDate Type DateTime
                info.AddValue("TariffOnGenerator", this.GeneratorStart_Tariff, typeof(byte));
                ///Adding ListLoadScheduling Type List<LoadScheduling>
                //info.AddValue("ListLoadScheduling", this.ListLoadScheduling, typeof(List<LoadScheduling>));
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ICloneable interface Members
        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(50))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Clone Param_Generator_Start ", ex);
            }
        }
        #endregion
    }
}
