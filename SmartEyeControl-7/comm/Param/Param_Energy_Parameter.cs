using System;
using DLMS;
using System.Xml.Serialization;
using DLMS.Comm;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_Energy_Parameter))]
    public class Param_Energy_Parameter : IParam, ICloneable
    {
        [XmlElement("EnergyQuadrant1", Type = typeof(bool))]
        public bool Quad1;
        [XmlElement("EnergyQuadrant2", Type = typeof(bool))]
        public bool Quad2;
        [XmlElement("EnergyQuadrant3", Type = typeof(bool))]
        public bool Quad3;
        [XmlElement("EnergyQuadrant4", Type = typeof(bool))]
        public bool Quad4;
        [XmlElement("IsReady", Type = typeof(bool))]
        public bool isReady;

        #region Encoder/Decoder Functions
        
        public Base_Class Encode_energy_parameters(GetSAPEntry CommObjectGetter)
        {
            Class_1 energy_parameters = (Class_1)CommObjectGetter.Invoke(Get_Index._EneryParams);
            energy_parameters.EncodingAttribute = 2;
            energy_parameters.EncodingType = DataTypes._A09_octet_string;

            byte[] myIntArray = new byte[4];
            myIntArray[0] = (byte)((Quad1) ? 1 : 0);
            myIntArray[1] = (byte)((Quad2) ? 1 : 0);
            myIntArray[2] = (byte)((Quad3) ? 1 : 0);
            myIntArray[3] = (byte)((Quad4) ? 1 : 0);
            energy_parameters.Value_Array = myIntArray;
            return energy_parameters;
        }

        public void Decode_energy_parameters(Base_Class arg)
        {
            try
            {
                Class_1 energy_parameters = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                if (energy_parameters.GetAttributeDecodingResult(2) == DecodingResult.Ready &&
                    energy_parameters.Value_Array != null &&
                    energy_parameters.Value_Array.GetType() == typeof(byte[]) &&
                    energy_parameters.Value_Array.Length == 4)
                {
                    byte[] myIntArray = (byte[])energy_parameters.Value_Array;
                    Quad1 = (myIntArray[0] != 0) ? true : false;
                    Quad2 = (myIntArray[1] != 0) ? true : false;
                    Quad3 = (myIntArray[2] != 0) ? true : false;
                    Quad4 = (myIntArray[3] != 0) ? true : false;

                    isReady = true;
                }
                else
                    isReady = false;/// throw new Exception("Energy_Parameters get failure");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        
        #endregion

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
